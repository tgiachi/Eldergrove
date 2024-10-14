using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Json.Dialogs;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class DialogService : IDialogService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, DialogObject> _dialogs = new();

    private readonly ITextService _textService;

    public DialogService(ILogger<DialogService> logger, IDataLoaderService dataLoaderService, ITextService textService)
    {
        _logger = logger;
        _textService = textService;

        dataLoaderService.SubscribeData<DialogObject>(OnDialogObject);
    }


    private Task OnDialogObject(DialogObject arg)
    {
        AddDialog(arg);
        return Task.CompletedTask;
    }

    public List<DialogObject> StartDialog(string dialogId)
    {
        if (!_dialogs.TryGetValue(dialogId, out var dialog))
        {
            _logger.LogError("Dialog {Id} not found", dialogId);
            throw new KeyNotFoundException($"Dialog {dialogId} not found");
        }

        return dialog.Options;
    }

    public List<DialogObject> ContinueDialog(DialogObject selectedDialog)
    {
        if (selectedDialog.Action != null)
        {
        }

        return selectedDialog.Options;
    }

    public void AddDialog(DialogObject dialogObject)
    {
        _logger.LogDebug("Loaded dialog {Id}", dialogObject.Id);
        _dialogs[dialogObject.Id] = dialogObject;
    }
}
