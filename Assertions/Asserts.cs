using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Assertions;

public static class Asserts {
    public static bool AssertTrue(
        [DoesNotReturnIf(false)] this bool booleanExpression,
        string message = "Boolean expression asserted to be true is false.",
        [CallerArgumentExpression("booleanExpression")] string? expression = null
    ) => booleanExpression ? true : throw new AssertException(message, expression);


    public static bool AssertFalse(
        [DoesNotReturnIf(true)] this bool booleanExpression,
        string message = "Boolean expression asserted to be false is true.",
        [CallerArgumentExpression("booleanExpression")] string? expression = null
    ) => booleanExpression ? throw new AssertException(message, expression) : false;
}

public class AssertException : Exception {
    public string? Expression { get; init; }

    public AssertException() { }
    protected AssertException( SerializationInfo info, StreamingContext context) : base(info, context) { }
    public AssertException(string? message) : base(message) { }

    public AssertException(string? message, string? expression) : base(
        message is not null && expression is not null
            ? message + "Expression: " + expression
            : message ?? expression) {
    }

    public AssertException( string? message,  Exception? innerException) : base(
        message,
        innerException) { }
}

public class AssertException<T> : AssertException {
    public readonly T Value;

    protected AssertException(SerializationInfo info, StreamingContext context) : base(info, context) 
        => Value = default!;

    public AssertException(string? message, T value) : base(message) 
        => Value = value;

    public AssertException(string? message, T value, Exception? innerException) : base(message, innerException) 
        => Value = value;
}

