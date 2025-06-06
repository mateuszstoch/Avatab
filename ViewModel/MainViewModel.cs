﻿using Avatab.Model;
using Avatab.Services.Interfaces;
using Avatab.View;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avatab.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private IDatabaseService databaseService;

        [ObservableProperty]
        private List<DBPerson> dBPeople;
        private ImportPopupViewModel importPopupViewModel;
        public MainViewModel(IDatabaseService _databaseService, ImportPopupViewModel _importPopupViewModel)
        {
            databaseService = _databaseService;
            DBPeople = new List<DBPerson>();
            importPopupViewModel = _importPopupViewModel;
            isRefreshing = false;
            this.Refresh();
        }

        [ObservableProperty]
        private bool isRefreshing;



        [RelayCommand]
        public void Refresh()
        {
            IsRefreshing = true;
            DBPeople = databaseService.GetAllPeople();
            foreach (DBPerson person in DBPeople)
            {
                if (databaseService.GetLectureOnTime(person.Id, DateTime.Now).Count > 0)
                {
                    person.isOccupied = true;
                }
            }
            DBPeople.Sort((a, b) => a.isOccupied.CompareTo(b.isOccupied));
            IsRefreshing = false;
        }

        [RelayCommand]
        public async void Import()
        {
            importPopupViewModel.reset();
            var output = await App.Current.MainPage.ShowPopupAsync(new ImportPopup(importPopupViewModel));
            if (output == null) return;
            foreach (DBLecture lecture in (List<DBLecture>)output)
            {
                databaseService.AddLecture(lecture);
            }
            this.Refresh();
        }

        [RelayCommand]
        public void Delete(DBPerson person)
        {
            databaseService.DeletePerson(person.Id);
            this.Refresh();
        }

        [RelayCommand]
        public async void Edit(DBPerson person)
        {
            Dictionary<string, object> navigationParameter = new Dictionary<string, object>();
            navigationParameter.Add("person", person);
            await Shell.Current.GoToAsync(Routes.PersonEditPage, navigationParameter);
        }

    }
}
