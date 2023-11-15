import '../styles/Header.css';
import {Link} from 'react-router-dom';
import useAuth from '../hooks/useAuth';

function Header() {
  const { auth } = useAuth();

  return (
    <header>
      <img id="logo" src="\dumbellLogo.jpg" alt="Logo" />
      <nav>
        <ul>
          <li><Link to="/">Home</Link></li>
          {!auth.isLogged && (
            <>
              <li><Link to="/login">Login</Link></li>
              <li><Link to="/register">Register</Link></li>
            </>
          )}
          {auth.isLogged && (
            <>
              <li><Link to="/about">About</Link></li>
              <li><Link to="/calculator">Calculator</Link></li>
              <li><Link to="/contacts">Contacts</Link></li>
              <li><Link to="/logout">Logout</Link></li>
            </>
          )}
        </ul>
      </nav>
    </header>
  );
}

export default Header;
