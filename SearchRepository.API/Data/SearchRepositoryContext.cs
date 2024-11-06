using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using SearchRepository.API.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL;

public class SearchRepositoryContext: DbContext{
    
    public DbSet<User> Users {get; set;}
    public DbSet<Repository> Repositories {get; set;}
    public DbSet<JsonRequest> jsonRequests {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;DataBase=21ip213;Username=localUser;Password=1234;");
    }
}