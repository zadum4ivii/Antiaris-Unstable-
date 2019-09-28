using Terraria.ModLoader;
using Terraria.Localization;

namespace Antiaris.Items.Miscellaneous
{
    public class BrokenHeavenlyClock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Heavenly Clock");
            Tooltip.SetDefault("");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Сломанные райские часы");
            Tooltip.AddTranslation(GameCulture.Russian, "");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 28;
            item.rare = 9;
            item.maxStack = 1;
        }
    }
}
