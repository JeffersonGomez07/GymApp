using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
        private readonly UserMapper _mapper;

        public UserCrudFactory() : base()
        {
            _mapper = new UserMapper();
            dao = SqlDao.GetInstance();
        }

        public string RegisterUser(BaseClass entityDTO, string hashedPassword, string baseStringSalt)
        {
            var errorMessage = new SqlParameter("@errorMessage", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlOperation operation = _mapper.GetRegisterUser(entityDTO, hashedPassword, baseStringSalt, errorMessage);
            dao.ExecuteStoredProcedure(operation);

            string error = errorMessage.Value as string;
            return error;
        }

        public string GetUserRoleName(int id)
        {
            SqlOperation operation = _mapper.GetUserRoleName(id);
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            return result.ContainsKey("role_name") ? result["role_name"].ToString() : null;
        }

        public BaseClass RetrieveUserByUsername(string username)
        {
            SqlOperation operation = _mapper.GetRetrieveUserByUsername(username);
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var user = _mapper.BuildObject(result);
            return user;
        }

        public string RetrieveSaltByUserId(int userId)
        {
            SqlOperation operation = _mapper.GetRetrieveSaltByUserId(userId);
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            return result.ContainsKey("salt") ? result["salt"].ToString() : null;
        }

        //Metodo para guardar el token de recuperacion de la contraseña
        public bool AddResetToken(int userId, string token)
        {
            SqlOperation operation = _mapper.GetAddToken(userId, token);
            dao.ExecuteStoredProcedure(operation);
            return true;
        }

        //Metodo para guardar el OTP en la tabla usuarios 
        public bool AddOtpCode(string email, string otp)
        {
            SqlOperation operation = _mapper.GetAddOtp(email, otp);
            dao.ExecuteStoredProcedure(operation);
            return true;
        }

        //Metodo para verificar el email con el OTP
        public string VerifyAccount(string otp)
        {
            var errorMessage = new SqlParameter("@errorMessage", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlOperation operation = _mapper.VerifyAccount(otp, errorMessage);
            dao.ExecuteStoredProcedure(operation);

            string error = errorMessage.Value as string;
            return error;
        }

        public string UpdatePasswordByToken(string token, string hashedPassword, string salt, string newPassword, string confirmPassword)
        {
            var errorMessage = new SqlParameter("@errorMessage", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlOperation operation = _mapper.GetUpdatePasswordByToken(token, hashedPassword, salt, newPassword, confirmPassword, errorMessage);
            dao.ExecuteStoredProcedure(operation);

            string error = errorMessage.Value as string;
            return error;
        }

        public BaseClass RetrieveByEmail(string email)
        {
            SqlOperation operation = _mapper.GetRetrieveByEmailStatement(email);
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var user = _mapper.BuildObject(result);
            return user;
        }

        public override BaseClass RetrieveById(int id)
        {
            SqlOperation operation = _mapper.GetRetrieveByIdStatement(id);
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var excercise = _mapper.BuildObject(result);
            return excercise;
        }

        public override void Create(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseClass entityDTO)
        {
            SqlOperation operation = _mapper.GetUpdateStatement((User)entityDTO);
                dao.ExecuteStoredProcedure(operation);
        }


        public override void Delete(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = _mapper.GetRetrieveAllStatement();
            var results = dao.ExecuteStoredProcedureWithQuery(operation);
            var users = _mapper.BuildObjects(results);

            var castedUsers = new List<T>();
            foreach (var user in users)
            {
                if (user is T castedUser)
                {
                    castedUsers.Add(castedUser);
                }
            }

            return castedUsers;
        }

        public void AssignRole(int userId, int roleId)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.AssignUserRole"
            };

            operation.AddIntegerParam("UserId", userId);
            operation.AddIntegerParam("RoleId", roleId);
            dao.ExecuteStoredProcedure(operation);
        }
    }
}