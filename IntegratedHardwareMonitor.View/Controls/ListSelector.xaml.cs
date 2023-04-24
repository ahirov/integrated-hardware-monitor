using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using IntegratedHardwareMonitor.Common.Entities;

namespace IntegratedHardwareMonitor.View.Controls
{
    /// <summary>
    /// Interaction logic for ListSelector.xaml
    /// </summary>
    public partial class ListSelector : UserControl
    {
        private readonly int _nullIndex = -1;
        public static DependencyProperty SelectedItemsProperty { get; }
        public static DependencyProperty TotalItemsProperty { get; }

        public ObservableCollection<HardwareComponent> SelectedItems
        {
            get => (ObservableCollection<HardwareComponent>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        public ObservableCollection<HardwareComponent> TotalItems
        {
            get => (ObservableCollection<HardwareComponent>)GetValue(TotalItemsProperty);
            set => SetValue(TotalItemsProperty, value);
        }

        static ListSelector()
        {
            SelectedItemsProperty = DependencyProperty.Register(
                nameof(SelectedItems),
                typeof(ObservableCollection<HardwareComponent>),
                typeof(ListSelector),
                new FrameworkPropertyMetadata(null));
            TotalItemsProperty = DependencyProperty.Register(
                nameof(TotalItems),
                typeof(ObservableCollection<HardwareComponent>),
                typeof(ListSelector),
                new FrameworkPropertyMetadata(null));
        }

        public ListSelector()
        {
            InitializeComponent();
        }

        private void OnChangeSelectedItemsLstBx(object sender, RoutedEventArgs args)
        {
            if (SelectedItemsLstBx.SelectedIndex != _nullIndex)
            {
                TotalItemsLstBx.SelectedIndex = _nullIndex;
                ToggleButtons(false, true, true, true);
            }
        }

        private void OnChangeTotalItemsLstBx(object sender, RoutedEventArgs args)
        {
            if (TotalItemsLstBx.SelectedIndex != _nullIndex)
            {
                SelectedItemsLstBx.SelectedIndex = _nullIndex;
                ToggleButtons(true);
            }
        }

        private void OnClickSelectBtn(object sender, RoutedEventArgs args)
        {
            HardwareComponent item = (HardwareComponent)TotalItemsLstBx.SelectedValue;
            SelectedItems.Add(item);
            _ = TotalItems.Remove(item);
            ToggleButtons();
        }

        private void OnClickDeselectBtn(object sender, RoutedEventArgs args)
        {
            HardwareComponent item = (HardwareComponent)SelectedItemsLstBx.SelectedValue;
            HardwareComponent previousItem = TotalItems
                .Where(i => i.Position < item.Position)
                .MaxBy(i => i.Position);
            int index = previousItem != null
                ? TotalItems.IndexOf(previousItem) + 1
                : 0;
            TotalItems.Insert(index, item);
            _ = SelectedItems.Remove(item);
            ToggleButtons();
        }

        private void OnClickSortUpBtn(object sender, RoutedEventArgs args)
        {
            int index = SelectedItemsLstBx.SelectedIndex;
            Move(index, index - 1);
        }

        private void OnClickSortDownBtn(object sender, RoutedEventArgs args)
        {
            int index = SelectedItemsLstBx.SelectedIndex;
            Move(index, index + 1);
        }

        private void Move(int oldIndex, int newIndex)
        {
            // check collection bounds
            if (newIndex >= 0 && newIndex < SelectedItems.Count)
            {
                SelectedItems.Move(oldIndex, newIndex);
            }
        }

        private void ToggleButtons(bool isSelect = false, bool isDeselect = false,
            bool isSortUp = false, bool isSortDown = false)
        {
            SelectBtn.IsEnabled = isSelect;
            DeselectBtn.IsEnabled = isDeselect;

            SortUpBtn.IsEnabled = isSortUp;
            SortDownBtn.IsEnabled = isSortDown;
        }
    }
}
