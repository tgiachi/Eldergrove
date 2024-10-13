namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IVariablesService
{

    public void AddVariableBuilder(string variableName, Func<object> builder);

    public void AddVariable(string variableName, object value);

    string TranslateText(string text);
}
