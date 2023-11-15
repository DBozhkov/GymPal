import axios from 'axios';
export const customAxios = axios.create({
    withCredentials: true,
});