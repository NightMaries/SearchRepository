using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using SearchRepository.API.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using Microsoft.Data.SqlClient;

public class SearchRepositoryContext: DbContext{
    
    public DbSet<User> Users {get; set;}
    public DbSet<Repository> Repositories {get; set;}
    public DbSet<JsonRequest> jsonRequests {get; set;}

    private readonly IConfiguration _configuration;

    public SearchRepositoryContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private string SqlConnectionString => _configuration["ConnectionString:DefaultConnection"];

    private SqlConnection SqlDbConnection => new(SqlConnectionString);

    public QueryFactory SqlQueryFactory => new(SqlDbConnection, new SqlServerCompiler(),60);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlConnectionString);
    }
}