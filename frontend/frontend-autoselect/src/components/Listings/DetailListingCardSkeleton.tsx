const DetailListingSkeleton = () => {
  return (
    <div className="min-h-screen bg-gray-950 py-8 px-4 animate-pulse">
      <div className="max-w-6xl mx-auto">
        <div className="h-4 w-48 bg-gray-800 rounded mb-6" />
        <div className="grid grid-cols-1 lg:grid-cols-[1fr_340px] gap-6">
          <div className="flex flex-col gap-6">
            <div className="aspect-16/10 rounded-xl bg-gray-900" />
            <div className="flex gap-2">
              {[...Array(4)].map((_, i) => (
                <div key={i} className="w-20 h-14 rounded-lg bg-gray-900" />
              ))}
            </div>
            <div className="bg-gray-900 rounded-xl h-48" />
          </div>
          <div className="flex flex-col gap-4">
            <div className="h-6 w-3/4 bg-gray-800 rounded" />
            <div className="flex gap-2">
              {[...Array(3)].map((_, i) => (
                <div key={i} className="h-6 w-16 bg-gray-800 rounded-full" />
              ))}
            </div>
            <div className="bg-gray-900 rounded-xl h-28" />
            <div className="bg-gray-900 rounded-xl h-10" />
            <div className="bg-gray-900 rounded-xl h-24" />
          </div>
        </div>
      </div>
    </div>
  );
}

export default DetailListingSkeleton;