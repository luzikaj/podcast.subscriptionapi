using Podcast.SubscriptionApi.Repository;

namespace Podcast.SubscriptionApi.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _repository;

    public SubscriptionService(ISubscriptionRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> ExistsAsync(string email)
    {
        return _repository.Get(email) != null;
    }

    public async Task<bool> AddAsync(string email)
    {
        return _repository.Add(email, new object());
    }

    public async Task<bool> RemoveAsync(string email)
    {
        return _repository.Delete(email);
    }
}