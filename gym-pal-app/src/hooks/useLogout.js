import * as authService from "../services/authService";
import useAuth from "./useAuth";
import useCustomAxios from "./useCustomAxios"; 

const useLogout = () => {
  const { setAuth, auth } = useAuth();
  const customAxios = useCustomAxios(); 

  const logout = async () => {
    try {
      await authService.logout(customAxios, auth.refreshToken); // Pass the refresh token
      setAuth({});
    } catch (error) {
      console.error("Logout error:", error);
      throw error;
    }
  };

  return { logout };
};

export default useLogout;
