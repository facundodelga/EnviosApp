const apiUrl = 'http://localhost:5056/api/Client';
const jwt = localStorage.getItem('jwt');

// Verificar autenticación
if (!jwt) {
    window.location.href = '/index.html';
}

// Función para decodificar el token JWT
function parseJwt(token) {
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        return JSON.parse(window.atob(base64));
    } catch (e) {
        return null;
    }
}

// Variables globales
let clientModal;
let editingId = null;

// Inicialización
document.addEventListener('DOMContentLoaded', function() {
    
    clientModal = new bootstrap.Modal(document.getElementById('clientModal'));
    // Mostrar/ocultar enlace de administración según el rol
    const adminLink = document.getElementById('adminLink');

    const decodedToken = parseJwt(jwt);
    if (decodedToken.role === 'admin') {
        adminLink.style.display = 'block';
    }
    loadClients();
});

// Cargar clientes
async function loadClients() {
    try {
        const response = await fetch(apiUrl, {
            headers: {
                "Authorization": `Bearer ${jwt}`
            }
        });

        if (!response.ok) throw new Error('Error al cargar clientes');

        const clients = await response.json();
        displayClients(clients);
    } catch (error) {
        console.error('Error:', error);
        alert('Error al cargar los clientes');
    }
}

// Mostrar clientes en la tabla
function displayClients(clients) {
    const tbody = document.querySelector('#clientsTable tbody');
    tbody.innerHTML = '';

    clients.forEach(client => {
        const row = document.createElement('tr');
        
        row.innerHTML = `
            <td>${client.name}</td>
            <td>${client.organization}</td>
            <td>${client.email}</td>
            <td>${client.telephone}</td>
            <td>${client.city}</td>
            <td>${client.country}</td>
            <td>
                <button class="btn btn-sm btn-primary me-2" onclick="editClient(${client.id})">
                    <i class="bi bi-pencil"></i>
                </button>
                <button class="btn btn-sm btn-danger" onclick="deleteClient(${client.id})">
                    <i class="bi bi-trash"></i>
                </button>
            </td>
        `
        // Asignar evento de clic a la fila
        row.addEventListener('click', () => {
            viewClient(client.id);
        });

        tbody.appendChild(row);
    });
}

// Buscar clientes
async function searchClients() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    try {
        const response = await fetch(`${apiUrl}/search?term=${searchTerm}`, {
            headers: {
                "Authorization": `Bearer ${jwt}`
            }
        });

        if (!response.ok) throw new Error('Error en la búsqueda');

        const clients = await response.json();
        displayClients(clients);
    } catch (error) {
        console.error('Error:', error);
        alert('Error al buscar clientes');
    }
}

// Mostrar formulario de nuevo cliente
function showAddClientForm() {
    editingId = null;
    document.getElementById('modalTitle').textContent = 'Nuevo Cliente';
    document.getElementById('clientForm').reset();
    clientModal.show();
}

// Editar cliente
async function editClient(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            headers: {
                "Authorization": `Bearer ${jwt}`
            }
        });

        if (!response.ok) throw new Error('Error al cargar el cliente');

        const client = await response.json();
        editingId = id;
        
        // Llenar el formulario con todos los campos
        document.getElementById('name').value = client.name;
        document.getElementById('organization').value = client.organization;
        document.getElementById('email').value = client.email;
        document.getElementById('telephone').value = client.telephone;
        document.getElementById('address').value = client.address;
        document.getElementById('city').value = client.city;
        document.getElementById('zipcode').value = client.zipCode;
        document.getElementById('country').value = client.country;
        
        document.getElementById('modalTitle').textContent = 'Editar Cliente';
        clientModal.show();
    } catch (error) {
        console.error('Error:', error);
        alert('Error al cargar el cliente para editar');
    }
}

// Guardar cliente (crear o actualizar)
async function saveClient() {
    const clientData = {
        Name: document.getElementById('name').value,
        Organization: document.getElementById('organization').value,
        Email: document.getElementById('email').value,
        Telephone: document.getElementById('telephone').value,
        Address: document.getElementById('address').value,
        City: document.getElementById('city').value,
        ZipCode: document.getElementById('zipcode').value,
        Country: document.getElementById('country').value
    };

    const url = editingId ? `${apiUrl}/${editingId}` : apiUrl;
    const method = editingId ? 'PUT' : 'POST';

    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${jwt}`
            },
            body: JSON.stringify(editingId ? { ...clientData, id: editingId } : clientData)
        });

        if (!response.ok) throw new Error('Error al guardar el cliente');

        clientModal.hide();
        loadClients();
        alert(editingId ? 'Cliente actualizado con éxito' : 'Cliente creado con éxito');
    } catch (error) {
        console.error('Error:', error);
        alert('Error al guardar el cliente');
    }
}

// Eliminar cliente
async function deleteClient(id) {
    if (!confirm('¿Está seguro de que desea eliminar este cliente?')) return;

    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE',
            headers: {
                "Authorization": `Bearer ${jwt}`
            }
        });

        if (!response.ok) throw new Error('Error al eliminar el cliente');

        loadClients();
        alert('Cliente eliminado con éxito');
    } catch (error) {
        console.error('Error:', error);
        alert('Error al eliminar el cliente');
    }
}
async function viewClient(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            headers: {
                method: 'GET',
                "Authorization": `Bearer ${jwt}`
            }
        });

        if (!response.ok) throw new Error('Error al cargar el cliente');

        const client = await response.json();

        // Rellenar los campos del modal con los datos del cliente
        document.getElementById('name').value = client.name;
        document.getElementById('organization').value = client.organization;
        document.getElementById('email').value = client.email;
        document.getElementById('telephone').value = client.telephone;
        document.getElementById('address').value = client.address;
        document.getElementById('city').value = client.city;
        document.getElementById('zipcode').value = client.zipCode;
        document.getElementById('country').value = client.country;

        // Configurar el modal en modo solo lectura
        document.getElementById('modalTitle').textContent = 'Detalles del Cliente';
        Array.from(document.querySelectorAll('#clientForm input')).forEach(input => input.setAttribute('readonly', true));
        Array.from(document.querySelectorAll('#clientForm select')).forEach(select => select.setAttribute('disabled', true));

        // Mostrar el botón de redirección a edición
        const editButton = document.getElementById('editClientButton');
        editButton.style.display = 'inline';
        editButton.onclick = () => {
            window.location.href = `/edit-client.html?id=${id}`;
        };

        clientModal.show();
    } catch (error) {
        console.error('Error:', error);
        alert('Error al cargar el cliente');
    }
}

// Restaurar el estado del modal al cerrarlo
document.getElementById('clientModal').addEventListener('hidden.bs.modal', () => {
    // Limpiar el formulario
    document.getElementById('clientForm').reset();

    // Restaurar los campos a modo editable
    Array.from(document.querySelectorAll('#clientForm input')).forEach(input => input.removeAttribute('readonly'));
    Array.from(document.querySelectorAll('#clientForm select')).forEach(select => select.removeAttribute('disabled'));

    // Restaurar título y botones
    document.getElementById('modalTitle').textContent = 'Nuevo Cliente';
    document.getElementById('editClientButton').style.display = 'none';
});


// Cerrar sesión
function logout() {
    localStorage.removeItem('jwt');
    window.location.href = '/index.html';
}