using System.Text.RegularExpressions;
using EatEase.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace EatEase.BlazorApp.Infrastructure;

public partial class RegisterConsumer : IConsumer<RegisterRequest>
{
    private readonly PasswordHasher<User> _hasher;
    private readonly UserRepository _userRepository;
    private readonly ILogger<RegisterConsumer> _logger;

    public RegisterConsumer(ILogger<RegisterConsumer> logger, UserRepository userRepository,
        PasswordHasher<User> hasher)
    {
        _logger = logger;
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public async Task Consume(ConsumeContext<RegisterRequest> context)
    {
        var request = context.Message;

        _logger.LogInformation("Creating new user {Message}", request);

        if (!EmailRegex().IsMatch(request.Email))
        {
            _logger.LogWarning("Email address is not valid");
            await context.RespondAsync<ErrorRegistering>(new
                {
                    Error = "Email address is not valid."
                })
                .ConfigureAwait(false);
            return;
        }

        if (await _userRepository.UserExistsAsync(request.Email, context.CancellationToken))
        {
            _logger.LogWarning("User with email {Email} already exists.", request.Email);
            await context.RespondAsync<ErrorRegistering>(new
                {
                    Error = $"User with email {request.Email} already exists."
                })
                .ConfigureAwait(false);
            return;
        }

        var user = new User
        {
            Id = NewId.NextGuid(),
            Email = request.Email,
            Username = request.Username
        };

        user.Password = _hasher.HashPassword(user, request.Password);

        await _userRepository
            .AddAsync(user, context.CancellationToken)
            .ConfigureAwait(false);

        await context.RespondAsync<UserCreated>(new
            {
                UserId = user.Id,
                user.Username,
                user.Email,
            })
            .ConfigureAwait(false);
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex EmailRegex();
}