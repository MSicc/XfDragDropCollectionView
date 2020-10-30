using DragDropTest.Controls;
using DragDropTest.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragDropTest.ViewModels
{
    public class ItemViewModel : DragDropCollectionItemViewModelBase, IScrollItem
    {
        public ItemViewModel(string title, bool enableDrag, bool allowDrop, DropCollectionViewModelBase parent) : base(parent)
        {
            this.Title = title;
            this.EnableDrag = enableDrag;
            this.AllowDrop = allowDrop;

            //this should normally be everything we need to adjust the scrolling, but
            //there is an unresolved bug           
            //https://github.com/xamarin/Xamarin.Forms/issues/9127
            //https://github.com/xamarin/Xamarin.Forms/issues/7788

            this.ScrollToConfig = new ScrollToConfiguration() { Animated = true, ScrollToPosition = ScrollToPosition.MakeVisible };
        }

        public string Title { get; set; }

        public ScrollToConfiguration ScrollToConfig { get; set; }

    }
}
