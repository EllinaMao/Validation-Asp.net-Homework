using Homework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Homework.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'HomeworkContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<RegisterService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

/*
 Создайте форму регистрации пользователя, которая использует следующие атрибуты для обеспечения достоверности и безопасности данных:

1) [ValidateNever]: Исключает поле "TermsOfService" из проверки.
2) [CreditCard]: Проверяет формат номера кредитной карты в поле "CreditCardNumber".
3) [Compare]: Сравнивает пароли в полях "Password" и "ConfirmPassword".
4) [EmailAddress]: Проверяет формат адреса электронной почты в поле "Email".
5) [PhoneNumber]: Проверяет формат номера телефона в поле "PhoneNumber".
6) [Range]: Ограничивает возраст пользователя в поле "Age" диапазоном от 18 до 100 лет.
7) [RegularExpression]: Проверяет, содержит ли имя пользователя в поле "Username" только буквы, цифры и подчеркивания.
8) [Required]: Делает поля "FirstName", "LastName", "Password", "ConfirmPassword" и "Email" обязательными.
9) [StringLength]: Ограничивает длину имени пользователя (поле "Username") 20 символами, а пароля (поля "Password" и "ConfirmPassword") - 100 символами.
10) [URL]: Проверяет формат URL-адреса в поле "Website".
11) [Remote]: Проверяет уникальность имени пользователя ("Username") путем вызова метода действия на сервере.

Создайте модель «RegisterViewModel» со свойствами, соответствующими перечисленным атрибутам. Добавьте атрибуты к соответствующим свойствам модели. 
Создайте контроллер «AccountController» с методом «Register». В методе Register реализуйте логику проверки данных модели, используя атрибуты.
В случае успешной проверки данных сохраните пользователя в базе данных.
В случае ошибки отобразите сообщение об ошибке пользователю.
 */
