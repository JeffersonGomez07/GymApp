using DTO;
using DataAccess.CRUD;


namespace BL
{
    // Clase que gestiona las operaciones CRUD para la entidad Equipment.
    public class EquipmentManager
    {
        // Método para crear un nuevo equipo.
        public void CreateEquipment(Equipment equipment)
        {
            EquipmentCrudFactory ex_crud = new EquipmentCrudFactory();
            ex_crud.Create(equipment);
        }

        // Método para obtener un equipo por ID.
        public Equipment GetEquipmentById(int id)
        {
            EquipmentCrudFactory ex_crud = new EquipmentCrudFactory();
            return (Equipment)ex_crud.RetrieveById(id);
        }

        // Método para obtener todos los equipos.
        public List<Equipment> GetAllEquipment()
        {
            EquipmentCrudFactory ex_crud = new EquipmentCrudFactory();
            return ex_crud.RetrieveAll<Equipment>();
        }

        // Método para actualizar un equipo.
        public void UpdateEquipment(Equipment equipment)
        {
            EquipmentCrudFactory ex_crud = new EquipmentCrudFactory();
            ex_crud.Update(equipment);
        }

        // Método para eliminar un equipo por ID.
        public void DeleteEquipment(int id)
        {
            EquipmentCrudFactory ex_crud = new EquipmentCrudFactory();
            var equipment = new Equipment { EquipmentId = id };
            ex_crud.Delete(equipment);
        }
    }
}
