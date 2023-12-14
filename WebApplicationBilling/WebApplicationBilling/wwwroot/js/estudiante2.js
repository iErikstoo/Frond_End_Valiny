document.addEventListener('DOMContentLoaded', function () {
    loadEstudiantes();
});

function loadEstudiantes() {
    fetch('/Estudiantes/GetAllEstudiantes') // Asegúrate de reemplazar con la ruta correcta
        .then(response => response.json())
        .then(data => {
            initializeDataTable(data.data);
        })
        .catch(error => console.error('Error:', error));
}


function initializeDataTable(estudiantes) {
    let table = document.getElementById('estudiantesTable');
    if (!table) {
        table = document.createElement('table');
        table.id = 'estudiantesTable';
        table.className = 'display'; // Clase necesaria para DataTables
        document.getElementById('estudiantesContainer').appendChild(table);
    }

    $(table).DataTable({
        responsive: true,
        data: estudiantes,
        columns: [
            { title: "EstudianteId", data: "estudianteId", className: "column-id" },
            { title: "P_Nombre", data: "p_Nombre", className: "column-name" },
            { title: "S_Nombre", data: "s_Nombre", className: "column-name" },
            { title: "T_Nombre", data: "t_Nombre", className: "column-name" },
            { title: "P_Apellido", data: "p_Apellido", className: "column-name" },
            { title: "S_Apellido", data: "s_Apellido", className: "column-name" },
            { title: "T_Documento", data: "t_Documento", className: "column-country" },

            {
                title: "Acciones",
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                                <a href="/Estudiantes/Detail/${data}" class="btn btn-primary"><i class="fa fa-eye"></i></a>
                                <a href="/Estudiantes/Edit/${data}" class="btn btn-secondary"><i class="fa fa-edit"></i></a>
                                <a onclick="Delete('/Estudiantes/Delete/${data}')" class="btn btn-danger"><i class="fa fa-trash"></i></a>
                            </div>`;
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
                        $('#estudiantesTable').DataTable().clear().destroy();
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

