using Terraria.Localization;

namespace Antiaris.Items.Quests
{
    public class StrangeDiary : QuestItem
    {
        public StrangeDiary()
        {
            questItem = true;
            uniqueStack = true;
            maxStack = 1;
            rare = -11;
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            base.SetDefaults();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Diary");
            Tooltip.SetDefault("\n \nThere're some strange words written down in this diary:\n'Spiral staircase, rhinoceros beetle,\ndesolation row, fig tart,\nrhinoceros beetle, via dolorosa,\nrhinoceros beetle, singularity point,\ngiotto, angel,\nhydrangea, rhinoceros beetle,\nsingularity point, secret emperor.'\n\nThe Guide might be able to help understand the meaning of these words.");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Странный дневник");
            Tooltip.AddTranslation(GameCulture.Russian, "\n \nВ дневнике записаны странные слова:\n'Спиральная лестница, жук-носорог,\nулица опустошения, инжирный пирог,\nжук-носорог, виа долороза,\nжук-носорог, точка сингулярности,\nджотто, ангел,\nгортензия, жук-носорог\nточка сингулярности, тайный император.'\n\nГид, возможно, поможет понять значение этих слов.");
        }
    }
}
