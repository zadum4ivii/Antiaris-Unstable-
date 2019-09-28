using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Buffs.Accessories
{
    public class BloodRepletion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("[c/DC143C:Blood Repletion]");
            Description.SetDefault("The heart fills itself with crimson blood to begin working again");
            DisplayName.AddTranslation(GameCulture.Russian, "[c/DC143C:Насыщение крови]");
            Description.AddTranslation(GameCulture.Russian, "Сердце наполняется багряной кровью, чтобы начать работать снова");
			DisplayName.AddTranslation(GameCulture.Chinese, "[c/DC143C:血液填充]");
			Description.AddTranslation(GameCulture.Chinese, "守护者之心需要重新蓄能以填充血腥之血");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
			canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AntiarisPlayer>(mod).guardianHeart = true;
        }
    }
}