import type { ListingResponse } from "../../types";

interface ExportCsvButtonProps {
    listings: ListingResponse[]
}

const ExportCsvButton = ({ listings }: ExportCsvButtonProps) => {

    const handleExport = () => {
        const headers = ["Brand", "Model", "Trim", "Year", "Kilometers", "Fuel", "Transmission", "Drive", "Price", "Status", "Seller"];

        const rows = listings.map(l => [
            l.car.brand,
            l.car.model,
            l.car.trim,
            l.car.buildYear,
            l.car.kilometers,
            l.car.fuel,
            l.car.transmission,
            l.car.drive,
            l.price,
            l.status,
            `${l.seller.firstName} ${l.seller.lastName}`
        ]);

        const csvContent = [
            headers.join(","),
            ...rows.map(row => row.join(","))
        ].join("\n");

        const blob = new Blob([csvContent], { type: "text/csv;charset=utf-8;" });
        const url = URL.createObjectURL(blob);
        const link = document.createElement("a");
        link.href = url;
        link.download = `listings_${new Date().toISOString().split("T")[0]}.csv`;
        link.click();
        URL.revokeObjectURL(url);
    };

    return (
        <button
            onClick={handleExport}
            className="text-xs bg-blue-600 hover:bg-blue-500 text-white px-3 py-1.5 rounded-lg transition-colors"
        >
            Export CSV
        </button>
    );
}

export default ExportCsvButton;