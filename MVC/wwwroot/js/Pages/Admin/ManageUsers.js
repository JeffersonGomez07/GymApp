$(document).ready(function () {
    // Cargar usuarios desde la API
    $.ajax({
        url: 'https://localhost:7280/api/Users/GetAllUsers',
        type: 'GET',
        success: function (data) {
            var usersTable = $('#usersTable tbody');
            usersTable.empty();

            data.forEach(function (user) {
                var row = '<tr data-user-id="' + user.id + '">' +
                    '<td>' + user.first_name + '</td>' +
                    '<td>' + user.last_name + '</td>' +
                    '<td>' + user.username + '</td>' +
                    '<td>' + user.email + '</td>' +
                    '<td>' + user.phone_number + '</td>' +
                    '<td>' + new Date(user.birthdate).toISOString().split('T')[0] + '</td>' +
                    '<td>' +
                    '<button class="btn btn-primary edit-btn">Edit</button> ' +
                    '<button class="btn btn-success save-btn" style="display:none;">Save</button>' +
                    '</td>' +
                    '</tr>';
                usersTable.append(row);
            });

            // Editar usuario
            $('.edit-btn').click(function () {
                var row = $(this).closest('tr');
                row.find('td').not(':last').each(function () {
                    var text = $(this).text();
                    $(this).html('<input type="text" class="form-control" value="' + text + '" />');
                });
                row.find('.edit-btn').hide();
                row.find('.save-btn').show();
            });

            // Guardar cambios del usuario
            $('.save-btn').click(function () {
                var row = $(this).closest('tr');
                var user = {
                    id: row.data('user-id'),
                    first_name: row.find('input').eq(0).val(),
                    last_name: row.find('input').eq(1).val(),
                    username: row.find('input').eq(2).val(),
                    email: row.find('input').eq(3).val(),
                    phone_number: row.find('input').eq(4).val(),
                    birthdate: new Date(row.find('input').eq(5).val()).toISOString()
                };

                // Llamada a la API para actualizar el usuario
                $.ajax({
                    url: 'https://localhost:7280/api/Users/UpdateUser',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(user),
                    success: function () {
                        alert('User updated successfully!');
                        row.find('input').each(function (index) {
                            var text = $(this).val();
                            $(this).parent().text(text);
                        });
                        row.find('.edit-btn').show();
                        row.find('.save-btn').hide();
                    },
                    error: function (error) {
                        console.error('Error updating user:', error);
                        alert('Failed to update user.');
                    }
                });
            });

            // Filtro de búsqueda
            $('#searchInput').on('keyup', function () {
                var value = $(this).val().toLowerCase();
                $('#usersTable tbody tr').filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                });
            });
        },
        error: function (error) {
            console.error('Error fetching users:', error);
        }
    });
});
