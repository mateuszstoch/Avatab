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
            this.reset();
        }

        public void reset()
        {
            IsImportFromUsosTogled = true;
            Name = string.Empty;
            Lectures = new List<DBLecture>();
            ImportStatus = string.Empty;
        }


        private IDatabaseService databaseService;

        [ObservableProperty]
        private bool isImportFromUsosTogled;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string importStatus;

        [ObservableProperty]
        private List<DBLecture> lectures;

        private DBPerson person;

        [RelayCommand]
        private async Task PickFile()
        {
            person = new DBPerson { name = Name };
            databaseService.AddPerson(person);
            try
            {
                var result = await FilePicker.PickAsync();
                if (result == null) return;

                using var stream = await result.OpenReadAsync();
                using var reader = new StreamReader(stream);

                if (IsImportFromUsosTogled)
                {
                    await ImportFromUsos(reader);

                }
                else
                {
                    await ImportFromCvs(reader);
                }


                ImportStatus = "Success";

            }
            catch (Exception ex)
            {
                ImportStatus = $"Error: {ex.Message}";
            }
        }

        private async Task ImportFromUsos(StreamReader reader)
        {
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
                var timeStart = parts[8].Split(":");
                var timeEnd = parts[9].Split(":");
                DateTime date = new DateTime(int.Parse(dateStructure[0]), int.Parse(dateStructure[1]), int.Parse(dateStructure[2]));
                Lectures.Add(new DBLecture
                {
                    Name = parts[3].Trim(),
                    profesor = parts[6],
                    lectureType = lectureType == 'w' ? LectureType.wyklad : lectureType == 'ć' ? LectureType.cwiczenia : LectureType.lektorat,
                    timeStart = new TimeSpan(int.Parse(timeStart[0]), int.Parse(timeStart[1]), 0),
                    timeEnd = new TimeSpan(int.Parse(timeEnd[0]), int.Parse(timeEnd[1]), 0),
                    date = date,
                    place = parts[10].Trim() + ' ' + parts[11].Trim(),
                    parentId = person.Id
                });
            }

        }

        private async Task ImportFromCvs(StreamReader reader)
        {
            throw new NotImplementedException();
        }

    }
}
