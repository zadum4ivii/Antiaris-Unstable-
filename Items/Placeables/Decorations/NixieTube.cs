using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Placeables.Decorations
{
    public class NixieTube : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 14;
            item.useTime = 14;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 1;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.createTile = mod.TileType("NixieTube");
            item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nixie Tube");
			Tooltip.SetDefault("<right> placed tube to configure it\n'Shows the divergence value of world line'");
            DisplayName.AddTranslation(GameCulture.Chinese, "数码管");
			Tooltip.AddTranslation(GameCulture.Chinese, "放置后 <right> 可以进行修改\n“显示世界线的分歧值”");
            DisplayName.AddTranslation(GameCulture.Russian, "Газоразрядный индикатор");
			Tooltip.AddTranslation(GameCulture.Russian, "<right> по установленному индикатору, чтобы настроить его\n'Показывает значение отклонения мировой линии'");
        }
    }
}