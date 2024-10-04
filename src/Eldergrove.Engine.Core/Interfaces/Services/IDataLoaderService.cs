using Eldergrove.Engine.Core.Interfaces.Services.Base;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IDataLoaderService : IEldergroveService
{
    void AddDataType<T>(string name) where T : class;

    void SubscribeData<T>(Func<IEnumerable<T>,Task> action) where T : class;
}
