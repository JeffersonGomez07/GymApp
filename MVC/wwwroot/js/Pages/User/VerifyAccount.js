const handleVerifyAccount = (event) => {
    event.preventDefault();

    // Obtiene el codigo para enviarselo al Api
    const otp = $("#otp").val()

    console.log(otp)
    const apiUrl = `${API_URL_BASE}/api/Users/VerifyAccount?otp=${encodeURIComponent(otp)}`;

    $.ajax({
        headers: {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        method: "GET",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        if (data.errorMessage) {
            Swal.fire({
                title: 'Error!',
                text: data.errorMessage,
                icon: 'error'
            });
        } else {
            Swal.fire({
                title: 'Mensaje',
                text: 'Account verified successfully!',
                icon: 'info'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Redirigir a la página de inicio de sesión
                    window.location.href = "/User/Login";
                }
            });
        }
    }).fail(function (jqXHR, status, error) {
        let errorMessage = "Unknown error";
        if (jqXHR.responseJSON && jqXHR.responseJSON.error_message) {
            errorMessage = jqXHR.responseJSON.errorMessage;
        } else if (jqXHR.responseText) {
            errorMessage = jqXHR.responseText;
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

$("#btnVerifyAccount").click(handleVerifyAccount);