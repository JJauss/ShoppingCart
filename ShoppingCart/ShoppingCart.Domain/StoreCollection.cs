using System;
using System.Linq;

namespace Beedevelop.ShoppingCart.Domain{
  public class StoreCollection : OrderedCollection<Store>{
    /// <inheritdoc />
    protected override void InsertItem(int index, Store item){
      if (item == null) {
        throw new ArgumentNullException(nameof(item));
      }
      if (string.IsNullOrWhiteSpace(item.Name)) throw new ArgumentException(nameof(item.Name));

      if (this.Any(_ => string.Equals(_.Name, item.Name, StringComparison.OrdinalIgnoreCase))) {
        throw new DuplicateItemException(item.Name);
      }

      base.InsertItem(index, item);
    }
  }
}