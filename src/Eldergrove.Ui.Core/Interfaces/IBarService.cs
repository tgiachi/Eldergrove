using Eldergrove.Engine.Core.Data.Json.Bars;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Ui.Core.Data.Internal;

namespace Eldergrove.Ui.Core.Interfaces;

public interface IBarService
{
    void AddBarObject(BarObject barObject);

    void BuildBar(string id, Action<List<BarColoredObject>> callback);

    string GetBarFromPosition(BarPositionType positionType);
}
