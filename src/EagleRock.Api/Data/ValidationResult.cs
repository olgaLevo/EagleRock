namespace EagleRock.Api.Data
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public ErrorsList Errors { get; set; } = new ErrorsList();
    }
}
