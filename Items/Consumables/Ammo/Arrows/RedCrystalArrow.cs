using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Consumables.Ammo.Arrows
{
    public class RedCrystalArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.maxStack = 999;
            item.consumable = true;
            item.width = 18;
            item.height = 46;			
            item.shoot = mod.ProjectileType("RedCrystalArrow");
            item.shootSpeed = 7.0f; 
            item.knockBack = 8.0f;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 5;
            item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Crystal Arrow");
            Tooltip.SetDefault("Increased damage at the cost of piercing");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличенный урон ценой проникания");
            DisplayName.AddTranslation(GameCulture.Russian, "Красная кристальная стрела");
            DisplayName.AddTranslation(GameCulture.Chinese, "红水晶箭");
			Tooltip.AddTranslation(GameCulture.Chinese, "");
        }
    }
}
