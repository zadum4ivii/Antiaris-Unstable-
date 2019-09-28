using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Antiaris.Items.Equipables.Armor.Developers
{
    [AutoloadEquip(EquipType.Head)]
    public class Zadum4iviiProtectiveMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 12;
            item.rare = 9;
			item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zadum4ivii's Protective Mask");
            Tooltip.SetDefault("'Great for impersonating devs!'");
            DisplayName.AddTranslation(GameCulture.Russian, "Защитная маска Zadum4ivii");
            Tooltip.AddTranslation(GameCulture.Russian, "'Поможет вам выдать себя за разработчика!'");
			DisplayName.AddTranslation(GameCulture.Chinese, "Zadum4ivii的护身面具");
            Tooltip.AddTranslation(GameCulture.Chinese, "“非常适合冒充开发者！”");
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Zadum4iviiProtectiveBreastplate") && legs.type == mod.ItemType("Zadum4iviiProtectiveGreaves");
        }
		
		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            glowMask = AntiarisGlowMasks.Zadum4iviiProtectiveMask;
            glowMaskColor = Color.White;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.8f, 0f, 0.8f);
        }
    }
}
