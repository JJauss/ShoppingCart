namespace Beedevelop.ShoppingCart.Domain{
  public class CardItem : IRanked{
    private int _count;

    public CardItem(){
      Count = 1;
    }

    public int Position{ get; private set; }

    public string Name{ get; set; }

    public int Count{
      get{ return _count; }
      set{
        if (value < 1){
          throw new InvalidCountException();
        }
        _count = value;
      }
    }

    /// <inheritdoc />
    int IRanked.Rank{
      set => Position = value;
    }
  }
}