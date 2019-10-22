using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models.Domain {
    public class Cart {
        #region Fields
        private readonly IList<CartLine> _lines = new List<CartLine>();
        #endregion

        #region Properties
        public IEnumerable<CartLine> CartLines => _lines.AsEnumerable();
        public int NumberOfItems => _lines.Count;
        public bool IsEmpty => NumberOfItems == 0;
        public decimal TotalValue => _lines.Sum(l => l.Product.Price * l.Quantity);
        #endregion

        #region Methods
        public void AddLine(Product product, int quantity) {
            CartLine line = _lines.FirstOrDefault(l => l.Product.Equals(product));
            if (line == null)
                _lines.Add(new CartLine(product, quantity));
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Product product) {
            CartLine line = _lines.SingleOrDefault(l => l.Product.Equals(product));
            if (line == null)
                throw new ArgumentException("Product not in cart");
            _lines.Remove(line);
        }

        public void Clear() {
            _lines.Clear();
        }

        #endregion
    }
}