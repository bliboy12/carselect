import { useEffect, useState } from "react";
import type { ListingResponse } from "../types";
import { useMutation, useQuery } from "@tanstack/react-query";
import { deleteFavorite, getAllFavoritesByUserId } from "../api/favoriteApi";
import ListingCard from "../components/Listings/ListingCard";
import { Link } from "react-router";
import ListingCardSkeleton from "../components/Listings/ListingCardSkeleton";
import useCurrentUser from "../hooks/useCurrentUser";


const FavoritesPage = () => {

    const [favoriteListings, setFavoriteListings] = useState<ListingResponse[]>([]);
    const [deletingListingId, setDeletingListingId] = useState<string | null>(null);

    const { userId } = useCurrentUser();

    const { data, isLoading, isError, error } = useQuery({
        queryKey: ["favoriteListings", userId],
        queryFn: () => getAllFavoritesByUserId(userId)
    })

    console.log(data);

    useEffect(() => {
        if (data)
            setFavoriteListings(data.listings);
    }, [data]);

    const { mutate: deleteFavoriteByListingId } = useMutation({
        mutationFn: async (listingId: string) => {
            setDeletingListingId(listingId);
            await deleteFavorite(userId, listingId);
            return listingId;
        },
        onSuccess: (listingId: string) => {
            setFavoriteListings((prev) => prev.filter((l) => l.id !== listingId))
            setDeletingListingId(null);
        }
    })

    const handleRemoveFavorite = (listingId: string) => {
        deleteFavoriteByListingId(listingId);
    }

    if (isLoading) {
        return (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5">
                {Array.from({ length: 8 }).map((_, i) => (<ListingCardSkeleton key={i} />))}
            </div>
        )
    }
    if (isError) return <p>{error.message}</p>
    if (data === undefined)
        return <p>NO FAVORITES ADDED...</p>

    return (
        <div className="px-6 py-10">

            {/* Header */}
            <div className="mb-8 pb-6 border-b border-gray-700">
                <p className="text-2xl font-semibold text-white mb-1">Favourites</p>
                <p className="text-sm text-gray-400">
                    {favoriteListings.length === 0 ? "You haven't saved any listings yet." : `${favoriteListings.length} saved listing${favoriteListings.length === 1 ? "" : "s"}`}
                </p>
            </div>

            {/* Empty state */}
            {favoriteListings.length === 0 ? (
                <div className="flex flex-col items-center justify-center py-24 gap-4">
                    <p className="text-gray-400 text-sm">No favourites yet — start exploring listings.</p>
                    <Link to="/" className="text-sm bg-blue-600 hover:bg-blue-500 transition-colors text-white px-5 py-2 rounded-lg">
                        Browse listings
                    </Link>
                </div>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-5">
                    {favoriteListings.map(listing => (
                        <div key={listing.id} className="relative group">
                            <Link to={`/listings/${listing.id}`}>
                                <ListingCard listing={listing} />
                            </Link>
                            {/* Remove button */}
                            <button onClick={() => handleRemoveFavorite(listing.id)} disabled={deletingListingId === listing.id}
                                className={`cursor-pointer absolute top-3 right-3 w-8 h-8 bg-gray-900/80 hover:bg-red-900/80 text-gray-300 hover:text-red-400 rounded-full flex items-center justify-center transition-colors text-xs opacity-0 group-hover:opacity-100 ${deletingListingId === listing.id ? "cursor-not-allowed opacity-50" : "cursor-pointer"}`}>
                                ✕
                            </button>
                        </div>
                    ))}
                </div>
            )}

        </div>
    );
}

export default FavoritesPage;