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
    firstname: string,
    lastname: string,
    email: string,
    registerDate: string,
    isAdmin: boolean
}

export interface Listing {
    id: string,
    sellerId: string,
    carId: string,
    price: number,
    createdAt: string,
    updatedAt: string,
    status: "active" | "sold" | "removed"
}

export interface Reviews {
    id: string,
    sellerId: string,
    reviewerId: string,
    rating: 1 | 2 | 3 | 4 | 5,
    comment: string,
    createdAt: string
}

export interface CarImage {
    id: string,
    listingId: string,
    imageUrl: string
}

export interface Favorites {
    userId: string,
    listingId: string
}