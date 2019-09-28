using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Buffs.Accessories
{
    public class BadgersResolve : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Badger's Resolve");
            Description.SetDefault("Increased health regeneration and reduced damage taken");
            DisplayName.AddTranslation(GameCulture.Russian, "Барсучья решимость");
            Description.AddTranslation(GameCulture.Russian, "Повышенное восстановление здоровья и сниженный получаемый урон");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Description.AddTranslation(GameCulture.Chinese, "");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 10;
            player.endurance += 0.30f;
            if (player.buffTime[buffIndex] == 0)
            {
                player.AddBuff(mod.BuffType("HoneyRecharge"), 1200);
            }
        }
    }
}