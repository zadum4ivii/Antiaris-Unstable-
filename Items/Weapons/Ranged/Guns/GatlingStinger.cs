using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Ranged.Guns
{
    public class GatlingStinger : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 28;
            item.ranged = true;
            item.width = 60;
            item.height = 40;
            item.useTime = 3;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.reuseDelay = 35;
            item.noMelee = true;
            item.knockBack = 2;
            item.rare = 4;
            item.autoReuse = true;
            item.shoot = 374;
            item.shootSpeed = 12f;
            item.value = Item.sellPrice(0, 3, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gatling Stinger");
            Tooltip.SetDefault("Fires a burst of stingers\nDoes not require ammo");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Жало гатлинга");
            Tooltip.AddTranslation(GameCulture.Russian, "Выстреливает залпом жал");
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(null, "HoneyExtract", 14);
            recipe.AddIngredient(ItemID.Stinger, 8);
            recipe.AddIngredient(ItemID.HoneyBlock, 10);
            recipe.SetResult(this);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}
