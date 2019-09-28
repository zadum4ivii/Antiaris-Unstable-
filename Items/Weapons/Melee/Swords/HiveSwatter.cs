using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.Items.Weapons.Melee.Swords
{
    public class HiveSwatter : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 32;
            item.melee = true;
            item.width = 48;
            item.height = 48;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 8;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("HoneySpray");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hive Swatter");
            DisplayName.AddTranslation(GameCulture.Russian, "Мухобойка улья");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.SetDefault("Shoots a spray of honey");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Russian, "Выстреливает потоками мёда");
        }

        public void OverhaulInit()
        {
            this.SetTag(ItemTags.Broadsword);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HoneyExtract", 15);
            recipe.AddIngredient(ItemID.HoneyBlock, 20);
            recipe.AddIngredient(ItemID.CrispyHoneyBlock, 16);
            recipe.SetResult(this);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}
