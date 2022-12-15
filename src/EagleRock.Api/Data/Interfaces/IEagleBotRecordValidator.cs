using EagleRock.Api.DomainModels;

namespace EagleRock.Api.Data.Interfaces
{
    public interface IEagleBotRecordValidator
    {
        ValidationResult ValidateNewEagleBootRecord(EagleBotRecord record);
    }
}
