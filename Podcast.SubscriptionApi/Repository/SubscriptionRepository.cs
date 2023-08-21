using System.Collections.Concurrent;

namespace Podcast.SubscriptionApi.Repository;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ConcurrentDictionary<string, object?> _subscriptions = new();


    public bool Add(string email, object? data)
    {
        return _subscriptions.TryAdd(email, data);
    }

    public object? Get(string email)
    {
        return _subscriptions.TryGetValue(email, out var subscription) ? subscription : null;
    }

    public bool Delete(string email)
    {
        return _subscriptions.Remove(email, out _);
    }
}