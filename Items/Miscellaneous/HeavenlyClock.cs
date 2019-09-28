using Terraria.ModLoader;
using Terraria.Localization;
using Terraria;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Antiaris.Items.Miscellaneous
{
    public class HeavenlyClock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavenly Clock");
            Tooltip.SetDefault("Tells the time\nAccelerates the time on use\n[c/FF0220:Acceleration may cause unalterable consequences]\n'Forged in the sky'");
            DisplayName.AddTranslation(GameCulture.Chinese, "天国之钟");
            Tooltip.AddTranslation(GameCulture.Chinese, "1、显示时间\n2、使用后加快时间流逝速度\n[C/FF0220:加速可能会导致无法挽回的后果]\n“天国的造物”");
            DisplayName.AddTranslation(GameCulture.Russian, "Райские часы");
            Tooltip.AddTranslation(GameCulture.Russian, "Показывают время\nУскоряют время при использовании\n[c/FF0220:Ускорение может вызвать необратимые последствия]\n'Сковано на небесах'");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.rare = 9;
            item.expert = true;
            item.maxStack = 1;
            item.useTime = 75;
            item.useAnimation = 75;
            item.useStyle = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return (AntiarisWorld.heavenClock != 1);
        }

        public override bool UseItem(Player player)
        {
            string TimeAccelerate = Language.GetTextValue("Mods.Antiaris.TimeAccelerate");
            player.GetModPlayer<AntiarisPlayer>(mod).heavenWarn++;
            if (player.GetModPlayer<AntiarisPlayer>(mod).heavenWarn == 4)
            {
                player.GetModPlayer<AntiarisPlayer>(mod).heavenWarn = 0;
                Main.NewText(TimeAccelerate, new Color(56, 78, 210));
                AntiarisWorld.heavenClock = 1;
            }
            return true;
        }
      
        public override void UpdateInventory(Player player)
        {
            player.accWatch = 3;
        }

        public override void ModifyTooltips(List<TooltipLine> Tooltips)
        {
            int time = (6000 - AntiarisWorld.heavenTimer) / 60;
            string SingularityTime = Language.GetTextValue("Mods.Antiaris.SingularityTime", time);
            if (AntiarisWorld.heavenClock == 1)
            {
                int pos = +2;
                for (int k = 0; k < Tooltips.Count; ++k)
                    if (Tooltips[k].Name.Equals("Expert"))
                    {
                        pos = k;
                        break;
                    }
                Tooltips.Insert(pos + 1, new TooltipLine(mod, "HeavenlyClock", SingularityTime));
            }

            foreach (TooltipLine TooltipLine in Tooltips)
                if (TooltipLine.mod == "Antiaris" && TooltipLine.Name == "HeavenlyClock")
                    TooltipLine.overrideColor = new Color(56, 78, 210);
        }
    }
}
