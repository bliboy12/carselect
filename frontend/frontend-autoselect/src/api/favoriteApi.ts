import type { FavoriteResponse, FavoritesResponse } from "../types"
import { axiosUsers } from "./axiosInstances"


export const createFavorite = async (userId: string, listingId: string): Promise<FavoriteResponse> => {
    const response = await axiosUsers.post<FavoriteResponse>(`/${userId}/favorites`, listingId);
    return response.data;
}

export const getAllFavoritesByUserId = async (userId: string): Promise<FavoritesResponse> => {
    const response = await axiosUsers.get<FavoritesResponse>(`/${userId}/favorites`);
    return response.data;
}

export const deleteFavorite = async (userId: string, listingId: string): Promise<void> => {
    await axiosUsers.delete(`/${userId}/favorites/${listingId}`);
}

export const isFavorited = async (userId: string, listingId: string): Promise<boolean> => {
    const response = await axiosUsers.get<boolean>(`/${userId}/favorites/${listingId}`);
    return response.data;
}