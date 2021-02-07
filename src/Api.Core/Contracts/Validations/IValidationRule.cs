namespace Api.Core.Interfaces.Validations
{
    public interface IValidationRule<in TEntity>
    {
        string ErrorMessage { get; }
        bool Valid(TEntity entity);
    }
}
