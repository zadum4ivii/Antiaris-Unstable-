using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.Items.Tools.Axes
{
    public class HoneyedAxe : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 33;
            item.melee = true;
            item.width = 44;
            item.height = 42;
            item.useTime = 15;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 0, 19, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.axe = 20;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honeyed Axe");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Медовый топор");
        }

        public void OverhaulInit()
        {
            this.SetTag(ItemTags.Tool);
            this.SetTag(ItemTags.BluntHit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Antiaris:CopperAxe");
            recipe.AddIngredient(null, "HoneyExtract", 6);
            recipe.AddIngredient(ItemID.HoneyBlock, 20);
            recipe.AddIngredient(ItemID.BottledHoney, 4);
            recipe.SetResult(this);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}