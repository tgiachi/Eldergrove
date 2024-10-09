using Eldergrove.Engine.Core.Interfaces.Services.Base;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IDataLoaderService : IEldergroveService
{
    void AddDataType<T>(string name) where T : class;

    void SubscribeData<T>(Func<T,Task> action) where T : class;

    void LoadData(string content);
}
