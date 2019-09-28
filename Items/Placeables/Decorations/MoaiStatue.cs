using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Placeables.Decorations
{
    public class MoaiStatue : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 64;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 1;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("MoaiStatue");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moai Statue");
            DisplayName.AddTranslation(GameCulture.Chinese, "摩艾石像");
            DisplayName.AddTranslation(GameCulture.Russian, "Статуя Моаи");
        }
    }
}
