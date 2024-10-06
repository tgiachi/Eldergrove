using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class ColorsModule
{
    private readonly IColorService _colorService;

    public ColorsModule(IColorService colorService)
    {
        _colorService = colorService;
    }

    [ScriptFunction("add_color")]
    public void AddColor(string colorName, params int[] value)
    {
        if (value.Length == 3)
        {
            _colorService.AddColor(colorName, (byte)value[0], (byte)value[1], (byte)value[2]);
            return;
        }

        if (value.Length == 4)
        {
            _colorService.AddColor(colorName, (byte)value[0], (byte)value[1], (byte)value[2], (byte)value[3]);
        }


        throw new ArgumentException("Invalid color value");
    }
}
