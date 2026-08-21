using MyMuse.Api.Data;
using MyMuse.Api.Features.Songs;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Features.Artists;
using MyMuse.Api.Features.Genres;
using MyMuse.Api.Features.Search;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

builder.AddMyMuseConnection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MigrateDb();

app.MapControllers();

app.Run();