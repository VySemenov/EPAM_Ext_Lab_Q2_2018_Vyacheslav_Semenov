USE Northwind;
GO

/* ������� 1.1
������� � ������� Orders ������, ������� ���� ���������� 
����� 6 ��� 1998 ���� (������� ShippedDate) ������������ � ������� ���������� � ShipVia >= 2. 
������ �������� ���� ������ ���� ������ ��� ����� ������������ ����������, 
�������� ����������� ������ �Writing International Transact-SQL Statements� � Books Online 
������ �Accessing and Changing Relational Data Overview�. 
���� ����� ������������ ����� ��� ���� �������. 
������ ������ ����������� ������ ������� OrderID, ShippedDate � ShipVia. 
�������� ������ ���� �� ������ ������ � NULL-�� � ������� ShippedDate. 
*/
SELECT OrderId, ShippedDate, ShipVia
FROM Northwind.Orders
WHERE (ShippedDate >= CONVERT(DATETIME, '1998.05.06', 102)) AND (ShipVia >= 2)

/* ����� �� ������: ��������� � NULL ���������� UNKNOWN ��������,
� �������� WHERE �������� ������ �� ������, ��� ������� ������� TRUE*/

/* ������� 1.2
�������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. 
� ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL 
������ �Not Shipped� � ������������ ��������� ������� CAS�. 
������ ������ ����������� ������ ������� OrderID � ShippedDate.
*/
SELECT OrderID, ShippedDate =
	CASE
	WHEN ShippedDate IS NULL THEN 'Not shipped'
	END
FROM Northwind.Orders
WHERE ShippedDate IS NULL

/* ������� 1.3
������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate) 
�� ������� ��� ���� ��� ������� ��� �� ����������. 
� ������� ������ ������������� ������ ������� OrderID (������������� � Order Number) 
� ShippedDate (������������� � Shipped Date). 
� ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�, 
��� ��������� �������� ����������� ���� � ������� �� ���������.
*/
SELECT OrderId as [Order Number], [Shipped Date] =
	CASE
	WHEN ShippedDate IS NULL THEN 'Not shipped'
	END
FROM Northwind.Orders
WHERE (ShippedDate IS NULL) OR (ShippedDate > CONVERT(DATETIME, '1998.05.06', 102))


/* ������� 2.1
������� �� ������� Customers ���� ����������, ����������� � USA � Canada. 
������ ������� � ������ ������� ��������� IN. 
����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
����������� ���������� ������� �� ����� ���������� � �� ����� ����������.
*/
SELECT ContactName, Country
FROM Northwind.Customers
WHERE Country IN ('USA','Canada')
ORDER BY ContactName, Country

/* ������� 2.2
������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada. 
������ ������� � ������� ��������� IN. 
����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
����������� ���������� ������� �� ����� ����������.
*/
SELECT ContactName, Country
FROM Northwind.Customers
WHERE Country NOT IN ('USA','Canada')
ORDER BY ContactName

/* ������� 2.3
������� �� ������� Customers ��� ������, � ������� ��������� ���������. 
������ ������ ���� ��������� ������ ���� ��� � ������ ������������ �� ��������. 
�� ������������ ����������� GROUP BY. ����������� ������ ���� ������� � ����������� �������. 
*/
SELECT DISTINCT Country
FROM Northwind.Customers
ORDER BY Country DESC


/* ������� 3.1
������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������), 
��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ��� ������� Quantity � ������� Order Details. 
������������ �������� BETWEEN. ������ ������ ����������� ������ ������� OrderID.
*/
SELECT DISTINCT OrderID
FROM Northwind.[Order Details]
WHERE Quantity BETWEEN 3 AND 10 

/* ������� 3.2
������� ���� ���������� �� ������� Customers, � ������� �������� ������ 
���������� �� ����� �� ��������� b � g. ������������ �������� BETWEEN. 
���������, ��� � ���������� ������� �������� Germany. 
������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.
*/

