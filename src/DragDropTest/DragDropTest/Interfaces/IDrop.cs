using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropTest.Interfaces
{
    public interface IDrop
    {
        bool AllowDrop { get; set; }

        ICommand DragOverCommand { get; }

        ICommand DropCommand { get; }
    }
}
