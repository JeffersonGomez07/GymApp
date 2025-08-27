using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class MeasurementAppointmentCrudFactory : CrudFactory
    {
        private RoutineMeasurementAppointmentMapper mapper;

        public MeasurementAppointmentCrudFactory() : base()
        {
            mapper = new RoutineMeasurementAppointmentMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass entityDTO)
        {
            SqlOperation operation = mapper.GetCreateStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }

        public override void Update(BaseClass entityDTO)
        {
            SqlOperation operation = mapper.GetUpdateStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }

        public override List<T> RetrieveAll<T>()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();
            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);
            List<BaseClass> mappedAppointments = mapper.BuildObjects(result);

            List<T> appointmentList = new List<T>();
            foreach (var appointment in mappedAppointments)
            {
                var convertedAppointment = (T)Convert.ChangeType(appointment, typeof(T));
                appointmentList.Add(convertedAppointment);
            }

            return appointmentList;
        }

        public override BaseClass RetrieveById(int id)
        {
            SqlOperation operation = mapper.GetRetrieveByIdStatement(id);
            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            var appointment = mapper.BuildObject(result);

            return appointment;
        }

        public override void Delete(BaseClass entityDTO)
        {
            SqlOperation operation = mapper.GetDeleteStatement(entityDTO);
            dao.ExecuteStoredProcedure(operation);
        }

        public RoutineMeasurementAppointment GetMeasurementAppointment(int memberId, int instructorId)
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_getMeasurementAppointmentByMemberAndInstructor"
            };
            operation.AddIntegerParam("member_id", memberId);
            operation.AddIntegerParam("instructor_id", instructorId);

            Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
            return (RoutineMeasurementAppointment)mapper.BuildObject(result);
        }
    }
}
