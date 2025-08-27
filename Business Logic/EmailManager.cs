using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace BL
{
    public class EmailManager
    {
        //Clase encargada de la lógica para enviar emails

        private readonly BrevoSettings _settings;

        public EmailManager(IOptions<BrevoSettings> settings)
        {
            _settings = settings.Value;
        }

        //Metodo para enviar el Email 
        public async System.Threading.Tasks.Task SendEmailAsync(string toEmail, string toName, string subject, string body)
        {
            Configuration.Default.ApiKey.Clear();
            Configuration.Default.ApiKey.Add("api-key", _settings.ApiKey);

            var apiInstance = new TransactionalEmailsApi();

            var email = new SendSmtpEmail(
                to: new List<SendSmtpEmailTo>
                {
                    new SendSmtpEmailTo(toEmail, toName)
                },
                subject: subject,
                htmlContent: body,
                sender: new SendSmtpEmailSender("Fitness Center Notifications", "fitnesscentercenfo@gmail.com")
            );

            try
            {
                var result = await apiInstance.SendTransacEmailAsync(email);
                Console.WriteLine(result.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling TransactionalEmailsApi.SendTransacEmailAsync: " + e.Message);
                throw;
            }
         
        }

        //Metodo para generar un OTP 
        public string GenerateOTP()
        {
            var rng = new Random();
            return rng.Next(100000, 999999).ToString();
        }

        //Body del Reset password 
        public string GetResetPasswordEmailBody(string resetLink)
        {
            return $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Reset Your Password</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #ffffff;
                        border: 1px solid #dddddd;
                        border-radius: 5px;
                    }}
                    .header {{
                        text-align: center;
                        padding: 20px 0;
                    }}
                    .header img {{
                        width: 100px;
                        height: auto;
                    }}
                    .content {{
                        padding: 20px;
                        text-align: center;
                    }}
                    .content p {{
                        font-size: 16px;
                        color: #333333;
                    }}
                    .button {{
                        margin: 20px 0;
                    }}
                    .button a {{
                        background-color: #007bff;
                        color: #ffffff;
                        padding: 15px 25px;
                        text-decoration: none;
                        border-radius: 5px;
                        display: inline-block;
                    }}
                    .footer {{
                        text-align: center;
                        padding: 20px;
                        font-size: 12px;
                        color: #777777;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <img src=""~/css/img/LogoFitnessCenter.png"" alt=""Logo"">
                    </div>
                    <div class=""content"">
                        <h1>Forgot Your Password?</h1>
                        <p>We've got a request from you to reset the password for your account. Please click on the button below to get a new password.</p>
                        <div class=""button"">
                            <a href=""{resetLink}"" target=""_blank"">Reset Password</a>
                        </div>
                        <p>If you did not request a password reset, please ignore this email.</p>
                    </div>
                    <div class=""footer"">
                        &copy; 2024 GymApp. All rights reserved.
                    </div>
                </div>
            </body>
            </html>";
        }

        public string GetOtpEmailBody(string otpCode)
        {
            return $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Verify Your Email</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #ffffff;
                        border: 1px solid #dddddd;
                        border-radius: 5px;
                    }}
                    .header {{
                        text-align: center;
                        padding: 20px 0;
                    }}
                    .header img {{
                        width: 100px;
                        height: auto;
                    }}
                    .content {{
                        padding: 20px;
                        text-align: center;
                    }}
                    .content p {{
                        font-size: 16px;
                        color: #333333;
                    }}
                    .otp-code {{
                        margin: 20px 0;
                        font-size: 24px;
                        font-weight: bold;
                        color: #007bff;
                    }}
                    .footer {{
                        text-align: center;
                        padding: 20px;
                        font-size: 12px;
                        color: #777777;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <img src=""~/css/img/LogoFitnessCenter.png"" alt=""Logo"">
                    </div>
                    <div class=""content"">
                        <h1>Email Verification</h1>
                        <p>Thank you for registering with us. To complete your registration, please use the following One-Time Password (OTP) to verify your email address.</p>
                        <div class=""otp-code"">
                            {otpCode}
                        </div>
                        <p>If you did not register with us, please ignore this email.</p>
                    </div>
                    <div class=""footer"">
                        &copy; 2024 GymApp. All rights reserved.
                    </div>
                </div>
            </body>
            </html>";
        }

    }
}