import { axiosCars, axiosListings } from "../api/CarSelectApi";
import { useQuery } from "@tanstack/react-query";
import { type CarInfo, type Listing } from "../types";
import ListingCard from "../components/Listings/ListingCard";


const HomePage = () => {

    const {data, isLoading, isError, error} = useQuery({
        queryKey: ["GetAllListings"],
        queryFn: async () => {
            return await axiosListings.get<Listing[]>("/")}
    })

    if (isLoading)
        return <p>Is Loading...</p>
    if (isError)
    {
        console.log(error.message);
        return <p>Something went wrong</p>
    }
    const result = data?.data[0];

    const car = result?.car;
    
    console.log(`ID: ${result?.id }, carId: ${result?.id}, CreatedAt: ${result?.createdAt}`);
    console.log(`\nBrand: ${car?.brand}, Model: ${car?.model}, Trim: ${car?.trim}, Color: ${car?.color}, Buildyear: ${car?.buildYear}, Transmission: ${car?.transmission}, Drive: ${car?.drive}`);
    return (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5">
            {data?.data.map((listing) => (<ListingCard key={listing.id} listing={listing}/>))}
        </div>
    );
}

export default HomePage;