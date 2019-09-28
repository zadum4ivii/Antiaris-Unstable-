using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Antiaris.Tiles.Decorations
{
    public class GildedCup : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 18 };
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table | AnchorType.SolidTile,TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Gilded Cup");
            name.AddTranslation(GameCulture.Chinese, "镀金杯");
            name.AddTranslation(GameCulture.Russian, "Позолоченная чаша");
            AddMapEntry(new Color(246, 214, 126), name);
        }

        public override bool Drop(int i, int j)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("GildedCup"), 1, false, 0, false, false);
            return false;
        }
    }
}
