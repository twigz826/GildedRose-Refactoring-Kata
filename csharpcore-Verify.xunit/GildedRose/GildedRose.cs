namespace GildedRoseKata
{
    public class GildedRose
    {
        private const int Min_Quality = 0;
        private const int Current_Date_Value = 0;
        private const int Max_Quality = 50;

        private readonly List<string> Legendary_Items = new() { "Sulfuras, Hand of Ragnaros" };
        private readonly IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (IsLegendaryItem(item))
                {
                    continue;
                }

                DegradeItemSellIn(item);

                if (IsItemAtMaxQuality(item) && !IsItemBackstagePass(item))
                {
                    continue;
                }

                switch (item.Name)
                {
                    case "Aged Brie":
                        IncreaseItemQuality(item, 1);
                        break;
                    case var backstagePass when backstagePass.StartsWith("Backstage passes"):
                        AdjustQualityOfBackstagePasses(item);
                        break;
                    case var conjuredItem when conjuredItem.StartsWith("Conjured"):
                        DegradeItemQuality(item, 2);
                        break;
                    default:
                        DegradeItemQuality(item, 1);
                        break;
                }
            }
        }

        private static bool IsItemBackstagePass(Item item)
        {
            return item.Name.StartsWith("Backstage passes");
        }

        private static void AdjustQualityOfBackstagePasses(Item item)
        {
            if (IsConcertFinished(item))
            {
                item.Quality = Min_Quality;
                return;
            }

            if (IsConcertOverTenDaysAway(item))
            {
                IncreaseItemQuality(item, 1);
            }

            if (IsConcertHappeningInFiveToTenDays(item))
            {
                IncreaseItemQuality(item, 2);
            }

            if (IsConcertHappeningWithinFiveDays(item))
            {
                IncreaseItemQuality(item, 3);
            }

            if (IsMaxQualityExceeded(item))
            {
                item.Quality = Max_Quality;
            }
        }

        private static bool IsMaxQualityExceeded(Item item)
        {
            return item.Quality > Max_Quality;
        }

        private static bool IsConcertFinished(Item item)
        {
            return IsItemOutOfDate(item);
        }

        private static bool IsConcertHappeningWithinFiveDays(Item item)
        {
            return item.SellIn >= 0 && item.SellIn <= 5;
        }

        private static bool IsConcertHappeningInFiveToTenDays(Item item)
        {
            return item.SellIn > 5 && item.SellIn <= 10;
        }

        private static bool IsConcertOverTenDaysAway(Item item)
        {
            return item.SellIn > 10;
        }

        private bool IsLegendaryItem(Item item)
        {
            return Legendary_Items.Contains(item.Name);
        }

        private static bool IsItemAtMaxQuality(Item item)
        {
            return item.Quality == Max_Quality;
        }

        private static void IncreaseItemQuality(Item item, int valueIncrease)
        {
            item.Quality += valueIncrease;
        }

        private static void DegradeItemQuality(Item item, int valueDecrease)
        {
            DegradeQuality(item, valueDecrease);

            if (IsItemAtOrBelowMinQualityValue(item))
            {
                item.Quality = Min_Quality;
                return;
            }

            if (IsItemOutOfDate(item))
            {
                DegradeQuality(item, valueDecrease);
            }
        }

        private static void DegradeQuality(Item item, int valueDecrease)
        {
            item.Quality -= valueDecrease;
        }

        private static bool IsItemAtOrBelowMinQualityValue(Item item)
        {
            return item.Quality <= Min_Quality;
        }

        private static bool IsItemOutOfDate(Item item)
        {
            return item.SellIn < Current_Date_Value;
        }

        private static void DegradeItemSellIn(Item item)
        {
            item.SellIn -= 1;
        }
    }
}
