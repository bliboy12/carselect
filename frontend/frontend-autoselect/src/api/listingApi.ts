import type { Listing, ListingRequest } from "../types";
import { axiosListings } from "./axiosInstances";


export const getListingsBySellerId = async (sellerId: string): Promise<Listing[]> => {
    const response = await axiosListings.get<Listing[]>(`?sellerId=${sellerId}`);
    return response.data;
}

export const createListing = async (listing: ListingRequest): Promise<Listing> => {
    const response = await axiosListings.post<Listing>("/", listing);
    return response.data;
}

export const uploadListingImages = async (listingId: string, files: File[]): Promise<void> => {
    const formData = new FormData();
    files.forEach(file => formData.append("files", file));

    await axiosListings.post(`/${listingId}/images`, formData, {
        headers: { "Content-Type": "multipart/form-data" }
    });
}