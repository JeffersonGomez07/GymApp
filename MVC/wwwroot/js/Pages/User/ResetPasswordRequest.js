const handleResetPassword = (event) => {
    event.preventDefault();

    // Obtiene el email y se lo manda al Api 
    const email = $("#email").val()

    //Construimos la URL de restablecimiento de contraseña
    const resetLink = UI_URL_BASE + "/User/ResetPassword";

    // Estructuramos el objeto de ResetPasswordRequest para enviar a la API
    const data = {
        email: email,
        resetUrl: resetLink
    }

    const apiUrl = API_URL_BASE + "/api/Emails/SendResetPasswordEmail";

            $.ajax({
                headers: {
                    'Accept': "application/json",
                    'Content-Type': "application/json"
                },
                method: "POST",
                url: apiUrl,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                hasContent: true
            }).done(function (data) {
                Swal.fire({
                    title: 'Mensaje',
                    text:   "We've sent you an email with instructions to reset your password.",
                    icon: 'info'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Redirigir a la página de log in  
                        window.location.href = "/User/Login";
                    }
                });
            }).fail(function (xhr, status, error) {
                let errorMessage = "Unknown error";
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMessage = xhr.responseJSON.message;
                } else if (xhr.responseText) {
                    errorMessage = xhr.responseText;
                } else if (error) {
                    errorMessage = error;
                }
                Swal.fire({
                    title: 'Error!',
                    text: errorMessage,
                    icon: 'error'
                });
            });
};

$("#btnResetPassword").click(handleResetPassword);