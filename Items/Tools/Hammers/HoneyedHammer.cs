using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.Items.Tools.Hammers
{
    public class HoneyedHammer : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 28;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 16;
            item.useAnimation = 27;
            item.useStyle = 1;
            item.knockBack = 7;
            item.value = Item.sellPrice(0, 0, 19, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.hammer = 85;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honeyed Hammer");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Медовый молот");
        }

        public void OverhaulInit()
        {
            this.SetTag(ItemTags.Tool);
            this.SetTag(ItemTags.BluntHit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Antiaris:CopperHammer");
            recipe.AddIngredient(null, "HoneyExtract", 6);
            recipe.AddIngredient(ItemID.HoneyBlock, 20);
            recipe.AddIngredient(ItemID.BottledHoney, 4);
            recipe.SetResult(this);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}