using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace Antiaris.Items.Materials
{
    public class HoneyExtract : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 30;
            item.value = Item.sellPrice(0, 0, 2, 0);
            item.rare = 3;
            item.maxStack = 999;
            item.consumable = true;
            item.buffType = BuffID.Honey;
            item.buffTime = 900;
            item.useAnimation = 15;
            item.useTime = 15;
            item.UseSound = SoundID.Item2;
            item.useStyle = 2;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Extract");
			Tooltip.SetDefault("Can be consumed to gain increased life regeneration");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
			Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Медовый экстракт");
			Tooltip.AddTranslation(GameCulture.Russian, "Можно съесть, чтобы повысить восстановление здоровья");
        }
    }
}
