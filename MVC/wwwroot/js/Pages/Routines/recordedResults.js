document.addEventListener("DOMContentLoaded", function () {
    // Obt�n el routineId de la URL
    const urlParams = new URLSearchParams(window.location.search);
    const routineId = urlParams.get('routineId');

    // Verifica si routineId es v�lido antes de proceder
    if (routineId) {
        fetch(`https://localhost:7280/api/Routine/GetRecordedResults?routineId=${routineId}`)
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    const results = data.data;
                    const tbody = document.querySelector("#resultsTable tbody");
                    tbody.innerHTML = ''; // Limpia el contenido actual

                    results.forEach(result => {
                        const row = document.createElement("tr");
                        row.innerHTML = `
                            <td>${result.exerciseId || 'N/A'}</td>
                            <td>${result.setsCompleted !== null && result.setsCompleted !== undefined ? result.setsCompleted : 'N/A'}</td>
                            <td>${result.repetitionsCompleted !== null && result.repetitionsCompleted !== undefined ? result.repetitionsCompleted : 'N/A'}</td>
                            <td>${result.weightUsed !== null && result.weightUsed !== undefined ? result.weightUsed : 'N/A'}</td>
                            <td>${result.timeDuration || 'N/A'}</td>
                            <td>${result.amrapTime || 'N/A'}</td>
                        `;
                        tbody.appendChild(row);
                    });
                } else {
                    Swal.fire({
                        title: 'Error!',
                        text: data.message,
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                }
            })
            .catch(error)
                Swal.fire({
                    title: 'Error!',
                    text: error,
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
    } else {
    Swal.fire({
        title: 'Error!',
        text: 'routineId is not defined',
        icon: 'error',
        confirmButtonText: 'OK'
    });
    }
});
