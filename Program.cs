using BookApi;
using BookApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BookStore");

builder.Services.AddSqlite<BookStoreContext>(connString);

//AddAuthentication: Adiciona e configura a autenticação JWT.
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

//AddAuthorization: Adiciona o serviço de autorização à aplicação.
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapBooksEndpoints();
app.MapCategoriesEndpoints();

await app.MigrateDbAsync();

app.Run();
