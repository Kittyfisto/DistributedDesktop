using System;
using System.Diagnostics;
using DistributedDesktop.Win32;

namespace DistributedDesktop.Controls.Windows
{
	/// <summary>
	/// Interaction logic for VirtualApplicationWindow.xaml
	/// </summary>
	public partial class VirtualApplicationWindow
		: IVirtualWindow
	{
		private readonly Process _process;
		private readonly IntPtr _hwnd;

		public VirtualApplicationWindow(int pid)
		{
			InitializeComponent();

			_process = Process.GetProcessById(pid);
			_hwnd = _process.MainWindowHandle;

			Refresh();
		}

		public void Refresh()
		{
			Title = Native.GetWindowText(_hwnd);

			RECT rect;
			if (Native.TryGetWindowRect(_hwnd, out rect))
			{
				Width = (rect.Right - rect.Left);
				Height = (rect.Bottom - rect.Top);
			}
		}
	}
}
