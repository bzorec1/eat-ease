using System.Text.RegularExpressions;
using EatEase.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EatEase.Messaging;

// TODO use the signInManager by microsoft
public partial class LoginConsumer : IConsumer<LoginRequest>
{
    private readonly ILogger<LoginConsumer> _logger;
    private readonly PasswordHasher<LoginRequest> _passwordHasher; // TODO use user object??

    public LoginConsumer(ILogger<LoginConsumer> logger, PasswordHasher<LoginRequest> passwordHasher)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public Task Consume(ConsumeContext<LoginRequest> context)
    {
        var request = context.Message;

        _logger.LogInformation("Logging in user {Message}", request);

        // TODO validate the user input
        // TODO on error respond with error response
        if (EmailRegex().IsMatch(request.Identifier))
        {
            string hashedPassword = "";
            var verificationResult = _passwordHasher.VerifyHashedPassword(request, hashedPassword, request.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Invalid password");
                return Task.CompletedTask;
            }

            // TODO email login
            throw new System.NotImplementedException();
        }

        // TODO username login
        // TODO respond with new auth token
        throw new System.NotImplementedException();
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex EmailRegex();
}