using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Json.Texts;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class TextService : ITextService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, string> _texts = new();

    private readonly IVariablesService _variablesService;

    public TextService(ILogger<TextService> logger, DataLoaderService dataLoaderService, IVariablesService variablesService)
    {
        _logger = logger;
        _variablesService = variablesService;

        dataLoaderService.SubscribeData<TextObject>(OnTextObject);
    }

    private Task OnTextObject(TextObject arg)
    {
        AddText(arg.Id, arg.Text);
        return Task.CompletedTask;
    }

    public void AddText(string id, string text)
    {
        _logger.LogDebug("Loaded text {Id}: {Text}", id, text);
        _texts[id] = text;
    }

    public string GetText(string id)
    {
        if (!_texts.TryGetValue(id, out var text))
        {
            _logger.LogError("Text {Id} not found", id);
            throw new KeyNotFoundException($"Text {id} not found");
        }

        return _variablesService.TranslateText(text);
    }
}
