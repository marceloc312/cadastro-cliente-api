using Api.Core.Validations;

namespace Api.Core.Interfaces.Validations
{
    public interface IValidation<in TEntity>
    {
        ValidateResult Valid(TEntity entity);
    }
}
