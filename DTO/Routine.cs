namespace DTO
{
    public class Routine : BaseClass
    {
        public int routineId { get; set; }
        public int memberId { get; set; }
        public int instructorId { get; set; }
        public int measurementAppointmentId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime creationDate { get; set; }
    }

}