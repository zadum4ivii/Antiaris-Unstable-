using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Thrown
{
    public class Pufferfish : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.damage = 12;
            item.thrown = true;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 0, 2, 0);
            item.rare = 1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Pufferfish");
            item.shootSpeed = 9f;
            item.consumable = true;
            item.maxStack = 999;
            item.rare = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pufferfish");
            Tooltip.SetDefault("Sticks to enemies dealing damage\nExplodes into bubbles");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Иглобрюх");
            Tooltip.AddTranslation(GameCulture.Russian, "Втыкается во врагов, нанося урон\nВзрывается в пузыри");
        }

        public override void PostUpdate()
        {
            if (item.wet)
            {
                if (item.velocity.Y > 0.86f)
                {
                    item.velocity.Y = item.velocity.Y * 0.9f;
                }
                item.velocity.Y = item.velocity.Y - 0.6f;
                if (item.velocity.Y < -2f)
                {
                    item.velocity.Y = -2f;
                }
            }
        }
    }
}
