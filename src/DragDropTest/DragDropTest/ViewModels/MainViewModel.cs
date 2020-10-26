using DragDropTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragDropTest.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            this.TopCollection = new DropCollectionViewModel();
            this.TopCollection.HasBeenSelectedAsDropTarget += HandleDropTargetHasBeenSelected;
            this.TopCollection.HasItemDraggingOver += HandleDropTargetHasItemDraggingOver;

            this.BottomCollection = new DropCollectionViewModel();
            this.BottomCollection.HasBeenSelectedAsDropTarget += HandleDropTargetHasBeenSelected;
            this.BottomCollection.HasItemDraggingOver += HandleDropTargetHasItemDraggingOver;

            for (int i = 0; i < 10; i++)
            {
                this.TopCollection.ItemVms.Add(new ItemViewModel($"Item {i}", i, true, true, this.TopCollection));
                foreach (var item in this.TopCollection.ItemVms)
                {
                    item.HasStartedDragging += HandleAnItemVmIsBeingDragged;
                    item.HasBeenDropped += HandleAnItemHasBeenDropped;
                    item.HasItemDraggingOver += HandleDropTargetHasItemDraggingOver;
                    item.HasBeenSelectedAsDropTarget += HandleDropTargetHasBeenSelected;
                }
            }
        }





        private void HandleDropTargetHasBeenSelected(object sender, EventArgs args)
        {
            if (sender is ItemViewModel itemVm)
            {
                if (this.CurrentDraggingItemVm != null)
                {
                    //preventing any action if we are not dragging far enough
                    if (itemVm.Parent == this.CurrentDraggingItemVm.Parent &&
                        itemVm.Order == this.CurrentDraggingItemVm.Order)
                    {
                        this.CurrentDraggingItemVm = null;
                        return;
                    }

                    this.CurrentDraggingItemVm.Parent.ItemVms.Remove(this.CurrentDraggingItemVm);


                    if (itemVm.Parent != this.CurrentDraggingItemVm.Parent) //different parent
                    {
                        if (itemVm == itemVm.Parent.ItemVms.Last())
                            itemVm.Parent.ItemVms.Add(this.CurrentDraggingItemVm);
                        else
                            itemVm.Parent.ItemVms.Insert(itemVm.Order, this.CurrentDraggingItemVm);
                    }
                    else //same parent
                    {
                        if (itemVm.Order == this.CurrentDraggingItemVm.Order)
                            itemVm.Parent.ItemVms.Insert(itemVm.Order + 1, this.CurrentDraggingItemVm);
                        else if (itemVm == itemVm.Parent.ItemVms.Last() &&
                            this.CurrentDraggingItemVm.Order < itemVm.Parent.ItemVms.Count)
                            itemVm.Parent.ItemVms.Add(this.CurrentDraggingItemVm);
                        else
                            itemVm.Parent.ItemVms.Insert(itemVm.Order, this.CurrentDraggingItemVm);
                    }
                }
            }

            if (sender is DropCollectionViewModel dropCollectionVm)
            {
                if (this.CurrentDraggingItemVm != null)
                {
                    this.CurrentDraggingItemVm.Parent.ItemVms.Remove(this.CurrentDraggingItemVm);
                    dropCollectionVm.ItemVms.Add(this.CurrentDraggingItemVm);
                }
            }

            this.CurrentDraggingItemVm = null;

        }


        private void HandleDropTargetHasItemDraggingOver(object sender, object args)
        {

        }

        private void HandleAnItemHasBeenDropped(object sender, ItemViewModel args)
        {
            //perform any action after all drag and drop actions have finished
        }

        private void HandleAnItemVmIsBeingDragged(object sender, ItemViewModel args) =>
            this.CurrentDraggingItemVm = args;


        public DropCollectionViewModel TopCollection { get; set; }

        public DropCollectionViewModel BottomCollection { get; set; }

        public ItemViewModel CurrentDraggingItemVm { get; set; }
    }
}
