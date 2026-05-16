using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Info.Title = "StudyRecommendation API";
        doc.Info.Version = "v1";
        doc.Info.Description = "API de recomendación de recursos educativos para estudiantes universitarios";
        return Task.CompletedTask;
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
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors();
app.UseHttpsRedirection();

app.MapSyllabusEndpoints();
app.MapRecommendationEndpoints();
app.MapFeedbackEndpoints();
app.MapSubjectEndpoints();

app.Run();
