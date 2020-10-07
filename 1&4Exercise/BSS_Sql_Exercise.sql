--FIRST

SELECT p.*, sp.City, c.CategoryName FROM Products p
JOIN Suppliers sp
ON p.SupplierId = sp.SupplierId
JOIN Categories c
ON p.CategoryID = c.CategoryID
WHERE CategoryName = 'X' AND City = 'NY'

--SECOND

SELECT CustomerId, AVG(Price) as AveragePrice FROM (
SELECT C.CustomerId, Price FROM orderDetails od
JOIN Orders o ON o.OrderID = od.OrderID
JOIN Customers c ON c.CustomerID = o.CustomerID)
GROUP BY CustomerId
ORDER BY AveragePrice DESC

--THIRD

SELECT z.EmployeeID 
FROM Employees z 
WHERE z.EmployeeID NOT IN (SELECT x.EmployeeID
FROM orders AS x
JOIN Employees e ON e.EmployeeID = x.EmployeeID 
JOIN Customers c ON c.CustomerId = x.CustomerId 
WHERE c.Country = 'USA' 
AND (o.OrderDate >= DateAdd(month, -3, GetDate()) 
AND o.OrderDate <= GetDate())
GROUP BY x.EmployeeId)