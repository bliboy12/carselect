import { createContext, useContext, useState, useEffect, type PropsWithChildren } from "react";
import { axiosUsers } from "../api/axiosInstances";
import useCurrentUser from "../hooks/useCurrentUser";

interface FavoritesContextType {
    favoritesCount: number
    incrementCount: () => void
    decrementCount: () => void
    refreshCount: () => void
}

const FavoritesContext = createContext<FavoritesContextType | null>(null);

export const FavoritesProvider = ({ children }: PropsWithChildren) => {
    const [favoritesCount, setFavoritesCount] = useState(0);
    const { userId, isAuthenticated } = useCurrentUser();

    const refreshCount = async () => {
        if (!isAuthenticated || !userId) return;
        try {
            const response = await axiosUsers.get(`/${userId}/favorites`);
            setFavoritesCount(response.data.listings?.length ?? 0);  // ← listings not favorites
        } catch (error) {
            console.error("favorites count error:", error);
            setFavoritesCount(0);
        }
    };
    useEffect(() => {
        if (!isAuthenticated || !userId) {
            setFavoritesCount(0); // reset when logged out
            return;
        }
        refreshCount();
    }, [isAuthenticated, userId]);

    const incrementCount = () => setFavoritesCount(prev => prev + 1);
    const decrementCount = () => setFavoritesCount(prev => Math.max(0, prev - 1));

    return (
        <FavoritesContext.Provider value={{ favoritesCount, incrementCount, decrementCount, refreshCount }}>
            {children}
        </FavoritesContext.Provider>
    );
};

export const useFavorites = () => {
    const context = useContext(FavoritesContext);
    if (!context)
        throw new Error("useFavorites must be used within a FavoritesProvider");
    return context;
};