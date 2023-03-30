# User management web app

.NET REST API using SQL database and Azure Blob Storage with Angular frontend

### Features:
Dodanie użytkownika do bazy danych (id, adres email, imię, nazwisko, wiek, obrazek) 
- add user to database with image
- edit user
- user image saved in Azure Blob Storage and uri is saved in database
- get user by id with included SAS token to get image from private container
- frontend app uses store
