using System.Collections.ObjectModel;
using Eldergrove.Ui.Core.Interfaces.Controls;
using SadConsole;
using Console = SadConsole.Console;

namespace Eldergrove.Ui.Core.Controls;

public class IconListBox<TItem> : Console where TItem : IIconListItem
{
    private readonly IFont _symbolFont;
    public ObservableCollection<TItem> Items { get; set; } = new();

    public int SelectedIndex { get; set; }

    public IconListBox(int width, int height, IFont symbolFont) : base(width, height)
    {
        _symbolFont = symbolFont;
        Items.CollectionChanged += (sender, args) => { Draw(); };
    }

    private void Draw()
    {
        this.Clear();
    }
}
