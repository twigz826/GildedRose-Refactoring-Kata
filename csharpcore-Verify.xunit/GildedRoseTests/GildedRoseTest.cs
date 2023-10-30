using FluentAssertions;
using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        public void UpdateQuality_WhenStandardItem_DecreasesSellInAndQualityBy1()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "StandardItem", SellIn = 10, Quality = 10 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(9);
            Items[0].Quality.Should().Be(9);
        }
    }
}
