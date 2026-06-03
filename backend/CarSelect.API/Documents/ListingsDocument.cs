using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class ListingsDocument : IDocument
{
    public IEnumerable<ListingModel> Listings { get; set; } = [];
    public DateTime ExportedAt { get; set; }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4.Landscape());
            page.Margin(24);

            page.Header().Column(header =>
            {
                header.Item().Text("CarSelect — Listings Export")
                    .FontSize(18).Bold().FontColor("#1d4ed8");
                header.Item().Text($"Exported on {ExportedAt:dd MMM yyyy}")
                    .FontSize(10).FontColor("#6b7280");
                header.Item().PaddingTop(8).LineHorizontal(1).LineColor("#e5e7eb");
            });

            page.Content().PaddingTop(16).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2); // Brand
                    columns.RelativeColumn(2); // Model
                    columns.RelativeColumn(2); // Trim
                    columns.RelativeColumn(1); // Year
                    columns.RelativeColumn(2); // Kilometers
                    columns.RelativeColumn(1); // Fuel
                    columns.RelativeColumn(2); // Transmission
                    columns.RelativeColumn(1); // Drive
                    columns.RelativeColumn(2); // Price
                    columns.RelativeColumn(1); // Status
                });

                // Header row
                static IContainer HeaderCell(IContainer container) =>
                    container.Background("#1e3a5f").Padding(6);

                static void HeaderText(IContainer container, string text) =>
                    container.Text(text).FontSize(9).Bold().FontColor("#ffffff");

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Brand"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Model"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Trim"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Year"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Kilometers"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Fuel"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Transmission"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Drive"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Price"));
                    header.Cell().Element(HeaderCell).Element(c => HeaderText(c, "Status"));
                });

                // Data rows
                var isEven = false;
                foreach (var listing in Listings)
                {
                    var bgColor = isEven ? "#f9fafb" : "#ffffff";
                    isEven = !isEven;

                    static IContainer DataCell(IContainer container, string bg) =>
                        container.Background(bg).Padding(6);

                    void DataText(IContainer container, string text) =>
                        container.Text(text).FontSize(8).FontColor("#111827");

                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Car.Brand));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Car.Model));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Car.Trim));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Car.BuildYear.ToString()));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, $"{listing.Car.Kilometers:N0} km"));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Car.Fuel.ToString()));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Car.Transmission.ToString()));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Car.Drive.ToString()));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, $"€{listing.Price:N0}"));
                    table.Cell().Element(c => DataCell(c, bgColor)).Element(c => DataText(c, listing.Status.ToString()));
                }
            });

            page.Footer().AlignCenter().Text(text =>
            {
                text.Span("Page ").FontSize(9).FontColor("#6b7280");
                text.CurrentPageNumber().FontSize(9).FontColor("#6b7280");
                text.Span(" of ").FontSize(9).FontColor("#6b7280");
                text.TotalPages().FontSize(9).FontColor("#6b7280");
            });
        });
    }
}