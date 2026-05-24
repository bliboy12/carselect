import { useEffect, useRef, type PropsWithChildren } from "react";
import { useAuth } from "react-oidc-context";
import { Navigate } from "react-router";


interface ProtectedRouteProps extends PropsWithChildren{
    adminOnly?: boolean 
}

const ProtectedRoute = ({ children, adminOnly = false }: ProtectedRouteProps) => {
    const auth = useAuth();
    const hasRedirected = useRef(false);

    useEffect(() => {
        if (!auth.isLoading && !auth.isAuthenticated && !hasRedirected.current) {
            hasRedirected.current = true;
            auth.signinRedirect();
        }
    }, [auth.isLoading, auth.isAuthenticated, auth]);

    if (auth.isLoading) {
        return (
            <div className="min-h-screen bg-gray-950 flex items-center justify-center">
                <div className="w-6 h-6 border-2 border-blue-500 border-t-transparent rounded-full animate-spin" />
            </div>
        );
    }
    if (adminOnly && auth.user?.profile.role !== "Admin")
        return <Navigate to={"/"} replace />
    
    return <>{children}</>
}

export default ProtectedRoute;