using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Interfaces.GameObjects;

public interface IGameObjectFactory<out TGameObject> where TGameObject : IGameObject
{
    TGameObject BuildGameObject(string id, Point position);
}
