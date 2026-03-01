using CommunityToolkit.Mvvm.ComponentModel;
using IoC_Container.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelPlanning.Utilties;

namespace TravelPlanning.Components.MapPanels.AddSavePlaceList
{
    [Transient]
    public class AddSaveListContext : ObservableObject
    {
        private string _saveListName = "未命名清單";
        private string _backupName;
        private bool _isEditingName;

        public string SaveListName
        {
            get => _saveListName;
            set => SetProperty(ref _saveListName, value);
        }

        public bool IsEditingName
        {
            get => _isEditingName;
            set => SetProperty(ref _isEditingName, value);
        }

        public ICommand BeginEditNameCommand => new RelayCommand(() =>
        {
            _backupName = SaveListName;
            IsEditingName = true;
        });

        public ICommand EndEditNameCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrWhiteSpace(SaveListName))
                SaveListName = "未命名清單";
            IsEditingName = false;
        });

        public ICommand CancelEditNameCommand => new RelayCommand(() =>
        {
            SaveListName = _backupName;
            IsEditingName = false;
        });
        public int SaveListCount { get; set; }
        public ObservableCollection<SaveListPlaceViewModel> SaveLists { get; set; }
        = new ObservableCollection<SaveListPlaceViewModel>()
        {
            new SaveListPlaceViewModel() {
                Name = "台北車站",
                Type = "車站",
                Rate = 2.5f
            }
        };
    
    }
}
