import { useQuery } from "@tanstack/react-query";
import ListingCard from "../components/Listings/ListingCard";
import ListingFilter from "../components/Listings/ListingFilter";
import { useState } from "react";
import { axiosListings } from "../api/axiosInstances";
import type { CarFilter, ListingResponse, PaginatedResponse } from "../types";
import ListingCardSkeleton from "../components/Listings/ListingCardSkeleton";
import { Link } from "react-router";

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
    drive: undefined,
    page: 1,
    pageSize: 12
}

const HomePage = () => {
    const [showFilter, setshowFilter] = useState(true);
    const [filter, setFilter] = useState<CarFilter>(defaultCarFilter);
    const [onSubmit, setOnSubmit] = useState<CarFilter>(defaultCarFilter);
    const [currentPage, setCurrentPage] = useState(1);

    const { data, isLoading, isError, error } = useQuery({
        queryKey: ["GetAllListingsFiltered", onSubmit, currentPage],
        queryFn: async () => {
            return await axiosListings.post<PaginatedResponse<ListingResponse>>("/search", {
                ...onSubmit,
                page: currentPage,
                pageSize: 12
            });
        }
    });

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
        window.scrollTo({ top: 0, behavior: "smooth" });
    };

    const handleSubmit = (newFilter: CarFilter) => {
        setCurrentPage(1); // reset to page 1 on new search
        setOnSubmit(newFilter);
    };

    if (isLoading) {
        return (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5">
                {Array.from({ length: 12 }).map((_, i) => (<ListingCardSkeleton key={i} />))}
            </div>
        );
    }

    if (isError) {
        console.log(error.message);
        return <p>Something went wrong</p>;
    }

    const listings = data?.data.items ?? [];
    const totalPages = data?.data.totalPages ?? 1;
    const cars = listings.map(l => l.car);

    if (listings.length === 0) {
        return (
            <div className="flex min-h-dvh">
                <ListingFilter showFilter={showFilter} onToggle={() => setshowFilter(prev => !prev)} filter={filter} setFilter={setFilter} setOnSubmit={handleSubmit} cars={cars} />
                <div className="flex-1 flex items-center justify-center">
                    <p className="text-gray-400">No results found</p>
                </div>
            </div>
        );
    }

    return (
        <div className="flex min-h-dvh flex-col">
            <div className="flex flex-1">
                <ListingFilter showFilter={showFilter} onToggle={() => setshowFilter(prev => !prev)} filter={filter} setFilter={setFilter} setOnSubmit={handleSubmit} cars={cars} />
                <div className="flex-1 flex flex-col">
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 p-5 gap-5">
                        {listings.map(listing => (
                            <Link className="block h-full" key={listing.id} to={`listings/${listing.id}`}>
                                <ListingCard listing={listing} />
                            </Link>
                        ))}
                    </div>

                    {/* Pagination */}
                    {totalPages > 1 && (
                        <div className="flex items-center justify-center gap-2 py-8">
                            <button
                                onClick={() => handlePageChange(currentPage - 1)}
                                disabled={currentPage === 1}
                                className="px-3 py-2 text-sm text-gray-400 border border-gray-700 rounded-lg hover:border-gray-500 hover:text-white disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                            >
                                ← Prev
                            </button>

                            {Array.from({ length: totalPages }, (_, i) => i + 1).map(page => (
                                <button
                                    key={page}
                                    onClick={() => handlePageChange(page)}
                                    className={`px-3 py-2 text-sm rounded-lg border transition-colors ${
                                        page === currentPage
                                            ? "bg-blue-600 border-blue-600 text-white"
                                            : "border-gray-700 text-gray-400 hover:border-gray-500 hover:text-white"
                                    }`}
                                >
                                    {page}
                                </button>
                            ))}

                            <button
                                onClick={() => handlePageChange(currentPage + 1)}
                                disabled={currentPage === totalPages}
                                className="px-3 py-2 text-sm text-gray-400 border border-gray-700 rounded-lg hover:border-gray-500 hover:text-white disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                            >
                                Next →
                            </button>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}

export default HomePage;