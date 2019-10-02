using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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

            view.Reload();

            if (_webViews.Any(x => x == view))
            {
                _webViews.Remove(view);
                return;
            }

            _webViews.Add(view);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) => ReloadViews();

        private void ReloadViews() => _webViews.ForEach(view => view?.Reload());

        private void WebControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
                ((EO.Wpf.WebControl) sender).WebView.Reload();
        }

        private void WebView_LoadFailed(object sender, LoadFailedEventArgs e) => ((EO.Wpf.WebView) sender).LoadHtml(e.ErrorMessage);
    }
}
