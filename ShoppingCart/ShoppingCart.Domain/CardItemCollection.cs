using System;
using System.Linq;

namespace Beedevelop.ShoppingCart.Domain{
  public class CardItemCollection : OrderedCollection<CardItem>{


    public CardItem AddItem(CardItem item){
      if (item == null){
        throw new ArgumentNullException(nameof(item));
      }
      CardItem existing = GetExisting(item);
      if (existing != null){
        existing.Count += item.Count;
      } else{
        existing = item;
        Add(existing);
      }
      return existing;
    }


    public void RemoveItem(CardItem item){
      if (item == null){
        throw new ArgumentNullException(nameof(item));
      }
      CardItem existing = GetExisting(item);
      if (existing?.Count > item.Count - 1){
        existing.Count -= item.Count;
      } else{
        Remove(existing);
      }
    }

    private CardItem GetExisting(CardItem item){
      return this.SingleOrDefault(_ => string.Equals(_.Name, item.Name, StringComparison.OrdinalIgnoreCase));
    }


    /// <inheritdoc />
    protected override void InsertItem(int index, CardItem item){
      if (item == null){
        throw new ArgumentNullException(nameof(item));
      }
      if (string.IsNullOrWhiteSpace(item.Name)) throw new ArgumentException(nameof(item.Name));

      if (this.Any(_ => string.Equals(_.Name, item.Name, StringComparison.OrdinalIgnoreCase))){
        throw new DuplicateItemException(item.Name);
      }


      base.InsertItem(index, item);
    }
  }
}