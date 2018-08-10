USE Northwind;
GO

/* Задание 1.1
Выбрать в таблице Orders заказы, которые были доставлены 
после 6 мая 1998 года (колонка ShippedDate) включительно и которые доставлены с ShipVia >= 2. 
Формат указания даты должен быть верным при любых региональных настройках, 
согласно требованиям статьи “Writing International Transact-SQL Statements” в Books Online 
раздел “Accessing and Changing Relational Data Overview”. 
Этот метод использовать далее для всех заданий. 
Запрос должен высвечивать только колонки OrderID, ShippedDate и ShipVia. 
Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate. 
*/
SELECT OrderId, ShippedDate, ShipVia
FROM Northwind.Orders
WHERE (ShippedDate >= CONVERT(DATETIME, '1998.05.06', 102)) AND (ShipVia >= 2)

/* Ответ на вопрос: сравнение с NULL возвращает UNKNOWN значение,
а оператор WHERE выбирает только те записи, для которых условие TRUE*/

/* Задание 1.2
Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL 
строку ‘Not Shipped’ – использовать системную функцию CASЕ. 
Запрос должен высвечивать только колонки OrderID и ShippedDate.
*/
SELECT OrderID, ShippedDate =
	CASE
	WHEN ShippedDate IS NULL THEN 'Not shipped'
	END
FROM Northwind.Orders
WHERE ShippedDate IS NULL

/* Задание 1.3
Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) 
не включая эту дату или которые еще не доставлены. 
В запросе должны высвечиваться только колонки OrderID (переименовать в Order Number) 
и ShippedDate (переименовать в Shipped Date). 
В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, 
для остальных значений высвечивать дату в формате по умолчанию.
*/
SELECT OrderId as [Order Number], [Shipped Date] =
	CASE
	WHEN ShippedDate IS NULL THEN 'Not shipped'
	END
FROM Northwind.Orders
WHERE (ShippedDate IS NULL) OR (ShippedDate > CONVERT(DATETIME, '1998.05.06', 102))


/* Задание 2.1
Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
Запрос сделать с только помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков и по месту проживания.
*/
SELECT ContactName, Country
FROM Northwind.Customers
WHERE Country IN ('USA','Canada')
ORDER BY ContactName, Country

/* Задание 2.2
Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
Запрос сделать с помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков.
*/
SELECT ContactName, Country
FROM Northwind.Customers
WHERE Country NOT IN ('USA','Canada')
ORDER BY ContactName

/* Задание 2.3
Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах запроса. 
*/
SELECT DISTINCT Country
FROM Northwind.Customers
ORDER BY Country DESC


/* Задание 3.1
Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), 
где встречаются продукты с количеством от 3 до 10 включительно – это колонка Quantity в таблице Order Details. 
Использовать оператор BETWEEN. Запрос должен высвечивать только колонку OrderID.
*/
SELECT DISTINCT OrderID
FROM Northwind.[Order Details]
WHERE Quantity BETWEEN 3 AND 10 

/* Задание 3.2
Выбрать всех заказчиков из таблицы Customers, у которых название страны 
начинается на буквы из диапазона b и g. Использовать оператор BETWEEN. 
Проверить, что в результаты запроса попадает Germany. 
Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.
*/

SELECT CustomerID, Country
FROM Northwind.Customers
WHERE SUBSTRING(Country, 1, 1) BETWEEN 'b' AND 'g'
ORDER BY Country
--Sort(ORDER BY:([Northwind].[Northwind].[Customers].[Country] ASC))
--Clustered Index Scan(OBJECT:([Northwind].[Northwind].[Customers].[PK_Customers]), WHERE:(substring([Northwind].[Northwind].[Customers].[Country],(1),(1))>=N'b' AND substring([Northwind].[Northwind].[Customers].[Country],(1),(1))<=N'g'))

/* Задание 3.3
Выбрать всех заказчиков из таблицы Customers, у которых название страны 
начинается на буквы из диапазона b и g, не используя оператор BETWEEN. 
С помощью опции “Execution Plan” определить какой 
запрос предпочтительнее 3.2 или 3.3 – для этого надо ввести в скрипт выполнение 
текстового Execution Plan-a для двух этих запросов, 
результаты выполнения Execution Plan надо ввести в скрипт в виде комментария и 
по их результатам дать ответ на вопрос – по какому параметру было проведено сравнение. 
Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.
*/
SELECT CustomerID, Country
FROM Northwind.Customers
WHERE Country LIKE '[b-g]%'
ORDER BY Country
--Sort(ORDER BY:([Northwind].[Northwind].[Customers].[Country] ASC))
--Clustered Index Scan(OBJECT:([Northwind].[Northwind].[Customers].[PK_Customers]), WHERE:([Northwind].[Northwind].[Customers].[Country] like N'[b-g]%'))

/* Более предпочтительным является вариант 3.3.
В случае 3.2 вызов дважды происходит вызов SUBSTRING и проводится проверка условия.
Также согласно предполагаемому плану выполнения запрос 3.3 имеет предполагаемое количество строк 39,
в то время как 3.2 имеет предполагаемое количество строк 43,7.
Несмотря на это, каждый из запросов выполняется одинаковое количество времени.*/

