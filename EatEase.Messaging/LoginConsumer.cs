using System.Text.RegularExpressions;
using EatEase.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EatEase.Messaging;

// TODO use the signInManager by microsoft
public partial class LoginConsumer : IConsumer<LoginRequest>
{
    private readonly UserRepository _repository;
    private readonly ILogger<LoginConsumer> _logger;
    private readonly PasswordHasher<User> _passwordHasher; // TODO use user object??

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

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

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