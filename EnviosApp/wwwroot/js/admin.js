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
                <td>${user.username}</td>
                <td>${user.role}</td>
                <td>
                    <button onclick="deleteUser(${user.id})">Eliminar</button>
                    <button onclick="editUser(${user.id}, '${user.username}', '${user.role}')">Editar</button>
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
    const username = document.getElementById('username').value;
    const role = document.getElementById('role').value;

    try {
        await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${jwt}`,
            },
            body: JSON.stringify({ username, role }),
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

// Editar usuario (ventana simple)
function editUser(id, currentUsername, currentRole) {
    const username = prompt('Nuevo nombre de usuario:', currentUsername);
    const role = prompt('Nuevo rol (admin/user):', currentRole);

    if (username && role) {
        updateUser(id, username, role);
    }
}

// Actualizar usuario
async function updateUser(id, username, role) {
    try {
        await fetch(`${apiUrl}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${jwt}`,
            },
            body: JSON.stringify({ username, role }),
        });

        alert('Usuario actualizado.');
        loadUsers();
    } catch (error) {
        console.error('Error actualizando usuario:', error);
    }
}

// Cerrar sesión
function logout() {
    localStorage.removeItem('jwt');
    window.location.href = '/index.html';
}

// Inicializar
checkAccess();
loadUsers();
