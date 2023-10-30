namespace GildedRoseKata
{
    public class GildedRose
    {
        private const int Quality_Min_Value = 0;
        private const int Current_Date_Value = 0;
        private const int Max_Quality = 50;

        private const int Backstage_Passes_First_Quality_Increase_Threshold = 10;
        private const int Backstage_Passes_Second_Quality_Increase_Threshold = 5;

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

                if (IsItemAtMaxQuality(item))
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

                //if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                //{
                //    if (Items[i].Quality > 0)
                //    {
                //        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                //        {
                //            Items[i].Quality = Items[i].Quality - 1;
                //        }
                //    }
                //}
                //else
                //{
                //    if (Items[i].Quality < 50)
                //    {
                //        Items[i].Quality = Items[i].Quality + 1;

                //        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                //        {
                //            if (Items[i].SellIn < 11)
                //            {
                //                if (Items[i].Quality < 50)
                //                {
                //                    Items[i].Quality = Items[i].Quality + 1;
                //                }
                //            }

                //            if (Items[i].SellIn < 6)
                //            {
                //                if (Items[i].Quality < 50)
                //                {
                //                    Items[i].Quality = Items[i].Quality + 1;
                //                }
                //            }
                //        }
                //    }
                //}

                //if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                //{
                //    Items[i].SellIn = Items[i].SellIn - 1;
                //}

                //if (Items[i].SellIn < 0)
                //{
                //    if (Items[i].Name != "Aged Brie")
                //    {
                //        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                //        {
                //            if (Items[i].Quality > 0)
                //            {
                //                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                //                {
                //                    Items[i].Quality = Items[i].Quality - 1;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                //        }
                //    }
                //    else
                //    {
                //        if (Items[i].Quality < 50)
                //        {
                //            Items[i].Quality = Items[i].Quality + 1;
                //        }
                //    }
                //}
            }
        }

        private void AdjustQualityOfBackstagePasses(Item item)
        {
            if (IsConcertFinished(item))
            {
                item.Quality = 0;
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

        }

        private static bool IsConcertFinished(Item item)
        {
            return item.SellIn <= 0;
        }

        private static bool IsConcertHappeningWithinFiveDays(Item item)
        {
            return item.SellIn > 0 && item.SellIn <= Backstage_Passes_Second_Quality_Increase_Threshold;
        }

        private static bool IsConcertHappeningInFiveToTenDays(Item item)
        {
            return item.SellIn > Backstage_Passes_Second_Quality_Increase_Threshold
                && item.SellIn <= Backstage_Passes_First_Quality_Increase_Threshold;
        }

        private static bool IsConcertOverTenDaysAway(Item item)
        {
            return item.SellIn > Backstage_Passes_First_Quality_Increase_Threshold;
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
            if (IsItemAtMinQualityValue(item))
            {
                return;
            }

            DegradeQuality(item, valueDecrease);

            if (IsItemOutOfDate(item))
            {
                DegradeQuality(item, valueDecrease);
            }
        }

        private static void DegradeQuality(Item item, int valueDecrease)
        {
            item.Quality -= valueDecrease;
        }

        private static bool IsItemAtMinQualityValue(Item item)
        {
            return item.Quality == Quality_Min_Value;
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
