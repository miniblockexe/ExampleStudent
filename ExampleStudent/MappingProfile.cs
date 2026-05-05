using AutoMapper;
using ExampleStudent.Models.Domain;
using ExampleStudent.Models.User;

namespace ExampleStudent
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationVM, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
