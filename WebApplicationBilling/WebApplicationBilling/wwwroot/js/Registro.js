document.addEventListener('DOMContentLoaded', function () {
    loadRegistros();
});

function loadRegistros() {
    fetch('/Registros/GetAllRegistros') // Asegúrate de reemplazar con la ruta correcta
        .then(response => response.json())
        .then(data => {
            initializeDataTable(data.data);
        })
        .catch(error => console.error('Error:', error));
}


function initializeDataTable(registros) {
    let table = document.getElementById('registrosTable');
    if (!table) {
        table = document.createElement('table');
        table.id = 'registrosTable';
        table.className = 'display'; // Clase necesaria para DataTables
        document.getElementById('registrosContainer').appendChild(table);
    }

    $(table).DataTable({
        responsive: true,
        data: registros,
        columns: [
            { title: "RegistroId", data: "registroId", className: "column-id" },
            { title: "TiposRegistroId", data: "tiposRegistroId", className: "column-id" },
            { title: "MatriculaId", data: "matriculaId", className: "column-id" },
            { title: "AdministrativoId", data: "administrativoId", className: "column-id" },
            { title: "Fecha", data: "fecha", className: "column-name" },


            {
                title: "Acciones",
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                                <a href="/Registros/Detail/${data}" class="btn btn-primary"><i class="fa fa-eye"></i></a>
                                <a href="/Registros/Edit/${data}" class="btn btn-secondary"><i class="fa fa-edit"></i></a>
                                <a onclick="Delete('/Registros/Delete/${data}')" class="btn btn-danger"><i class="fa fa-trash"></i></a>
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
                        $('#registrosTable').DataTable().clear().destroy();
                        loadRegistros();
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

