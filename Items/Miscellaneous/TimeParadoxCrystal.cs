using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace Antiaris.Items.Miscellaneous
{
    public class TimeParadoxCrystal : ModItem
    {
		private int rippleCount = 3;
		private int rippleSize = 5;
		private int rippleSpeed = 15;
		private int buffID;
		private float distortStrength = 100f;
		
        public override void SetDefaults()
        {
			item.mana = 15;
            item.width = 28;
            item.height = 36;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
			item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.rare = 9;
            item.expert = true;
            item.autoReuse = true;
			item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Paradox Crystal");
            Tooltip.SetDefault("Freezes time for 10 seconds\nStopping time includes stopping any projectile and enemy\n'Stop, time!'");
            DisplayName.AddTranslation(GameCulture.Chinese, "时间驳论水晶");
            Tooltip.AddTranslation(GameCulture.Chinese, "冻结时间 10 秒\n“时停！”");
            DisplayName.AddTranslation(GameCulture.Russian, "Кристалл временного парадокса");
            Tooltip.AddTranslation(GameCulture.Russian, "Замораживает время на 10 секунд\nОстановка времени включает в себя остановку любого снаряда или врага\n'Время, остановись!'");
        }

        public override bool CanUseItem(Player player)
		{
			return !mod.GetModWorld<AntiarisWorld>().frozenTime && !player.buffType.Contains(mod.BuffType("CrystalRecharge"));
		}

        public override bool UseItem(Player player)
        {
			string TimeStop1 = Language.GetTextValue("Mods.Antiaris.TimeStop1", player.name);
			Main.NewText(TimeStop1, 255, 255, 255);
			var aPlayer = player.GetModPlayer<AntiarisPlayer>(mod);	
			if (aPlayer.bizzare)
			{
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Items/TimeParadoxCrystal3"), Main.player[Main.myPlayer].position);
			}
			else
			{
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Items/TimeParadoxCrystal"), Main.player[Main.myPlayer].position);
			}
			Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("TimeWave"), 0, 0f, player.whoAmI, 0.0f, 0.0f);
            player.AddBuff(mod.BuffType("FrozenTime"), 600);
            return true;
        }
    }
}
