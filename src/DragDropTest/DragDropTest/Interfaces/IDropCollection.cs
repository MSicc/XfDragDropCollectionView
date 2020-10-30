using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DragDropTest.Interfaces
{

    public interface IDropCollection : IDrop
    {
        ObservableCollection<IDropCollectionItem> Items { get; set; }
    }

    public interface IDropCollectionItem : IDrag
    {
        public IDropCollection ParentCollection { get; set; }

        public int Order { get; set; }
    }
}
