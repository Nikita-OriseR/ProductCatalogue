using GalaSoft.MvvmLight;
using System;

namespace ProductCatalogue.Models
{
    public class Item : ObservableObject, IItem
    {
        private Guid _Id;
        private Guid _IdPrice;
        private int _Code;
        private string _Name;
        private string _BarCode;
        private Decimal _Quantity;
        private string _Model;
        private string _Sort;
        private string _Color;
        private string _Size;
        private string _Wight;
        private DateTime _DateChanges;

        /// <summary>
        /// Get or set Id value
        /// </summary>
        public Guid Id
        {
            get { return _Id; }
            set { Set(ref _Id, value); }
        }

        /// <summary>
        /// Get or set IdPrice value
        /// </summary>
        public Guid IdPrice
        {
            get { return _IdPrice; }
            set { Set(ref _IdPrice, value); }
        }

        /// <summary>
        /// Get or set Code value
        /// </summary>
        public int Code
        {
            get { return _Code; }
            set { Set(ref _Code, value); }
        }

        /// <summary>
        /// Get or set Name value
        /// </summary>
        public string Name  
        {
            get { return _Name; }
            set { Set(ref _Name, value); }
        }

        /// <summary>
        /// Get or set BarCode value
        /// </summary>
        public string BarCode
        {
            get { return _BarCode; }
            set { Set(ref _BarCode, value); }
        }

        /// <summary>
        /// Get or set Quantity value
        /// </summary>
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { Set(ref _Quantity, value); }
        }

        /// <summary>
        /// Get or set Model value
        /// </summary>
        public string Model
        {
            get { return _Model; }
            set { Set(ref _Model, value); }
        }

        /// <summary>
        /// Get or set Sort value
        /// </summary>
        public string Sort
        {
            get { return _Sort; }
            set { Set(ref _Sort, value); }
        }

        /// <summary>
        /// Get or set Color value
        /// </summary>
        public string Color
        {
            get { return _Color; }
            set { Set(ref _Color, value); }
        }

        /// <summary>
        /// Get or set Size value
        /// </summary>
        public string Size
        {
            get { return _Size; }
            set { Set(ref _Size, value); }
        }

        /// <summary>
        /// Get or set Wight value
        /// </summary>
        public string Wight
        {
            get { return _Wight; }
            set { Set(ref _Wight, value); }
        }

        /// <summary>
        /// Get or set DateChanges value
        /// </summary>
        public DateTime DateChanges
        {
            get { return _DateChanges; }
            set { Set(ref _DateChanges, value); }
        }
    }
}