using ProductCatalogue.EventArgs;
using ProductCatalogue.Models;
using ProductCatalogue.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace ProductCatalogue.ViewModels
{
    public class EditViewModel : ViewModelBase
    {
        #region Constructors

        public EditViewModel(OpenEditWindowArgs args, IDataProvider dataProvider, IDialogService dialogService)
        {
            Args = args;
            DataProvider = dataProvider;
            DialogService = dialogService;

            switch (args.Type)
            {
                case ActionType.Add:
                    CurrentItem = new Item { Id = Guid.NewGuid(), DateChanges = new DateTime(1990, 1, 1) };
                    break;

                case ActionType.Edit:
                    // Clone a new object
                    CurrentItem = new Item
                    {
                        Id = args.Item.Id,
                        IdPrice = args.Item.IdPrice,
                        Code = args.Item.Code,
                        Name = args.Item.Name,
                        BarCode = args.Item.BarCode,
                        Quantity = args.Item.Quantity,
                        Model = args.Item.Model,
                        Sort = args.Item.Sort,
                        Color = args.Item.Color,
                        Size = args.Item.Size,
                        Wight = args.Item.Wight,
                        DateChanges = args.Item.DateChanges
                    };
                    break;
            }

            SaveDataCommand = new RelayCommand(SaveData);
        }

        #endregion Constructors

        #region Properties

        public Item CurrentItem { get; set; }
        public IDataProvider DataProvider { get; }
        public IDialogService DialogService { get; set; }
        public RelayCommand SaveDataCommand { get; set; }
        protected OpenEditWindowArgs Args { get; }

        #endregion Properties

        #region Methods

        private void SaveData()
        {
            if (string.IsNullOrWhiteSpace(CurrentItem.Id.ToString()))
            {
                DialogService.Warning("Id is required");
                return;
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.IdPrice.ToString()))
            {
                DialogService.Warning("Price id is required");
                return;
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.Code.ToString()))
            {
                DialogService.Warning("Code is required");
                return;
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.Name.ToString()))
            {
                DialogService.Warning("Name is required");
                return;
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.BarCode))
            {
                DialogService.Warning("Barcode is required");
                return;
            }

            bool result = false;
            switch (Args.Type)
            {
                case ActionType.Add:
                    result = DataProvider.Insert(CurrentItem);
                    break;

                case ActionType.Edit:
                    result = DataProvider.Update(CurrentItem);
                    break;
            }

            if (result)
            {
                DialogService.ShowMessage($"{Args.Type} item successfully");

                // Send a message to Close the current View(EditWindow)
                Messenger.Default.Send(new CloseWindowEventArgs());
            }
            else
            {
                DialogService.Warning($"Error occured, save data failed");
            }
        }

        #endregion Methods
    }
}