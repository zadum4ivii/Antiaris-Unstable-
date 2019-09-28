using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Placeables.Banners
{
    public class WilloWispBanner : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 28;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 1;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.createTile = mod.TileType("Banners");
            item.placeStyle = 24;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Will-o'-Wisp Banner");
            Tooltip.SetDefault("Nearby players get a bonus against: Will-o'-Wisp");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Знамя блуждающего огонька");
            Tooltip.AddTranslation(GameCulture.Russian, "Игроки поблизости получают бонус против: Блуждающий огонёк");
        }
    }
}
