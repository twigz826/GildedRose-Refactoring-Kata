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
            var items = new List<Item> { new Item { Name = "StandardItem", SellIn = 10, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(9);
            items[0].Quality.Should().Be(9);
        }

        [Fact]
        public void UpdateQuality_WhenStandardItemAndQualityAt0_QualityRemainsAt0()
        {
            var items = new List<Item> { new Item { Name = "StandardItem", SellIn = 10, Quality = 0 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(9);
            items[0].Quality.Should().Be(0);
        }

        [Fact]
        public void UpdateQuality_WhenStandardItemAndNegative_QualityDegradesTwiceAsFast()
        {
            var items = new List<Item> { new Item { Name = "StandardItem", SellIn = 0, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(-1);
            items[0].Quality.Should().Be(8);
        }

        [Fact]
        public void UpdateQuality_WhenAgedBrie_QualityIncreases()
        {
            var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(9);
            items[0].Quality.Should().Be(11);
        }

        [Fact]
        public void UpdateQuality_WhenAgedBrie_QualityIncreasesTwiceAsFastWhenPastSellbyDate()
        {
            var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(-1);
            items[0].Quality.Should().Be(12);
        }

        [Fact]
        public void UpdateQuality_WhenAnyItem_QualityDoesNotGoAbove50()
        {
            var items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = 10, Quality = 50 },
                new Item { Name = "Another Item", SellIn = 10, Quality = 50 }
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(9);
            items[0].Quality.Should().Be(50);
            items[1].SellIn.Should().Be(9);
            items[1].Quality.Should().Be(49);
        }

        [Fact]
        public void UpdateQuality_WhenLegendaryItem_SellInAndQualityDoNotChange()
        {
            var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 20, Quality = 80 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(20);
            items[0].Quality.Should().Be(80);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityIncreasesWhenSellInOver10()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(19);
            items[0].Quality.Should().Be(11);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityIncreasesBy2WhenSellInBetween10And6()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(9);
            items[0].Quality.Should().Be(12);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityIncreasesBy3WhenSellInBetween5And1()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(4);
            items[0].Quality.Should().Be(13);
        }

        [Fact]
        public void UpdateQuality_WhenBackstagePasses_QualityGoesTo0WhenSellInReachesNegative1()
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(-1);
            items[0].Quality.Should().Be(0);
        }

        [Fact]
        public void UpdateQuality_WhenConjuredItems_QualityDegradesTwiceAsFast()
        {
            var items = new List<Item> { new Item { Name = "Conjured Dorito", SellIn = 8, Quality = 20 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(7);
            items[0].Quality.Should().Be(18);
        }
    }
}
