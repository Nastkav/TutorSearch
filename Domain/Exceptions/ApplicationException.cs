namespace Domain.Exceptions;

public abstract class BaseApplicationException : Exception
{
    public BaseApplicationException(string input) : base(input) { }
}

public class AccessDenyException : BaseApplicationException
{
    public AccessDenyException(string username) : base($"Access denied for '{username}.'") { }
}

public class IncorrectUserId : BaseApplicationException
{
    public IncorrectUserId(string message) : base(message) { }
}