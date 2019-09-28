using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Consumables.Crates
{
    public class GildedCrate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.rare = 2;
            item.maxStack = 99;
            item.createTile = mod.TileType("GildedCrate");
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilded Crate");
            Tooltip.SetDefault("<right> to open");
            DisplayName.AddTranslation(GameCulture.Chinese, "镀金板条箱");
            Tooltip.AddTranslation(GameCulture.Chinese, "<right>来打开");
            DisplayName.AddTranslation(GameCulture.Russian, "Позолоченный ящик");
            Tooltip.AddTranslation(GameCulture.Russian, "<right>, чтобы открыть");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(20) == 0)
                player.QuickSpawnItem(3064);
            if (Main.rand.Next(20) == 0)
                player.QuickSpawnItem(mod.ItemType("GiantEmerald"));
            if (Main.rand.Next(20) == 0)
                player.QuickSpawnItem(mod.ItemType("GiantDiamond"));
            if (Main.rand.Next(10) == 0)
                player.QuickSpawnItem(ItemID.HardySaddle);
            if (Main.rand.Next(9) == 0)
                switch (Main.rand.Next(0, 4))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.SilverBar, Main.rand.Next(15,30));
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.TungstenBar, Main.rand.Next(15, 30));
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.GoldBar, Main.rand.Next(15, 30));
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.PlatinumBar, Main.rand.Next(15, 30));
                        break;
                }
            if (Main.hardMode && Main.rand.Next(9) == 0)
                switch (Main.rand.Next(0, 4))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.MythrilBar, Main.rand.Next(15, 30));
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.OrichalcumBar, Main.rand.Next(15, 30));
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.AdamantiteBar, Main.rand.Next(15, 30));
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.TitaniumBar, Main.rand.Next(15, 30));
                        break;
                }
            if (Main.rand.Next(3) == 0)
                switch (Main.rand.Next(0, 5))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.ObsidianSkinPotion, Main.rand.Next(2, 5));
                        break;
                    case 1:
                        player.QuickSpawnItem(ItemID.SpelunkerPotion, Main.rand.Next(2, 5));
                        break;
                    case 2:
                        player.QuickSpawnItem(ItemID.GravitationPotion, Main.rand.Next(2, 5));
                        break;
                    case 3:
                        player.QuickSpawnItem(ItemID.MiningPotion, Main.rand.Next(2, 5));
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.HeartreachPotion, Main.rand.Next(2, 5));
                        break;
                }
            if (Main.rand.Next(2) == 0)
                switch (Main.rand.Next(0, 2))
                {
                    case 0:
                        player.QuickSpawnItem(ItemID.GreaterHealingPotion, Main.rand.Next(5, 20));
                        break;
                    default:
                        player.QuickSpawnItem(ItemID.GreaterManaPotion, Main.rand.Next(5, 20));
                        break;
                }
            if (Main.rand.Next(1,5) == 0)
                player.QuickSpawnItem(ItemID.MasterBait, Main.rand.Next(3, 7));
            if (Main.rand.Next(3) == 0)
                player.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(8, 20));
        }
    }
}
