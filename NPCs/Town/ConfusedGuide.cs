using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Antiaris.NPCs.Town
{
	public class ConfusedGuide : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Confused Guide");
			DisplayName.AddTranslation(GameCulture.Chinese, "");
			DisplayName.AddTranslation(GameCulture.Russian, "Сбитый с толку Гид");
		}

		public override void SetDefaults()
		{
			npc.dontTakeDamage = true;
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 26;
			npc.height = 50;
			npc.aiStyle = 7;
			npc.damage = 0;
			npc.defense = 50;
			npc.lifeMax = 700;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.0f;
			animationType = -1;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.64");
		}

		public override void AI()
		{
			npc.rotation = npc.velocity.X = 0.0f;
			npc.velocity.Y = 5.0f;
			npc.direction = npc.spriteDirection;
			Player player = Main.player[Main.myPlayer];
			if ((double)player.position.X > (double)npc.position.X) npc.spriteDirection = 1;
			else if ((double)player.position.X < (double)npc.position.X) npc.spriteDirection = -1;
			if (Main.netMode != 1 && npc.townNPC)
			{
				npc.homeless = false;
				npc.homeTileX = -1;
				npc.homeTileY = -1;
				npc.netUpdate = true;
			}
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				try { guideQuest(); }
				catch (Exception exception)
				{
					Main.NewText("Oh no, an error happened! Report this to Zerokk and send him the file Terraria/ModLoader/Logs/Logs.txt");
					ErrorLogger.Log(exception);
				}
			}
		}

		public bool finishedGuideQuest = false;
		protected void guideQuest()
		{
			Player player = Main.player[Main.myPlayer];
			GuideQuestSystem guideQuestSystem = player.GetModPlayer<GuideQuestSystem>(mod);
			if (finishedGuideQuest)
			{ }
			else if (guideQuestSystem.CurrentQuest == -1)
			{
				var NewQuest = guideQuestSystem.ChooseNewQuest();
				Main.npcChatText = guideQuestSystem.Quests[NewQuest].ToString();
				Main.npcChatCornerItem = guideQuestSystem.Quests[NewQuest].ItemType;
				guideQuestSystem.CurrentQuest = NewQuest;
				return;
			}
			else
			{
				if (guideQuestSystem.CheckQuest())
				{
					guideQuestSystem.CompleteQuest(this);
					return;
				}
				else
				{
					Main.npcChatText = guideQuestSystem.GetCurrentQuest().ToString();
					Main.npcChatCornerItem = guideQuestSystem.GetCurrentQuest().ItemType;
					return;
				}
			}
		}

		public void WakeUp()
		{
			npc.dontTakeDamage = false;
			Main.npcChatText = Language.GetTextValue("Mods.Antiaris.GuideThanks");
			if (Main.netMode == NetmodeID.SinglePlayer)
				npc.Transform(22);
			else
			{
				ModPacket packet = mod.GetPacket();
				packet.Write((byte)6);
				packet.Write(npc.whoAmI);
				packet.Send();
			}
		}

		public override void HitEffect(int hitDirection, double damage) { damage = 0; }
		public override bool UsesPartyHat() { return false; }
		public override string GetChat() { return Language.GetTextValue("Mods.Antiaris.GuideChat"); }
		public class GuideQuestSystem : ModPlayer
		{
			public List<Quest> Quests = new List<Quest>();
			public int CurrentQuest = -1;
			public override void Initialize()
			{
				Quests.Clear();
				Quests.Add(new Quest("Mods.Antiaris.GuideQuest", mod.ItemType("TheGuideforGuides"), 1, 1d, "Mods.Antiaris.GuideThanks"));
			}

			public Quest GetCurrentQuest()
			{
				return Quests[CurrentQuest];
			}

			public int currentQuest
			{
				get { return CurrentQuest; }
				set { CurrentQuest = value; }
			}

			public override void clientClone(ModPlayer clientClone)
			{
				GuideQuestSystem clone = clientClone as GuideQuestSystem;
				clone.CurrentQuest = CurrentQuest;
			}

			public override void SendClientChanges(ModPlayer clientPlayer)
			{
				GuideQuestSystem clone = clientPlayer as GuideQuestSystem;
				if (clone.CurrentQuest != CurrentQuest)
				{
					ModPacket packet = mod.GetPacket();
					packet.Write((byte)5);
					packet.Write(player.whoAmI);
					packet.Write(CurrentQuest);
					packet.Send();
				}
			}

			public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
			{
				ModPacket packet = mod.GetPacket();
				packet.Write((byte)5);
				packet.Write(player.whoAmI);
				packet.Write(CurrentQuest);
				packet.Send(toWho, fromWho);
			}
			
			public bool CheckQuest()
			{
				if (CurrentQuest == -1)
					return false;
				Quest quest = Quests[CurrentQuest];
				if (player.CountItem(quest.ItemType, quest.ItemAmount) >= quest.ItemAmount)
				{
					int LeftToRemove = quest.ItemAmount;
					foreach (Item item in player.inventory)
					{
						if (item.type == quest.ItemType)
						{
							int Removed = Math.Min(item.stack, LeftToRemove);
							item.stack -= Removed;
							LeftToRemove -= Removed;
							if (item.stack <= 0)
								item.SetDefaults();
							if (LeftToRemove <= 0)
								return true;
						}
					}
				}
				return false;
			}

			public void CompleteQuest(ConfusedGuide npc)
			{
				Quest quest = Quests[CurrentQuest];
				npc.finishedGuideQuest = true;
				CurrentQuest = -1;
				npc.WakeUp();
			}

			public int ChooseNewQuest()
			{
				return 0;
			}
		}

		public class Quest
		{
			public int ItemAmount;
			public int ItemType;
			public string Name;
			public Action<NPC> SpawnReward;
			public string SpecialThanks;
			public double Weight;

			public Quest(string name, int itemID, int itemAmount = 1, double weight = 1d, string specialThanks = null, Action<NPC> spawnReward = null)
			{
				Name = name;
				ItemType = itemID;
				ItemAmount = itemAmount;
				Weight = weight;
				SpecialThanks = specialThanks;
				SpawnReward = spawnReward;
			}

			public override string ToString()
			{
				return Language.GetTextValue(Name, Main.LocalPlayer.name);
			}

			public string SayThanks()
			{
				return Language.GetTextValue(SpecialThanks);
			}
		}
		
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = mod.GetTexture("Miscellaneous/QuestIcon2");
			Texture2D texture2 = mod.GetTexture("Miscellaneous/QuestIcon3");
			Player player = Main.player[Main.myPlayer];
			var guideQuestSystem = player.GetModPlayer<GuideQuestSystem>();
			if (guideQuestSystem.CurrentQuest == -1) 
			{
				if (texture == null) return;
				Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
				float y = 50.0f;
				Vector2 position = npc.Center - Main.screenPosition - new Vector2(0.0f, y);
				spriteBatch.Draw(texture, position, null, Color.White, 0, origin, npc.scale, SpriteEffects.None, 0.0f);
			}
			foreach (var item in player.inventory)
			{
				if (item.type == mod.ItemType("TheGuideforGuides") && guideQuestSystem.CurrentQuest != -1)
				{
					if (texture == null) return;
					Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
					float y = 50.0f;
					Vector2 position = npc.Center - Main.screenPosition - new Vector2(0.0f, y);
					spriteBatch.Draw(texture2, position, null, Color.White, 0, origin, npc.scale, SpriteEffects.None, 0.0f);
				}
			}
		}
	}
}