SELECT CustomerID, Country
FROM Northwind.Customers
WHERE SUBSTRING(Country, 1, 1) BETWEEN 'b' AND 'g'
ORDER BY Country
--Sort(ORDER BY:([Northwind].[Northwind].[Customers].[Country] ASC))
--Clustered Index Scan(OBJECT:([Northwind].[Northwind].[Customers].[PK_Customers]), WHERE:(substring([Northwind].[Northwind].[Customers].[Country],(1),(1))>=N'b' AND substring([Northwind].[Northwind].[Customers].[Country],(1),(1))<=N'g'))

/* ������� 3.3
������� ���� ���������� �� ������� Customers, � ������� �������� ������ 
���������� �� ����� �� ��������� b � g, �� ��������� �������� BETWEEN. 
� ������� ����� �Execution Plan� ���������� ����� 
������ ���������������� 3.2 ��� 3.3 � ��� ����� ���� ������ � ������ ���������� 
���������� Execution Plan-a ��� ���� ���� ��������, 
���������� ���������� Execution Plan ���� ������ � ������ � ���� ����������� � 
�� �� ����������� ���� ����� �� ������ � �� ������ ��������� ���� ��������� ���������. 
������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.
*/
SELECT CustomerID, Country
FROM Northwind.Customers
WHERE Country LIKE '[b-g]%'
ORDER BY Country
--Sort(ORDER BY:([Northwind].[Northwind].[Customers].[Country] ASC))
--Clustered Index Scan(OBJECT:([Northwind].[Northwind].[Customers].[PK_Customers]), WHERE:([Northwind].[Northwind].[Customers].[Country] like N'[b-g]%'))

/* ����� ���������������� �������� ������� 3.3.
� ������ 3.2 ����� ������ ���������� ����� SUBSTRING � ���������� �������� �������.
����� �������� ��������������� ����� ���������� ������ 3.3 ����� �������������� ���������� ����� 39,
� �� ����� ��� 3.2 ����� �������������� ���������� ����� 43,7.
�������� �� ���, ������ �� �������� ����������� ���������� ���������� �������.*/

/* ������� 4.1
� ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'. 
��������, ��� � ��������� 'chocolade' ����� ���� �������� ���� ����� 'c' � �������� - ����� ��� ��������, 
������� ������������� ����� �������. 
���������: ���������� ������� ������ ����������� 2 ������.
*/
SELECT *
FROM Northwind.Products
WHERE ProductName LIKE '%cho_olade%'


/* ������� 5.1
����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� ����������� ������� � ������ �� ���. 
��������� ��������� �� ����� � ��������� � ����� 1 ��� ���� ������ money.  
������ (������� Discount) ���������� ������� �� ��������� ��� ������� ������. 
��� ����������� �������������� ���� �� ��������� ������� ���� ������� ������ �� ��������� � ������� UnitPrice ����. 
����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.
*/
SELECT CONVERT(money, ROUND(SUM((UnitPrice - UnitPrice*Discount)*Quantity), 2), 1) as [Totals]
FROM Northwind.[Order Details]

/* ������� 5.2
�� ������� Orders ����� ���������� �������, ������� ��� �� ���� 
���������� (�.�. � ������� ShippedDate ��� �������� ���� ��������). 
������������ ��� ���� ������� ������ �������� COUNT. 
�� ������������ ����������� WHERE � GROUP.
*/
SELECT COUNT(*) - COUNT(ShippedDate) as [Not shipped]
FROM Northwind.Orders

/* ������� 5.3
�� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� ������. 
������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.
*/
SELECT COUNT(*) as [Customers]
FROM (SELECT DISTINCT CustomerId FROM Northwind.Orders) as s



/* ������� 6.1 
�� ������� Orders ����� ���������� ������� � ������������ �� �����. 
� ����������� ������� ���� ����������� ��� ������� c ���������� Year � Total. 
�������� ����������� ������, ������� ��������� ���������� ���� �������.
*/
SELECT [Year] = DATEPART(year, OrderDate), [Count] = COUNT(*)
FROM Northwind.Orders
GROUP BY DATEPART(year, OrderDate)

SELECT COUNT(*) as Total
FROM Northwind.Orders

