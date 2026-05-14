import type { ImageRequest, ListingResponse, ListingRequest, ListingUpdateRequest } from "../types";
import { axiosListings } from "./axiosInstances";


export const getListingsBySellerId = async (sellerId: string): Promise<ListingResponse[]> => {
    const response = await axiosListings.get<ListingResponse[]>(`?sellerId=${sellerId}`);
    return response.data;
}

export const getListingById = async (listingId: string): Promise<ListingResponse> => {
    const response = await axiosListings.get<ListingResponse>(`/${listingId}`);
    return response.data;
}

export const createListing = async (listing: ListingRequest): Promise<ListingResponse> => {
    const response = await axiosListings.post<ListingResponse>("/", listing);
    return response.data;
}

export const uploadListingImages = async (listingId: string, images: ImageRequest[]): Promise<void> => {
    const formData = new FormData();
    images.forEach((img, i) => {
        formData.append(`files[${i}].File`, img.file);
        formData.append(`files[${i}].IsMainImage`, String(img.isMainImage))
    })

    await axiosListings.post(`/${listingId}/images`, formData, {
        headers: { "Content-Type": "multipart/form-data" }
    });
}

export const updateListing = async (id: string, listing: ListingUpdateRequest): Promise<ListingResponse> => {
    const response = await axiosListings.put<ListingResponse>(`${id}`, listing);
    return response.data;
}

export const DeleteImageById = async (listingId: string, imageId: string): Promise<void> => {
    await axiosListings.delete<void>(`${listingId}/images/${imageId}`);
}

export const DeleteListingById = async (listingId: string): Promise<void> => {
    await axiosListings.delete<void>(`${listingId}`);
}