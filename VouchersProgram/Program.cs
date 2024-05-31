using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using VouchersProgram.Areas.Identity;
using VouchersProgram.Data;
using Syncfusion.Blazor;
using VouchersProgram.Data.Services;
using VouchersProgram.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<ISqlConnectionConfiguration, SqlConnectionConfiguration>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IVWS_HeaderService, VWS_HeaderService>();
builder.Services.AddScoped<IVWS_LineService, VWS_LineService>();
builder.Services.AddScoped<IAuthorizersService, AuthorizersService>();
builder.Services.AddScoped<IPayMethodService, PayMethodService>();
builder.Services.AddScoped<IExpense_AccountService, Expense_AccountService>();
builder.Services.AddScoped<IDeparmentService, DeparmentService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IBanksService, BanksService>();

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzIwODQ3N0AzMjM1MmUzMDJlMzBCRG94anZzemJ2MGJEWENoNTQ0L2M2RmVGa3Ewb0I3QUF6Q29UKy9kbVZRPQ==");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
