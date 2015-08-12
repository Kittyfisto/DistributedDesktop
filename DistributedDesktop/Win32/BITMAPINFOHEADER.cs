using System.Runtime.InteropServices;

namespace DistributedDesktop.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BITMAPINFOHEADER
	{
		public uint biSize;
		public int biWidth;
		public int biHeight;
		public ushort biPlanes;
		public ushort biBitCount;
		public BitmapCompressionMode biCompression;
		public uint biSizeImage;
		public int biXPelsPerMeter;
		public int biYPelsPerMeter;
		public uint biClrUsed;
		public uint biClrImportant;

		public void Init()
		{
			biSize = (uint)Marshal.SizeOf(this);
		}
	}
}