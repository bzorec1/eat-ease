using System.Text.RegularExpressions;
using EatEase.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace EatEase.BlazorApp.Infrastructure;

public partial class LoginConsumer : IConsumer<LoginRequest>
{
    private readonly UserRepository _repository;
    private readonly ILogger<LoginConsumer> _logger;
    private readonly PasswordHasher<User> _passwordHasher;

    public LoginConsumer(ILogger<LoginConsumer> logger, PasswordHasher<User> passwordHasher,
        UserRepository repository)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<LoginRequest> context)
    {
        var request = context.Message;
        _logger.LogInformation("Logging in user {Message}", request);

        var user = (EmailRegex().IsMatch(request.Identifier)
                       ? await _repository.GetUserByEmailAsync(request.Identifier, context.CancellationToken)
                       : await _repository.GetUserByUserNameAsync(request.Identifier, context.CancellationToken))
                   ?? throw new InvalidOperationException();

        if (string.IsNullOrEmpty(request.Password))
        {
            _logger.LogWarning("Password is empty.");
            return;
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.Password ?? throw new InvalidOperationException(),
            request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            _logger.LogWarning("Invalid password");
            return;
        }

        // TODO respond with new auth token
        throw new System.NotImplementedException();
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex EmailRegex();
}