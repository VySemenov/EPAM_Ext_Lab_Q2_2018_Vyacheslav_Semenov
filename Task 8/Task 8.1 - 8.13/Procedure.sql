USE [Northwind]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Задание 13.1 
Написать процедуру, которая возвращает самый крупный заказ для каждого из продавцов за определенный год. 
В результатах не может быть несколько заказов одного продавца, должен быть только один и самый крупный. 
В результатах запроса должны быть выведены следующие колонки: колонка с именем и фамилией 
продавца (FirstName и LastName – пример: Nancy Davolio), номер заказа и его стоимость. 
В запросе надо учитывать Discount при продаже товаров. 
Процедуре передается год, за который надо сделать отчет, и количество возвращаемых записей. 
Результаты запроса должны быть упорядочены по убыванию суммы заказа. 
Процедура должна быть реализована с использованием оператора SELECT и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ. 
*/

IF OBJECT_ID('[Northwind].[GreatestOrders]') IS NOT NULL
	DROP PROCEDURE [Northwind].[GreatestOrders]
GO

CREATE PROCEDURE [Northwind].[GreatestOrders]
	@year INT,
	@num INT
AS
BEGIN
	SELECT TOP(@num) LastName + ' ' + FirstName AS [Name], OrderID, Max AS [Value]
	FROM Northwind.Employees AS emp
		JOIN (SELECT s.EmployeeID, sq.OrderID, Max
			  FROM (SELECT EmployeeID, MAX(P) AS [Max]
					FROM (SELECT ord.EmployeeID, ord.OrderID, SUM(UnitPrice*(1 - Discount)*Quantity) AS [P]
						  FROM Northwind.Orders AS ord 
						  JOIN Northwind.[Order Details] AS orddet
						  ON ord.OrderID = orddet.OrderID
						  WHERE DATEPART(year, ord.OrderDate) = @year
						  GROUP BY ord.OrderID, EmployeeID) AS s
				    GROUP BY EmployeeID) AS s
			  JOIN (SELECT ord.EmployeeID, ord.OrderID, SUM(UnitPrice*(1 - Discount)*Quantity) AS [P]
					FROM Northwind.Orders AS ord 
						JOIN Northwind.[Order Details] AS orddet
						ON ord.OrderID = orddet.OrderID
					WHERE DATEPART(year, ord.OrderDate) = @year
					GROUP BY ord.OrderID, EmployeeID) AS sq
			  ON P = Max) AS sq
		ON emp.EmployeeID = sq.EmployeeID
	ORDER BY Value DESC
END
GO


/* Задание 13.2
Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку 
доставки в днях (разница между OrderDate и ShippedDate).  
В результатах должны быть возвращены заказы, срок которых превышает переданное значение или 
еще недоставленные заказы. Значению по умолчанию для передаваемого срока 35 дней. 
Название процедуры ShippedOrdersDiff. 
Процедура должна высвечивать следующие колонки: OrderID, OrderDate, ShippedDate, 
ShippedDelay (разность в днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение).  
*/
IF OBJECT_ID('[Northwind].[ShippedOrdersDiff]') IS NOT NULL
	DROP PROCEDURE [Northwind].[ShippedOrdersDiff]
GO

CREATE PROCEDURE [Northwind].[ShippedOrdersDiff]
	@days INT = 35
AS
BEGIN
	SELECT OrderID, OrderDate, ShippedDate, DATEDIFF(day, OrderDate, ShippedDate) AS [Shipped Delay], @days AS [SpecifiedDelay]
	FROM Northwind.Orders
	WHERE DATEDIFF(day, OrderDate, ShippedDate) > @days OR ShippedDate IS NULL
END
GO

/* Задание 13.3 
Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, 
так и подчиненных его подчиненных. В качестве входного параметра функции используется EmployeeID. 
Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать оператор PRINT) 
согласно иерархии подчинения. 
Продавец, для которого надо найти подчиненных также должен быть высвечен. 
Название процедуры SubordinationInfo.
*/
IF OBJECT_ID('[Northwind].[SubordinationInfo]') IS NOT NULL
	DROP PROCEDURE [Northwind].[SubordinationInfo]
GO

CREATE PROCEDURE [Northwind].[SubordinationInfo]
	@EmployeeID INT
AS
BEGIN
	DECLARE @emp nvarchar(128)
	DECLARE @bossID nvarchar(128)
	DECLARE @empID nvarchar(128)
	DECLARE @level nvarchar(128)
	DECLARE cur CURSOR FOR
	WITH Test
	AS(
		SELECT LastName + ' ' + FirstName AS [Employee], ReportsTo AS [BossID], EmployeeID, 0 AS [Level]
		FROM Northwind.Employees
		WHERE EmployeeID = @EmployeeID
	UNION ALL
		SELECT LastName + ' ' + FirstName AS [Employee], t.BossID, emp.EmployeeID, Level + 1
		FROM Test AS t, Northwind.Employees AS emp
		WHERE emp.ReportsTo = t.EmployeeID)
	SELECT * FROM Test;

	OPEN cur

	FETCH NEXT FROM cur INTO @emp, @bossID, @empID, @level;
	WHILE @@FETCH_STATUS = 0
	BEGIN   
	PRINT REPLICATE('- ', @level) + @emp 
	FETCH NEXT FROM cur INTO @emp, @bossID, @empID, @level;
	END

	CLOSE cur;
	DEALLOCATE cur;
END
GO

/* Задание 13.4 
Написать функцию, которая определяет, есть ли у продавца подчиненные. 
Возвращает тип данных BIT. 
В качестве входного параметра функции используется EmployeeID. Название функции IsBoss. 
Продемонстрировать использование функции для всех продавцов из таблицы Employees.
*/

IF OBJECT_ID('[Northwind].[IsBoss]') IS NOT NULL
	DROP FUNCTION [Northwind].[IsBoss]
GO

CREATE FUNCTION [Northwind].[IsBoss] (@EmpID INT)
RETURNS BIT
AS
BEGIN
	DECLARE @num INT
	SELECT @num =
	CASE
		WHEN COUNT(*) = 0 THEN 0
		WHEN COUNT(*) <> 0 THEN 1
	END
	FROM Northwind.Employees
	WHERE ReportsTo = @EmpID
	RETURN @num
END
GO