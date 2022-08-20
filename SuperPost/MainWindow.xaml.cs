using ChaoControls.Style;
using SuperPost.Entity;
using SuperPost.ViewModel;
using SuperPost.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperPost
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow
    {

        public MainViewModel vieModel;
        public MainWindow()
        {
            InitializeComponent();
            SidePanelWidth.Width = new GridLength(DEFAULT_SIDEPANEL_WIDTH);
            beforeHideIndex = 1;


            vieModel = new MainViewModel();
            this.DataContext = vieModel;
        }

        private bool _canChangeSidePanel;
        private bool canChangeSidePanel
        {
            get
            {
                return _canChangeSidePanel;
            }
            set
            {
                _canChangeSidePanel = value;
                if (value) dragRectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 127, 212));
                else dragRectangle.Fill = Brushes.Transparent;
            }
        }


        private Point sizeChangeBeginPoint;
        private double currentPanelWidth = 0;
        private double DEFAULT_SIDEPANEL_WIDTH = 300;
        private double MIN_SIDEPANEL_WIDTH = 200;
        private int beforeHideIndex = 0;
        private void SizeChangeBegin(object sender, MouseButtonEventArgs e)
        {
            currentPanelWidth = SidePanelWidth.ActualWidth;
            canChangeSidePanel = true;
            sizeChangeBeginPoint = PointToScreen(e.GetPosition(this));
        }

        private void SizeChangeEnd(object sender, MouseButtonEventArgs e)
        {
            canChangeSidePanel = false;
        }

        private void SizeChanging(object sender, MouseEventArgs e)
        {
            if (!canChangeSidePanel) return;
            Point p= PointToScreen(e.GetPosition(this));
            double width = p.X - sizeChangeBeginPoint.X;
            double targetWidth = currentPanelWidth + width;
            if (targetWidth >= MIN_SIDEPANEL_WIDTH && targetWidth<=this.Width/3*2)
            {
                SidePanelWidth.Width = new GridLength(currentPanelWidth + width);
               
                SideTabControl.SelectedIndex = beforeHideIndex;
            }
            else if(targetWidth <= MIN_SIDEPANEL_WIDTH/2)
            {
                hideSidePanel();
            }
        }


        private void hideSidePanel()
        {
            SidePanelWidth.Width = new GridLength(50);
            if (SideTabControl.SelectedIndex != 0) beforeHideIndex = SideTabControl.SelectedIndex;
            SideTabControl.SelectedIndex = 0;
        }





        private void ShowDrag(object sender, MouseEventArgs e)
        {
            dragRectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 127, 212));
        }

        private void HideDrag(object sender, MouseEventArgs e)
        {
            if(!canChangeSidePanel)
                dragRectangle.Fill = Brushes.Transparent;
        }


        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            canChangeSidePanel = false;
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SidePanelWidth.ActualWidth > this.Width / 3 * 2)
            {
                SidePanelWidth.Width = new GridLength(this.Width / 3 * 2);
            }
        }

        private void SideItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SideTabControl.SelectedIndex != 0) beforeHideIndex = SideTabControl.SelectedIndex;
            if (SidePanelWidth.ActualWidth<=MIN_SIDEPANEL_WIDTH/2)
                SidePanelWidth.Width = new GridLength(DEFAULT_SIDEPANEL_WIDTH);
        }


        private bool _toHideSidePanel = false;
        private bool ToHideSidePanel
        {
            get
            {
                return _toHideSidePanel;
            }
            set
            {
                _toHideSidePanel = value;
                if (value) hideSidePanel();
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            foreach (TabItem item in SideTabControl.Items)
            {
                item.PreviewMouseLeftButtonDown += handleTabItemClick;
            }

            ToggleStackPanel.Children.Clear();
            List<Project> projects = vieModel.Projects.ToList();
            foreach (var item in projects)
            {
                TogglePanel togglePanel = new TogglePanel();
                togglePanel.Style= Application.Current.FindResource("SideTogglePanel") as Style;
                togglePanel.Header = item.Name;
                //togglePanel.IsExpanded = true;
                if (item.Contents != null)
                {
                    foreach (var element in item.Contents)
                    {
                        RadioButton radioButton = new RadioButton();
                        radioButton.Content = element.Name;
                        radioButton.Style = Application.Current.FindResource("SideRadioButton") as Style;
                        togglePanel.Items.Add(radioButton);
                    }
                }
                ToggleStackPanel.Children.Add(togglePanel);
            }




            UIElementCollection collection = ToggleStackPanel.Children;
            List<UIElement> uIElements = new List<UIElement>();
            foreach (UIElement item in collection)
            {
                uIElements.Add(item);
            }
            moveAndSetPadding(uIElements,0+ ((System.Windows.Thickness)uIElements[0].GetValue(PaddingProperty)).Left);
        }


        private void moveAndSetPadding(List<UIElement> collection, double paddingLeft)
        {
            List<UIElement> togglePanels = new List<UIElement>();
            foreach (UIElement item in collection)
            {
                System.Windows.Thickness property = (System.Windows.Thickness)item.GetValue(PaddingProperty);
                property.Left = paddingLeft;
                item.SetValue(PaddingProperty, property);
                if (item.GetType() == typeof(ChaoControls.Style.TogglePanel))
                {
                    togglePanels.Add(item);
                    ChaoControls.Style.TogglePanel togglePanel = item as ChaoControls.Style.TogglePanel;
                    List<UIElement> uIElements = new List<UIElement>();
                    foreach (UIElement uIElement in togglePanel.Items)
                    {
                        uIElements.Add(uIElement);
                    }
                    moveAndSetPadding(uIElements, paddingLeft + 20);//深度优先
                }
            }

            //将可展开的移动到一起
            foreach (var item in togglePanels)
            {
                int idx = collection.IndexOf(item);
                collection.RemoveAt(idx);
                collection.Insert(0, item);
            }
        }

        public void handleTabItemClick(object sender ,MouseButtonEventArgs e)
        {
            if (e.GetPosition(this).X <= 50)
            {
                TabItem item = sender as TabItem;
                if (item.IsSelected)
                {
                    ToHideSidePanel = true;
                }
            }

        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
                Button btn = sender as Button;
                ContextMenu contextMenu = btn.ContextMenu;
                contextMenu.PlacementTarget = btn;
                contextMenu.IsOpen = true;
        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {
            new About().Show();
        }

        private void ShowAllSide(object sender, RoutedEventArgs e)
        {
            setTogglePanelStatus(getToggleStackPanelChild(), true);
        }

        private List<UIElement> getToggleStackPanelChild()
        {
            UIElementCollection collection = ToggleStackPanel.Children;
            List<UIElement> uIElements = new List<UIElement>();
            foreach (UIElement item in collection)
            {
                uIElements.Add(item);
            }
            return uIElements;
        }

        private void setTogglePanelStatus(List<UIElement> collection, bool status)
        {
            foreach (UIElement item in collection)
            {
                if (item.GetType() == typeof(ChaoControls.Style.TogglePanel))
                {
                    
                    ChaoControls.Style.TogglePanel togglePanel = item as ChaoControls.Style.TogglePanel;
                    togglePanel.IsExpanded = status;
                    List<UIElement> uIElements = new List<UIElement>();
                    foreach (UIElement uIElement in togglePanel.Items)
                    {
                        uIElements.Add(uIElement);
                    }
                    setTogglePanelStatus(uIElements, status);//深度优先
                }
            }
        }

        private void HideAllSide(object sender, RoutedEventArgs e)
        {
            setTogglePanelStatus(getToggleStackPanelChild(), false);
        }
    }
}
