# Book API

A robust RESTful API for managing a collection of books. Built with ASP.NET Core and Entity Framework Core, this API allows for CRUD operations and integrates with a SQLite database.

## Features

- **CRUD Operations**: Create, Read, Update, Delete books and categories.
- **Entity Framework Core**: Utilizes EF Core for database management.
- **SQLite**: Simple and efficient database setup with SQLite.
- **AutoMapper**: Maps entities to DTOs for clean and efficient data transfer.
- **Validation**: Ensures data integrity and validity.

## Technologies Used

- **ASP.NET Core**: Web framework for building APIs.
- **Entity Framework Core**: ORM for database operations.
- **SQLite**: Lightweight database solution.
- **AutoMapper**: Object-object mapper.
- **Dependency Injection**: Inversion of control for better manageability.

## Project Structure

```bash
Book_Api/
├── Data/
│ ├── BookStoreContext.cs
├── Dtos/
│ ├── BookDto.cs
│ ├── CreateBookDto.cs
├── Endpoints/
│ ├── BooksEndpoints.cs
├── Entities/
│ ├── Book.cs
│ ├── Genre.cs
├── Mapping/
│ ├── BookProfile.cs
├── Program.cs
├── appsettings.json
└── README.md
```

### Data

Contains the `BookStoreContext.cs` which is responsible for the database context and configurations.

### Dtos

Contains Data Transfer Objects (DTOs) for transferring data between the API and clients.

### Endpoints

Contains the endpoint definitions for the API routes.

### Entities

Contains the entity classes representing the data models.

### Mapping

Contains AutoMapper profiles for mapping between entities and DTOs.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html)

### Installation

1. Clone the repository:

```bash
git clone https://github.com/caiolagreca/Book_Api.git
cd Book_Api
```

2. Restore the dependencies:

```bash
dotnet restore
```

3. Apply migrations to the SQLite database:

```bash
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

## API Endpoints

### Books

#### GET /books

Retrieve all books.

#### GET /books/{id}

Retrieve a book by ID.

#### POST /books

Create a new book.

#### PUT /books/{id}

Update an existing book.

#### DELETE /books/{id}

Delete a book.

### Categories

#### GET /categories

Retrieve all categories.

#### GET /categories/{id}

Retrieve a category by ID.

#### POST /categories

Create a new category.

#### PUT /categories/{id}

Update an existing category.

#### DELETE /categories/{id}

Delete a category.

## Contributing

Contributions are welcome! Please fork this repository and submit pull requests.

## License

This project is licensed under the MIT License.

## Contact

For any questions or suggestions, please contact:

Caio Lagreca

## Extra notes (draft):

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
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

dotnet ef migrations add InitialCreate --output-dir Data\Migrations //create the DB migrations; You can choose any name for InitialCreate
dotnet ef database update //update the database
```
