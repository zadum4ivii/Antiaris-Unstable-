using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Equipables.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    public class ScientistCoat : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.rare = 1;
            item.vanity = true;
            item.value = Item.buyPrice(0, 30, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scientist Coat");
            Tooltip.SetDefault("'100% increased mad scientist skills'");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Халат учёного");
            Tooltip.AddTranslation(GameCulture.Russian, "'На 100% увеличивает навыки безумного учёного'");
        }
    }
}
