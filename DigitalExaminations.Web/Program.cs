using DigitalExaminations.BLL.Services;
using DigitalExaminations.DataAccess.Data;
using DigitalExaminations.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IExamService, ExamService>();
builder.Services.AddTransient<IQnAService, QnAService>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".DigitalExamination.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Adjust the timeout as needed
                                                    // Other session configuration options can be set here
});

//Validate anti forgery token
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
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
app.UseSession();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.Run();
