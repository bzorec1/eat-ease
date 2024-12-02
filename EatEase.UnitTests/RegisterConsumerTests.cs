using EatEase.BlazorApp.Infrastructure;
using EatEase.Contracts;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EatEase.UnitTests;

public class RegisterConsumerTests
{
    [Fact]
    public async Task ConsumerShouldConsumeMessageWhenPublished()
    {
        const string email = "test@email.com";
        var username = Guid.NewGuid().ToString();
        var password = Guid.NewGuid().ToString();

        var serviceCollection = CreateServiceCollection();

        serviceCollection.AddMassTransitTestHarness(x =>
            x.AddConsumer<RegisterConsumer>());

        var provider = serviceCollection.BuildServiceProvider();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        await harness.Bus.Publish(new RegisterRequest
        {
            Email = email,
            Username = username,
            Password = password
        });

        Assert.True(await harness.Published.Any<RegisterRequest>());
        Assert.True(await harness.Consumed.Any<RegisterRequest>());
    }

    [Fact]
    public async Task ConsumerShouldConsumeExactMessageWhenPublished()
    {
        const string email = "test@email.com";
        var username = Guid.NewGuid().ToString();
        var password = Guid.NewGuid().ToString();

        var serviceCollection = CreateServiceCollection();

        serviceCollection.AddMassTransitTestHarness(x =>
            x.AddConsumer<RegisterConsumer>());

        var provider = serviceCollection.BuildServiceProvider();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        await harness.Bus.Publish(new RegisterRequest
        {
            Email = email,
            Username = username,
            Password = password
        });

        Assert.True(await harness.Published.Any<RegisterRequest>());
        Assert.True(await harness.Consumed.Any<RegisterRequest>());

        var consumerHarness = harness.GetConsumerHarness<RegisterConsumer>();

        Assert.True(await consumerHarness.Consumed.Any<RegisterRequest>());

        var message = await consumerHarness.Consumed.SelectAsync<RegisterRequest>().FirstOrDefault();
        Assert.NotNull(message);
        Assert.True(message.Context.Message.Email == email);
        Assert.True(message.Context.Message.Password == password);
        Assert.True(message.Context.Message.Username == username);
    }

    private static ServiceCollection CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging();
        serviceCollection.AddSingleton<PasswordHasher<User>>();
        serviceCollection.AddTransient<UserRepository>();
        serviceCollection.AddDbContext<UserContext>(options => options.UseInMemoryDatabase("InMemoryAppDb"));

        return serviceCollection;
    }
}