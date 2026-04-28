import type { CarImage, CarInfo, Listing } from "../../types";
import vehicle from "../../assets/car.jpg"

interface listingCardProp {
    listing: Listing,
}

const ListingCard = (prop: listingCardProp) => {

    const {listing} = prop;
    const {car, user, carImages} = listing;
    //const mainImage = carImages.find((c) => c.isMainImage === true);

    const carImage = carImages.find(c => c.isMainImage == true);

    new Intl.NumberFormat("de-DE", {style: "currency", currency: "EUR"}).format(listing.price);

    // CarImage is hardcoded, needs to be replaced!
    return (
        <div className="group bg-gray-900 border border-gray-800 rounded-2xl overflow-hidden hover:border-gray-600 hover:shadow-xl hover:shadow-black/40 transition-all duration-200 cursor-pointer ">
            <div className="relative aspect-4/3 bg-gray-800 overflow-hidden">
                <img src={vehicle} className="w-full h-full object-cover"/>
                {/* Sold overlay */}
                {status === "sold" && (
                    <div className="absolute inset-0 bg-gray-950/75 flex items-center justify-center">
                        <span className="bg-gray-800 text-gray-300 text-xs font-semibold px-4 py-1.5 rounded-full border border-gray-600 tracking-wide uppercase">
                            Sold
                        </span>
                    </div>
                )}
            </div>

            <div className="p-5 flex flex-col gap-4">
                <div className="flex flex-col gap-1">
                    <div className="flex justify-between items-start">
                        <div>
                            <h3 className="font-bold text-2xl text-white">{car.brand} {car.model}</h3>
                            <p className="text-sm text-gray-400">{car.trim}</p>
                        </div>
                        <p className="text-xl font-bold text-blue-400">€ {listing.price}</p>
                    </div>
                </div>
                <div className="flex gap-2 flex-wrap">
                    <span className="text-sm text-gray-400 border border-gray-600 px-4 py-1.5 rounded-full">{car.buildYear}</span>
                    <span className="text-sm text-gray-400 border border-gray-600 px-4 py-1.5 rounded-full">{car.kilometers} km</span>
                    <span className="text-sm text-gray-400 border border-gray-600 px-4 py-1.5 rounded-full">{car.fuel}</span>
                    <span className="text-sm text-gray-400 border border-gray-600 px-4 py-1.5 rounded-full">{car.transmission}</span>
                    <span className="text-sm text-gray-400 border border-gray-600 px-4 py-1.5 rounded-full">{car.drive}</span>
                    <span className="text-sm text-gray-400 border border-gray-600 px-4 py-1.5 rounded-full">{car.doors} Doors</span>
                </div>
            </div>
        </div>
    );
}
export default ListingCard;