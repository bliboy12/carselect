import { Outlet } from "react-router";
// import Header from "../components/Header";
// import Footer from "../components/Footer";
import Navbar from "../components/NavBar";

const RootLayout = () => {
    return (
        <div className="bg-gray-950 min-h-screen">
            <Navbar/>
            <Outlet/>
        </div>
    )
}

export default RootLayout;