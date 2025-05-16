using Avatab.Model;
using Avatab.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avatab.ViewModel
{
    public partial class ImportPopupViewModel : ObservableObject
    {
        public ImportPopupViewModel(IDatabaseService _databaseService) {
            databaseService = _databaseService;
        }

        private IDatabaseService databaseService;
        private int parentId;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string importStatus;

        private List<DBLecture> lectures = new List<DBLecture>();

        [RelayCommand]
        private async Task PickFile()
        {
            if (Name != String.Empty) return;
            parentId = databaseService.AddPerson(new DBPerson { name = Name });
            try
            {
                var result = await FilePicker.PickAsync();
                if (result == null) return;

                using var stream = await result.OpenReadAsync();
                using var reader = new StreamReader(stream);
                int lineIndex = 0;

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    lineIndex++;
                    if (lineIndex <= 1) continue;

                    var parts = line.Split(';');
                    if (parts.Length < 10) continue;
                    char lectureType = parts[6].Trim()[0];
                    lectures.Add(new DBLecture(
                        parts[3].Trim(),
                        parts[4],
                        lectureType == 'w' ? LectureType.wyklad : lectureType == 'ć' ? LectureType.cwiczenia : LectureType.lektorat,
                        parts[8].Trim(),
                        parts[9].Trim(),
                        DateTime.Parse(parts[7].Trim()),
                        parts[10].Trim() + ' ' + parts[11].Trim(),
                        parentId));
                }
                ImportStatus = "Success";

            }
            catch (Exception ex)
            {
                ImportStatus = $"Error: {ex.Message}";
            }
        }
        [RelayCommand]
        private void Import()
        {
            foreach(DBLecture lecture in lectures)
            {
                databaseService.AddLecture(lecture);
            }
        }
    }
}
