import type { Listing } from "../types";
import { axiosListings } from "./axiosInstances";


export const getListingsBySellerId = async (sellerId: string): Promise<Listing[]> => {
    const response = await axiosListings.get<Listing[]>(`?sellerId=${sellerId}`);
    return response.data;
}