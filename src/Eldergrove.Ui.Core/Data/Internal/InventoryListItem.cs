using Eldergrove.Ui.Core.Interfaces.Controls;

namespace Eldergrove.Ui.Core.Data.Internal;

public class InventoryListItem : IIconListItem
{
    public int Symbol { get; set; }
    public string Text { get; set; }

    public InventoryListItem(int symbol, string text)
    {
        Symbol = symbol;
        Text = text;
    }
}
