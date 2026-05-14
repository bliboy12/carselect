//import { axiosListings } from "../api/CarSelectApi";
//import { useQuery } from "@tanstack/react-query";
//import { type CarImage, type CarInfo, type Listing } from "../types";
import { useQuery } from "@tanstack/react-query";
import ListingCard from "../components/Listings/ListingCard";
import ListingFilter from "../components/Listings/ListingFilter";
//import ListingCardSkeleton from "../components/Listings/ListingCardSkeleton";
//import { dummyListings } from "../dummy/ListingDummy";
import { useState } from "react";
import { axiosListings } from "../api/axiosInstances";
import type { CarFilter, ListingResponse } from "../types";
import ListingCardSkeleton from "../components/Listings/ListingCardSkeleton";
import { Link } from "react-router";


// interface listingCardProp {
//     listing: Listing,
//     car: CarInfo,
//     carImages: CarImage
// }



const HomePage = () => {
    

    const defaultCarFilter: CarFilter = {
        brand: undefined,
        model: undefined,
        color: undefined,
        trim: undefined,
        yearFrom: undefined,
        yearTo: undefined,
        fuel: undefined,
        transmission: undefined,
        minKilometers: undefined,
        maxKilometers: undefined,
        doors: undefined,
        drive: undefined
    }

    const [showFilter, setshowFilter] = useState(true);
    const [filter, setFilter] = useState<CarFilter>(defaultCarFilter);
    const [onSubmit, setOnSubmit] = useState<CarFilter>(defaultCarFilter);

    const {data, isLoading, isError, error} = useQuery({
        queryKey: ["GetAllListings", onSubmit],
        queryFn: async () => {
            return await axiosListings.post<ListingResponse[]>("/search", onSubmit)}
    })


    if (isLoading)
    {
        return (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5">
                {Array.from({length: 8}).map((_, i) => (<ListingCardSkeleton key={i}/>))}
            </div>
        );
    }
    if (isError)
    {
        console.log(error.message);
        return <p>Something went wrong</p>
    }

    // console.log(`ID: ${result?.id }, carId: ${result?.id}, CreatedAt: ${result?.createdAt}, Price: ${result?.price}`);
    // console.log(`\nBrand: ${car?.brand}, Model: ${car?.model}, Trim: ${car?.trim}, Color: ${car?.color}, Buildyear: ${car?.buildYear}, Transmission: ${car?.transmission}, Drive: ${car?.drive}`);

    // const { data } = useQuery({
    //     queryKey: ["GetAllCarsFilter", onSubmit],
    //     queryFn: async () => {
    //         return await axiosCars.post<CarInfo[]>("/search", onSubmit)
    //     }
    // });

    const cars = data?.data.map((l) => l.car) ?? [];

    if (cars.length === 0)
    {
        return (
            <div className="flex min-h-dvh">
                <ListingFilter showFilter={showFilter} onToggle={() => setshowFilter((prev) => !prev)} filter={filter} setFilter={setFilter} setOnSubmit={setOnSubmit} cars={cars} />
                <p>NO RESULTS</p>
            </div>
        )
    }

    return (
        <div className="flex min-h-dvh">
            <ListingFilter showFilter={showFilter} onToggle={() => setshowFilter((prev) => !prev)} filter={filter} setFilter={setFilter} setOnSubmit={setOnSubmit} cars={cars} />
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5 flex-1 h-full">
                {data?.data.map((listing) => (<Link key={listing.id} to={`listings/${listing.id}`}><ListingCard key={listing.id} listing={listing} /></Link>))}
            </div>
        </div>
    );
}

export default HomePage;