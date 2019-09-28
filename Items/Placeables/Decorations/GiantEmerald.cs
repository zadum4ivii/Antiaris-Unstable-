using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Placeables.Decorations
{
    public class GiantEmerald : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 48;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 2;
            item.value = Item.sellPrice(0, 35, 0, 0);
            item.createTile = mod.TileType("GiantEmerald");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Emerald");
            DisplayName.AddTranslation(GameCulture.Chinese, "巨大翡翠");
            DisplayName.AddTranslation(GameCulture.Russian, "Гигантский изумруд");
        }
    }
}