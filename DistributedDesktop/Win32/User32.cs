using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DistributedDesktop.Win32
{
	internal static class User32
	{
		#region External methods

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

		#endregion

		public static bool TryGetWindowRect(IntPtr hWnd, out RECT rect)
		{
			return GetWindowRect(hWnd, out rect);
		}

		public static string GetWindowText(IntPtr hWnd)
		{
			var builder = new StringBuilder();
			return GetWindowText(hWnd, builder);
		}

		public static string GetWindowText(IntPtr hWnd, StringBuilder builder)
		{
			// Allocate correct string length first
			int length = GetWindowTextLength(hWnd);
			builder.Capacity = Math.Max(builder.Capacity, length + 1);
			GetWindowText(hWnd, builder, builder.Capacity);
			return builder.ToString();
		}
	}
}