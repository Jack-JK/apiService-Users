using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;

using apiServicio;

var builder = WebApplication.CreateBuilder(args);

var starup = new Startup(builder.Configuration);

starup.ConfigureServices(builder.Services);

// Add services to the container.



var app = builder.Build();

// Configure the HTTP request pipeline.
starup.Configure(app, app.Environment);

app.Run();
