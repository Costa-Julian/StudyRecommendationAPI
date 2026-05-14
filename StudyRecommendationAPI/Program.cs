using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "StudyRecommendation API",
        Version = "v1",
        Description = "API de recomendación de recursos educativos para estudiantes universitarios"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=studyrecommendation.db"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    await SeedData.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyRecommendation API v1"));
}

app.UseCors();
app.UseHttpsRedirection();

app.MapSyllabusEndpoints();
app.MapRecommendationEndpoints();
app.MapFeedbackEndpoints();
app.MapSubjectEndpoints();

app.Run();
