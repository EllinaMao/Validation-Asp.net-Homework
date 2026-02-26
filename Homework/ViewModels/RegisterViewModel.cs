using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Homework.ViewModels
{
    /*
     
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
     */
    public record class RegisterViewModel
    {
        [ValidateNever]
        public bool TempsOfService { get; set; }
        
        [CreditCard(ErrorMessage = "Please enter a valid credit card number.")]
        public string CreditCardNumber { get; set; }
        
        [Required(ErrorMessage = "You need to enter password")]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Required(ErrorMessage = "You need to confirm password")]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
        
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }
        
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }
        
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        [Remote(action: "IsUsernameAvailable", controller: "Account", ErrorMessage = "Username is already taken.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string Website { get; set; }
    }
}
