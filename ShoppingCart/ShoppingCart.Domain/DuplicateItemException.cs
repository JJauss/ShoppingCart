using System;

namespace Beedevelop.ShoppingCart.Domain{
  public class DuplicateItemException : Exception{
    private readonly string _name;

    public DuplicateItemException(string name){
      _name = name;
    }

    /// <inheritdoc />
    public override string Message => $"The item '{_name}' already exists.";
  }
}