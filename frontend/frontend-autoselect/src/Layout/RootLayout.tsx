import { Outlet } from "react-router";
// import Header from "../components/Header";
// import Footer from "../components/Footer";
import Navbar from "../components/NavBar";

const RootLayout = () => {
    return (
        <div>
            <Navbar/>
            <Outlet/>
        </div>
    )
}

export default RootLayout;