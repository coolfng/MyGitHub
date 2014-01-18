//      *********    请勿修改此文件     *********
//      此文件由设计工具再生成。更改
//      此文件可能会导致错误。
namespace Expression.Blend.SampleData.SampleDataSource
{
	using System; 

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
	internal class SampleDataSource { }
#else

	public class SampleDataSource : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public SampleDataSource()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/HKW.Practices.Controls.Silverlight.Test;component/SampleData/SampleDataSource/SampleDataSource.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private ItemCollection _Collection = new ItemCollection();

		public ItemCollection Collection
		{
			get
			{
				return this._Collection;
			}
		}
	}

	public class Item : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _姓名 = string.Empty;

		public string 姓名
		{
			get
			{
				return this._姓名;
			}

			set
			{
				if (this._姓名 != value)
				{
					this._姓名 = value;
					this.OnPropertyChanged("姓名");
				}
			}
		}

		private bool _婚姻 = false;

		public bool 婚姻
		{
			get
			{
				return this._婚姻;
			}

			set
			{
				if (this._婚姻 != value)
				{
					this._婚姻 = value;
					this.OnPropertyChanged("婚姻");
				}
			}
		}

		private string _地址 = string.Empty;

		public string 地址
		{
			get
			{
				return this._地址;
			}

			set
			{
				if (this._地址 != value)
				{
					this._地址 = value;
					this.OnPropertyChanged("地址");
				}
			}
		}
	}

	public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
	{ 
	}
#endif
}
