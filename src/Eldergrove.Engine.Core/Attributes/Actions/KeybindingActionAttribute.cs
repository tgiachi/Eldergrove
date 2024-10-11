namespace Eldergrove.Engine.Core.Attributes.Actions;

[AttributeUsage(AttributeTargets.Class)]
public class KeybindingActionAttribute : Attribute
{
    public string Key { get; }

    public KeybindingActionAttribute(string key)
    {
        Key = key;
    }

}
