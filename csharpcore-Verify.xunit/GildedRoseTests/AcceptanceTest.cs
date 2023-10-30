using FluentAssertions;
using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class AcceptanceTest
    {
        readonly List<Item> items = new()
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 21 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 3, Quality = 30 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 28 },
            new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 },
            new Item { Name = "Conjured Rainbow Dust", SellIn = -10, Quality = 24 }
        };

        [Fact]
        public void GildedRoseFirstAcceptanceTest_OneDay()
        {
            var days = 1;
            var gildedRoseStore = CreateStore(items);

            for (var i = 0; i < days; i++)
            {
                gildedRoseStore.UpdateQuality();
            }

            items[0].Quality.Should().Be(19);
            items[1].Quality.Should().Be(22);
            items[2].Quality.Should().Be(29);
            items[3].SellIn.Should().Be(0);
            items[3].Quality.Should().Be(80);
            items[4].SellIn.Should().Be(-1);
            items[4].Quality.Should().Be(80);
            items[5].Quality.Should().Be(21);
            items[6].Quality.Should().Be(12);
            items[7].Quality.Should().Be(50);
            items[8].Quality.Should().Be(31);
            //items[9].SellIn.Should().Be(2);
            //items[9].Quality.Should().Be(4);
            //items[10].Quality.Should().Be(20);
        }

        [Fact]
        public void GildedRoseSecondAcceptanceTesty_FiveDays()
        {
            var days = 5;
            var gildedRoseStore = CreateStore(items);

            for (var i = 0; i < days; i++)
            {
                gildedRoseStore.UpdateQuality();
            }

            items[0].Quality.Should().Be(15);
            items[1].Quality.Should().Be(29);
            items[2].Quality.Should().Be(23);
            items[3].SellIn.Should().Be(0);
            items[3].Quality.Should().Be(80);
            items[4].SellIn.Should().Be(-1);
            items[4].Quality.Should().Be(80);
            items[5].Quality.Should().Be(25);
            items[6].Quality.Should().Be(20);
            items[7].Quality.Should().Be(50);
            items[8].Quality.Should().Be(0);
            //items[9].SellIn.Should().Be(-2);
            //items[9].Quality.Should().Be(0);
            //items[10].Quality.Should().Be(4);
        }

        [Fact]
        public void GildedRoseThirdAcceptanceTest_ThirtyDays()
        {
            var days = 30;
            var gildedRoseStore = CreateStore(items);

            for (var i = 0; i < days; i++)
            {
                gildedRoseStore.UpdateQuality();
            }

            items[0].Quality.Should().Be(0);
            items[1].Quality.Should().Be(50);
            items[2].Quality.Should().Be(0);
            items[3].SellIn.Should().Be(0);
            items[3].Quality.Should().Be(80);
            items[4].SellIn.Should().Be(-1);
            items[4].Quality.Should().Be(80);
            items[5].Quality.Should().Be(0);
            items[6].Quality.Should().Be(0);
            items[7].Quality.Should().Be(0);
            items[8].Quality.Should().Be(0);
            //items[9].SellIn.Should().Be(-27);
            //items[9].Quality.Should().Be(0);
            //items[10].Quality.Should().Be(0);
        }

        private static GildedRose CreateStore(List<Item> items)
        {
            return new GildedRose(items);
        }
    }
}