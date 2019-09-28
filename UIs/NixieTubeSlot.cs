using System;
using Antiaris.Items.Miscellaneous;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Antiaris.UIs
{
	public class NixieTubeSlot : UIElement
	{
		public Texture2D texture;

		public Color color;

		public int type = 0;

		public NixieTubeSlot(int type)
		{
			color = Color.White;
			texture = Main.inventoryBackTexture;

			Width.Set(texture.Width, 0.0f);
			Height.Set(texture.Height, 0.0f);

			this.type = type;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
            base.DrawSelf(spriteBatch);

			float swapFloat = 0.85f;
			Utils.Swap<float>(ref swapFloat, ref Main.inventoryScale);

			Vector2 newPos = GetDimensions().Position();

			Color newColor = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);

			//DrawSlot(texture, spriteBatch, newPos, Main.inventoryBack, Main.inventoryScale);

			Utils.Swap<float>(ref swapFloat, ref Main.inventoryScale);

			Item item = (type > 0 ? NixieTubeUI.entity.Chip : NixieTubeUI.entity.Lightbulb);
			Player player = Main.LocalPlayer;
			if (ItemSlot.ShiftInUse && Main.mouseItem.type <= 0)
			{
				for (int x = 0; x < 10; x++)
				{
					for (int y2 = 0; y2 < 5; y2++)
					{
						Main.inventoryScale = 0.85f;
						int x2 = (int)(20.0 + (double)(x * 56) * (double)Main.inventoryScale);
						int y = (int)(20.0 + (double)(y2 * 56) * (double)Main.inventoryScale);
						int slot = x + y2 * 10;
						if (Main.mouseX >= x2 && (double)Main.mouseX <= (double)x2 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && (Main.mouseY >= y && (double)Main.mouseY <= (double)y + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
						{
							if (Main.mouseLeftRelease && Main.mouseLeft)
							{
								Main.LocalPlayer.mouseInterface = true;
								Item slotItem = player.inventory[slot];
								if (slotItem.stack > 0 && item.stack == 0 && (type > 0 ? slotItem.modItem is LightingChip : slotItem.modItem is Lightbulb2))
								{
									Main.PlaySound(7, -1, -1, 1, 1.0f, 0.0f);
									if (item.type == 0)
									{
										item.SetDefaults(slotItem.type);
										item.stack = 0;
									}
									item.stack++;
									if (--slotItem.stack <= 0)
									{
										slotItem.SetDefaults(0, false);
										break;
									}
									if (type > 0) NixieTubeUI.entity.Chip = item;
									else NixieTubeUI.entity.Lightbulb = item;
									if (Main.netMode == 1)
										NixieTubeUI.entity.SendClientMessage();
									break;
								}
							}
						}
					}
				}
				if (Utils.CenteredRectangle(newPos, texture.Size() * Main.inventoryScale).Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)
				{
					Main.LocalPlayer.mouseInterface = true;
					if (Main.mouseLeftRelease &&
							Main.mouseLeft)
					{
						if (ItemSlot.ShiftInUse)
						{
							if (!IsEmpty(item) && IsEmpty(Main.mouseItem))
							{
								DropItem(item);
								if (type > 0) NixieTubeUI.entity.Chip = new Item();
								else NixieTubeUI.entity.Lightbulb = new Item();
								if (Main.netMode == 1)
									NixieTubeUI.entity.SendClientMessage();
							}
						}
					}
				}
			}
			else
			{			
				MouseActions(newPos, ref Main.mouseItem, ref item, ref swapFloat);
				if (type > 0) NixieTubeUI.entity.Chip = item;
				else NixieTubeUI.entity.Lightbulb = item;
				if (Main.netMode == 1)
					NixieTubeUI.entity.SendClientMessage();
			}
			Mod mod = ModLoader.GetMod("Antiaris");
			Texture2D texture2 = mod.GetTexture("UIs/Slot");
			if (type > 0) texture2 = mod.GetTexture("UIs/Chip");
			Vector2 drawPos = newPos - texture.Size() * swapFloat / 2.0f + new Vector2(texture2.Width, texture2.Height) * swapFloat / 2.0f;
			if ((type > 0 ? NixieTubeUI.entity.Chip : NixieTubeUI.entity.Lightbulb) == null || (type > 0 ? NixieTubeUI.entity.Chip : NixieTubeUI.entity.Lightbulb).type <= 0 || (type > 0 ? NixieTubeUI.entity.Chip : NixieTubeUI.entity.Lightbulb).stack <= 0)
				spriteBatch.Draw(texture2, drawPos + (type > 0 ? new Vector2(2, 3) : new Vector2(5, -5)), null, Color.White * 0.35f, 0.0f, new Vector2(), swapFloat, SpriteEffects.None, 0.0f);
			else DrawItem(spriteBatch, ref item, newPos, color, ref swapFloat);
		}

		protected bool AllowItem(Item item)
		{
			return type > 0 ? item.modItem is LightingChip : item.modItem is Lightbulb2;
		}

		protected bool IsEmpty(Item item)
		{ return item == null || item.type <= 0 || item.stack <= 0; }

		protected void DropItem(Item item)
		{
			Player player = Main.LocalPlayer;
			for (int i = 49; i >= 0; --i)
			{
				if (item.maxStack > 1)
				{
					if (player.inventory[i].stack < player.inventory[i].maxStack && item.IsTheSameAs(player.inventory[i]))
					{
						int stack = item.stack;
						if (item.stack + player.inventory[i].stack > player.inventory[i].maxStack)
							stack = player.inventory[i].maxStack - player.inventory[i].stack;

						item.stack -= stack;
						player.inventory[i].stack += stack;
						Main.PlaySound(7, -1, -1, 1, 1.0f, 0.0f);
						if (item.stack <= 0)
						{
							item.SetDefaults(0, false);
							break;
						}
					}
				}
				else
				{
					if (item.stack > 0)
					{
						if (player.inventory[i].stack == 0)
						{
							Main.PlaySound(7, -1, -1, 1, 1.0f, 0.0f);
							player.inventory[i] = item.Clone();
							item.SetDefaults(0, false);
							break;
						}
					}
				}
			}
		}

		protected bool MouseActions(Vector2 position, ref Item item1, ref Item item2, ref float inventoryScale, bool delete = false, bool mouseItemActions = false)
		{
			if (Utils.CenteredRectangle(position, texture.Size() * inventoryScale).Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;
				if (Main.mouseLeftRelease &&
						Main.mouseLeft)
				{
					if (ItemSlot.ShiftInUse)
					{ }
					else
					{
						if (!mouseItemActions)
							if ((!IsEmpty(item2) && IsEmpty(item1) || !IsEmpty(item1) && AllowItem(item1)))
							{
								if (delete)
									if (type > 0) NixieTubeUI.entity.Chip = new Item();
									else NixieTubeUI.entity.Lightbulb = new Item();
								if (item1.IsTheSameAs(item2) || item2.IsTheSameAs(item1))
								{
									if (item1.stack + item2.stack <= item1.maxStack)
									{
										item2.stack += item1.stack;
										item1.stack = 0;
									}
									else
									{
										if (item2.stack >= item2.maxStack)
											Utils.Swap<Item>(ref item1, ref item2);
										else
										{
											int stack = item1.maxStack - item2.stack;
											item2.stack += stack;
											item1.stack -= stack;
										}
									}
									Main.PlaySound(7, -1, -1, 1, 1.0f, 0.0f);
									return true;
								}
								else
								{
									Utils.Swap<Item>(ref item2, ref item1);
									Main.PlaySound(7, -1, -1, 1, 1.0f, 0.0f);
									return true;
								}
							}
					}
					return true;
				}
				if (!Main.mouseRightRelease ||
						!Main.mouseRight)
				{
					if (!mouseItemActions)
						if ((!IsEmpty(item2) && IsEmpty(item1) || !IsEmpty(item1) && AllowItem(item1)))
							if (item1.IsTheSameAs(item2) || item1.type == 0)
								if (item1.stack < item1.maxStack || item1.type == 0 && item2.stack > 0)
									if (Main.stackSplit <= 1 && Main.mouseRight)
									{
										Main.PlaySound(7, -1, -1, 1, 1.0f, 0.0f);
										if (item1.type == 0)
										{
											item1.SetDefaults(item2.type);
											item1.stack = 0;
										}
										++item1.stack;
										if (--item2.stack <= 0) item2.SetDefaults(0, false);
										Main.stackSplit = Main.stackSplit != 0 ? Main.stackDelay : 15;
									}
				}
			}
			return false;
		}

		protected void DrawSlot(Texture2D texture, SpriteBatch spriteBatch, Vector2 drawPos, Color lightColor, float inventoryScale, float alpha = 1.0f)
		{
			spriteBatch.Draw(texture, drawPos, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), lightColor * alpha, 0.0f, texture.Size() / 2.0f, inventoryScale, SpriteEffects.None, 0.0f);
		}

		protected void DrawItem(SpriteBatch spriteBatch, ref Item item, Vector2 drawPos, Color lightColor, ref float inventoryScale, float alpha = 1.0f)
		{
			Texture2D itemTexture = Main.itemTexture[item.type];

			Rectangle r = Main.itemAnimations[item.type] == null ? itemTexture.Frame(1, 1, 0, 0) : Main.itemAnimations[item.type].GetFrame(itemTexture);

			Color currentColor = lightColor;

			float scale = 1.0f;

			ItemSlot.GetItemLight(ref currentColor, ref scale, item, false);

			float newScale = 1.0f;
			if (r.Width > 32 || r.Height > 32)
				newScale = r.Width <= r.Height ? 32.0f / (float)r.Height : 32.0f / (float)r.Width;
			float currentScale = newScale * inventoryScale;

			Vector2 position = drawPos - r.Size() * currentScale / 2.0f;
			Vector2 origin = r.Size() * (float)((double)scale / 2.0 - 0.5);

			spriteBatch.Draw(itemTexture, position, new Rectangle?(r), item.GetAlpha(currentColor) * alpha, 0.0f, origin, currentScale * scale, SpriteEffects.None, 0.0f);

			if (item.stack > 1)
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, item.stack.ToString(), drawPos + new Vector2(-16.0f, 0.0f) * inventoryScale, color, 0.0f, Vector2.Zero, new Vector2(inventoryScale), -1.0f, inventoryScale);
		}
	}
}