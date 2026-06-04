import { useState } from "react";
import { useAuth } from "react-oidc-context";
import { Link, useLocation } from "react-router";
import { useFavorites } from "../context/FavoritesContext";

const Navbar = () => {
    // TODO: when the login is pressed, the page loads but doesn't stop
    // Need to find a way to stop the loading when an error occurs (infinite loading)

    // When the user presses on the login/register button
    const [isRedirecting, setIsRedirecting] = useState(false);
    const [isLogginOut, setIsLoggingOut] = useState(false);

    const location = useLocation();
    const auth = useAuth();
    // const navigate = useNavigate();

    const { favoritesCount } = useFavorites();

    const handleLogin = async () => {
        setIsRedirecting(true);
        try {
            await auth.signinRedirect();
        } catch (error) {
            setIsRedirecting(false);
            console.error("Login failed", error);
        }
    };

    const handleLogout = async () => {
        setIsLoggingOut(true);
        try {
            await auth.signoutRedirect();
        } catch (error) {
            setIsLoggingOut(false);
            console.error("Login failed", error);
        }
    }


    return (
        <>
            {(isRedirecting || isLogginOut) && (
                <div className="fixed inset-0 bg-gray-950/80 backdrop-blur-sm z-50 flex flex-col items-center justify-center gap-4">
                    <div className="w-8 h-8 border-2 border-blue-500 border-t-transparent rounded-full animate-spin" />
                    <p className="text-gray-400 text-sm">{isRedirecting ? "Redirecting to login..." : "Signing Out..."}</p>
                </div>
            )}
            <nav className="sticky top-0 z-50 bg-gray-950/90 backdrop-blur-sm border-b border-gray-800">

                <div className="max-w-7xl mx-auto px-6 h-16 flex items-center justify-between">

                    {/* Logo */}
                    <Link to="/" className="flex items-center gap-2">
                        <span className="text-blue-500 font-bold text-lg tracking-tight">● CarSelect</span>
                    </Link>

                    {/* Nav links */}
                    <div className="hidden md:flex items-center gap-6 text-sm text-gray-400">
                        <Link to="/" className={`hover:text-white transition-colors ${location.pathname === "/" ? "text-white" : ""}`}>
                            Listings
                        </Link>
                        <Link to="/favorites" className={`relative hover:text-white transition-colors ${location.pathname === "/favorites" ? "text-white" : ""}`}>
                            Favorieten
                            {auth.isAuthenticated && favoritesCount > 0 && (
                                <span className="absolute -top-2 -right-4 bg-blue-600 text-white text-xs rounded-full w-4 h-4 flex items-center justify-center">
                                    {favoritesCount}
                                </span>
                            )}
                        </Link>
                    </div>

                    {/* Auth buttons */}
                    <div className="flex items-center gap-3">
                        {auth.isAuthenticated ? (
                            <>
                                <span className="text-sm text-gray-400">
                                    {auth.user?.profile.given_name} {auth.user?.profile.family_name}
                                </span>
                                <Link to="/profile" className={`text-sm text-gray-400 hover:text-white transition-colors ${location.pathname === "/profile" ? "text-white" : ""}`}>
                                    Profile
                                </Link>
                                <button onClick={handleLogout} className="text-sm text-gray-400 hover:text-white transition-colors">
                                    Uitloggen
                                </button>
                            </>
                        ) : (
                            <>
                                <button onClick={handleLogin} className="cursor-pointer text-sm text-gray-400 hover:text-white transition-colors">
                                    Inloggen
                                </button>
                                <button onClick={() => window.location.href = "https://carselect-identityserver-evafhmh8eacxbbgd.westeurope-01.azurewebsites.net/Account/Register"} className="cursor-pointer text-sm bg-blue-600 hover:bg-blue-700 transition-colors text-white px-4 py-2 rounded-md font-medium">
                                    Registreren
                                </button>
                            </>
                        )
                        }
                    </div>
                </div>
            </nav>
        </>
    );
};

export default Navbar;