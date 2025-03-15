import React from 'react';
import FormInput from './LoginFormInput';
import SubmitButton from './LoginSubmitButton';
import login from '../services/LoginService';

const LoginContainer = () => {
    const submitForm = (event) => {
        event.preventDefault();
        const username = event.target.username.value;
        const password = event.target.password.value;

        login({ username, password })
            .then((response) => {
                if (response.success) {
                    console.log('Login successful:', response.data);
                    // Handle successful login (e.g., redirect or store token)
                } else {
                    const errorMessage = document.getElementById('errorMessage');
                    errorMessage.style.display = 'block';
                    errorMessage.textContent = response.message || 'Error al iniciar sesión';
                }
            })
            .catch((error) => {
                console.error('Login error:', error);
                const errorMessage = document.getElementById('errorMessage');
                errorMessage.style.display = 'block';
                errorMessage.textContent = 'Error al conectar con el servidor';
            });
    };

    return (
        <div className="login-container">
            <h2>Iniciar Sesión</h2>
            <form id="loginForm" onSubmit={submitForm}>
                <FormInput label="Usuario" type="text" id="username" name="username" required />
                <FormInput label="Contraseña" type="password" id="password" name="password" required />
                <div id="errorMessage" style={{ color: 'red', display: 'none' }}></div>
                <SubmitButton text="Ingresar" />
            </form>
        </div>
    );
};

export default LoginContainer;
