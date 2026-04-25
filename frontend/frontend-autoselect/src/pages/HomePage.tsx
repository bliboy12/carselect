import { axiosCars, axiosListings } from "../api/CarSelectApi";
import { useQuery } from "@tanstack/react-query";
import { type CarInfo, type Listing } from "../types";

interface listingInfoProps {
    listings: Listing[],
}


const HomePage = () => {

    const {data} = useQuery({
        queryKey: ["GetAllListings"],
        queryFn: async () => {
            return await axiosListings.get<Listing[]>("/")}
    })
    const result = data?.data[0];

    const carData = useQuery({
        queryKey: ["GetAllCars", result?.carId],
        queryFn: async () => {
            return await axiosCars.get<CarInfo>(`/${result?.carId}`);
        },
        enabled: !!result?.carId
    })

    const car = carData.data?.data;
    console.log(`ID: ${result?.id }, carId: ${result?.carId}, CreatedAt: ${result?.createdAt}`);
    console.log(`\nBrand: ${car?.brand}, Model: ${car?.model}, Trim: ${car?.trim}, Color: ${car?.color}, Buildyear: ${car?.buildYear}, Transmission: ${car?.transmission}, Drive: ${car?.drive}`);
    return (
        <div>
            <p>Test case</p>
        </div>
    );
}

export default HomePage;