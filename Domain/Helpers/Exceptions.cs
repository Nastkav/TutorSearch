namespace Domain.Helpers;

public class DomainException : Exception
{
    public DomainException()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }
}

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}

public class CommandParameterException : DomainException
{
    public CommandParameterException(string message) : base(message)
    {
    }
}

public class ReviewException : DomainException
{
    public ReviewException(string message, int UserId) : base(message)
    {
    }
}

public class AccessDeniedException : DomainException
{
    public AccessDeniedException(string message) : base(message)
    {
    }
}

public class SolutionException : DomainException
{
    public SolutionException(string message) : base(message)
    {
    }
}

public class SubjectNotFoundException : DomainException
{
    public SubjectNotFoundException(string message) : base(message)
    {
    }
}

public class LessonException : DomainException
{
    public LessonException(string message) : base(message)
    {
    }
}

public class RequestException : DomainException
{
    public RequestException(string message) : base(message)
    {
    }
}

public class AssignmentException : DomainException
{
    public AssignmentException(string message) : base(message)
    {
    }
}

public class TimeRangeException : DomainException
{
    public TimeRangeException(string message)
    {
    }

    public TimeRangeException(string message, DateTime first, DateTime? second = null) : base(message)
    {
    }
}