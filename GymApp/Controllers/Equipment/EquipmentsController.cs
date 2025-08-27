using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BL;



namespace API.Controllers.Equipments
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipmentsController : Controller
    {
        // Método HTTP GET para obtener todos los equipos.
        [HttpGet]
        public ActionResult<List<Equipment>> GetAllEquipment()
        {
            try
            {
                EquipmentManager equipmentManager = new EquipmentManager();
                List<Equipment> equipmentList = equipmentManager.GetAllEquipment();

                if (equipmentList == null || equipmentList.Count == 0)
                {
                    return NotFound("No equipment found.");
                }

                return Ok(equipmentList);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Método HTTP POST para crear un nuevo equipo.
        [HttpPost]
        public IActionResult CreateEquipment([FromBody] Equipment equipment)
        {
            try
            {
                if (equipment == null)
                {
                    return Json(new { success = false, message = "Equipment data is null." });
                }

                EquipmentManager equipmentManager = new EquipmentManager();
                equipmentManager.CreateEquipment(equipment);

                return Json(new { success = true, message = "Equipment created successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Método HTTP POST para editar un equipo existente.
        [HttpPost]
        public IActionResult EditEquipment([FromBody] Equipment equipment)
        {
            try
            {
                if (equipment == null)
                {
                    return BadRequest("Equipment data is null.");
                }

                EquipmentManager equipmentManager = new EquipmentManager();
                equipmentManager.UpdateEquipment(equipment);

                return Json(new { success = true, message = "Equipment updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Método HTTP DELETE para eliminar un equipo por su ID.
        [HttpDelete("{id}")]
        public ActionResult DeleteEquipment(int id)
        {
            try
            {
                EquipmentManager equipmentManager = new EquipmentManager();
                equipmentManager.DeleteEquipment(id);

                return Json(new { success = true, message = "Equipment deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
 