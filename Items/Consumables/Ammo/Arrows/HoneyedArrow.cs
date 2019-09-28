using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Consumables.Ammo.Arrows
{
    public class HoneyedArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 14;
            item.ranged = true;
            item.maxStack = 999;
            item.consumable = true;
            item.width = 14;
            item.height = 32;
            item.shoot = mod.ProjectileType("HoneyedArrow");
            item.shootSpeed = 3f;
            item.knockBack = 3.0f;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 4;
            item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honeyed Arrow");
            Tooltip.SetDefault("Has a chance to summon honey candy on hit\nIt restores health and increases regeneration upon pickup");
            DisplayName.AddTranslation(GameCulture.Russian, "Медовая стрела");
            Tooltip.AddTranslation(GameCulture.Russian, "Имеет шанс призвать медовую конфетку при попадании\nКонфетка восстанавливает здоровье и увеличивает его восстановление при поднятии");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 150);
            recipe.AddIngredient(null, "HoneyExtract");
            recipe.SetResult(this, 150);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}
