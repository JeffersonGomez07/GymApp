const handleCreateRoutine = (event) => {
    event.preventDefault()
    //Recopilar la informacion y mandarla al API

    const routine = {}
    routine.exercise_name = $("#exercise_name").val()
    routine.exercise_type = $("#exercise_type").val()
    routine.sets = $("#sets").val()
    routine.weight = $("#weight").val()
    routine.time_duration = $("#time_duration").val()
    routine.machine = $("#machine").val()

    const apiUrl = API_BASE_URL + "/Routine/CreateRoutine"
    $.ajax({
        url: apiUrl,
        method: "POST",
        hasContent: true,
        data: JSON.stringify(exercise),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
      })
        .done( (result) => {
            console.log(result);
             Swal.fire({
             title: "Mensaje",
             text: "Routine created successfully",
             icon: "success",
           })
        }).fail((responseData) => {
            if (responseData.responseCode) {
                console.error(responseData.responseCode);
            }
        });
        
}

$("#createRoutineForm").on("submit", handleCreateRoutine)