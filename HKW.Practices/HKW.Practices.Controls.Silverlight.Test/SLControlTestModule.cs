using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace HKW.Practices.Controls.Silverlight.Test
{
    [ModuleExport(typeof(SLControlTestModule))]
    public class SLControlTestModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            RegionManager.RequestNavigate("RootRegion", typeof(TestView).Name);
        }
    }
}
