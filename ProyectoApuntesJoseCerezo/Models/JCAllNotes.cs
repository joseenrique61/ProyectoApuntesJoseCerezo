using System.Collections.ObjectModel;

namespace ProyectoApuntesJoseCerezo.Models;

internal class JCAllNotes
{
    public ObservableCollection<JCNote> Notes { get; set; } = new ObservableCollection<JCNote>();

    public JCAllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<JCNote> notes = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.notes.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new JCNote()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetLastWriteTime(filename)
                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (JCNote note in notes)
            Notes.Add(note);
    }
}
