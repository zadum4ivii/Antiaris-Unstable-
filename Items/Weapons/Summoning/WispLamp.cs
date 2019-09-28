using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Summoning
{
    public class WispLamp : ModItem
    {
        public override void HoldItem(Player player) { AntiarisGlowMask2.AddGlowMask(mod.ItemType(GetType().Name), "Antiaris/Glow/" + GetType().Name + "_GlowMask"); }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) { AntiarisUtils.DrawItemGlowMaskWorld(spriteBatch, item, mod.GetTexture("Glow/" + GetType().Name + "_GlowMask"), rotation, scale); }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.summon = true;
            item.mana = 12;
            item.width = 52;
            item.height = 32;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 4;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 9, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("WispFlameCentral");
            item.shootSpeed = 22f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wisp Lamp");
            Tooltip.SetDefault("Summons homing flames around the cursor");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Лампа духа");
            Tooltip.AddTranslation(GameCulture.Russian, "Призывает самонаводящиеся огни вокруг курсора");
        }
    }
}
