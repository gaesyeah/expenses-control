using ExpensesControl.Data;
using ExpensesControl.Handlers;
using ExpensesControl.Routes;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<ExpensesControlContext>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Sem isso, o Scalar gerava URLs de teste em http:// mesmo com o site em
// https:// no Render, e o navegador bloqueava a requisição por segurança.
// Só afeta o ambiente publicado, localmente não faz diferença.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Aplica automaticamente quaisquer migrações pendentes do EF Core na inicialização,
// para que o esquema do banco de dados seja criado/atualizado sem comandos manuais do `dotnet ef`.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ExpensesControlContext>();
    await context.Database.MigrateAsync();
}

app.MapOpenApi();
app.MapScalarApiReference();
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseExceptionHandler();

app.PersonRoutes();
app.TransactionRoutes();
app.TotalRoutes();

app.Run();