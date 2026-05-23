import { useAuth } from "react-oidc-context"


const useCurrentUser = () => {
    const auth = useAuth();

    return {
        userId: auth.user?.profile.sub ?? "",
        firstName: auth.user?.profile.given_name ?? "",
        lastName: auth.user?.profile.family_name ?? "",
        isAuthenticated: auth.isAuthenticated,
        isLoading: auth.isLoading,
        token: auth.user?.access_token ?? ""
    }
}

export default useCurrentUser;