/* ������� 6.2
6.2	�� ������� Orders ����� ���������� �������, c�������� ������ ���������. 
����� ��� ���������� �������� � ��� ����� ������ � ������� Orders, ��� � ������� 
EmployeeID ������ �������� ��� ������� ��������. 
� ����������� ������� ���� ����������� ������� � ������ 
�������� (������ ������������� ��� ���������� ������������� LastName & FirstName. 
��� ������ LastName & FirstName ������ ���� �������� ��������� �������� � ������� ��������� �������. 
����� �������� ������ ������ ������������ ����������� �� EmployeeID.) � ��������� ������� �Seller� � 
������� c ����������� ������� ����������� � ��������� 'Amount'. 
���������� ������� ������ ���� ����������� �� �������� ���������� �������. 
*/
SELECT EmployeeID as Seller,
		(SELECT CONCAT(FirstName, LastName) 
		FROM Northwind.Employees as emp 
		WHERE ord.EmployeeID = emp.EmployeeID) as [Name], 
		COUNT(*) as [Amount]
FROM Northwind.Orders as ord
GROUP BY EmployeeID
ORDER BY Amount DESC

/*������� 6.3
�� ������� Orders ����� ���������� �������, c�������� ������ ��������� � ��� ������� ����������. 
���������� ���������� ��� ������ ��� ������� ��������� � 1998 ����. 
� ����������� ������� ���� ����������� ������� � ������ �������� (�������� ������� �Seller�), 
������� � ������ ���������� (�������� ������� �Customer�)  � ������� c ����������� 
������� ����������� � ��������� 'Amount'. 
� ������� ���������� ������������ ����������� �������� 
����� T-SQL ��� ������ � ���������� GROUP (���� �� �������� ������� �������� ������ �ALL� � ����������� �������). 
����������� ������ ���� ������� �� ID �������� � ����������. 
���������� ������� ������ ���� ����������� �� ��������, ���������� � �� �������� ���������� ������. 
� ����������� ������ ���� ������� ���������� �� ��������. 
�.�. � ������������� ������ ������ �������������� ������������� � ���������� � �������� 
�������� ��� ������� ���������� ��������� �������:
Seller		Customer	Amount
ALL 		ALL		<����� ����� ������>
<���>		ALL		<����� ������ ��� ������� ��������>
ALL		   <���>    <����� ������ ��� ������� ����������>
<���>	   <���>	<����� ������ ������� �������� ��� �������� ����������>
*/
SELECT ISNULL((SELECT CONCAT(FirstName, LastName) 
		FROM Northwind.Employees as emp 
		WHERE ord.EmployeeID = emp.EmployeeID), 'ALL') as Seller,
		ISNULL((SELECT ContactName 
		FROM Northwind.Customers as cust 
		WHERE ord.CustomerID = cust.CustomerID), 'ALL') as Customer, 
		COUNT(*) AS Amount
FROM Northwind.Orders as ord
WHERE DATEPART(year, OrderDate) = '1998'
GROUP BY cube (EmployeeID, CustomerID)
ORDER BY Seller, Customer, Amount DESC

/* ������� 6.4
����� ����������� � ���������, ������� ����� � ����� ������. 
���� � ������ ����� ������ ���� ��� ��������� ��������� ��� ������ ���� ��� ��������� �����������, 
�� ���������� � ����� ���������� � ��������� �� ������ �������� � �������������� �����. 
�� ������������ ����������� JOIN. 
� ����������� ������� ���������� ������� ��������� ��������� ��� ����������� �������: 
�Person�, �Type� (����� ���� �������� ������ �Customer� ���  �Seller� � ��������� �� ���� ������), �City�. 
������������� ���������� ������� �� ������� �City� � �� �Person�.
*/
SELECT cust.ContactName AS [Person], 'Customer' AS [Type], cust.City AS [City]
FROM Northwind.Customers AS cust
WHERE EXISTS (SELECT emp.City 
              FROM Northwind.Employees AS emp
              WHERE emp.City = cust.City)
UNION ALL
SELECT emp.FirstName + ' ' + emp.LastName AS [Person], 'Seller' AS [Type], emp.City AS [City]
FROM Northwind.Employees AS emp
WHERE EXISTS (SELECT cust.City 
              FROM Northwind.Customers AS cust
              WHERE emp.City = cust.City)
ORDER BY City, Person

