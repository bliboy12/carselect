import { useMutation, useQuery } from "@tanstack/react-query";
import { useNavigate, useParams } from "react-router";
import { DeleteImageById, getListingById, updateListing, uploadListingImages } from "../api/listingApi";
import { useForm } from "react-hook-form";
import type { CarImage, CarRequest, CarUpdateRequest, ImageRequest, ListingUpdateRequest } from "../types";
import { useEffect, useState, type ChangeEvent } from "react";
import PillSelector from "../components/Listings/Create/PillSelector";
import { UpdateCarById } from "../api/carApi";


interface ListingRequestForm {
    brand: string
    model: string
    trim: string
    color: string
    buildYear: number
    kilometers: number
    doors: number
    price: number
}

const EditListingPage = () => {

    const navigate = useNavigate();
    const { id } = useParams<{ id: string }>();

    const [selectedFuel, setSelectedFuel] = useState<string | null>(null);
    const [selectedDrive, setSelectedDrive] = useState<string | null>(null);
    const [selectedTransmission, setSelectedTransmission] = useState<string | null>(null);

    const [newImages, setNewImages] = useState<ImageRequest[]>([]);
    const [existingImages, setExistingImages] = useState<CarImage[]>([]);

    const [sellerId, setSellerId] = useState<string>("");
    const [carId, setCarId] = useState<string>("");


    const { data: listing, isLoading, isError, error} = useQuery({
        queryKey: ["listing", id],
        queryFn: () => getListingById(id!),
        enabled: !!id // only run if there is an ID in the params
    });

    const { register, handleSubmit, reset } = useForm<ListingRequestForm>();


    useEffect(() => {
        if (listing)
        {
            reset({
                brand: listing.car.brand,
                model: listing.car.model,
                trim: listing.car.trim,
                color: listing.car.color,
                buildYear: listing.car.buildYear,
                kilometers: listing.car.kilometers,
                doors: listing.car.doors,
                price: listing.price
            })
            setSelectedFuel(listing.car.fuel);
            setSelectedDrive(listing.car.drive);
            setSelectedTransmission(listing.car.transmission);
            setExistingImages(listing.carImages);
            setSellerId(listing.seller.id);
            setCarId(listing.car.id);
        }
    }, [listing, reset])

    const { mutate: saveListing, isPending } = useMutation({
        mutationFn: async (data: { car: CarUpdateRequest, listing: ListingUpdateRequest, newImages: ImageRequest[] }) => {
            await UpdateCarById(carId, data.car);
            await updateListing(id!, data.listing);
            if (data.newImages.length > 0)
                await uploadListingImages(id!, data.newImages);
        },
        onSuccess: () => navigate("/profile")
    });

    const { mutate: deleteImage } = useMutation({
        mutationFn: async (data: { listingId: string, imageId: string }) => await DeleteImageById(data.listingId, data.imageId)
    })

    // Update the listing with new data
    const onSubmit = (formData: ListingRequestForm) => {
        saveListing({
            car: {
                id: carId,
                brand: formData.brand,
                model: formData.model,
                trim: formData.trim,
                color: formData.color,
                buildYear: formData.buildYear,
                kilometers: formData.kilometers,
                doors: formData.doors,
                fuel: selectedFuel as CarRequest["fuel"],
                transmission: selectedTransmission as CarRequest["transmission"],
                drive: selectedDrive as CarRequest["drive"]
            },
            listing: {
                sellerId: sellerId,
                carId: carId,
                price: formData.price,
                status: "active"
            },
            newImages
        });
    };
    // Removes images that had been saved to the blob storage
    const onDeleteExisting = (listingId: string, imageId: string) => {
        // Delete the newImages out of the Blob Storage
        deleteImage({ listingId: listingId, imageId: imageId });
        // Delete the newImages out of the list
        setExistingImages((prev) => prev.filter((c) => c.id !== imageId));
    };

    // Removes newly added images which aren't yet saved to the blob storage
    const handleRemoveNew = (index: number) => {
        setNewImages((prev) => prev.filter((_, i) => i !== index))
    }

    const handleNewImageUpload = (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.files)
        {
            const upload: ImageRequest[] = Array.from(e.target.files).map(file => ({
                file,
                isMainImage: false
            }));
            setNewImages(prev => [...prev, ...upload]);
        }
    }
    const handleSetMainExisting = (imageId: string) => {
        setExistingImages((prev) => prev.map(img => ({
            ...img,
            isMainImage: img.id === imageId
        })));
    };
    const handleSetMainNew = (index: number) => {
        setNewImages(prev => prev.map((img, i) => ({
            ...img,
            isMainImage: i === index
        })));
    };

    if (isLoading)
        return <p>IS LOADING...</p>
    if (isError)
        return <p>{error.message}</p>
    
    // protects against missing ID in useParams, and must be called after all the hooks.
    // React tracks hooks by the order they're called in every render. If a hook is called conditionally (sometimes yes, sometimes no), React loses track of which hook is which and everything breaks.
    if (!id) return <p>Invalid Listing ID { id }</p>
    
    return (
        <div className="max-w-3xl mx-auto px-6 py-10">

            {/* Header */}
            <div className="mb-8 pb-6 border-b border-gray-700">
                <p className="text-2xl font-semibold text-white mb-1">Edit listing</p>
                <p className="text-sm text-gray-400">Update the details of your listing</p>
            </div>

            <div className="flex flex-col gap-8">

                {/* Car Details */}
                <div>
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Car details</p>
                    <div className="grid grid-cols-2 gap-4">
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Brand</label>
                            <input
                                type="text"
                                placeholder="e.g. BMW"
                                {...register("brand")}
                                className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                            />
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Model</label>
                            <input
                                type="text"
                                placeholder="e.g. 3 Series"
                                {...register("model")}
                                className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                            />
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Trim</label>
                            <input
                                type="text"
                                placeholder="e.g. M Sport"
                                {...register("trim")}
                                className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                            />
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Color</label>
                            <input
                                type="text"
                                placeholder="e.g. Black"
                                {...register("color")}
                                className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                            />
                        </div>
                    </div>
                </div>

                <div className="border-t border-gray-700" />

                {/* Specs */}
                <div>
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Specs</p>
                    <div className="grid grid-cols-3 gap-4">
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Build year</label>
                            <input
                                type="number"
                                placeholder="e.g. 2021"
                                {...register("buildYear", { valueAsNumber: true })}
                                className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                            />
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Kilometers</label>
                            <input
                                type="number"
                                placeholder="e.g. 45000"
                                {...register("kilometers", { valueAsNumber: true })}
                                className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                            />
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Doors</label>
                            <input
                                type="number"
                                placeholder="e.g. 4"
                                {...register("doors", { valueAsNumber: true })}
                                className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                            />
                        </div>
                    </div>
                </div>

                <div className="border-t border-gray-700" />

                {/* Drivetrain */}
                <div>
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Drivetrain</p>
                    <div className="flex flex-col gap-5">
                        <div>
                            <label className="block text-xs text-gray-400 mb-2">Fuel type</label>
                            <PillSelector
                                options={["Petrol", "Diesel", "Electric", "Hybrid"]}
                                selected={selectedFuel}
                                onSelect={(val) => setSelectedFuel(val)}
                            />
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-2">Transmission</label>
                            <PillSelector
                                options={["Manual", "Automatic"]}
                                selected={selectedTransmission}
                                onSelect={(val) => setSelectedTransmission(val)}
                            />
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-2">Drive type</label>
                            <PillSelector
                                options={["FWD", "RWD", "AWD", "4WD"]}
                                selected={selectedDrive}
                                onSelect={(val) => setSelectedDrive(val)}
                            />
                        </div>
                    </div>
                </div>

                <div className="border-t border-gray-700" />

                {/* Pricing */}
                <div>
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Pricing</p>
                    <div className="max-w-240px">
                        <label className="block text-xs text-gray-400 mb-1.5">Price (€)</label>
                        <input
                            type="number"
                            placeholder="e.g. 24000"
                            {...register("price", { valueAsNumber: true })}
                            className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600"
                        />
                    </div>
                </div>

                <div className="border-t border-gray-700" />

                {/* Existing Images */}
                {existingImages.length > 0 && (
                    <div className="mb-6">
                        <p className="text-xs text-gray-400 mb-3">Current images</p>
                        <div className="grid grid-cols-3 gap-3">
                            {existingImages.map((img) => (
                                <div
                                    key={img.id}
                                    className={`relative rounded-xl overflow-hidden border-2 transition-all ${
                                        img.isMainImage ? "border-blue-500" : "border-gray-700"
                                    }`}
                                >
                                    <img
                                        src={img.imageUrl}
                                        alt="listing image"
                                        className="w-full aspect-4/3 object-cover"
                                    />
                                    {img.isMainImage && (
                                        <div className="absolute top-2 left-2 bg-blue-600 text-white text-xs px-2 py-0.5 rounded-full">
                                            Main
                                        </div>
                                    )}
                                    {!img.isMainImage && (
                                        <button
                                            type="button"
                                            onClick={() => handleSetMainExisting(img.id)}
                                            className="absolute top-2 left-2 bg-gray-900/80 text-gray-300 hover:text-white text-xs px-2 py-0.5 rounded-full transition-colors"
                                        >
                                            Set as main
                                        </button>
                                    )}
                                    <button
                                        type="button"
                                        onClick={() => onDeleteExisting(id, img.id)}
                                        className="absolute top-2 right-2 w-6 h-6 bg-gray-900/80 hover:bg-red-900/80 text-gray-300 hover:text-red-400 rounded-full flex items-center justify-center transition-colors text-xs"
                                    >
                                        ✕
                                    </button>
                                </div>
                            ))}
                        </div>
                    </div>
                )}

                {/* Images */}
                <div>
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Images</p>
                    <label className="block border-2 border-dashed border-gray-700 rounded-xl p-10 text-center cursor-pointer hover:border-gray-500 transition-colors">
                        <p className="text-sm text-white mb-1">Drop images here or click to upload</p>
                        <p className="text-xs text-gray-400">PNG, JPG up to 10MB each</p>
                        <input type="file" multiple accept="image/*" className="hidden" onChange={handleNewImageUpload}/>
                    </label>

                    {newImages.length > 0 && (
                        <div className="mt-4 grid grid-cols-3 gap-3">
                            {newImages.map((img, i) => (
                                <div key={i} className={`relative rounded-xl overflow-hidden border-2 transition-all ${ img.isMainImage ? "border-blue-500" : "border-gray-700"}`}>
                                    {/* Thumbnail */}
                                    <img src={URL.createObjectURL(img.file)} alt={img.file.name} className="w-full aspect-4/3 object-cover"/>

                                    {/* Main image badge */}
                                    {img.isMainImage && (
                                        <div className="absolute top-2 left-2 bg-blue-600 text-white text-xs px-2 py-0.5 rounded-full">
                                            Main
                                        </div>
                                    )}

                                    {/* Set as main button */}
                                    {!img.isMainImage && (
                                        <button type="button" onClick={() => handleSetMainNew(i)} className="absolute top-2 left-2 bg-gray-900/80 text-gray-300 hover:text-white text-xs px-2 py-0.5 rounded-full transition-colors">
                                            Set as main
                                        </button>
                                    )}

                                    {/* Remove button */}
                                    <button type="button" onClick={() => handleRemoveNew(i)} className="absolute top-2 right-2 w-6 h-6 bg-gray-900/80 hover:bg-red-900/80 text-gray-300 hover:text-red-400 rounded-full flex items-center justify-center transition-colors text-xs">
                                        ✕
                                    </button>
                                </div>
                            ))}
                        </div>
                    )}
                </div>

                {/* Actions */}
                <div className="flex justify-end gap-3 pt-2">
                    <button
                        type="button"
                        onClick={() => navigate("/profile")}
                        className="border border-gray-700 text-gray-400 hover:text-white hover:border-gray-500 text-sm px-5 py-2 rounded-lg transition-colors"
                    >
                        Cancel
                    </button>
                    <button
                        type="button"
                        disabled={isPending}
                        onClick={handleSubmit(onSubmit)}
                        className="bg-blue-600 hover:bg-blue-500 disabled:opacity-50 disabled:cursor-not-allowed text-white text-sm px-6 py-2 rounded-lg transition-colors"
                    >
                        {isPending ? "Saving..." : "Save Changes"}
                    </button>
                </div>

            </div>
        </div>
    )
}

export default EditListingPage;