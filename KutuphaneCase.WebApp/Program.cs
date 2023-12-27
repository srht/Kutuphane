using FluentValidation;
using KutuphaneCase.Data;
using KutuphaneCase.Service;
using KutuphaneCase.WebApp.Models;
using KutuphaneCase.WebApp.Validators;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;

Log.Logger = new LoggerConfiguration().WriteTo.File("log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

	var builder = WebApplication.CreateBuilder(args);
	builder.Services.AddDbContext<AppDbContext>(options =>
	{
		options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"));
	}); ;
	builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
	builder.Services.AddScoped<IBooksService, BooksService>();

	builder.Services.AddScoped<IValidator<BookViewModel>, BookValidator>();
	builder.Services.AddScoped<IValidator<BorrowViewModel>, BorrowValidator>();
	builder.Services.AddControllersWithViews();
builder.Host.UseSerilog();

var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Books/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}
	app.UseSerilogRequestLogging();
	app.UseHttpsRedirection();
	app.UseStaticFiles();

	app.UseRouting();

	app.UseAuthorization();

	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Books}/{action=Index}/{id?}");

	app.Run();
