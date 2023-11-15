import axios from 'axios';

const baseUrl = 'https://localhost:44323';

export async function register(user) {
  try {
      const response = await axios.post(`${baseUrl}/api/auth/Register`, user, {
          headers: { "Content-Type": "application/json" },
          withCredentials: true
      });
      return response;
  } catch (error) {
      throw error;
  }
}

export async function login(user){
  try {
    const response = await axios.post(`${baseUrl}/api/auth/Login`, user, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true
    });
    return response;
    } catch (error) {
      console.error('Login Error:', error.response.data);
      throw error;
    }
}

export async function logout(customAxios, refreshToken) {
  try {
    const response = await customAxios.post(`${baseUrl}/api/auth/Logout`, { refreshToken }, {
      withCredentials: true,
    });

    return response;
  } catch (error) {
    throw error;
  }
}

export function validateUsername(username) {
  return username ? true : false;
}

export function validatePassword(password) {
  return password && password.length >= 5 && password.length <= 25;
}

export function passwordsMatch(password, confirmPassword) {
  return password && confirmPassword && password.trim() === confirmPassword.trim();
}