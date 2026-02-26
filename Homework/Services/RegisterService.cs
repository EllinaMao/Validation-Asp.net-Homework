using Homework.Data;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.Services
{
    /*
     IsUsernameAvailable
     */
    public class RegisterService
    {
        private readonly ApplicationContext _context;
        public RegisterService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            // await освобождает поток, пока БД выполняет запрос
            bool userExists = await _context.User
                .AnyAsync(u => u.Username.ToLower() == username.ToLower());

            return !userExists;
        }


        public async Task AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }


    }
}
