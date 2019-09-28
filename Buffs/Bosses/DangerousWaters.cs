using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Buffs.Bosses
{
    public class DangerousWaters : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Dangerous Waters");
            Description.SetDefault("Waters are going to consume you even faster");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Description.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Опсаные воды");
            Description.AddTranslation(GameCulture.Russian, "Воды поглотят вас ещё быстрее");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            canBeCleared = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AntiarisPlayer>(mod).dangerousWaters = true;
        }
    }
}
