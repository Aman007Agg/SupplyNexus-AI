
CREATE TABLE tenants (
    id UUID PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE warehouses (
    id UUID PRIMARY KEY,
    tenant_id UUID NOT NULL REFERENCES tenants(id),
    code VARCHAR(50) NOT NULL,
    name VARCHAR(200) NOT NULL,
    UNIQUE (tenant_id, id),
    UNIQUE (tenant_id, code)
);

CREATE TABLE products (
    id UUID PRIMARY KEY,
    tenant_id UUID NOT NULL REFERENCES tenants(id),
    sku VARCHAR(100) NOT NULL,
    name VARCHAR(250) NOT NULL,
    UNIQUE (tenant_id, id),
    UNIQUE (tenant_id, sku)
);

CREATE TABLE inventory_balances (
    tenant_id UUID NOT NULL,
    warehouse_id UUID NOT NULL,
    product_id UUID NOT NULL,
    on_hand_quantity INTEGER NOT NULL DEFAULT 0
        CHECK (on_hand_quantity >= 0),
    reserved_quantity INTEGER NOT NULL DEFAULT 0
        CHECK (reserved_quantity >= 0),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (tenant_id, warehouse_id, product_id),
    FOREIGN KEY (tenant_id, warehouse_id)
        REFERENCES warehouses(tenant_id, id),
    FOREIGN KEY (tenant_id, product_id)
        REFERENCES products(tenant_id, id)
);

CREATE TABLE orders (
    id UUID PRIMARY KEY,
    tenant_id UUID NOT NULL REFERENCES tenants(id),
    order_number VARCHAR(100) NOT NULL,
    status VARCHAR(30) NOT NULL
        CHECK (status IN (
            'Pending', 'Confirmed', 'Allocated',
            'Shipped', 'Cancelled', 'Returned'
        )),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    UNIQUE (tenant_id, id),
    UNIQUE (tenant_id, order_number)
);

CREATE TABLE order_lines (
    id UUID PRIMARY KEY,
    tenant_id UUID NOT NULL,
    order_id UUID NOT NULL,
    product_id UUID NOT NULL,
    ordered_quantity INTEGER NOT NULL CHECK (ordered_quantity > 0),
    UNIQUE (tenant_id, id),
    FOREIGN KEY (tenant_id, order_id)
        REFERENCES orders(tenant_id, id),
    FOREIGN KEY (tenant_id, product_id)
        REFERENCES products(tenant_id, id)
);

CREATE TABLE inventory_reservations (
    id UUID PRIMARY KEY,
    tenant_id UUID NOT NULL,
    order_line_id UUID NOT NULL,
    warehouse_id UUID NOT NULL,
    product_id UUID NOT NULL,
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    status VARCHAR(20) NOT NULL
        CHECK (status IN ('Active', 'Released', 'Fulfilled')),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    FOREIGN KEY (tenant_id, order_line_id)
        REFERENCES order_lines(tenant_id, id),
    FOREIGN KEY (tenant_id, warehouse_id)
        REFERENCES warehouses(tenant_id, id),
    FOREIGN KEY (tenant_id, product_id)
        REFERENCES products(tenant_id, id)
);

CREATE INDEX ix_orders_tenant_created
    ON orders (tenant_id, created_at DESC);

CREATE INDEX ix_order_lines_tenant_order
    ON order_lines (tenant_id, order_id);

CREATE INDEX ix_reservations_tenant_status
    ON inventory_reservations (tenant_id, status);
