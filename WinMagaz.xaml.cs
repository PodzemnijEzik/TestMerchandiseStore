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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Media.Animation;
using System.Data;

namespace WpfApp1
{
	//класс данных карточки товара
	public class Tovar
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public double Price { get; set; }

		[JsonProperty("image")]
		public string Image { get; set; }
	
	}

	//класс окна магазина товаров 
	public partial class WinMagaz : Window
	{
		public static List<Tovar> listTovar;
		public static List<Tovar> listBasket;
		delegate void Btn1_click(object sender, RoutedEventArgs e);
		private void Btn1_click1(object sender, RoutedEventArgs e)
		{

		}
		public WinMagaz()
		{
			//загрузка данных о товарах
			var json = File.ReadAllText("data.json");
			listTovar = JsonConvert.DeserializeObject<List<Tovar>>(json);

			//загрузка уже выбранных товаров
			json = File.ReadAllText("basket.json");
			listBasket = JsonConvert.DeserializeObject<List<Tovar>>(json);
			InitializeComponent();

			/*
			 Можно было тоже сделать все с помощью ObservableCollection
			но я решил попробовать реализовать все кодом)
			 */
			/*
			 Создание объектов и заполнеие WarpMagazMenu
			 */
			foreach (Tovar tovar in listTovar)
			{
				//Место для карточки товара
				Border buttonTovar = new Border { Width = 200, Height = 300, Background = new SolidColorBrush(Colors.White), Margin = new Thickness(10) };
				
				//общий блок куда поместятся все объекты данных о товаре
				Grid grid = new Grid { };

				//Название товара
				grid.Children.Add(new Label
				{
					Content = tovar.Name.ToString(),
					HorizontalAlignment = HorizontalAlignment.Center
				});

				//Картинка товара
				grid.Children.Add(new Image {
					Width = 190,
					Height = 200,
					Source = new BitmapImage(new Uri(("ApplicationData\\Current\\LocalFolder\\" + tovar.Image), UriKind.Relative)),
					Margin = new Thickness(0, 30, 0, 0),
					VerticalAlignment = VerticalAlignment.Top });

				//Цена товара
				grid.Children.Add(new Label {
					Content = "$" + tovar.Price.ToString(),
					Margin = new Thickness(10, 230, 10, 10),
					HorizontalAlignment = HorizontalAlignment.Center
				});

				//кнопка добавить в корзину
				Button button = new Button {
					Width = 130,
					Height = 30,
					Margin = new Thickness(10, 260, 10, 10),
					Content = "Добавить в корзину"
					};
				button.Click += (s, e) => {
					Tovar tov = tovar;
					tov.Image = ("ApplicationData\\Current\\LocalFolder\\" + tovar.Image);
					listBasket.Add(tov);
					string json = JsonConvert.SerializeObject(listBasket, Formatting.Indented);
					File.WriteAllText("basket.json", json);
					button.IsEnabled = false; };
				grid.Children.Add(button);

				//проверка если товар уже добавлен
				foreach (var iter in listBasket)
				{
					if(iter.Id == tovar.Id)
					{
						button.IsEnabled = false;
					}
				}
				buttonTovar.Child = grid;

				//выгрузка карточки
				WarpMagazMenu.Children.Add(buttonTovar);
			}


		}
	}
}
