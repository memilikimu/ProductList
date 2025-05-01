USE ProductList
GO

CREATE PROCEDURE sp_GetCountSale
@Product nvarchar(50) = NULL

AS
BEGIN
	SET NOCOUNT ON;

    SELECT Count(*)
    FROM Sale
	WHERE (@Product IS NULL OR Product LIKE '%' + @Product + '%')

END;