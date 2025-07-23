namespace TaskAndTeamManagementSystem.ExceptionHandler;

public class ValidationException : Exception
{
	public List<string> Errors { get; }

	public ValidationException(IEnumerable<string> errors)
		: base("Validation failed.")
	{
		Errors = errors.ToList();
	}
}