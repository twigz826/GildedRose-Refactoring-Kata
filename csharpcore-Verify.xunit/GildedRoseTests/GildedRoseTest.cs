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

        [Fact]
        public void UpdateQuality_WhenStandardItemAndQualityAt0_QualityRemainsAt0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "StandardItem", SellIn = 10, Quality = 0 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(9);
            Items[0].Quality.Should().Be(0);
        }

        [Fact]
        public void UpdateQuality_WhenStandardItemAndNegative_QualityDegradesTwiceAsFast()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "StandardItem", SellIn = 0, Quality = 10 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(-1);
            Items[0].Quality.Should().Be(8);
        }

        [Fact]
        public void UpdateQuality_WhenAgedBrie_QualityIncreases()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(-1);
            Items[0].Quality.Should().Be(11);
        }
    }
}
