using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Ui.Core.Interfaces.Controls;

namespace Eldergrove.Ui.Core.Data.Internal;

public class InventoryListItem : IIconListItem
{
    public int Symbol { get; set; }
    public string Text { get; set; }

    public ItemGameObject ItemGameObject { get; set; }

    public InventoryListItem(int symbol, string text, ItemGameObject itemGameObject)
    {
        Symbol = symbol;
        Text = text;

        ItemGameObject = itemGameObject;
    }
}
