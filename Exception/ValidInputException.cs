using System;

public class ValidInputException : Exception
{
    public ValidInputException()
    {
    }

    public ValidInputException(string message)
        : base(message)
    {
    }
}