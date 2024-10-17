namespace Eldergrove.Engine.Core.Attributes.Actions;

[AttributeUsage(AttributeTargets.Class)]
public class KeybindingActionAttribute : Attribute
{

    public string Context { get; set; }
    public string Key { get; }

    public KeybindingActionAttribute(string context,  string key)
    {
        Context = context;
        Key = key;


        
    }

}
