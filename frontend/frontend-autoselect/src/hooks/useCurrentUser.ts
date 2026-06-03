import { useContext } from "react";
import { UserContext } from "../context/UserContext";

const useCurrentUser = () => {
    const context = useContext(UserContext);
    if (!context)
        throw new Error("useCurrentUser must be used within a UserProvider");
    return context;
};

export default useCurrentUser;