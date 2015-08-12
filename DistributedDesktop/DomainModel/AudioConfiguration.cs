using System.Runtime.Serialization;

namespace DistributedDesktop.DomainModel
{
	/// <summary>
	/// The audio configuration of a cluster (basically wheter, and if, to which computer audio is streamed to).
	/// </summary>
	[DataContract]
	public sealed class AudioConfiguration
	{
		/// <summary>
		/// Whether or not audio is streamed to a particular computer (slave or master).
		/// </summary>
		[DataMember(Order = 1)]
		public bool StreamAudio;

		/// <summary>
		/// The name of the computer to which audio from *all other* computers is streamed to.
		/// </summary>
		[DataMember(Order = 2)]
		public string OutputComputerName;
	}
}