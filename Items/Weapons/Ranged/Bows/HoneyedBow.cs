using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.Items.Weapons.Ranged.Bows
{
    public class HoneyedBow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 40;
            item.ranged = true;
            item.width = 20;
            item.height = 42;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.rare = 4;
            item.UseSound = SoundID.Item5;
            item.autoReuse = false;
            item.shoot = 1;
            item.shootSpeed = 6f;
            item.value = Item.sellPrice(0, 0, 15, 0);
            item.useAmmo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honeyed Bow");
            Tooltip.SetDefault("Transforms arrows into honeyed arrows");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Медовый лук");
            Tooltip.AddTranslation(GameCulture.Russian, "Превращает стрелы в медовые");
        }

        public void OverhaulInit()
        {
            this.SetTag(ItemTags.Bow);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("HoneyedArrow");
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Antiaris:CopperBow");
            recipe.AddIngredient(null, "HoneyExtract", 16);
            recipe.AddIngredient(ItemID.HoneyBlock, 8);
            recipe.AddIngredient(ItemID.BottledHoney, 5);
            recipe.SetResult(this);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}
