import { useEffect } from 'react';
import axios from 'axios';
import useToken from './useToken';
import useAuth from './useAuth';
import { customAxios } from '../api/customAxios';

const useCustomAxios = () => {
    const token = useToken();
    const { auth, setAuth } = useAuth();

    useEffect(() => {
        const requestIntercept = customAxios.interceptors.request.use(
            (config) => {
                if (!config.headers['Authorization'] && auth?.jwtToken) {
                    config.headers['Authorization'] = `Bearer ${auth.jwtToken}`;
                }
                return config;
            },
            (error) => Promise.reject(error)
        );

        const responseIntercept = customAxios.interceptors.response.use(
            (response) => response,
            async (error) => {
                const prevRequest = error?.config;

                if (
                    (error?.response?.status === 403 || error?.response?.status === 401) &&
                    !prevRequest?.sent
                ) {
                    prevRequest.sent = true;
                    try {
                        const newAccessToken = await token();

                        setAuth((prev) => ({
                            ...prev,
                            jwtToken: newAccessToken,
                        }));

                        prevRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
                        return axios(prevRequest);
                    } catch (refreshError) {
                        console.error('Refresh Token Error:', refreshError);
                        return Promise.reject(error);
                    }
                }

                return Promise.reject(error);
            }
        );

        return () => {
            customAxios.interceptors.request.eject(requestIntercept);
            customAxios.interceptors.response.eject(responseIntercept);
        };
    }, [auth, token, setAuth]);

    return customAxios;
};

export default useCustomAxios;
