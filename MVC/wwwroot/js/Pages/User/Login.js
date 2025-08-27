const handleLogin = (event) => {
    event.preventDefault();

    // Recopilar la información y enviarla al API
    var username = $("#username").val();
    var password = $("#password").val();

    // Estructuramos el objeto de ResetPassword para enviar a la API
    const data = {
        username: username,
        password: password
    }

    var apiUrl = API_URL_BASE + "/api/Users/Login";

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
        if (data.errorMessage) {
            Swal.fire({
                title: 'Error!',
                text: data.errorMessage,
                icon: 'error'
            }); 
        } else {
            localStorage.setItem('userId', data.userId);
                Swal.fire({
                    title: 'Welcome To',
                    text: 'Your Fitness Center Acount',
                    icon: 'success'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Redirigir a la página basada en el rol del usuario
                        switch (data.role) {

                            case 'Admin':
                                window.location.href = "/Admin/AdminPage";
                                break;
                            case 'Trainer':
                                window.location.href = "/Trainer/TrainerPage";
                                break;
                            case 'Receptionist':
                                window.location.href = "/Receptionist/ReceptionistPage";
                                break;
                            case 'Member':
                                window.location.href = "/Member/MemberPage";
                                break;
                            case 'User':
                                window.location.href = "/Default/DefaultPage";
                                break;
                            default:
                                window.location.href = "/Default/DefaultPage";
                                break;
                        }
                    }
                });
            }
    }).fail(function (jqXHR, textStatus, errorThrown) {
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
            text: 'Incorrect password, please check your password.',
            icon: 'error'
        });
    });
};

$("#btnLogin").click(handleLogin);