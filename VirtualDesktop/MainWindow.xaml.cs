using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace VirtualDesktop
{
	public partial class MainWindow
	{
		private readonly WasapiCapture _capture;
		private readonly Stopwatch _sw;
		private TimeSpan _latency;
		private readonly DispatcherTimer _timer;
		private int _length;
		private readonly TcpListener _server;
		private readonly TcpClient _client;
		private TcpClient _serverToClient;
		private NetworkStream _serverStream;
		private NetworkStream _clientStream;
		private readonly WaveFormat _format;
		private WasapiOut _playbackDevice;
		private WaveInProvider _waveInProvider;
		private WaveInTest _waveInTest;

		public MainWindow()
		{
			InitializeComponent();

			var deviceEnumerator = new MMDeviceEnumerator();
			var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
			var device = devices.First(x => x.FriendlyName.Contains("Mikrofon (Realtek"));
			_capture = new WasapiCapture(device) { ShareMode = AudioClientShareMode.Shared };
			_format = device.AudioClient.MixFormat;
			_capture.WaveFormat = _format;

			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromMilliseconds(100);
			_timer.Tick += TimerOnTick;
			_timer.Start();

			_server = new TcpListener(new IPEndPoint(IPAddress.Loopback, 54321));
			_server.Start();
			_server.BeginAcceptTcpClient(OnServerConnected, null);

			_client = new TcpClient();
			_client.BeginConnect(IPAddress.Loopback, 54321, OnClientConnected, null);

			_sw = new Stopwatch();
		}

		private void OnServerConnected(IAsyncResult ar)
		{
			_serverToClient = _server.EndAcceptTcpClient(ar);
			_serverStream = _serverToClient.GetStream();
			_waveInTest = new WaveInTest {WaveFormat = _format};
			_waveInProvider = new WaveInProvider(_waveInTest);

			var deviceEnumerator = new MMDeviceEnumerator();
			var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
			var device = devices.First(x => x.FriendlyName.Contains("Lautsprecher"));
			_playbackDevice = new WasapiOut(device, AudioClientShareMode.Shared, true, 0);

			_playbackDevice.Init(_waveInProvider);
			_playbackDevice.Play();

			_capture.DataAvailable += CaptureOnDataAvailable;
			_capture.StartRecording();
		}

		private void OnClientConnected(IAsyncResult ar)
		{
			_client.EndConnect(ar);
			_clientStream = _client.GetStream();
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			Latency.Text = string.Format("{0}ms", _latency.TotalMilliseconds);
			Length.Text = string.Format("{0} b", _length);
		}

		private void CaptureOnDataAvailable(object sender, WaveInEventArgs e)
		{
			_sw.Stop();
			_latency = _sw.Elapsed;
			_length = e.BytesRecorded;
			//_clientStream.Write(e.Buffer, 0, e.BytesRecorded);
			_waveInTest.Add(e.Buffer, e.BytesRecorded);

			_sw.Restart();
		}
	}
}
