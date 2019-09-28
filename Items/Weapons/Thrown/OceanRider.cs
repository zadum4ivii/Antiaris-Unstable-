using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Thrown
{
    public class OceanRider : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 35;
            item.thrown = true;
            item.width = 52;
            item.height = 52;
            item.noUseGraphic = true;
            item.useTime = 25;
            item.useAnimation = 25;
            item.shoot = mod.ProjectileType("OceanRider");
            item.shootSpeed = 9f;
            item.useStyle = 1;
            item.knockBack = 3.5f;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Rider");
            Tooltip.SetDefault("Throws a spear that leaves homing bubbles behind");
            DisplayName.AddTranslation(GameCulture.Russian, "Океанский ездок");
            Tooltip.AddTranslation(GameCulture.Russian, "Бросает копьё, которое оставляет за собой самонаводящиеся пузыри");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
        }
    }
}
