import axios from "axios";


export const axiosCars = axios.create({
    baseURL: "http://localhost:5028/api/cars"
});

export const axiosListings = axios.create({
    baseURL: "http://localhost:5028/api/listings"
});

export const axiosUsers = axios.create({
    baseURL: "http://localhost:5028/api/users"
});
