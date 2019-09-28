using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Miscellaneous
{
    public class LightingChip : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lighting Chip");
            Tooltip.SetDefault("Can be inserted into Nixie Tube to make its symbol more luminous");
            DisplayName.AddTranslation(GameCulture.Chinese, "照明芯片");
            Tooltip.AddTranslation(GameCulture.Chinese, "可以插入数码管，使其符号更明亮");
            DisplayName.AddTranslation(GameCulture.Russian, "Чип свечения");
            Tooltip.AddTranslation(GameCulture.Russian, "Можно вставить в Газоразрядный индикатор, чтобы сделать его символ более светящимся");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.rare = 1;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 0, 80, 0);
        }
    }
}
