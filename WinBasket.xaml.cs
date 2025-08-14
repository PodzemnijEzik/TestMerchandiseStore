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
	/// <summary>
	/// Логика взаимодействия для WinBasket.xaml
	/// </summary>
	public partial class WinBasket : Window
	{
		public static ObservableCollection<Tovar> listBasket;

		public WinBasket()
		{
			
			InitializeComponent();
			var json = File.ReadAllText("basket.json");
			listBasket = new ObservableCollection<Tovar>(JsonConvert.DeserializeObject<List<Tovar>>(json));
			BasketListView.ItemsSource = listBasket;
			Count.Content = "Количество товара: " + listBasket.Count.ToString();
			double sum = 0;
			foreach(Tovar tovar in listBasket)
			{
				sum += tovar.Price;
			}
			Price.Content = "Общая стоимость: $" + sum.ToString();
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			while (BasketListView.SelectedItems.Count > 0) {
				listBasket.Remove(BasketListView.SelectedItems[0] as Tovar);
			}
			string json = JsonConvert.SerializeObject(listBasket, Formatting.Indented);
			File.WriteAllText("basket.json", json);

			Count.Content = "Количество товара: " + listBasket.Count.ToString();
			double sum = 0;
			foreach (Tovar tovar in listBasket)
			{
				sum += tovar.Price;
			}
			Price.Content = "Общая стоимость: $" + sum.ToString();
		}
	}
}
