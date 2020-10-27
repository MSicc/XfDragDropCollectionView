using DragDropTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragDropTest.ViewModels
{
    public class DropCollectionViewModel : ObservableObject, IDrop
    {
        public event EventHandler HasBeenSelectedAsDropTarget;
        public event EventHandler<DropCollectionViewModel> HasItemDraggingOver;

        private bool _allowDrop;

        private Command _dropCommand;
        private Command _dragOverCommand;
        private ItemViewModel _scrollToVm;

        private bool _isCurrentDropTarget;

        public DropCollectionViewModel()
        {
            this.ItemVms = new ObservableCollection<ItemViewModel>();
            this.ItemVms.CollectionChanged += ItemVmsCollectionChanged;
        }

        private void ItemVmsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var itemVm in this.ItemVms)
            {
                itemVm.Parent = this;
                itemVm.Order = this.ItemVms.IndexOf(itemVm);
            }

            if (e.Action == NotifyCollectionChangedAction.Add && _isCurrentDropTarget)
            {
                this.ScrollToVm = this.ItemVms[e.NewStartingIndex];

                _isCurrentDropTarget = false;
            }
        }

        public void ExecuteDraggingOver(object o) => this.HasItemDraggingOver?.Invoke(this, (DropCollectionViewModel)o);

        public void ExecuteDrop(object o)
        {
            this._isCurrentDropTarget = true;
            this.HasBeenSelectedAsDropTarget?.Invoke(this, EventArgs.Empty);
        }

        public ObservableCollection<ItemViewModel> ItemVms { get; set; }

        public ItemViewModel ScrollToVm
        {
            get => _scrollToVm;
            set
            {
                Set(ref _scrollToVm, value);
            }
        }

        public bool AllowDrop { get => _allowDrop; set => Set(ref _allowDrop, value); }

        public ICommand DragOverCommand => _dragOverCommand ??= new Command((o) => ExecuteDraggingOver(o));
        public ICommand DropCommand => _dropCommand ??= new Command((o) => ExecuteDrop(o));

    }
}
