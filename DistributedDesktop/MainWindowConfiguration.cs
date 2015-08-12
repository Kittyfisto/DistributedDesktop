using System.Windows;
using DistributedDesktop.DomainModel;

namespace DistributedDesktop
{
	public static class MainWindowConfiguration
	{
		public static void Store(WindowConfiguration config)
		{
			Properties.Settings.Default.Left = config.Left;
			Properties.Settings.Default.Top = config.Top;
			Properties.Settings.Default.Width= config.Width;
			Properties.Settings.Default.Height = config.Height;
			Properties.Settings.Default.IsMaximized = config.State == WindowState.Maximized;
			Properties.Settings.Default.IsMinimized = config.State == WindowState.Minimized;
			Properties.Settings.Default.Save();
		}

		public static WindowConfiguration Restore()
		{
			var config = new WindowConfiguration
				{
					Left = Properties.Settings.Default.Left,
					Top = Properties.Settings.Default.Top,
					Width = Properties.Settings.Default.Width,
					Height = Properties.Settings.Default.Height,
				};
			if (Properties.Settings.Default.IsMaximized)
				config.State = WindowState.Maximized;
			else if (Properties.Settings.Default.IsMinimized)
				config.State = WindowState.Minimized;
			else
				config.State = WindowState.Normal;

			if (config.Width == 0 || config.Height == 0)
				return null;

			return config;
		}
	}
}