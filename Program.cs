using BookApi;
using BookApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BookStore");

builder.Services.AddSqlite<BookStoreContext>(connString);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapBooksEndpoints();
app.MapCategoriesEndpoints();

await app.MigrateDbAsync();

app.Run();

