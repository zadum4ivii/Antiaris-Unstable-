using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Placeables.MusicBoxes
{
    public class KingSlimeMusicBox : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("KingSlimeMusicBox");
            item.width = 32;
            item.height = 26;
            item.rare = 4;
            item.value = 100000;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (King Slime)");
            DisplayName.AddTranslation(GameCulture.Chinese, "音乐盒（史莱姆王）");
            DisplayName.AddTranslation(GameCulture.Russian, "Музыкальная шкатулка (Король слизней)");
        }
    }
}
