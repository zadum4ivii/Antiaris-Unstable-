using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace Antiaris.Items.Miscellaneous
{
    public class OrangeLightbulb : Lightbulb2
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orange Lightbulb");
            Tooltip.SetDefault("Can be inserted into Nixie Tube to change its color");
            DisplayName.AddTranslation(GameCulture.Chinese, "橙色灯泡");
            Tooltip.AddTranslation(GameCulture.Chinese, "可以放进数码管改变其颜色");
            DisplayName.AddTranslation(GameCulture.Russian, "Оранжевая лампочка");
            Tooltip.AddTranslation(GameCulture.Russian, "Можно вставить в Газоразрядный индикатор, чтобы изменить его цвет");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 32;
            item.rare = 1;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Lightbulb");
            recipe.AddIngredient(ItemID.Amber);
            recipe.SetResult(this);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();
        }
    }
}
