# EPAM_Ext_Lab_Q2_2018_Vyacheslav_Semenov

Решение Final Task/SocNetwork содержит выполненную финальную работу.
Для запуска необходимо:
1. Запустить SocNetwork.Db проект, указав в свойствах проекта строку подключения к вашему SQL Server.
2. В проекте SocNetwork.DAL изменить строку подключения к БД на вашу. Строка содержится в ConnectionStrings/ConnectionString.cs
3. Запустить SocNetwork.MVC

Данные для авторизации:
Администратор: E-mail: admin@kek.ru Pass: admin
Модератор: E-mail: moder@kek.ru Pass: moder
Пользователь: E-mail: user@kek.ru Pass: user

Возможности:
- Регистрация пользователей
- Публикация сообщений на "стене" пользователя
- Редактирование сообщений
- Редактирование информации о пользователе
- Система друзей (друзья, входящие и исходящие заявки)
- Модерация сообщений модераторами и администраторами
- Управление пользователями администраторами

Не реализовано, но оставлены заглушки для:
- системы личных сообщений;
- страницы восстановления пароля.