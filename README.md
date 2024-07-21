steps to create project:

1. dotnet new webapi -n NameOfProject
2. Create Dtos record classes
3. Create Entite classes
4. Create Data Storage content and configurations.
   Add to appsettings.json:
   ```csharp
   "ConnectionStrings": {
   "BookStore": "Data Source=BookStore.db"
   }
   ```
   Add DB's configurations in the Program.cs
5. Create Mapping classes
6. Create Endpoints classes

CLI commands and libraries installed:

```csharp
dotnet new webapi -n NameOfProject

dotnet add package MinimalApis.Extensions
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add InitialCreate --output-dir Data\Migrations //create the DB migrations; You can choose any name for InitialCreate
dotnet ef database update //update the database

```
#   B o o k _ A p i  
 