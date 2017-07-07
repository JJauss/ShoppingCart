using System;

namespace Beedevelop.ShoppingCart.Domain{
  public class InvalidCountException : Exception{
    /// <inheritdoc />
    public override string Message => "Unable to set count lower then 1. Remove item if no count wanted.";
  }
}