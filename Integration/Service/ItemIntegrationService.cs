using System.Collections.Concurrent;
using Integration.Common;
using Integration.Backend;

namespace Integration.Service;

public sealed class ItemIntegrationService
{
    // Using a concurrent dictionary to support multi-threading.
    private static readonly ConcurrentDictionary<string, bool> LocalCache = new();

    //This is a dependency that is normally fulfilled externally.
    private ItemOperationBackend ItemIntegrationBackend { get; set; } = new();


    // This is called externally and can be called multithreaded, in parallel.
    // More than one item with the same content should not be saved. However,
    // calling this with different contents at the same time is OK, and should
    // be allowed for performance reasons.
    public Result SaveItem(string itemContent)
    {

        if (ItemIntegrationBackend.FindItemsWithContent(itemContent).Count != 0 || LocalCache.ContainsKey(itemContent))
        {
            return new Result(false, $"Duplicate item received with content {itemContent}.");
        }

        LocalCache.TryAdd(itemContent, true);

        var item = ItemIntegrationBackend.SaveItem(itemContent);

        LocalCache.TryRemove(itemContent, out _);

        return new Result(true, $"Item with content {itemContent} saved with id {item.Id}");
    }

    public List<Item> GetAllItems()
    {
        return ItemIntegrationBackend.GetAllItems();
    }
}