using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Antiaris.Tiles.Miscellaneous
{
    public class DeadManChest : ModTile
    {
        private bool opened = false;
        private int spawnX = 0;
        private int spawnY = 0;

        public override void SetDefaults()
        {
            Main.tileSpelunker[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 1200;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table | AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Dead Man's Chest");
            name.AddTranslation(GameCulture.Russian, "Сундук мертвеца");
            name.AddTranslation(GameCulture.Chinese, "");
            AddMapEntry(new Color(57, 146, 219), name);
            animationFrameHeight = 38;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY != 0)
            {
                top--;
            }
            player.showItemIcon2 = -1;
            if (!opened)
            {
                player.showItemIcon2 = mod.ItemType("DavyKey");
                player.showItemIconText = "";
            }
            player.noThrow = 2;
            player.showItemIcon = true;
        }

        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.player[Main.myPlayer];
            if (player.showItemIconText == "")
            {
                player.showItemIcon = false;
                player.showItemIcon2 = 0;
            }
        }

        public override void RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.showItemIcon2 == mod.ItemType("DavyKey"))
            {
                for (int a = 0; a < 58; a++)
                {
                    if (player.inventory[a].type == mod.ItemType("DavyKey") && player.inventory[a].stack > 0)
                    {
                        player.inventory[a].stack = 0;
                        opened = true;
                        Main.PlaySound(22, i * 16, j * 16);
                        player.QuickSpawnItem(mod.ItemType("BrokenDavyKey"), 1);
                    }
                }
            }
            spawnX = i * 16;
            spawnY = j * 16;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (opened)
            {
                frameCounter++;
            }
            if (frameCounter == 100)
            {
                frame++;
            }
            if (frameCounter == 105)
            {
                frame++;
                var player = Main.player[Main.myPlayer];
                Projectile.NewProjectile(spawnX, spawnY - 80, 0, 0, mod.ProjectileType("TimeWave"), 0, 0f, player.whoAmI, 0.0f, 0.0f);
                NPC.NewNPC(spawnX, spawnY - 80, mod.NPCType("DeadlyJones"));
            }
            if (opened && !NPC.AnyNPCs(mod.NPCType("DeadlyJones")) && frameCounter > 105)
            {
                frame = 0;
            }
        }
    }
}