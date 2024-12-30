// Verificar si hay un token JWT almacenado
const jwt = localStorage.getItem('jwt');

// Redirigir si no hay token
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

// Configurar la página según el rol del usuario
document.addEventListener('DOMContentLoaded', function() {
    const decodedToken = parseJwt(jwt);
    
    if (decodedToken) {
        // Mostrar información del usuario
        const userInfo = document.getElementById('userInfo');
        userInfo.textContent = `Bienvenido, ${decodedToken.username}`;

        // Actualizar mensaje de bienvenida
        const welcomeMessage = document.getElementById('welcomeMessage');
        welcomeMessage.textContent = `¡Bienvenido, ${decodedToken.username}!`;

        // Mostrar/ocultar enlace de administración según el rol
        const adminLink = document.getElementById('adminLink');
        if (decodedToken.role === 'admin') {
            adminLink.style.display = 'block';
        }
    }
});

// Función de cierre de sesión
function logout() {
    localStorage.removeItem('jwt');
    window.location.href = '/index.html';
}

// Función para mostrar mensaje de "Próximamente"
function showComingSoon(feature) {
    alert(`La funcionalidad de ${feature} estará disponible próximamente.`);
}