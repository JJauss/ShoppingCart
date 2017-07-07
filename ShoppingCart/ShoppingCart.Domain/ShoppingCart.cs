namespace Beedevelop.ShoppingCart.Domain{
  public class ShoppingCart{
    private readonly CardItemCollection _items;
    private readonly StoreCollection _stores;

    public CardItemCollection Items => _items;

    public StoreCollection Stores => _stores;

    public ShoppingCart(){
      _items  = new CardItemCollection();
      _stores = new StoreCollection();
    }
  }
}