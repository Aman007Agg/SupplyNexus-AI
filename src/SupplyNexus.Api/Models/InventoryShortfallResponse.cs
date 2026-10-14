
namespace SupplyNexus.Api.Models;

public sealed record InventoryShortfallResponse(
    string OrderNumber,
    string Sku,
    int OrderedQuantity,
    int OnHandQuantity,
    int ReservedQuantity,
    int AvailableQuantity,
    int QuantityShortfall);
