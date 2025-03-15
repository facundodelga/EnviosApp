async function login() {
    const form = document.getElementById('loginForm');
    const formData = new FormData(form);
    const errorMessageElement = document.getElementById('errorMessage');

    // Convertir los datos del formulario a un objeto JSON
    const jsonData = {};
    formData.forEach((value, key) => {
        jsonData[key] = value;
    });

    // Enviar la solicitud usando fetch
    try {
        const response = await fetch('http://localhost:5056/api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(jsonData),
        });

        return await response.json();
    } catch (error) {
        console.error('Error al iniciar sesión:', error);
        errorMessageElement.style.display = 'block';
        errorMessageElement.textContent = 'Error al conectar con el servidor';
    }
}

