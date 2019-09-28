using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Consumables.Ammo.Endless
{
    public class EndlessBuckshotBox : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 4;
            item.ranged = true;
            item.maxStack = 1;
            item.consumable = false;
            item.height = 14;
            item.width = 14;
            item.shootSpeed = 5f;
            item.knockBack = 1.4f;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 2;
            item.ammo = mod.ItemType("Buckshot");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Buckshot Box");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Бесконечная коробка картечи");
        }

        public override void PickAmmo(Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            type = mod.ProjectileType("Buckshot");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Buckshot", 3996);
            recipe.SetResult(this);
            recipe.AddTile(125);
            recipe.AddRecipe();
        }
    }
}
