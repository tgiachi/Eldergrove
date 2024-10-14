using Eldergrove.Engine.Core.Data.Json.Dialogs;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IDialogService
{
    List<DialogObject> StartDialog(string dialogId);

    List<DialogObject> ContinueDialog(DialogObject selectedDialog);

    void AddDialog( DialogObject dialogObject);

}
