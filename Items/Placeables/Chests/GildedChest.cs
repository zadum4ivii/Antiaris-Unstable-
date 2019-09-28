using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;

namespace Antiaris.Items.Placeables.Chests
{
    public class GildedChest : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 22;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.createTile = mod.TileType("DeadManChest");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilded Chest");
            DisplayName.AddTranslation(GameCulture.Chinese, "镀金箱");
            DisplayName.AddTranslation(GameCulture.Russian, "Позолоченный сундук");
        }
    }
}
