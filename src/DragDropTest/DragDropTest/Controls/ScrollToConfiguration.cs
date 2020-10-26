using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DragDropTest.Controls
{
    public class ScrollToConfiguration
    {
        public bool Animated { get; set; } = true;

        public ScrollToPosition ScrollToPosition { get; set; } = ScrollToPosition.Center;
    }
}
