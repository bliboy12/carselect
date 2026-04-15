import axios from "axios";


const CarMakes = axios.create({
    baseURL: "https://api.api-ninjas.com/v1/",
    headers: {
        "X-Api-Key":import.meta.env.VITE_NINJA_CAR_API
    }
})

export default CarMakes;