
using Microsoft.AspNetCore.Mvc;
using BL;
using Microsoft.AspNetCore.Cors;
using DTO;
using DataAccess.CRUD;

namespace API.Controllers.Routines
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoutineController : Controller
    {
        [HttpPost]
        public IActionResult CreateRoutine([FromBody] Routine routine)
        {
            try
            {
                routine.creationDate = DateTime.Now;

                MeasurementAppointmentCrudFactory crudFactory = new MeasurementAppointmentCrudFactory();
                var measurementAppointment = crudFactory.GetMeasurementAppointment(routine.memberId, routine.instructorId);

                if (measurementAppointment == null)
                {
                    return Json(new { success = false, message = "No measurement appointment found for this member and instructor." });
                }

                routine.measurementAppointmentId = measurementAppointment.MeasurementAppointmentId;

                RoutineCrudFactory routineCrud = new RoutineCrudFactory();
                routineCrud.Create(routine);

                return Json(new { success = true, routineId = routine.routineId, message = "Routine created successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetMeasurementAppointment(int memberId, int instructorId)
        {
            try
            {
                MeasurementAppointmentCrudFactory crudFactory = new MeasurementAppointmentCrudFactory();
                var measurementAppointment = crudFactory.GetMeasurementAppointment(memberId, instructorId);

                if (measurementAppointment == null)
                {
                    return Json(new { measurementAppointmentId = (int?)null });
                }

                return Json(new { measurementAppointmentId = measurementAppointment.MeasurementAppointmentId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public Routine GetRoutine(int id)
        {
            RoutineManager manager = new RoutineManager();
            Routine routine = manager.GetRoutineById(id);

            return routine;
        }
        [HttpGet]
        public List<Routine> GetAllRoutines()
        {
            RoutineManager manager = new RoutineManager();
            List<Routine> routineList = manager.GetAllRoutines();

            return routineList;
        }

        [HttpPost]
        public IActionResult AddExerciseToRoutine([FromBody] RoutineExerciseCreate routineExerciseDTO)
        {
            try
            {
                if (routineExerciseDTO == null)
                {
                    return BadRequest(new { success = false, message = "The request body is null" });
                }

                // Convertir el DTO a RoutineExercise
                RoutineExercise routineExercise = new RoutineExercise
                {
                    routineId = routineExerciseDTO.RoutineId,
                    exerciseId = routineExerciseDTO.ExerciseId,
                    exerciseTypeId = routineExerciseDTO.ExerciseTypeId,
                    sets = routineExerciseDTO.Sets,
                    repetitions = routineExerciseDTO.Repetitions,
                    weight = routineExerciseDTO.Weight,
                    timeDuration = routineExerciseDTO.TimeDuration,
                    amrapTime = routineExerciseDTO.AmrapTime
                };

                RoutineExerciseManager manager = new RoutineExerciseManager();
                manager.AddExerciseToRoutine(routineExercise);

                return Json(new { success = true, message = "Exercise added to routine successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




        [HttpGet]
        public IActionResult GetExercisesForRoutine(int routineId)
        {
            try
            {
                RoutineExerciseManager manager = new RoutineExerciseManager();
                var exercises = manager.GetExercisesForRoutine(routineId);

                // Asumiendo que RoutineExercise contiene los campos correctos, mapeamos los datos.
                var exercise = exercises.Select(e => new
                {
                    exerciseId = e.exerciseId,
                    exerciseName = e.exerciseName,
                    exerciseTypeId = e.exerciseTypeId,
                    sets = e.sets,
                    repetitions = e.repetitions,
                    weight = e.weight,
                    timeDuration = e.timeDuration,
                    amrapTime = e.amrapTime,
                }).ToList();

                return Ok(exercise);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult GetRecordedResults(int routineId)
        {
            try
            {
                RoutineResultManager manager = new RoutineResultManager();
                var results = manager.GetResultsByRoutineId(routineId);

                return Json(new { success = true, data = results });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddRoutineResults([FromBody] RoutineResult routineResult)
        {
            try
            {
                if (routineResult == null)
                {
                    return BadRequest(new { success = false, message = "The request body is null" });
                }

                RoutineResultManager manager = new RoutineResultManager();
                manager.AddRoutineResults(routineResult);

                return Json(new { success = true, message = "Results recorded successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public List<RoutineList> GetAllRoutineList()
        {
            RoutineListManager manager = new RoutineListManager();
            List<RoutineList> routineList = manager.GetAllRoutineList();
            return routineList;
        }

        [HttpPost]
    public IActionResult SubmitResults([FromBody] RoutineResult routineResult)
    {
        try
        {
            RoutineResultManager manager = new RoutineResultManager();
            manager.CreateRoutineResult(routineResult);

            // Devolver una respuesta JSON con un mensaje de éxito
            return Json(new { success = true, message = "Routine results submitted successfully" });
        }
        catch (Exception ex)
        {
            // Manejar el error y devolver una respuesta JSON con un mensaje de error
            return Json(new { success = false, message = ex.Message });
        }
    }
        [HttpDelete("{id}")]
        public IActionResult DeleteRoutine(int id)
        {
            try
            {
                RoutineManager manager = new RoutineManager();
                manager.DeleteRoutine(id);

                return Json(new { success = true, message = "Routine deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
