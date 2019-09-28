using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Consumables.Ammo.Arrows
{
    public class GreenCrystalArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 14;
            item.ranged = true;
            item.maxStack = 999;
            item.consumable = true;
            item.width = 18;
            item.height = 46;			
            item.shoot = mod.ProjectileType("GreenCrystalArrow");
            item.shootSpeed = 6.0f; 
            item.knockBack = 5.0f;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 5;
            item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Crystal Arrow");
			Tooltip.SetDefault("Pierces through enemies three times before disappearing");
            DisplayName.AddTranslation(GameCulture.Russian, "Зеленая кристальная стрела");
            Tooltip.AddTranslation(GameCulture.Russian, "Проникает через врагов три раза перед тем как исчезнуть");
            DisplayName.AddTranslation(GameCulture.Chinese, "绿水晶箭");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
        }
    }
}
