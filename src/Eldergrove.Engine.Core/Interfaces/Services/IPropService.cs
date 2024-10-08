using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services.Base;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IPropService : IEldergroveService
{
    void AddProp(PropObject prop);

    PropObject GetPropById(string id);

}
