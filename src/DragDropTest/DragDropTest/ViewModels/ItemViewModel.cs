using DragDropTest.Controls;
using DragDropTest.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragDropTest.ViewModels
{
    public class ItemViewModel : ObservableObject, IDrag, IDrop, IScrollItem
    {
        private int _order;

        private bool _enableDrag;
        private bool _isCurrentlyDragged;

        private bool _allowDrop;


        private Command _dragStartingCommand;
        private Command _dropCompletedCommand;

        private Command _dropCommand;
        private Command _dragOverCommand;

        public event EventHandler<ItemViewModel> HasStartedDragging;
        public event EventHandler<ItemViewModel> HasBeenDropped;
        public event EventHandler<ItemViewModel> HasItemDraggingOver;
        public event EventHandler HasBeenSelectedAsDropTarget;

        public ItemViewModel(string title, int order, bool enableDrag, bool allowDrop, DropCollectionViewModel parent)
        {
            this.Title = title;
            this.Order = order;
            this.Parent = parent;
            this.EnableDrag = enableDrag;
            this.AllowDrop = allowDrop;

            //this should normally be everything we need to adjust the scrolling, but
            //there is an unresolved bug           
            //https://github.com/xamarin/Xamarin.Forms/issues/9127
            //https://github.com/xamarin/Xamarin.Forms/issues/7788

            this.ScrollToConfig = new ScrollToConfiguration() { Animated = true, ScrollToPosition = ScrollToPosition.MakeVisible };
        }


        public void ExecuteDragStarting(object o)
        {
            this.IsCurrentlyDragged = true;
            this.HasStartedDragging?.Invoke(this, this);
        }

        public void ExecuteDraggingOver(object o) =>
            HasItemDraggingOver(this, (ItemViewModel)o);


        public void ExecuteDrop(object o)
            => this.HasBeenSelectedAsDropTarget(this, EventArgs.Empty);

        public void ExecuteDropCompleted(object o)
        {
            this.IsCurrentlyDragged = false;
            this.HasBeenDropped?.Invoke(this, this);
        }


        public bool EnableDrag { get => _enableDrag; set => Set(ref _enableDrag, value); }

        public bool IsCurrentlyDragged { get => _isCurrentlyDragged; set => Set(ref _isCurrentlyDragged, value); }

        public bool AllowDrop { get => _allowDrop; set => Set(ref _allowDrop, value); }




        public string Title { get; set; }
        public int Order { get => _order; set => Set(ref _order, value); }
        public DropCollectionViewModel Parent { get; set; }
        public ScrollToConfiguration ScrollToConfig { get; set; }

        public ICommand DragStartingCommand => _dragStartingCommand ??= new Command((o) => ExecuteDragStarting(o));
        public ICommand DropCompletedCommand => _dropCompletedCommand ??= new Command((o) => ExecuteDropCompleted(o));


        public ICommand DragOverCommand => _dragOverCommand ??= new Command((o) => ExecuteDraggingOver(o));
        public ICommand DropCommand => _dropCommand ??= new Command((o) => ExecuteDrop(o));


    }
}
