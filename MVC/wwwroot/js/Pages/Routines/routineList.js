document.addEventListener("DOMContentLoaded", function () {
    fetchRoutines();
}); // Carga del DOM y Fetch de Rutinas

function fetchRoutines() {
    const apiUrl = API_URL_BASE + "/api/Routine/GetAllRoutineList"; // Define la URL de la API para obtener todas las rutinas
    fetch(apiUrl) // Realiza una solicitud HTTP a la URL de la API definida.
        .then(response => response.json()) // Convierte la respuesta en formato JSON.
        .then(routines => {
            if (routines && routines.length) {
                routines.forEach(routine => {
                    fetchRoutineExercises(routine);
                });
            } else {
                console.error("No routines found.");
            }
        })
        .catch(error => console.error("Error fetching routines:", error)); // Maneja los datos obtenidos de la API, Si hay rutinas disponibles, se llama a fetchRoutineExercises para cada rutina
    // Si no hay rutinas, se muestra un error en la consola.
}

function fetchRoutineExercises(routine) {
    const apiUrl = API_URL_BASE + "/api/Routine/GetExercisesForRoutine?routineId=" + routine.routineId;
    fetch(apiUrl)
        .then(response => response.json())
        .then(exercises => { // Pasa los ejercicios obtenidos y la rutina actual a la función populateRoutineTable.
            populateRoutineTable(routine, exercises);
        })
        .catch(error => console.error("Error fetching exercises:", error));
}

function populateRoutineTable(routine, exercises) {
    const routineList = document.getElementById("routine-list");

    // Formatear la fecha de creación de la rutina
    const creationDate = new Date(routine.creationDate);
    const options = { day: 'numeric', month: 'short', year: 'numeric' };
    const formattedDate = creationDate.toLocaleDateString('es-ES', options);

    // Crear la fila para la rutina
    const row = document.createElement("tr");
    row.innerHTML = `
        <td>${routine.memberUsername || 'N/A'}</td>
        <td>${routine.instructorUsername}</td>
        <td>${routine.name}</td>
        <td>${routine.description}</td>
        <td>${formattedDate}</td>
        <td>
            <a href="/Routine/Results?routineId=${routine.routineId}" class="btn btn-primary">Results</a>
            <a href="/Routine/RecordResults?routineId=${routine.routineId}" class="btn btn-secondary">Record Results</a>
            <button class="btn btn-danger" onclick="deleteRoutine(${routine.routineId})">Delete</button>
        </td>
    `;
    routineList.appendChild(row);

    if (exercises && exercises.length) {
        exercises.forEach(exercise => {
            let exerciseContent = `<strong>Exercise:</strong> ${exercise.exerciseName || 'N/A'}`;

            if (exercise.TypeName !== null) {
                exerciseContent += `, <strong>Type:</strong> ${exercise.exerciseTypeId || 'N/A'}`;
            }
            if (exercise.sets !== null) {
                exerciseContent += `, <strong>Sets:</strong> ${exercise.sets}`;
            }
            if (exercise.repetitions !== null) {
                exerciseContent += `, <strong>Reps:</strong> ${exercise.repetitions}`;
            }
            if (exercise.weight !== null) {
                exerciseContent += `, <strong>Weight:</strong> ${exercise.weight}`;
            }
            if (exercise.timeDuration !== null) {
                exerciseContent += `, <strong>Duration:</strong> ${exercise.timeDuration}`;
            }
            if (exercise.amrapTime !== null) {
                exerciseContent += `, <strong>AMRAP Time:</strong> ${exercise.amrapTime}`;
            }

            const exerciseRow = document.createElement("tr");
            exerciseRow.innerHTML = `
                <td colspan="7" style="padding-left: 20px;">
                    ${exerciseContent}
                </td>
            `;
            routineList.appendChild(exerciseRow);
        });
    } else {
        const noExerciseRow = document.createElement("tr");
        noExerciseRow.innerHTML = `<td colspan="7" style="padding-left: 20px;">No exercises found for this routine.</td>`;
        routineList.appendChild(noExerciseRow);
    }
}

// Definir la función deleteRoutine fuera de populateRoutineTable
function deleteRoutine(routineId) {
    const apiUrl = `https://localhost:7280/api/Routine/DeleteRoutine/${routineId}`;

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            // Perform the delete operation
            fetch(apiUrl, {
                method: 'DELETE'
            })
                .then(response => {
                    if (response.ok) {
                        swalWithBootstrapButtons.fire(
                            "Deleted!",
                            "The routine has been deleted.",
                            "success"
                        ).then(() => {
                            // Reload the list of routines after deletion
                            fetchRoutines();
                        });
                    } else {
                        swalWithBootstrapButtons.fire(
                            "Error!",
                            "There was an error deleting the routine.",
                            "error"
                        );
                    }
                })
                .catch(error => {
                    console.error("Error deleting routine:", error);
                    swalWithBootstrapButtons.fire(
                        "Error!",
                        "There was an error deleting the routine.",
                        "error"
                    );
                });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                "Cancelled",
                "The routine is safe :)",
                "error"
            );
        }
    });
}
