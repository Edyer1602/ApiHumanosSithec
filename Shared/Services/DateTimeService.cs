using Application.Interfaces;

namespace Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);
    }
}
