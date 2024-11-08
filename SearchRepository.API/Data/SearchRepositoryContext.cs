using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using SearchRepository.API.Entities;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using Microsoft.AspNetCore.Mvc;


public class SearchRepositoryContext: DbContext{
    
    private readonly IConfiguration _configuration;
    
    private string SqlConnectionString => _configuration["ConnectionString:DefaultConnection"];

    private SqlConnection  SqlDbConnection => new(SqlConnectionString);

    public QueryFactory SqlQueryFactory =>  new(SqlDbConnection, new MySqlCompiler(),30);


    public DbSet<User> Users {get; set;}
    public DbSet<Repository> Repositories {get; set;}
    public DbSet<JsonRequest> jsonRequests {get; set;}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }
}