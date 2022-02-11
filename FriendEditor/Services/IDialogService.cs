using System;

namespace ProductCatalogue.Services
{
    public interface IDialogService
    {
        bool Confirm(string message);

        void ShowMessage(string message);

        void Warning(string message);

        void Exception(Exception ex);
    }
}