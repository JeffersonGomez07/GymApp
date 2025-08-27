const handleAppointment = (event) => {
    Swal.fire({
        title: "Success",
        text: "Your measurement appointment has been successfully scheduled!",
        icon: "success"
    });
};

$("#btnAppointment").click(handleAppointment);