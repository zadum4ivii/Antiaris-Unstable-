using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Consumables.Bags
{
    public class FishBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.value = Item.sellPrice(0, 0, 15, 0);
            item.width = 28;
            item.height = 32;
            item.rare = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Bag");
            Tooltip.SetDefault("<right> to open");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Сумка с рыбой");
            Tooltip.AddTranslation(GameCulture.Russian, "<right>, чтобы открыть");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.CopperCoin, Main.rand.Next(15, 90));
            switch (Main.rand.Next(7))
            {
                case 0:
                    player.QuickSpawnItem(ItemID.Bass, Main.rand.Next(1, 4));
                    break;
                case 1:
                    player.QuickSpawnItem(ItemID.RedSnapper, Main.rand.Next(1, 4));
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.Salmon, Main.rand.Next(1, 4));
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.Shrimp, Main.rand.Next(1, 4));
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.Trout, Main.rand.Next(1, 4));
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.AtlanticCod, Main.rand.Next(1, 4));
                    break;
                default:
                    player.QuickSpawnItem(ItemID.Tuna, Main.rand.Next(1, 4));
                    break;
            }
            if (Main.hardMode)
            {
                switch (Main.rand.Next(12))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.AtlanticCod, Main.rand.Next(1, 3));
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.ChaosFish, Main.rand.Next(1, 3));
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.CrimsonTigerfish, Main.rand.Next(1, 3));
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.Damselfish, Main.rand.Next(1, 3));
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.DoubleCod, Main.rand.Next(1, 3));
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.Ebonkoi, Main.rand.Next(1, 3));
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.FlarefinKoi, Main.rand.Next(1, 3));
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.FrostMinnow, Main.rand.Next(1, 3));
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.Hemopiranha, Main.rand.Next(1, 3));
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.NeonTetra, Main.rand.Next(1, 3));
                        break;
                    case 10:
                        player.QuickSpawnItem(ItemID.PrincessFish, Main.rand.Next(1, 3));
                        break;
                    case 11:
                        player.QuickSpawnItem(ItemID.VariegatedLardfish, Main.rand.Next(1, 3));
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.Tuna, Main.rand.Next(1, 3));
                        break;
                }
            }
            else
            {
                switch (Main.rand.Next(10))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.AtlanticCod, Main.rand.Next(1, 3));
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.CrimsonTigerfish, Main.rand.Next(1, 3));
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.Damselfish, Main.rand.Next(1, 3));
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.DoubleCod, Main.rand.Next(1, 3));
                        break;
                    case 4:
                        player.QuickSpawnItem(ItemID.Ebonkoi, Main.rand.Next(1, 3));
                        break;
                    case 5:
                        player.QuickSpawnItem(ItemID.FlarefinKoi, Main.rand.Next(1, 3));
                        break;
                    case 6:
                        player.QuickSpawnItem(ItemID.FrostMinnow, Main.rand.Next(1, 3));
                        break;
                    case 7:
                        player.QuickSpawnItem(ItemID.Hemopiranha, Main.rand.Next(1, 3));
                        break;
                    case 8:
                        player.QuickSpawnItem(ItemID.NeonTetra, Main.rand.Next(1, 3));
                        break;
                    case 9:
                        player.QuickSpawnItem(ItemID.VariegatedLardfish, Main.rand.Next(1, 3));
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.Tuna, Main.rand.Next(1, 3));
                        break;
                }
            }
        }
    }
}
