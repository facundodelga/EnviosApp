import { API_BASE_URL } from "../config";

const loginUser = async (username, password) => {
    try {
        const response = await fetch(`${API_BASE_URL}/auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ username, password }),
        });
        return response;
    } catch (error) {
        console.error('Error logging in:', error);

        return { status: 500, message: 'Internal Server Error' };
    }
};

export default loginUser;