/* Задание 4.1
В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. 
Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - найти все продукты, 
которые удовлетворяют этому условию. 
Подсказка: результаты запроса должны высвечивать 2 строки.
*/
SELECT *
FROM Northwind.Products
WHERE ProductName LIKE '%cho_olade%'


/* Задание 5.1
Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним. 
Результат округлить до сотых и высветить в стиле 1 для типа данных money.  
Скидка (колонка Discount) составляет процент из стоимости для данного товара. 
Для определения действительной цены на проданный продукт надо вычесть скидку из указанной в колонке UnitPrice цены. 
Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
*/
SELECT CONVERT(money, ROUND(SUM((UnitPrice - UnitPrice*Discount)*Quantity), 2), 1) as [Totals]
FROM Northwind.[Order Details]

/* Задание 5.2
По таблице Orders найти количество заказов, которые еще не были 
доставлены (т.е. в колонке ShippedDate нет значения даты доставки). 
Использовать при этом запросе только оператор COUNT. 
Не использовать предложения WHERE и GROUP.
*/
SELECT COUNT(*) - COUNT(ShippedDate) as [Not shipped]
FROM Northwind.Orders

/* Задание 5.3
По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/
SELECT COUNT(*) as [Customers]
FROM (SELECT DISTINCT CustomerId FROM Northwind.Orders) as s



/* Задание 6.1 
По таблице Orders найти количество заказов с группировкой по годам. 
В результатах запроса надо высвечивать две колонки c названиями Year и Total. 
Написать проверочный запрос, который вычисляет количество всех заказов.
*/
SELECT [Year] = DATEPART(year, OrderDate), [Count] = COUNT(*)
FROM Northwind.Orders
GROUP BY DATEPART(year, OrderDate)

SELECT COUNT(*) as Total
FROM Northwind.Orders

/* Задание 6.2
6.2	По таблице Orders найти количество заказов, cделанных каждым продавцом. 
Заказ для указанного продавца – это любая запись в таблице Orders, где в колонке 
EmployeeID задано значение для данного продавца. 
В результатах запроса надо высвечивать колонку с именем 
продавца (Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного запроса. 
Также основной запрос должен использовать группировку по EmployeeID.) с названием колонки ‘Seller’ и 
колонку c количеством заказов высвечивать с названием 'Amount'. 
Результаты запроса должны быть упорядочены по убыванию количества заказов. 
*/
SELECT EmployeeID as Seller,
		(SELECT CONCAT(FirstName, LastName) 
		FROM Northwind.Employees as emp 
		WHERE ord.EmployeeID = emp.EmployeeID) as [Name], 
		COUNT(*) as [Amount]
FROM Northwind.Orders as ord
GROUP BY EmployeeID
ORDER BY Amount DESC

/*Задание 6.3
По таблице Orders найти количество заказов, cделанных каждым продавцом и для каждого покупателя. 
Необходимо определить это только для заказов сделанных в 1998 году. 
В результатах запроса надо высвечивать колонку с именем продавца (название колонки ‘Seller’), 
колонку с именем покупателя (название колонки ‘Customer’)  и колонку c количеством 
заказов высвечивать с названием 'Amount'. 
В запросе необходимо использовать специальный оператор 
языка T-SQL для работы с выражением GROUP (Этот же оператор поможет выводить строку “ALL” в результатах запроса). 
Группировки должны быть сделаны по ID продавца и покупателя. 
Результаты запроса должны быть упорядочены по продавцу, покупателю и по убыванию количества продаж. 
В результатах должна быть сводная информация по продажам. 
Т.е. в резульирующем наборе должны присутствовать дополнительно к информации о продажах 
продавца для каждого покупателя следующие строчки:
Seller		Customer	Amount
ALL 		ALL		<общее число продаж>
<имя>		ALL		<число продаж для данного продавца>
ALL		   <имя>    <число продаж для данного покупателя>
<имя>	   <имя>	<число продаж данного продавца для даннного покупателя>
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

/* Задание 6.4
Найти покупателей и продавцов, которые живут в одном городе. 
Если в городе живут только один или несколько продавцов или только один или несколько покупателей, 
то информация о таких покупателя и продавцах не должна попадать в результирующий набор. 
Не использовать конструкцию JOIN. 
В результатах запроса необходимо вывести следующие заголовки для результатов запроса: 
‘Person’, ‘Type’ (здесь надо выводить строку ‘Customer’ или  ‘Seller’ в завимости от типа записи), ‘City’. 
Отсортировать результаты запроса по колонке ‘City’ и по ‘Person’.
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

/* Задание 6.5 
Найти всех покупателей, которые живут в одном городе. 
В запросе использовать соединение таблицы Customers c собой - самосоединение. 
Высветить колонки CustomerID и City. Запрос не должен высвечивать дублируемые записи. 
Для проверки написать запрос, который высвечивает города, которые встречаются более одного раза в таблице Customers. 
Это позволит проверить правильность запроса.
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

/* Задание 6.6
По таблице Employees найти для каждого продавца его руководителя, т.е. кому он делает репорты. 
Высветить колонки с именами 'User Name' (LastName) и 'Boss'. 
В колонках должны быть высвечены имена из колонки LastName. Высвечены ли все продавцы в этом запросе?
*/
SELECT a.LastName AS [User name], b.LastName AS [Boss]
FROM Northwind.Employees AS a JOIN Northwind.Employees AS b
ON a.ReportsTo = b.EmployeeID



