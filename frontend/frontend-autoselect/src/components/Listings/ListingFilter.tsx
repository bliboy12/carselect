import { ChevronLeft, ChevronRight } from "lucide-react";


interface ListingFilterProps {
    showFilter: boolean,
    onToggle: () => void
}

const ListingFilter = ({ showFilter, onToggle }: ListingFilterProps) => {
    const inputClass = "w-full bg-gray-800 text-white text-sm border border-gray-700 rounded-lg px-3 py-2 focus:outline-none focus:border-blue-500";
    const labelClass = "text-sm text-gray-400";

    return (
        <div className={`${showFilter ? "w-64" : "w-16"} bg-gray-900 border-r border-gray-800 sticky top-16 self-start text-white h-[calc(100vh-4rem)] overflow-y-auto no-scrollbar transition-all duration-300`}>
            <div className={`flex ${showFilter ? "justify-between" : "justify-center"} p-3`}>
                {showFilter && <h2 className="font-semibold text-white">Filters</h2>}
                <button className={`${showFilter ? "size-6" : "size-10"}`} onClick={onToggle}>
                    {showFilter ? <ChevronLeft className="size-5 text-gray-400"/> : <ChevronRight className="size-7 text-gray-400"/>}
                </button>
            </div>
            {showFilter && (
                <div className="flex flex-col gap-4 px-5 pb-5">
                    <div className="flex flex-col gap-1">
                        <label htmlFor="brand" className={labelClass}>Brand</label>
                        <select id="brand" className={inputClass}>
                            <option value="">All</option>
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="model" className={labelClass}>Model</label>
                        <select id="model" className={inputClass}>
                            <option value="">All</option>
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
                        <input id="trim" placeholder="e.g. S-Line" className={inputClass}/>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="year-from" className={labelClass}>Year From</label>
                        <input id="year-from" placeholder="e.g. 2018" className={inputClass}/>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="year-to" className={labelClass}>Year To</label>
                        <input id="year-to" placeholder="e.g. 2024" className={inputClass}/>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="fuel" className={labelClass}>Fuel</label>
                        <select id="fuel" className={inputClass}>
                            <option value="">All</option>
                            <option value="petrol">Petrol</option>
                            <option value="diesel">Diesel</option>
                            <option value="hybrid">Hybrid</option>
                            <option value="electric">Electric</option>
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="transmission" className={labelClass}>Transmission</label>
                        <select id="transmission" className={inputClass}>
                            <option value="">All</option>
                            <option value="manual">Manual</option>
                            <option value="automatic">Automatic</option>
                        </select>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="min-kilometers" className={labelClass}>Min Kilometers</label>
                        <input id="min-kilometers" placeholder="e.g. 0" className={inputClass}/>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="max-kilometers" className={labelClass}>Max Kilometers</label>
                        <input id="max-kilometers" placeholder="e.g. 150000" className={inputClass}/>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="doors" className={labelClass}>Doors</label>
                        <input id="doors" placeholder="e.g. 4" className={inputClass}/>
                    </div>
                    <div className="flex flex-col gap-1">
                        <label htmlFor="drive" className={labelClass}>Drive</label>
                        <select id="drive" className={inputClass}>
                            <option value="">All</option>
                            <option value="4wd">4WD</option>
                            <option value="awd">AWD</option>
                            <option value="fwd">FWD</option>
                            <option value="rwd">RWD</option>
                        </select>
                    </div>
                    <button className="w-full py-2 rounded-lg bg-blue-600 hover:bg-blue-700 text-white font-semibold transition-colors duration-200 cursor-pointer">Search</button>
                </div>
            )}
        </div>
    );
}

export default ListingFilter;