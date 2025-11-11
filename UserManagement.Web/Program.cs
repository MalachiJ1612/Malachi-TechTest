using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Westwind.AspNetCore.Markdown;
using UserManagement.Data;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Services.Domain.Implementations;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite("Data Source=usermanagement.db"));

builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers(); // Only for APIs, no MVC Views
builder.Services.AddMarkdown();
#endregion

#region Build App

var app = builder.Build();
#endregion

#region Middleware Pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseMarkdown();
#endregion

#region Routing


app.MapControllers();


app.MapBlazorHub();

app.MapFallbackToPage("/_Host");
#endregion

#region Run App

app.Run();
#endregion
