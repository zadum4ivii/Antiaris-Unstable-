using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Miscellaneous
{
    public class GildedKey : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 38;
            item.rare = 4;
            item.maxStack = 1;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilded Key");
            Tooltip.SetDefault("Opens Gilded Chests in Pirate's Coves");
            DisplayName.AddTranslation(GameCulture.Chinese, "镀金之匙");
            Tooltip.AddTranslation(GameCulture.Chinese, "可以打开海盗洞窟中的镀金箱");
            DisplayName.AddTranslation(GameCulture.Russian, "Позолоченный ключ");
            Tooltip.AddTranslation(GameCulture.Russian, "Открывает Позолоченные сундуки в Пиратских бухтах");
        }
    }
}