USE ProductList
GO

CREATE PROCEDURE sp_GetAllSale
@Product nvarchar(50) = NULL,
@PageSize int = NULL,
@PageNumber int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	WITH CTESales AS (
    SELECT *
    FROM Sale
	WHERE
		(@Product IS NULL OR Product LIKE '%' + @Product + '%')
	)
	
	SELECT * FROM CTESales
	ORDER BY Product
	OFFSET 
		CASE 
            WHEN @PageNumber IS NULL OR @PageSize IS NULL THEN 0
            ELSE (@PageNumber - 1) * @PageSize
        END
	ROWS
    FETCH NEXT 
		CASE 
            WHEN @PageNumber IS NULL OR @PageSize IS NULL THEN 1000000
            ELSE @PageSize
        END 
	ROWS ONLY;
END;