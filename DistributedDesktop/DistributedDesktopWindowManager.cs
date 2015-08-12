using System;
using System.Windows.Threading;
using DistributedDesktop.Controls.Windows;

namespace DistributedDesktop
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class DistributedDesktopWindowManager
	{
		private readonly VirtualWindow _applicationWindow;
		private readonly DispatcherTimer _timer;

		public DistributedDesktopWindowManager()
		{
			_applicationWindow = new VirtualWindow(7088);
			_applicationWindow.Show();

			_timer= new DispatcherTimer(DispatcherPriority.Send) {Interval = TimeSpan.FromMilliseconds(16)};
			_timer.Tick += TimerOnTick;
			_timer.Start();
		}

		private void TimerOnTick(object sender, EventArgs e)
		{
			_applicationWindow.RefreshWindow();
		}
	}
}