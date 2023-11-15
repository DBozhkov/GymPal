import React from "react";
import useLogout from "../../hooks/useLogout";
import { useNavigate } from "react-router-dom";
import "../../styles/Logout.css";

const Logout = () => {
    const { logout } = useLogout();
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
            await logout();
            navigate('/');
        } catch (error) {
            console.error("Logout error:", error);

        }
    };

    const handleCancel = () => {
        navigate("/");
    };

    return (
        <div className="logout-container">
            <h2>Are you sure you want to logout?</h2>
            <button className="logout" onClick={handleLogout}>
                Logout
            </button>
            <button className="cancel" onClick={handleCancel}>
                Cancel
            </button>
        </div>
    );
};

export default Logout;
