namespace ProyectoApuntesJoseCerezo.Models
{
	class About
	{
		public string Title => "José Cerezo";

		public string Version => AppInfo.VersionString;

		public string MoreInfoUrl => "https://aka.ms/maui";

		public string Message => "Me llamo José Cerezo, tengo 20 años, soy de Venezuela y tengo varios gustos y hobbies, entre los cuales se encuentran la programación, los videojuegos y la lectura.";

		public string Image => "meme.jpeg";
	}
}
