import { useEffect } from "react";
import { useAuth } from "react-oidc-context";
import { useNavigate } from "react-router";

const CallbackPage = () => {
    const auth = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if (!auth.isLoading && !auth.error)
            navigate("/");
    }, [auth.isLoading, auth.error, navigate]);

    if (auth.isLoading) return <p className="text-white p-6">Logging in...</p>;
    if (auth.error) return <p className="text-red-400 p-6">Authentication error: {auth.error.message}</p>;

    return null;
}

export default CallbackPage;