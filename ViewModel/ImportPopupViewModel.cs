using Avatab.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avatab.ViewModel
{
    public partial class ImportPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        private int parentId;

        [ObservableProperty]
        private string importStatus;
        public ICommand PickFileCommand => new AsyncRelayCommand(PickFileAndParse);

        private async Task PickFileAndParse()
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result == null) return;

                var people = new List<DBLecture>();

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
                    people.Add(new DBLecture(
                        parts[3].Trim(),
                        parts[4],
                        lectureType == 'w' ? LectureType.wyklad : lectureType == 'ć' ? LectureType.cwiczenia : LectureType.lektorat,
                        parts[8].Trim(),
                        parts[9].Trim(),
                        DateTime.Parse(parts[7].Trim()),
                        parts[10].Trim() + ' ' + parts[11].Trim(),
                        ParentId));
                }


            }
            catch (Exception ex)
            {
                ImportStatus = $"Error: {ex.Message}";
            }
        }
    }
}
