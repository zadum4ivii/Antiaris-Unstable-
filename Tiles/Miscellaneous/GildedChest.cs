using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;

namespace Antiaris.Tiles.Miscellaneous
{
    public class GildedChest : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSpelunker[Type] = true;
            Main.tileContainer[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 1200;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileValue[Type] = 500;
            TileID.Sets.HasOutlines[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.HookCheck = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
            TileObjectData.newTile.AnchorInvalidTiles = new int[] { 127 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Gilded Chest");
            name.AddTranslation(GameCulture.Chinese, "镀金箱");
            name.AddTranslation(GameCulture.Russian, "Позолоченный сундук");
            AddMapEntry(new Color(208, 139, 90), name, MapChestName);
            dustType = 7;
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.Containers };
            chest = "Gilded Chest";
            chestDrop = mod.ItemType("GildedChest");
            TileID.Sets.BasicChest[Type] = true;
        }

        public override bool HasSmartInteract()
        {
            return true;
        }

        public string MapChestName(string name, int i, int j)
        {
            int left = i;
            int top = j;
            Tile tile = Main.tile[i, j];
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY != 0)
            {
                top--;
            }
            int chest = Chest.FindChest(left, top);
            if (Main.chest[chest].name == "")
            {
                return name;
            }
            else
            {
                return name + ": " + Main.chest[chest].name;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 1;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, chestDrop, 1, false, 0, false, false);
            Chest.DestroyChest(i, j);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
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
            return Chest.CanDestroyChest(left, top);
        }

        public override void RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.showItemIcon2 == mod.ItemType("GildedKey"))
            {
                for (int a = 0; a < 58; a++)
                {
                    if (player.inventory[a].type == mod.ItemType("GildedKey") && player.inventory[a].stack > 0)
                    {
                        Tile tile2 = Main.tile[i, j];
                        int left = i;
                        int top = j;
                        if (tile2.frameX % 36 != 0)
                        {
                            left--;
                        }
                        if (tile2.frameY != 0)
                        {
                            top--;
                        }
                        Main.tile[left, top].frameX = 0;
                        Main.tile[left, top + 1].frameX = 0;
                        Main.tile[left + 1, top].frameX = 18;
                        Main.tile[left + 1, top + 1].frameX = 18;
                        NetMessage.SendTileSquare(-1, left, top, 2, TileChangeType.None);
                        Main.PlaySound(22, left*16, top*16);
                    }
                }
            }
            Tile tile = Main.tile[i, j];
            if (tile.frameX != 72 && tile.frameX != 90)
            {
				//thanks to Dan Yami for this code
                Main.mouseRightRelease = false;
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
                if (player.sign >= 0)
                {
                    Main.PlaySound(SoundID.MenuClose);
                    player.sign = -1;
                    Main.editSign = false;
                    Main.npcChatText = "";
                }
                if (Main.editChest)
                {
                    Main.PlaySound(SoundID.MenuTick);
                    Main.editChest = false;
                    Main.npcChatText = "";
                }
                if (player.editedChestName)
                {
                    NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
                    player.editedChestName = false;
                }
                if (Main.netMode == 1)
                {
                    if (left == player.chestX && top == player.chestY && player.chest >= 0)
                    {
                        player.chest = -1;
                        Recipe.FindRecipes();
                        Main.PlaySound(SoundID.MenuClose);
                    }
                    else
                    {
                        NetMessage.SendData(31, -1, -1, null, left, (float)top, 0f, 0f, 0, 0, 0);
                        Main.stackSplit = 600;
                    }
                }
                else
                {
                    int chest = Chest.FindChest(left, top);
                    if (chest >= 0)
                    {
                        Main.stackSplit = 600;
                        if (chest == player.chest)
                        {
                            player.chest = -1;
                            Main.PlaySound(SoundID.MenuClose);
                        }
                        else
                        {
                            player.chest = chest;
                            Main.playerInventory = true;
                            Main.recBigList = false;
                            player.chestX = left;
                            player.chestY = top;
                            Main.PlaySound(SoundID.MenuOpen);
                        }
                        Recipe.FindRecipes();
                    }
                }
            }
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
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
            int chest = Chest.FindChest(left, top);
            player.showItemIcon2 = -1;
            if (chest < 0)
            {
                player.showItemIconText = Language.GetTextValue("ChestType.0");
            }
            else
            {
                if (tile.frameX == 72 || tile.frameX == 90)
                {
                    player.showItemIcon2 = mod.ItemType("GildedKey");
                    player.showItemIconText = "";
                }
                else
                {
                    player.showItemIcon2 = mod.ItemType("GildedChest");
                }
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
    }
}