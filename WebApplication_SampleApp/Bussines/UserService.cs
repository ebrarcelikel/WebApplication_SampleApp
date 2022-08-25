using WebApplication_SampleApp.Core;
using WebApplication_SampleApp.Models;
using WebApplication_SampleApp.Models.Context;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Bussines
{

    public class UserService
    {
        private DatabaseContext _db = new DatabaseContext();
        private HttpContext _httpContext;
        public UserService(HttpContext httpContext)
        {
            _httpContext = httpContext;

        }
        public ServiceResult<object> Register(RegisterViewModel model)
        {
            ServiceResult<object> result = new ServiceResult<object>();
            model.Username = model.Username.Trim().ToLower();
            model.Email = model.Email.Trim().ToLower();

            if (_db.Users.Any(x => x.Email.ToLower() == model.Email.ToLower()))
            {
                result.AddError($"'{model.Email}' address is already registered.");
                return result;

            }

            if (_db.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
            {
                result.AddError($"'{model.Username}' username is already registered.");
                return result;

            }
            _db.Users.Add(new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                IsActive = true,
                IsAdmin = false,
                CreatedUser = "register",
                CreatedAt = DateTime.Now

            });
            if (_db.SaveChanges() == 0)
            {
                result.AddError("Failed to register.");
            }
            return result;
        }
        public ServiceResult<User> Login(LoginViewModel model)
        {
            ServiceResult<User> result = new ServiceResult<User>();

            model.Username = model.Username.Trim().ToLower();

            User user = _db.Users.SingleOrDefault(x =>
            x.Username.ToLower() == model.Username &&
            x.Password == model.Password);

            if (user == null)
            {

                result.AddError("Username or password is wrong");

            }
            else
            {
                user.Password = String.Empty;
                result.Data = user;
            }

            return result;
        }

        public ServiceResult<List<User>> List()
        {
            List<User> users = _db.Users.ToList();
            users.ForEach(user => user.Password = String.Empty);
            ServiceResult<List<User>> result = new ServiceResult<List<User>>();
            result.Data = users;
            return result;
        }

        public ServiceResult<User> Create(UserViewModel model, HttpContext httpContext)
        {
            ServiceResult<User> result = new ServiceResult<User>();
            model.Username = model.Username.Trim().ToLower();
            model.Email = model.Email.Trim().ToLower();

            if (_db.Users.Any(x => x.Email.ToLower() == model.Email.ToLower()))
            {
                result.AddError($"'{model.Email}' address is already registered.");
                return result;

            }

            if (_db.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
            {
                result.AddError($"'{model.Username}' username is already registered.");
                return result;
            }
            User user = new User
            {
                Fullname = model.Fullname,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                IsActive = model.IsActive,
                IsAdmin = model.IsAdmin,
                CreatedUser = httpContext.Session.GetString(Constants.Username),
                CreatedAt = DateTime.Now

            };
            _db.Users.Add(user);
            if (_db.SaveChanges() == 0)
                result.AddError("Failed to register.");
            else
                result.Data = user;

            return result;
        }

        public ServiceResult<User> Find(int ID)
        {
            ServiceResult<User> result = new ServiceResult<User>()
            {
                Data = _db.Users.Find(ID)
            };
            if (result.Data == null)
                result.AddError("The user is not found");
            else
                result.Data.Password = string.Empty;


            return result;
        }

        public ServiceResult<User> Update(int ID, UserEditViewModel model, HttpContext httpContext)
        {
            ServiceResult<User> result = new ServiceResult<User>();
            model.Username = model.Username.Trim().ToLower();
            model.Email = model.Email.Trim().ToLower();

            if (_db.Users.Any(x => x.Email.ToLower() == model.Email.ToLower() && x.ID != ID))
            {
                result.AddError($"'{model.Email}' address is already registered.");
                return result;
            }

            if (_db.Users.Any(x => x.Username.ToLower() == model.Username.ToLower() && x.ID != ID))
            {
                result.AddError($"'{model.Username}' username is already registered.");
                return result;
            }
            User user = _db.Users.Find(ID);

            user.Fullname = model.Fullname;
            user.Username = model.Username;
            user.Email = model.Email;
            user.IsActive = model.IsActive;
            user.IsAdmin = model.IsAdmin;
            user.ModifiedUser = httpContext.Session.GetString(Constants.Username);
            user.ModifiedAt = DateTime.Now;

            if (_db.SaveChanges() == 0)
                result.AddError("Failed to edit.");
            else
                result.Data = user;

            return result;

        }

        public ServiceResult<object> Remove(int ID)
        {
            ServiceResult<object> result = new ServiceResult<object>();
            User user = _db.Users.Find(ID);
            if (user != null)
            {
                _db.Users.Remove(user);
                if (_db.SaveChanges() == 0)
                    result.AddError("Failed to delete.");


            }


            return result;
        }
    }
}
