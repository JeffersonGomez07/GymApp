$(document).ready(function () {
    // Load members and instructors when the page is ready
    console.log("Page loaded");
    loadMembers();
    loadInstructors();

    // Handle form submission
    $('#submitRoutine').click(function () {
        console.log("Submit button clicked");
        createRoutine();
    });
});

function loadMembers() {
    $.ajax({
        type: "GET",
        url: "https://localhost:7280/api/Member/GetAllMembers",
        success: function (response) {
            console.log("Members loaded:", response);
            var $memberDropdown = $('#member_id');
            $memberDropdown.empty().append('<option value="">Select a member</option>');
            $.each(response, function (index, member) {
                $memberDropdown.append(new Option(member.username, member.member_id));
            });
        },
        error: function (xhr, status, error) {
            console.error("Error loading members:", error);
        }
    });
}

function loadInstructors() {
    $.ajax({
        type: "GET",
        url: "https://localhost:7280/api/Instructor/GetAllInstructors",
        success: function (response) {
            console.log("Instructors loaded:", response);
            var $instructorDropdown = $('#instructor_id');
            $instructorDropdown.empty().append('<option value="">Select an instructor</option>');
            $.each(response, function (index, instructor) {
                $instructorDropdown.append(new Option(instructor.username, instructor.instructorId));
            });
        },
        error: function (xhr, status, error) {
            console.error("Error loading instructors:", error);
        }
    });
}

function createRoutine() {
    var memberId = parseInt($("#member_id").val(), 10);
    var instructorId = parseInt($("#instructor_id").val(), 10);
    var name = $("#name").val().trim();
    var description = $("#description").val().trim();

    console.log("Creating routine with:", {
        memberId: memberId,
        instructorId: instructorId,
        name: name,
        description: description
    });

    // Ensure memberId and instructorId are valid integers
    if (isNaN(memberId) || isNaN(instructorId) || memberId <= 0 || instructorId <= 0) {
        $("#responseMessage").text("Please select a valid member and instructor.");
        return;
    }

    $.ajax({
        type: "POST",
        url: "https://localhost:7280/api/Routine/CreateRoutine",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            memberId: memberId,
            instructorId: instructorId,
            name: name,
            description: description
        }),
        success: function (response) {
            console.log("Routine created:", response);
            if (response.success) {
                // Show SweetAlert on success
                Swal.fire({
                    title: 'Success!',
                    text: 'Routine created successfully',
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then(() => {
                    // Redirigir a la página de confirmación o de inicio
                    window.location.href = "/Routine/Create";
                });
            } else {
                // Show SweetAlert on error
                Swal.fire({
                    title: 'Error!',
                    text: response.message,
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }
        },
        error: function (xhr, status, error) {
            console.error("Error creating routine:", error);
            // Show SweetAlert on AJAX error
            Swal.fire({
                title: 'Error!',
                text: 'An error occurred while creating the routine.',
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
    });
}
