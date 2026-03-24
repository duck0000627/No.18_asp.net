using No._18.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// ======== 加入這段自動建表與更新資料庫的程式碼 ========
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<No._18.Models.AppDbContext>();
        // 這一行會自動尋找最新的 Migration，並幫你在雲端建立資料表
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        // 萬一建表失敗，可以在這裡捕捉錯誤 (目前先留空即可)
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "自動建立資料庫時發生錯誤。");
    }
}
// ========================================================

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
    pattern: "{controller=Case}/{action=Index}/{id?}");

app.Run();
