using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using SearchRepository.API.Entities;

public class SearchRepositoryContext: DbContext{
    
    public DbSet<User> Users {get; set;}
    public DbSet<Repository> Repositories {get; set;}
    public DbSet<JsonRequest> jsonRequests {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = 21ip213; Trusted_Connection = True;");
        
        Console.WriteLine(optionsBuilder.UseSqlServer());
    }
}