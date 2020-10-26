using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropTest.Interfaces
{
    public interface IDrag
    {
        bool EnableDrag { get; set; }

        bool IsCurrentlyDragged { get; set; }

        ICommand DragStartingCommand { get; }

        ICommand DropCompletedCommand { get; }
    }
}
