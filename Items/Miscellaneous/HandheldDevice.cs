using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Miscellaneous
{
    public class HandheldDevice : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 38;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.noMelee = true;
            item.rare = 8;
            item.autoReuse = true;
            item.value = Item.buyPrice(0, 50, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Handheld Device");
            Tooltip.SetDefault("");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Портативный девайс");
            Tooltip.AddTranslation(GameCulture.Russian, "");
        }

        public override bool UseItem(Player player)
        {
            var aPlayer = player.GetModPlayer<AntiarisPlayer>(mod);
            aPlayer.handheldDevice = !aPlayer.handheldDevice;
            return true;
        }
    }
}
