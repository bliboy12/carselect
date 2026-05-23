import { Outlet } from "react-router";
// import Header from "../components/Header";
// import Footer from "../components/Footer";
import Navbar from "../components/NavBar";
import useAxiosAuth from "../hooks/useAxiosAuth";

const RootLayout = () => {
    // adds bearer to every request going out, to be authenticated
    useAxiosAuth();

    return (
        <div className="bg-gray-950 min-h-screen">
            <Navbar/>
            <Outlet/>
        </div>
    )
}

export default RootLayout;