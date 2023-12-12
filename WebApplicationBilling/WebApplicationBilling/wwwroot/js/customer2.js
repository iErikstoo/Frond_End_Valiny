﻿document.addEventListener('DOMContentLoaded', function () {
    loadCustomers();
});

function loadCustomers() {
    fetch('/Customers/GetAllCustomers') 
        .then(response => response.json())
        .then(data => {
            initializeDataTable(data.data);
        })
        .catch(error => console.error('Error:', error));
}

function initializeDataTable(customers) {
    // Crear una tabla si aún no existe
    let table = document.getElementById('customersTable');
    if (!table) {
        table = document.createElement('table');
        table.id = 'customersTable';
        table.className = 'display'; // Clase necesaria para DataTables
        document.getElementById('customersContainer').appendChild(table);
    }

    $(table).DataTable({
        responsive: true,
        data: customers,
        columns: [
            { title: "ID", data: "id", className: "column-id" },
            { title: "Nombre", data: "firstName", className: "column-name" },
            { title: "Apellido", data: "lastName", className: "column-name" },
            { title: "País", data: "country", className: "column-country" },
            { title: "Teléfono", data: "phone", className: "column-phone" },
            {
                title: "Acciones", data: null, className: "column-actions",
                render: function (data, type, row) {
                    return `<div>
                    <a href="/Customers/Detail/${data}"><i class="fa fa-eye"></i></a> ' +
                        <a href="/Customers/Edit/${data}"><i class="fa fa-edit"></i></a> ' +
                        <a onclick=Delete('/Customers/Delete/${data}')<i class="fa fa-trash"></i></a>;
                        </div >`
                },
                className: "column-actions"
            }
        ]
    });

}
function Delete(url) {
    Swal.fire({
        title: "¿Está seguro de querer borrar el registro?",
        text: "¡Esta acción no puede ser revertida!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, bórralo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (response) {
                    if (response && response.success) {
                        toastr.success(response.message || "Registro eliminado con éxito.");
                        // Recargar DataTables
                        $('#customersTable').DataTable().clear().destroy();
                        loadCustomers();
                    } else {
                        toastr.error(response.message || "Ocurrió un error desconocido.");
                    }
                },
                error: function (error) {
                    toastr.error("Error al intentar eliminar el registro.");
                    console.error('Error:', error);
                }
            });
        }
    });
}
