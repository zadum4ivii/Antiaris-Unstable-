using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Antiaris.Tiles.Decorations
{
    public class GiantEmerald : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            dustType = 46;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Giant Emerald");
            name.AddTranslation(GameCulture.Russian, "Гигантский изумруд");
            name.AddTranslation(GameCulture.Chinese, "大翡翠");
            AddMapEntry(new Color(33, 184, 115), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (frameX == 0)
            {
                Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("GiantEmerald"), 1, false, 0, false, false);
            }
        }
    }
}