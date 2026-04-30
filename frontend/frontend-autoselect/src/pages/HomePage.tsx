//import { axiosListings } from "../api/CarSelectApi";
//import { useQuery } from "@tanstack/react-query";
//import { type CarImage, type CarInfo, type Listing } from "../types";
import ListingCard from "../components/Listings/ListingCard";
import ListingFilter from "../components/Listings/ListingFilter";
//import ListingCardSkeleton from "../components/Listings/ListingCardSkeleton";
import { dummyListings } from "../dummy/ListingDummy";
import { useState } from "react";


// interface listingCardProp {
//     listing: Listing,
//     car: CarInfo,
//     carImages: CarImage
// }



const HomePage = () => {

    // const {data, isLoading, isError, error} = useQuery({
    //     queryKey: ["GetAllListings"],
    //     queryFn: async () => {
    //         return await axiosListings.get<Listing[]>("/")}
    // })

    
    // if (isLoading)
    // {
    //     return (
    //         <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5">
    //             {Array.from({length: 8}).map((_, i) => (<ListingCardSkeleton key={i}/>))}
    //         </div>
    //     );
    // }
    // if (isError)
    // {
    //     console.log(error.message);
    //     return <p>Something went wrong</p>
    // }
    // const result = data?.data[0];

    // const car = result?.car;
    
    // console.log(`ID: ${result?.id }, carId: ${result?.id}, CreatedAt: ${result?.createdAt}, Price: ${result?.price}`);
    // console.log(`\nBrand: ${car?.brand}, Model: ${car?.model}, Trim: ${car?.trim}, Color: ${car?.color}, Buildyear: ${car?.buildYear}, Transmission: ${car?.transmission}, Drive: ${car?.drive}`);

    const [showFilter, setshowFilter] = useState(true);


    return (
        <div className="flex min-h-dvh">
            <ListingFilter showFilter={showFilter} onToggle={() => setshowFilter((prev) => !prev)}/>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5 flex-1 h-full">
                {dummyListings.map((listing) => (<ListingCard key={listing.id} listing={listing}/>))}
            </div>
        </div>
    );
}

export default HomePage;