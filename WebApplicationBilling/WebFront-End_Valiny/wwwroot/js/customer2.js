document.addEventListener('DOMContentLoaded', function () {
    loadCustomers();
});

function loadCustomers() {
    fetch('/Customers/GetAllCustomers') // Asegúrate de reemplazar con la ruta correcta
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
                    return '<a href="/Customers/Detail/' + row.id + '"><i class="fa fa-eye"></i></a> ' +
                        '<a href="/Customers/Edit/' + row.id + '"><i class="fa fa-edit"></i></a> ' +
                        '<a href="/Customers/Delete/' + row.id + '"><i class="fa fa-trash"></i></a>';
                }
            }
        ]
    });

}


