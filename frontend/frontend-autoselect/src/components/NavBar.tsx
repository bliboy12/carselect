import { useAuth } from "react-oidc-context";
import { Link, useLocation, useNavigate } from "react-router";

const Navbar = () => {
    const location = useLocation();
    const auth = useAuth();
    const navigate = useNavigate();

    const handleLogin = () => {
        auth.signinRedirect();
    }

    const handleLogout = () => {
        auth.signoutRedirect();
    }


    return (
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
                    <Link to="/favorites" className={`hover:text-white transition-colors ${location.pathname === "/favorites" ? "text-white" : ""}`}>
                        Favorieten
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
                            <button onClick={handleLogin} className="text-sm text-gray-400 hover:text-white transition-colors">
                                Inloggen
                            </button>
                            <button onClick={() => navigate("/register")} className="text-sm bg-blue-600 hover:bg-blue-700 transition-colors text-white px-4 py-2 rounded-md font-medium">
                                Registreren
                            </button>
                        </>
                    )}
                </div>

            </div>
        </nav>
    );
};

export default Navbar;