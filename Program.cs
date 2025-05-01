using ProjectTaskManagement.API;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.ConfigMapster();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseSwaggerUI(optons =>
    {
        optons.DocExpansion(DocExpansion.None);
        optons.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

}


app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
