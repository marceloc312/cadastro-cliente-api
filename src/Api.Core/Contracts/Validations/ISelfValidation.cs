using Api.Core.Validations;

namespace Api.Core.Interfaces.Validations
{
    public interface ISelfValidation
    {
        ValidateResult ValidationResult { get; }
        bool IsValid();
    }
}
