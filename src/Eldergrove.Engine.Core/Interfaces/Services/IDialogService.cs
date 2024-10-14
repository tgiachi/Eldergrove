namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IDialogService
{
    List<string> StartDialog(string dialogId);

    List<string> ContinueDialog(string dialogId, string optionId);

}
