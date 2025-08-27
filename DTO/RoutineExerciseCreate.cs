namespace DTO
{
    public class RoutineExerciseCreate
    {
        public int RoutineId { get; set; }
        public int ExerciseId { get; set; }
        public int ExerciseTypeId { get; set; }
        public int? Sets { get; set; }
        public int? Repetitions { get; set; }
        public decimal? Weight { get; set; }
        public TimeSpan? TimeDuration { get; set; }
        public TimeSpan? AmrapTime { get; set; }
    }
}
