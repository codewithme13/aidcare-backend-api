using AidCare.Entities;
using AidCare.DataAccess;

namespace AidCare.Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByTcKimlikNoAsync(string tcKimlikNo)
        {
            return await _userRepository.FirstOrDefaultAsync(u => u.TcKimlikNo == tcKimlikNo);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            var existingUser = await GetByTcKimlikNoAsync(user.TcKimlikNo);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Bu TC Kimlik No ile kayıtlı kullanıcı zaten mevcut.");
            }

            var existingEmail = await GetByEmailAsync(user.Email);
            if (existingEmail != null)
            {
                throw new InvalidOperationException("Bu email adresi ile kayıtlı kullanıcı zaten mevcut.");
            }

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Güncellenecek kullanıcı bulunamadı.");
            }

            return await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                throw new InvalidOperationException("Silinecek kullanıcı bulunamadı.");
            }

            await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> TcKimlikNoExistsAsync(string tcKimlikNo)
        {
            return await _userRepository.ExistsAsync(u => u.TcKimlikNo == tcKimlikNo);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.ExistsAsync(u => u.Email == email);
        }
    }
}