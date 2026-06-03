import useExportListings from "../../hooks/useExportListings";

const ExportPdfButton = () => {
    const { exportToPdf } = useExportListings();

    return (
        <button onClick={exportToPdf} className="text-xs bg-blue-600 hover:bg-blue-500 text-white px-3 py-1.5 rounded-lg transition-colors">
            Export PDF
        </button>
    );
}

export default ExportPdfButton;