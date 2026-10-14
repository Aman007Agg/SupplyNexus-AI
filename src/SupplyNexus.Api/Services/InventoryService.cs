
using Npgsql;
using SupplyNexus.Api.Models;

namespace SupplyNexus.Api.Services;

public sealed class InventoryService(NpgsqlDataSource dataSource)
{
    public async Task<IReadOnlyList<InventoryShortfallResponse>> GetShortfallsAsync(
        Guid tenantId,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT
                o.order_number,
                p.sku,
                ol.ordered_quantity,
                ib.on_hand_quantity,
                ib.reserved_quantity,
                ib.on_hand_quantity - ib.reserved_quantity AS available_quantity,
                GREATEST(
                    ol.ordered_quantity -
                    (ib.on_hand_quantity - ib.reserved_quantity),
                    0
                ) AS quantity_shortfall
            FROM orders o
            JOIN order_lines ol
                ON ol.tenant_id = o.tenant_id
               AND ol.order_id = o.id
            JOIN products p
                ON p.tenant_id = ol.tenant_id
               AND p.id = ol.product_id
            JOIN inventory_balances ib
                ON ib.tenant_id = p.tenant_id
               AND ib.product_id = p.id
            WHERE o.tenant_id = @tenantId
              AND ol.tenant_id = @tenantId
              AND p.tenant_id = @tenantId
              AND ib.tenant_id = @tenantId
              AND ol.ordered_quantity >
                  (ib.on_hand_quantity - ib.reserved_quantity)
            ORDER BY o.created_at DESC, o.order_number;
            """;

        var results = new List<InventoryShortfallResponse>();

        await using var command = dataSource.CreateCommand(sql);
        command.Parameters.AddWithValue("tenantId", tenantId);

        await using var reader =
            await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            results.Add(new InventoryShortfallResponse(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetInt32(2),
                reader.GetInt32(3),
                reader.GetInt32(4),
                reader.GetInt32(5),
                reader.GetInt32(6)));
        }

        return results;
    }
}
