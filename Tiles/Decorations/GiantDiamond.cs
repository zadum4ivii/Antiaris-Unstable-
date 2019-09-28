using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Antiaris.Tiles.Decorations
{
    public class GiantDiamond : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            dustType = 15;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Giant Diamond");
            name.AddTranslation(GameCulture.Russian, "Гигантский алмаз");
            name.AddTranslation(GameCulture.Chinese, "大钻石");
            AddMapEntry(new Color(141, 165, 214), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (frameX == 0)
            {
                Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("GiantDiamond"), 1, false, 0, false, false);
            }
        }
    }
}
