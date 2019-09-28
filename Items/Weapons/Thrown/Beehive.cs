using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Thrown
{
    public class Beehive : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 30;
            item.thrown = true;
            item.width = 32;
            item.height = 32;
            item.noUseGraphic = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("Beehive");
            item.shootSpeed = 6f;
            item.useStyle = 1;
            item.knockBack = 3.5f;
            item.value = Item.sellPrice(0, 0, 0, 60);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beehive");
            Tooltip.SetDefault("Explodes into a swarm of wasps");
            DisplayName.AddTranslation(GameCulture.Russian, "Пчелиный улей");
            Tooltip.AddTranslation(GameCulture.Russian, "Взрывается в рой ос");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Beenade, 25);
            recipe.AddIngredient(null, "HoneyExtract");
            recipe.SetResult(this, 25);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}
