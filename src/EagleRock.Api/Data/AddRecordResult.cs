namespace EagleRock.Api.Data
{
    public class AddRecordResult
    {
        public AddRecordResult(ValidationResult validationResult, bool isDuplicate)
        {
            ValidationResult = validationResult;
            IsDuplicate = isDuplicate;
        }

        public bool IsSuccess => IsValid && !IsDuplicate;
        public ValidationResult ValidationResult { get; }
        public bool IsValid => ValidationResult.IsValid;
        public bool IsDuplicate { get; }
    }
}
