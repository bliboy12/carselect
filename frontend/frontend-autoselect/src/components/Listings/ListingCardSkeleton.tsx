const ListingCardSkeleton = () => {
    return (
        <div className="bg-gray-900 border border-gray-800 rounded-2xl overflow-hidden duration-200 cursor-pointer animate-pulse">
            <div className="relative aspect-4/3 bg-gray-800 overflow-hidden" />

            <div className="p-5 flex flex-col gap-4">
                <div className="flex flex-col gap-1">
                    <div className="flex justify-between items-start">
                        <div className="flex flex-col gap-3 w-3/4">
                            <div className="bg-gray-700 h-6 rounded-md" />
                            <div className="bg-gray-700 h-4 w-2/3 rounded-md" />
                        </div>
                        <div className="bg-gray-700 h-7 w-2/12 rounded-md" />
                    </div>
                </div>
                <div className="flex gap-2 flex-wrap">
                    <div className="bg-gray-700 h-8 w-20 rounded-full" />
                    <div className="bg-gray-700 h-8 w-20 rounded-full" />
                    <div className="bg-gray-700 h-8 w-20 rounded-full" />
                    <div className="bg-gray-700 h-8 w-20 rounded-full" />
                    <div className="bg-gray-700 h-8 w-20 rounded-full" />
                    <div className="bg-gray-700 h-8 w-20 rounded-full" />
                </div>
            </div>
        </div>
    )
}

export default ListingCardSkeleton;