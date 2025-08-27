using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

public class RoutineExerciseCrudFactory : CrudFactory
{
    private RoutineExerciseMapper mapper;

    public RoutineExerciseCrudFactory() : base()
    {
        mapper = new RoutineExerciseMapper();
        dao = SqlDao.GetInstance();
    }

    public override void Create(BaseClass entityDTO)
    {
        SqlOperation operation = mapper.GetCreateStatement(entityDTO);
        dao.ExecuteStoredProcedure(operation);
    }

    public override void Update(BaseClass entityDTO)
    {
        throw new NotImplementedException();
    }

    public override void Delete(BaseClass entityDTO)
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        SqlOperation operation = mapper.GetRetrieveAllStatement();

        List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);

        List<BaseClass> mappedExercises = mapper.BuildObjects(result);

        List<T> exerciseList = new List<T>();

        foreach (var exercise in mappedExercises)
        {
            var convertedExercise = (T)Convert.ChangeType(exercise, typeof(T));
            exerciseList.Add(convertedExercise);
        }

        return exerciseList;
    }


    public override BaseClass RetrieveById(int id)
    {
        SqlOperation operation = mapper.GetRetrieveByIdStatement(id);

        Dictionary<string, object> result = dao.ExecuteStoredProcedureWithUniqueResult(operation);
        var exercise = mapper.BuildObject(result);

        return exercise;
    }

    public void AddExerciseToRoutine(RoutineExercise routineExercise)
    {
        SqlOperation operation = mapper.GetCreateStatement(routineExercise);
        dao.ExecuteStoredProcedure(operation);
    }



    public List<RoutineExercise> RetrieveByRoutineId(int routineId)
    {
        SqlOperation operation = mapper.GetRetrieveByRoutineIdStatement(routineId);
        var result = dao.ExecuteStoredProcedureWithQuery(operation);
        return mapper.BuildObjects(result).Cast<RoutineExercise>().ToList();
    }

}