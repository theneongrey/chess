using ChessApi.Dto;
using ChessApi.Model;

namespace ChessApi.Data
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers();
        User? GetUserById(Guid id);
        bool UserExists(string id);
        User AddUser(string name);
        User? UpdateUser(Guid id, string name);
        bool DeleteUser(Guid id);
    }
}
