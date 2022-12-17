using AutoMapper;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.PaymentApiModel;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Block, BlockDto>().ReverseMap();
            CreateMap<Block, CreateBlockDto>().ReverseMap();
            CreateMap<Block, UpdateBlockDto>().ReverseMap();

            CreateMap<Building, BuildingDto>().ReverseMap();
            CreateMap<Building, CreateBuildingDto>().ReverseMap();
            CreateMap<Building, UpdateBuildingDto>().ReverseMap();

            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Expense, CreateExpenseDto>().ReverseMap();
            CreateMap<Expense, UpdateExpenseDto>().ReverseMap();
            CreateMap<Expense, CreatePaymentDto>().ReverseMap();

            CreateMap<ExpenseType, ExpenseTypeDto>().ReverseMap();
            CreateMap<ExpenseType, CreateExpenseTypeDto>().ReverseMap();
            CreateMap<ExpenseType, UpdateExpenseTypeDto>().ReverseMap();

            CreateMap<Flat, FlatDto>().ReverseMap();
            CreateMap<Flat, CreateFlatDto>().ReverseMap();
            CreateMap<Flat, UpdateFlatDto>().ReverseMap();
            CreateMap<Flat, UpdateFlatUserDto>().ReverseMap();

            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();


        }
    }
}
