import { useState } from "react";
import { useForm } from "react-hook-form";
import type { CarRequest, ListingRequest } from "../types";
import { useMutation } from "@tanstack/react-query";
import { createListing, uploadListingImages } from "../api/listingApi";
import { useNavigate } from "react-router";
import PillSelector from "../components/Listings/Create/PillSelector";

interface CreateListingForm {
    brand: string
    model: string
    trim: string
    color: string
    buildYear: number
    kilometers: number
    doors: number
    price: number
}

const TEMP_USER_ID = "8cd1612e-8161-4c61-89d1-d0ba9e1153af";

const CreateListingPage = () => {

    const navigate = useNavigate();

    const { register, handleSubmit } = useForm<CreateListingForm>();

    // pill selector
    const [selectedFuel, setSelectedFuel] = useState<string | null>(null);
    const [selectedTransmission, setSelectedTransmission] = useState<string | null>(null);
    const [selectedDrive, setSelectedDrive] = useState<string | null>(null);

    // Image uploads
    const [images, setImages] = useState<File[]>([]);

    // Post Request 
    const {mutate: submitListing, isPending } = useMutation({
        mutationFn: async (data: ListingRequest) => {
            const response = await createListing(data);
            await uploadListingImages(response.id, data.carImages);
        },
        onSuccess: () => { navigate("/profile"); }
    })

    const onSubmit = (formData: CreateListingForm) => {
        submitListing({
            sellerId: TEMP_USER_ID,
            status: "active",
            price: formData.price,
            carImages: images,
            car: {
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
            }
        });
    }

    const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files) {
            setImages(Array.from(e.target.files));
        }
    };
    return (
        <div className="max-w-3xl mx-auto px-6 py-10">

            {/* Header */}
            <div className="mb-8 pb-6 border-b border-gray-700">
                <p className="text-2xl font-semibold text-white mb-1">Add a listing</p>
                <p className="text-sm text-gray-400">Fill in the details about your car to create a new listing.</p>
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

                {/* Images */}
                <div>
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Images</p>
                    <label className="block border-2 border-dashed border-gray-700 rounded-xl p-10 text-center cursor-pointer hover:border-gray-500 transition-colors">
                        <p className="text-sm text-white mb-1">Drop images here or click to upload</p>
                        <p className="text-xs text-gray-400">PNG, JPG up to 10MB each</p>
                        <input
                            type="file"
                            multiple
                            accept="image/*"
                            className="hidden"
                            onChange={handleImageUpload}
                        />
                    </label>
                    {images.length > 0 && (
                        <div className="mt-3 flex flex-wrap gap-2">
                            {images.map((img, i) => (
                                <span key={i} className="text-xs text-gray-400 bg-gray-800 px-3 py-1 rounded-full">
                                    {img.name}
                                </span>
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
                        {isPending ? "Creating..." : "Create listing"}
                    </button>
                </div>

            </div>
        </div>
    )
}

export default CreateListingPage;