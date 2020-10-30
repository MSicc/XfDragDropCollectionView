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
                var itemVm = new ItemViewModel($"Item {i}", true, true, this.TopCollection);
                this.TopCollection.Items.Add(itemVm);

                foreach (ItemViewModel item in this.TopCollection.Items)
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
                    if (itemVm.ParentCollection == this.CurrentDraggingItemVm.ParentCollection &&
                        itemVm.Order == this.CurrentDraggingItemVm.Order)
                    {
                        this.CurrentDraggingItemVm = null;
                        return;
                    }

                    this.CurrentDraggingItemVm.ParentCollection.Items.Remove(this.CurrentDraggingItemVm);


                    if (itemVm.ParentCollection != this.CurrentDraggingItemVm.ParentCollection) //different parent
                    {
                        if (itemVm == itemVm.ParentCollection.Items.Last())
                            itemVm.ParentCollection.Items.Add(this.CurrentDraggingItemVm);
                        else
                            itemVm.ParentCollection.Items.Insert(itemVm.Order, this.CurrentDraggingItemVm);
                    }
                    else //same parent
                    {
                        if (itemVm.Order == this.CurrentDraggingItemVm.Order)
                            itemVm.ParentCollection.Items.Insert(itemVm.Order + 1, this.CurrentDraggingItemVm);
                        else if (itemVm == itemVm.ParentCollection.Items.Last() &&
                            this.CurrentDraggingItemVm.Order < itemVm.ParentCollection.Items.Count)
                            itemVm.ParentCollection.Items.Add(this.CurrentDraggingItemVm);
                        else
                            itemVm.ParentCollection.Items.Insert(itemVm.Order, this.CurrentDraggingItemVm);
                    }
                }
            }

            if (sender is DropCollectionViewModel dropCollectionVm)
            {
                if (this.CurrentDraggingItemVm != null)
                {
                    this.CurrentDraggingItemVm.ParentCollection.Items.Remove(this.CurrentDraggingItemVm);
                    dropCollectionVm.Items.Add(this.CurrentDraggingItemVm);
                }
            }

            this.CurrentDraggingItemVm = null;

        }


        private void HandleDropTargetHasItemDraggingOver(object sender, object args)
        {
            //perform any actions related to dragging over
        }

        private void HandleAnItemHasBeenDropped(object sender, IDrag args)
        {
            //perform any action after all drag and drop actions have finished
        }

        private void HandleAnItemVmIsBeingDragged(object sender, IDrag args) =>
            this.CurrentDraggingItemVm = (ItemViewModel)args;


        public DropCollectionViewModel TopCollection { get; set; }

        public DropCollectionViewModel BottomCollection { get; set; }

        public ItemViewModel CurrentDraggingItemVm { get; set; }
    }
}
