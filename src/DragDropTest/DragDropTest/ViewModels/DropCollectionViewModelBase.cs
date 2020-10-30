using DragDropTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragDropTest.ViewModels
{
    public class DropCollectionViewModelBase : ObservableObject, IDropCollection
    {
        private bool _allowDrop;

        private Command _dropCommand;
        private Command _dragOverCommand;

        public event EventHandler<IDrop> HasItemDraggingOver;
        public event EventHandler HasBeenSelectedAsDropTarget;

        public DropCollectionViewModelBase()
        {
            this.Items = new ObservableCollection<IDropCollectionItem>();
            this.Items.CollectionChanged += OnItemsCollectionChanged;
        }

        public virtual void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in this.Items)
            {
                item.ParentCollection = this;
                item.Order = this.Items.IndexOf(item);
            }
        }


        public virtual void ExecuteDraggingOver(object o) => this.HasItemDraggingOver?.Invoke(this, (IDrop)o);


#pragma warning disable IDE0060 // Remove unused parameter
        public virtual void ExecuteDrop(object o)
        {
            this.IsCurrentDropTarget = true;
            this.HasBeenSelectedAsDropTarget?.Invoke(this, EventArgs.Empty);
        }
#pragma warning restore IDE0060 // Remove unused parameter



        public ObservableCollection<IDropCollectionItem> Items { get; set; }

        public bool AllowDrop { get => _allowDrop; set => Set(ref _allowDrop, value); }

        public bool IsCurrentDropTarget { get; internal set; }

        public ICommand DragOverCommand => _dragOverCommand ??= new Command((o) => ExecuteDraggingOver(o));

        public ICommand DropCommand => _dropCommand ??= new Command((o) => ExecuteDrop(o));
    }








}
