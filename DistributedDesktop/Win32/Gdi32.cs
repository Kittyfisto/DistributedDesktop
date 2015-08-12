using System;
using System.Runtime.InteropServices;

namespace DistributedDesktop.Win32
{
	public static class Gdi32
	{
		/// <summary>Selects an object into the specified device context (DC). The new object replaces the previous object of the same type.</summary>
		/// <param name="hdc">A handle to the DC.</param>
		/// <param name="hgdiobj">A handle to the object to be selected.</param>
		/// <returns>
		///   <para>If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced. If the selected object is a region and the function succeeds, the return value is one of the following values.</para>
		///   <para>SIMPLEREGION - Region consists of a single rectangle.</para>
		///   <para>COMPLEXREGION - Region consists of more than one rectangle.</para>
		///   <para>NULLREGION - Region is empty.</para>
		///   <para>If an error occurs and the selected object is not a region, the return value is <c>NULL</c>. Otherwise, it is <c>HGDI_ERROR</c>.</para>
		/// </returns>
		/// <remarks>
		///   <para>This function returns the previously selected object of the specified type. An application should always replace a new object with the original, default object after it has finished drawing with the new object.</para>
		///   <para>An application cannot select a single bitmap into more than one DC at a time.</para>
		///   <para>ICM: If the object being selected is a brush or a pen, color management is performed.</para>
		/// </remarks>
		[DllImport("gdi32.dll", EntryPoint = "SelectObject")]
		public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

		/// <summary>
		///    Performs a bit-block transfer of the color data corresponding to a
		///    rectangle of pixels from the specified source device context into
		///    a destination device context.
		/// </summary>
		/// <param name="hdc">Handle to the destination device context.</param>
		/// <param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
		/// <param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
		/// <param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
		/// <param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
		/// <param name="hdcSrc">Handle to the source device context.</param>
		/// <param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
		/// <param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
		/// <param name="dwRop">A raster-operation code.</param>
		/// <returns>
		///    <c>true</c> if the operation succeedes, <c>false</c> otherwise. To get extended error information, call <see cref="System.Runtime.InteropServices.Marshal.GetLastWin32Error"/>.
		/// </returns>
		[DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

		/// <summary>
		///        Creates a bitmap compatible with the device that is associated with the specified device context.
		/// </summary>
		/// <param name="hdc">A handle to a device context.</param>
		/// <param name="nWidth">The bitmap width, in pixels.</param>
		/// <param name="nHeight">The bitmap height, in pixels.</param>
		/// <returns>If the function succeeds, the return value is a handle to the compatible bitmap (DDB). If the function fails, the return value is <see cref="System.IntPtr.Zero"/>.</returns>
		[DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
		public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);

		/// <summary>
		///        Creates a memory device context (DC) compatible with the specified device.
		/// </summary>
		/// <param name="hdc">A handle to an existing DC. If this handle is NULL,
		///        the function creates a memory DC compatible with the application's current screen.</param>
		/// <returns>
		///        If the function succeeds, the return value is the handle to a memory DC.
		///        If the function fails, the return value is <see cref="System.IntPtr.Zero"/>.
		/// </returns>
		[DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

		/// <summary>Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
		/// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
		/// <returns>
		///   <para>If the function succeeds, the return value is nonzero.</para>
		///   <para>If the specified handle is not valid or is currently selected into a DC, the return value is zero.</para>
		/// </returns>
		/// <remarks>
		///   <para>Do not delete a drawing object (pen or brush) while it is still selected into a DC.</para>
		///   <para>When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.</para>
		/// </remarks>
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject([In] IntPtr hObject);

		/// <summary>Deletes the specified device context (DC).</summary>
		/// <param name="hdc">A handle to the device context.</param>
		/// <returns><para>If the function succeeds, the return value is nonzero.</para><para>If the function fails, the return value is zero.</para></returns>
		/// <remarks>An application must not delete a DC whose handle was obtained by calling the <c>GetDC</c> function. Instead, it must call the <c>ReleaseDC</c> function to free the DC.</remarks>
		[DllImport("gdi32.dll", EntryPoint = "DeleteDC", SetLastError = true)]
		public static extern bool DeleteDC([In] IntPtr hdc);

		/// <summary>
		///        Retrieves the bits of the specified compatible bitmap and copies them into a buffer as a DIB using the specified format.
		/// </summary>
		/// <param name="hdc">A handle to the device context.</param>
		/// <param name="hbmp">A handle to the bitmap. This must be a compatible bitmap (DDB).</param>
		/// <param name="uStartScan">The first scan line to retrieve.</param>
		/// <param name="cScanLines">The number of scan lines to retrieve.</param>
		/// <param name="lpvBits">A pointer to a buffer to receive the bitmap data. If this parameter is <see cref="IntPtr.Zero"/>, the function passes the dimensions and format of the bitmap to the <see cref="BITMAPINFO"/> structure pointed to by the <paramref name="lpbi"/> parameter.</param>
		/// <param name="lpbi">A pointer to a <see cref="BITMAPINFO"/> structure that specifies the desired format for the DIB data.</param>
		/// <param name="uUsage">The format of the bmiColors member of the <see cref="BITMAPINFO"/> structure. It must be one of the following values.</param>
		/// <returns>If the lpvBits parameter is non-NULL and the function succeeds, the return value is the number of scan lines copied from the bitmap.
		/// If the lpvBits parameter is NULL and GetDIBits successfully fills the <see cref="BITMAPINFO"/> structure, the return value is nonzero.
		/// If the function fails, the return value is zero.
		/// This function can return the following value: ERROR_INVALID_PARAMETER (87 (0×57))</returns>
		[DllImport("gdi32.dll", EntryPoint = "GetDIBits", SetLastError = true)]
		public static extern int GetDIBits([In] IntPtr hdc, [In] IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] byte[] lpvBits, ref BITMAPINFO lpbi, DIB_Color_Mode uUsage);

		[DllImport("gdi32.dll")]
		public static extern int SetDIBits(IntPtr hdc, IntPtr hbmp, uint uStartScan, uint
		   cScanLines, byte[] lpvBits, [In] ref BITMAPINFO lpbmi, DIB_Color_Mode uUsage);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDIBSection(IntPtr hdc,
			[In] ref BITMAPINFO pbmi,
			DIB_Color_Mode uUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		public const uint SRCCOPY = 0x00CC0020;
		public const uint SRCPAINT = 0x00EE0086;
		public const uint SRCAND = 0x008800C6;
		public const uint SRCINVERT = 0x00660046;
		public const uint SRCERASE = 0x00440328;
		public const uint NOTSRCCOPY = 0x00330008;
		public const uint NOTSRCERASE = 0x001100A6;
		public const uint MERGECOPY = 0x00C000CA;
		public const uint MERGEPAINT = 0x00BB0226;
		public const uint PATCOPY = 0x00F00021;
		public const uint PATPAINT = 0x00FB0A09;
		public const uint PATINVERT = 0x005A0049;
		public const uint DSTINVERT = 0x00550009;
		public const uint BLACKNESS = 0x00000042;
		public const uint WHITENESS = 0x00FF0062;
		public const uint CAPTUREBLT = 0x40000000; //only if WinVer >= 5.0.0 (see wingdi.h)
	}
}