using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropTest.Interfaces
{
    public interface IDrop
    {
        event EventHandler HasBeenSelectedAsDropTarget;

        event EventHandler<object> HasItemDraggingOver;

        bool AllowDrop { get; set; }

        ICommand DragOverCommand { get; }

        ICommand DropCommand { get; }
    }
}
