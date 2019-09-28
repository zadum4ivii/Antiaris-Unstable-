using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Placeables.Bonuses
{
    public class TheJollyRoger : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.createTile = mod.TileType("TheJollyRoger");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Jolly Roger");
            Tooltip.SetDefault("Nearby enemies drop more coins");
            DisplayName.AddTranslation(GameCulture.Chinese, "海盗旗");
            Tooltip.AddTranslation(GameCulture.Chinese, "在其附近的敌人会掉落更多的钱币");
            DisplayName.AddTranslation(GameCulture.Russian, "Весёлый Роджер");
            Tooltip.AddTranslation(GameCulture.Russian, "С ближайших врагов выпадает больше монет");
        }
    }
}