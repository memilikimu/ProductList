USE ProductList
GO

CREATE PROCEDURE sp_UpdateSale
	@Id INT,
    @Product NVARCHAR(50),
    @Price INT,
    @Amount INT,
	@SellPrice INT,
    @SellAmount INT
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE Sale
    SET Sale.Product = @Product,
        Price = @Price,
        Amount = @Amount,
		SellPrice = @SellPrice,
        SellAmount = @SellAmount
    WHERE Id = @Id;
END;