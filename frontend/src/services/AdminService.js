const apiUrl = 'http://localhost:5056/api/User'; // Cambia según tu configuración

// Obtén el token JWT del localStorage
const jwt = localStorage.getItem('jwt');

// Redirigir si no hay token
if (!jwt) {
    alert('No tienes acceso a esta página.');
    window.location.href = '/index.html';
}

// Verificar el rol del usuario
async function checkAccess() {
    try {
        const response = await fetch(apiUrl+"/1", {
            method: 'GET',
            headers: {
                "Authorization": `Bearer ${jwt}`,
            },
        });

        // Debugear la respuesta completa
        console.log('Status:', response.status);
        const responseData = await response.text();
        console.log('Response:', responseData);

        if (response.status === 403 || response.status === 401) {
            alert('Acceso denegado.');
            window.location.href = '/index.html';
        }
    } catch (error) {
        console.error('Error verificando acceso:', error);
    }
}

// Cargar usuarios
async function loadUsers() {
    try {
        const response = await fetch(apiUrl + "/all", {
            headers: {
                "Authorization": `Bearer ${jwt}`,
            },
        });

        const users = await response.json();
        const tableBody = document.querySelector('#usersTable tbody');
        tableBody.innerHTML = '';

        users.forEach(user => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${user.id}</td>
                <td>${user.name}</td>
                <td>${user.username}</td>
                <td>${user.role}</td>
                <td>
                    <button class="btn btn-danger btn-sm" onclick="deleteUser(${user.id})">Eliminar</button>
                    <button class="btn btn-primary btn-sm ms-2" onclick="editUser(${user.id})">Editar</button>
                </td>
            `;
            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error cargando usuarios:', error);
    }
}

// Agregar usuario
async function addUser() {
    const name = document.getElementById('fullname').value;
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const role = document.getElementById('role').value;

    try {
        await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${jwt}`,
            },
            body: JSON.stringify({ name, username, password, role }),
        });

        alert('Usuario agregado.');
        loadUsers();
    } catch (error) {
        console.error('Error agregando usuario:', error);
    }
}

// Eliminar usuario
async function deleteUser(id) {
    try {
        await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE',
            headers: {
                "Authorization": `Bearer ${jwt}`,
            },
        });

        alert('Usuario eliminado.');
        loadUsers();
    } catch (error) {
        console.error('Error eliminando usuario:', error);
    }
}

let editingId = null;

async function editUser(id) {
    const response = await fetch(apiUrl+`/${id}`, {
        method: 'GET',
        headers: {
            "Authorization": `Bearer ${jwt}`,
        },
    });
    const user = await response.json();
    // Mostrar los datos en el formulario
    document.getElementById('fullname').value = user.name;
    document.getElementById('username').value = user.userName;
    document.getElementById('password').value = user.password;
    document.getElementById('role').value = user.role;

    // Cambiar el texto del formulario y los botones
    document.getElementById('formTitle').textContent = 'Editar Usuario';
    document.getElementById('saveButton').textContent = 'Guardar';
    document.getElementById('cancelButton').classList.remove('d-none');

    // Guardar el ID del usuario que se está editando
    editingId = id;
}

function cancelEdit() {
    // Limpiar el formulario
    document.getElementById('addUserForm').reset();

    // Restaurar el estado original del formulario
    document.getElementById('formTitle').textContent = 'Agregar Usuario';
    document.getElementById('saveButton').textContent = 'Agregar Usuario';
    document.getElementById('cancelButton').classList.add('d-none');

    // Reiniciar el ID de edición
    editingId = null;
}

function saveUser() {
    const name = document.getElementById('fullname').value;
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const role = document.getElementById('role').value;

    if (editingId) {
        // Editar un usuario existente
        fetch(`/api/user/${editingId}`, {
            method: 'PUT',
            headers: { 
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${jwt}`
            },
            
            body: JSON.stringify({ name, username, password, role })
        })
        .then(response => {
            if (!response.ok) throw new Error('Error al editar el usuario');
            return response.json();
        })
        .then(data => {
            alert('Usuario editado correctamente');
            cancelEdit();
            // Recargar la tabla de usuarios
            loadUsers();
        })
        .catch(error => alert(error.message));
    } else {
        addUser();
    }
    loadUsers();
}
// Inicializar la tabla de usuarios al cargar la página
document.addEventListener('DOMContentLoaded', loadUsers);

// Cerrar sesión
function logout() {
    localStorage.removeItem('jwt');
    window.location.href = '/index.html';
}

// Inicializar
checkAccess();
loadUsers();
