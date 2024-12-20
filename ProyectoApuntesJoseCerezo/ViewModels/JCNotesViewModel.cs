using CommunityToolkit.Mvvm.Input;
using ProyectoApuntesJoseCerezo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProyectoApuntesJoseCerezo.ViewModels
{
	internal class JCNotesViewModel : IQueryAttributable
	{
		public ObservableCollection<ViewModels.JCNoteViewModel> AllNotes { get; }

		public ICommand NewCommand { get; }

		public ICommand SelectNoteCommand { get; }

		public JCNotesViewModel()
		{
			AllNotes = new ObservableCollection<ViewModels.JCNoteViewModel>(JCNote.LoadAll().Select(n => new JCNoteViewModel(n)));
			NewCommand = new AsyncRelayCommand(NewNoteAsync);
			SelectNoteCommand = new AsyncRelayCommand<ViewModels.JCNoteViewModel>(SelectNoteAsync);
		}

		private async Task NewNoteAsync()
		{
			await Shell.Current.GoToAsync(nameof(Views.NotePage));
		}

		private async Task SelectNoteAsync(ViewModels.JCNoteViewModel note)
		{
			if (note != null)
				await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
		}

		void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
		{
			if (query.ContainsKey("deleted"))
			{
				string noteId = query["deleted"].ToString();
				JCNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

				// If note exists, delete it
				if (matchedNote != null)
					AllNotes.Remove(matchedNote);
			}
			else if (query.ContainsKey("saved"))
			{
				string noteId = query["saved"].ToString();
				JCNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

				// If note is found, update it
				if (matchedNote != null)
					matchedNote.Reload();

				// If note isn't found, it's new; add it.
				else
					AllNotes.Add(new JCNoteViewModel(JCNote.Load(noteId)));
			}
		}
	}
}
