using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HKW.Practices.Controls.Silverlight
{
    public class BaseView : BusyIndicator
    {
        public BaseView()
        {
            IsBusy = false;
            BusyContent = "数据加载中...";
            FontFamily = new FontFamily("Segoe UI, SimSun");
            FontSize = 12;
            //this.Margin = new Thickness(10, 10, 10, 10);
        }
    }
}
