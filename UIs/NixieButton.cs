using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Antiaris.UIs
{
	public class NixieButton : UIElement
	{
		public int Index;
		public NixieButton()
		{
		}

		public override void Click(UIMouseEvent evt)
		{
			if (NixieTubeUI.visible)
			{
				Main.PlaySound(28, (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y, 0);
				int x = NixieTubeUI.entity.Position.X - 1;
				int y = NixieTubeUI.entity.Position.Y - 1;

				int width = 2;
				int height = 3;
				for (int i = x; i < x + width; i++)
				{
					for (int j = y; j < y + height; j++)
					{
						Tile tile = Main.tile[i, j];
						if (tile.active())
						{
							int frameWidth = width * 18;
							int frameHeight = height * 18 + 2;

							int index = Index - 35;
							tile.frameX = (short)((int)tile.frameX % frameWidth);
							tile.frameY = (short)((int)tile.frameY % frameHeight);

							bool isMax = Index > 34;
							int index2 = Index - 70;
							tile.frameX += (short)((isMax ? (Index > 69 ? index2 : index) : Index) * frameWidth);

							tile.frameY += (short)((isMax ? (Index > 69 ? 2 : 1) : 0) * frameHeight * 2);

							if (Main.netMode == 1)
								NetMessage.SendTileSquare(-1, i, j, 3);
						}
					}
				}
			}
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (NixieTubeUI.visible)
			{
				Player player = Main.LocalPlayer;
				float opacity = IsMouseHovering ? 1f : .3f;
				CalculatedStyle dimensions = base.GetDimensions();
				Texture2D texture = ModContent.GetTexture("Antiaris/UIs/NixieTubeButtonTexture");
				spriteBatch.Draw
					(
						texture,
						dimensions.Position(),
						new Rectangle(30 * (Index >= 41 ? Index - 41 : Index), 50 * (Index >= 41 ? 1 : 0), 30, 48),
						Color.White * opacity,
						0f,
						Vector2.Zero,
						1f,
						SpriteEffects.None,
						0
					);
			}
		}
	}

	public class UIDisable : UIElement
	{
		public int Index;
		public UIDisable()
		{
			Texture2D _texture = ModContent.GetTexture("Antiaris/UIs/CloseButtonTexture");
			base.Width.Set(_texture.Width, 0);
			base.Height.Set(_texture.Height, 0); ;
		}

		public override void Click(UIMouseEvent evt)
		{
			if (NixieTubeUI.visible)
			{
				Main.PlaySound(11, (int)Main.player[Main.myPlayer].position.X, (int)Main.player[Main.myPlayer].position.Y, 0);
				NixieTubeUI.visible = false;
			}
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (NixieTubeUI.visible)
			{
				Player player = Main.LocalPlayer;
				float opacity = IsMouseHovering ? 1f : .3f;
				CalculatedStyle dimensions = base.GetDimensions();
				Texture2D texture = ModContent.GetTexture("Antiaris/UIs/CloseButtonTexture");
				spriteBatch.Draw
					(
						texture,
						dimensions.Position(),
						null,
						Color.White * opacity,
						0f,
						Vector2.Zero,
						1f,
						SpriteEffects.None,
						0
					);
			}
		}
	}
}
