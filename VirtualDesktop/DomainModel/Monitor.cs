using System;
using System.Runtime.Serialization;

namespace VirtualDesktop.DomainModel
{
	/// <summary>
	///     Describes a monitor within a cluster of computers, it's virtual position, size, etc..
	/// </summary>
	[DataContract]
	public sealed class Monitor
	{
		/// <summary>
		///     A user friendly name for this monitor.
		/// </summary>
		[DataMember(Order = 4)] public string FriendlyName;

		/// <summary>
		///     Identifies this screen amongst all others within the cluster.
		/// </summary>
		[DataMember(Order = 1)] public Guid Id;

		/// <summary>
		///     The name of the slave this screen belongs to.
		/// </summary>
		[DataMember(Order = 2)] public string SlaveComputerName;

		/// <summary>
		///     Identifies this screen on its slave ONLY - might not be unique amongst all slaves connected to a cluster.
		/// </summary>
		[DataMember(Order = 3)] public string SlaveInstanceName;

		#region Virtual Position

		/// <summary>
		///     The height of the monitor
		/// </summary>
		[DataMember(Order = 8)] public double Height;

		/// <summary>
		///     The horizontal position of the monitor within the virtual arrangement of monitors.
		/// </summary>
		/// <remarks>
		///     A value of 0 represents the left border of primary monitor.
		///     Values smaller than 0 are left of the primary monitor,
		///     values greater than the primary monitor's width are right of the primary monitor.
		/// </remarks>
		[DataMember(Order = 5)] public double Left;

		/// <summary>
		///     The vertical position of the monitor within the virtual arrangement of monitors.
		/// </summary>
		/// <remarks>
		///     A value of 0 represents the top border of the primary monitor.
		///     Values smaller than 0 are on top of the primary monitor,
		///     values greater than the primary monitor's height are underneath the primary monitor.
		/// </remarks>
		[DataMember(Order = 6)] public double Top;

		/// <summary>
		///     The width of the monitor
		/// </summary>
		[DataMember(Order = 7)] public double Width;

		#endregion
	}
}