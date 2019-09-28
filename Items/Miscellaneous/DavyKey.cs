using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Miscellaneous
{
    public class DavyKey : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.rare = 2;
            item.maxStack = 1;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Davy's Key");
            Tooltip.SetDefault("");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Ключ Дэйви");
            Tooltip.AddTranslation(GameCulture.Russian, "");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BrokenDavyKey");
            recipe.AddRecipeGroup("Antiaris:DemoniteBar", 10);
            recipe.AddIngredient(ItemID.Sapphire, 4);
            recipe.AddIngredient(null, "TranquilityElement");
            recipe.SetResult(this);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();
        }
    }
}