namespace DTO
{
    public class Exercise : BaseClass
    {
        public int exerciseId { get; set; }
        public int exerciseTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string primaryMuscle { get; set; }
    }
}

