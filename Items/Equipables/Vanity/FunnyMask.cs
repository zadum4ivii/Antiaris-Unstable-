using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Equipables.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class FunnyMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.rare = 1;
            item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Funny Mask");
            DisplayName.AddTranslation(GameCulture.Chinese, "滑稽果");
            DisplayName.AddTranslation(GameCulture.Russian, "Забавная маска");
        }
    }
}
