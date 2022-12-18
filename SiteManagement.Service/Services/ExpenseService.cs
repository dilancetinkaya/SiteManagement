using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MimeKit;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Domain.PaymentApiModel;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using SiteManagement.Infrastructure.IServices.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IPaymentAPIService _paymentAPIService;
        private readonly IFlatRepository _flatRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string AllExpenseKey = "EXPENSEALL";
        private const string ExpenseByRelationsKey = "EXPENSERELATİONS";
        private const string ExpenseByUsersKey = "EXPENSEUSER";
        private MemoryCacheEntryOptions _cacheOptions;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IFlatRepository flatRepository, IMemoryCache memoryCache, IPaymentAPIService paymentAPIService)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(10));
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _flatRepository = flatRepository;
            _paymentAPIService = paymentAPIService;
        }

        public async Task AddAsync(CreateExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);

            await _expenseRepository.AddAsync(expense);
            _memoryCache.Remove(AllExpenseKey);
            _memoryCache.Remove(ExpenseByRelationsKey);
            _memoryCache.Remove(ExpenseByUsersKey);
        }

        public async Task<ICollection<CreateExpenseDto>> AddRangeAsync(ICollection<CreateExpenseDto> expenseDtos)
        {
            var expenses = _mapper.Map<ICollection<Expense>>(expenseDtos);

            await _expenseRepository.AddRangeAsync(expenses);
            _memoryCache.Remove(AllExpenseKey);
            _memoryCache.Remove(ExpenseByRelationsKey);
            _memoryCache.Remove(ExpenseByUsersKey);
            return expenseDtos;
        }

        public async Task<ICollection<ExpenseDto>> GetAllAsync()
        {
            return await _memoryCache.GetOrCreateAsync(AllExpenseKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var expenses = await _expenseRepository.GetAllAsync();
                return _mapper.Map<ICollection<ExpenseDto>>(expenses);
            });
        }

        public async Task<ExpenseDto> GetByIdAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense is null) throw new Exception("Expense is not found");

            var expenseDto = _mapper.Map<ExpenseDto>(expense);
            return expenseDto;
        }

        public async Task RemoveAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense is null) throw new Exception("Expense is not found");

            _expenseRepository.Remove(expense);
            _memoryCache.Remove(AllExpenseKey);
            _memoryCache.Remove(ExpenseByRelationsKey);
            _memoryCache.Remove(ExpenseByUsersKey);
        }

        public async Task UpdateAsync(UpdateExpenseDto expenseDto, int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            expense.Price = expenseDto.Price;
            expense.IsPaid = expenseDto.IsPaid;
            expense.InvoiceDate = expenseDto.InvoiceDate;
            expense.FlatId = expenseDto.FlatId;
            expense.ExpenseTypeId = expenseDto.ExpenseTypeId;

            _expenseRepository.Update(expense);
            _memoryCache.Remove(AllExpenseKey);
            _memoryCache.Remove(ExpenseByRelationsKey);
            _memoryCache.Remove(ExpenseByUsersKey);

        }

        /// <summary>
        /// payment api ye ödeme ekleme
        /// </summary>
        public async Task<CreatePaymentDto> AddPayment(CreatePaymentDto createPaymentDto)
        {
            var paidExpense = await _expenseRepository.GetByIdAsync(createPaymentDto.ExpenseId);

            createPaymentDto.InvoiceAmount = paidExpense.Price;
            createPaymentDto.FlatId = paidExpense.FlatId;
            createPaymentDto.ExpenseId = paidExpense.Id;
            var payment = await _paymentAPIService.CreatePayment(createPaymentDto);
            if (payment.StatusCode == StatusCodes.Status500InternalServerError) throw new Exception(payment.Message);

            paidExpense.IsPaid = true;
            var updatedExpense = _expenseRepository.Update(paidExpense);
            _memoryCache.Remove(AllExpenseKey);
            _memoryCache.Remove(ExpenseByRelationsKey);
            _memoryCache.Remove(ExpenseByUsersKey);
            var paymentDto = _mapper.Map<CreatePaymentDto>(updatedExpense);
            return paymentDto;
        }

        /// <summary>
        /// expenseleri iliskili oldugu verilerle getirir
        /// </summary>
        public async Task<ICollection<ExpenseDto>> GetExpensesWithRelations()
        {
            return await _memoryCache.GetOrCreateAsync(ExpenseByRelationsKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var expenses = await _expenseRepository.GetExpensesWithRelations();

                var expenseDtos = expenses.Select(e => new ExpenseDto()
                {
                    Id = e.Id,
                    IsPaid = e.IsPaid,
                    Price = e.Price,
                    InvoiceDate = e.InvoiceDate,
                    ExpenseTypeId = e.ExpenseTypeId,
                    FlatId = e.FlatId,

                }).ToList();
                return expenseDtos;
            });
        }

        /// <summary>
        /// Kullanıcının tüm giderlerini getirir
        /// </summary>
        public async Task<ICollection<ExpenseDto>> GetExpensesWithUserIdAsync(string id)
        {
            return await _memoryCache.GetOrCreateAsync(ExpenseByUsersKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var userExpense = await _expenseRepository.GetExpensesWithUserIdAsync(id);

                var expenseDtos = userExpense.Select(e => new ExpenseDto()
                {
                    Id = e.Id,
                    IsPaid = e.IsPaid,
                    Price = e.Price,
                    InvoiceDate = e.InvoiceDate,
                    FlatId = e.FlatId,
                    ExpenseTypeId = e.ExpenseTypeId,
                }).ToList();
                return expenseDtos;
            });
        }

        /// <summary>
        /// otomatik olarak tüm dairelere borç ekleme
        /// </summary>
        public async Task AddDebtMultiple(DebtMultipleDto expenseDto)
        {
            var flats = _mapper.Map<ICollection<FlatDto>>(await _flatRepository.GetAllAsync());

            var expenseDtoList = flats.Select(f => new CreateExpenseDto()
            {
                FlatId = f.Id,
                InvoiceDate = DateTime.Now,
                Price = expenseDto.Price,
                ExpenseTypeId = expenseDto.ExpenseTypeId,
                IsPaid = false
            }).ToList();
            await AddRangeAsync(expenseDtoList);
            _memoryCache.Remove(AllExpenseKey);
            _memoryCache.Remove(ExpenseByRelationsKey);
            _memoryCache.Remove(ExpenseByUsersKey);
        }

        /// <summary>
        /// Verilen tarih araliginda olan expenseleri getirirne
        /// </summary>
        public async Task<ICollection<ExpenseDto>> GetDebtWithDate(DateTime startDate, DateTime endDate)
        {
            var expenses = await _expenseRepository.GetExpensesWithRelations();

            var expensesDate = expenses.Where(x => !x.IsPaid)
                                       .Where(x => x.InvoiceDate >= startDate && x.InvoiceDate <= endDate);
            var expensesDto = _mapper.Map<ICollection<ExpenseDto>>(expensesDate);
            return expensesDto;

        }

        /// <summary>
        /// Ödenmemiş faturaları olan kullanıcılara mail atma
        /// </summary> 
        public async Task SendMail()
        {
            var expenses = await _expenseRepository.GetExpensesWithRelations();

            foreach (var expense in expenses.Where(x => !x.IsPaid))
            {
                var email = expense.Flat.User.Email;
                var expenseType = expense.ExpenseType.ExpenseTypeName;


                MimeMessage mimeMessage = new();
                MailboxAddress mailboxAddressFrom = new("Site", "dilancetinkaya007@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);

                MailboxAddress mailboxAddressTo = new("User", email);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyByilder = new BodyBuilder();
                bodyByilder.TextBody = $"{expenseType} {expense.Price} tutarında ödenmemiş Faturanız mecvuttur";
                mimeMessage.Body = bodyByilder.ToMessageBody();
                mimeMessage.Subject = "Site Yönetimi";

                SmtpClient client = new();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("dilancetinkaya007@gmail.com", "syfgerdrnqeslexp");
                client.Send(mimeMessage);
            }

        }
    }
}