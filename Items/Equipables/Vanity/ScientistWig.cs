using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Equipables.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class ScientistWig : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 1;
            item.vanity = true;
            item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scientist Wig");
            Tooltip.SetDefault("");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Парик учёного");
            Tooltip.AddTranslation(GameCulture.Russian, "");
        }
    }
}
