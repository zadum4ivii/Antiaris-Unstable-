using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaOverhaul;

namespace Antiaris.Tiles.Miscellaneous
{
    public class GildedStrongbox : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileCut[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table | AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Gilded Strongbox");
            name.AddTranslation(GameCulture.Chinese, "镀金保险箱");
            name.AddTranslation(GameCulture.Russian, "Позолоченный сейф");
            AddMapEntry(new Color(246, 214, 126), name);
        }

        public void OverhaulInit()
        {
            this.SetTag(TileTags.Flammable);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ItemID.GoldCoin, Main.rand.Next(8,14), false, 0, false, false);
            if (Main.rand.Next(20) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("GiantEmerald"), 1, false, 0, false, false);
            if (Main.rand.Next(20) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("GiantDiamond"), 1, false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Emerald, Main.rand.Next(5, 9), false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Sapphire, Main.rand.Next(5, 9), false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Ruby, Main.rand.Next(5, 9), false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Topaz, Main.rand.Next(5, 9), false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Amethyst, Main.rand.Next(5, 9), false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Diamond, Main.rand.Next(5, 9), false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Amber, Main.rand.Next(5, 9), false, 0, false, false);
            if (Main.rand.Next(3) == 0 && !Main.hardMode)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.LesserHealingPotion, 1, false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.HealingPotion, 1, false, 0, false, false);
            if (Main.rand.Next(2) == 0 && !Main.hardMode)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.WoodenArrow, Main.rand.Next(10, 20), false, 0, false, false);
            if (Main.rand.Next(2) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.UnholyArrow, Main.rand.Next(10, 20), false, 0, false, false);
            if (Main.rand.Next(4) == 0 && !Main.hardMode)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Shuriken, Main.rand.Next(10, 20), false, 0, false, false);
            if (Main.rand.Next(4) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Grenade, Main.rand.Next(10, 20), false, 0, false, false);
            if (Main.rand.Next(2) == 0)
                Item.NewItem(i * 16, j * 16, 32, 16, ItemID.Rope, Main.rand.Next(20, 40), false, 0, false, false);
        }
    }
}
