using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Melee.Yoyos
{
    public class Dingo : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("Dingo");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.knockBack = 1f;
            item.damage = 32;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.useStyle = 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dingo");
            Tooltip.SetDefault("Sprays honey in different directions after each fifth enemy hit");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Динго");
            Tooltip.AddTranslation(GameCulture.Russian, "Распыляет мёд в разных направлениях при каждом пятом ударе");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleYoyo);
            recipe.AddIngredient(null, "HoneyExtract", 10);
            recipe.AddIngredient(ItemID.HoneyBlock, 30);
            recipe.AddIngredient(ItemID.BottledHoney, 8);
            recipe.SetResult(this);
            recipe.AddTile(134);
            recipe.AddRecipe();
        }
    }
}
