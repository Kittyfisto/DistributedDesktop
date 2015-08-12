using System.Runtime.InteropServices;

namespace DistributedDesktop.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BITMAPINFO
	{
		/// <summary>
		/// A BITMAPINFOHEADER structure that contains information about the dimensions of color format.
		/// </summary>
		public BITMAPINFOHEADER bmiHeader;

		/// <summary>
		/// An array of RGBQUAD. The elements of the array that make up the color table.
		/// </summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
		public RGBQUAD[] bmiColors;
	}
}