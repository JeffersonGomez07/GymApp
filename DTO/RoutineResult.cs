namespace DTO
{
    public class RoutineResult: BaseClass
    {
        public int RoutineId { get; set; }
        public int ExerciseId { get; set; }
        public int? SetsCompleted { get; set; }
        public int? RepetitionsCompleted { get; set; }
        public decimal? WeightUsed { get; set; }
        public TimeSpan? TimeDuration { get; set; }
        public TimeSpan? AmrapTime { get; set; }
        public DateTime ResultDate { get; set; } = DateTime.Now;



    }

}