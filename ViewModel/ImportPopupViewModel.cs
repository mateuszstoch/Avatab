using Avatab.Model;
using Avatab.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avatab.ViewModel
{
    public partial class ImportPopupViewModel : ObservableObject
    {
        public ImportPopupViewModel(IDatabaseService _databaseService)
        {
            databaseService = _databaseService;
            Name = string.Empty;
            Lectures = new List<DBLecture>();
            ImportStatus = string.Empty;
        }

        private IDatabaseService databaseService;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string importStatus;

        [ObservableProperty]
        private List<DBLecture> lectures;

        [RelayCommand]
        private async Task PickFile()
        {
            if (Name == string.Empty) return;
            DBPerson person = new DBPerson { name = Name };
            databaseService.AddPerson(person);
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

                    var parts = line.Replace("\\", "").Replace("\"", "").Split(';');
                    if (parts.Length < 10) continue;
                    char lectureType = parts[4].Trim()[0];
                    var dateStructure = parts[7].Split("-");
                    Lectures.Add(new DBLecture
                    {
                        Name = parts[3].Trim(),
                        profesor = parts[6],
                        lectureType = lectureType == 'w' ? LectureType.wyklad : lectureType == 'ć' ? LectureType.cwiczenia : LectureType.lektorat,
                        timeStart = parts[8].Trim(),
                        timeEnd = parts[9].Trim(),
                        date = new DateTime(int.Parse(dateStructure[0]), int.Parse(dateStructure[1]), int.Parse(dateStructure[2])),
                        place = parts[10].Trim() + ' ' + parts[11].Trim(),
                        parentId = person.Id
                    });
                }
                ImportStatus = "Success";

            }
            catch (Exception ex)
            {
                ImportStatus = $"Error: {ex.Message}";
            }
        }

    }
}
