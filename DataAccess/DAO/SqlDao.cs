using System.Data.SqlClient;
using System.Data;

namespace DataAccess.Dao
{
    public class SqlDao
    {
        private static SqlDao instance = new SqlDao();
        private string _connString = "Server=localhost; Database=FitnessCenterDB; User Id=SA; Password=Persy2024!";


        public static SqlDao GetInstance()
        {
            if (instance == null)
                instance = new SqlDao();
            return instance;
        }

        public void ExecuteStoredProcedure(SqlOperation operation)
        {
            //hacer la conexion
            string connectionString = _connString;
            SqlConnection conn = new SqlConnection(connectionString);

            //Armar el query
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = operation.ProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            //Agregar los parametros
            foreach (var p in operation.parameters)
            {
                command.Parameters.Add(p);
            }
            //abrir conexion
            conn.Open();
            //Ejecutar el comando
            command.ExecuteNonQuery();
        }

        public List<Dictionary<string, object>> ExecuteStoredProcedureWithQuery(SqlOperation operation)
        {
            var conn = _connString;
            List<Dictionary<string, object>> lstResults = new List<Dictionary<string, object>>();

            var connection = new SqlConnection(conn);
            var command = new SqlCommand();

            //preparar el comando a ejecutar
            command.Connection = connection;
            command.CommandText = operation.ProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            //Agregar los parametros
            foreach (var p in operation.parameters)
            {
                command.Parameters.Add(p);
            }
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            //Recorrer el resultado para poder armar la Lista de diccionarios
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Dictionary<string, object> diccObj = new Dictionary<string, object>();

                    for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                    {
                        diccObj.Add(reader.GetName(fieldCount), reader.GetValue(fieldCount));
                    }
                    lstResults.Add(diccObj);

                }
            }
            return lstResults;
        }

        public Dictionary<string, object> ExecuteStoredProcedureWithUniqueResult(SqlOperation operation)
        {
            var conn = _connString;
            List<Dictionary<string, object>> lstResults = new List<Dictionary<string, object>>();

            var connection = new SqlConnection(conn);
            var command = new SqlCommand();

            //preparar el comando a ejecutar
            command.Connection = connection;
            command.CommandText = operation.ProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            //Agregar los parametros
            foreach (var p in operation.parameters)
            {
                command.Parameters.Add(p);
            }

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            //Recorrer el resultado para poder armar la Lista de diccionarios
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Dictionary<string, object> diccObj = new Dictionary<string, object>();

                    for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                    {
                        diccObj.Add(reader.GetName(fieldCount), reader.GetValue(fieldCount));
                    }
                    return diccObj;
                }
            }
            return null;
        }

    }

}