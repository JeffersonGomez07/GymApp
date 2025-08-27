using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BL;
using System.Net.Mail;

namespace API.Controllers.Emails
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailsController : Controller
    {
        private readonly EmailManager _emailService;

        public EmailsController(EmailManager emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail([FromBody] ResetPasswordRequest request)
        {
            //Verificamos que si se ingreso un email
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required.");
            }

            //Obtenemos el usuario por email y verificamos que existe
            UserManager manager = new UserManager();
            User user = manager.GetUserByEmail(request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Generamos un token de restablecimiento de contraseña
            var resetToken = Guid.NewGuid().ToString();

            //Enviamos el token en la tabla de usuario y verificamso que se guardo 
            bool tokenSaved = manager.AddResetToken(user.Id, resetToken);
            if (!tokenSaved)
            {
                return StatusCode(500, "Unable to save the reset token.");
            }

            //Generamos el link de restablecimiento con el token 
            var resetLink = $"{request.ResetUrl}?token={resetToken}";

            //Url.Action("ResetPassword", "Users", new { token = resetToken }, protocol: HttpContext.Request.Scheme);

            // Obtenemos el cuerpo del correo electrónico usando la plantilla HTML
            var emailBody = _emailService.GetResetPasswordEmailBody(resetLink);

            // Enviamos el correo de restablecimiento
            await _emailService.SendEmailAsync(user.Email, "User", "Reset Password", emailBody);

            return Ok(new { Message = "Reset password email sent." });
        }

        [HttpPost]
        public async Task<IActionResult> SendVerifyAccountEmail([FromBody] VerifyAccountRequest emailRequest)
        {
            UserManager user = new UserManager();

            if (string.IsNullOrEmpty(emailRequest.Email))
            {
                return BadRequest(new { errorMessage = "Email is required." });
            }

            string otpCode = _emailService.GenerateOTP();

            var emailBody = _emailService.GetOtpEmailBody(otpCode);

            user.AddOtpCode(emailRequest.Email, otpCode);

            await _emailService.SendEmailAsync(emailRequest.Email, "User", "Verify Account", emailBody);

            return Ok(new { Message = "OTP email sent successfully." });
        }
    }

}
