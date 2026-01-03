using Microsoft.EntityFrameworkCore;
using RhPortal.Api.Application.Departments;
using RhPortal.Api.Application.Departments.Handlers;
using RhPortal.Api.Application.Units.Handlers;
using RhPortal.Api.Application.Units;
using RhPortal.Api.Infrastructure.Data;
using RhPortal.Api.Infrastructure.Tenancy;
using RhPortal.Api.Swagger;
using System.Text.Json.Serialization;
using RhPortal.Api.Application.JobPositions.Handlers;
using RhPortal.Api.Application.JobPositions;
using RhPortal.Api.Application.Managers;
using RhPortal.Api.Application.Managers.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<TenantHeaderOperationFilter>();
});

// Tenancy
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddScoped<TenantMiddleware>();

// SQLite + EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var conn = builder.Configuration.GetConnectionString("Default");
    options.UseSqlite(conn);
});

// Application services + handlers
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IJobPositionService, JobPositionService>();
builder.Services.AddScoped<IManagerService, ManagerService>();

//Departamentos
builder.Services.AddScoped<IListDepartmentsHandler, ListDepartmentsHandler>();
builder.Services.AddScoped<IGetDepartmentByIdHandler, GetDepartmentByIdHandler>();
builder.Services.AddScoped<ICreateDepartmentHandler, CreateDepartmentHandler>();
builder.Services.AddScoped<IUpdateDepartmentHandler, UpdateDepartmentHandler>();
builder.Services.AddScoped<IDeleteDepartmentHandler, DeleteDepartmentHandler>();

//Unidades|Filiais
builder.Services.AddScoped<IListUnitsHandler, ListUnitsHandler>();
builder.Services.AddScoped<IGetUnitByIdHandler, GetUnitByIdHandler>();
builder.Services.AddScoped<ICreateUnitHandler, CreateUnitHandler>();
builder.Services.AddScoped<IUpdateUnitHandler, UpdateUnitHandler>();
builder.Services.AddScoped<IDeleteUnitHandler, DeleteUnitHandler>();

//Cargos
builder.Services.AddScoped<IListJobPositionsHandler, ListJobPositionsHandler>();
builder.Services.AddScoped<IGetJobPositionByIdHandler, GetJobPositionByIdHandler>();
builder.Services.AddScoped<ICreateJobPositionHandler, CreateJobPositionHandler>();
builder.Services.AddScoped<IUpdateJobPositionHandler, UpdateJobPositionHandler>();
builder.Services.AddScoped<IDeleteJobPositionHandler, DeleteJobPositionHandler>();

//Gestores
builder.Services.AddScoped<IListManagersHandler, ListManagersHandler>();
builder.Services.AddScoped<IGetManagerByIdHandler, GetManagerByIdHandler>();
builder.Services.AddScoped<ICreateManagerHandler, CreateManagerHandler>();
builder.Services.AddScoped<IUpdateManagerHandler, UpdateManagerHandler>();
builder.Services.AddScoped<IDeleteManagerHandler, DeleteManagerHandler>();

builder.Services
    .AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// garante pasta do banco
Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath, "App_Data"));

// migrate + seed ao subir (ótimo para DEV)
await DbSeeder.MigrateAndSeedAsync(app.Services, app.Configuration, app.Environment);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Tenant obrigatório ANTES de Authorization
app.UseMiddleware<TenantMiddleware>();

app.MapControllers();
app.Run();
