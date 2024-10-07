using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Interfaces.Services.Base;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IPropService : IEldergroveService
{
    void AddProp(PropObject prop);

    PropObject GetProp(string id);


}
