using DataAccess.Dao;
using DTO;

namespace DataAccess.Mapper
{
    // Clase que implementa ICrudStatements e IObjectMapper para manejar las operaciones CRUD de la entidad Equipment.
    public class EquipmentMapper : ICrudStatements, IObjectMapper
    {
        // Método para construir una lista de objetos BaseClass a partir de una lista de diccionarios.
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();

            foreach (var row in objectRows)
            {
                var equipment = BuildObject(row);
                list.Add(equipment);
            }

            return list;
        }

        // Método para construir un objeto BaseClass a partir de un diccionario.
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var equipment = new Equipment
            {
                EquipmentId = int.Parse(row["equipment_id"].ToString()),
                Name = row["name"].ToString(),
                Description = row["description"].ToString(),
                Quantity = row.ContainsKey("quantity") && !string.IsNullOrEmpty(row["quantity"].ToString()) ? int.Parse(row["quantity"].ToString()) : 0,
                Status = row["status"].ToString()
            };

            return equipment;
        }

        // Método para obtener una declaración SQL para crear un nuevo equipo.
        public SqlOperation GetCreateStatement(BaseClass entity)
        {
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_addEquipment"
            };

            var equipment = (Equipment)entity;
            operation.AddVarcharParam("name", equipment.Name);
            operation.AddVarcharParam("description", equipment.Description);
            operation.AddIntegerParam("quantity", equipment.Quantity);
            operation.AddVarcharParam("status", equipment.Status);

            return operation;
        }

        // Método para obtener una declaración SQL para eliminar un equipo.
        public SqlOperation GetDeleteStatement(BaseClass entity)
        {
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_deleteEquipment"
            };

            var equipment = (Equipment)entity;
            operation.AddIntegerParam("equipment_id", equipment.EquipmentId);

            return operation;
        }

        // Método para obtener una declaración SQL para recuperar todos los equipos.
        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getAllEquipment"
            };

            return operation;
        }

        // Método para obtener una declaración SQL para recuperar un equipo por su ID.
        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getEquipment"
            };

            operation.AddIntegerParam("equipment_id", id);

            return operation;
        }

        // Método para obtener una declaración SQL para actualizar un equipo existente.
        public SqlOperation GetUpdateStatement(BaseClass entity)
        {
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_updateEquipment"
            };

            var equipment = (Equipment)entity;
            operation.AddIntegerParam("equipment_id", equipment.EquipmentId);
            operation.AddVarcharParam("name", equipment.Name);
            operation.AddVarcharParam("description", equipment.Description);
            operation.AddIntegerParam("quantity", equipment.Quantity);
            operation.AddVarcharParam("status", equipment.Status);

            return operation;
        }
    }
}