/* ������� 6.5 
����� ���� �����������, ������� ����� � ����� ������. 
� ������� ������������ ���������� ������� Customers c ����� - ��������������. 
��������� ������� CustomerID � City. ������ �� ������ ����������� ����������� ������. 
��� �������� �������� ������, ������� ����������� ������, ������� ����������� ����� ������ ���� � ������� Customers. 
��� �������� ��������� ������������ �������.
*/
SELECT DISTINCT a.CustomerID, a.City
FROM Northwind.Customers AS a JOIN Northwind.Customers AS b
ON a.City = b.City
GROUP BY a.City, a.CustomerID
HAVING COUNT(*) > 1

SELECT City, COUNT(*) AS [Count]
FROM Northwind.Customers
GROUP BY City
HAVING COUNT(*) > 1

/* ������� 6.6
�� ������� Employees ����� ��� ������� �������� ��� ������������, �.�. ���� �� ������ �������. 
��������� ������� � ������� 'User Name' (LastName) � 'Boss'. 
� �������� ������ ���� ��������� ����� �� ������� LastName. ��������� �� ��� �������� � ���� �������?
*/
SELECT a.LastName AS [User name], b.LastName AS [Boss]
FROM Northwind.Employees AS a JOIN Northwind.Employees AS b
ON a.ReportsTo = b.EmployeeID



/* ������� 7.1
���������� ���������, ������� ����������� ������ 'Western' (������� Region). 
���������� ������� ������ ����������� ��� ����: 'LastName' �������� � �������� 
������������� ���������� ('TerritoryDescription' �� ������� Territories). 
������ ������ ������������ JOIN � ����������� FROM. 
��� ����������� ������ ����� ��������� Employees � Territories ���� ������������ ����������� ��������� ��� ���� Northwind.
*/
SELECT emp.LastName, t.TerritoryDescription
FROM 
	Northwind.EmployeeTerritories AS empt 
	JOIN Northwind.Territories AS t 
	ON empt.TerritoryID = t.TerritoryID 
	JOIN Northwind.Region AS r 
	ON t.RegionID = r.RegionID
	JOIN Northwind.Employees AS emp
	ON emp.EmployeeID = empt.EmployeeID
WHERE r.RegionDescription = 'Western'


/* ������� 8.1
��������� � ����������� ������� ����� ���� ���������� �� ������� Customers � 
��������� ���������� �� ������� �� ������� Orders. 
������� �� ��������, ��� � ��������� ���������� ��� �������, �� ��� ����� ������ ���� �������� � ����������� �������. 
����������� ���������� ������� �� ����������� ���������� �������.
*/

SELECT cust.ContactName, COUNT(ord.OrderID) AS [Sum]
FROM Northwind.Customers AS cust
	LEFT JOIN Northwind.Orders AS ord
	ON cust.CustomerID = ord.CustomerID
GROUP BY cust.CustomerID, cust.ContactName
ORDER BY Sum



/* ������� 9.1 
��������� ���� ����������� ������� CompanyName � ������� Suppliers, � ������� 
��� ���� �� ������ �������� �� ������ (UnitsInStock � ������� Products ����� 0). 
������������ ��������� SELECT ��� ����� ������� � �������������� ��������� IN. 
����� �� ������������ ������ ��������� IN �������� '=' ?
*/

SELECT CompanyName
FROM Northwind.Suppliers
WHERE SupplierID 
	IN(SELECT SupplierID
	FROM Northwind.Products
	WHERE UnitsInStock = 0)
/* ����� �� ������: ���, �.�. �������� '=' �������� ������ � ��� ������,
����� ������� ������ �������� �������� (�.�. �� �������� ����������).
�������� IN ���������, ������ �� ������� � ����������� ��������� */

/* ������� 10.1
��������� ���� ���������, ������� ����� ����� 150 �������. 
������������ ��������� ��������������� SELECT.
*/

SELECT emp.EmployeeID, emp.LastName
FROM Northwind.Employees AS emp
WHERE 150 < (SELECT COUNT(*) 
			FROM Northwind.Orders AS ord 
			WHERE ord.EmployeeID = emp.EmployeeID 
			GROUP BY ord.EmployeeID)

/*����������� ������*/
SELECT EmployeeID, COUNT(*) AS [Count]
FROM Northwind.Orders
GROUP BY EmployeeID
HAVING COUNT(*) > 150