/* Задание 7.1
Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
Результаты запроса должны высвечивать два поля: 'LastName' продавца и название 
обслуживаемой территории ('TerritoryDescription' из таблицы Territories). 
Запрос должен использовать JOIN в предложении FROM. 
Для определения связей между таблицами Employees и Territories надо использовать графические диаграммы для базы Northwind.
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


/* Задание 8.1
Высветить в результатах запроса имена всех заказчиков из таблицы Customers и 
суммарное количество их заказов из таблицы Orders. 
Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса. 
Упорядочить результаты запроса по возрастанию количества заказов.
*/

SELECT cust.ContactName, COUNT(ord.OrderID) AS [Sum]
FROM Northwind.Customers AS cust
	LEFT JOIN Northwind.Orders AS ord
	ON cust.CustomerID = ord.CustomerID
GROUP BY cust.CustomerID, cust.ContactName
ORDER BY Sum



/* Задание 9.1 
Высветить всех поставщиков колонка CompanyName в таблице Suppliers, у которых 
нет хотя бы одного продукта на складе (UnitsInStock в таблице Products равно 0). 
Использовать вложенный SELECT для этого запроса с использованием оператора IN. 
Можно ли использовать вместо оператора IN оператор '=' ?
*/

SELECT CompanyName
FROM Northwind.Suppliers
WHERE SupplierID 
	IN(SELECT SupplierID
	FROM Northwind.Products
	WHERE UnitsInStock = 0)
/* Ответ на вопрос: нет, т.к. оператор '=' работает только в том случае,
когда операнд справа является скаляром (т.е. не является множеством).
Оператор IN проверяет, входит ли элемент в определённое множество */

/* Задание 10.1
Высветить всех продавцов, которые имеют более 150 заказов. 
Использовать вложенный коррелированный SELECT.
*/

SELECT emp.EmployeeID, emp.LastName
FROM Northwind.Employees AS emp
WHERE 150 < (SELECT COUNT(*) 
			FROM Northwind.Orders AS ord 
			WHERE ord.EmployeeID = emp.EmployeeID 
			GROUP BY ord.EmployeeID)

/*Проверочный запрос*/
SELECT EmployeeID, COUNT(*) AS [Count]
FROM Northwind.Orders
GROUP BY EmployeeID
HAVING COUNT(*) > 150

/* Задание 11.1
Высветить всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders). 
Использовать коррелированный SELECT и оператор EXISTS.
*/

SELECT *
FROM Northwind.Customers AS cust
WHERE NOT EXISTS(SELECT * 
				FROM Northwind.Orders AS ord 
				WHERE ord.CustomerID = cust.CustomerID)

/* Задание 12.1
Для формирования алфавитного указателя Employees высветить из таблицы Employees 
список только тех букв алфавита, с которых начинаются фамилии Employees (колонка LastName ) из этой таблицы. 
Алфавитный список должен быть отсортирован по возрастанию.
*/

SELECT DISTINCT SUBSTRING(LastName, 1, 1) AS [Letter]
FROM Northwind.Employees
ORDER BY Letter


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
DECLARE @const_year int = 1998
DECLARE @const_num int = 5

exec [Northwind].[GreatestOrders] @year = @const_year, @num = @const_num

/* Проверочный запрос */
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



/* Задание 13.2 
Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку 
доставки в днях (разница между OrderDate и ShippedDate).  
В результатах должны быть возвращены заказы, срок которых превышает переданное значение или 
еще недоставленные заказы. Значению по умолчанию для передаваемого срока 35 дней. 
Название процедуры ShippedOrdersDiff. 
Процедура должна высвечивать следующие колонки: OrderID, OrderDate, ShippedDate, 
ShippedDelay (разность в днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение). 
*/

exec [Northwind].[ShippedOrdersDiff]


/* Задание 13.3 
Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, 
так и подчиненных его подчиненных. В качестве входного параметра функции используется EmployeeID. 
Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать оператор PRINT) 
согласно иерархии подчинения. 
Продавец, для которого надо найти подчиненных также должен быть высвечен. 
Название процедуры SubordinationInfo.
*/
exec [Northwind].[SubordinationInfo] @EmployeeID = 2



/* Задание 13.4
Написать функцию, которая определяет, есть ли у продавца подчиненные. 
Возвращает тип данных BIT. 
В качестве входного параметра функции используется EmployeeID. Название функции IsBoss. 
Продемонстрировать использование функции для всех продавцов из таблицы Employees.
*/
SELECT EmployeeID, [Northwind].[IsBoss](EmployeeID) AS [IsBoss]
FROM Northwind.Employees