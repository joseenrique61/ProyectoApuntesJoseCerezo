using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ProyectoApuntesJoseCerezo.ViewModels
{
	internal class JCAboutViewModel
	{
		public string Title => "José Cerezo";

		public string Version => AppInfo.VersionString;

		public string MoreInfoUrl => "https://aka.ms/maui";

		public string Message => "Me llamo José Cerezo, tengo 20 años, soy de Venezuela y tengo varios gustos y hobbies, entre los cuales se encuentran la programación, los videojuegos y la lectura.";

		public string Image => "meme.jpeg";

		public ICommand ShowMoreInfoCommand { get; }

		public JCAboutViewModel()
		{
			ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
		}

		async Task ShowMoreInfo() =>
			await Launcher.Default.OpenAsync(MoreInfoUrl);
	}
}
