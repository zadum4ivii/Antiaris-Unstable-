using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Melee.Yoyos
{
    public class Arthur : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 13;
            item.width = 30;
            item.height = 26;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("Arthur");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.knockBack = 2.5f;
            item.damage = 12;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.useStyle = 5;
            item.crit = 16;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arthur");
            Tooltip.SetDefault("Critical strikes summon piercing enchanted swords flying a random direction");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Артур");
            Tooltip.AddTranslation(GameCulture.Russian, "Критические удары призывают проникающие зачарованные мечи, летящие в случайном направлении");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EnchantedShard", 10);
            recipe.AddIngredient(ItemID.WoodYoyo);
            recipe.SetResult(this);
            recipe.AddTile(16);
            recipe.AddRecipe();
        }
    }
}
