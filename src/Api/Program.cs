using Api;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Register services...");
IoC.RegisteredServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Enable Swagger in development environment...");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "User API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    if (dbContext.Database.IsRelational())
    {
        int retries = 0;
        bool migrated = false;
        while (!migrated && retries < 10)
        {
            try
            {
                dbContext.Database.Migrate();
                migrated = true;
            }
            catch (Exception ex)
            {
                retries++;
                Console.WriteLine($"Database is not ready yet. Retry {retries}/10. Error: {ex.Message}");
                if (retries < 10)
                {
                    Thread.Sleep(3000);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

Console.WriteLine("Run the application...");
app.Run();