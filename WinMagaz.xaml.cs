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
	/// <summary>
	/// Логика взаимодействия для WinMagaz.xaml
	/// </summary>
	/// 

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
	public class RelayCommand : ICommand
	{
		private Action<object> execute;
		private Func<object, bool> canExecute;

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return this.canExecute == null || this.canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			this.execute(parameter);
		}
	}
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
			var json = File.ReadAllText("data.json");
			listTovar = JsonConvert.DeserializeObject<List<Tovar>>(json);
			json = File.ReadAllText("basket.json");
			listBasket = JsonConvert.DeserializeObject<List<Tovar>>(json);
			InitializeComponent();

			foreach (Tovar tovar in listTovar)
			{
				Border buttonTovar = new Border { Width = 200, Height = 300, Background = new SolidColorBrush(Colors.White), Margin = new Thickness(10) };
				Grid grid = new Grid { };
				grid.Children.Add(new Label
				{
					Content = tovar.Name.ToString(),
					HorizontalAlignment = HorizontalAlignment.Center
				});
				grid.Children.Add(new Image {
					Width = 190,
					Height = 200,
					Source = new BitmapImage(new Uri(("ApplicationData\\Current\\LocalFolder\\" + tovar.Image), UriKind.Relative)),
					Margin = new Thickness(0, 30, 0, 0),
					VerticalAlignment = VerticalAlignment.Top });
				grid.Children.Add(new Label {
					Content = "$" + tovar.Price.ToString(),
					Margin = new Thickness(10, 230, 10, 10),
					HorizontalAlignment = HorizontalAlignment.Center
				});
				Button button = new Button {
					Width = 130,
					Height = 30,
					Margin = new Thickness(10, 260, 10, 10),
					Content = "Добавить в корзину",
					Command = new RelayCommand(o => {
						Tovar tov = tovar;
						tov.Image = ("ApplicationData\\Current\\LocalFolder\\" + tovar.Image);
						listBasket.Add(tov);
						string json = JsonConvert.SerializeObject(listBasket, Formatting.Indented);
						File.WriteAllText("basket.json", json);
					}) };
				foreach(var iter in listBasket)
				{
					if(iter.Id == tovar.Id)
					{
						button.IsEnabled = false;
					}
				}
				button.Click += (s, e) => { button.IsEnabled = false; };
				grid.Children.Add(button);
				buttonTovar.Child = grid;
				WarpMagazMenu.Children.Add(buttonTovar);
			}


		}
	}
}
