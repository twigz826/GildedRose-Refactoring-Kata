namespace GildedRoseKata
{
    public class GildedRose
    {
        private const int Min_Quality = 0;
        private const int Max_Quality = 50;

        private const int Current_Date_Value = 0;
        private const int Daily_SellIn_Adjustment = 1;

        private const int Standard_Quality_Adjustment = 1;
        private const int Accelerated_Quality_Adjustment = 2;

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

                switch (item.Name)
                {
                    case "Aged Brie":
                        IncreaseItemQuality(item, Standard_Quality_Adjustment);
                        DegradeItemSellIn(item);
                        AgedBrieAdjustment(item);
                        break;
                    case var backstagePass when backstagePass.StartsWith("Backstage passes"):
                        AdjustQualityOfBackstagePasses(item);
                        DegradeItemSellIn(item);
                        ResetQualityIfConcertFinished(item);
                        break;
                    case var conjuredItem when conjuredItem.StartsWith("Conjured"):
                        DegradeItemQuality(item, Accelerated_Quality_Adjustment);
                        DegradeItemSellIn(item);
                        DegradeIfOutOfDate(item, Accelerated_Quality_Adjustment);
                        break;
                    default:
                        DegradeItemQuality(item, Standard_Quality_Adjustment);
                        DegradeItemSellIn(item);
                        DegradeIfOutOfDate(item, Standard_Quality_Adjustment);
                        break;
                }
            }
        }

        private static void DegradeIfOutOfDate(Item item, int valueDecrease)
        {
            if (!IsItemOutOfDate(item))
            {
                return;
            }
            DegradeQuality(item, valueDecrease);
            ResetQualityIfMinReached(item);
        }

        private static void AgedBrieAdjustment(Item item)
        {
            if (!IsItemOutOfDate(item))
            {
                return;
            }

            IncreaseItemQuality(item, Standard_Quality_Adjustment);
            ResetMaxQualityIfExceeded(item);
        }

        private static void AdjustQualityOfBackstagePasses(Item item)
        {
            ResetQualityIfConcertFinished(item);
            IncreaseItemQuality(item, Standard_Quality_Adjustment);

            if (IsConcertHappeningWithinTenDays(item))
            {
                IncreaseItemQuality(item, Standard_Quality_Adjustment);
            }

            if (IsConcertHappeningWithinFiveDays(item))
            {
                IncreaseItemQuality(item, Standard_Quality_Adjustment);
            }

            ResetMaxQualityIfExceeded(item);
        }

        private static void ResetQualityIfConcertFinished(Item item)
        {
            if (!IsConcertFinished(item))
            {
                return;
            }
            item.Quality = Min_Quality;
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
            return item.SellIn < 6;
        }

        private static bool IsConcertHappeningWithinTenDays(Item item)
        {
            return item.SellIn < 11;
        }

        private bool IsLegendaryItem(Item item)
        {
            return Legendary_Items.Contains(item.Name);
        }

        private static void IncreaseItemQuality(Item item, int valueIncrease)
        {
            item.Quality += valueIncrease;
            ResetMaxQualityIfExceeded(item);
        }

        private static void DegradeItemQuality(Item item, int valueDecrease)
        {
            DegradeQuality(item, valueDecrease);
            ResetQualityIfMinReached(item);
        }

        private static void ResetMaxQualityIfExceeded(Item item)
        {
            if (IsMaxQualityExceeded(item))
            {
                item.Quality = Max_Quality;
            }
        }

        private static void ResetQualityIfMinReached(Item item)
        {
            if (IsItemAtOrBelowMinQualityValue(item))
            {
                item.Quality = Min_Quality;
                return;
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
            item.SellIn -= Daily_SellIn_Adjustment;
        }
    }
}
