using System.Runtime.InteropServices;

namespace DistributedDesktop.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RGBQUAD
	{
		public byte rgbBlue;
		public byte rgbGreen;
		public byte rgbRed;
		public byte rgbReserved;
	}
}