using Avatab.Model;
using Avatab.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Avatab.ViewModel
{
    public partial class ImportPopupViewModel : ObservableObject
    {
        public ImportPopupViewModel(IDatabaseService _databaseService)
        {
            databaseService = _databaseService;
            persons = databaseService.GetAllPeople();
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
        private DataTemplate currentContent;

        [ObservableProperty]
        private bool isImportFromUsosTogled;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string importStatus;

        [ObservableProperty]
        private List<DBLecture> lectures;

        private List<DBPerson> persons;

        [ObservableProperty]
        private List<DBPerson> filteredPeople;

        [ObservableProperty]
        private bool isAddPersonMode = true;

        [ObservableProperty]
        private bool isImportScheduleMode = false;

        [ObservableProperty]
        private ObservableCollection<OptionItem> options = new();

        [ObservableProperty]
        private OptionItem selectedOption;

        [RelayCommand]
        private async Task PickFile()
        {
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
                    parentId = SelectedOption.Id
                });
            }

        }

        private async Task ImportFromCvs(StreamReader reader)
        {
            int lineIndex = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lineIndex++;
                if (lineIndex <= 1) continue;

                var parts = line.Split(';');
                if (parts.Length < 6) continue;
                var dateStructure = parts[2].Split("-");
                var timeStart = parts[3].Split(":");
                var timeEnd = parts[4].Split(":");
                DateTime date = new DateTime(int.Parse(dateStructure[0]), int.Parse(dateStructure[1]), int.Parse(dateStructure[2]));
                Lectures.Add(new DBLecture
                {
                    Name = parts[0].Trim(),
                    profesor = parts[1],
                    timeStart = new TimeSpan(int.Parse(timeStart[0]), int.Parse(timeStart[1]), 0),
                    timeEnd = new TimeSpan(int.Parse(timeEnd[0]), int.Parse(timeEnd[1]), 0),
                    date = date,
                    place = parts[5].Trim(),
                    parentId = SelectedOption.Id
                });
            }
        }

        [RelayCommand]
        private void ShowAddPerson()
        {
            IsAddPersonMode = true;
            IsImportScheduleMode = false;
        }

        [RelayCommand]
        private void ShowImportSchedule()
        {
            IsAddPersonMode = false;
            IsImportScheduleMode = true;
            persons = databaseService.GetAllPeople();
            Options.Clear();
            foreach (var person in persons)
            {
                Options.Add(new OptionItem
                {
                    Id = person.Id,
                    Name = person.name
                });
            }
        }

        [RelayCommand]
        private void AddNew()
        {
            databaseService.AddPerson(new DBPerson { name = Name });
        }

    }
    public class OptionItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public override string ToString() => Name;
    }

}
