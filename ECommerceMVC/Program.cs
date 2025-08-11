using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Business.Services.Concrete;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.DataAccess.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>(); //nesneyi otomatik oluþturur



builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICartService, CartService>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
