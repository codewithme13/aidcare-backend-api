using AidCare.Entities;
using AidCare.DataAccess;

namespace AidCare.Business
{
    public class BloodGlucoseService : IBloodGlucoseService
    {
        private readonly IRepository<BloodGlucose> _bloodGlucoseRepository;
        private readonly IRepository<User> _userRepository;

        public BloodGlucoseService(IRepository<BloodGlucose> bloodGlucoseRepository, IRepository<User> userRepository)
        {
            _bloodGlucoseRepository = bloodGlucoseRepository;
            _userRepository = userRepository;
        }

        public async Task<BloodGlucose?> GetByIdAsync(int id)
        {
            return await _bloodGlucoseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BloodGlucose>> GetAllAsync()
        {
            return await _bloodGlucoseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<BloodGlucose>> GetByUserIdAsync(int userId)
        {
            return await _bloodGlucoseRepository.FindAsync(bg => bg.UserId == userId);
        }

        public async Task<BloodGlucose> CreateAsync(BloodGlucose bloodGlucose)
        {
            var user = await _userRepository.GetByIdAsync(bloodGlucose.UserId);
            if (user == null)
            {
                throw new InvalidOperationException("Belirtilen kullanıcı bulunamadı.");
            }

            if (bloodGlucose.GlucoseValue <= 0)
            {
                throw new InvalidOperationException("Kan şekeri değeri 0'dan büyük olmalıdır.");
            }

            return await _bloodGlucoseRepository.AddAsync(bloodGlucose);
        }

        public async Task<BloodGlucose> UpdateAsync(BloodGlucose bloodGlucose)
        {
            var existingRecord = await GetByIdAsync(bloodGlucose.Id);
            if (existingRecord == null)
            {
                throw new InvalidOperationException("Güncellenecek kan şekeri kaydı bulunamadı.");
            }

            if (bloodGlucose.GlucoseValue <= 0)
            {
                throw new InvalidOperationException("Kan şekeri değeri 0'dan büyük olmalıdır.");
            }

            return await _bloodGlucoseRepository.UpdateAsync(bloodGlucose);
        }

        public async Task DeleteAsync(int id)
        {
            var record = await GetByIdAsync(id);
            if (record == null)
            {
                throw new InvalidOperationException("Silinecek kan şekeri kaydı bulunamadı.");
            }

            await _bloodGlucoseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BloodGlucose>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _bloodGlucoseRepository.FindAsync(bg =>
                bg.UserId == userId &&
                bg.MeasurementDate >= startDate &&
                bg.MeasurementDate <= endDate);
        }
    }
}