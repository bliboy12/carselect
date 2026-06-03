import { axiosApi } from "../api/axiosInstances";

const useExportListings = () => {

    const exportToPdf = async () => {
        try {
            const response = await axiosApi.get('/listings/export/pdf', {
                responseType: 'blob'
            });

            const url = window.URL.createObjectURL(
                new Blob([response.data], { type: 'application/pdf' })
            );
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', `listings_${new Date().toISOString().split('T')[0]}.pdf`);
            document.body.appendChild(link);
            link.click();
            link.remove();
            window.URL.revokeObjectURL(url);
        }
        catch (error) {
            console.error('Export failed', error);
        }
    };

    return { exportToPdf };
};

export default useExportListings;