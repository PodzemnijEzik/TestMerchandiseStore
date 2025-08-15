using Newtonsoft.Json;
using System;
using System.IO;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WpfApp1
{
	//класс окна покупной корзины
	public partial class WinBasket : Window
	{
		public static ObservableCollection<Tovar> listBasket;

		public WinBasket()
		{
			
			InitializeComponent();

			//загрузка уже выбранных товаров
			var json = File.ReadAllText("basket.json");
			listBasket = new ObservableCollection<Tovar>(JsonConvert.DeserializeObject<List<Tovar>>(json));

			//загрузка уже выбранных товаровотображение товаров
			BasketListView.ItemsSource = listBasket;

			//вывод количества товара
			Count.Content = "Количество товара: " + listBasket.Count.ToString();

			//подсчет общей суммы цены
			double sum = 0;
			foreach(Tovar tovar in listBasket)
			{
				sum += tovar.Price;
			}

			// вывод общей суммы цены
			Price.Content = "Общая стоимость: $" + sum.ToString();
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			//удаление всех выбранных товаров из списка
			while (BasketListView.SelectedItems.Count > 0) {
				listBasket.Remove(BasketListView.SelectedItems[0] as Tovar);
			}

			//сохранение оставшихся товаров
			string json = JsonConvert.SerializeObject(listBasket, Formatting.Indented);
			File.WriteAllText("basket.json", json);

			//вывод нового количества товара
			Count.Content = "Количество товара: " + listBasket.Count.ToString();

			//переподсчет общей суммы цены
			double sum = 0;
			foreach (Tovar tovar in listBasket)
			{
				sum += tovar.Price;
			}

			// вывод новой общей суммы цены
			Price.Content = "Общая стоимость: $" + sum.ToString();
		}
	}
}
