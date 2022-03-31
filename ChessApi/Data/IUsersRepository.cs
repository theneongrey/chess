using ChessApi.Dto;
using ChessApi.Model;

namespace ChessApi.Data
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers();
        User? GetUserById(Guid id);
        User AddUser(string name);
        User? UpdateUser(Guid id, string name);
        bool DeleteUser(Guid id);
    }
}
