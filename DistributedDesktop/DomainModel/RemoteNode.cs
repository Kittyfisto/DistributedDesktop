using System;

namespace DistributedDesktop.DomainModel
{
	/// <summary>
	/// Represents a remote computer (definately NOT this one).
	/// </summary>
	public sealed class RemoteNode
		: INode
	{
		private readonly string _computerName;

		public RemoteNode(string computerName)
		{
			if (computerName == null) throw new ArgumentNullException("computerName");

			_computerName = computerName;
		}
	}
}