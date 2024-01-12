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
            var items = new List<Item> { new() { Name = "StandardItem", SellIn = 10, Quality = 10 } };
            var app = new GildedRose(items);
            app.UpdateQuality();
            items[0].SellIn.Should().Be(9);
            items[0].Quality.Should().Be(9);
        }
    }
}
