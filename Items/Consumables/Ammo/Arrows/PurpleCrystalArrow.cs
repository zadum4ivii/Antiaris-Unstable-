using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Consumables.Ammo.Arrows
{
    public class PurpleCrystalArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 13;
            item.ranged = true;
            item.maxStack = 999;
            item.consumable = true;
            item.width = 18;
            item.height = 46;		
            item.shoot = mod.ProjectileType("PurpleCrystalArrow");
            item.shootSpeed = 7.0f; 
            item.knockBack = 6.0f;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 5;
            item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purple Crystal Arrow");
            Tooltip.SetDefault("Splits into two arrows when hitting blocks or after piercing a second enemy");
            Tooltip.AddTranslation(GameCulture.Russian, "Разделяется на две стрелы при попадании в блок или второго врага");
            DisplayName.AddTranslation(GameCulture.Russian, "Фиолетовая кристальная стрела");
            DisplayName.AddTranslation(GameCulture.Chinese, "紫水晶箭");
        }
    }
}
