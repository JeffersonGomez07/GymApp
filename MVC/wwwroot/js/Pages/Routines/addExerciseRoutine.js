
document.addEventListener('DOMContentLoaded', function () {
    fetch('https://localhost:7280/api/Routine/GetAllRoutines') //Realiza una solicitud a la API para obtener todas las rutinas.
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            const routineSelect = document.getElementById('routine_id');
            if (routineSelect) {
                data.forEach(routine => {
                    const option = document.createElement('option');
                    option.value = routine.routineId;
                    option.textContent = routine.name;
                    routineSelect.appendChild(option);
                });
            } else {
                console.error('Element with ID "routine_id" not found');
            }
        })
        .catch(error => {
            console.error('Error fetching routines:', error);
            Swal.fire({
                title: 'Error',
                text: 'Failed to fetch routines. Please try again.',
                icon: 'error'
            });
        });

    fetch(API_URL_BASE + "/api/Exercise/GetAllExercises")
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            const exerciseSelect = document.getElementById('exercise_id');
            if (exerciseSelect) {
                data.forEach(exercise => {
                    const option = document.createElement('option');
                    option.value = exercise.exerciseId;
                    option.textContent = exercise.name;
                    option.dataset.type = exercise.exerciseTypeId;
                    exerciseSelect.appendChild(option);
                });
            } else {
                console.error('Element with ID "exercise_id" not found');
            }
        })
        .catch(error => {
            console.error('Error fetching exercises:', error);
            Swal.fire({
                title: 'Error',
                text: 'Failed to fetch exercises. Please try again.',
                icon: 'error'
            });
        });

    const form = document.getElementById('selectExercisesForm');
    if (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();

            const routineExercise = {
                routineId: parseInt(document.getElementById('routine_id').value),
                exerciseId: parseInt(document.getElementById('exercise_id').value),
                exerciseTypeId: parseInt(document.getElementById('exercise_type_id').value),
                sets: document.getElementById('sets').value ? parseInt(document.getElementById('sets').value) : null,
                repetitions: document.getElementById('repetitions').value ? parseInt(document.getElementById('repetitions').value) : null,
                weight: document.getElementById('weight').value ? parseFloat(document.getElementById('weight').value) : null,
                timeDuration: document.getElementById('time_duration').value || null,
                amrapTime: document.getElementById('amrap_time').value || null
            };


            fetch(API_URL_BASE + '/api/Routine/AddExerciseToRoutine', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(routineExercise)
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        Swal.fire({
                            title: 'Success',
                            text: 'Exercise successfully added to the routine!',
                            icon: 'success'
                        }).then(() => {
                            // Redirigir a la página de confirmación o de inicio
                            window.location.href = "/Routine/addExerciseRoutine";
                        });
                    } else {
                        Swal.fire({
                            title: 'Error',
                            text: 'Error: ' + data.message,
                            icon: 'error'
                        });
                    }
                })
                .catch(error => {
                    console.error('Error adding exercise:', error);
                    Swal.fire({
                        title: 'Error',
                        text: 'There was an error adding exercise. Please try again.',
                        icon: 'error'
                    });
                });
        });
    } else {
        console.error('Form with ID "selectExercisesForm" not found');
    }

    document.getElementById('exercise_id').addEventListener('change', function () {
        const selectedOption = this.options[this.selectedIndex];
        const exerciseTypeId = selectedOption.dataset.type;
        document.getElementById('exercise_type_id').value = exerciseTypeId;
        toggleFieldsBasedOnType(exerciseTypeId);
    });

    function toggleFieldsBasedOnType(exerciseTypeId) {
        const timeDurationField = document.getElementById('time_duration');
        const amrapTimeField = document.getElementById('amrap_time');
        const setsField = document.getElementById('sets');
        const repetitionsField = document.getElementById('repetitions');
        const weightField = document.getElementById('weight');

        // Configuraci�n basada en el ID del tipo de ejercicio
        switch (parseInt(exerciseTypeId)) {
            case 1: // Weight-Based
                timeDurationField.disabled = true;
                amrapTimeField.disabled = true;
                setsField.disabled = false;
                repetitionsField.disabled = false;
                weightField.disabled = false;
                break;
            case 2: // Time-Based
                setsField.disabled = true;
                repetitionsField.disabled = true;
                weightField.disabled = true;
                amrapTimeField.disabled = true;
                timeDurationField.disabled = false;
                break;
            case 3: // AMRAP
                setsField.disabled = false;
                repetitionsField.disabled = false;
                weightField.disabled = false;
                timeDurationField.disabled = true;
                break;
            default:
                console.warn('Unknown exercise type ID:', exerciseTypeId);
                break;
        }
    }
});
