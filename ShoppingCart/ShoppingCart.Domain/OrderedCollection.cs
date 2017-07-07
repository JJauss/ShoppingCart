using System;
using System.Collections.ObjectModel;

namespace Beedevelop.ShoppingCart.Domain{
  public class OrderedCollection<T> : Collection<T> where T : class, IRanked{
    public void MoveUp(T item){
      int index = IndexOf(item);
      if (index > 0){
        RemoveAt(index);
        Insert(index - 1, item);
      }
    }

    /// <inheritdoc />
    protected override void InsertItem(int index, T item){
      if (item == null){
        throw new ArgumentNullException(nameof(item));
      }
      base.InsertItem(index, item);
      CalculatePositions();
    }

    private void CalculatePositions(){
      for (int index = Count - 1; index >= 0; index--)
        this[index].Rank = index + 1;
    }

    /// <inheritdoc />
    protected override void RemoveItem(int index){
      base.RemoveItem(index);
      CalculatePositions();
    }

    public void MoveDown(T item){
      int index = IndexOf(item);
      if (index < Count - 1){
        RemoveAt(index);
        Insert(index + 1, item);
      }
    }
  }
}