import { Route, Routes } from "react-router-dom";
import './App.css';
import Home from './components/Home/Home.js'
import About from './components/About'
import Header from './components/Header'
import GainWeightSupplements from "./components/Supplements/GainWeightSupplements/GainWeightSupplements";
import LoseWeightSupplements from "./components/Supplements/LoseWeightSupplements/LoseWeightSupplements";
import KeepFormSupplements from "./components/Supplements/KeepFormSupplements/KeepFormSupplements";
import BMICalculator from "./components/Calculator/BMICalculator"
import CreateSupplement from "./components/Supplements/CreateSupplement";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import Logout from "./components/Auth/Logout";
import CheckAuth from "./components/Auth/CheckAuth.js";
import Contacts from "../src/components/Contacts.js"

function App() {
  return (
    <div className="App">
        <Header style={{ className: 'App-header' }} />
        <CheckAuth />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/gainweight" element={<GainWeightSupplements />} />
          <Route path="/loseweight" element={<LoseWeightSupplements />} />
          <Route path="/stayfit" element={<KeepFormSupplements />} />
          <Route path="/calculator" element={<BMICalculator />} />
          <Route path="/create-supplement" element={<CreateSupplement />} />
          <Route path="/contacts" element={<Contacts />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/logout" element={<Logout />} />
        </Routes>

    </div>
  );
}

export default App;
