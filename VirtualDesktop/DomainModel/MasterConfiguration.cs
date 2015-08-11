using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VirtualDesktop.DomainModel
{
	[DataContract]
	public sealed class MasterConfiguration
	{
		/// <summary>
		/// The name of the master computer - should be unique within the local network as it's
		/// used to identify masters.
		/// </summary>
		[DataMember(Order = 1)]
		public string ComputerName;

		/// <summary>
		/// The slaves the master has added.
		/// </summary>
		[DataMember(Order = 2)]
		public List<SlaveConfiguration> Slaves;
	}
}