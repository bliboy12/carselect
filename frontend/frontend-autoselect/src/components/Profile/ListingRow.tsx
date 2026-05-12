import type { Listing } from "../../types";
import { FaRegEdit } from "react-icons/fa";
import { MdDelete } from "react-icons/md";

interface ListingRowProps {
    listing: Listing,
    onEdit: (id: string) => void,
    onDelete: (id: string) => void
}

const ListingRow = ({ listing, onEdit, onDelete }: ListingRowProps) => {

    const mainImage = listing.carImages.find(img => img.isMainImage);
    const car = listing.car;

    return (
        <div className="flex items-center gap-4 py-3">

            {/* Thumbnail */}
            <div className="w-20 h-14 rounded-lg bg-gray-800 shrink-0 overflow-hidden">
                <img
                    src={mainImage?.imageUrl ?? '/bmw-fallback.jpg'}
                    alt={`${car.brand} ${car.model}`}
                    className="w-full h-full object-cover"
                />
            </div>

            {/* Car details */}
            <div className="flex-1 min-w-0">
                <p className="text-sm font-medium text-white truncate capitalize">
                    {`${car.brand} ${car.model} ${car.trim}`}
                </p>
                <p className="text-xs text-gray-400 mt-0.5">
                    {`${car.buildYear} · ${car.kilometers.toLocaleString()} km`}
                </p>
            </div>

            {/* Price */}
            <p className="text-sm font-medium text-white shrink-0">
                €{listing.price.toLocaleString()}
            </p>

            {/* Actions */}
            <div className="flex items-center gap-2 shrink-0">
                <button
                    onClick={() => onEdit(listing.id)}
                    aria-label="Edit listing"
                    className="w-8 h-8 flex items-center justify-center rounded-lg border border-gray-700 text-gray-400 hover:text-white hover:border-gray-500 transition-colors"
                >
                    <FaRegEdit size={14} />
                </button>
                <button
                    onClick={() => onDelete(listing.id)}
                    aria-label="Delete listing"
                    className="w-8 h-8 flex items-center justify-center rounded-lg border border-red-800 text-red-400 hover:text-red-300 hover:border-red-600 transition-colors"
                >
                    <MdDelete size={14} />
                </button>
            </div>

        </div>
    );
}

export default ListingRow;