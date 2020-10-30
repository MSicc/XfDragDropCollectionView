using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropTest.Interfaces
{
    public interface IDrop
    {
        event EventHandler<IDrop> HasItemDraggingOver;
        event EventHandler HasBeenSelectedAsDropTarget;

        bool AllowDrop { get; set; }

        bool IsCurrentDropTarget { get; }

        ICommand DragOverCommand { get; }

        ICommand DropCommand { get; }
    }
}
