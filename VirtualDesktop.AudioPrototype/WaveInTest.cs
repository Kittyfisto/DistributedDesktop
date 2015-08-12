using System;
using NAudio.Wave;

namespace DistributedDesktop.AudioPrototype
{
	public sealed class WaveInTest
		: IWaveIn
	{
		public void Dispose()
		{
			
		}

		public void StartRecording()
		{
			
		}

		public void StopRecording()
		{
			
		}

		public void Add(byte[] buffer, int bytes)
		{
			var fn = DataAvailable;
			if (fn != null)
			{
				fn(this, new WaveInEventArgs(buffer, bytes));
			}
		}

		public WaveFormat WaveFormat { get; set; }

		public event EventHandler<WaveInEventArgs> DataAvailable;
		public event EventHandler<StoppedEventArgs> RecordingStopped;
	}
}