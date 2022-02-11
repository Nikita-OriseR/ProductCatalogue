using ProductCatalogue.Models;

namespace ProductCatalogue.EventArgs
{
    /// <summary>
    /// Action Type
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Add item
        /// </summary>
        Add,

        /// <summary>
        /// Edit item
        /// </summary>
        Edit
    }

    /// <summary>
    /// Open EditWindow arguments
    /// </summary>
    public class OpenEditWindowArgs
    {
        /// <summary>
        /// If <see cref="ActionType"/> is Edit, then the value for this property need to be provided
        /// </summary>
        public Item Item { get; set; }

        public ActionType Type { get; set; }
    }
}