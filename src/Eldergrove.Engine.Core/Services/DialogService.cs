using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Json.Dialogs;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class DialogService : IDialogService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, DialogObject> _dialogs = new();


    public DialogService(ILogger<DialogService> logger, IDataLoaderService dataLoaderService)
    {
        _logger = logger;

        dataLoaderService.SubscribeData<DialogObject>(OnDialogObject);
    }


    private Task OnDialogObject(DialogObject arg)
    {
        return Task.CompletedTask;
    }

    public List<string> StartDialog(string dialogId)
    {
        return new List<string>();
    }

    public List<string> ContinueDialog(string dialogId, string optionId)
    {
        return new List<string>();
    }
}
