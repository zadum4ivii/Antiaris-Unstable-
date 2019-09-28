using Antiaris.Items.Miscellaneous;
using Antiaris.Tiles.Decorations;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Antiaris.UIs
{
	public class NixieTubeEntity : ModTileEntity
	{
		public const short maxResources = 1;

		public Item Lightbulb { get; set; }
		public Item Chip { get; set; }

		public NixieTubeEntity()
		{
			Lightbulb = new Item();
			Chip = new Item();
		}

		public override void NetSend(BinaryWriter writer, bool lightSend)
		{
			writer.Write(Lightbulb.type);
			writer.Write(Chip.type);
		}

		public override void NetReceive(BinaryReader reader, bool lightReceive)
		{
			Lightbulb.type = reader.ReadInt32();
			Chip.type = reader.ReadInt32();
		}

		public void SendClientMessage()
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
				return;
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)11);
			packet.Write(ID);
			packet.Write(Lightbulb.type);
			packet.Write(Chip.type);
			packet.Send();
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 3);
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 1, Type);
				return -1;
			}
			return Place(i, j - 1);
		}

		public override bool ValidTile(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			return tile.active() && tile.type == mod.TileType<NixieTube>() && (tile.frameX % 36 / 18 == 1) && (tile.frameY % 56 / 18 == 1);
		}

		public override void OnKill()
		{
			Rectangle hitbox = new Rectangle((Position.X - 1) * 16, (Position.Y - 1) * 16, 36, 56);
			if (Lightbulb != null || Lightbulb != new Item() && Lightbulb.modItem is Lightbulb2)
				Item.NewItem(hitbox, Lightbulb.type, Lightbulb.stack);
			if (Chip != null || Chip != new Item() && Chip.modItem is LightingChip)
				Item.NewItem(hitbox, Chip.type, Chip.stack);
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				{
					"lightbulb",
					Lightbulb
				},
				{
					"chip",
					Chip
				}
			};
		}

		public override void Load(TagCompound tag)
		{
			if (tag.ContainsKey("lightbulb"))
				Lightbulb = tag.Get<Item>("lightbulb");
			if (tag.ContainsKey("chip"))
				Chip = tag.Get<Item>("chip");
		}

		public void CloseUI()
		{
			NixieTubeUI.visible = false;

			NixieTubeUI.entity = new NixieTubeEntity();
			NixieTubeUI.assignedTile = null;

			Main.PlaySound(SoundID.MenuClose);
		}

		public void OpenUI()
		{
			NixieTubeUI.visible = true;
			NixieTubeUI.entity = this;

			Main.playerInventory = true;

			Main.recBigList = false;

			if (!(NixieTubeUI.entity.Lightbulb.modItem is Lightbulb2)) NixieTubeUI.entity.Lightbulb = new Item();
			if (!(NixieTubeUI.entity.Chip.modItem is LightingChip)) NixieTubeUI.entity.Chip = new Item();
		}
	}
}
