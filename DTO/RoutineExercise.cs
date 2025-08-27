
namespace DTO
{
    public class RoutineExercise : BaseClass
    {
        public int routineId { get; set; }
        public int exerciseId { get; set; }  // Añadir este campo
        public String exerciseName { get; set; }
        public int exerciseTypeId { get; set; }
        public int? sets { get; set; }
        public int? repetitions { get; set; }
        public decimal? weight { get; set; }
        public TimeSpan? timeDuration { get; set; }
        public TimeSpan? amrapTime { get; set; }

    }
}
