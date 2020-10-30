using DragDropTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropTest.Interfaces
{
    public interface IDrag
    {
        event EventHandler<IDrag> HasStartedDragging;
        event EventHandler<IDrag> HasBeenDropped;


        bool EnableDrag { get; set; }

        bool IsCurrentlyDragged { get; set; }

        ICommand DragStartingCommand { get; }

        ICommand DropCompletedCommand { get; }
    }
}
