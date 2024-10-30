using EatEase.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EatEase.Messaging;

public class RegisterConsumer : IConsumer<RegisterRequest>
{
    private readonly ILogger<RegisterConsumer> _logger;

    public RegisterConsumer(ILogger<RegisterConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RegisterRequest> context)
    {
        var request = context.Message;

        _logger.LogInformation("Creating new user {Message}", request);

        var newUser = NewId.NextGuid();

        // TODO validate data, check if exists
        // TODO create new user
        await context.RespondAsync<UserCreated>(new
            {
                UserId = newUser // TODO use this when generating auth token
            })
            .ConfigureAwait(false);
    }
}