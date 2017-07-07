using System;
using Beedevelop.ShoppingCart.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests{
  [TestClass]
  public class CardItemCollectionTests{
    [TestMethod]
    public void AddingItems_ShouldbeAvailableInList(){
      CardItemCollection cardItemCollection = new CardItemCollection{new CardItem{Name = "Butter", Count = 2}, new CardItem{Name = "Brot", Count = 1}, new CardItem{Name = "Käse geschnitten", Count = 1}};
      Assert.AreEqual(3, cardItemCollection.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddItem_WithoutName_ShouldThrow(){
      CardItemCollection cardItemCollection = new CardItemCollection();
      cardItemCollection.Add(new CardItem());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddItem_WithNull_ShouldThrow(){
      // ReSharper disable once ObjectCreationAsStatement
      new CardItemCollection{null};
    }

    [TestMethod]
    public void AddItem_ShouldSetPosition(){
      CardItemCollection cardItemCollection = new CardItemCollection();
      CardItem item1 = new CardItem{Name = "test 01"};
      CardItem item2 = new CardItem{Name = "test 02"};
      cardItemCollection.Add(item1);
      cardItemCollection.Add(item2);
      Assert.AreEqual(1, item1.Position);
      Assert.AreEqual(2, item2.Position);
    }

    [TestMethod]
    public void MoveUp_OnFirstItem_ShouldIgnore(){
      CardItemCollection cardItemCollection = new CardItemCollection();
      CardItem item = new CardItem{Name = "test"};
      cardItemCollection.Add(item);
      cardItemCollection.MoveUp(item);
      Assert.AreEqual(1, item.Position);
    }

    [TestMethod]
    public void MoveUp_OnSecondItem_ShouldMove(){
      CardItemCollection cardItemCollection = new CardItemCollection();
      CardItem item1 = new CardItem{Name = "test 01"};
      CardItem item2 = new CardItem{Name = "test 02"};
      cardItemCollection.Add(item1);
      cardItemCollection.Add(item2);
      cardItemCollection.MoveUp(item2);
      Assert.AreEqual(2, item1.Position);
      Assert.AreEqual(1, item2.Position);
    }

    [TestMethod]
    public void MoveDown_OnLastItem_ShouldIgnore(){
      CardItemCollection cardItemCollection = new CardItemCollection();
      CardItem cardItem = new CardItem{Name = "test"};
      cardItemCollection.Add(cardItem);
      cardItemCollection.MoveDown(cardItem);
      Assert.AreEqual(1, cardItem.Position);
    }

    [TestMethod]
    public void MoveDown_OnSecondItem_ShouldMove(){
      CardItemCollection cardItemCollection = new CardItemCollection();
      CardItem item1 = new CardItem{Name = "test 01"};
      CardItem item2 = new CardItem{Name = "test 02"};
      CardItem item3 = new CardItem{Name = "test 03"};
      cardItemCollection.Add(item1);
      cardItemCollection.Add(item2);
      cardItemCollection.Add(item3);
      cardItemCollection.MoveDown(item2);
      Assert.AreEqual(1, item1.Position);
      Assert.AreEqual(3, item2.Position);
      Assert.AreEqual(2, item3.Position);
    }

    [TestMethod]
    public void RemoveItem_ShouldRecalcPositions(){
      CardItemCollection cardItemCollection = new CardItemCollection();
      CardItem item1 = new CardItem{Name = "test 01"};
      CardItem item2 = new CardItem{Name = "test 02"};
      CardItem item3 = new CardItem{Name = "test 03"};
      cardItemCollection.Add(item1);
      cardItemCollection.Add(item2);
      cardItemCollection.Add(item3);

      cardItemCollection.Remove(item2);

      Assert.AreEqual(1, item1.Position);
      Assert.AreEqual(2, item3.Position);
    }

    [TestMethod]
    [ExpectedException(typeof(DuplicateItemException))]
    public void DuplucateName_ShouldThrow(){
      // ReSharper disable once ObjectCreationAsStatement
      new CardItemCollection{new CardItem{Name = "test"}, new CardItem{Name = "test"}};
    }

    [TestMethod]
    public void AddOrMerge_ShouldAddNewItems(){
      CardItemCollection cardItemCollection = new CardItemCollection{new CardItem{Name = "test 01"}};
      cardItemCollection.AddItem(new CardItem{Name = "test 02"});
      Assert.AreEqual(2, cardItemCollection.Count);
    }

    [TestMethod]
    public void AddOrMerge_ShouldMergeExistingItem() {
      CardItemCollection cardItemCollection = new CardItemCollection { new CardItem { Name = "test 01" } };
      CardItem existing = cardItemCollection.AddItem(new CardItem { Name = "test 01", Count = 2});
      Assert.AreEqual(1, cardItemCollection.Count);
      Assert.AreEqual(3, existing.Count);
    }


    [TestMethod]
    public void RemoveItem_SameNameSubstractsCount() {
      CardItem cardItem = new CardItem { Name = "test 01", Count = 10 };
      CardItemCollection cardItemCollection = new CardItemCollection { cardItem };
      cardItemCollection.RemoveItem(new CardItem{Name = "test 01", Count = 3});
      Assert.AreEqual(7, cardItem.Count);
    }

    [TestMethod]
    public void RemoveItem_SameName_RemoveItem() {
      CardItem cardItem = new CardItem { Name = "test 01", Count = 10 };
      CardItemCollection cardItemCollection = new CardItemCollection { cardItem };
      cardItemCollection.RemoveItem(new CardItem { Name = "test 01", Count = 12 });
      Assert.AreEqual(-1, cardItemCollection.IndexOf(cardItem));
    }
  }
}