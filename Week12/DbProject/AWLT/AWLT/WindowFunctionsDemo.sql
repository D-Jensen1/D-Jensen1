-- Write your own SQL object definition here, and it'll be included in your package.
SELECT
    SalesOrderID,
    SalesOrderDetailID,
    ROW_NUMBER() OVER (PARTITION BY SalesOrderID ORDER BY SalesOrderDetailID) AS RowNum,
    LineTotal,
    SUM(LineTotal) OVER (PARTITION BY SalesOrderID ORDER BY SalesOrderDetailID) AS RunningTotal,
    DENSE_RANK() OVER (PARTITION BY SalesOrderID ORDER BY LineTotal DESC) AS RankByLineTotal
FROM SalesLT.SalesOrderDetail

ORDER BY SalesOrderID, SalesOrderDetailID