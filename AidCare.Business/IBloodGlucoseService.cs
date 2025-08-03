using AidCare.Entities;

namespace AidCare.Business
{
    public interface IBloodGlucoseService
    {
        Task<BloodGlucose?> GetByIdAsync(int id);
        Task<IEnumerable<BloodGlucose>> GetAllAsync();
        Task<IEnumerable<BloodGlucose>> GetByUserIdAsync(int userId);
        Task<BloodGlucose> CreateAsync(BloodGlucose bloodGlucose);
        Task<BloodGlucose> UpdateAsync(BloodGlucose bloodGlucose);
        Task DeleteAsync(int id);
        Task<IEnumerable<BloodGlucose>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
    }
}