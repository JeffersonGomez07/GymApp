using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;


namespace DataAccess.CRUD
{
    // Clase que gestiona las operaciones CRUD para la entidad Equipment utilizando el patrón Factory.
    public class EquipmentCrudFactory : CrudFactory
    {
        private readonly EquipmentMapper _mapper;

        // Constructor que inicializa la instancia de EquipmentMapper y obtiene una instancia de SqlDao.
        public EquipmentCrudFactory() : base()
        {
            _mapper = new EquipmentMapper();
            dao = SqlDao.GetInstance();
        }

        // Método para crear un nuevo equipo: recibe los datos del equipo y ejecuta el procedimiento almacenado de creación.
        public override void Create(BaseClass entityDTO)
        {
            var operation = _mapper.GetCreateStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }

        // Método para actualizar un equipo: recibe los datos del equipo y ejecuta el procedimiento almacenado de actualización.
        public override void Update(BaseClass entityDTO)
        {
            var operation = _mapper.GetUpdateStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }

        // Método para eliminar un equipo: recibe el ID del equipo y ejecuta el procedimiento almacenado de eliminación.
        public override void Delete(BaseClass entityDTO)
        {
            var operation = _mapper.GetDeleteStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }

        // Método para obtener todos los equipos: ejecuta el procedimiento almacenado para recuperar todos los equipos y los mapea a objetos de tipo Equipment.
        public override List<T> RetrieveAll<T>()
        {
            var operation = _mapper.GetRetrieveAllStatement();
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            var mappedEquipments = _mapper.BuildObjects(result);
            var equipmentList = new List<T>();

            foreach (var equipment in mappedEquipments)
            {
                var convertedEquipment = (T)Convert.ChangeType(equipment, typeof(T));
                equipmentList.Add(convertedEquipment);
            }

            return equipmentList;
        }

        // Método para obtener un equipo por ID: ejecuta el procedimiento almacenado para recuperar un equipo por su ID y lo mapea a un objeto de tipo Equipment.
        public override BaseClass RetrieveById(int id)
        {
            var operation = _mapper.GetRetrieveByIdStatement(id);
            var result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var equipment = _mapper.BuildObject(result);

            return equipment;
        }
    }
}
