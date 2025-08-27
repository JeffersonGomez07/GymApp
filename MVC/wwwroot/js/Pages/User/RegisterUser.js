const handleCreateUser = (event) => {
        event.preventDefault();

        // recopilar la informacion y mandarla al Api
        const user = {
            First_name: $("#firstName").val(),
            Last_name: $("#lastName").val(),
            Username: $("#username").val(),
            Email: $("#email").val(),
            Password: $("#password").val(),
            Phone_number: $("#phoneNumber").val(),
            Birthdate: $("#birthDate").val()
        };

        const profileImageFile = $("#profileImage")[0].files[0];
        const idImageFile = $("#idImage")[0].files[0];

        const reader = new FileReader();
        reader.onload = function () {
            user.Profile_image = reader.result.split(',')[1];

            const reader2 = new FileReader();
            reader2.onload = function () {
                user.Id_image = reader2.result.split(',')[1];

                var apiUrl = API_URL_BASE + "/api/Users/RegisterUser";

                $.ajax({
                    headers: {
                        'Accept': "application/json",
                        'Content-Type': "application/json"
                    },
                    method: "POST",
                    url: apiUrl,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(user),
                    hasContent: true
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
                            text: "We've sent you a verification code, please check your email to verify your account!",
                            icon: 'info'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                // Llamar a la API para enviar el correo de verificación

                                var apiUrl = API_URL_BASE + "/api/Emails/SendVerifyAccountEmail";

                                $.ajax({
                                    headers: {
                                        'Accept': "application/json",
                                        'Content-Type': "application/json"
                                    },
                                    method: "POST",
                                    url: apiUrl,
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ email: user.Email }),
                                    hasContent: true
                                }).done(function (data) {
                                    // Redirigir a la página para ingresar el OTP
                                    window.location.href = "/User/VerifyAccount";

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
                            }
                        });
                    } 
                }).fail(function (xhr, status, error) {
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

            reader2.readAsDataURL(idImageFile);
        };

        reader.readAsDataURL(profileImageFile);
    };

$("#btnCreate").click(handleCreateUser);