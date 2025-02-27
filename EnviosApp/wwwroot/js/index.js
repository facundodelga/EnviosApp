async function submitForm() {
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

        console.log('Response received:', response);
        
        
        // Manejar la respuesta del servidor
        if (response.ok) {
            const result = await response.json();
            console.log('Inicio de sesión exitoso:', result);
            localStorage.setItem('jwt', result.token);
            window.location.href = '/home.html';
        } else {
            
            console.error('Error en el inicio de sesión:', response);

            // Mostrar el mensaje de error en la página
            errorMessageElement.textContent = 'Wrong username or password';
            errorMessageElement.style.display = 'block';
        }
    } catch (error) {
        console.error('Error en el inicio de sesión:', error);

        // Mostrar un mensaje de error genérico en la página
        errorMessageElement.textContent = 'Ocurrió un error al enviar los datos.';
        errorMessageElement.style.display = 'block';
    }
}

