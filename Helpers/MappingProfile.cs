using AutoMapper;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<EditUserDto, User>();
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<PurchaseTransaction, PurchaseTransactionDto>();
            CreateMap<PurchaseTransactionDto, PurchaseTransaction>();
            CreateMap<PurchaseTransactionDetail, PurchaseTransactionDetailDto>();
            CreateMap<PurchaseTransactionDetailDto, PurchaseTransactionDetail>();
        }
    }
}