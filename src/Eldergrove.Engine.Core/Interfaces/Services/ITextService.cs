namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface ITextService
{
    void AddText(string id, string text);

    string GetText(string id);
}
