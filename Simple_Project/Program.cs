using Simple_Project;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = "Data Source=DESKTOP-BNNL7BL\\SQLEXPRESS;Initial Catalog=MyProductDatabase;Integrated Security=True;Trust Server Certificate=True";

builder.Services.AddControllersWithViews();
builder.Services.AddTransient(provider => new DapperRepository(connectionString));

var app = builder.Build();


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
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
