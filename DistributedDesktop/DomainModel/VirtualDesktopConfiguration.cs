using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistributedDesktop.DomainModel
{
	/// <summary>
	///     The configuration of the arrangement of the virtual desktop.
	/// </summary>
	[DataContract]
	public sealed class VirtualDesktopConfiguration
	{
		/// <summary>
		///     The monitors that span the virtual desktop.
		/// </summary>
		[DataMember(Order = 1)] public List<Monitor> Monitors;
	}
}