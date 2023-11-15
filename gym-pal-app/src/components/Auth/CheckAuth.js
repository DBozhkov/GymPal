import { useEffect, useRef } from "react";
import useToken from "../../hooks/useToken";
import useAuth from "../../hooks/useAuth";

const CheckAuth = () => {
  const refresh = useToken();
  const { auth } = useAuth();
  const isAuth = useRef(false);

  useEffect(() => {
    const verifyRefreshToken = async () => {
      try {
        await refresh();
      } catch (error) {
        console.error(error);
      }
    };

    if (!isAuth.current) {
      !auth?.jwtToken && verifyRefreshToken();
      isAuth.current = true;
    }
  }, [auth?.jwtToken, refresh]);

  return (
    <>
    </>
  );
};

export default CheckAuth;