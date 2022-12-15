using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private IFlatRepository _flatRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IFlatRepository flatRepository)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _flatRepository = flatRepository;
        }

        public async Task AddAsync(CreateExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            await _expenseRepository.AddAsync(expense);
        }

        public async Task<ICollection<CreateExpenseDto>> AddRangeAsync(ICollection<CreateExpenseDto> expenseDtos)
        {
            var expenses = _mapper.Map<ICollection<Expense>>(expenseDtos);
            await _expenseRepository.AddRangeAsync(expenses);
            return expenseDtos;
        }

        public async Task<ICollection<ExpenseDto>> GetAllAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();
            return _mapper.Map<ICollection<ExpenseDto>>(expenses);
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
        }

        public UpdateExpenseDto Update(UpdateExpenseDto expenseDto, int id)
        {
            var updatedExpense = _mapper.Map<Expense>(expenseDto);
            updatedExpense.Id = id;
            _expenseRepository.Update(updatedExpense);
            return expenseDto;
        }
        public async Task<ICollection<ExpenseDto>> GetExpensesWithRelations()
        {
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
        }
        /// <summary>
        /// otomatik olarak tüm dairelere borç ekler
        /// </summary>
        /// <param name="expenseDto"></param>
        /// <returns></returns>
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
        }

        public async Task<ICollection<ExpenseDto>> GetExpensesWithUserIdAsync(string id)
        {
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
        }


        public async Task<ICollection<ExpenseDto>> GetMonthlyDebt(DateTime startDate, DateTime endDate)
        {
            var expenses = await _expenseRepository.GetExpensesWithRelations();
            var expensesDate = expenses.Where(x => !x.IsPaid)
                                       .Where(x => x.InvoiceDate >= startDate && x.InvoiceDate <= endDate);
           var expensesDto= _mapper.Map<ICollection<ExpenseDto>>(expensesDate);
           return expensesDto;

        }
        public async Task SendMail()
        {
            var expenses = await _expenseRepository.GetExpensesWithRelations();

            foreach (var expense in expenses.Where(x => !x.IsPaid))
            {
                var email = expense.Flat.User.Email;

                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Site Yönetimi", "dilancetinkaya007@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);

                MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyByilder = new BodyBuilder();
                bodyByilder.TextBody = "Ödenmemiş Faturanız mecvuttur";
                mimeMessage.Body = bodyByilder.ToMessageBody();
                mimeMessage.Subject = "Site Yönetimi";

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("dilancetinkaya007@gmail.com", "syfgerdrnqeslexp");
                client.Send(mimeMessage);
            }

        }
    }
}