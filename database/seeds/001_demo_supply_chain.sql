
BEGIN;

-- Demo tenant
INSERT INTO tenants (id, name)
VALUES ('11111111-1111-4111-8111-111111111111', 'Demo Supply Co');

-- Warehouse
INSERT INTO warehouses (id, tenant_id, code, name)
VALUES (
    '22222222-2222-4222-8222-222222222222',
    '11111111-1111-4111-8111-111111111111',
    'WH-DEL-01',
    'Delhi Main Warehouse'
);

-- Product
INSERT INTO products (id, tenant_id, sku, name)
VALUES (
    '33333333-3333-4333-8333-333333333333',
    '11111111-1111-4111-8111-111111111111',
    'SKU-10023',
    'Demo Industrial Component'
);

-- Inventory: 10 units on hand, 2 reserved, 8 available
INSERT INTO inventory_balances (
    tenant_id, warehouse_id, product_id,
    on_hand_quantity, reserved_quantity
)
VALUES (
    '11111111-1111-4111-8111-111111111111',
    '22222222-2222-4222-8222-222222222222',
    '33333333-3333-4333-8333-333333333333',
    10, 2
);

-- Order requiring 12 units: exceeds the 8 available units
INSERT INTO orders (id, tenant_id, order_number, status)
VALUES (
    '44444444-4444-4444-8444-444444444444',
    '11111111-1111-4111-8111-111111111111',
    'ORD-10001',
    'Confirmed'
);

INSERT INTO order_lines (
    id, tenant_id, order_id, product_id, ordered_quantity
)
VALUES (
    '55555555-5555-4555-8555-555555555555',
    '11111111-1111-4111-8111-111111111111',
    '44444444-4444-4444-8444-444444444444',
    '33333333-3333-4333-8333-333333333333',
    12
);

COMMIT;
