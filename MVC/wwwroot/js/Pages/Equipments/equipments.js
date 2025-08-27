$(document).ready(function () {
    loadEquipments();

    // Manejar el envío del formulario de creación de equipos
    $("#createEquipmentForm").on("submit", function (event) {
        event.preventDefault();
        const equipment = {
            name: $("#name").val(),
            description: $("#description").val(),
            quantity: $("#quantity").val(),
            status: $("#status").val()
        };

        if (validateEquipmentForm(equipment)) {
            createEquipment(equipment);
        }
    });

    // Manejar el envío del formulario de edición de equipos
    $("#editEquipmentForm").on("submit", function (event) {
        event.preventDefault();
        const equipment = {
            equipmentId: $("#editEquipmentId").val(),
            name: $("#editName").val(),
            description: $("#editDescription").val(),
            quantity: $("#editQuantity").val(),
            status: $("#editStatus").val()
        };

        if (validateEquipmentForm(equipment)) {
            editEquipment(equipment);
        }
    });

    // Manejar el clic del botón de eliminación
    $(document).on("click", ".delete-btn", function () {
        const equipmentId = $(this).data("id");
        deleteEquipment(equipmentId);
    });

    // Manejar el clic del botón de edición
    $(document).on("click", ".edit-btn", function () {
        const equipmentId = $(this).data("id");
        const equipmentName = $(this).data("name");
        const equipmentDescription = $(this).data("description");
        const equipmentQuantity = $(this).data("quantity");
        const equipmentStatus = $(this).data("status");

        // Llenar el formulario de edición con los datos del equipo
        $("#editEquipmentId").val(equipmentId);
        $("#editName").val(equipmentName);
        $("#editDescription").val(equipmentDescription);
        $("#editQuantity").val(equipmentQuantity);
        $("#editStatus").val(equipmentStatus);

        // Mostrar la sección de edición
        $("#editEquipmentSection").show();
    });
});

function loadEquipments() {
    $.ajax({
        url: 'https://localhost:7280/api/Equipments/GetAllEquipment',
        method: 'GET',
        success: function (data) {
            if (data && data.length > 0) {
                renderEquipmentList(data);
            } else {
                Swal.fire({
                    title: "Información",
                    text: "No hay equipos disponibles para mostrar.",
                    icon: "info",
                });
            }
        },
        error: function (error) {
            console.error("Error al cargar los equipos: ", error);
            Swal.fire({
                title: "Error",
                text: "Hubo un problema al cargar la lista de equipos.",
                icon: "error",
            });
        }
    });
}

function createEquipment(equipment) {
    $.ajax({
        url: 'https://localhost:7280/api/Equipments/CreateEquipment',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(equipment),
        success: function (response) {
            Swal.fire({
                title: "Éxito",
                text: "¡Equipo creado exitosamente!",
                icon: "success",
            }).then(() => {
                $("#createEquipmentForm")[0].reset(); // Limpiar el formulario después de crear
                loadEquipments();
            });
        },
        error: function (error) {
            console.error("Error al crear el equipo: ", error);
            Swal.fire({
                title: "Error",
                text: "No se pudo crear el equipo. Por favor, intente de nuevo.",
                icon: "error",
            });
        }
    });
}

function editEquipment(equipment) {
    $.ajax({
        url: 'https://localhost:7280/api/Equipments/EditEquipment',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(equipment),
        success: function (response) {
            Swal.fire({
                title: "Éxito",
                text: "¡Equipo actualizado exitosamente!",
                icon: "success",
            }).then(() => {
                $("#editEquipmentSection").hide();
                loadEquipments();
            });
        },
        error: function (error) {
            console.error("Error al actualizar el equipo: ", error);
            Swal.fire({
                title: "Error",
                text: "No se pudo actualizar el equipo. Por favor, intente de nuevo.",
                icon: "error",
            });
        }
    });
}

function deleteEquipment(equipmentId) {
    Swal.fire({
        title: "¿Está seguro?",
        text: "Esta acción no se puede deshacer.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `https://localhost:7280/api/Equipments/DeleteEquipment/${equipmentId}`,
                method: 'DELETE',
                success: function (response) {
                    Swal.fire({
                        title: "Éxito",
                        text: "¡Equipo eliminado exitosamente!",
                        icon: "success",
                    }).then(() => {
                        loadEquipments();
                    });
                },
                error: function (error) {
                    console.error("Error al eliminar el equipo: ", error);
                    Swal.fire({
                        title: "Error",
                        text: "No se pudo eliminar el equipo. Por favor, intente de nuevo.",
                        icon: "error",
                    });
                }
            });
        }
    });
}

function renderEquipmentList(equipments) {
    let tableBody = $("#equipmentTable tbody");
    tableBody.empty();
    equipments.forEach(function (equipment) {
        let row = `<tr>
            <td>${equipment.name}</td>
            <td>${equipment.description}</td>
            <td>${equipment.quantity}</td>
            <td>${equipment.status}</td>
            <td>
                <button class="edit-btn btn btn-primary" 
                        data-id="${equipment.equipmentId}" 
                        data-name="${equipment.name}" 
                        data-description="${equipment.description}" 
                        data-quantity="${equipment.quantity}" 
                        data-status="${equipment.status}">Editar</button>
                <button class="delete-btn btn btn-danger" data-id="${equipment.equipmentId}">Eliminar</button>
            </td>
        </tr>`;
        tableBody.append(row);
    });
}

function validateEquipmentForm(equipment) {
    if (!equipment.name || !equipment.description || equipment.quantity == null || !equipment.status) {
        Swal.fire({
            title: "Advertencia",
            text: "Por favor, complete todos los campos antes de enviar.",
            icon: "warning",
        });
        return false;
    }
    return true;
}
