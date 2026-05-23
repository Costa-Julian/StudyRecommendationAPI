using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StudyRecommendationAPI.Configuration;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.Extensions;
using StudyRecommendationAPI.Services;

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

builder.Services.Configure<ExternalApisConfig>(
    builder.Configuration.GetSection(ExternalApisConfig.Section));

builder.Services.AddSingleton<ClaudeCodeService>();

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
    await db.Database.ExecuteSqlRawAsync("""
        CREATE TABLE IF NOT EXISTS SearchResults (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Topic TEXT NOT NULL,
            VideoUrl TEXT NULL,
            VideoPositiveVotes INTEGER NOT NULL DEFAULT 0,
            VideoNegativeVotes INTEGER NOT NULL DEFAULT 0,
            ArticleUrl TEXT NULL,
            ArticlePositiveVotes INTEGER NOT NULL DEFAULT 0,
            ArticleNegativeVotes INTEGER NOT NULL DEFAULT 0,
            CreatedAt TEXT NOT NULL
        );
        CREATE INDEX IF NOT EXISTS IX_SearchResults_Topic ON SearchResults (Topic);
    """);
    await SeedData.SeedAsync(db);
}

app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors();
app.UseHttpsRedirection();

app.MapSyllabusEndpoints();
app.MapRecommendationEndpoints();
app.MapSearchEndpoints();
app.MapFeedbackEndpoints();
app.MapSubjectEndpoints();

app.Run();
