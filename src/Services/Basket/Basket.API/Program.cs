var builder = WebApplication.CreateBuilder(args);

// Add Services to the IoC Container

var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();
