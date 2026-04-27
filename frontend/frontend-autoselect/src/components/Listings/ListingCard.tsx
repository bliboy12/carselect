import type { CarImage, CarInfo, Listing } from "../../types";
import vehicle from "../../assets/car.jpg"

interface listingCardProp {
    listing: Listing,
}

const ListingCard = (prop: listingCardProp) => {

    const {listing} = prop;
    const {car, user, carImages} = listing;
    //const mainImage = carImages.find((c) => c.isMainImage === true);

    return (
        <div className="group bg-gray-900 border border-gray-800 rounded-2xl overflow-hidden hover:border-gray-600 hover:shadow-xl hover:shadow-black/40 transition-all duration-200 cursor-pointer ">
            <div className="relative aspect-4/3 bg-gray-800 overflow-hidden">
                <img src={vehicle} className="w-full h-full object-cover"/>
                {/* Sold overlay */}
                {listing.status === "sold" && (
                    <div className="absolute inset-0 bg-gray-950/75 flex items-center justify-center">
                        <span className="bg-gray-800 text-gray-300 text-xs font-semibold px-4 py-1.5 rounded-full border border-gray-600 tracking-wide uppercase">
                            Sold
                        </span>
                    </div>
                )}
            </div>

            <div className="p-5 flex flex-col gap-4">
                <div>
                    <div className="flex justify-between">
                        <h3>{car.brand} test {car.model}</h3>
                        <p>{car.trim} {car.buildYear}</p>
                    </div>
                    <span>{listing.price}</span>
                </div>
                <br/>
                <div>
                    <span>{car.kilometers}</span>
                    <span>{car.fuel}</span>
                    <span>{car.transmission}</span>
                    <span>{car.doors}</span>
                </div>
            </div> 
        </div>
    );
}
export default ListingCard;