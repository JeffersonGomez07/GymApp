using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DTO;
using System.Data.SqlClient;

namespace DataAccess.Mapper
{
    public class UserMapper : ICrudStatements, IObjectMapper
    {
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            List<BaseClass> users = new List<BaseClass>();

            foreach (var row in objectRows)
            {
                var user = BuildObject(row);
                users.Add(user);
            }

            return users;
        }

        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var user = new User();

            if (row.ContainsKey("user_id") && int.TryParse(row["user_id"].ToString(), out int userId))
                user.Id = userId;

            if (row.ContainsKey("role_id") && int.TryParse(row["role_id"].ToString(), out int roleId))
                user.Role_id = roleId;

            user.First_name = row.ContainsKey("first_name") ? row["first_name"].ToString() : null;
            user.Last_name = row.ContainsKey("last_name") ? row["last_name"].ToString() : null;
            user.Username = row.ContainsKey("username") ? row["username"].ToString() : null;
            user.Email = row.ContainsKey("email") ? row["email"].ToString() : null;
            user.Password = row.ContainsKey("password") ? row["password"].ToString() : null;
            user.Phone_number = row.ContainsKey("phone_number") ? row["phone_number"].ToString() : null;

            if (row.ContainsKey("birthdate") && DateTime.TryParse(row["birthdate"].ToString(), out DateTime birthdate))
                user.Birthdate = birthdate;
            else
                user.Birthdate = default;

            user.Profile_image = row.ContainsKey("profile_image") ? row["profile_image"].ToString() : null;
            user.Id_image = row.ContainsKey("id_image") ? row["id_image"].ToString() : null;

            return user;
        }

        public SqlOperation GetRegisterUser(BaseClass entityDTO, string hashedPassword, string baseStringSalt, SqlParameter errorMessage)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_addUserAccount"
            };

            User user = (User)entityDTO;

            operation.AddIntegerParam("role_id", user.Role_id);
            operation.AddVarcharParam("first_name", user.First_name);
            operation.AddVarcharParam("last_name", user.Last_name);
            operation.AddVarcharParam("username", user.Username);
            operation.AddVarcharParam("email", user.Email);
            operation.AddVarcharParam("hashedPassword", hashedPassword);
            operation.AddVarcharParam("phone_number", user.Phone_number);
            operation.AddDateTimeParam("birthdate", user.Birthdate);
            operation.AddVarcharParam("profile_image", user.Profile_image);
            operation.AddVarcharParam("id_image", user.Id_image);
            operation.AddVarcharParam("password", user.Password);
            operation.AddVarcharParam("salt", baseStringSalt);

            operation.parameters.Add(errorMessage);

            return operation;
        }


        public SqlOperation GetRegisterSalt(int userId, string salt)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_addUserSalt"
            };

            operation.AddIntegerParam("user_id", userId);
            operation.AddVarcharParam("salt", salt);

            return operation;
        }

        public SqlOperation GetUserRoleName(int id)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.GetUserRoleName"
            };

            operation.AddIntegerParam("user_id", id);

            return operation;
        }

        public SqlOperation GetRetrieveByEmailStatement(string email)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getUserByEmail"
            };

            operation.AddVarcharParam("Email", email);

            return operation;
        }

        public SqlOperation GetRetrieveUserByUsername(string username)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getUserByUsername"
            };

            operation.AddVarcharParam("username", username);

            return operation;
        }

        public SqlOperation GetRetrieveSaltByUserId(int userId)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getUserSaltByUserId"
            };

            operation.AddIntegerParam("user_id", userId);

            return operation;
        }

        public SqlOperation GetAddToken(int userId, string token)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_addUserToken"
            };

            operation.AddIntegerParam("user_id", userId);
            operation.AddVarcharParam("token", token);

            return operation;
        }

        public SqlOperation GetUpdatePasswordByToken(string token, string hashedPassword, string salt)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_updateUserPassword"
            };

            operation.AddVarcharParam("token", token);
            operation.AddVarcharParam("hashedPassword", hashedPassword);
            operation.AddVarcharParam("salt", salt);

            return operation;
        }

        public SqlOperation GetAddOtp(string email, string otp)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_addUserOtp"
            };

            operation.AddVarcharParam("user_email", email);
            operation.AddVarcharParam("otp", otp);

            return operation;
        }

        public SqlOperation VerifyAccount(string otp, SqlParameter errorMessage)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_verifyAccountOtp"
            };

            operation.AddVarcharParam("otp", otp);
            operation.parameters.Add(errorMessage);

            return operation;
        }

        public SqlOperation GetUpdatePasswordByToken(string token, string hashedPassword, string salt, string newPassword, string confirmPassword, SqlParameter errorMessage)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_updateUserPassword"
            };

            operation.AddVarcharParam("token", token);
            operation.AddVarcharParam("hashedPassword", hashedPassword);
            operation.AddVarcharParam("salt", salt);
            operation.AddVarcharParam("password", newPassword);
            operation.AddVarcharParam("confirmPassword", confirmPassword);

            operation.parameters.Add(errorMessage);

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int userId)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getUserById"
            };

            operation.AddIntegerParam("user_id", userId); // Cambiado 'Id' a 'user_id'

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.GetAllUsers"
            };

            return operation;
        }

        public SqlOperation GetAssignRoleStatement(int userId, int roleId)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.AssignUserRole"
            };

            operation.AddIntegerParam("UserId", userId);
            operation.AddIntegerParam("RoleId", roleId);

            return operation;
        }

        public SqlOperation GetUpdateStatement(User user)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_updateUser"
            };

            operation.AddIntegerParam("user_id", user.Id);
            operation.AddVarcharParam("first_name", user.First_name);
            operation.AddVarcharParam("last_name", user.Last_name);
            operation.AddVarcharParam("username", user.Username);
            operation.AddVarcharParam("email", user.Email);
            operation.AddVarcharParam("phone_number", user.Phone_number);
            operation.AddDateTimeParam("birthdate", user.Birthdate);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}