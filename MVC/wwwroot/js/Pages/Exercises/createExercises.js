document.addEventListener('DOMContentLoaded', function () {
    fetch('https://localhost:7280/api/ExerciseType/GetAllExerciseTypes')
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            const exerciseTypeSelect = document.getElementById('exercise_type_id');
            if (exerciseTypeSelect) {
                data.forEach(exerciseType => {
                    const option = document.createElement('option');
                    option.value = exerciseType.exerciseTypeId;
                    option.textContent = exerciseType.typeName;
                    exerciseTypeSelect.appendChild(option);
                });
            } else {
                console.error('Element with ID "exercise_type_id" not found');
            }
        })
        .catch(error => {
            console.error('Error fetching exercise types:', error);
            Swal.fire({
                title: 'Error',
                text: 'Failed to fetch exercise types. Please try again.',
                icon: 'error'
            });
        });
});
const handleCreateExercise = (event) => {
    event.preventDefault();

    // Recopilar la informaci�n y mandarla al API
    const exercise = {
        exerciseTypeId: $("#exercise_type_id").val(),
        name: $("#name").val(),
        description: $("#description").val(),
        primarymuscle: $("#primary_muscle").val()
    };

    const apiUrl = API_URL_BASE + "/api/Exercise/CreateExercise";

    $.ajax({
        url: apiUrl,
        method: "POST",
        hasContent: true,
        data: JSON.stringify(exercise),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
    }).done((result) => {
        console.log(result);
        Swal.fire({
            title: "Exercise Creation",
            text: "Exercise has been Successfully register!",
            icon: "success",
        }).then(() => {
            // Redirigir a la página de confirmación o de inicio
            window.location.href = "/Exercise/Create";
        });
    }).fail((jqXHR, textStatus, errorThrown) => {
        console.error(textStatus, errorThrown);
        Swal.fire({
            title: "Error",
            text: "Failed to create exercise. Please try again.",
            icon: "error",
        });
    });
};

$(document).ready(() => {
    $("#createExerciseForm").on("submit", handleCreateExercise);
});