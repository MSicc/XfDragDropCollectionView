using DragDropTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragDropTest.ViewModels
{
    public abstract class DragViewModelBase : ObservableObject, IDrag
    {
        public event EventHandler<IDrag> HasStartedDragging;
        public event EventHandler<IDrag> HasBeenDropped;

        private bool _enableDrag;
        private bool _isCurrentlyDragged;

        private Command _dragStartingCommand;
        private Command _dropCompletedCommand;


        public DragViewModelBase()
        {
        }


#pragma warning disable IDE0060 // Remove unused parameter
        public virtual void ExecuteDragStarting(object o)
        {
            this.IsCurrentlyDragged = true;
            this.HasStartedDragging?.Invoke(this, this);
        }

        public virtual void ExecuteDropCompleted(object o)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            this.IsCurrentlyDragged = false;
            this.HasBeenDropped?.Invoke(this, this);
        }

        public bool EnableDrag { get => _enableDrag; set => Set(ref _enableDrag, value); }
        public bool IsCurrentlyDragged { get => _isCurrentlyDragged; set => Set(ref _isCurrentlyDragged, value); }


        public ICommand DragStartingCommand => _dragStartingCommand ??= new Command((o) => ExecuteDragStarting(o));
        public ICommand DropCompletedCommand => _dropCompletedCommand ??= new Command((o) => ExecuteDropCompleted(o));
    }

    public abstract class DropCollectionItemViewModelBase : DragViewModelBase, IDropCollectionItem
    {

        public DropCollectionItemViewModelBase(IDropCollection parentCollection) =>
            this.ParentCollection = parentCollection;

        public int Order { get; set; }

        public IDropCollection ParentCollection { get; set; }

    }


    public abstract class DragDropViewModelBase : DragViewModelBase, IDrop
    {
        private bool _allowDrop;

        private Command _dropCommand;
        private Command _dragOverCommand;

        public event EventHandler<IDrop> HasItemDraggingOver;
        public event EventHandler HasBeenSelectedAsDropTarget;

        public DragDropViewModelBase()
        {

        }

        public void ExecuteDraggingOver(object o) =>
            HasItemDraggingOver?.Invoke(this, (IDrop)o);

        public void ExecuteDrop(object o)
            => this.HasBeenSelectedAsDropTarget?.Invoke(this, EventArgs.Empty);



        public bool AllowDrop { get => _allowDrop; set => Set(ref _allowDrop, value); }

        public bool IsCurrentDropTarget { get; internal set; }

        public ICommand DragOverCommand => _dragOverCommand ??= new Command((o) => ExecuteDraggingOver(o));
        public ICommand DropCommand => _dropCommand ??= new Command((o) => ExecuteDrop(o));


    }


    public abstract class DragDropCollectionItemViewModelBase : DragDropViewModelBase, IDropCollectionItem
    {

        public DragDropCollectionItemViewModelBase(IDropCollection parentCollection) =>
            this.ParentCollection = parentCollection;

        public int Order { get; set; }

        public IDropCollection ParentCollection { get; set; }

    }
}
