using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Antiaris.UIs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader.IO;
using Antiaris.Items.Miscellaneous;

namespace Antiaris.Tiles.Decorations
{
    public class NixieTube : ModTile
    {
		public int index;
        public int indexY;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<NixieTubeEntity>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            dustType = 7;
			ModTranslation name = CreateMapEntryName();
            name.SetDefault("Nixie Tube");
            name.AddTranslation(GameCulture.Chinese, "");
            name.AddTranslation(GameCulture.Russian, "Газоразрядный индикатор");
            AddMapEntry(new Color(153, 38, 0), name);
            Main.tileSolidTop[Type] = true;
            Main.tileLighted[Type] = true;
        }

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("NixieTube"), 1, false, 0, false, false);
			mod.GetTileEntity<NixieTubeEntity>().CloseUI();
			mod.GetTileEntity<NixieTubeEntity>().Kill(i + 1, j + 1);
		}

		public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.showItemIcon = true;
            player.showItemIcon2 = ItemID.Cog;
        }

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			var tile = Main.tile[i, j];
			var Zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				Zero = Vector2.Zero;
			}
			var Height = 16;

			Point16 entityPosition = EntityPosition(i, j);

			NixieTubeEntity entity = mod.GetTileEntity<NixieTubeEntity>(entityPosition);

			if (entity != null)
			{
				Texture2D texture = mod.GetTexture("Glow/NixieTube_GlowMask");

				if (entity.Lightbulb.type == mod.ItemType<BlueLightbulb>())
					texture = mod.GetTexture("Glow/NixieTubeBlue_GlowMask");
				else if (entity.Lightbulb.type == mod.ItemType<OrangeLightbulb>())
					texture = mod.GetTexture("Glow/NixieTubeOrange_GlowMask");
				else if (entity.Lightbulb.type == mod.ItemType<GreenLightbulb>())
					texture = mod.GetTexture("Glow/NixieTubeGreen_GlowMask");
				else if (entity.Lightbulb.type == mod.ItemType<PurpleLightbulb>())
					texture = mod.GetTexture("Glow/NixieTubePurple_GlowMask");
				else if (entity.Lightbulb.type == mod.ItemType<RedLightbulb>())
					texture = mod.GetTexture("Glow/NixieTubeRed_GlowMask");
				else if (entity.Lightbulb.type == mod.ItemType<WhiteLightbulb>())
					texture = mod.GetTexture("Glow/NixieTubeWhite_GlowMask");
				else if (entity.Lightbulb.type == mod.ItemType<YellowLightbulb>())
					texture = mod.GetTexture("Glow/NixieTubeYellow_GlowMask");
				else
					texture = mod.GetTexture("Glow/NixieTube_GlowMask");

				if (entity.Chip.type > 0) Main.spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + Zero, new Rectangle(tile.frameX, tile.frameY, 16, Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
		}

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
			Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTube");

			Point16 entityPosition = EntityPosition(i, j);

			NixieTubeEntity entity = mod.GetTileEntity<NixieTubeEntity>(entityPosition);

			if (entity != null)
			{
				if (entity.Lightbulb.type <= 0)
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTube");

				Texture2D texture = mod.GetTexture("Glow/NixieTube_GlowMask");

				if (entity.Lightbulb.type == mod.ItemType<BlueLightbulb>())
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTubeBlue");
				else if (entity.Lightbulb.type == mod.ItemType<OrangeLightbulb>())
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTubeOrange");
				else if (entity.Lightbulb.type == mod.ItemType<GreenLightbulb>())
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTubeGreen");
				else if (entity.Lightbulb.type == mod.ItemType<PurpleLightbulb>())
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTubePurple");
				else if (entity.Lightbulb.type == mod.ItemType<RedLightbulb>())
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTubeRed");
				else if (entity.Lightbulb.type == mod.ItemType<WhiteLightbulb>())
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTubeWhite");
				else if (entity.Lightbulb.type == mod.ItemType<YellowLightbulb>())
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTubeYellow");
				else
					Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTube");
			}
			else Main.tileTexture[Type] = mod.GetTexture("Tiles/Decorations/NixieTube");
			return true;
		}

		public override void RightClick(int i, int j)
		{
			Main.mouseRightRelease = false;

			Point16 entityPosition = EntityPosition(i, j);

			NixieTubeEntity entity = mod.GetTileEntity<NixieTubeEntity>(entityPosition);

			if (entity == null)
				return;

			Player player = Main.LocalPlayer;

			Main.PlaySound(10, (int)player.position.X, (int)player.position.Y, 0);

			NixieTubeUI.assignedTile = this;

			NixieTubeUI.entity = entity;

			if (!(NixieTubeUI.entity.Lightbulb.modItem is Lightbulb2)) NixieTubeUI.entity.Lightbulb = new Item();
			if (!(NixieTubeUI.entity.Chip.modItem is LightingChip)) NixieTubeUI.entity.Chip = new Item();

			if (!NixieTubeUI.visible)
				entity.OpenUI();

			Tile tile = Main.tile[i, j];

			int x = i - (tile.frameX / 18) % 2;
			int y = j - (tile.frameY / 18) % 3;

			NixieTubeUI.cordX = x;
			NixieTubeUI.cordY = y;
		}

		Point16 EntityPosition(int i, int j) { return new Point16(i - Main.tile[i, j].frameX % 36 / 18 + 1, j - Main.tile[i, j].frameY % 56 / 18 + 1); }

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if ((tile.frameX > 36 && tile.frameY < 56) || (tile.frameY > 112 && tile.frameY < 168) || (tile.frameY > 226 && tile.frameY < 282))
            {
				Point16 entityPosition = EntityPosition(i, j);

				NixieTubeEntity entity = mod.GetTileEntity<NixieTubeEntity>(entityPosition);

				if (entity != null)
				{
					if (entity.Lightbulb.type == mod.ItemType<BlueLightbulb>())
					{
						r = 0.3f;
						g = 0.3f;
						b = 0.9f;
					}
					else if (entity.Lightbulb.type == mod.ItemType<OrangeLightbulb>())
					{
						r = 0.9f;
						g = 0.4f;
						b = 0.3f;
					}
					else if (entity.Lightbulb.type == mod.ItemType<GreenLightbulb>())
					{
						r = 0.4f;
						g = 0.8f;
						b = 0.6f;
					}
					else if (entity.Lightbulb.type == mod.ItemType<PurpleLightbulb>())
					{
						r = 0.7f;
						g = 0.4f;
						b = 0.9f;
					}
					else if (entity.Lightbulb.type == mod.ItemType<RedLightbulb>())
					{
						r = 0.9f;
						g = 0.4f;
						b = 0.4f;
					}
					else if (entity.Lightbulb.type == mod.ItemType<WhiteLightbulb>())
					{
						r = 0.9f;
						g = 0.9f;
						b = 0.9f;
					}
					else if (entity.Lightbulb.type == mod.ItemType<YellowLightbulb>())
					{
						r = 0.9f;
						g = 0.7f;
						b = 0.2f;
					}
					else
					{
						r = 0.9f;
						g = 0.5f;
						b = 0.5f;
					}
				}
            }
        }

		private int state = 1;
        public override void HitWire(int i, int j)
        {
            state = -state;
            int x = i - (Main.tile[i, j].frameX / 18) % 2;
            int y = j - (Main.tile[i, j].frameY / 18) % 3;

            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x, y + 2);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
            Wiring.SkipWire(x + 1, y + 2);

            for (int l = x; l < x + 2; l++)
            {
                for (int m = y; m < y + 3; m++)
                {
                    if (Main.tile[l, m] == null)
                    {
                        Main.tile[l, m] = new Tile();
                    }
                    if (Main.tile[l, m].active() && Main.tile[l, m].type == Type)
                    {
                        if (Main.tile[l, m].frameY < 56)
                        {
                            Main.tile[l, m].frameY += 56;
                        }
						else if (Main.tile[l, m].frameY < 112)
						{
                            Main.tile[l, m].frameY -= 56;
                        }
						if (Main.tile[l, m].frameY >= 112)
						{
							if (Main.tile[l, m].frameY < 168)
							Main.tile[l, m].frameY += 56;
							else Main.tile[l, m].frameY -= 56;
						}
					}
                }
            }
        }
    }
}