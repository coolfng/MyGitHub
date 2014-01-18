using System.Windows;
using System.Windows.Controls;

namespace HKW.Practices.Controls.Silverlight
{
    public partial class TextBox 
    {
        public TextBox()
        {
            InitializeComponent();
        }

        #region UpdateExplict - DependencyProperty.RegisterAttached
        public bool BindingUpdateOnTextChanged
        {
            get { return (bool)GetValue(BindingUpdateOnTextChangedProperty); }
            set
            {
                SetValue(BindingUpdateOnTextChangedProperty, value);
            }
        }

        public static readonly DependencyProperty BindingUpdateOnTextChangedProperty =
            DependencyProperty.RegisterAttached("BindingOnTextChanged", typeof(bool), typeof(TextBox), new PropertyMetadata(false, UpdateOnPropertyChangedPropertyCallback));

        public static void UpdateOnPropertyChangedPropertyCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is TextBox)
            {
                var txt = dependencyObject as TextBox;
                txt.TextChanged += TxtTextChanged;
            }
        }

        static void TxtTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt != null)
            {
                var bindingExpression = txt.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpression != null)
                {
                    bindingExpression.UpdateSource();
                }
            }
        }
        #endregion
    }
}
