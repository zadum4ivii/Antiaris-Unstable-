using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.Items.Tools.Pickaxes
{
    public class HoneyedPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 12;
            item.melee = true;
            item.width = 34;
            item.height = 36;
            item.useTime = 20;
            item.useAnimation = 23;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 0, 19, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.pick = 120;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honeyed Pickaxe");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Медовая кирка");
        }

        public void OverhaulInit()
        {
            this.SetTag(ItemTags.Tool);
            this.SetTag(ItemTags.BluntHit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Antiaris:CopperPickaxe");
            recipe.AddIngredient(null, "HoneyExtract", 8);
            recipe.AddIngredient(ItemID.HoneyBlock, 25);
            recipe.AddIngredient(ItemID.BottledHoney, 5);
            recipe.SetResult(this);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}