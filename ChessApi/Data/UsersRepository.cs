using AutoMapper;
using ChessApi.Dto;
using ChessApi.Model;

namespace ChessApi.Data
{
    public class UsersRepository : IUsersRepository
    {
        private DataContext _dataContext;
        
        public UsersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dataContext.Users!;
        }

        public User? GetUserById(Guid id)
        {
            return _dataContext.Users!.FirstOrDefault(u => u.Id == id);
        }

        public User AddUser(string name)
        {
            var user = new User
            {
                Id = new Guid(),
                Name = name,
            };

            _dataContext.Users!.Add(user);
            _dataContext.SaveChanges();

            return user;
        }

        public User? UpdateUser(Guid userId, string name)
        {
            var user = GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            user.Name = name;

            _dataContext.Users!.Update(user);
            _dataContext.SaveChanges();

            return user;
        }

        public bool DeleteUser(Guid id)
        {
            var user = GetUserById(id);
            if (user == null)
            {
                return false;
            }

            _dataContext.Users!.Remove(user);
            _dataContext.SaveChanges();

            return true;
        }
    }
}
