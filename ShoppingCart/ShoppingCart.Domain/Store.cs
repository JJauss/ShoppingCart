namespace Beedevelop.ShoppingCart.Domain{
  public class Store:IRanked{
    public Store(){
      Items = new CardItemCollection();
    }

    public string Name{ get; set; }

    public CardItemCollection Items{ get; }

    public int Position{ get; private set; }

    /// <inheritdoc />
    int IRanked.Rank{
      set => Position = value;
    }
  }
}