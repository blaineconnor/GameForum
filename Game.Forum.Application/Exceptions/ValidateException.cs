using FluentValidation.Results;

namespace Game.Forum.Application.Exceptions
{
    public class ValidateException : Exception
    {
        public List<string> ErrorMessages { get; set; }

        public ValidateException(ValidationResult result) : base()
        {
            ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}
