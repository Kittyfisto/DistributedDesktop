using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DistributedDesktop.Win32;

namespace DistributedDesktop.Controls.Windows
{
	internal sealed class VirtualWindow
		: Form
		, IVirtualWindow
	{
		private readonly Process _process;
		private readonly IntPtr _hWnd;
		private readonly StringBuilder _titleBuilder;

		public VirtualWindow(int pid)
		{
			_titleBuilder = new StringBuilder();
			_process = Process.GetProcessById(pid);
			_hWnd = _process.MainWindowHandle;

			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);

			Refresh();
		}

		public void RefreshWindow()
		{
			Text = User32.GetWindowText(_hWnd, _titleBuilder);

			RECT rect;
			if (User32.TryGetWindowRect(_hWnd, out rect))
			{
				int width, height;

				Width = width = (rect.Right - rect.Left);
				Height = height = (rect.Bottom - rect.Top);

				BITMAPINFO info;
				var data = CaptureWindow(0, 0, width, height, out info);
				OnDataReceived(data, info);
			}
		}

		#region V2
		/*
		private byte[] _data;
		private BITMAPINFO _info;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			var header = _info.bmiHeader;
			var width = header.biWidth;
			var height = header.biHeight;
			if (width != 0 && height != 0)
			{
				e.Graphics.Clear(Color.White);

				IntPtr targetDC = IntPtr.Zero;
				IntPtr compatibleDC = IntPtr.Zero;
				IntPtr hBitmap = IntPtr.Zero;

				targetDC = e.Graphics.GetHdc();
				compatibleDC = Gdi32.CreateCompatibleDC(targetDC);

				IntPtr data;
				hBitmap = Gdi32.CreateDIBSection(compatibleDC,
				                                 ref _info,
				                                 DIB_Color_Mode.DIB_RGB_COLORS,
				                                 out data,
				                                 IntPtr.Zero,
				                                 0);
				Marshal.Copy(_data, 0, data, _data.Length);

				IntPtr oldObject = Gdi32.SelectObject(compatibleDC, hBitmap);
				bool ret2 = Gdi32.BitBlt(targetDC, 0, 0, width, height, compatibleDC, 0, 0, Gdi32.SRCCOPY);
				Debug.Assert(ret2);
				Gdi32.SelectObject(compatibleDC, oldObject);

				bool ret = Gdi32.DeleteObject(hBitmap);
				Debug.Assert(ret);

				ret = Gdi32.DeleteDC(compatibleDC);
				Debug.Assert(ret);
				e.Graphics.ReleaseHdc(targetDC);
			}
		}

		private void OnDataReceived(byte[] data, BITMAPINFO info)
		{
			_data = data;
			_info = info;
			Invalidate();
		}
		*/
		#endregion

		#region V1

		private Bitmap _bitmap;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (_bitmap != null)
			{
				e.Graphics.DrawImage(_bitmap, 0, 0);
			}
		}

		private void OnDataReceived(byte[] data, BITMAPINFO info)
		{
			const PixelFormat format = PixelFormat.Format32bppRgb;
			var header = info.bmiHeader;

			var width = header.biWidth;
			var height = header.biHeight;
			if (width == 0 || height == 0)
				return;

			var bitmap = new Bitmap(width, height, format);
			try
			{
				var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height),
				                                 ImageLockMode.WriteOnly,
				                                 format);
				try
				{
					Marshal.Copy(data, 0, bitmapData.Scan0, (int) header.biSizeImage);
				}
				finally
				{
					bitmap.UnlockBits(bitmapData);
				}
			}
			catch (Exception)
			{
				bitmap.Dispose();
				throw;
			}

			if (_bitmap != null)
				_bitmap.Dispose();
			_bitmap = bitmap;

			Invalidate();
		}

		#endregion

		private byte[] CaptureWindow(int x, int y, int width, int height, out BITMAPINFO info)
		{
			IntPtr sourceDC = IntPtr.Zero;
			IntPtr targetDC = IntPtr.Zero;
			IntPtr compatibleBitmapHandle = IntPtr.Zero;

			try
			{
				sourceDC = User32.GetDC(_hWnd);
				targetDC = Gdi32.CreateCompatibleDC(sourceDC);

				compatibleBitmapHandle = Gdi32.CreateCompatibleBitmap(sourceDC, width, height);

				IntPtr oldObject = Gdi32.SelectObject(targetDC, compatibleBitmapHandle);

				Gdi32.BitBlt(targetDC, 0, 0, width, height, sourceDC, x, y, Gdi32.SRCCOPY);

				Gdi32.SelectObject(targetDC, oldObject);

				var data = CopyBitmapToArray(height, targetDC, compatibleBitmapHandle, out info);
				return data;
			}
			finally
			{
				bool ret = Gdi32.DeleteObject(compatibleBitmapHandle);
				Debug.Assert(ret);

				ret = User32.ReleaseDC(_hWnd, sourceDC);
				Debug.Assert(ret);

				ret = Gdi32.DeleteDC(targetDC);
				Debug.Assert(ret);
			}
		}

		private static byte[] CopyBitmapToArray(int height, IntPtr targetDC, IntPtr compatibleBitmapHandle, out BITMAPINFO info)
		{
			info = GetBitmapInfo(targetDC, compatibleBitmapHandle);
			//info.bmiHeader.biCompression = BitmapCompressionMode.BI_RGB;
			info.bmiHeader.biHeight = -info.bmiHeader.biHeight;

			var data = new byte[info.bmiHeader.biSizeImage];
			int ret = Gdi32.GetDIBits(targetDC, compatibleBitmapHandle,
			                      0,
			                      (uint) height,
			                      data,
			                      ref info,
			                      DIB_Color_Mode.DIB_RGB_COLORS);
			Debug.Assert(ret == height);

			info.bmiHeader.biHeight = -info.bmiHeader.biHeight;

			return data;
		}

		[Pure]
		private static BITMAPINFO GetBitmapInfo(IntPtr targetDC, IntPtr compatibleBitmapHandle)
		{
			BITMAPINFO info;
			info = new BITMAPINFO
				{
					bmiHeader =
						{
							biSize = (uint) Marshal.SizeOf(typeof (BITMAPINFOHEADER)),
						}
				};

			int ret = Gdi32.GetDIBits(targetDC, compatibleBitmapHandle,
			                          0,
			                          0,
			                          null,
			                          ref info,
			                          DIB_Color_Mode.DIB_RGB_COLORS);
			Debug.Assert(ret != 0);
			return info;
		}
	}
}