USE ProductList
GO

CREATE PROCEDURE sp_InsertSale
    @Product NVARCHAR(50),
    @Price INT,
    @Amount INT,
	@SellPrice INT,
    @SellAmount INT
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO Sale(Product, Price, Amount, SellPrice, SellAmount)
    VALUES (@Product, @Price, @Amount, @SellPrice, @SellAmount);
    --SELECT SCOPE_IDENTITY() AS NewId;
END;