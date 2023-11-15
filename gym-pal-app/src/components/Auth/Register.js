import { useState } from "react";
import * as authService from '../../services/authService.js';
import { Link, useNavigate } from 'react-router-dom';
import '../../styles/Register.css';

const Register = () => {
    const navigate = useNavigate();

    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPass, setConfirmPass] = useState("");
    const [errMsg, setErrMsg] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        const user = {
            username: username,
            email: email,
            password: password,
            confirmPassword: confirmPass,
        };

        const isUsernameValid = authService.validateUsername(user.username);
        const isPassValid = authService.validatePassword(user.password);
        const isMatch = authService.passwordsMatch(user.password, user.confirmPassword);

        if (!isUsernameValid || !isPassValid || !isMatch) {
            setErrMsg("Invalid input.");
            return;
        }

        try {
            const response = await authService.register(user);
            refreshState();
            navigate("/login");
        } catch (err) {
            handleError(err);
        }
    };

    function handleError(err) {
        if (!err?.response) {
            setErrMsg("No Server Response");
        } else if (err.response.status === 403) {
            setErrMsg("Email already taken!");
        } else {
            setErrMsg("Registration Failed!");
        }
    }

    function refreshState() {
        setErrMsg("");
        setUsername("");
        setEmail("");
        setPassword("");
        setConfirmPass("");
    }

    return (
        <div>
            <h2>Register</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="userName">Username:</label>
                    <input
                        type="username"
                        name="userName"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        name="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="confirmPassword">Confirm Password:</label>
                    <input
                        type="password"
                        name="confirmPassword"
                        placeholder="Confirm Password"
                        value={confirmPass}
                        onChange={(e) => setConfirmPass(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        name="email"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Register</button>
            </form>

            {errMsg && <div>{errMsg}</div>}
            <Link to="/login">Login</Link>
        </div>
    );
};

export default Register;
