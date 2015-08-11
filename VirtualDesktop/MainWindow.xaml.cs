using System.ComponentModel;
using VirtualDesktop.DomainModel;

namespace VirtualDesktop
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			var config = MainWindowConfiguration.Restore();
			if (config != null)
			{
				config.RestoreTo(this);
			}

			InitializeComponent();

			Closing += OnClosing;
		}

		private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
		{
			var config = WindowConfiguration.From(this);
			MainWindowConfiguration.Store(config);
		}
	}
}
