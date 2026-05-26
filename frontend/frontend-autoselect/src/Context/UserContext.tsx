import { createContext, type PropsWithChildren } from "react";
import { useAuth } from "react-oidc-context";

interface UserContextType {
    userId: string
    firstName: string
    lastName: string
    isAdmin: boolean
    token: string
    isAuthenticated: boolean
    isLoading: boolean
}

export const UserContext = createContext<UserContextType | null>(null);

const UserProvider = ({ children }: PropsWithChildren) => {
    const auth = useAuth();

    const value: UserContextType = {
        userId: auth.user?.profile.sub ?? "",
        firstName: auth.user?.profile.given_name ?? "",
        lastName: auth.user?.profile.family_name ?? "",
        isAdmin: auth.user?.profile.role === "Admin",
        token: auth.user?.access_token ?? "",
        isAuthenticated: auth.isAuthenticated,
        isLoading: auth.isLoading
    };

    return (
        <UserContext.Provider value={value}>
            {children}
        </UserContext.Provider>
    );
};

export default UserProvider;