using ProductCatalogue.EventArgs;
using ProductCatalogue.Models;
using ProductCatalogue.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProductCatalogue.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Variables

        private ObservableCollection<Item> _allItems;
        private Item _selectedItem;

        #endregion Variables

        #region Constructors

        public MainViewModel(IDataProvider dataProvider, IEditWindowController editWindowController, IDialogService dialogService)
        {
            DataProvider = dataProvider;
            EditWindowController = editWindowController;
            DialogService = dialogService;

            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand<Item>(EditItem, item => SelectedItem != null);
            DeleteItemCommand = new RelayCommand<Item>(DeleteItem, item => SelectedItem != null);

            AllItems = new ObservableCollection<Item>(dataProvider.GetAllItems().OfType<Item>());
        }

        #endregion Constructors

        #region Properties

        public RelayCommand AddItemCommand { get; set; }

        /// <summary>
        /// Get or set AllFriends value
        /// </summary>
        public ObservableCollection<Item> AllItems
        {
            get { return _allItems; }
            set { Set(ref _allItems, value); }
        }

        public IDataProvider DataProvider { get; }
        public RelayCommand<Item> DeleteItemCommand { get; set; }
        public IDialogService DialogService { get; }
        public RelayCommand<Item> EditItemCommand { get; set; }
        public IEditWindowController EditWindowController { get; }

        /// <summary>
        /// Get or set SelectedItem value
        /// </summary>
        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);

                // If SelectedFriend property changed, check if Edit&Delete commands can execute
                DeleteItemCommand.RaiseCanExecuteChanged();
                EditItemCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion Properties

        #region Methods

        private void AddItem()
        {
            var result = EditWindowController.ShowDialog(new OpenEditWindowArgs { Type = ActionType.Add });
            if (result.HasValue && result.Value)
            {
                AllItems = new ObservableCollection<Item>(DataProvider.GetAllItems().OfType<Item>());
            }
        }

        private void DeleteItem(Item item)
        {
            if (DialogService.Confirm("Really want to delete this item?"))
            {
                AllItems.Remove(item);
                DataProvider.Delete(item);
                DialogService.ShowMessage("Item delete successfully");
            }
        }

        private void EditItem(Item item)
        {
            var result = EditWindowController.ShowDialog(new OpenEditWindowArgs { Type = ActionType.Edit, Item = SelectedItem });
            if (result.HasValue && result.Value)
            {
                // Remember user's selection
                int index = AllItems.IndexOf(SelectedItem);
                AllItems = new ObservableCollection<Item>(DataProvider.GetAllItems().OfType<Item>());

                // re-selected the original item
                SelectedItem = AllItems[index];
            }
        }

        #endregion Methods
    }
}