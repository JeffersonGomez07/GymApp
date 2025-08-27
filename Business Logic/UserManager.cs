using DTO;
using DataAccess.CRUD;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BL
{
    public class UserManager
    {
        public string RegisterUser(User user)
        {
            var passwordHelper = new PasswordHelper();
            byte[] salt = passwordHelper.GenerateSalt();
            byte[] hashedPassword = passwordHelper.HashPassword(user.Password, salt);

            user.Role_id = 1;

            UserCrudFactory us_crud = new UserCrudFactory();
            string baseStringPassword = Convert.ToBase64String(hashedPassword);
            string baseStringSalt = Convert.ToBase64String(salt);

            return us_crud.RegisterUser(user, baseStringPassword, baseStringSalt);
        }

        public string GetUserRoleName(int id)
        {
            RoleCrudFactory roleCrud = new RoleCrudFactory();
            var role = roleCrud.GetRoleByUserId(id);
            return role.Name;
        }

        public User Login(string username, string password)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            User user = (User)us_crud.RetrieveUserByUsername(username);
            if (user == null)
            {
                return null;
            }

            string saltBase64 = us_crud.RetrieveSaltByUserId(user.Id);

            try
            {
                byte[] salt = Convert.FromBase64String(saltBase64);
                byte[] storedHash = Convert.FromBase64String(user.Password);

                var passwordHelper = new PasswordHelper();

                bool isValidPassword = passwordHelper.VerifyPassword(password, storedHash, salt);

                return isValidPassword ? user : null;
            }
            catch (FormatException)
            {
                throw new Exception("La sal recuperada no es una cadena Base64 válida.");
            }
        }

        public string UpdatePassword(ResetPassword request)
        {
            var passwordHelper = new PasswordHelper();
            byte[] salt = passwordHelper.GenerateSalt();
            byte[] hashedPassword = passwordHelper.HashPassword(request.NewPassword, salt);

            string baseStringPassword = Convert.ToBase64String(hashedPassword);
            string baseStringSalt = Convert.ToBase64String(salt);

            UserCrudFactory us_crud = new UserCrudFactory();
            return us_crud.UpdatePasswordByToken(request.Token, baseStringPassword, baseStringSalt, request.NewPassword, request.confirmPassword);
        }

        //Metodo para agregar el Token de recuperacion de contraseña
        public bool AddResetToken(int userId, string token)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            return us_crud.AddResetToken(userId, token);
        }

        //Metodo para agregar el Otp a la tabal de usuario
        public bool AddOtpCode(string email, string otp)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            return us_crud.AddOtpCode(email, otp);
        }

        //Metodo para verificar Email con el OTP 
        public string VerifyAccount(string otp)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            return us_crud.VerifyAccount(otp);
        }

        public User GetUserByUsername(string username)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            return (User)us_crud.RetrieveByEmail(username);
        }

        public User GetUserByEmail(string email)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            return (User)us_crud.RetrieveByEmail(email);
        }

        public User GetUserById(int id)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            return (User)us_crud.RetrieveById(id);
        }

        public List<User> GetAllUsers()
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            return us_crud.RetrieveAll<User>();
        }

        public void UpdateUser(User user)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            us_crud.Update(user);
        }


        public void AssignRole(int userId, int roleId)
        {
            UserCrudFactory us_crud = new UserCrudFactory();
            us_crud.AssignRole(userId, roleId);
        }
    }
}