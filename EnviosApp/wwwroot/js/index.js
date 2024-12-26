async function submitForm() {
    // Capturar el formulario y sus campos
    const form = document.getElementById('loginForm');
    const formData = new FormData(form);

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

        // Manejar la respuesta del servidor
        if (response.ok) {
            const result = await response.json();
            console.log('Inicio de sesión exitoso:', result);
            alert(result.message);
        } else {
            const error = await response.json();
            console.error('Error en el inicio de sesión:', error);
            alert(error.message || 'Error en el inicio de sesión');
        }
    } catch (error) {
        console.error('Error al enviar los datos:', error);
        alert('Ocurrió un error al enviar los datos.');
    }
}
