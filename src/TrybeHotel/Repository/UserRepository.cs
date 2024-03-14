using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            var user = _context.Users.Find(userId) ?? throw new Exception("Usuário não encontrado");
            
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserDto Login(LoginDto login)
        {
            
           var userExist = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
            if (userExist == null)
            {
                throw new Exception("Incorrect e-mail or password");
            }
            return new UserDto
            {
                UserId = userExist.UserId,
                Name = userExist.Name,
                Email = userExist.Email,
                UserType = userExist.UserType
            };
        }
        public UserDto Add(UserDtoInsert user)
        {
            var userEntity = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "admin"
            };
            _context.Users.Add(userEntity);
            _context.SaveChanges();
            return new UserDto
            {
                UserId = userEntity.UserId,
                Name = userEntity.Name,
                Email = userEntity.Email,
                UserType = userEntity.UserType
            };
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail) ?? throw new Exception("Usuário não encontrado");
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email
            };
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = from user in _context.Users
                        select new UserDto
                        {
                            UserId = user.UserId,
                            Name = user.Name,
                            Email = user.Email,
                            UserType = user.UserType
                        };
            return users;
        }

    }
}