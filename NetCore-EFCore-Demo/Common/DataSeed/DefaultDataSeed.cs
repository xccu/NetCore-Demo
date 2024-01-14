namespace Common.DataSeed;

internal class DefaultDataSeed : IDataSeed
{
    private IEnumerable<IDataSeedProvider> _providers;

    public DefaultDataSeed(IEnumerable<IDataSeedProvider> providers)
    {
        _providers = providers;
    }

    public async Task ExecuteAsync()
    {
        foreach (var provider in _providers)
        {
            provider.EnsureDatabaseCreated();
            provider.EnsureTablesCreated();
            await provider.EnsureDataCreatedAsync();
        }
    }
}