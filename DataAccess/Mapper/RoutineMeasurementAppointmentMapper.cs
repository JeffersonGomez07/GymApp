using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class RoutineMeasurementAppointmentMapper : ICrudStatements, IObjectMapper
    {
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var list = new List<BaseClass>();

            foreach (var row in objectRows)
            {
                var appointment = BuildObject(row);
                list.Add(appointment);
            }

            return list;
        }

        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var appointment = new RoutineMeasurementAppointment()
            {
                MeasurementAppointmentId = int.Parse(row["measurement_appointment_id"].ToString()),
                MemberId = int.Parse(row["member_id"].ToString()),
                InstructorId = int.Parse(row["instructor_id"].ToString()),
                AppointmentDate = DateTime.Parse(row["appointment_date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind),
                Status = row["status"].ToString()
            };

            return appointment;
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            var appointment = (RoutineMeasurementAppointment)entityDTO;
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_addMeasurementAppointment"
            };

            operation.AddIntegerParam("member_id", appointment.MemberId);
            operation.AddIntegerParam("instructor_id", appointment.InstructorId);
            operation.AddDateTimeParam("appointment_date", appointment.AppointmentDate);
            operation.AddVarcharParam("status", appointment.Status);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            var appointment = (RoutineMeasurementAppointment)entityDTO;
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_deleteMeasurementAppointment"
            };

            operation.AddIntegerParam("measurement_appointment_id", appointment.MeasurementAppointmentId);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getAllMeasurementAppointments"
            };

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getMeasurementAppointmentById"
            };

            operation.AddIntegerParam("measurement_appointment_id", id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            var appointment = (RoutineMeasurementAppointment)entityDTO;
            var operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_updateMeasurementAppointment"
            };

            operation.AddIntegerParam("measurement_appointment_id", appointment.MeasurementAppointmentId);
            operation.AddIntegerParam("member_id", appointment.MemberId);
            operation.AddIntegerParam("instructor_id", appointment.InstructorId);
            operation.AddDateTimeParam("appointment_date", appointment.AppointmentDate);
            operation.AddVarcharParam("status", appointment.Status);

            return operation;
        }


    }
}
 