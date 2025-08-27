using DTO;

public class RoutineList : BaseClass
{
    public int routineId { get; set; }
    public string memberUsername { get; set; } // Cambiado para reflejar el nombre del miembro
    public string instructorUsername { get; set; } // Nuevo campo para el nombre del instructor
    public string name { get; set; }
    public string description { get; set; }
    public DateTime creationDate { get; set; }
}
