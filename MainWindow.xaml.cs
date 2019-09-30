using System;
using System.Collections.Generic;
using System.Windows;
using EO.WebBrowser;

namespace DashMonitoramento
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<EO.Wpf.WebView> _webViews;

        public MainWindow()
        {
            InitializeComponent();

            _webViews = new List<EO.Wpf.WebView>();

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer.Start();
        }

        private void WebView_LoadCompleted(object sender, LoadCompletedEventArgs e) =>
            ((EO.Wpf.WebView)sender).EvalScript("document.body.style.overflow ='hidden'");

        private void WebView_MouseDoubleClick(object sender, EO.Base.UI.MouseEventArgs e)
        {
            var view = (EO.Wpf.WebView)sender;
            _webViews.Add(view);
            view.Reload();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) => ReloadViews();

        private void ReloadViews() => _webViews.ForEach(view => view?.Reload());
    }
}
