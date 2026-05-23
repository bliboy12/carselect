import { useEffect } from "react";
import { useAuth } from "react-oidc-context";
import { axiosListings, axiosUsers, axiosCars } from "../api/axiosInstances";

// Adds the Bearer for each axios request that is going out, it attaches to it before sending out
const useAxiosAuth = () => {
    const auth = useAuth();

    useEffect(() => {
        const token = auth.user?.access_token;

        // Add interceptor to every axios instance
        const instances = [axiosListings, axiosUsers, axiosCars];
        
        const interceptors = instances.map(instance =>
            instance.interceptors.request.use(config => {
                if (token)
                    config.headers.Authorization = `Bearer ${token}`;
                return config;
            })
        );

        // Cleanup — remove interceptors when token changes
        return () => {
            instances.forEach((instance, i) =>
                instance.interceptors.request.eject(interceptors[i])
            );
        };
    }, [auth.user?.access_token]);
};

export default useAxiosAuth;