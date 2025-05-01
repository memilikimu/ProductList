USE ProductList
GO

CREATE PROCEDURE sp_GetSaleById
@Id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Sale
	Where Id = @Id
END;