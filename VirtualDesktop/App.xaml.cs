namespace DistributedDesktop
{
	public partial class App
	{
		private readonly DistributedDesktopWindowManager _vdwm;

		public App()
		{
			_vdwm = new DistributedDesktopWindowManager();
		}
	}
}