using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
	//класс главного окна
	public partial class MainWindow : Window
	{
		public static WinMagaz winMagaz;
		public static WinBasket winBasket;
		public MainWindow()
		{
			InitializeComponent();
			
		}

		//открытие окна с катологом товаров
		private void Btn2_click(object sender, RoutedEventArgs e)
		{
			
			if (winMagaz == null || winMagaz.IsEnabled)
			{	winMagaz = new WinMagaz();
				winMagaz.ShowDialog();
				
			}
		}

		//открытие покупной корзины
		private void Btn1_click(object sender, RoutedEventArgs e)
		{
			if (winBasket == null || winBasket.IsEnabled)
			{
				winBasket = new WinBasket();
				winBasket.ShowDialog();

			}
		}
	}
}
