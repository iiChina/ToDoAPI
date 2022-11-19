using ToDo.Data;

var builder = WebApplication.CreateBuilder(args);

// Necessário para que o ASP.NET identifique que o projeto esta separando as Actions dentro de controllers.
builder.Services.AddControllers();
/*
    Transforma a classe de acesso a dados em um serviço que vai ser gerenciado pelo ASP.NET,
    isso abstrai a preocupação de criar e destruir conexões com o banco, pois o ASP 
    já gerencia isso.
*/
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Necessário para que o ASP.NET cosiga mapear todas as classes que são controllers.
app.MapControllers();


app.Run();
