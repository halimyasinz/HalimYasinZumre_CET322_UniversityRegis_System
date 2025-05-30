using CET322Final.Data;
using CET322Final.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 💾 Veritabanı bağlantısı (EF Core)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 👤 Identity Servisleri
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // ROL DESTEĞİ EKLENDİ
.AddEntityFrameworkStores<ApplicationDbContext>();

// 🔧 MVC + Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// 🌟 ROL VE ADMIN KULLANICI OLUŞTUR (uygulama başında bir kez çalışır)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Admin rolü yoksa oluştur
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Admin yapılacak kullanıcıyı e-posta ile bul
    var adminUser = await userManager.FindByEmailAsync("halimyasinz@gmail.com");

    if (adminUser != null)
    {
        // Eğer admin değilse role ekle
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// 🔒 Hata yönetimi ve güvenlik
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 👤 Kimlik doğrulama ve yetkilendirme
app.UseAuthentication();  // ✨ Identity için gerekli
app.UseAuthorization();

// 🌐 Route ayarları
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}")
    .WithStaticAssets();

// 🧩 Identity Razor Pages (Login, Register vs.)
app.MapRazorPages();

// 📦 Static assets (önceki yapı uyumu için)
app.MapStaticAssets();

app.Run();
