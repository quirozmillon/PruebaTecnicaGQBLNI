using Datos;
using Negocio;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddLogicaNegocio();

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

    var xmlDocumentacion = Path.Combine(
        AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
    );

    if (File.Exists(xmlDocumentacion))
    {
        options.IncludeXmlComments(xmlDocumentacion, includeControllerXmlComments: true);
    }

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "My API Documentation";
        options.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
