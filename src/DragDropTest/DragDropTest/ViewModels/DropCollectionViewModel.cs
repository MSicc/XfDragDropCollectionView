using DragDropTest.Controls;
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
    public class DropCollectionViewModel : DropCollectionViewModelBase
    {
        private IScrollItem _scrollToVm;

        public DropCollectionViewModel()
        {
            this.AllowDrop = true;
        }

        public override void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsCollectionChanged(sender, e);

            if (e.Action == NotifyCollectionChangedAction.Add && this.IsCurrentDropTarget)
            {
                this.ScrollToVm = (IScrollItem)this.Items[e.NewStartingIndex];

                this.IsCurrentDropTarget = false;
            }
        }


        public override void ExecuteDraggingOver(object o)
        {
            base.ExecuteDraggingOver(o);
        }

        public override void ExecuteDrop(object o)
        {
            base.ExecuteDrop(o);
        }


        public IScrollItem ScrollToVm { get => _scrollToVm; set => Set(ref _scrollToVm, value); }
    }

}
