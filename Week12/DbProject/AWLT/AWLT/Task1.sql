-- Write your own SQL object definition here, and it'll be included in your package.
CREATE PROCEDURE SalesLT.usp_ProductCategory
as
SELECT
    p.ProductID,
    p.Name as ProductName,
    p.ProductNumber,
    pc.Name as ProductCategory
FROM SalesLT.Product p JOIN SalesLT.ProductCategory pc
    ON p.ProductCategoryID = pc.ProductCategoryID