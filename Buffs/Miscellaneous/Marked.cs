using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Buffs.Miscellaneous
{
    public class Marked : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Marked");
            DisplayName.AddTranslation(GameCulture.Chinese, "标记");
            DisplayName.AddTranslation(GameCulture.Russian, "Отмеченный");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            canBeCleared = false;
            Main.persistentBuff[Type] = true;
        }
    }
}
