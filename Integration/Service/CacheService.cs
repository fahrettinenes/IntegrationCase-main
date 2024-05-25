using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Integration.Service;

public class CacheService
{
    // Using the redlock algorithm to provide distrubited locking.
    private readonly RedLockFactory _redlockFactory;

    public CacheService(string redisConnectionString)
    {
        var multiplexer = new List<RedLockMultiplexer>
        {
            ConnectionMultiplexer.Connect(redisConnectionString)
        };

        _redlockFactory = RedLockFactory.Create(multiplexer);
    }
    
    public IRedLock CreateLock(string content)
    {
        // 10 seconds of delaying is all up to transaction. In this case, 10 seconds of delay will be enough.
        // RedLock is adding a prefix for each log automatically, that's why I didn't prefer to create a new prefix.
        return _redlockFactory.CreateLock(content, TimeSpan.FromSeconds(10));
    }
}
