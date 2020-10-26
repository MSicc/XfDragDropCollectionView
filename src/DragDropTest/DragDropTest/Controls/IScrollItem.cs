using System;
using System.Collections.Generic;
using System.Text;

namespace DragDropTest.Controls
{
    public interface IConfigurableScrollItem
    {
        ScrollToConfiguration ScrollToConfig { get; set; }
    }

    public interface IScrollItem : IConfigurableScrollItem
    {
    }


    public interface IGroupScrollItem : IConfigurableScrollItem
    {
        object GroupValue { get; set; }
    }
}
