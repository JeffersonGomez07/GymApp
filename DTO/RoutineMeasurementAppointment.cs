namespace DTO
{
    public class RoutineMeasurementAppointment : BaseClass
    {
        public int MeasurementAppointmentId { get; set; }
        public int MemberId { get; set; }
        public int InstructorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
    }
}
