using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;

namespace HKW.Practices
{
    public class Bootstrapper : MefBootstrapper
    {
        private const string ModuleCatalogUri = "/HKW.Practices;component/ModulesCatalog.xaml";

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri(ModuleCatalogUri,
                                                                                          UriKind.Relative));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.RootVisual = (Shell)Shell;
        }
    }
}
