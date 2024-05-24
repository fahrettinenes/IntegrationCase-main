using Integration.Common;
using Integration.Backend;

namespace Integration.Service;

public sealed class ItemIntegrationService
{
    // Using an external cache service to minimize the ItemIntegrationService class as soon as possible and support the SOLID principles.
    private readonly CacheService _cacheService = new("localhost");

    //This is a dependency that is normally fulfilled externally.
    private ItemOperationBackend ItemIntegrationBackend { get; set; } = new();


    // This is called externally and can be called multithreaded, in parallel.
    // More than one item with the same content should not be saved. However,
    // calling this with different contents at the same time is OK, and should
    // be allowed for performance reasons.
    public Result SaveItem(string itemContent)
    {
        // Disposing the lock after the flow.
        using var redlock = _cacheService.CreateLock(itemContent);

        if (ItemIntegrationBackend.FindItemsWithContent(itemContent).Count != 0)
        {
            return new Result(false, $"Duplicate item received with content {itemContent}.");
        }

        if (!redlock.IsAcquired)
        {
            return new Result(false, $"Couldn't lock the {itemContent}.");
        }

        var item = ItemIntegrationBackend.SaveItem(itemContent);

        return new Result(true, $"Item with content {itemContent} saved with id {item.Id}");
    }

    public List<Item> GetAllItems()
    {
        return ItemIntegrationBackend.GetAllItems();
    }
}