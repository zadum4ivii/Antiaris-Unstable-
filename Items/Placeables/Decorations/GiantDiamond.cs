using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Placeables.Decorations
{
    public class GiantDiamond : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 40;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 2;
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.createTile = mod.TileType("GiantDiamond");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Diamond");
            Tooltip.SetDefault("'Not to be confused with Unbreakable diamond'");
            DisplayName.AddTranslation(GameCulture.Chinese, "巨大钻石");
            Tooltip.AddTranslation(GameCulture.Chinese, "“不要与坚不可摧的钻石混淆”");
            DisplayName.AddTranslation(GameCulture.Russian, "Гигантский алмаз");
            Tooltip.AddTranslation(GameCulture.Russian, "'Не путать с Несокрушимым алмазом'");
        }
    }
}