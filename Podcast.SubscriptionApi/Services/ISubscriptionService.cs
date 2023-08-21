namespace Podcast.SubscriptionApi.Services;

public interface ISubscriptionService
{
    Task<bool> ExistsAsync(string email);

    Task<bool> AddAsync(string email);

    Task<bool> RemoveAsync(string email);
}