-- SQL Scripts to create the Cart tables

-- Create Cart Table
CREATE TABLE Cart (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClientId UNIQUEIDENTIFIER NOT NULL
    -- Optionally, you can add a FOREIGN KEY constraint to the client table:
    -- CONSTRAINT FK_Cart_Client FOREIGN KEY (ClientId) REFERENCES client(Id)
);

-- Create Cart Line Table
CREATE TABLE CartLinea (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CartId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    Quantitat INT NOT NULL,
    
    -- Link Cart Line to Cart
    CONSTRAINT FK_CartLinea_Cart FOREIGN KEY (CartId) REFERENCES Cart(Id) ON DELETE CASCADE,
    
    -- Assuming your product table is named 'producte' based on your diagram
    -- CONSTRAINT FK_CartLinea_Producte FOREIGN KEY (ProductId) REFERENCES producte(id)
);
