namespace GildedRoseKata
{
    public class GildedRose
    {
        private const int Quality_Min_Value = 0;
        private const int Current_Date_Value = 0;
        private const int Max_Quality = 50;
        private List<string> Legendary_Items = new() { "Sulfuras, Hand of Ragnaros" };
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
                        IncreaseItemQuality(item);
                        break;
                    case var name when name.StartsWith("Backstage passes"):
                        AdjustQualityOfBackstagePasses(item);
                        break;
                    default:
                        DegradeItemQuality(item);
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
            IncreaseItemQuality(item);
        }

        private bool IsLegendaryItem(Item item)
        {
            return Legendary_Items.Contains(item.Name);
        }

        private bool IsItemAtMaxQuality(Item item)
        {
            return item.Quality == Max_Quality;
        }

        private static void IncreaseItemQuality(Item item)
        {
            item.Quality += 1;
        }

        private static void DegradeItemQuality(Item item)
        {
            if (IsItemAtMinValue(item))
            {
                return;
            }

            DegradeQuality(item);

            if (IsItemOutOfDate(item))
            {
                DegradeQuality(item);
            }
        }

        private static void DegradeQuality(Item item)
        {
            item.Quality -= 1;
        }

        private static bool IsItemAtMinValue(Item item)
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
