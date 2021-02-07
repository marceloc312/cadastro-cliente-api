using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.Validations
{
    public class ValidateResult
    {
        private readonly List<ValidationError> _erros;
        private string Message { get; set; }
        public bool IsValid { get { return !_erros.Any(); } }
        public IEnumerable<ValidationError> Errors { get { return _erros; } }

        public ValidateResult()
        {
            _erros = new List<ValidationError>();
        }

        public ValidateResult Add(string errorMessage)
        {
            _erros.Add(new ValidationError(errorMessage));
            return this;
        }

        public ValidateResult Add(ValidationError error)
        {
            _erros.Add(error);
            return this;
        }

        public ValidateResult Add(params ValidateResult[] validationResults)
        {
            if (validationResults == null) return this;

            foreach (var result in validationResults.Where(r => r != null))
                _erros.AddRange(result.Errors);

            return this;
        }

        public ValidateResult Remove(ValidationError error)
        {
            if (_erros.Contains(error))
                _erros.Remove(error);
            return this;
        }

        public string GetMessage()
        {
            return string.Join(' ', _erros.Select(x =>x.Message));
        }
    }
}
