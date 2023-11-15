// useToken.js
import axios from "axios";
import useAuth from "./useAuth";

const baseUrl = 'https://localhost:44323';

const useToken = () => {
    const { setAuth } = useAuth();

    const refresh = async () => {
        try {
            const response = await axios.get(`${baseUrl}/api/auth/RefreshToken`, {
                withCredentials: true,
            });

            const userData = response?.data;

            setAuth((prev) => ({
                ...prev,
                roles: userData?.roles,
                jwtToken: userData?.jwtToken,
                refreshToken: userData?.refreshToken, // Set the refresh token here
                userId: userData?.id,
                username: userData?.username,
                email: userData?.email,
                isLogged: response ? true : false,
            }));

            return userData?.refreshToken; // Return the refresh token
        } catch (error) {
            console.error('Refresh Token Error:', error);
            throw error;
        }
    };

    return refresh;
};

export default useToken;
