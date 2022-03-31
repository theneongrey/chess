using AutoMapper;
using ChessApi.Dto;
using ChessApi.Model;

namespace ChessApi.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
