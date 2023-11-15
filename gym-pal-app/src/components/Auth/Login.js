import { React, useRef, useEffect, useState } from "react";
import * as authService from '../../services/authService.js';
import useAuth from "../../hooks/useAuth";
import { Link, useNavigate, useLocation } from 'react-router-dom';
import '../../styles/Login.css';

const Login = () => {
  const { setAuth } = useAuth();
  const [errMsg, setErrMsg] = useState("");

  const usernameRef = useRef();
  const passwordRef = useRef();

  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from || "/";

  useEffect(() => {
    setErrMsg("");
  }, []);

  const handleLogin = async (e) => {
    e.preventDefault();

    const user = {
      username: usernameRef.current.value,
      password: passwordRef.current.value,
    };

    console.log('Login Data:', user);

    try {
      const response = await authService.login(user);

      const userId = response?.data?.id;
      const jwtToken = response?.data?.jwtToken;
      const roles = response?.data?.roles;
      const email = response?.data?.email;
      const username = response?.data?.username;
      const refreshToken = response?.data?.refreshToken;

      setAuth({
        userId,
        jwtToken,
        refreshToken,
        roles,
        email,
        isLogged: true,
        username,
      });

      console.log(response.data);

      navigate(from, { replace: true });

    } catch (err) {
      handleError(err);
    }
  };

  function handleError(err) {
    console.error('Error:', err);
    setErrMsg('Login failed!');
  }

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleLogin}>
        <div className="form-group">
          <label htmlFor="userName">Username:</label>
          <input
            type="username"
            name="userName"
            placeholder="Username"
            ref={usernameRef}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            name="password"
            placeholder="Password"
            ref={passwordRef}
            required
          />
        </div>
        <button type="submit" className="submit-button">Login</button>
      </form>

      {errMsg && <div>{errMsg}</div>}

      <Link to="/register">Register</Link>
    </div>
  );
};

export default Login;
