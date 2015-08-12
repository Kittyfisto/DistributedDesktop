using System.Collections.Generic;

namespace DistributedDesktop.DomainModel
{
	/// <summary>
	/// Controls a cluster of computers.
	/// </summary>
	public sealed class Cluster
	{
		private readonly List<INode> _nodes;

		public Cluster()
		{
			_nodes = new List<INode> {new LocalNode()};
		}

		public void Add(RemoteNode node)
		{
			_nodes.Add(node);
		}

		public bool Remove(RemoteNode node)
		{
			return _nodes.Remove(node);
		}
	}
}