import { axiosCars, axiosListings } from "../api/CarSelectApi";
import { useQuery } from "@tanstack/react-query";
import { type CarImage, type CarInfo, type Listing } from "../types";
import ListingCard from "../components/Listings/ListingCard";

interface listingInfoProps {
    listings: Listing[],
}

interface listingCardProp {
    listing: Listing,
    car: CarInfo,
    carImages: CarImage
}

const HomePage = () => {

    const { data } = useQuery({
        queryKey: ["GetAllListings"],
        queryFn: async () => {
            return await axiosListings.get<Listing[]>("/")
        },
        refetchOnWindowFocus: false,
    })

    const listings = data?.data;


    return (
        <div>
            {listings?.map((listing) => (<ListingCard key={listing.id} listing={listing} />))}
        </div>
    );
}

export default HomePage;