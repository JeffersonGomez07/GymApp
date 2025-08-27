using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BL;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers.Memberships
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MembershipsController : Controller
    {
        private readonly MembershipManager _membershipManager;

        // Constructor
        public MembershipsController()
        {
            _membershipManager = new MembershipManager();
        }

        //Metodo para registrar el pago de la membresia 
        [HttpPost]
        public ActionResult ProcessPayment()
        {
            var userIdString = Request.Form["userId"];
            var membershipType = Request.Form["membershipType"];
            var paymentMethod = Request.Form["paymentMethod"];
            var amountString = Request.Form["amount"];
            var receiptImageBase64 = Request.Form["receiptImageBase64"];

            if (string.IsNullOrEmpty(userIdString) || string.IsNullOrEmpty(membershipType) ||
                string.IsNullOrEmpty(paymentMethod) || string.IsNullOrEmpty(amountString) )
            {
                return BadRequest("Missing required fields.");
            }

            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID.");
            }

            if (!decimal.TryParse(amountString, out var amount))
            {
                return BadRequest("Invalid amount.");
            }

            var payment = new PaymentInfo
            {
                UserId = userId,
                MembershipType = membershipType,
                Amount = amount,
                PaymentMethod = paymentMethod,
                ReceiptImage = receiptImageBase64, // Base64 string for receipt image
            };

            string errorMessage = _membershipManager.ProcessPayment(payment);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return Ok(new { error = errorMessage });
            }

            return Ok(new { success = true });
        }

        // Método para aprobar el pago de la membresia 
        [HttpPost]
        public IActionResult ApproveMembershipPayment([FromBody] ApprovePaymentRequest payment)
        {
            if (payment == null)
            {
              return BadRequest("Apporve payment request data is null.");
            }

            var errorMessage = _membershipManager.ApproveMembershipPayment(payment);

            if (string.IsNullOrEmpty(errorMessage))
            {
                return Ok(new { error = errorMessage });
            }

            return Ok(new { success = true });
        }

        // Método para subir el recibo del pago de la membresia 
        [HttpPost]
        public IActionResult UploadPaymentReceipt([FromBody] UploadPaymentRecipt payment)
        {
            if (payment == null)
            {
                return BadRequest("Upload receipt payment request data is null.");
            }

            var errorMessage = _membershipManager.UploadPaymentReceipt(payment);

            if (string.IsNullOrEmpty(errorMessage))
            {
                return Ok(new { error = errorMessage });
            }

            return Ok(new { success = true });
        }

        //Metodo para obtener la informacion de todas las membresias 
        [HttpGet]
        public IActionResult GetAllMemberships()
        {
            var memberships = _membershipManager.GetAllMemberships();

            if (memberships == null || memberships.Count == 0)
            {
                return NotFound("No memberships found.");
            }

            return Ok(memberships);
        }

    }
}
