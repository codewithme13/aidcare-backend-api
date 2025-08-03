using AidCare.Entities;

namespace AidCare.Business
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByTcKimlikNoAsync(string tcKimlikNo);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> TcKimlikNoExistsAsync(string tcKimlikNo);
        Task<bool> EmailExistsAsync(string email);
    }
}