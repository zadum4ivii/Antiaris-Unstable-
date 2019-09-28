using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using TerrariaOverhaul;

namespace Antiaris.Tiles.Miscellaneous
{
    public class GildedCrate : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table | AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Gilded Crate");
            name.AddTranslation(GameCulture.Chinese, "镀金板条箱");
            name.AddTranslation(GameCulture.Russian, "Позолоченный ящик");
            AddMapEntry(new Color(246, 214, 126), name);
        }

        public void OverhaulInit()
        {
            this.SetTag(TileTags.Flammable);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("GildedCrate"), 1, false, 0, false, false);
        }
    }
}
