using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Materials
{
    public class RoyalWeaponParts : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.rare = 1;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Weapon Parts");
			Tooltip.SetDefault("'A part of Davy's past'");
            DisplayName.AddTranslation(GameCulture.Chinese, "皇家武器零件");
			Tooltip.AddTranslation(GameCulture.Chinese, "“戴维过去的一部分”");
            DisplayName.AddTranslation(GameCulture.Russian, "Королевские части оружия");
			Tooltip.AddTranslation(GameCulture.Russian, "'Часть прошлого Дэйви'");
        }
    }
}
