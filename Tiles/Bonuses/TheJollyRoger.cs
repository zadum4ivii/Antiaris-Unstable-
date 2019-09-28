using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaOverhaul;
using Terraria.ID;

namespace Antiaris.Tiles.Bonuses
{
    public class TheJollyRoger : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("The Jolly Roger");
            name.AddTranslation(GameCulture.Russian, "Весёлый Роджер");
            name.AddTranslation(GameCulture.Chinese, "海盗旗");
            AddMapEntry(new Color(208, 139, 90), name);
        }

        public void OverhaulInit()
        {
            this.SetTag(TileTags.Flammable);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (frameX == 0)
            {
                Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("TheJollyRoger"));
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            for (int k = 0; k < 200; k++)
            {
                float distanceTo = Vector2.Distance(Main.npc[k].Center, new Vector2(i * 16, j * 16));
                float distance = 800.0f;
                if ((double)distanceTo <= (double)distance && !Main.npc[k].friendly)
                {
                    Main.npc[k].AddBuff(BuffID.Midas, 60);
                }
            }
        }
    }
}


