using AssignmentM2M.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Initializer>();
builder.Services.AddScoped<IStoreRepository, DbStoreRepository>();
builder.Services.AddScoped<IItemRepository, DbItemRepository>();
builder.Services.AddScoped<ISupplierRepository, DbSupplierRepository>();
builder.Services.AddScoped<IStoreItemRepository, DbStoreItemRepository>();
builder.Services.AddScoped<ISupplierItemRepository, DbSupplierItemRepository>();

var app = builder.Build();
SeedData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{itemId?}");

app.Run();


static void SeedData(WebApplication app)
{
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	try
	{
		var initializer = services.GetRequiredService<Initializer>();
		initializer.SeedDatabase();
	}
	catch (Exception ex)
	{
		var logger = services.GetRequiredService<ILogger<Program>>();
		logger.LogError($"An error occurred while seeding the database: {ex.Message}");
	}
}
