using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users
{
    public static class UserErrors
    {
       public static Error NotFound => new Error("User not found", "User not found");
       public static Error InvalidCredentials => new Error("Invalid credentials", "Invalid credentials");
    }
}