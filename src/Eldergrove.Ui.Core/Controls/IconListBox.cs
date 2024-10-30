using System.Collections.ObjectModel;
using Eldergrove.Ui.Core.Controls.Base;
using Eldergrove.Ui.Core.Interfaces.Controls;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace Eldergrove.Ui.Core.Controls;

public class IconListBox<TItem> : BaseGuiControl where TItem : IIconListItem
{
    private readonly IFont _symbolFont;
    public ObservableCollection<TItem> Items { get; set; } = new();

    public int SelectedIndex { get; set; }

    public delegate void ItemSelectedHandler(TItem item);

    public delegate void SelectedIndexChangedHandler(TItem item);

    private Color SelectedForeground { get; set; } = Color.Yellow;

    public event ItemSelectedHandler? ItemSelected;

    public event SelectedIndexChangedHandler? SelectedIndexChanged;


    public IconListBox(Point size, IFont symbolFont) : base(size)
    {
        _symbolFont = symbolFont;
        Items.CollectionChanged += (sender, args) => { Draw(); };

        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(SelectedIndex))
            {
                SelectedIndexChanged?.Invoke(Items[SelectedIndex]);

                Draw();
            }
        };
    }


    public override void Update(TimeSpan delta)
    {
    }

    private void Draw()
    {
        this.Clear();
        for (var i = 0; i < Items.Count; i++)
        {
            var item = Items[i];
            var foreground = i == SelectedIndex ? Color.Yellow : Color.White;

            this.Print(2, i, item.Text, foreground, Color.Transparent);
        }
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        if (keyboard.IsKeyPressed(Keys.Up))
        {
            if (SelectedIndex == 0)
            {
                SelectedIndex = Items.Count - 1;
            }
            else
            {
                SelectedIndex--;
            }

            return true;
        }

        if (keyboard.IsKeyPressed(Keys.Down))
        {
            if (SelectedIndex == Items.Count - 1)
            {
                SelectedIndex = 0;
            }
            else
            {
                SelectedIndex++;
            }

            return true;
        }

        if (keyboard.IsKeyPressed(Keys.Enter))
        {
            var item = Items[SelectedIndex];

            ItemSelected?.Invoke(item);

            Close();


            return true;
        }

        return base.ProcessKeyboard(keyboard);
    }

    // public override bool ProcessMouse(MouseScreenObjectState state)
    // {
    //     //     if (state.Mouse.LeftClicked)
    //     //     {
    //     //         SelectedIndex = state.CellPosition.Y;
    //     //         return true;
    //     //     }
    //     //
    //     //     return base.ProcessMouse(state);
    //     // }
    // }
}
