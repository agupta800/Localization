using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var cultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("fr"),
            new CultureInfo("ne")

        };
        options.DefaultRequestCulture = new RequestCulture("en-US");
        options.SupportedCultures = cultures;
        options.SupportedUICultures = cultures;


    });


var app = builder.Build();

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

//var cultures = new[] { "en", "fr", "ne" };
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(cultures[0])
//    .AddSupportedCultures(cultures)
//       .AddSupportedUICultures(cultures);
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
