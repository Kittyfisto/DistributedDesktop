using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistributedDesktop.DomainModel
{
	/// <summary>
	/// The configuration for a cluster of computers.
	/// </summary>
	[DataContract]
	public sealed class ClusterConfiguration
	{
		/// <summary>
		/// The name of the master computer - should be unique within the local network as it's
		/// used to identify masters.
		/// </summary>
		[DataMember(Order = 1)]
		public string MasterComputerName;

		/// <summary>
		/// The names of the slave computer's.
		/// </summary>
		[DataMember(Order = 2)]
		public List<string> SlaveComputerNames;

		/// <summary>
		/// The audio configuration of the cluster (basically wheter, and if, to which computer audio is streamed to).
		/// </summary>
		[DataMember(Order = 3)]
		public AudioConfiguration AudioConfiguration;
	}
}