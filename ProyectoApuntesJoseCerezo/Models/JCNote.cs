namespace ProyectoApuntesJoseCerezo.Models
{
	class JCNote
	{
		public string Filename { get; set; }

		public string Text { get; set; }

		public DateTime Date { get; set; }

		public JCNote()
		{
			Filename = $"{Path.GetRandomFileName()}.notes.txt";
			Date = DateTime.Now;
			Text = "";
		}

		public void Save() =>
File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, Filename), Text);

		public void Delete() =>
			File.Delete(Path.Combine(FileSystem.AppDataDirectory, Filename));

		public static JCNote Load(string filename)
		{
			filename = Path.Combine(FileSystem.AppDataDirectory, filename);

			if (!File.Exists(filename))
				throw new FileNotFoundException("No se encontró el archivo.", filename);

			return
				new()
				{
					Filename = Path.GetFileName(filename),
					Text = File.ReadAllText(filename),
					Date = File.GetLastWriteTime(filename)
				};
		}

		public static IEnumerable<JCNote> LoadAll()
		{
			// Get the folder where the notes are stored.
			string appDataPath = FileSystem.AppDataDirectory;

			// Use Linq extensions to load the *.notes.txt files.
			return Directory

					// Select the file names from the directory
					.EnumerateFiles(appDataPath, "*.notes.txt")

					// Each file name is used to load a note
					.Select(filename => JCNote.Load(Path.GetFileName(filename)))

					// With the final collection of notes, order them by date
					.OrderByDescending(note => note.Date);
		}
	}
}