/* ������� 11.1
��������� ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ (��������� �� ������� Orders). 
������������ ��������������� SELECT � �������� EXISTS.
*/

SELECT *
FROM Northwind.Customers AS cust
WHERE NOT EXISTS(SELECT * 
				FROM Northwind.Orders AS ord 
				WHERE ord.CustomerID = cust.CustomerID)

/* ������� 12.1
��� ������������ ����������� ��������� Employees ��������� �� ������� Employees 
������ ������ ��� ���� ��������, � ������� ���������� ������� Employees (������� LastName ) �� ���� �������. 
���������� ������ ������ ���� ������������ �� �����������.
*/

SELECT DISTINCT SUBSTRING(LastName, 1, 1) AS [Letter]
FROM Northwind.Employees
ORDER BY Letter


/* ������� 13.1 
�������� ���������, ������� ���������� ����� ������� ����� ��� ������� �� ��������� �� ������������ ���. 
� ����������� �� ����� ���� ��������� ������� ������ ��������, ������ ���� ������ ���� � ����� �������. 
� ����������� ������� ������ ���� �������� ��������� �������: ������� � ������ � �������� 
�������� (FirstName � LastName � ������: Nancy Davolio), ����� ������ � ��� ���������. 
� ������� ���� ��������� Discount ��� ������� �������. 
��������� ���������� ���, �� ������� ���� ������� �����, � ���������� ������������ �������. 
���������� ������� ������ ���� ����������� �� �������� ����� ������. 
��������� ������ ���� ����������� � �������������� ��������� SELECT � ��� ������������� ��������. 
*/
DECLARE @const_year int = 1998
DECLARE @const_num int = 5

exec [Northwind].[GreatestOrders] @year = @const_year, @num = @const_num

/* ����������� ������ */
SELECT LastName + ' ' + FirstName AS [Name], OrderID, P AS [Value]
FROM Northwind.Employees AS emp
	JOIN (SELECT ord.EmployeeID, ord.OrderID, SUM(UnitPrice*(1 - Discount)*Quantity) AS [P]
		  FROM Northwind.Orders AS ord 
			JOIN Northwind.[Order Details] AS orddet
			ON ord.OrderID = orddet.OrderID
		  WHERE DATEPART(year, ord.OrderDate) = @const_year
		  GROUP BY ord.OrderID, EmployeeID) AS s
	ON emp.EmployeeID = s.EmployeeID
ORDER BY emp.EmployeeID, P DESC



/* ������� 13.2 
�������� ���������, ������� ���������� ������ � ������� Orders, �������� ���������� ����� 
�������� � ���� (������� ����� OrderDate � ShippedDate).  
� ����������� ������ ���� ���������� ������, ���� ������� ��������� ���������� �������� ��� 
��� �������������� ������. �������� �� ��������� ��� ������������� ����� 35 ����. 
�������� ��������� ShippedOrdersDiff. 
��������� ������ ����������� ��������� �������: OrderID, OrderDate, ShippedDate, 
ShippedDelay (�������� � ���� ����� ShippedDate � OrderDate), SpecifiedDelay (���������� � ��������� ��������). 
*/

exec [Northwind].[ShippedOrdersDiff]


/* ������� 13.3 
�������� ���������, ������� ����������� ���� ����������� ��������� ��������, ��� ����������������, 
��� � ����������� ��� �����������. � �������� �������� ��������� ������� ������������ EmployeeID. 
���������� ����������� ����� ����������� � ��������� �� � ������ (������������ �������� PRINT) 
�������� �������� ����������. 
��������, ��� �������� ���� ����� ����������� ����� ������ ���� ��������. 
�������� ��������� SubordinationInfo.
*/
exec [Northwind].[SubordinationInfo] @EmployeeID = 2



/* ������� 13.4
�������� �������, ������� ����������, ���� �� � �������� �����������. 
���������� ��� ������ BIT. 
� �������� �������� ��������� ������� ������������ EmployeeID. �������� ������� IsBoss. 
������������������ ������������� ������� ��� ���� ��������� �� ������� Employees.
*/
SELECT EmployeeID, [Northwind].[IsBoss](EmployeeID) AS [IsBoss]
FROM Northwind.Employees