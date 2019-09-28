using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Miscellaneous
{
    public class Spyglass : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spyglass");
            Tooltip.SetDefault("Right click to zoom out\nHovering over an enemy with cursor marks them for 5 seconds\nMarked enemies get 20% increased ranged damage");
            DisplayName.AddTranslation(GameCulture.Chinese, "单筒望远镜");
            Tooltip.AddTranslation(GameCulture.Chinese, "1、右键允许查看远处\n2、持有此物时用光标接触敌人可以将它们瞄准 5 秒钟\n被瞄准的敌人额外承受 20% 的远程伤害");
            DisplayName.AddTranslation(GameCulture.Russian, "Подзорная труба");
            Tooltip.AddTranslation(GameCulture.Russian, "Нажмите правую кнопку мыши, чтобы прицелиться\nНаведение курсора на врага отмечает его на 5 секунд\nОтмеченные враги получают на 20% больше урона дальнего боя");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 46;
            item.rare = 4;
            item.maxStack = 1;
        }
    }
}