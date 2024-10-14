using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class TextServiceModule
{
    private readonly ITextService _textService;

    public TextServiceModule(ITextService textService)
    {
        _textService = textService;
    }

    [ScriptFunction("add_text")]
    public void AddText(string id, string text)
    {
        _textService.AddText(id, text);
    }


    [ScriptFunction("get_text")]
    public string GetText(string id) => _textService.GetText(id);
}
