import React from 'react';
import loginUser from '../services/LoginService';
import { useState } from 'react';


const LoginContainer = ({setToken}) => {
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [error, setError] = useState(false);

    const submitForm = async (event) => {
        event.preventDefault();
        if(name === '' || password === '') {
            setError(true);
            setErrorMessage('Username and password are required');
            return;
        }
        const response = await loginUser(name, password);
        
        if(response.status === 200) {
            const payload = await response.json();
            // Guardar el token en el local storage
            console.log(payload.token);
            setToken(payload.token);
            
        }
        else {
            setError(true);
            setErrorMessage(response.message);
        }

            
    };

    return (
        <section className='login-container'>

            <h2>Iniciar Sesión</h2>
            
            <form className="form-group" onSubmit={submitForm}>
                {error && <p style={{ color: 'red'  }}>{errorMessage}</p>}
                <input placeholder="Username" type="text"
                    value={name}
                    onChange={(event) => setName(event.target.value)}
                />
                <input placeholder="Password" type="password"
                    value={password}
                    onChange={(event) => setPassword(event.target.value)}
                />
                <button type="submit">Iniciar Sesión</button>
                
            </form>
            
        </section>

    );
};

export default LoginContainer;
