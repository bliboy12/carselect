import axios from "axios";
// import useCurrentUser from "../hooks/useCurrentUser";

// const authentication = () => {
//     const { userId } = useCurrentUser;
// }

export const axiosApi = axios.create({
    baseURL: "https://carselect-api-cmbgbafrdqhebmgz.westeurope-01.azurewebsites.net/api/"
});

export const axiosCars = axios.create({
    baseURL: "https://carselect-api-cmbgbafrdqhebmgz.westeurope-01.azurewebsites.net/api/cars"
});

export const axiosListings = axios.create({
    baseURL: "https://carselect-api-cmbgbafrdqhebmgz.westeurope-01.azurewebsites.net/api/listings"
});

export const axiosUsers = axios.create({
    baseURL: "https://carselect-api-cmbgbafrdqhebmgz.westeurope-01.azurewebsites.net/api/users"
});
