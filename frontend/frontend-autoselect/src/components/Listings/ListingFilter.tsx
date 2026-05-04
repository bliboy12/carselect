import { ChevronLeft, ChevronRight } from "lucide-react";
import { useState } from "react";
import type { CarFilter, CarInfo } from "../../types";
import { useQuery } from "@tanstack/react-query";
import { axiosCars } from "../../api/CarSelectApi";
import { CarDummy } from "../../dummy/CarDummy";


interface ListingFilterProps {
    showFilter: boolean,
    onToggle: () => void,
    filter: CarFilter,
    setFilter: React.Dispatch<React.SetStateAction<CarFilter>>,
    setOnSubmit: React.Dispatch<React.SetStateAction<CarFilter>>,
    cars: CarInfo[]
}



const ListingFilter = ({ showFilter, onToggle, filter, setFilter, setOnSubmit, cars }: ListingFilterProps) => {
    const inputClass = "w-full bg-gray-800 text-white text-sm border border-gray-700 rounded-lg px-3 py-2 focus:outline-none focus:border-blue-500";
    const labelClass = "text-sm text-gray-400";

    // const [searchBrand, setSearchBrand] = useState("");
    // const [searchModel, setSearchModel] = useState("");


    // const handleSubmit = () => {

    // }

    // const [submitQuery, setSubmitQuery] = useState<CarFilter>();

    // const filteredCars = () => {

    // }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { id, value } = e.target;

        setFilter((prev) => ({
            ...prev,
            [id]: value
        }));
    };


    const brands = [...new Set(cars?.map((c) => c.brand))];
    const filteredModels = [...new Set(cars?.filter((c) => c.brand === filter.brand).map((c) => c.model))];

    return (
        <div className={`${showFilter ? "w-64" : "w-16"} bg-gray-900 border-r border-gray-800 sticky top-16 self-start text-white h-[calc(100vh-4rem)] overflow-y-auto no-scrollbar transition-all duration-300`}>
            <div className={`flex ${showFilter ? "justify-between" : "justify-center"} p-3`}>
                {showFilter && <h2 className="font-semibold text-white">Filters</h2>}
                <button className={`${showFilter ? "size-6" : "size-10"}`} onClick={onToggle}>
                    {showFilter ? <ChevronLeft className="size-5 text-gray-400" /> : <ChevronRight className="size-7 text-gray-400" />}
                </button>
            </div>
            {showFilter && (
                <div className="flex flex-col gap-4 px-5 pb-5">
                    <div className="flex flex-col gap-1">
                        <label htmlFor="brand" className={labelClass}>Brand</label>
                        <select id="brand" value={filter.brand} onChange={handleChange} className={`${inputClass}`}>
                            <option value="All" >All</option>
                            {brands?.map((c) => <option key={c} value={c}>{c}</option>)}
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="model" className={labelClass}>Model</label>
                        <select id="model" className={`${inputClass} ${filter.brand === "All" || filter.brand === "" ? "opacity-50 cursor-not-allowed" : ""}`} disabled={filter.brand === "All" || filter.brand === ""} onChange={handleChange} value={filter.model}>
                            <option value="">All</option>
                            {filteredModels.map((m) => (<option key={m} value={m}>{m}</option>))}
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="color" className={labelClass}>Color</label>
                        <select id="color" className={inputClass}>
                            <option value="">All</option>
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="trim" className={labelClass}>Trim</label>
                        <input id="trim" value={filter.trim} onChange={handleChange} placeholder="e.g. S-Line" className={inputClass} />
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="yearFrom" className={labelClass}>Year From</label>
                        <input id="yearFrom" value={filter.yearFrom} onChange={handleChange} placeholder="e.g. 2018" className={inputClass} />
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="yearTo" className={labelClass}>Year To</label>
                        <input id="yearTo" value={filter.yearTo} onChange={handleChange} placeholder="e.g. 2024" className={inputClass} />
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="fuel" className={labelClass}>Fuel</label>
                        <select id="fuel" value={filter.fuel} onChange={handleChange} className={inputClass}>
                            <option value="all">All</option>
                            <option value="petrol">Petrol</option>
                            <option value="diesel">Diesel</option>
                            <option value="hybrid">Hybrid</option>
                            <option value="electric">Electric</option>
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="transmission" className={labelClass}>Transmission</label>
                        <select id="transmission" value={filter.transmission} onChange={handleChange} className={inputClass}>
                            <option value="all">All</option>
                            <option value="manual">Manual</option>
                            <option value="automatic">Automatic</option>
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="minKilometers" className={labelClass}>Min Kilometers</label>
                        <input id="minKilometers" value={filter.minKilometers} onChange={handleChange} placeholder="e.g. 0" className={inputClass} />
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="maxKilometers" className={labelClass}>Max Kilometers</label>
                        <input id="maxKilometers" value={filter.maxKilometers} onChange={handleChange} placeholder="e.g. 150000" className={inputClass} />
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="doors" className={labelClass}>Doors</label>
                        <input id="doors" value={filter.doors} onChange={handleChange} placeholder="e.g. 4" className={inputClass} />
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="drive" className={labelClass}>Drive</label>
                        <select id="drive" value={filter.drive} onChange={handleChange} className={inputClass}>
                            <option value="all">All</option>
                            <option value="4wd">4WD</option>
                            <option value="awd">AWD</option>
                            <option value="fwd">FWD</option>
                            <option value="rwd">RWD</option>
                        </select>
                    </div>
                    <button className="w-full py-2 rounded-lg bg-blue-600 hover:bg-blue-700 text-white font-semibold transition-colors duration-200 cursor-pointer" onClick={() => { setOnSubmit(filter) }}>Search</button>
                </div>
            )}
        </div>
    );
}

export default ListingFilter;