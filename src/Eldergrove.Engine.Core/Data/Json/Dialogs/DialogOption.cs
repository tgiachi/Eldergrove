namespace Eldergrove.Engine.Core.Data.Json.Dialogs;

public class DialogOption
{
    public string OptionText { get; set; }
    public int NextNodeId { get; set; }
    public string? Action { get; set; }

    public DialogOption(string optionText, int nextNodeId, string? action = null)
    {
        OptionText = optionText;
        NextNodeId = nextNodeId;
        Action = action;
    }
}
