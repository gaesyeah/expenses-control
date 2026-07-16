using ExpensesControl.Data;
using ExpensesControl.Handlers;
using ExpensesControl.Routes;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<ExpensesControlContext>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

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