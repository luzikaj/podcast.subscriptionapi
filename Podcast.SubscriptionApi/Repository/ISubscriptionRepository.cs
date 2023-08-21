namespace Podcast.SubscriptionApi.Repository;

public interface ISubscriptionRepository
{
    bool Add(string email, object? data);
    object? Get(string email);
    bool Delete(string email);
}