using ProductCatalogue.EventArgs;

namespace ProductCatalogue.Services
{
    public interface IEditWindowController
    {
        /// <summary>
        /// Show EditWindow
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool? ShowDialog(OpenEditWindowArgs args);
    }
}