import logo from "./logo.svg";
import "./App.css";
import { Routes, Route } from "react-router-dom";

import MainPage from "./pages/MainPage";
import Admin from "./pages/Admin";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="yonet" element={<Admin />} />
      </Routes>
    </div>
  );
}

export default App;
