import { useEffect, useState } from "react";
import { useMutation, useQuery } from "@tanstack/react-query";
import { Link, useParams } from "react-router";
import { axiosListings } from "../../api/axiosInstances";
import type { ListingResponse } from "../../types";
import DetailListingSkeleton from "./DetailListingCardSkeleton";
import defaultCarImage from "../../assets/car.jpg"
import { CiHeart } from "react-icons/ci";
import { FaHeart } from "react-icons/fa";
import { createFavorite, deleteFavorite, isFavorited } from "../../api/favoriteApi";
import useCurrentUser from "../../hooks/useCurrentUser";


const DetailListingCard = () => {

    const { id } = useParams();

    const { userId } = useCurrentUser();

    const [activeImage, setActiveImage] = useState(0);
    const [isFavorite, setIsFavorite] = useState<boolean>(false);

    const { data: dataListing, isLoading, isError, error } = useQuery({
        queryKey: ["listing", id],
        queryFn: async () => await axiosListings.get<ListingResponse>(`/${id}`),
        refetchOnMount: false,
        refetchOnReconnect: false,
        refetchOnWindowFocus: false
    });

    const { data: favoriteStatus } = useQuery({
        queryKey: ["isFavorite", id],
        queryFn: () => isFavorited(userId, id!),
        enabled: !!id
    });

    useEffect(() => {
        if (favoriteStatus !== undefined)
            setIsFavorite(favoriteStatus);
    }, [favoriteStatus]);

    const { mutate: createFavoriteMutation } = useMutation({
        mutationFn: async () => {
            const response = await createFavorite(userId, id!);
            return response;
        },
        onSuccess: () => { setIsFavorite(true); }
    })

    const { mutate: deleteFavoriteMutation } = useMutation({
        mutationFn: async () => {
            await deleteFavorite(userId, id!);
        },
        onSuccess: () => { setIsFavorite(false); }
    })

    const handleFavorite = () => {
        if (isFavorite)
            deleteFavoriteMutation();
        else
            createFavoriteMutation();
    }

    if (isLoading)
        return (<DetailListingSkeleton />)
    if (isError)
        return <p>{error.message}</p>
    if (!dataListing)
        return <p>Listing doesn't exist</p>


    const listing = dataListing.data;
    const car = listing.car;
    const seller = listing.seller;

    const carImages = listing.carImages.length > 0
        ? listing.carImages.map((img) => img.imageUrl)
        : Array.from({ length: 5 }, () => defaultCarImage);

    console.log(`${seller.firstName}${seller.lastName}`.toUpperCase());

    const sellerInitials = `${seller.firstName[0]}${seller.lastName[0]}`.toUpperCase();

    return (
        <div className="min-h-screen bg-gray-950 py-8 px-4">
            <div className="max-w-6xl mx-auto">
                <nav className="mb-6 text-sm text-gray-500">
                    <Link to="/" className="font-semibold text-blue-400 hover:text-blue-300 transition-colors">Listings</Link>
                    <span className="mx-2">{">"}</span>
                    <span className="text-gray-300">{`${car?.brand} ${car?.model}`}</span>
                </nav>

                <div className="grid grid-cols-1 lg:grid-cols-[1fr_340px] gap-6">
                    {/* Images + Specs */}
                    <div className="flex flex-col gap-6">
                        {/* Images */}
                        <div className="flex flex-col gap-3">
                            {/* Main Image */}
                            <div className="relative aspect-video rounded-xl overflow-hidden bg-gray-900 border border-gray-800">
                                <img src={carImages[activeImage]} alt={`${car.brand} ${car.model}`} className="w-full h-full object-cover" />
                                <span className="absolute bottom-3 right-3 bg-black/60 text-gray-300 text-xs px-2 py-1 rounded">
                                    {activeImage + 1}/{carImages.length}
                                </span>
                            </div>
                            {/* Thumbnails */}
                            <div className="flex gap-2 pb-1 overflow-x-auto">
                                {carImages.map((img, i) => (
                                    <button
                                        key={i}
                                        onClick={() => setActiveImage(i)}
                                        className={`shrink-0 w-20 h-14 rounded-lg border transition-colors overflow-hidden ${i === activeImage ? "border-blue-500" : "border-gray-700 hover:border-gray-500"}`}
                                    >
                                        <img src={img} alt={`View ${i + 1}`} className="w-full h-full object-cover" />
                                    </button>
                                ))}
                            </div>
                        </div>

                        {/* Specs */}
                        <div className="bg-gray-900 rounded-xl border border-gray-800 p-5">
                            <h2 className="text-xs font-medium text-gray-500 uppercase tracking-widest mb-4">Specifications</h2>
                            <div className="grid grid-cols-2 gap-x-6">
                                {[
                                    { label: "Brand", value: car.brand },
                                    { label: "Model", value: car.model },
                                    { label: "Trim", value: car.trim },
                                    { label: "Year", value: car.buildYear },
                                    { label: "Fuel", value: car.fuel },
                                    { label: "Kilometers", value: `${car.kilometers.toLocaleString()} km` },
                                    { label: "Drive", value: car.drive },
                                    { label: "Transmission", value: car.transmission },
                                    { label: "Color", value: car.color },
                                ].map((spec, i, arr) => (
                                    <div key={spec.label} className={`py-3 ${i < arr.length - 2 ? "border-b border-gray-800" : ""}`}>
                                        <p className="text-xs text-gray-500 mb-1">{spec.label}</p>
                                        <p className="text-sm text-gray-100 font-medium">{spec.value}</p>
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>

                    {/* Pricing, Seller, Favorite */}
                    <div className="flex flex-col gap-4 lg:sticky lg:top-24 lg:self-start">
                        {/* Title */}
                        <div>
                            <h1 className="text-xl font-medium text-gray-50">{`${car.buildYear} ${car.brand} ${car.model} ${car.trim}`}</h1>
                            <div className="flex flex-wrap gap-2 mt-3">
                                <span className="text-xs px-2.5 py-1 rounded-full bg-blue-950 text-blue-300 border border-blue-800">{listing.status}</span>
                                <span className="text-xs px-2.5 py-1 rounded-full bg-gray-800 text-gray-400 border border-gray-700">{car.fuel}</span>
                                <span className="text-xs px-2.5 py-1 rounded-full bg-gray-800 text-gray-400 border border-gray-700">{car.transmission}</span>
                            </div>
                        </div>

                        {/* Price */}
                        <div className="bg-gray-900 rounded-xl border border-gray-800 p-5">
                            <p className="text-xs text-gray-500 uppercase tracking-widest mb-1">Asking price</p>
                            <p className="text-3xl font-medium text-gray-50">€{listing.price.toLocaleString()}</p>
                            <p className="text-xs text-gray-500 mt-1">
                                Listed {new Date(listing.createdAt).toLocaleDateString("en-GB", { day: "numeric", month: "short", year: "numeric" })}
                            </p>
                        </div>

                        {/* Save Listing */}
                        <button onClick={() => handleFavorite()} className="w-full flex items-center justify-center gap-2 py-2.5 rounded-xl border border-gray-700 text-gray-300 hover:border-gray-500 hover:text-gray-100 transition-colors text-sm cursor-pointer">
                            {isFavorite ? <FaHeart className="size-5" /> : <CiHeart className="size-5" />}
                            Save listing
                        </button>

                        <hr className="border-gray-800" />

                        {/* Seller */}
                        <div className="bg-gray-900 rounded-xl border border-gray-800 p-5">
                            <p className="text-xs text-gray-500 uppercase tracking-widest mb-3">Seller</p>
                            <div className="flex items-center gap-3">
                                <div className="w-10 h-10 rounded-full bg-blue-950 border border-blue-800 flex items-center justify-center text-blue-300 text-sm font-medium shrink-0">
                                    {sellerInitials}
                                </div>
                                <div>
                                    <p className="text-sm font-medium text-gray-100">{seller.firstName} {seller.lastName}</p>
                                    <div className="flex items-center gap-1 mt-1">
                                        {[1, 2, 3, 4, 5].map((star) => (
                                            <svg key={star} xmlns="http://www.w3.org/2000/svg" className={`w-3.5 h-3.5 ${star <= 4 ? "text-yellow-400" : "text-gray-600"}`} viewBox="0 0 24 24" fill="currentColor">
                                                <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z" />
                                            </svg>
                                        ))}
                                        <span className="text-xs text-gray-400 ml-1">4.0</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default DetailListingCard;