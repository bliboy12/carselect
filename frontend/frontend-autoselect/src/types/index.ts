export interface CarInfo {
    id: string,
    brand: string,
    model: string,
    color: string,
    trim: string,
    buildYear: number,
    fuel: "petrol" | "diesel" | "electric" | "hybrid",
    transmission: "manual" | "automatic",
    kilometers: number,
    doors: number,
    drive: "fwd" | "rwd" | "awd" | "4wd"

}

export interface User {
    id: string
    firstName: string,
    lastName: string,
    email: string,
    registerDate: string,
    isAdmin: boolean
}

export interface UserRequest {
    firstName: string,
    lastName: string,
    email: string
}

export interface Seller {
    id: string,
    firstName: string,
    lastName: string
}

export interface ListingResponse {
    id: string,
    seller: Seller,
    car: CarInfo,
    price: number,
    createdAt: string,
    updatedAt: string,
    status: "active" | "sold" | "removed",
    carImages: CarImage[]
}

export interface ListingRequest {
    sellerId: string,
    car: CarRequest,
    price: number,
    status: "active" | "sold" | "removed",
}

export interface ListingUpdateRequest {
    sellerId: string,
    carId: string,
    price: number,
    status: "active" | "sold" | "removed",
}

export interface CarRequest {
    brand: string,
    model: string,
    color: string,
    trim: string,
    buildYear: number,
    fuel: "petrol" | "diesel" | "electric" | "hybrid",
    transmission: "manual" | "automatic",
    kilometers: number,
    doors: number,
    drive: "fwd" | "rwd" | "awd" | "4wd"
}

export interface CarUpdateRequest extends CarRequest {
    id: string
}

export interface Reviews {
    id: string,
    sellerId: string,
    reviewerId: string,
    rating: 1 | 2 | 3 | 4 | 5,
    comment: string,
    createdAt: string
}

export interface ImageRequest {
    file: File,
    isMainImage: boolean
}

export interface CarImage {
    id: string,
    listingId: string,
    imageUrl: string,
    isMainImage: boolean
}

export interface FavoriteResponse {
    userId: string,
    listing: ListingResponse
}

export interface FavoritesResponse {
    userId: string,
    listings: ListingResponse[]
}

export interface CarFilter {
    brand?: string,
    model?: string,
    color?: string,
    trim?: string,
    yearFrom?: string,
    yearTo?: string,
    fuel?: "petrol" | "diesel" | "electric" | "hybrid",
    transmission?: "manual" | "automatic",
    minKilometers?: number,
    maxKilometers?: number,
    doors?: number,
    drive?: "fwd" | "rwd" | "awd" | "4wd",
    page?: number,
    pageSize?: number
};

export interface PaginatedResponse<T> {
    items: T[]
    totalCount: number
    page: number
    pageSize: number
    totalPages: number
}