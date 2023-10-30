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

        [Fact]
        public void UpdateQuality_WhenAnyItem_QualityDoesNotGoAbove50()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = 10, Quality = 50 },
                new Item { Name = "Another Item", SellIn = 0, Quality = 50 }
            };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(9);
            Items[0].Quality.Should().Be(50);
            Items[1].SellIn.Should().Be(-1);
            Items[1].Quality.Should().Be(50);
        }

        [Fact]
        public void UpdateQuality_WhenLegendaryItem_SellInAndQualityDoNotChange()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 20, Quality = 80 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(20);
            Items[0].Quality.Should().Be(80);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityIncreasesWhenSellInOver10()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 10 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(19);
            Items[0].Quality.Should().Be(11);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityIncreasesBy2WhenSellInBetween10And6()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(10);
            Items[0].Quality.Should().Be(12);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityIncreasesBy3WhenSellInBetween5And1()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 6, Quality = 10 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(5);
            Items[0].Quality.Should().Be(13);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityGoesTo0WhenSellInReaches0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 10 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(0);
            Items[0].Quality.Should().Be(0);
        }

        [Fact]
        public void UpdateQuality_WhenConjuredItems_QualityDegradesTwiceAsFast()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Conjured Dorito", SellIn = 8, Quality = 20 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Items[0].SellIn.Should().Be(7);
            Items[0].Quality.Should().Be(18);
        }
    }
}
