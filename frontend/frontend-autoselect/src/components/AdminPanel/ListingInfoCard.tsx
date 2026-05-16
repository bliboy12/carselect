import { MdDelete } from "react-icons/md";
import type { ListingResponse } from "../../types";


interface ListingInfoCardProps {
    listing: ListingResponse,
    onDelete: (listingId: string) => void,
    isDeleting: boolean
}

const ListingInfoCard = ({ listing, onDelete, isDeleting }: ListingInfoCardProps) => {
    
    const mainImage = listing.carImages.find(img => img.isMainImage);
    const car = listing.car;
    
        return (
        <div className="flex items-center gap-4 py-3">

            {/* Thumbnail */}
            <div className="w-20 h-14 rounded-lg bg-gray-800 shrink-0 overflow-hidden">
                {/* A correct fallback image needs to be added */}
                <img src={mainImage?.imageUrl ?? '/bmw-fallback.jpg'} alt={`${car.brand} ${car.model}`} className="w-full h-full object-cover"/>
            </div>

            {/* Car details */}
            <div className="flex-1 min-w-0">
                <p className="text-sm font-medium text-white truncate capitalize">{`${car.brand} ${car.model} ${car.trim}`}</p>
                <p className="text-xs text-gray-400 mt-0.5">{`${car.buildYear} · ${car.kilometers.toLocaleString()} km`}</p>
            </div>
            
            <p className="text-xs text-gray-400 mt-0.5">by {listing.seller.firstName} {listing.seller.lastName} · {`${car.buildYear} · ${car.kilometers.toLocaleString()} km`}</p>

            {/* Price */}
            <p className="text-sm font-medium text-white shrink-0">€{listing.price.toLocaleString()}</p>

            {/* Actions */}
            <div className="flex items-center gap-2 shrink-0">
                    <button onClick={() => onDelete(listing.id)} aria-label="Delete listing" className={`w-8 h-8 flex items-center justify-center rounded-lg border border-red-800 text-red-400 hover:text-red-300 hover:border-red-600 transition-colors ${isDeleting ? "opacity-50 cursor-not-allowed" : ""}`}>
                    { isDeleting ? "..." : <MdDelete size={14} />}
                </button>
            </div>

        </div>
    );
}

export default ListingInfoCard;