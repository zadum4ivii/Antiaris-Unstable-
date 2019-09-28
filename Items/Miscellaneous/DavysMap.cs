using Terraria.ModLoader;
using Terraria.Localization;

namespace Antiaris.Items.Miscellaneous
{
    public class DavysMap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Davy's Map");
            Tooltip.SetDefault("Marks the location of the Pirate's Cove on the map when in the inventory");
            DisplayName.AddTranslation(GameCulture.Chinese, "戴维的地图");
            Tooltip.AddTranslation(GameCulture.Chinese, "在物品栏时打开地图后标出海盗湾的所在地");
            DisplayName.AddTranslation(GameCulture.Russian, "Карта Дэйви");
            Tooltip.AddTranslation(GameCulture.Russian, "Отмечает местоположение Пиратской бухты на карте, если находится в инвентаре");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 28;
            item.rare = 9;
            item.maxStack = 1;
            item.expert = true;
        }
    }
}
