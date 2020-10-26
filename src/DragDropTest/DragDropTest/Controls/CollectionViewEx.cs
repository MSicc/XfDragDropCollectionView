using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DragDropTest.Controls
{
    public class CollectionViewEx : CollectionView
    {
        public static BindableProperty ScrollToItemWithConfigProperty = BindableProperty.Create(nameof(ScrollToItemWithConfig), typeof(IConfigurableScrollItem), typeof(CollectionViewEx), default(IConfigurableScrollItem), BindingMode.Default, propertyChanged: OnScrollToItemWithConfigPropertyChanged);

        public IConfigurableScrollItem ScrollToItemWithConfig
        {
            get => (IConfigurableScrollItem)GetValue(ScrollToItemWithConfigProperty);
            set => SetValue(ScrollToItemWithConfigProperty, value);
        }

        private static void OnScrollToItemWithConfigPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            if (bindable is CollectionViewEx current)
            {
                if (newValue is IGroupScrollItem scrollToItemWithGroup)
                {
                    if (scrollToItemWithGroup.ScrollToConfig == null)
                        scrollToItemWithGroup.ScrollToConfig = new ScrollToConfiguration();

                    current.ScrollTo(scrollToItemWithGroup, scrollToItemWithGroup.GroupValue, scrollToItemWithGroup.ScrollToConfig.ScrollToPosition, scrollToItemWithGroup.ScrollToConfig.Animated);

                }
                else if (newValue is IScrollItem scrollToItem)
                {
                    if (scrollToItem.ScrollToConfig == null)
                        scrollToItem.ScrollToConfig = new ScrollToConfiguration();

                    current.ScrollTo(scrollToItem, null, scrollToItem.ScrollToConfig.ScrollToPosition, scrollToItem.ScrollToConfig.Animated);
                }

            }
        }

    }
}
