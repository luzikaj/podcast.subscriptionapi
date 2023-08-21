using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Podcast.SubscriptionApi.Controllers;
using Podcast.SubscriptionApi.Services;

namespace Podcast.SubscriptionApi.Tests;

public class ControllerTests
{
    [Fact]
    public async Task Start_EmptyHeader_Unauthorized()
    {
        var subscriptionService = Substitute.For<ISubscriptionService>();
        var apiController = new SubscriptionController(subscriptionService)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var response = await apiController.Start();

        Assert.IsType<UnauthorizedResult>(response);
    }

    [Fact]
    public async Task Start_AddSubscription_Ok()
    {
        const string testEmail = "jaworowski@gmail.com";
        
        var subscriptionService = Substitute.For<ISubscriptionService>();
        subscriptionService.ExistsAsync(testEmail).Returns(false);
        subscriptionService.AddAsync(testEmail).Returns(true);
        
        var apiController = new SubscriptionController(subscriptionService)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
        apiController.ControllerContext.HttpContext.Request.Headers["X-Custom-Auth"] = testEmail;
        
        var response = await apiController.Start();

        Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public async Task Start_AddSubscription_AlreadyExists()
    {
        const string testEmail = "jaworowski+1@gmail.com";

        var subscriptionService = Substitute.For<ISubscriptionService>();
        subscriptionService.ExistsAsync(testEmail).Returns(true);
        
        var apiController = new SubscriptionController(subscriptionService)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
        apiController.ControllerContext.HttpContext.Request.Headers["X-Custom-Auth"] = testEmail;
        
        var response = await apiController.Start();

        Assert.IsType<ConflictObjectResult>(response);
    }
    
}