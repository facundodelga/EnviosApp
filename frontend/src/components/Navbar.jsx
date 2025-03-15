import React from 'react';
import {jwtDecode} from 'jwt-decode';

const Navbar = () => {
    const token = localStorage.getItem('token'); // Assuming the token is stored in localStorage
    let role = '';

    if (token) {
        const decodedToken = jwtDecode(token);
        const role = decodedToken.role; // Assuming the token contains a 'role' field
    }

    return (
        <h1>Navbar</h1>
        
    );
};

export default Navbar;