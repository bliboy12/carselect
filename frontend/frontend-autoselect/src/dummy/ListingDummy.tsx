import { type Listing, type User, type CarImage } from "../types";

const dummyUser: User = {
    id: "u1",
    firstname: "John",
    lastname: "Doe",
    email: "john@example.com",
    registerDate: "2024-01-01",
    isAdmin: false
}

const dummyImage = (listingId: string): CarImage[] => [{
    id: `img-${listingId}`,
    listingId,
    imageUrl: "https://placehold.co/600x400",
    isMainImage: true
}]

export const dummyListings: Listing[] = [
    {
        id: "l1",
        seller: dummyUser,
        car: { id: "c1", brand: "Audi", model: "Q8", trim: "S-Line", fuel: "petrol", buildYear: 2020, color: "black", kilometers: 120000, transmission: "automatic", drive: "4wd", doors: 5 },
        price: 52000,
        createdAt: "2024-01-01",
        updatedAt: "2024-01-01",
        status: "active",
        carImages: dummyImage("l1")
    },
    {
        id: "l2",
        seller: dummyUser,
        car: { id: "c2", brand: "BMW", model: "M3", trim: "Competition", fuel: "petrol", buildYear: 2021, color: "white", kilometers: 45000, transmission: "automatic", drive: "rwd", doors: 4 },
        price: 78000,
        createdAt: "2024-01-02",
        updatedAt: "2024-01-02",
        status: "active",
        carImages: dummyImage("l2")
    },
    {
        id: "l3",
        seller: dummyUser,
        car: { id: "c3", brand: "Toyota", model: "Corolla", trim: "GR Sport", fuel: "hybrid", buildYear: 2022, color: "red", kilometers: 30000, transmission: "automatic", drive: "fwd", doors: 4 },
        price: 31900,
        createdAt: "2024-01-03",
        updatedAt: "2024-01-03",
        status: "sold",
        carImages: dummyImage("l3")
    },
    {
        id: "l4",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "l5",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "l6",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "l7",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "l8",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "l9",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
    {
        id: "20",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "21",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "22",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "23",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "24",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "25",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "26",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },
        {
        id: "27",
        seller: dummyUser,
        car: { id: "c4", brand: "Volkswagen", model: "Golf", trim: "GTI", fuel: "petrol", buildYear: 2019, color: "grey", kilometers: 89000, transmission: "manual", drive: "fwd", doors: 5 },
        price: 24500,
        createdAt: "2024-01-04",
        updatedAt: "2024-01-04",
        status: "active",
        carImages: dummyImage("l4")
    },

]