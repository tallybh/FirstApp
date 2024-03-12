using FirstApp;
using FirstApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITransientService, OperationService>();
builder.Services.AddScoped<IScopedService, OperationService>();
builder.Services.AddSingleton<ISingletonService, OperationService>();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.Use(async (context, next) => {

    await context.Response.WriteAsync(" My Middleware A1 \n");
    await next();
    await context.Response.WriteAsync(" My Middleware A2 \n");
});

app.Use(async (context, next) => {
    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "1")
    {
        await context.Response.WriteAsync(" My Middleware custom \n");
    }
    await context.Response.WriteAsync(" My Middleware B1 \n");
    await next();
    await context.Response.WriteAsync(" My Middleware B2 \n");
});

app.UseMiddleware<MiddleC>();


app.Run(async (context) => {
    await context.Response.WriteAsync(" APP RUN \n");
});


app.Run();

