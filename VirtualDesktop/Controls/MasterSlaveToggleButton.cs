using System.Windows;
using System.Windows.Controls;

namespace DistributedDesktop.Controls
{
	public class MasterSlaveToggleButton : Control
	{
		public static readonly DependencyProperty IsMasterProperty =
			DependencyProperty.Register("IsMaster", typeof (bool), typeof (MasterSlaveToggleButton), new PropertyMetadata(true, OnIsMasterChanged));

		private static void OnIsMasterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			((MasterSlaveToggleButton)dependencyObject).OnIsMasterChanged((bool) e.NewValue);
		}

		private void OnIsMasterChanged(bool isMaster)
		{
			IsSlave = !isMaster;
		}

		public static readonly DependencyProperty IsSlaveProperty =
			DependencyProperty.Register("IsSlave", typeof (bool), typeof (MasterSlaveToggleButton), new PropertyMetadata(false, OnIsSlaveChanged));

		private static void OnIsSlaveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			((MasterSlaveToggleButton)dependencyObject).OnIsSlaveChanged((bool)e.NewValue);
		}

		private void OnIsSlaveChanged(bool isSlave)
		{
			IsMaster = !isSlave;
		}

		static MasterSlaveToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (MasterSlaveToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (MasterSlaveToggleButton)));
		}

		public bool IsSlave
		{
			get { return (bool) GetValue(IsSlaveProperty); }
			set { SetValue(IsSlaveProperty, value); }
		}

		public bool IsMaster
		{
			get { return (bool) GetValue(IsMasterProperty); }
			set { SetValue(IsMasterProperty, value); }
		}
	}
}