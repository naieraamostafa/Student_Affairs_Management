using Microsoft.EntityFrameworkCore;
using Student_Affairs.Models;

namespace Student_Affairs.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, UserDTO userDTO)
        {
            var existingUser = await _dbContext.Users.FindAsync(id);

            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }

            // Update the user properties with values from the DTO
            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;
            existingUser.Email = userDTO.Email;
            existingUser.PhoneNumber = userDTO.PhoneNumber;
            existingUser.Address = userDTO.Address;
            existingUser.Department = userDTO.Department;
            existingUser.GPA = (double)userDTO.GPA;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true; // User deleted successfully
            }
            return false; // User not found
        }

    }
}
