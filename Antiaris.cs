using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Antiaris.NPCs.Town;
using Antiaris.VEffects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Antiaris.UIs;
using Terraria.UI;

namespace Antiaris
{
    public class Antiaris : Mod
    {
        public static Mod Thorium;
		public static Mod kRPG;
		public static Mod RockosARPG;
		public static Mod TerrariaOverhaul;
        public static Mod Unleveled;
        public static Texture2D cQuestTexture;
        public static Mod Instance;
        public static int coin;
        public static ModHotKey adventurerKey;
        public static ModHotKey hideTracker;
        internal QuestTrackerUI questTracker;
        private UserInterface questInterface;
        internal CurrentQuestUI cQuestUI;
        private UserInterface questLog;
        public static Texture2D trackerTexture;
        public static ModHotKey stand;
        internal static Antiaris instance;
		private UserInterface NixieFace;
        internal NixieTubeUI NixieUI;
        private static float lifePerHeart = 20f;
        private Color rb = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);

        public bool Tracker = true;

        public Antiaris()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

        public static string ConfigFileRelativePath {
			get { return "Mod Configs/Antiaris.json"; }
		}

        private Mod mod
        {
            get
            {
                return ModLoader.GetMod("Antiaris");
            }
        }

        public static void ReloadConfigFromFile() {
			// Define implementation to reload your mod's config data from file
		}

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
			byte msgType = reader.ReadByte();
            switch (msgType)
            {
                // id 1 = transform
                case 1:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
                    int transformedNPC = reader.ReadInt32();
                    Main.npc[transformedNPC].Transform(mod.NPCType("BrokenMirror"));
                    break;
                case 2:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
                    int transformedNPC2 = reader.ReadInt32();
                    Main.npc[transformedNPC2].Transform(mod.NPCType("Adventurer"));
                    break;
                case 3:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
					int player = reader.ReadInt32();
                    int currentQuest = reader.ReadInt32();
                    Main.player[player].GetModPlayer<QuestSystem>(mod).CurrentQuest = currentQuest;
                    break;
				case 4:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
					int player2 = reader.ReadInt32();
                    int currentPirateQuest = reader.ReadInt32();
                    Main.player[player2].GetModPlayer<Pirate.PirateQuestSystem>(mod).CurrentPirateQuest = currentPirateQuest;
                    break;
                case 5:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
                    int player3 = reader.ReadInt32();
                    int currentGuideQuest = reader.ReadInt32();
                    Main.player[player3].GetModPlayer<ConfusedGuide.GuideQuestSystem>(mod).CurrentQuest = currentGuideQuest;
                    break;
                case 6:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
                    int transformedNPC3 = reader.ReadInt32();
                    Main.npc[transformedNPC3].Transform(22);
                    break;
                case 7:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
                    int pirateCovePositionX = reader.ReadInt32();
                    AntiarisWorld.PirateCovePositionX = pirateCovePositionX;
                    break;
                case 8:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
                    int pirateCovePositionY = reader.ReadInt32();
                    AntiarisWorld.PirateCovePositionY = pirateCovePositionY;
                    break;
				case 11:
					if (Main.netMode == NetmodeID.MultiplayerClient)
						return;
					NixieTubeEntity entity = this.GetTileEntity<NixieTubeEntity>(reader.ReadInt32());
					int item1 = reader.ReadInt32();
					entity.Lightbulb.type = Main.item[item1].type;
					int item2 = reader.ReadInt32();
					entity.Chip.type = Main.item[item2].type;
					break;
            }
            if (Main.netMode != 2)
                return;
            NetMessage.SendData(7, -1, -1, (NetworkText)null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }

        public override void PostSetupContent()
        {
			Thorium = ModLoader.GetMod("ThoriumMod");
			kRPG = ModLoader.GetMod("kRPG");
			RockosARPG = ModLoader.GetMod("RockosARPG");
            kRPG = ModLoader.GetMod("kRPG");
            Unleveled = ModLoader.GetMod("Unleveled");
            TerrariaOverhaul = ModLoader.GetMod("TerrariaOverhaul");
            var bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                //SlimeKing = 1f;
                //EyeOfCthulhu = 2f;
                //EaterOfWorlds = 3f;
                //QueenBee = 4f;
                //Skeletron = 5f;
                //WallOfFlesh = 6f;
                //TheTwins = 7f;
                //TheDestroyer = 8f;
                //SkeletronPrime = 9f;
                //Plantera = 10f;
                //Golem = 11f;
                //DukeFishron = 12f;
                //LunaticCultist = 13f;
                //Moonlord = 14f;
                bossChecklist.Call("AddBossWithInfo", "Deadly Jones", 3.2f, (Func<bool>)(() => mod.GetModWorld<AntiarisWorld>().DownedDeadlyJones), "Open the Dead Man's Chest on Pirate's Boat using [i:" + ItemType("DavyKey") + "]");
                bossChecklist.Call("AddBossWithInfo", "Antlion Queen", 4.5f, (Func<bool>)(() => mod.GetModWorld<AntiarisWorld>().DownedAntlionQueen), "Use a [i:" + ItemType("AntlionDoll") + "] in Desert after beating Queen Bee");
				bossChecklist.Call("AddBossWithInfo", "Tower Keeper", 6.2f, (Func<bool>)(() => mod.GetModWorld<AntiarisWorld>().DownedTowerKeeper), "Break the Mirror in the Cursed Tower in Corruption or Crimson using [i:" + ItemType("StoneHammer1") +"] or [i:" + ItemType("StoneHammer2") +"]. Can be also summoned by using [i:" + ItemType("PocketCursedMirror") +"] or [i:" + ItemType("PocketCursedMirror2") +"] in Corruption or Crimson.");
            }
			
			Mod bossAssist = ModLoader.GetMod("BossAssist");
			if (bossAssist != null)
			{
				List<int> BossCollection = new List<int>()
				{
					ItemType("AntlionQueenMask"),
					ItemType("AntlionQueenTrophy"),
					ItemType("AntlionQueenMusicBox")
				};
				List<int> BossLoot = new List<int>()
				{
					ItemType("AntlionQueenTreasureBag"),
					ItemType("AntlionQueenClaw"),
					ItemType("DesertRage"),
					ItemType("ThousandNeedles"),
					ItemType("AntlionLongbow"),
					ItemType("AntlionStave"),
					ItemType("AntlionQueenEgg"),
					ItemType("AntlionCarapace"),
					ItemType("SandstormScroll"),
				};
				bossAssist.Call("AddStatPage",
								4.5f,
								NPCType("AntlionQueen"),
								Name,
								"Antlion Queen",
								(Func<bool>)(() => mod.GetModWorld<AntiarisWorld>().DownedAntlionQueen),
								ItemType("AntlionDoll"),
								BossCollection,
								BossLoot,
								"Antiaris/Miscellaneous/AntlionQueen");
									
			    List<int> BossCollection2 = new List<int>()
				{
					ItemType("TowerKeeperMask2"),
					ItemType("TowerKeeperTrophy2"),
					ItemType("TowerKeeperMusicBox2")
				};
				List<int> BossLoot2 = new List<int>()
				{
					ItemType("TowerKeeperTreasureBag2"),
					ItemType("TimeParadoxCrystal"),
					ItemType("ShadowChargedCrystal"),
					ItemType("MirrorShard"),
					ItemType("GuardianHeart2"),
				};
				bossAssist.Call("AddStatPage",
								6.2f,
								NPCType("TowerKeeper2"),
								Name,
								"Tower Keeper (Corruption)",
								(Func<bool>)(() => mod.GetModWorld<AntiarisWorld>().DownedTowerKeeper),
								ItemType("PocketCursedMirror"),
								BossCollection2,
								BossLoot2,
								"Antiaris/Miscellaneous/TowerKeeper2");
								
				List<int> BossCollection3 = new List<int>()
				{
					ItemType("TowerKeeperMask1"),
					ItemType("TowerKeeperTrophy1"),
					ItemType("TowerKeeperMusicBox1")
				};
				List<int> BossLoot3 = new List<int>()
				{
					ItemType("TowerKeeperTreasureBag1"),
					ItemType("TimeParadoxCrystal"),
					ItemType("BloodyChargedCrystal"),
					ItemType("MirrorShard"),
					ItemType("GuardianHeart"),
				};
				bossAssist.Call("AddStatPage",
								6.2f,
								NPCType("TowerKeeper"),
								Name,
								"Tower Keeper (Crimson)",
								(Func<bool>)(() => mod.GetModWorld<AntiarisWorld>().DownedTowerKeeper),
								ItemType("PocketCursedMirror2"),
								BossCollection3,
								BossLoot3,
								"Antiaris/Miscellaneous/TowerKeeper");
								
				List<int> BossCollection4 = new List<int>()
				{
					ItemType("DeadlyJonesMask"),
					ItemType("DeadlyJonesTrophy"),
				};
				List<int> BossLoot4 = new List<int>()
				{
					ItemType("DeadlyJonesTreasureBag"),
					ItemType("DavysMap"),
					ItemType("RoyalWeaponParts"),
				};
				bossAssist.Call("AddStatPage",
								3.2f,
								NPCType("DeadlyJones"),
								Name,
                                "Deadly Jones",
								(Func<bool>)(() => mod.GetModWorld<AntiarisWorld>().DownedDeadlyJones),
								ItemType(""),
								BossCollection4,
								BossLoot4,
                                "Antiaris/Miscellaneous/DeadlyJones");
			}
        }

        public override void Load()
        {
            ///UI codo
            #region Load UI
            cQuestTexture = ModContent.GetTexture("Antiaris/Miscellaneous/NoteBackground");
            trackerTexture = ModContent.GetTexture("Antiaris/Miscellaneous/QuestTracker");
            if (!Main.dedServ)
            {
                questTracker = new QuestTrackerUI();
                questTracker.Activate();
                questInterface = new UserInterface();
                questInterface.SetState(questTracker);

                cQuestUI = new CurrentQuestUI();
                cQuestUI.Activate();
                questLog = new UserInterface();
                questLog.SetState(cQuestUI);
            }
            #endregion
            ModExplorer._initialize();
            AntiarisGlowMasks.Load();
            adventurerKey = RegisterHotKey("Special Ability", "L");
			hideTracker = RegisterHotKey("Enable/Disable Quest Tracker", "Q");
            GameShaders.Armor.BindShader(ItemType("GooDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorSolar")).UseColor(0.1f, 0.3f, 0.2f).UseSecondaryColor(0.1f, 0.3f, 0.2f);
            coin = CustomCurrencyManager.RegisterCurrency(new CustomCoin(mod.ItemType("IronCoin"), 999L));
			AddEquipTexture(null, EquipType.Head, "BlackCap", "Antiaris/Items/Equipables/Vanity/BlackBizarreCap_Head");
            AddEquipTexture(null, EquipType.Head, "WhiteCap", "Antiaris/Items/Equipables/Vanity/WhiteBizarreCap_Head");
			AddEquipTexture(null, EquipType.Head, "PurpleCap", "Antiaris/Items/Equipables/Vanity/PurpleBizarreCap_Head");
			Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect")); // The path to the compiled shader file.
            Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["Shockwave"].Load();
			if (!Main.dedServ)
			{
				NixieUI = new NixieTubeUI();
				NixieUI.Activate();
				NixieFace = new UserInterface();
				NixieFace.SetState(NixieUI);
            }
            if (!Main.dedServ)
            {
                LightningBolt.SegmentTexture = GetTexture("VEffects/LightningSegment");
                LightningBolt.EndTexture = GetTexture("VEffects/LightningEnd");
                Filters.Scene["Antiaris:AntlionQueen"] = new Filter(new Data("FilterMiniTower").UseColor(0.9f, 0.5f, 0.2f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                SkyManager.Instance["Antiaris:AntlionQueen"] = new Sky();
				Filters.Scene["Antiaris:Corruption"] = new Filter(new Data("FilterMiniTower").UseColor(0.1f, 0.1f, 0.1f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["Antiaris:Corruption"] = new Sky();
				Filters.Scene["Antiaris:TimeSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.6f, 0.6f, 0.6f), EffectPriority.High);
                SkyManager.Instance["Antiaris:TimeSky"] = new TimeSky();
                Filters.Scene["Antiaris:TimeSky2"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(Main.DiscoR, Main.DiscoG, Main.DiscoB), EffectPriority.High);
                SkyManager.Instance["Antiaris:TimeSky2"] = new TimeSky();
                Filters.Scene["Antiaris:TimeSky3"] = new Filter(new ScreenShaderData("FilterInvert"), EffectPriority.High);
                SkyManager.Instance["Antiaris:TimeSky3"] = new Sky();
                Filters.Scene["Antiaris:DeadlyJones"] = new Filter(new Data("FilterMiniTower").UseColor(0.2f, 0.9f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["Antiaris:DeadlyJones"] = new Sky();
                Filters.Scene["Antiaris:HandheldDevice"] = new Filter(new Data("FilterTest2"), EffectPriority.VeryHigh);
                SkyManager.Instance["Antiaris:HandheldDevice"] = new Sky();
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AntlionQueen"), ItemType("AntlionQueenMusicBox"), TileType("AntlionQueenMusicBox"));
				if(WorldGen.crimson)
				{
					AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TowerKeeper"), ItemType("TowerKeeperMusicBox1"), TileType("TowerKeeperMusicBox1"));
				}
				else if (!WorldGen.crimson)
				{
					AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TowerKeeper"), ItemType("TowerKeeperMusicBox2"), TileType("TowerKeeperMusicBox2"));
				}
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/HallowRain"), ItemType("HallowRainMusicBox"), TileType("HallowRainMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/CrimsonRain"), ItemType("CrimsonRainMusicBox"), TileType("CrimsonRainMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/CorruptionRain"), ItemType("CorruptionRainMusicBox"), TileType("CorruptionRainMusicBox"));
				AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Blizzard"), ItemType("BlizzardMusicBox"), TileType("BlizzardMusicBox"));
				AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/UndergroundDesert"), ItemType("UndergroundDesertMusicBox"), TileType("UndergroundDesertMusicBox"));
				AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SlimeKing"), ItemType("KingSlimeMusicBox"), TileType("KingSlimeMusicBox"));
				AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/HallowNight"), ItemType("HallowNightMusicBox"), TileType("HallowNightMusicBox"));
            }
			
            #region Translations
            ModTranslation text = CreateTranslation("UnconsciousGuide");
            text.SetDefault("Ugh... My head hurts so much... Oh, hello, {0}! A horde of zombies bursted into my house by night and pretty much destroyed it... If you could repair it I'd live in it again! You should look into my chest, it has some things that can help you with progression.");
            text.AddTranslation(GameCulture.Chinese, "呃啊…我的头…哦，哈喽，{0}!之前有人来到这里洗劫了我的家，还把它给烧毁了...你能不能修复它？求你了，否则我将无家可归...你可以翻一下我家里的箱子，也许有对你有用的东西。顺带一提，有问题尽管找我！");
            text.AddTranslation(GameCulture.Russian, "Угх... Моя голова так болит... Оу, здравствуй, {0}! Толпа зомби ворвалась в мой дом под покровом ночи и почти разрушила его... Если бы ты смог его починить, я бы мог там снова жить! В моем сундуке есть немного вещей, которые помогут тебе с продвижением. ");
            AddTranslation(text);
			
            text = CreateTranslation("EnchantedSetBonus");
            text.SetDefault("17% reduced mana usage\nIncreases maximum mana by 20\nHitting an enemy may cause a dagger to pierce them");
            text.AddTranslation(GameCulture.Chinese, "1、减少 17% 的魔力消耗\n2、魔力最大值增加 20\n3、攻击敌人可能会有光之刃为你刺杀它们");
            text.AddTranslation(GameCulture.Russian, "Снижает использование маны на 17%\nУвеличивает максимальное количество маны на 20\nПри ударе по врагу может появиться клинок, который пронзит его");
			AddTranslation(text);

            text = CreateTranslation("DiscipleSetBonus");
            text.SetDefault("Increases maximum mana by 20\nEach third damage dealt creates a magical energy that restores mana");
            text.AddTranslation(GameCulture.Chinese, "1、增加 20 点魔力最大值\n2、每第三次施予攻击将召唤能够恢复魔力的魔法能量");
            text.AddTranslation(GameCulture.Russian, "Увеличивает максимальное количество маны на 20\nКаждое третье попадание создаст магическую энергию, восстанавливающую ману");
            AddTranslation(text);
            
            text = CreateTranslation("SorcererSetBonus");
            text.SetDefault("Increases maximum mana by 30\n10% reduced mana usage\nWhen the player is moving, he receives mana every 2 seconds");
            text.AddTranslation(GameCulture.Chinese, "1、增加 30 点魔力最大值\n2、减少 10% 魔力消耗\\n3、当玩家移动时，每 2 秒将接收魔力");
            text.AddTranslation(GameCulture.Russian, "Увеличивает максимальное количество маны на 30\nСнижает использование маны на 10%\nКогда игрок двигается, он восстанавливает ману каждые 2 секунды");
            AddTranslation(text);
			
			text = CreateTranslation("GooSetBonus");
            text.SetDefault("Grants 5 defense if there are slimes nearby");
            text.AddTranslation(GameCulture.Chinese, "如果附近有史莱姆将增加 5 防御力");
            text.AddTranslation(GameCulture.Russian, "Дает 5 защиты, если рядом есть слизни");
			AddTranslation(text);
			
			text = CreateTranslation("AntlionSetBonus");
            text.SetDefault("Periodically summons friendly Antlion Swarmers (Maximum amount is 3)\nEach Swarmer increases minion damage by 10%, grants 2 defense and decreases damage taken by 5%\nSwarmers disappear after sometime but then spawn again");
            text.AddTranslation(GameCulture.Chinese, "定期召唤蚁狮蜂（最大数量为 3 ）\n每个蚁狮蜂会为你增加 10% 召唤伤害，2 防御力且减少 5% 所承受的伤害伤害\n蚁狮蜂会在一定时间后消失，然后再次被召唤");
            text.AddTranslation(GameCulture.Russian, "Периодически призывает дружественных Взрослых муравьиных львов (Максимальное количество - 3)\nКаждый Муравьиный лев увеличивает урон миньонов на 10%, даёт 2 защиты и снижает получаемый урон на 5%\nМуравьиные львы исчезают через некоторое время, но затем появляются снова");
			AddTranslation(text);
			
			text = CreateTranslation("NecromancerSetBonus");
            text.SetDefault("Killing enemies drains their life force, healing the player for a small amount of life");
            text.AddTranslation(GameCulture.Chinese, "杀死敌人时耗尽他们的生命力，治愈玩家少量生命值");
            text.AddTranslation(GameCulture.Russian, "Убийство врага высасывает его жизненную силу, восстанавливая небольшое количество здоровья игроку");
			AddTranslation(text);
			
			text = CreateTranslation("LeafRoll");
            text.SetDefault("Uses: ");
            text.AddTranslation(GameCulture.Chinese, "使用次数：");
            text.AddTranslation(GameCulture.Russian, "Использований: ");
			AddTranslation(text);

            text = CreateTranslation("Note1");
            text.SetDefault("   If you don't give us\n  the information we need,\n   we will slaughter you.\n Don't pretend you don't\n     know where the thing\n we look for is.");
            text.AddTranslation(GameCulture.Chinese, "      如果你不提供\n   我们所需要的证据，\n   我们会宰了你。\n      不要假装你不知道那些，\n   我们在暗中观察你。");
			text.AddTranslation(GameCulture.Russian, "Если ты не дашь нам ин-\n     формацию, которую\nумалчиваешь, мы тебя\nуничтожим. Не притво-\n  ряйся, что не знаешь,\n   где то, что мы ищем.");
			AddTranslation(text);

            text = CreateTranslation("Note2");
            text.SetDefault("  If ye're readin' this\n       - don't free this\n           beast at any cost!\n      Leave 'im alone\n  t' rot!");
            text.AddTranslation(GameCulture.Chinese, "  如果你正在阅读这个\n       不要不惜一切代价\n释放这个野兽！\n      让我一人\n    独自腐烂！");
            text.AddTranslation(GameCulture.Russian, "  Если вы читаете это\n    не освобождайте это \n чудище любой ценой! \n       оставьте его\n                    гнить!");
            AddTranslation(text);

            text = CreateTranslation("PirateHelp1");
            text.SetDefault("Help me, stranger!");
            text.AddTranslation(GameCulture.Chinese, "能不能来救救我？");
            text.AddTranslation(GameCulture.Russian, "Помоги мне, незнакомец!");
			AddTranslation(text);
			
			text = CreateTranslation("PirateHelp1F");
			text.SetDefault("Help me, stranger!");
			text.AddTranslation(GameCulture.Chinese, "能不能来救救我？");
			text.AddTranslation(GameCulture.Russian, "Помоги мне, незнакомка!");
			AddTranslation(text);
			
			text = CreateTranslation("PirateHelp2");
            text.SetDefault("Help me and I will reward you!");
            text.AddTranslation(GameCulture.Chinese, "救救我！我会回报你的!");
            text.AddTranslation(GameCulture.Russian, "Помоги мне и я дам тебе награду!");
			AddTranslation(text);
			
			text = CreateTranslation("PirateHelp3");
            text.SetDefault("You're not going to leave an old pirate, right?");
            text.AddTranslation(GameCulture.Chinese, "你不会丢下个老爷子不管的，对吧？");
            text.AddTranslation(GameCulture.Russian, "Ты же не бросишь старого пирата, да?");
			AddTranslation(text);

            text = CreateTranslation("FrozenAdventurer1");
            text.SetDefault("Looks like this man was snowed in. He's unconscious but he can be helped by digging him out. A shovel I've seen near the door would be useful...");
            text.AddTranslation(GameCulture.Chinese, "看来这个人被雪埋住并昏迷了，但是仍然可以铲掉他身上的雪救他，门旁边的铁锹也许有点用处...");
            text.AddTranslation(GameCulture.Russian, "Похоже, что этого человека завалило снегом. Он без сознания, но ему можно помочь, выкопав его. Лопата, замеченная мною около двери, тут бы пригодилась...");
			AddTranslation(text);
			
			text = CreateTranslation("FrozenAdventurer2");
            text.SetDefault("Oh, thanks for saving me! That blizzard was so hard, so I decided to hide in this house. Unluckily, icicles have shattered the windows and... You've seen what happened. Couldn't get out without your help, thanks again!");
            text.AddTranslation(GameCulture.Chinese, "哦，谢谢你救了我！那场暴风雪太大了，所以我决定躲在这个房子里。不幸的是，冰锥打碎了窗户以及……我猜你已经看到发生了什么。没有你的帮助，我可能真的就死在这里了，再次感谢！");
            text.AddTranslation(GameCulture.Russian, "Ох, благодарю за спасение! Та буря была такой сильной, что я решил спрятаться в этом доме. К несчастью, сосульки разбили окна и... Вы видели, что произошло. Не смог выбраться без вашей помощи, вновь благодарю!");
			AddTranslation(text);

            text = CreateTranslation("Adventurer1");
            text.SetDefault("Somebody once told me that I will never become an great adventurer. And look at me - I've achieved a lot!");
            text.AddTranslation(GameCulture.Chinese, "有很多人曾经告诉我，我的理想就是一个笑话，不过你看看，对于理想，我现在已经接近了很多！");
            text.AddTranslation(GameCulture.Russian, "Однажды мне кто-то сказал, что я никогда не буду великим путешественником. И взгляни на меня - я достиг очень многого!");
			AddTranslation(text);

            text = CreateTranslation("Adventurer2");
            text.SetDefault("When you're going on an adventure, make sure to take some kind of knife with you to cut vines. One time I didn't take it and I had to tear vines with bare hands!");
            text.AddTranslation(GameCulture.Chinese, "当你去探险的时候，一定要拿一把匕首割开藤蔓，上次我忘记拿它了，只好用手撕开它...");
            text.AddTranslation(GameCulture.Russian, "Когда отправляешься в путешествие, убедись, что у тебя есть какой-то нож, чтобы резать лианы. Один раз я не взял его и мне пришлось рвать лианы голыми руками!");
			AddTranslation(text);

            text = CreateTranslation("Adventurer3");
            text.SetDefault("You know who am I? I am professor of archeology, expert on the occult, and how does one say it? Obtainer of rare antiquities.");
            text.AddTranslation(GameCulture.Chinese, "你知道我是谁吗？该怎么说呢…？我是一个挖掘珍贵文物的考古学家和神秘学家。");
            text.AddTranslation(GameCulture.Russian, "Ты знаешь, кто я? Я профессор археологии, эксперт по оккультным наукам и, как там говорят? Добыватель редких древних вещей.");
			AddTranslation(text);

            text = CreateTranslation("Adventurer4");
            text.SetDefault("I know that hornets can be pretty big but when I've entered the jungle and saw a hornet that was bigger then me... To put it mildly, I was shocked and never returned to the jungle again.");
            text.AddTranslation(GameCulture.Chinese, "我知道蜂类生物可能会很大，但是我进入丛林，发现了一堆比我自身还要巨大的马蜂…咳咳，说的好听一点，我很吃惊，从那以后我就再也没有回到丛林。");
            text.AddTranslation(GameCulture.Russian, "Я знаю, что шершни могут быть достаточно крупными, но когда я вошёл в джунгли и увидел шершня больше меня... Мягко говоря, я был шокирован и никогда больше не возвращался в джунгли");
			AddTranslation(text);

            text = CreateTranslation("Adventurer5");
            text.SetDefault("I can travel anywhere without a map! I never get lost. Wanna know how I'm doing this? I... Don't know.");
            text.AddTranslation(GameCulture.Chinese, "我可以没有地图在任何的地方旅行！而且从来不迷路！你想知道我是怎么做到的吗？我...不知道。");
            text.AddTranslation(GameCulture.Russian, "Я могу путешествовать где угодно без карты! Я даже никогда не теряюсь. Хочешь знать, как я это делаю? Я... Не знаю.");
			AddTranslation(text);

            text = CreateTranslation("Adventurer6");
            text.SetDefault("During my adventures I often was close to death, but you know what do we say to death? Not today.");
            text.AddTranslation(GameCulture.Chinese, "在我的冒险生涯里，我经常与死神擦肩而过，但你知道我们应该对死神说什么吗？不是今天。");
            text.AddTranslation(GameCulture.Russian, "Во время моих путешествий я часто был близок к смерти, но ты знаешь, что мы говорим смерти? Не сегодня.");
			AddTranslation(text);

            text = CreateTranslation("Adventurer7");
            text.SetDefault("Do you help this kid, {0}, with his needs? You know, I need help more than him and I also provide pretty better rewards for you.");
            text.AddTranslation(GameCulture.Chinese, "你帮这个熊孩子，{0}，满足需求吗？你懂得，我更需要你的帮助，我也会给你带来更好的回报。");
            text.AddTranslation(GameCulture.Russian, "Ты ведь помогаешь этому парнишке, {0}, с тем, что ему нужно? Знаешь, мне помощь нужна больше, чем ему, а я ведь еще предоставляю награды получше.");
            AddTranslation(text);

            text = CreateTranslation("Adventurer8");
            text.SetDefault("Some people are afraid of night, since many monsters appear during it. These people say that night is dark and full of terrors. But I'm not afraid of it, I'm brave!");
            text.AddTranslation(GameCulture.Chinese, "这些人总是说夜晚是充满了恐惧和黑暗的，因为许多怪物都在这个时候出现。但是我有胆子，才不怕那些东西！");
            text.AddTranslation(GameCulture.Russian, "Некоторые люди боятся ночи, потому что в ночное время появляются много монстров. Эти люди говорят, что ночь темна и полна ужасов. Но я её не боюсь, я храбрый!");
			AddTranslation(text);

            text = CreateTranslation("Thanks");
            text.SetDefault("Thank you for your help! I really appreciate it!");
            text.AddTranslation(GameCulture.Chinese, "十分感谢，我真的感激不尽！");
            text.AddTranslation(GameCulture.Russian, "Спасибо за твою помощь! Я очень ценю это!");
			AddTranslation(text);

            text = CreateTranslation("NoQuest1");
            text.SetDefault("Come back later if you wanna help me again!");
            text.AddTranslation(GameCulture.Chinese, "如果你想再帮我一次以后再过来！");
            text.AddTranslation(GameCulture.Russian, "Приходи попозже, если хочешь снова помочь мне!");
			AddTranslation(text);

            text = CreateTranslation("NoQuest2");
            text.SetDefault("Right now I don't have any quests for you, come back later!");
            text.AddTranslation(GameCulture.Chinese, "现在我没什么事情拜托你，以后再来!");
            text.AddTranslation(GameCulture.Russian, "Сейчас у меня нет для тебя никаких заданий, приходи попозже!");
			AddTranslation(text);

            text = CreateTranslation("NoQuest3");
            text.SetDefault("I think I can give you another quest soon if you need it!");
            text.AddTranslation(GameCulture.Chinese, "我想如果你有需要的话，我以后可以给你另一个任务！");
            text.AddTranslation(GameCulture.Russian, "Я думаю, что смогу скоро дать тебе еще задание, если оно тебе нужно!");
			AddTranslation(text);

            text = CreateTranslation("Quest0");
            text.SetDefault("One day when our crew was crossing the sea, we got into a dreadful storm. We've lost a lot of people, our ship was damaged, but the most terrible loss was my compass. My favourite compass with which I had so many adventures now lies at the bottom of the ocean. Please, try fishing it out in the ocean, I really miss it!");
            text.AddTranslation(GameCulture.Chinese, "有一天，我们横渡大海时，遭遇了一场可怕的暴风雨。我们失去了很多的船员，而且我们的船也损坏了，但对我而言最可怕的是我的罗盘已沉入海底。我最喜欢的罗盘和这么多的奇遇现在静静的躺在海底，你能不能试着帮我找回来，哪怕是用一个鱼钩如同大海捞针那样，我真的很想念它！");
            text.AddTranslation(GameCulture.Russian, "Однажды, когда я со своей командой пересекал море, мы попали в ужасный шторм. Мы потеряли много людей, наш корабль был поврежден, но самой ужасной потерей был мой компас. Мой любимый компас, с которым у меня было столько приключений, теперь лежит на дне океана. Пожалуйста, попробуй выудить его в океане, мне он ужасно дорог!");
			AddTranslation(text);
			
			text = CreateTranslation("Name0");
			text.SetDefault("'Lost in the Sea'");
			text.AddTranslation(GameCulture.Chinese, "“迷失深海”");
			text.AddTranslation(GameCulture.Russian, "'Утерянный в океане'");
			AddTranslation(text);
			
            text = CreateTranslation("Quest1");
            text.SetDefault("My friend once told me about an interesting artifact. We were in Egypt when he told me about this artifact and it was very hot there. This artifact is a some kind of ice crystal that can cool things. I thought that it would be awesome if I could cool down by using it when it's too hot outside. I only know that different ice creatures contain this crystal inside of them. Can you please find it and bring it to me?");
            text.AddTranslation(GameCulture.Chinese, "我的朋友曾经告诉我一些有趣的东西。我们在埃及时，他告诉了我这个东西，这个东西是一种能冷却物体的冰晶，我想如果外面太热了的话我可以用它来降温避免中暑。我只知道不同的冰元素生物体内含有这种冰晶，你能找到它并把它捎给我吗？");
            text.AddTranslation(GameCulture.Russian, "Мой друг однажды рассказал мне об интересном артефакте. В тот день, когда он рассказал мне о нем, мы были в Египте, а там было очень жарко. Этот артефакт это какой-то кристалл, который может охлаждать вещи. Я подумал, что было бы круто, если бы я смог охлаждаться с его помощью, если на улице слишком жарко. Я только лишь знаю, что разные ледяные существа содержат этот кристалл в себе. Можешь ли ты, пожалуйста, найти его и принести мне?");
			AddTranslation(text);
			
			text = CreateTranslation("Name1");
			text.SetDefault("'Fighting the heat'");
			text.AddTranslation(GameCulture.Chinese, "“防暑");
			text.AddTranslation(GameCulture.Russian, "'Борьба с жарой'");
			AddTranslation(text);

            text = CreateTranslation("Quest2");
            text.SetDefault("My beautiful hat is a bit damaged! I really need to fix it but I need some leather for this. Can you bring me... Let's say... 12 leather, so I can fix it?");
            text.AddTranslation(GameCulture.Chinese, "你看看我漂亮的帽子，已经有好几个破损的地方了！我真的需要修理它，但我需要一些皮革，你能捎给我吗？打个比方...12块山猪皮，这样我可以修复它？");
			text.AddTranslation(GameCulture.Russian, "Моя красивая шляпа немного повреждена! Я очень хочу починить её, но мне нужно немного кожи, чтобы сделать это. Ты можешь принести мне... Скажем... 12 кожи, чтобы я смог починить её?");
			AddTranslation(text);
			
			text = CreateTranslation("Name2");
			text.SetDefault("'Save the hat!'");
			text.AddTranslation(GameCulture.Chinese, "“修理帽子”");
			text.AddTranslation(GameCulture.Russian, "'Спасите шляпу!'");
			AddTranslation(text);

            text = CreateTranslation("Quest3");
            text.SetDefault("I've heard legends about creatures that were living in deserts. Unfortunately, all of them died during a massive cataclysm. Bones of those creatures are probably lost in the sands. I really want to know more about those creatures but the only way to do it is to find at least a little bone of them. Maybe you can dig in the sand and find any remains of those creatures?");
            text.AddTranslation(GameCulture.Chinese, "我听说过关于生活在沙漠里的生物的传说。不幸的是，它们都在一场大灾变中丧生了。这些生物的骸骨应该是被沙漠埋葬了，我真的很想知道更多关于这些生物的事情。但目前唯一的办法就是找到至少一点的骸骨，也许你可以挖开那些沙子找到这些远古生物的骸骨？");
            text.AddTranslation(GameCulture.Russian, "Я слышал легенды о существах, живших в пустынях. К несчастью, они все погибли из-за огромного катаклизма. Кости тех существ, вероятнее, затеряны в песках. Я очень хочу узнать побольше о тех существах, но единственный способ сделать это, это найти хотя бы маленькую их кость. Может, ты покопаешься в песке и найдешь какие-нибудь останки тех существ?");
			AddTranslation(text);
			
			text = CreateTranslation("Name3");
			text.SetDefault("'Ancient legends'");
			text.AddTranslation(GameCulture.Chinese, "“远古传说”");
			text.AddTranslation(GameCulture.Russian, "'Древние легенды'");
			AddTranslation(text);

            text = CreateTranslation("Quest4");
            text.SetDefault("Have you ever tried an omelette made of harpy eggs? No? Me too, but I really want to try it. Maybe you can bring me a harpy egg so I can cook the omelette of it? I can share it with you!");
            text.AddTranslation(GameCulture.Chinese, "你有没有试过用鹰身女妖的蛋来煎蛋？没有？我也是，但我真的想知道那是什么味道的，也许你可以给我一个鹰身女妖的蛋，这样我可以做它的鸡蛋卷？而且我可以和你一起分享！");
			text.AddTranslation(GameCulture.Russian, "Ты когда-нибудь пробовал яичницу из яиц гарпий? Нет? Я тоже, но я очень хочу попробовать её. Может ты можешь принести мне яйцо гарпии, чтобы я приготовил эту яичницу? Я могу с тобой ей поделиться!");
			AddTranslation(text);
			
			text = CreateTranslation("Name4");
			text.SetDefault("'Tasty omelette'");
			text.AddTranslation(GameCulture.Chinese, "“美味煎蛋”");
			text.AddTranslation(GameCulture.Russian, "'Вкусная яичница'");
			AddTranslation(text);
			
			text = CreateTranslation("Quest5");
            text.SetDefault("I think I just had a brilliant idea... I'll need an apple that's covered in pure gold. But I can't just go and get that kind of thing in a shop! Can you help me out with this? Don't ask why I need the apple, you'll see later.");
            text.AddTranslation(GameCulture.Chinese, "我有一个有趣的主意...我需要一个纯金的苹果。但我不能在商店买到那种东西！你能帮我这个忙吗？不要问我为什么需要它，以后你会明白的。");
            text.AddTranslation(GameCulture.Russian, "Хм, кажется, у меня появилась гениальная идея... Мне понадобится яблоко, покрытое чистым золотом. Но ведь такое в магазине не купишь! Может, ты мне поможешь? Не спрашивай, зачем мне это яблоко, потом узнаешь.");
			AddTranslation(text);
			
			text = CreateTranslation("Name5");
			text.SetDefault("'Strange experiment'");
			text.AddTranslation(GameCulture.Chinese, "“古怪试验”");
			text.AddTranslation(GameCulture.Russian, "'Странный эксперимент'");
			AddTranslation(text);

            text = CreateTranslation("ThanksApple1");
            text.SetDefault("Yes! Yes! Finally! Behold my creation! This is a golden apple mask! A true masterpiece! Do you like it? Here you go, try it on. I poked holes in it so that you can eat and breathe. Oh, and one more thing: I melted the excess gold and turned it back into coins. But they are worthless compared to this wonderful mask.");
            text.AddTranslation(GameCulture.Chinese, "耶！就是这样！最后！看看我的杰作！这是一个金苹果面具！你喜欢吗？给你，试试看。我在里面戳了一个洞，这样你就可以吃和呼吸了。哦，还有一件事，我把多余的金子熔炼成硬币。但是和这个漂亮的面具比起来，它们毫无价值。");
            text.AddTranslation(GameCulture.Russian, "Да! Да! Наконец-то! Узри моё творение! Это маска золотого яблока! Это же шедевр! Тебе нравится? На, померяй. Я вырезал в ней дыры, чтобы ты мог есть и дышать. А, и еще одно: остатки золота я перековал обратно в монеты. Но по сравнению с этой великолепной маской, они ничего не стоят.");
			AddTranslation(text);
			
			text = CreateTranslation("Quest6");
            text.SetDefault("Do you know that skeletons from that dark dungeon can revive fallen allies? I really want to be able to revive dead creatures too! I've gathered some information and found out that they use Necronomicon to do this. Can you go to that spooky place and bring me that magical book?");
            text.AddTranslation(GameCulture.Chinese, "你知道黑暗地牢的那些骷髅可以复活死去的生物吗？我真的希望借此来复活死去的动物们！我收集了一些信息，它们使用一种被叫做“死灵之书”做这个。你能去那个鬼地方给我捎来这本神奇的书吗？");
            text.AddTranslation(GameCulture.Russian, "Ты знаешь, что скелеты из того темного подземелья могут воскрешать мертвых союзников? Я тоже очень хочу воскрешать мертвых существ! Я собрал немного информации и узнал, что они используют Некрономикон чтобы делать это. Ты можешь сходить в это страшное место и принести мне эту магическую книгу?");
			AddTranslation(text);
			
			text = CreateTranslation("Name6");
			text.SetDefault("'Dark Magic'");
			text.AddTranslation(GameCulture.Chinese, "“黑魔法”");
			text.AddTranslation(GameCulture.Russian, "'Тёмная магия'");
			AddTranslation(text);
			
			text = CreateTranslation("Quest7");
            text.SetDefault("I'm so tired of these slimes! Whenever I go outside, they immediately attack me! I really want to kill them all but I'm a bad warrior! Can you kill, let's say 25 slimes so I could go outside without any troubles?");
            text.AddTranslation(GameCulture.Chinese, "我真讨厌这些史莱姆！每当我出门时，它们总是立刻攻击我！我真的干死它们，但我却是个很差劲的战士...你能干掉，比方说25个史莱姆吗？让我们出门在外没有任何烦心事！");
            text.AddTranslation(GameCulture.Russian, "Я так устал от этих слизней! Каждый раз, когда я выхожу на улицу, они сразу же меня атакуют! Я очень хочу убить их всех, но из меня ужасный воин. Можешь ли ты убить, скажем, 25 слизней, чтобы я смог выйти на улицу без всяких проблем?");
			AddTranslation(text);
			
			text = CreateTranslation("Name7");
			text.SetDefault("'Annoying creatures'");
			text.AddTranslation(GameCulture.Chinese, "“恼人粘液”");
			text.AddTranslation(GameCulture.Russian, "'Надоедливые существа'");
			AddTranslation(text);
			
			text = CreateTranslation("Quest8");
            text.SetDefault("When we once lost our way in caverns, one of our group members told me about a skeleton with a gold hat. We have met one and it captured our mapmaker! Soon we found him dead, I must take revenge on that skeleton! Can you kill it?");
            text.AddTranslation(GameCulture.Chinese, "我们迷失在洞穴的时候，队伍里有一个人告诉我发现了一个戴着黄金矿工头盔的骷髅。我们遇见了那样的骷髅，但是它抓走了我们的制图师，然后很快我们就发现他已经死了。我必须要为死去的队友报仇，你能帮我杀了它吗？");
            text.AddTranslation(GameCulture.Russian, "Когда мы один раз заблудились в подземельях, один из участников нашей команды рассказал мне о скелете с золотой каской. Мы встретили такого, и он схватил нашего картографа! Вскоре мы нашли его мертвым, я должен отомстить тому скелету! Не мог бы ты уничтожить его?");
			AddTranslation(text);
			
			text = CreateTranslation("Name8");
			text.SetDefault("'Vengeance'");
			text.AddTranslation(GameCulture.Chinese, "“偿还血债”");
			text.AddTranslation(GameCulture.Russian, "'Отомщение'");
			AddTranslation(text);
			
			text = CreateTranslation("Quest9");
            text.SetDefault("Yesterday I got to a sky island using magic. Everything was peaceful at the beginning but then I got attacked by giant birds! I fell off the island, luckily I haven't broken any bones. Can you kill these birds so I will not get attacked next time?");
            text.AddTranslation(GameCulture.Chinese, "昨天我登上了空岛施法，就在我觉得一切安全时，一些蓝绿色巨禽用它的爪子抓住了我把我从空岛扔了下来，幸好我掉进了水里，没被摔惨。你能干一票它们吗？这样我去空岛也许不会再体验一次蹦极");
            text.AddTranslation(GameCulture.Russian, "Вчера я попал на летающий остров при помощи магии. Сначала всё было мирно, но потом меня атаковали огромные птицы! Я упал с острова, к счастью, я не сломал ни одной кости. Ты можешь убить этих птиц, чтобы в следующий раз на меня не напали?");
			AddTranslation(text);
			
			text = CreateTranslation("Name9");
			text.SetDefault("'Mutated birds'");
			text.AddTranslation(GameCulture.Chinese, "“突变猛禽”");
			text.AddTranslation(GameCulture.Russian, "'Мутировавшие птицы'");
			AddTranslation(text);

            text = CreateTranslation("Quest10");
            text.SetDefault("Let's toss a coin! The loser will bring something to the winner. Ugh... Our coins seems to not have heads and tails... Guess I've won! You have to bring me a silk scarf!");
            text.AddTranslation(GameCulture.Chinese, "我们投硬币吧！赌输了的要给赌赢了的一些东西。诶等等…我们的硬币好像没有正面和反面…我猜我赢了！你得给我一条丝绸围巾！");
            text.AddTranslation(GameCulture.Russian, "Давай подбросим монетку! Проигравший что-нибудь принесёт победителю. Блин, на наших монетах нет орла и решки... Пожалуй, я победил! Ты должен принести мне шёлковый шарфик!");
			AddTranslation(text);
			
			text = CreateTranslation("Name10");
			text.SetDefault("'Prize for the winner'");
			text.AddTranslation(GameCulture.Chinese, "“获胜奖品”");
			text.AddTranslation(GameCulture.Russian, "'Приз победившему'");
			AddTranslation(text);

            text = CreateTranslation("Quest11");
            text.SetDefault("The idea of going fishing during night turned out to be bad! These disgusting zombies snatched fishing rod from my hands and broke it! How am I supposed to fish without fishing rod!? Can you please gather the pieces and repair it?");
            text.AddTranslation(GameCulture.Chinese, "在晚上钓鱼真是个馊主意。那些恶心的僵尸从我的手中夺走并打碎了鱼竿。没鱼竿我怎么钓鱼？你能帮我找回它的残骸并修理吗？");
            text.AddTranslation(GameCulture.Russian, "Идея сходить порыбачить ночью оказалась ужасной! Эти отвратительные зомби вырвали удочку из моих рук и сломали! Как я должен рыбачить без удочки!? Можешь ли ты, пожалуйста, собрать части удочки и починить её?");
			AddTranslation(text);
			
			text = CreateTranslation("Name11");
			text.SetDefault("'The failed fishing'");
			text.AddTranslation(GameCulture.Chinese, "“摸鱼失败”");
			text.AddTranslation(GameCulture.Russian, "'Неудавшаяся рыбалка'");
			AddTranslation(text);

			text = CreateTranslation("Quest12");
            text.SetDefault("I just noticed that this gunslinger is selling something that looks like a blueprint of a weapon! Unfortunately, I don't have enough money to buy it and even if I had - I'm not that smart to understand all these schemes. Think you can buy that blueprint and craft the weapon for me?");
            text.AddTranslation(GameCulture.Chinese, "我只是注意到那个军火商看起来正在卖一个武器的蓝图，糟糕的是我根本没有钱能够买它，就算我有，我也难以理解蓝图所写的设计方案。我猜，你也许能给我买到那个蓝图并制作出武器？");
            text.AddTranslation(GameCulture.Russian, "Я только что заметил, что этот стрелок продаёт что-то похожее на чертёж оружия! К сожалению, у меня нет нужного количества денег, чтобы купить его, а даже если было - я не настолько умный, чтобы понять все эти схемы. Может ты сможешь купить чертёж и создать для меня это оружие?");
			AddTranslation(text);
			
			text = CreateTranslation("Name12");
			text.SetDefault("'Making a powerful weapon'");
			text.AddTranslation(GameCulture.Chinese, "“锻造重武”");
			text.AddTranslation(GameCulture.Russian, "'Создание мощного оружия'");
			AddTranslation(text);
			
			text = CreateTranslation("ThanksBonebardier");
            text.SetDefault("Ohh, would you look at this beauty! Glad you helped me out! ... Actually, I don't think I'll ever use this gun so guess you can take it as your reward.");
            text.AddTranslation(GameCulture.Chinese, "哇哦，这家伙看起来真棒！很高兴你帮了我！…其实，我并不知道如何使用这把枪，所以你可以把它当作你的奖励。");
            text.AddTranslation(GameCulture.Russian, "Охх, только взгляни на эту красоту! Рад, что ты помог мне! ... На самом деле, я не думаю, что я когда-нибудь буду использовать эту пушку, так что я думаю, что ты можешь взять её в качестве своей награды.");
			AddTranslation(text);
			
			text = CreateTranslation("Quest13");
            text.SetDefault("Making wings is a really hard process! You need a strong material so the wings won't tear apart when you're flying. I think pieces of demon wings should be suitable. Please, bring me 12 pieces so I can make good wings!");
            text.AddTranslation(GameCulture.Chinese, "制造翅膀的过程是非常困难的！你需要一个强大的材料制作它，这样在你飞行时翅膀才不会断裂。我想恶魔翅膀的碎片应该是合适的，请给我12块碎片让我做一个不错的翅膀！");
            text.AddTranslation(GameCulture.Russian, "Создание крыльев это тяжелый процесс! Нужно подобрать такой крепкий материал, чтобы крылья не порвались при полёте. Я думаю, что части крыльев демона подойдут. Пожалуйста, принеси мне 12 частей, чтобы я смог сделать хорошие крылья!");
			AddTranslation(text);
			
			text = CreateTranslation("Name13");
			text.SetDefault("'How to make wings'");
			text.AddTranslation(GameCulture.Chinese, "“想入飞飞”");
			text.AddTranslation(GameCulture.Russian, "'Как создать крылья'");
			AddTranslation(text);
			
			text = CreateTranslation("Quest14");
            text.SetDefault("Where's my chest!? Please, don't tell me that I've lost it... There were so many useful things in it! Wait a second... I didn't lose it! It was a shark who attacked my boat! Yeah, right, it ate my chest! Please, kill some sharks until you find the one who ate my chest and then bring the chest back to me!");
            text.AddTranslation(GameCulture.Chinese, "我的箱子在哪里？别告诉我我把它弄丢了！里面有这么多有用的东西…等等…我没有丢！是一条鲨鱼袭击了我的船后吞下了它。你能在海里杀掉一些鲨鱼，以找到那个吞下我的箱子的那条吗？然后把箱子还给我！");
            text.AddTranslation(GameCulture.Russian, "Где мой сундучок!? Пожалуйста, не говорите, что я потерял его... В нём было столько полезных вещей! Секундочку... Я не потерял его! Это всё акула, которая напала на мой корабль! Да, всё верно, именно она съела мой сундучок! Пожалуйста, убей немного акул, пока не найдешь ту, которая съела мой сундучок и принеси его мне обратно!");
			AddTranslation(text);
			
			text = CreateTranslation("Name14");
			text.SetDefault("'In a shark's stomach'");
			text.AddTranslation(GameCulture.Chinese, "“在鲨鱼的肚子里”");
			text.AddTranslation(GameCulture.Russian, "'В желудке акулы'");
			AddTranslation(text);
			
			text = CreateTranslation("ThanksChest");
            text.SetDefault("Oh, I can't find words to thank you! You really helped me out! Here, take this book from my chest. Somebody gave it to me long time ago and I don't think I'll use it.");
            text.AddTranslation(GameCulture.Chinese, "哇哦…我想我已经找不到能感谢你的话了！你真的帮了我个大忙！来，从我的箱子里把这本书拿走吧。很久以前有人给我的，我想我不会用它。");
            text.AddTranslation(GameCulture.Russian, "Ох, я не могу найти слов, чтобы отблагодарить тебя! Ты очень сильно помог мне! Вот, возьми эту книгу из сундучка. Кто-то дал её мне давным-давно, и я не думаю, что буду её использовать.");
			AddTranslation(text);	
			
			text = CreateTranslation("Quest15");
            text.SetDefault("I have tried different food during my life but I've never eaten a coconut! It may sound oddly but it's true. I know that there're some palms growing near the ocean and I've even seen coconuts on them! The problem is that I'm too short to get them. Please, bring me 16 coconuts and then I can find out if coconuts are that tasty as many people say!");
            text.AddTranslation(GameCulture.Chinese, "我一生中品尝过诸多食物，但我仍然没有吃过椰子！听起来很奇怪，但这是真的。我知道海边生长着一些棕榈树，然后看到上面长了很多的椰子！问题是我太矮了，而且我没有斧子所以拿不到。能给我16个椰子吗？我想知道椰子是不是像许多人说的那样美味！");
            text.AddTranslation(GameCulture.Russian, "В течение своей жизни я пробовал разную еду, но я никогда не ел кокос! Это звучит странно, но это так. Я знаю, что рядом с океаном растёт несколько пальм и я даже видел на них кокосы! Проблема в том, что я не настолько высокий чтобы достать их. Пожалуйста, принеси мне 16 кокосов, чтобы я смог понять, действительно ли они такие вкусные, как многие говорят!");
			AddTranslation(text);
			
			text = CreateTranslation("Name15");
			text.SetDefault("'Delicious food'");
			text.AddTranslation(GameCulture.Chinese, "“美味佳肴”");
			text.AddTranslation(GameCulture.Russian, "'Изысканная еда'");
			AddTranslation(text);			

			text = CreateTranslation("Quest16");
            text.SetDefault("I'm currently trying to make a potion that will allow one to climb on walls. A potion like this would be very useful for my adventures! The problem is that I need some spider samples to make it and I am... Afraid of spiders. Can you gather 12 spider masses for me? Just go to a spider nest, kill some baby creepers and then gather the mass.");
            text.AddTranslation(GameCulture.Chinese, "我正在尝试制作一种可以让人进行攀爬的药水。这样的药水对我的冒险而言非常有用！问题是我需要一些蜘蛛样本来制作，但是…我怕蜘蛛…你能帮我收集12个蜘蛛分泌物吗？只需要去蜘蛛洞杀掉一些爬行者幼体来采集分泌物。");
            text.AddTranslation(GameCulture.Russian, "Сейчас я пытаюсь сделать зелье, которое позволит ползать по стенам. Такое зелье было бы очень полезным для моих приключений! Но проблема в том, что мне нужно немного образцов пауков, чтобы сделать его, а я... Боюсь пауков. Можешь ли ты собрать 12 паучих масс для меня? Просто иди в гнездо пауков, убей немного маленьких паучков и затем собери массу.");
			AddTranslation(text);
			
			text = CreateTranslation("Name16");
			text.SetDefault("'Arachnophobia'");
			text.AddTranslation(GameCulture.Chinese, "“蜘蛛恐惧症”");
			text.AddTranslation(GameCulture.Russian, "'Арахнофобия'");
			AddTranslation(text);

			text = CreateTranslation("Quest17");
            text.SetDefault("I'm really-really upset right now! Wanna know what happened? I was making a presents for my friends. When I've made 20 of them, a monster whose name is Krampus came and stole the presents! I really don't want my friends to be left without presents from me this year! Please, find that monster and bring my presents back!");
            text.AddTranslation(GameCulture.Chinese, "我现在真的很难过！想知道发生了什么事了吗？我在给朋友做礼物，当我做了第20件时，一个叫 Krampus 的怪物把礼物全偷走了！我真的不想让我的朋友今年不给我送礼物！请找到那个怪物，把我的礼物夺回来！");
            text.AddTranslation(GameCulture.Russian, "Я очень-очень расстроен! Хочешь знать, что произошло? Я делал подарки для моих друзей. Когда я сделал 20, монстр, чьё имя Крампус, пришёл и украл подарки! Я очень не хочу, чтоб мои друзья остались без подарков от меня в этом году! Пожалуйста, найди этого монстра и верни мои подарки!");
			AddTranslation(text);		
			
			text = CreateTranslation("Name17");
			text.SetDefault("'Stolen Christmas'");
			text.AddTranslation(GameCulture.Chinese, "被盗的圣诞节");
			text.AddTranslation(GameCulture.Russian, "'Украденное Рожедство'");
			AddTranslation(text);

			text = CreateTranslation("Quest18");
            text.SetDefault("There're rumors about very strange slimes living deep in the caves. You probably wonder why strange, right? That's because those chunks of gel eat emeralds! That's why they're covered with emerald shards like with a shell. Bring me some of these shards and I'll create something. Now go!");
            text.AddTranslation(GameCulture.Chinese, "有传言说在地下深处生存着非常古怪的史莱姆。你肯定想知道它为什么古怪，对吧？那是因为这些凝胶居然吃翡翠！这就是为什么它们被像是翡翠的东西包裹住，给我点它们的碎片，我会做点有趣的东西，出发吧！");
            text.AddTranslation(GameCulture.Russian, "Ходят слухи об очень странных слизнях, живущих глубоко в пещерах. Ты наверное думаешь, почему о странных, да? Всё потому что эти куски геля едят изумруды! Именно поэтому они покрыты изумрудными осколками, как будто панцирем. Принеси мне немного этих осколков и я кое-что сделаю. Иди же!");
			AddTranslation(text);

			text = CreateTranslation("Name18");
			text.SetDefault("'Slimes that eat emeralds'");
			text.AddTranslation(GameCulture.Chinese, "“吃翡翠的史莱姆”");
			text.AddTranslation(GameCulture.Russian, "'Слизни, что едят изумруды'");
			AddTranslation(text);	

            text = CreateTranslation("Quest19");
            text.SetDefault("You've come to me to ask about these parts you've found? An old friend of mine once told me about an ancient artifact, and I'm pretty sure these are its parts, clock parts to be more precise. The secret of fixing this clock can be found in vampiric diaries. Slay some vampires, get the diary and I'm sure you'll be able to fix the clock!");
            text.AddTranslation(GameCulture.Russian, "Ты пришел спросить об этих частях, которые нашел? Мой старый друг однажды рассказал мне о древнем артефакте, и я уверен, что это его части, части часов, если быть точными. Секрет починки этих часов скрыт в вампирских дневниках. Убей несколько вампиров, заполучи дневник, и я уверен, ты починишь эти часы!");
            AddTranslation(text);

            text = CreateTranslation("Name19");
            text.SetDefault("'The Stairway to Heaven'");
            text.AddTranslation(GameCulture.Russian, "'Лестница на небеса'");
            AddTranslation(text);

            text = CreateTranslation("ThanksClock");
            text.SetDefault("Oh, so you've actually managed to fix the clock? To be honest, I was joking when I've told you to fix it, but you did great! It's a dangerous artifact, so remember that with big power comes big responsibility.");
            text.AddTranslation(GameCulture.Russian, "О, так ты и правда умудрился починить часы? Честно говоря, я шутил, когда говорил тебе починить их, но ты отлично справился! Это опасный артефакт, так что запомни, что с большой силой приходит большая ответственность.");
            AddTranslation(text);

            text = CreateTranslation("GuideClockHelp");
            text.SetDefault("What? You've found a strange diary and can't understand anything in it? You've found the right guy to help you with this problem! Let's see... Just give me a second... Oh, I think I got it! Looks like you need to take both this diary and clock parts to the sky, and the clock will be repaired. I don't believe in magic, but if it says so...");
            text.AddTranslation(GameCulture.Russian, "Что? Ты нашёл странный дневник и не понимаешь, что в нём написано? Ты нашёл правильного парня, который поможет тебе с этой проблемой! Дай-ка взглянуть... Секундочку... О, кажется я понял! Похоже, что тебе нужно отправиться и с дневником, и с частями часов на небо, и тогда часы восстановятся. Я не верю в магию, но раз тут так написано...");
            AddTranslation(text);

            text = CreateTranslation("Quest20");
            text.SetDefault("Did you know that if you cut down burnt trees you will get charcoal? I bet you did not. Well, since now you have this information, can you please get me 25 charcoal? I really need it to make some torches!");
            text.AddTranslation(GameCulture.Chinese, "你知道如果砍伐烧焦的树木会得到木炭吗？ 我赌五毛你肯定不知道。 那么，既然你了解到了这些，能给我25个木炭吗？ 我真的需要它来制作一些火把！");
            text.AddTranslation(GameCulture.Russian, "А ты знал, что если срубить сгоревшие дерева, то получишь древесный уголь? Спорю, что не знал. Что же, раз теперь ты владеешь такой информацией, можешь ли ты принести мне 25 древесного угля? Он очень нужен мне для создания факелов!");
            AddTranslation(text);

            text = CreateTranslation("Name20");
            text.SetDefault("'Hot to the touch'");
            text.AddTranslation(GameCulture.Chinese, "“触手可及”");
            text.AddTranslation(GameCulture.Russian, "'Горячий на ощупь");
            AddTranslation(text);

            text = CreateTranslation("ThanksShards");
            text.SetDefault("You've managed to get them? Amazing! These shards look like they're alive, they're probably got affected by those slimes impact. If I'll try using a magic enchantment on them, you will get a new familiar... It worked! Take it, it will light up your way wherever you go!");
            text.AddTranslation(GameCulture.Chinese, "你设法弄到了？太棒了！这些碎片看起来仍然活着，我断定是因为受到史莱姆的影响，如果我试着用魔法附魔，你会得到一个新的…它能用了！拿着它，无论到哪里，它都可以照亮你前进的路！");
            text.AddTranslation(GameCulture.Russian, "Ты добыл его? Невероятно! Эти осколки выглядят как живые, наверное, на них сказалось необычное взаимодействие от тех слизней. Если я попробую использовать на них кое-какое магическое зачарование, то у тебя появится новый питомец... Получилось! Держи, он будеть освещать тебе путь, куда бы ты не направился!");
			AddTranslation(text);			

            text = CreateTranslation("PirateChat");
            text.SetDefault("Yarr, I really need ya help to leave this appalin' place!");
            text.AddTranslation(GameCulture.Chinese, "啊！我真的需要你的帮助来离开这个鬼地方!");
            text.AddTranslation(GameCulture.Russian, "Йаррр, мне очень нужна твоя помощь чтобы покинуть это ужасное место!");
			AddTranslation(text);
			
			text = CreateTranslation("PirateQuest");
            text.SetDefault("Yarr! I need your help... I got robbed by dirty mongrels and they took away my magical gimmick, moreover they haven't left a drop of rum aboard. These mongrels were talking about three beasts, they want to break my amulet and feed them it's parts! Of course I'm a sailor but I also want visit the land but I can't do it without my amulet. Get this amulet and kill them all for me!");
            text.AddTranslation(GameCulture.Chinese, "啊！我需要你的帮助...我被三个大怪物抢劫了，它们抢走了我神奇的道具，而且没留下一滴朗姆酒。它们甚至打碎我的护身符，抢走了其它的碎片妄图充当食物！我是个水手，但是我也想游览陆地，不过我不能没有我的护身符。去帮我拿回护身符...等等，它们其中有一个是眼球，一个是一大块粘液，还有一个我想不起来了，总之，把这些怪物都宰了！");
            text.AddTranslation(GameCulture.Russian, "Йарр! Мне нужна твоя помощь... Меня обокрали гадкие черти и унесли мою магическую диковинку, да еще и ни капли рома на борту не оставили. Эти гады болтали о каких-то трех тварях, они хотят разбить мой амулет и скормить им его части! Я конечно моряк, но и на суше побывать охота, а без моего амулета я этого сделать не могу. Достань этот амулет и прикончи всех за меня!");
			AddTranslation(text);
			
			text = CreateTranslation("PirateThanks");
            text.SetDefault("Yarr, thankee so much! At last I can leave this world! Maybe we will meet again! Take this as my reward! This chest standin' next t' ye contains untold riches 'n I give them all jus' t' ye!");
            text.AddTranslation(GameCulture.Russian, "Йаррр, спасибо тебе! Наконец-то я могу покинуть этот мир! Быть может, мы встретимся вновь! Возьми это в качестве награды! Этот сундук, стоящий рядом с тобой, содержит несметные богатства, и я даю их всех тебе!");
			AddTranslation(text);
			
			text = CreateTranslation("BoundPirate");
            text.SetDefault("Thank ya for rescuin' me!");
            text.AddTranslation(GameCulture.Chinese, "谢谢你救了我！");
            text.AddTranslation(GameCulture.Russian, "Спасибо тебе за то, что спас меня!");
			AddTranslation(text);
			
			text = CreateTranslation("PirateCompleted");
            text.SetDefault("Ya already helped me and I appreciate it!");
            text.AddTranslation(GameCulture.Chinese, "你已经帮助了我，我感激不尽！");
            text.AddTranslation(GameCulture.Russian, "Ты уже помог мне и я ценю это!");
			AddTranslation(text);
			
			text = CreateTranslation("AmuletDeath");
            text.SetDefault("{0} was torn to pieces by dark forces...");
            text.AddTranslation(GameCulture.Chinese, "{0} 被黑暗力量撕成碎片...");
            text.AddTranslation(GameCulture.Russian, "{0} был разорван куски тёмными силами...");
			AddTranslation(text);
			
			text = CreateTranslation("AmuletDeathF");
            text.SetDefault("{0} was torn to pieces by dark forces...");
            text.AddTranslation(GameCulture.Chinese, "{0} 被黑暗力量撕成碎片...");
            text.AddTranslation(GameCulture.Russian, "{0} была разорвана куски тёмными силами...");
			AddTranslation(text);
			
            text = CreateTranslation("PirateBoatGen");
            text.SetDefault("The Pirate is sailing to the world...");
            text.AddTranslation(GameCulture.Chinese, "船长正在向世界航行...");
            text.AddTranslation(GameCulture.Russian, "Пират приплывает в мир...");
			AddTranslation(text);
			
			text = CreateTranslation("GuideHouseGen");
            text.SetDefault("Someone is building a house for the Guide...");
            text.AddTranslation(GameCulture.Chinese, "有人正在为向导修建房屋...");
            text.AddTranslation(GameCulture.Russian, "Кто-то строит дом Гиду...");
			AddTranslation(text);
			
			text = CreateTranslation("PyramideGen");
            text.SetDefault("Creating pyramids in the sand...");
            text.AddTranslation(GameCulture.Chinese, "在沙漠上创建金字塔...");
            text.AddTranslation(GameCulture.Russian, "Идёт создание пирамид в песках...");
			AddTranslation(text);

            text = CreateTranslation("SubmarineGen");
            text.SetDefault("A submarine sinks in the ocean...");
            text.AddTranslation(GameCulture.Chinese, "潜水艇在深海沉没...");
            text.AddTranslation(GameCulture.Russian, "Субмарина тонет в океане...");
			AddTranslation(text);

            text = CreateTranslation("EnchantedGen");
            text.SetDefault("Enchanted stones are appearing in caves...");
            text.AddTranslation(GameCulture.Chinese, "附魔石在洞穴缓慢生长...");
            text.AddTranslation(GameCulture.Russian, "В пещерах появляются зачарованные камни...");
			AddTranslation(text);

            text = CreateTranslation("CrystalGen");
            text.SetDefault("Nature crystals are appearing on the grass...");
            text.AddTranslation(GameCulture.Chinese, "自然水晶在草木之中生长...");
            text.AddTranslation(GameCulture.Russian, "На траве появляются кристаллы природы...");
			AddTranslation(text);

            text = CreateTranslation("CampGen");
            text.SetDefault("Robbers are making their camp...");
            text.AddTranslation(GameCulture.Chinese, "土匪们正在建造营地...");
            text.AddTranslation(GameCulture.Russian, "Разбойники создают свой лагерь...");
			AddTranslation(text);
			
			text = CreateTranslation("TowerGen");
            text.SetDefault("A strange tower appears in the dangerous forests...");
            text.AddTranslation(GameCulture.Chinese, "诡异的石塔出现于危机四伏的森林...");
            text.AddTranslation(GameCulture.Russian, "Странная башня появляется в опасных лесах...");
			AddTranslation(text);
			
			text = CreateTranslation("FortressGen");
            text.SetDefault("Something ancient and forbidden appears underground...");
            text.AddTranslation(GameCulture.Chinese, "某些古老与禁忌的事物出现在地下...");
            text.AddTranslation(GameCulture.Russian, "Что-то древнее и запретное появляется в подземельях...");
			AddTranslation(text);

            text = CreateTranslation("QuestKilled");
            text.SetDefault("\n \nKilled: ");
            text.AddTranslation(GameCulture.Chinese, "\n \n已击杀：");
            text.AddTranslation(GameCulture.Russian, "\n \nУбито: ");
			AddTranslation(text);
			
			text = CreateTranslation("QuestKilled2");
            text.SetDefault(" out of ");
            text.AddTranslation(GameCulture.Chinese, " 需要击杀：");
            text.AddTranslation(GameCulture.Russian, " из ");
			AddTranslation(text);

            text = CreateTranslation("IronCoin");
            text.SetDefault("iron coin");
            text.AddTranslation(GameCulture.Chinese, "铁币");
            text.AddTranslation(GameCulture.Russian, "мон. железа");
			AddTranslation(text);
			
			text = CreateTranslation("Mirror1");
            text.SetDefault("You look in the mirror. You see your reflection but there's also something moving behind you.");
            text.AddTranslation(GameCulture.Chinese, "你凝视着镜子，看着另一个自己。但是“自己”的身后似乎有什么东西。");
            text.AddTranslation(GameCulture.Russian, "Вы смотрите в зеркало. Вы видите своё отражение, но ещё позади вас что-то движется.");
			AddTranslation(text);
			
			text = CreateTranslation("Mirror3");
            text.SetDefault("The mirror seems to be broken.");
            text.AddTranslation(GameCulture.Chinese, "魔镜已经碎了");
            text.AddTranslation(GameCulture.Russian, "Похоже, что зеркало разбито.");
			AddTranslation(text);
			
			text = CreateTranslation("CurrentQuest");
			text.SetDefault("Current Quest");
            text.AddTranslation(GameCulture.Chinese, "当前任务");
			text.AddTranslation(GameCulture.Russian, "Текущий квест");
			AddTranslation(text);
			
			text = CreateTranslation("TurnIn");
            text.SetDefault("Ready for turn-in");
            text.AddTranslation(GameCulture.Chinese, "任务完成");
            text.AddTranslation(GameCulture.Russian, "Можно сдать");
			AddTranslation(text);
			
			text = CreateTranslation("Information1");
			text.SetDefault("Thank you for playing with Antiaris!");
			text.AddTranslation(GameCulture.Chinese, "感谢你游玩Antiaris！我们的QQ官方群号码:669341455!");
			text.AddTranslation(GameCulture.Russian, "Спасибо, что играете с Antiaris!");
			AddTranslation(text);

			text = CreateTranslation("TimeStop1");
            text.SetDefault("<{0}> Time, stop!");
            text.AddTranslation(GameCulture.Chinese, "<{0}> 时停！");
            text.AddTranslation(GameCulture.Russian, "<{0}> Время, остановись!");
            AddTranslation(text);
			
			text = CreateTranslation("TimeStop2");
            text.SetDefault("<{0}> Time has resumed.");
            text.AddTranslation(GameCulture.Chinese, "<{0}> 时间恢复运转");
            text.AddTranslation(GameCulture.Russian, "<{0}> Время возобновило свой ход.");
            AddTranslation(text);
			
			text = CreateTranslation("PixieLampCollect");
            text.SetDefault("Collect");
            text.AddTranslation(GameCulture.Chinese, "收集");
            text.AddTranslation(GameCulture.Russian, "Собрать");
            AddTranslation(text);
			
			text = CreateTranslation("PixieLamp");
            text.SetDefault("Pixie lamp is slowly floating in the air, attracting pixies with it's look.");
            text.AddTranslation(GameCulture.Chinese, "精灵之灯在空中缓缓飘动，看样子吸引了诸多精灵");
            text.AddTranslation(GameCulture.Russian, "Лампа пикси медленно парит в воздухе, привлекая пикси своим видом.");
            AddTranslation(text);
			
			text = CreateTranslation("BitesTheDust");
			text.SetDefault("{0} bites the dust...");
			text.AddTranslation(GameCulture.Chinese, "{0} 尘埃落定...");
			text.AddTranslation(GameCulture.Russian, "{0} глотает пыль...");
			AddTranslation(text);

			text = CreateTranslation("SnowHouseGen");
			text.SetDefault("A cozy house appears in snow-capped lands...");
			text.AddTranslation(GameCulture.Russian, "Уютный домик появляется в заснеженных землях...");
			text.AddTranslation(GameCulture.Chinese, "一个舒适的房子出现在积雪覆盖的土地上…");
            AddTranslation(text);
			
			text = CreateTranslation("InjuredDeath");
            text.SetDefault("{0} couldn't stop the bleeding.");
            text.AddTranslation(GameCulture.Russian, "{0} не смог остановить кровотечение.");
            text.AddTranslation(GameCulture.Chinese, "{0} 无法止住流血...");
            AddTranslation(text);

            text = CreateTranslation("AdventurerSaid");
            text.SetDefault("I've written down\nAdventurer's words, he said: \n");
            text.AddTranslation(GameCulture.Russian, "Я записал слова\nПутешественника, он сказал: \n");
            text.AddTranslation(GameCulture.Chinese, "我记下了冒险家的话，他说：\n");
            AddTranslation(text);

            text = CreateTranslation("AdventurerHelp");
            text.SetDefault("\n\nI need to help him if I want to get a reward.");
            text.AddTranslation(GameCulture.Russian, "\n\nЯ должен помочь ему, если хочу получить награду.");
            text.AddTranslation(GameCulture.Chinese, "\n\n看来如果我想得到报酬，我需要帮助他。");
            AddTranslation(text);

            text = CreateTranslation("NoTask");
            text.SetDefault("I don't have any tasks from Adventurer.\nMaybe I should ask him if he has one for me?");
            text.AddTranslation(GameCulture.Russian, "У меня нету никаких заданий от\nПутешественника. Может мне стоит спросить его, есть ли у него какое-нибудь задание для меня?");
            text.AddTranslation(GameCulture.Chinese, "我目前没有冒险家的任何任务\n也许我应该问他是否有一个能够给我？");
            AddTranslation(text);

            text = CreateTranslation("TrackerButton");
            text.SetDefault("You can move the tracker by holding it.\nPress this button to get full quest description.\nPress {0} to open/close the tracker.");
            text.AddTranslation(GameCulture.Russian, "Вы можете передвигать трэкер, держа его.\nНажмите эту кнопку, чтобы получить полное описание квеста.\nPress {0}, чтобы открыть/закрыть трэкер.");
            text.AddTranslation(GameCulture.Chinese, "你可以按住任务追踪器来拖动它\n点击这个按钮以获得完整的任务描述\n点击 {0} 打开/关闭任务追踪器");
            AddTranslation(text);

            text = CreateTranslation("TrackerButton1");
            text.SetDefault("You can move the tracker by holding it.\nPress this button to get full quest description.\nPress ");
            text.AddTranslation(GameCulture.Russian, "Вы можете передвигать трэкер, держа его.\nНажмите эту кнопку, чтобы получить полное описание квеста.\nНажмите ");
            text.AddTranslation(GameCulture.Chinese, "你可以按住任务追踪器来拖动它\n点击这个按钮以获得完整的任务描述\n点击 ");
            AddTranslation(text);

            text = CreateTranslation("TrackerButton2");
            text.SetDefault(" to open/close the tracker.");
            text.AddTranslation(GameCulture.Russian, ", чтобы открыть/закрыть трэкер");
            text.AddTranslation(GameCulture.Chinese, " 打开/关闭追踪器");
            AddTranslation(text);

            text = CreateTranslation("TrackerNoQuest1");
            text.SetDefault("You have no quest active!");
            text.AddTranslation(GameCulture.Russian, "У вас нету активного квеста!");
            text.AddTranslation(GameCulture.Chinese, "你目前没有任务！");
            AddTranslation(text);

            text = CreateTranslation("TrackerNoQuest2");
            text.SetDefault("");
            text.AddTranslation(GameCulture.Russian, "");
            text.AddTranslation(GameCulture.Chinese, "");
            AddTranslation(text);

            text = CreateTranslation("TrackerNoQuest3");
            text.SetDefault("");
            text.AddTranslation(GameCulture.Russian, "");
            text.AddTranslation(GameCulture.Chinese, "");
            AddTranslation(text);

            text = CreateTranslation("LifeCrystalCanUse");
            text.SetDefault("[c/E5000B:Amount of Life Crystals you can use: {0}]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Количество Кристаллов жизни, которых вы можете использовать: {0}]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你可以使用的生命水晶数量 {0}]");
            AddTranslation(text);

            text = CreateTranslation("LifeCrystalNoUse");
            text.SetDefault("[c/E5000B:You've reached the limit of using Life Crystals!]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Вы достигли лимит по использованию Кристаллов жизни!]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你已经达到使用生命水晶的最大上限了！]");
            AddTranslation(text);

            text = CreateTranslation("LifeCrystalNoUse2");
            text.SetDefault("[c/E5000B:In order to increase maximum amount of health, find Blazing Hearts in the Underworld.]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Для того, чтобы увеличить максимальное количество жизней, найдите Пылающие сердца в Аду.]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:如果你想继续增加最大体力值，请到地狱寻找燃烧之心]");
            AddTranslation(text);

            text = CreateTranslation("BlazingHeartCanUse");
            text.SetDefault("[c/E5000B:Amount of Blazing Hearts you can use: {0}]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Количество Пылающих сердец, которых вы можете использовать: {0}]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你可以使用的燃烧之心数量 {0}]");
            AddTranslation(text);

            text = CreateTranslation("BlazingHeartCantUse");
            text.SetDefault("[c/E5000B:You can't use Blazing Hearts until you reach 300 health!]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Вы не можете использовать Пылающие сердца, пока не достигнете 300 здоровья!]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你不能够在最大体力值到 300 之前使用燃烧之心！]");
            AddTranslation(text);

            text = CreateTranslation("BlazingHeartNoUse");
            text.SetDefault("[c/E5000B:You've reached the limit of using Blazing Hearts!]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Вы достигли лимит по использованию Пылающих сердец!]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你已经达到使用燃烧之心的最大上限了！]");
            AddTranslation(text);

            text = CreateTranslation("BlazingHeartNoUse2");
            text.SetDefault("[c/E5000B:In order to increase maximum amount of health, find Dazzling Hearts in the Underground Hallow.]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Для того, чтобы увеличить максимальное количество жизней, найдите Сияющие сердца в подземном Святом биоме.]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:如果你想继续增加最大体力值，请到神圣之地的地下寻找璀璨之心]");
            AddTranslation(text);

            text = CreateTranslation("DazzlingHeartCanUse");
            text.SetDefault("[c/E5000B:Amount of Dazzling Hearts you can use: {0}]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Количество Сияющих сердец, которых вы можете использовать: {0}]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你可以使用的璀璨之心数量 {0}]");
            AddTranslation(text);

            text = CreateTranslation("DazzlingHeartCantUse");
            text.SetDefault("[c/E5000B:You can't use Dazzling Hearts until you reach 400 health!]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Вы не можете использовать Сияющие сердца, пока не достигнете 400 здоровья!]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你不能够在最大体力值到 400 之前使用璀璨之心！]");
            AddTranslation(text);

            text = CreateTranslation("DazzlingHeartNoUse");
            text.SetDefault("[c/E5000B:You've reached the limit of using Dazzling Hearts!]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Вы достигли лимит по использованию Сияющих сердец!]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你已经达到使用璀璨之心的最大上限了！]");
            AddTranslation(text);

            text = CreateTranslation("DazzlingHeartNoUse2");
            text.SetDefault("[c/E5000B:In order to increase maximum amount of health, find Life Fruits in the Underground Jungle after you defeat any mechanical boss.]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Для того, чтобы увеличить максимальное количество жизней, найдите Фрукты жизни в подземных Джунглях после победы над любым механическим боссом.]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:如果你想继续增加最大体力值，请在击败所有机械Boss后进入丛林地下寻找生命果]");
            AddTranslation(text);

            text = CreateTranslation("LifeFruitCanUse");
            text.SetDefault("[c/E5000B:Amount of Life Fruits you can use: {0}]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Количество Фруктов жизни, которых вы можете использовать: {0}]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你可以使用的生命果数量 {0}]");
            AddTranslation(text);

            text = CreateTranslation("LifeFruitCantUse");
            text.SetDefault("[c/E5000B:You can't use Life Fruits until you reach 450 health!]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Вы не можете использовать Фрукты жизни, пока не достигнете 450 здоровья!]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你不能够在最大体力值到 450 之前使用生命果！]");
            AddTranslation(text);

            text = CreateTranslation("LifeFruitNoUse");
            text.SetDefault("[c/E5000B:You've reached the limit of using Life Fruits!]");
            text.AddTranslation(GameCulture.Russian, "[c/E5000B:Вы достигли лимит по использованию Фруктов жизни!]");
            text.AddTranslation(GameCulture.Chinese, "[c/E5000B:你已经达到使用生命果的最大上限了！]");
            AddTranslation(text);

            text = CreateTranslation("RodBroken");
            text.SetDefault("<{0}> Uh-oh... I hope the Adventurer will not notice that I've broken his fishing rod again...");
            text.AddTranslation(GameCulture.Russian, "<{0}> Ой-ой... Я надеюсь, Путешественник не заметит, что я вновь сломал его удочку...");
            text.AddTranslation(GameCulture.Chinese, "<{0}> 啊哦…我希望冒险家不会注意到我又弄坏了他的鱼竿...");
            AddTranslation(text);

            text = CreateTranslation("ThanksRod1");
            text.SetDefault("Thank you for your help! I really appreciate it!");
            text.AddTranslation(GameCulture.Chinese, "十分感谢，我真的感激不尽！");
            text.AddTranslation(GameCulture.Russian, "Спасибо за твою помощь! Я очень ценю это!");
            AddTranslation(text);

            text = CreateTranslation("ThanksRod2");
            text.SetDefault("Great! Thank your for your he-... Wait... What are these strange marks?.. Oh, now I get it. Have you really tried to fish with my rod and it broke again!? Did I allow you to use it? Go away, you will not get any rewards.");
            text.AddTranslation(GameCulture.Russian, "Отлично! Спасибо тебе за по-.. Погоди-ка... Что это за странные следы?.. А, теперь мне всё ясно. Ты что, правда решил порыбачить моей удочкой и она опять сломалась!? Я тебе вообще разрешал использовать её? Уходи, не получишь ты никаких наград.");
            text.AddTranslation(GameCulture.Chinese, "漂亮！谢谢你的…等等，这些奇怪的痕迹是什么？我猜，我现在明白了。你真的试着使用它并且又弄坏了！？我让你用它了吗？滚开，我不会给你任何报酬。");
            AddTranslation(text);

            text = CreateTranslation("HarpyEggBroken");
            text.SetDefault("<{0}> Great, the egg got destroyed! Now I have to find another one.");
            text.AddTranslation(GameCulture.Russian, "<{0}> Отлично, яйцо было уничтожено! Теперь мне придется искать ещё одно.");
            text.AddTranslation(GameCulture.Chinese, "<{0}> 真是漂亮...蛋已经碎了！现在我必须要找到下一个。");
            AddTranslation(text);

            text = CreateTranslation("HarpyEggDeath");
            text.SetDefault("Baby Egg was obliterated...");
            text.AddTranslation(GameCulture.Russian, "Маленькое Яйцо было уничтожено...");
            text.AddTranslation(GameCulture.Chinese, "蛋被扼杀了...");
            AddTranslation(text);

            text = CreateTranslation("Information2");
            text.SetDefault("It looks like your world doesn't have the Pirate's Cove structure! If you don't want to create a new world, you can use '/generateCove' command to generate it in this world.");
            text.AddTranslation(GameCulture.Russian, "Похоже, что в вашем мире нету структуры Пиратская бухта! Если вы не хотите создавтаь новый мир, то вы можете использовать команду '/generateCove', чтобы создать её в этом мире.");
            text.AddTranslation(GameCulture.Chinese, "看来你的世界没有海盗湾！如果你不想创建一个新世界，可以输入命令“/generateCove”在这个世界创建它！");
            AddTranslation(text);

            text = CreateTranslation("PirateCove");
            text.SetDefault("The Pirate's Cove");
            text.AddTranslation(GameCulture.Russian, "Пиратская бухта");
            text.AddTranslation(GameCulture.Chinese, "海盗湾");
            AddTranslation(text);

            text = CreateTranslation("PirateCoveGen");
            text.SetDefault("Pirates are hiding their treasures...");
            text.AddTranslation(GameCulture.Russian, "Пираты прячут свои сокровища...");
            text.AddTranslation(GameCulture.Chinese, "海盗们藏匿着他们的财宝…");
            AddTranslation(text);

            text = CreateTranslation("PirateCoveCommand1");
            text.SetDefault("You've used dark magic, and the Pirate's Cove has appeared!");
            text.AddTranslation(GameCulture.Russian, "Вы использовали тёмную магию, и Пиратская бухта появилась!");
            text.AddTranslation(GameCulture.Chinese, "你使用了黑魔法，使得海盗湾出现了！");
            AddTranslation(text);

            text = CreateTranslation("PirateCoveCommand2");
            text.SetDefault("There's already a Pirate's Cove in this world!");
            text.AddTranslation(GameCulture.Russian, "В этом мире уже есть Пиратская бухта!");
            text.AddTranslation(GameCulture.Chinese, "海盗湾已经出现在世界上了！");
            AddTranslation(text);
			
			text = CreateTranslation("PirateCoveCommand3");
            text.SetDefault("Due to bugs, you can only use this command in singleplayer.");
            text.AddTranslation(GameCulture.Russian, "Из-за багов, вы можете использовать эту команду только в одиночной игре.");
            text.AddTranslation(GameCulture.Chinese, "因为错误，你只能在单人游戏下使用此命令。");
            AddTranslation(text);

            text = CreateTranslation("GuideChat");
            text.SetDefault("Please, help me! I really need your help to continue fulfilling my duty!");
            text.AddTranslation(GameCulture.Russian, "Пожалуйста, помоги мне! Мне правда нужна твоя помощь, чтобы продолжить выполнять свой долг!");
            text.AddTranslation(GameCulture.Chinese, "请帮帮我！我真的需要你的帮助来继续履行我的职责！");
            AddTranslation(text);

            text = CreateTranslation("GuideQuest");
            text.SetDefault("When zombies burst into my house, they stole my precious book, 'The Guide for Guides!' I'm really ashamed to say this, but... I can't do anything without it! The book tells me what to do, and without it, I can't call myself a Guide! I beg of you, find the zombies that stole my book and return it to me!");
            text.AddTranslation(GameCulture.Russian, "Когда зомби ворвались в мой дом, они украли мою прелестную книгу 'Руководство для Гидов'! Мне очень стыдно признать это, но... Я не могу ничего делать без неё! Эта книга рассказывает, что мне надо делать, и без неё я не могу называть себя Гидом! Умоляю, перебей несколько зомби и верни мне книгу!");
            text.AddTranslation(GameCulture.Chinese, "当僵尸闯入我的房子时，他们偷走了我珍贵的《向导指南》。我真的很尴尬，但是…没有它我什么都做不了！这本书告诉我该怎么合成物品，怎么指引玩家，没有它，我不能称自己为向导！我恳求你，找到那些把我的书偷走的僵尸并把书还给我！");
            AddTranslation(text);

            text = CreateTranslation("GuideThanks");
            text.SetDefault("Thank you! THANK YOU! That's just what I needed to continue my job as a guide! Now let's see what should I do... Let's see, page 102... Oh! I need to help you and give you advices! That's what I'll do to thank you.");
            text.AddTranslation(GameCulture.Russian, "Спасибо! СПАСИБО! Это как раз то, что мне нужно для продолжения работы в качестве гида! Посмотри, что мне нужно делать... Так, страница 102... О! Мне нужно помогать тебе и давать советы. Это я и буду делать, чтобы отблагодарить тебя.");
            text.AddTranslation(GameCulture.Chinese, "谢谢你！谢 谢 你！这正是我继续做向导所需要的！现在让我们看看我该怎么办…让我们看看，第102页…哦！我需要帮助你，给你建议！这就是我要感谢你的原因。");
            AddTranslation(text);

            text = CreateTranslation("Quest");
            text.SetDefault("Quest");
            text.AddTranslation(GameCulture.Russian, "Квест");
            text.AddTranslation(GameCulture.Chinese, "任务");
            AddTranslation(text);

            text = CreateTranslation("Mechanic1");
            text.SetDefault("My name is {0} and I sell nixie tubes... and nixie tubes accessories!");
            text.AddTranslation(GameCulture.Russian, "Моё имя {0} и я продаю газоразрядные индикаторы... и принадлежности для газоразрядных индикаторов!");
            text.AddTranslation(GameCulture.Chinese, "我叫做 {0}，我卖数码管…还有一些数码管配件！");
            AddTranslation(text);

            text = CreateTranslation("GuideWerewolf");
            text.SetDefault("According to my book, werewolves are afraid of silver. Don't make me try this out!");
            text.AddTranslation(GameCulture.Russian, "Судя по моей книге, оборотни боятся серебра. Не заставляй меня проверять это!");
            text.AddTranslation(GameCulture.Chinese, "根据我的书上记载，狼人害怕白银，别逼我试试这个！");
            AddTranslation(text);

            text = CreateTranslation("MerchantWerewolf");
            text.SetDefault("Sorry, I don't sell razors, but you really need to have a shave...");
            text.AddTranslation(GameCulture.Russian, "Извини, я не продаю бритвы, но тебе действительно нужно побриться...");
            text.AddTranslation(GameCulture.Chinese, "对不起，我这里不卖剃须刀，但你真的需要刮刮胡子…");
            AddTranslation(text);

            text = CreateTranslation("NurseWerewolf");
            text.SetDefault("I can't fix your current state. Don't even ask.");
            text.AddTranslation(GameCulture.Russian, "Я не могу исправить твое текущее состояние. Даже не спрашивай.");
            text.AddTranslation(GameCulture.Chinese, "我无法修复你当前的状态，不要再问了。");
            AddTranslation(text);

            text = CreateTranslation("DemolitionistWerewolf");
            text.SetDefault("I only give my precious explosives into good hands. As you can see, you have paws.");
            text.AddTranslation(GameCulture.Russian, "Я отдаю свою взрывчатку только в хорошие руки. А у тебя, как видишь, лапы.");
            text.AddTranslation(GameCulture.Chinese, "我只把我的宝贝炸药交给一个好手。如你所见，你有爪子。");
            AddTranslation(text);

            text = CreateTranslation("DyeTraderWerewolf");
            text.SetDefault("You sure it's safe to color your fur with regular dyes? Consider buying actual hair dye, even I'm not sure it would work on your fur.");
            text.AddTranslation(GameCulture.Russian, "Ты уверен, что безопасно красить твою шерсть обычной краской? Подумай о покупке краски для волос, хотя я не уверен, что она сработает на твою шерсть.");
            text.AddTranslation(GameCulture.Chinese, "你确定用普通染料给你的皮毛上色是安全的吗？你可以考虑购买真正的染发剂，即使我不确定它是否会对你的皮毛起到作用。");
            AddTranslation(text);

            text = CreateTranslation("DryadWerewolf");
            text.SetDefault("Don't try it! I'll use my special power... 「PURIFICATION POWDER」 ! Wait, where did I leave it...");
            text.AddTranslation(GameCulture.Russian, "Даже не думай! Я буду использовать свою специальную силу... 「ОЧИЩАЮЩИЙ ПОРОШОК」 ! Минутку, куда я его положила...");
            text.AddTranslation(GameCulture.Chinese, "不要尝试这个东西！我会用我的特殊力量…「净化粉末」！等等，我把它放在哪儿了…");
            AddTranslation(text);

            text = CreateTranslation("TavernkeepWerewolf");
            text.SetDefault("Never seen a creature like you in my world. Do you drink ale?");
            text.AddTranslation(GameCulture.Russian, "Никогда не видел такого существа в своем мире. Ты пьёшь эль?");
            text.AddTranslation(GameCulture.Chinese, "在我的世界里从未见过像你这样的生物。你喝麦芽酒吗？");
            AddTranslation(text);

            text = CreateTranslation("ArmsDealerWerewolf");
            text.SetDefault("A werewolf, huh? Are you here to buy some silver bullets?");
            text.AddTranslation(GameCulture.Russian, "Оборотень, ха? Ты здесь, чтобы купить серебрянные пули?");
            text.AddTranslation(GameCulture.Chinese, "一个狼人，对吧？ 你想在这里买一些银弹吗？");
            AddTranslation(text);

            text = CreateTranslation("StylistWerewolf");
            text.SetDefault("Never done grooming before! Well, everything happens for the first time, right?");
            text.AddTranslation(GameCulture.Russian, "Никогда раньше не занималась грумингом! Ну, все случается в первый раз, не так ли?");
            text.AddTranslation(GameCulture.Chinese, "从来没有做过梳理！ 好吧，一切都是第一次发生，对吗？");
            AddTranslation(text);

            text = CreateTranslation("PainterWerewolf");
            text.SetDefault("оSorry, I don't have a painting with a werewolf on it! But... I suppose I can make one!");
            text.AddTranslation(GameCulture.Russian, "Извини, у меня нету картины с обротнем! Но... Полагаю, что я могу нарисовать такую!");
            text.AddTranslation(GameCulture.Chinese, "哦吼抱歉，我没有带狼人的画！ 但是......我想我可以制作一个！");
            AddTranslation(text);

            text = CreateTranslation("AnglerWerewolf");
            text.SetDefault("You know the similarity between a wolf and a human? Right! They both go fishing! So get me another fish.");
            text.AddTranslation(GameCulture.Russian, "Ты знаешь, чем схожи человек и волк? Правильно! Они оба ходят на рыбалку! Поэтому иди и принеси мне еще одну рыбу.");
            text.AddTranslation(GameCulture.Chinese, "你知道狼和人之间的相似之处吗？没错！他们都钓鱼了！所以滚去给我钓另一条鱼。");
            AddTranslation(text);

            text = CreateTranslation("GoblinTinkererWerewolf");
            text.SetDefault("It feels nice to meet someone, who's not a human like me! Hope you'll not eat me.");
            text.AddTranslation(GameCulture.Russian, "Приятно встретить кого-то, кто тоже не человек, как и я! Надеюсь, ты меня не съешь.");
            text.AddTranslation(GameCulture.Chinese, "很高兴认识一个不像我这样的人！希望你不要吃我。");
            AddTranslation(text);

            text = CreateTranslation("WitchDoctorWerewolf");
            text.SetDefault("Are you some kind of a guardian spirit? Just asking, because I've seen a pink cat spirit before, thought you are somehow related.");
            text.AddTranslation(GameCulture.Russian, "Ты какой-то дух-защитник? Просто спрашиваю, потому что когда-то я видел духа розового кота, подумал, что вы как-то связаны.");
            text.AddTranslation(GameCulture.Chinese, "你是某种守护之魂吗？只是问一下，因为我以前见过粉红猫之魂，以为你有某种关联。");
            AddTranslation(text);

            text = CreateTranslation("ClothierWerewolf");
            text.SetDefault("Please, don't bite me because of this scarf made of wolf fur! I don't want to transform into monster again!");
            text.AddTranslation(GameCulture.Russian, "Пожалуйста, не кусай меня за то, что этот шарф сделан из волчьей шерсти! Я не хочу опять превратиться в монстра!");
            text.AddTranslation(GameCulture.Chinese, "请不要因为我这条用毛皮制成的围巾而咬我！ 我不想再变成怪物了！");
            AddTranslation(text);

            text = CreateTranslation("MechanicWerewolf");
            text.SetDefault("Didn't know wolves were into mechanisms! Know any other creatures that might like my stuff?");
            text.AddTranslation(GameCulture.Russian, "Не знала, что волкам нравятся механизмы! Знаешь еще каких-нибудь существ, которым могли бы понравиться мои штуки?");
            text.AddTranslation(GameCulture.Chinese, "没想到狼群进入了机械装置！你知道还有其他生物会喜欢我的东西吗？");
            AddTranslation(text);

            text = CreateTranslation("PartyGirlWerewolf");
            text.SetDefault("Oh, a doggie! Who's a good boy? Who's a gooood boooy?");
            text.AddTranslation(GameCulture.Russian, "Ой, пёсик! Кто хороший мальчик? Кто хорооошиий маальчиик?");
            text.AddTranslation(GameCulture.Chinese, "哦，一条小狗！谁是个好孩子？谁是个好孩——子——？");
            AddTranslation(text);

            text = CreateTranslation("WizardWerewolf");
            text.SetDefault("Were you transformed into a wolf by an evil witch? I might be able to remove your curse!");
            text.AddTranslation(GameCulture.Russian, "Тебя превратила в волка злая ведьма? Я могу попробовать помочь снять заклятие!");
            text.AddTranslation(GameCulture.Chinese, "你被邪恶的女巫变成了狼吗？ 我也许能够消除你的诅咒！");
            AddTranslation(text);

            text = CreateTranslation("TaxCollectorWerewolf");
            text.SetDefault("A DOG?! I am allergic to dogs, go away!");
            text.AddTranslation(GameCulture.Russian, "СОБАКА?! У меня аллергия на собак, убирайся!");
            text.AddTranslation(GameCulture.Chinese, "一条狗？！我对狗过敏，滚开！");
            AddTranslation(text);

            text = CreateTranslation("TruffleWerewolf");
            text.SetDefault("Uhh... Wolves don't eat mushrooms... R-right? Don't eat me...");
            text.AddTranslation(GameCulture.Russian, "Эээ... Волки не едят грибы... П-правильно? Не ешь меня...");
            text.AddTranslation(GameCulture.Chinese, "呃…狼不吃蘑菇…对吗？不要吃我…");
            AddTranslation(text);

            text = CreateTranslation("PirateWerewolf");
            text.SetDefault("Yarr! Ya look ferocious! Here, loot a bone! Don't mind it bein' a bone o' me dead crew mate.");
            text.AddTranslation(GameCulture.Russian, "Йарр! Ты выглядишь свирепым! Вот, возьми косточку! Не обращай внимания на то, что это кость мёртвого члена моей команды.");
            text.AddTranslation(GameCulture.Chinese, "呀！你看起来很凶！在这里，掠夺骨头！不要介意，它只是一个骨头，而不是我死去的船员。");
            AddTranslation(text);

            text = CreateTranslation("SteampunkerWerewolf");
            text.SetDefault("I've already made a penguin fly, so now it's time to make a dog fly! Come on, just put on this jetpack...");
            text.AddTranslation(GameCulture.Russian, "Я уже смогла сделать так, что пингвин полетел, поэтому теперь время собаке полетать! Ну же, просто надень этот реактивный ранец...");
            text.AddTranslation(GameCulture.Chinese, "我已经让企鹅上天了，现在该让狗上天了！快点，穿上这个喷气背包…");
            AddTranslation(text);

            text = CreateTranslation("CyborgWerewolf");
            text.SetDefault("Did you came from future where wolves took over the world? Or are you from another world line? I should check my divergence meter to see if anything changed...");
            text.AddTranslation(GameCulture.Russian, "Ты прибыл из будущего, где волки захватили мир? Или ты из другой мировой линии? Я должен проверить свой измеритель отклонения, вдруг что-то изменилось...");
            text.AddTranslation(GameCulture.Chinese, "你是不是来自一个被狼占领的未来世界？或者你来自另一条世界线？我应该检查一下计量器，看看有没有什么变化…");
            AddTranslation(text);

            text = CreateTranslation("SantaClausWerewolf");
            text.SetDefault("A wolf? Oh, I get it! Are you dressed up for a christmas party?");
            text.AddTranslation(GameCulture.Russian, "Волк? А, я понял! Ты приоделся на рождественскую вечеринку?");
            text.AddTranslation(GameCulture.Chinese, "一条狼？哦，我懂了！你是盛装参加圣诞派对的吗？");
            AddTranslation(text);

            text = CreateTranslation("TravellingMerchantWerewolf");
            text.SetDefault("I've seen my friend selling wolf's teeth once. Hopefully, those were not teeth of your brother or someone close to you.");
            text.AddTranslation(GameCulture.Russian, "Я видел, как однажды мой друг продавал зубы волка. Надеюсь, это не были зубы твоего брата или кого-нибудь ещё, близкого к тебе.");
            text.AddTranslation(GameCulture.Chinese, "我见过我的朋友曾经卖过狼的牙齿。希望那些不是你兄弟姐妹的牙齿。");
            AddTranslation(text);

            text = CreateTranslation("SkeletonMerchantWerewolf");
            text.SetDefault("Don't think you can chew on my bones, I need them!");
            text.AddTranslation(GameCulture.Russian, "Даже не думай, что сможешь погрызть мои кости, они мне нужны!");
            text.AddTranslation(GameCulture.Chinese, "别以为你能咀嚼我的骨头，我需要它们！");
            AddTranslation(text);

            text = CreateTranslation("AdventurerWerewolf");
            text.SetDefault("Think I'm scared of you? The only wolf I was scared of during my adventures, was a giant gray wolf with a sword in its jaws.");
            text.AddTranslation(GameCulture.Russian, "Думаешь, я боюсь тебя? Единственным волком, которого я боялся во время своих приключений, был огромный серый волк с мечом в зубах.");
            text.AddTranslation(GameCulture.Chinese, "你以为我害怕你吗？我在探险中唯一害怕的狼，是一条巨大的灰狼，下颚有一把剑。");
            AddTranslation(text);

            text = CreateTranslation("CursedAir");
            text.SetDefault("The air around your slowly becomes cursed...");
            text.AddTranslation(GameCulture.Russian, "Воздух вокруг вас постепенно становится проклятым...");
            text.AddTranslation(GameCulture.Chinese, "你周围的空气慢慢地变成了诅咒…");
            AddTranslation(text);

            text = CreateTranslation("DavyAwoken");
            text.SetDefault("Davy prepares to unleash his power!");
            text.AddTranslation(GameCulture.Russian, "Дэйви готовится высвободить свою силу!");
            text.AddTranslation(GameCulture.Chinese, "戴维准备释放他的力量！");
            AddTranslation(text);

            text = CreateTranslation("CursedPirateBattle");
            text.SetDefault("Fight");
            text.AddTranslation(GameCulture.Russian, "Сразиться");
            text.AddTranslation(GameCulture.Chinese, "战斗");
            AddTranslation(text);

            text = CreateTranslation("CursedPirate1");
            text.SetDefault("Ye've come t' fight me again? How foolish! Do ye reckon ye've become stronger aft yer complete failure last time?");
            text.AddTranslation(GameCulture.Russian, "Ты пришел снова сразиться со мной? Как глупо! Ты думаешь, что стал сильнее после своего последнего полного провала?");
            text.AddTranslation(GameCulture.Chinese, "你来到这里再一次来挑战我？多么愚蠢！你认为你上次彻底失败后变得更强大了吗？");
            AddTranslation(text);

            text = CreateTranslation("CursedPirate2");
            text.SetDefault("I can do this all day... Ye shall fall once again afore the might o' the sea!");
            text.AddTranslation(GameCulture.Russian, "Я могу делать это целый день... Ты снова падешь перед мощью моря!");
            text.AddTranslation(GameCulture.Chinese, "我可以整天这样做......你会再次因为大海的力量而堕落！");
            AddTranslation(text);

            text = CreateTranslation("SoulStolen");
            text.SetDefault("Soul is stolen!");
            text.AddTranslation(GameCulture.Russian, "Душа украдена!");
            text.AddTranslation(GameCulture.Chinese, "灵魂被偷窃了");
            AddTranslation(text);

            text = CreateTranslation("SoulReturned");
            text.SetDefault("Soul is returned!");
            text.AddTranslation(GameCulture.Russian, "Душа возвращена!");
            text.AddTranslation(GameCulture.Chinese, "灵魂归来了！");
            AddTranslation(text);

            text = CreateTranslation("LeavingBeach");
            text.SetDefault("<Deadly> Ye can try t' run from anyone but nah from me!");
            text.AddTranslation(GameCulture.Russian, "<Дэдли> Ты можешь попытаться сбежать от кого угодно, но не от меня!");
            AddTranslation(text);

            text = CreateTranslation("Singularity");
            text.SetDefault("The universe is approaching its singularity point...");
            text.AddTranslation(GameCulture.Russian, "Вселенная приближается к своей точке сингулярности...");
            text.AddTranslation(GameCulture.Chinese, "宇宙正在接近它的“奇点”…");
            AddTranslation(text);

            text = CreateTranslation("SingularityTime");
            text.SetDefault("Time until hitting a singularity point: {0} sec.");
            text.AddTranslation(GameCulture.Russian, "Время до достижения точки сингулярности: {0} сек.");
            text.AddTranslation(GameCulture.Chinese, "到达奇点前的时间： {0} sec.");
            AddTranslation(text);

            text = CreateTranslation("Warn1");
            text.SetDefault("<???> Are you sure you want to do this? There will be no turning back.");
            text.AddTranslation(GameCulture.Russian, "<???> Вы уверены, что хотите сделать это? Пути назад не будет.");
            text.AddTranslation(GameCulture.Chinese, "<???> 你确定要这样做吗？这里没有回头路。");
            AddTranslation(text);

            text = CreateTranslation("Warn2");
            text.SetDefault("<???> You won't be able to undo what will happen.");
            text.AddTranslation(GameCulture.Russian, "<???> Вы не сможете отменить то, что произойдёт.");
            text.AddTranslation(GameCulture.Chinese, "<???> 你将无法挽回将要发生的事情。");
            AddTranslation(text);

            text = CreateTranslation("Warn3");
            text.SetDefault("<???> You were warned.");
            text.AddTranslation(GameCulture.Russian, "<???> Вы были предупреждены.");
            text.AddTranslation(GameCulture.Chinese, "<???> 你已经被警告过了。");
            AddTranslation(text);

            text = CreateTranslation("TimeAccelerate");
            text.SetDefault("The time begins to accelerate!");
            text.AddTranslation(GameCulture.Russian, "Время начинает ускоряться!");
            text.AddTranslation(GameCulture.Chinese, "时间开始加速流逝了！");
            AddTranslation(text);

            text = CreateTranslation("Resetting");
            text.SetDefault("The universe is resetting");
            text.AddTranslation(GameCulture.Russian, "Вселенная перезапускается");
            text.AddTranslation(GameCulture.Chinese, "宇宙正在重置");
            AddTranslation(text);

            text = CreateTranslation("HeavenTime");
            text.SetDefault("Time for Heaven...");
            text.AddTranslation(GameCulture.Russian, "Время  для  Рая...");
            AddTranslation(text);

            text = CreateTranslation("HiveShrineGen");
            text.SetDefault("Bees are building a shrine...");
            text.AddTranslation(GameCulture.Russian, "Пчёлы строят святилище...");
            AddTranslation(text);

            #endregion
            Main.reforgeTexture[0] = ModContent.GetTexture("Antiaris/Miscellaneous/Reforge_0");
            Main.reforgeTexture[1] = ModContent.GetTexture("Antiaris/Miscellaneous/Reforge_1");
            Main.HBLockTexture[0] = ModContent.GetTexture("Antiaris/Miscellaneous/Lock_0");
            Main.HBLockTexture[1] = ModContent.GetTexture("Antiaris/Miscellaneous/Lock_1");
			Main.frozenTexture = ModContent.GetTexture("Antiaris/Miscellaneous/Frozen");
			Main.itemTexture[989] = ModContent.GetTexture("Antiaris/Miscellaneous/Item_989");
			Main.projectileTexture[173] = ModContent.GetTexture("Antiaris/Miscellaneous/Projectile_173");
			Main.itemTexture[55] = ModContent.GetTexture("Antiaris/Miscellaneous/Item_55");
			Main.projectileTexture[6] = ModContent.GetTexture("Antiaris/Miscellaneous/Projectile_6");
			Main.tileTexture[187] = ModContent.GetTexture("Antiaris/Miscellaneous/Tiles_187");
        }

        public override void Unload()
        {
            cQuestTexture = null;
            Instance = null;
            Thorium = null;
            kRPG = null;
            RockosARPG = null;
            TerrariaOverhaul = null;
            Unleveled = null;
            stand = null;
            hideTracker = null;
            trackerTexture = null;
            adventurerKey = null;


            AntiarisGlowMasks.Unload();
            //Jofairden's code
            instance = null;
            if (!Main.dedServ)
            {
                Main.reforgeTexture[0] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "UI", Path.DirectorySeparatorChar, "Reforge_0" }));
                Main.reforgeTexture[1] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "UI", Path.DirectorySeparatorChar, "Reforge_1" }));
                Main.HBLockTexture[0] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Lock_0" }));
                Main.HBLockTexture[1] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Lock_1" }));
                Main.frozenTexture = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Frozen" }));
				Main.itemTexture[989] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Item_989" }));
				Main.projectileTexture[173] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Projectile_173" }));
				Main.itemTexture[55] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Item_55" }));
				Main.projectileTexture[6] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Projectile_6" }));
				Main.tileTexture[187] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[] { "Images", Path.DirectorySeparatorChar, "Tiles_187" }));
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (questInterface != null)
                questInterface.Update(gameTime);
            if (questLog != null)
                questLog.Update(gameTime);
            if (NixieFace != null)
                NixieFace.Update(gameTime);
        }

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "Shadowflame", 12);
			recipe.AddIngredient(ItemID.SoulofNight, 6);
			recipe.AddIngredient(ItemID.Marrow);
            recipe.SetResult(3052);
            recipe.AddTile(134);
            recipe.AddRecipe();
			
			recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "Shadowflame", 10);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(ItemID.MagicDagger);
            recipe.SetResult(3054);
            recipe.AddTile(134);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "HoneyExtract");
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.SetResult(ItemID.BottledHoney);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "HoneyExtract", 5);
            recipe.SetResult(ItemID.HoneyBlock, 50);
            recipe.AddTile(220);
            recipe.AddRecipe();
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
			{
				var aPlayer = Main.player[Main.myPlayer].GetModPlayer<AntiarisPlayer>(mod);
				// Make sure your logic here goes from lowest priority to highest so your intended priority is maintained.
				if (mod.GetModWorld<AntiarisWorld>().frozenTime)
				{
					music = 0;
					priority = MusicPriority.BossHigh;
				}
				if (!mod.GetModWorld<AntiarisWorld>().frozenTime)
				{
					if (NPC.AnyNPCs(NPCID.KingSlime))
					{
						music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/SlimeKing");
						priority = MusicPriority.BossLow;
					}
					if (!Main.player[Main.myPlayer].ZoneDirtLayerHeight)
					{
						if (!Main.player[Main.myPlayer].ZoneRockLayerHeight)
						{
							if (Main.player[Main.myPlayer].ZoneCrimson && Main.raining)
							{
								music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/CrimsonRain");
								priority = MusicPriority.BiomeHigh;
							}
							if (Main.player[Main.myPlayer].ZoneHoly && Main.raining)
							{
								music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/HallowRain");
								priority = MusicPriority.BiomeHigh;
							}
							if (Main.player[Main.myPlayer].ZoneCorrupt && Main.raining)
							{
								music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/CorruptionRain");
								priority = MusicPriority.BiomeHigh;
							}
							if (Main.player[Main.myPlayer].ZoneSnow && Main.raining)
							{
								music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Blizzard");
								priority = MusicPriority.BiomeHigh;
							}
							if (Main.player[Main.myPlayer].ZoneHoly && !Main.dayTime && !Main.player[Main.myPlayer].ZoneSnow)
							{
								music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/HallowNight");
								priority = MusicPriority.BiomeHigh;
							}	
						}
					}
					if (Main.player[Main.myPlayer].ZoneDesert)
					{
						if (Main.player[Main.myPlayer].ZoneDirtLayerHeight || Main.player[Main.myPlayer].ZoneRockLayerHeight)
						{
							music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/UndergroundDesert");
							priority = MusicPriority.BiomeHigh;
						}
					}
				}
            }
		}

        public override object Call(params object[] args)
        {
            try
            {
                string message = args[0] as string;
                if (message == "AddItemQuest")
                {
                    string name = args[1] as string;
                    int itemID = Convert.ToInt32(args[2]);
                    int itemAmount = Convert.ToInt32(args[3]);
                    double weight = Convert.ToInt32(args[4]);
                    string specialThanks = args[5] as string;
                    var quest = new ItemQuest(name, itemID, itemAmount, weight, specialThanks);
                    if (args.Length > 6)
                        quest.SpawnReward = (Action<NPC>)args[6];
                    if (args.Length > 7)
                        quest.IsAvailable = (Func<bool>)args[7];
                    return QuestSystem.Quests.Count - 1;
                }
                else if (message == "AddKillQuest")
                {
                    string name = args[1] as string;
                    int[] npcType = args[2] as int[];
                    int npcAmount = Convert.ToInt32(args[3]);
                    double weight = Convert.ToInt32(args[4]);
                    string specialThanks = args[5] as string;
                    var quest = new KillQuest(name, npcType, npcAmount, weight, specialThanks);
                    if (args.Length > 6)
                        quest.SpawnReward = (Action<NPC>)args[6];
                    if (args.Length > 7)
                        quest.IsAvailable = (Func<bool>)args[7];
                    return QuestSystem.Quests.Count - 1;
                }
                else if (message == "GetCurrentQuest")
                {
                    var player = Main.player[Main.myPlayer];
                    if (args.Length > 1)
                        player = args[1] as Player;
                    return player.GetModPlayer<QuestSystem>().CurrentQuest;
                }
                else
                {
                    ErrorLogger.Log("Oh no, an error happened! Report this to Zerokk and send him the file Terraria/ModLoader/Logs/Logs.txt");
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Log("Oh no, an error happened! Report this to Zerokk and send him the file Terraria/ModLoader/Logs/Logs.txt");
                ErrorLogger.Log(e.ToString());
            }
            return "ERROR!";
        }

        public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemType("WoodenPickaxe")), new int[]
			{
				ItemType("WoodenPickaxe"),
				ItemType("BorealWoodPickaxe"),
				ItemType("EbonwoodPickaxe"),
				ItemType("PearlwoodPickaxe"),
				ItemType("ShadewoodPickaxe"),
				ItemType("PalmWoodPickaxe"),
				ItemType("PhantomwoodPickaxe"),
				ItemType("RichMahoganyPickaxe")
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodenPickaxe", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemType("WoodenAxe")), new int[]
			{
				ItemType("WoodenAxe"),
				ItemType("BorealWoodAxe"),
				ItemType("EbonwoodAxe"),
				ItemType("PearlwoodAxe"),
				ItemType("ShadewoodAxe"),
				ItemType("PalmWoodAxe"),
				ItemType("RichMahoganyAxe")
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodenAxe", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.WoodenHammer), new int[]
			{
				ItemID.WoodenHammer,
				ItemID.BorealWoodHammer,
				ItemID.EbonwoodHammer,
				ItemID.PearlwoodHammer,
				ItemID.ShadewoodHammer,
				ItemID.PalmWoodHammer,
				ItemID.RichMahoganyHammer
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodenHammer", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemType("WoodenSpear")), new int[]
			{
				ItemType("WoodenSpear"),
				ItemType("BorealWoodSpear"),
				ItemType("EbonwoodSpear"),
				ItemType("PearlwoodSpear"),
				ItemType("ShadewoodSpear"),
				ItemType("PalmWoodSpear"),
				ItemType("RichMahoganySpear")
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodenSpear", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.WoodenBow), new int[]
			{
				ItemID.WoodenBow,
				ItemID.BorealWoodBow,
				ItemID.EbonwoodBow,
				ItemID.PearlwoodBow,
				ItemID.ShadewoodBow,
				ItemID.PalmWoodBow,
				ItemID.RichMahoganyBow
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodenBow", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.WoodenSword), new int[]
			{
				ItemID.WoodenSword,
				ItemID.BorealWoodSword,
				ItemID.EbonwoodSword,
				ItemID.PearlwoodSword,
				ItemID.ShadewoodSword,
				ItemID.PalmWoodSword
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodenSword", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.WoodHelmet), new int[]
			{
				ItemID.WoodHelmet,
				ItemID.BorealWoodHelmet,
				ItemID.EbonwoodHelmet,
				ItemID.PearlwoodHelmet,
				ItemID.ShadewoodHelmet,
				ItemID.PalmWoodHelmet,
				ItemID.RichMahoganyHelmet
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodHelmet", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.WoodBreastplate), new int[]
			{
				ItemID.WoodBreastplate,
				ItemID.BorealWoodBreastplate,
				ItemID.EbonwoodBreastplate,
				ItemID.PearlwoodBreastplate,
				ItemID.ShadewoodBreastplate,
				ItemID.PalmWoodBreastplate,
				ItemID.RichMahoganyBreastplate
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodBreastplate", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.WoodGreaves), new int[]
			{
				ItemID.WoodGreaves,
				ItemID.BorealWoodGreaves,
				ItemID.EbonwoodGreaves,
				ItemID.PearlwoodGreaves,
				ItemID.ShadewoodGreaves,
				ItemID.PalmWoodGreaves,
				ItemID.RichMahoganyGreaves
			});
			RecipeGroup.RegisterGroup("Antiaris:WoodGreaves", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.SilverBar), new int[]
			{
				ItemID.SilverBar,
				ItemID.TungstenBar,
			});
			RecipeGroup.RegisterGroup("Antiaris:SilverBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CopperBar), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar,
            });
            RecipeGroup.RegisterGroup("Antiaris:CopperBar", group);

			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.GoldBar), new int[]
			{
				ItemID.GoldBar,
				ItemID.PlatinumBar,
			});
            RecipeGroup.RegisterGroup("Antiaris:GoldBar", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CopperPickaxe), new int[]
			{
				ItemID.CopperPickaxe,
				ItemID.TinPickaxe,
			});
            RecipeGroup.RegisterGroup("Antiaris:CopperPickaxe", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.DemoniteBar), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
            });
            RecipeGroup.RegisterGroup("Antiaris:DemoniteBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CopperAxe), new int[]
			{
				ItemID.CopperAxe,
				ItemID.TinAxe,
			});
            RecipeGroup.RegisterGroup("Antiaris:CopperAxe", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CopperHammer), new int[]
			{
				ItemID.CopperHammer,
				ItemID.TinHammer,
			});
            RecipeGroup.RegisterGroup("Antiaris:CopperHammer", group);
			
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CopperBow), new int[]
			{
				ItemID.CopperBow,
				ItemID.TinBow,
			});
            RecipeGroup.RegisterGroup("Antiaris:CopperBow", group);
        }

        float scale = 0f;
        float opacity = 1f;
        int dotAmount = 1;
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.GetModPlayer<AntiarisPlayer>().effectTimer == 1110)
            {
                Main.soundVolume = 0;
            }
            if (player.GetModPlayer<AntiarisPlayer>().effectTimer % 40 == 0)
            {
                dotAmount++;
            }
            if (dotAmount > 3)
            {
                dotAmount = 1;
            }
            scale = player.GetModPlayer<AntiarisPlayer>().effectTimer / 1000;
            opacity -= player.GetModPlayer<AntiarisPlayer>().effectTimer / 200;
            if (opacity <= 0f)
                opacity = 0f;
            if (scale > 2)
            {
                scale = 2;
            }
            var background = mod.GetTexture("Miscellaneous/Universe");
            var background2 = mod.GetTexture("Miscellaneous/ResetBackground");
            string Resetting = Language.GetTextValue("Mods.Antiaris.Resetting");
            switch(dotAmount)
            {
                case 1:
                    Resetting = Language.GetTextValue("Mods.Antiaris.Resetting") + ".";
                    break;
                case 2:
                    Resetting = Language.GetTextValue("Mods.Antiaris.Resetting") + "..";
                    break;
                case 3:
                    Resetting = Language.GetTextValue("Mods.Antiaris.Resetting") + "...";
                    break;           
            }
            Vector2 measure = Main.fontDeathText.MeasureString(Resetting) * scale;
            if (player.GetModPlayer<AntiarisPlayer>().darkness == 1)
            { 
                spriteBatch.Draw(background, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
                spriteBatch.Draw(background2, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * opacity);
                if (opacity == 0f)
                    Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, Resetting, (int)((Main.screenWidth / 2) - ((float)measure.X / 2.0f)), (int)(Main.screenHeight / 2), Color.White, Color.Black, new Vector2(), scale);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            #region Quest Tracker + log
            Mod mod = ModLoader.GetMod("Antiaris");
			var questSystem = Main.player[Main.myPlayer].GetModPlayer<QuestSystem>(mod);
            var aPlayer = Main.player[Main.myPlayer].GetModPlayer<AntiarisPlayer>(mod);
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                    "Antiaris: Quest UI",
                    delegate
                    {
                        if(QuestTrackerUI.visible)
                            questTracker.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                    "Antiaris: Quest UI",
                    delegate
                    {
                        if (CurrentQuestUI.visible)
                            cQuestUI.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                ); 
            }

            #endregion
            if (!Main.player[Main.myPlayer].ghost && aPlayer.OpenWindow)
            {
                var index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var UIState = new LegacyGameInterfaceLayer("Antiaris: UI",
                    delegate
                    {
                        DrawButton(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(index, UIState);
            }
			if (!Main.player[Main.myPlayer].ghost && aPlayer.OpenWindow3)
            {
                var index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var UIState = new LegacyGameInterfaceLayer("Antiaris: UI",
                    delegate
                    {
                        DrawButton2(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(index, UIState);
            }
			if (Antiaris.kRPG == null && Antiaris.RockosARPG == null && Antiaris.Unleveled == null)
			{
				var heartLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
				var heartState = new LegacyGameInterfaceLayer("Antiaris: UI2",
					delegate
					{
						DrawNewHearts(Main.spriteBatch);
						return true;
					},
					InterfaceScaleType.UI);
				layers.Insert(heartLayer, heartState);
			}
			int MouseTextIndex2 = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            
            int cursor = layers.FindIndex((Predicate<GameInterfaceLayer>)(layer => layer.Name == "Vanilla: Cursor"));
            if (cursor != -1)
            {
                Player player = Main.player[Main.myPlayer];
                if (!Main.LocalPlayer.mouseInterface && player.GetModPlayer<AntiarisPlayer>().darkness == 1)
                    layers[cursor] = (GameInterfaceLayer)new LegacyGameInterfaceLayer(layers[cursor].Name, (GameInterfaceDrawMethod)(() =>
                    { return true; }), InterfaceScaleType.UI);
            }
            if (Main.player[Main.myPlayer].GetModPlayer<AntiarisPlayer>(mod).heavenWarn > 0 && Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == mod.ItemType("HeavenlyClock"))
            {
                var index2 = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var UIState2 = new LegacyGameInterfaceLayer("Antiaris: Warning",
                    delegate
                    {
                        DrawWarning(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(index2, UIState2);
            }
            if (!Main.player[Main.myPlayer].ghost)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                    "Antiaris: NixieUI",
                    delegate
                    {
                        if (NixieTubeUI.visible)
                            NixieUI.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

        }

        public void DrawButton(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("Antiaris");
            var background = mod.GetTexture("Miscellaneous/NoteBackground");
            string note = Language.GetTextValue("Mods.Antiaris.Note1");
            spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 120, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, note, Main.screenWidth / 2 - 130, 41, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
        }

        public void DrawButton2(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("Antiaris");
            var background = mod.GetTexture("Miscellaneous/NoteBackground");
            string note = Language.GetTextValue("Mods.Antiaris.Note2");
            spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 120, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, note, Main.screenWidth / 2 - 130, 41, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
        }

        public void DrawWarning(SpriteBatch spriteBatch)
        {
            Player player = Main.player[Main.myPlayer];
            string Warn = Language.GetTextValue("Mods.Antiaris.Warn1");
            if (player.GetModPlayer<AntiarisPlayer>(mod).heavenWarn == 2)
                Warn = Language.GetTextValue("Mods.Antiaris.Warn2");
            if (player.GetModPlayer<AntiarisPlayer>(mod).heavenWarn == 3)
                Warn = Language.GetTextValue("Mods.Antiaris.Warn3");
            Vector2 measure = Main.fontDeathText.MeasureString(Warn) * 1f;
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, Warn, (int)((Main.screenWidth / 2) - ((float)measure.X / 2.0f)), (int)(Main.screenHeight / 2), new Color(255, 40, 76), Color.Black, new Vector2(), 1f);
        }

        private float timer = 0.0f;
        private int currentState = 0;
        public void DrawNewHearts(SpriteBatch spriteBatch)
        {
            lifePerHeart = 20f;
            var lifeForHeart = Main.player[Main.myPlayer].statLifeMax / 20;
            var lifeForBlazingHeart = (int)((Main.player[Main.myPlayer].statLifeMax - 300) / 5f);
            if (lifeForBlazingHeart < 0)
                lifeForBlazingHeart = 0;
            if (lifeForBlazingHeart > 0)
            {
                lifeForHeart = Main.player[Main.myPlayer].statLifeMax / (20 + lifeForBlazingHeart / 4);
                lifePerHeart = (float)Main.player[Main.myPlayer].statLifeMax / 20f;
            }
            var lifeForDazzlingHeart = (int)((Main.player[Main.myPlayer].statLifeMax - 400) / 2.5f);
            if (lifeForDazzlingHeart < 0)
                lifeForDazzlingHeart = 0;
            if (lifeForDazzlingHeart > 0)
            {
                lifeForHeart = Main.player[Main.myPlayer].statLifeMax / (20 + lifeForDazzlingHeart / 4);
                lifePerHeart = (float)Main.player[Main.myPlayer].statLifeMax / 20f;
            }
            var lifeForLifeFruit = (int)((Main.player[Main.myPlayer].statLifeMax - 450) / 2.5f);
            if (lifeForLifeFruit < 0)
                lifeForLifeFruit = 0;
            if (lifeForLifeFruit > 0)
            {
                lifeForHeart = Main.player[Main.myPlayer].statLifeMax / (20 + lifeForLifeFruit / 4);
                lifePerHeart = (float)Main.player[Main.myPlayer].statLifeMax / 20f;
            }
            var playerLife = Main.player[Main.myPlayer].statLifeMax2 - Main.player[Main.myPlayer].statLifeMax;
            lifePerHeart += (float)(playerLife / lifeForHeart);
            var hearts = (int)((double)Main.player[Main.myPlayer].statLifeMax2 / (double)lifePerHeart);
            if (hearts >= 10)
                hearts = 10;
            for (int oneHeart = 1; oneHeart < (int)((double)Main.player[Main.myPlayer].statLifeMax2 / (double)lifePerHeart) + 1; ++oneHeart)
            {
                var scale = 1f;
                var checkDrawPos = false;
                var statLife = 0;
                if ((double)Main.player[Main.myPlayer].statLife >= (double)oneHeart * (double)lifePerHeart)
                {
                    statLife = 255;
                    if ((double)Main.player[Main.myPlayer].statLife == (double)oneHeart * (double)lifePerHeart)
                        checkDrawPos = true;
                }
                else
                {
                    float checkOwnLifeForDraw = ((float)Main.player[Main.myPlayer].statLife - (float)(oneHeart - 1) * lifePerHeart) / lifePerHeart;
                    statLife = (int)(30.0 + 225.0 * (double)checkOwnLifeForDraw);
                    if (statLife < 30)
                        statLife = 30;
                    scale = (float)((double)checkOwnLifeForDraw / 4.0 + 0.75);
                    if ((double)scale < 0.75)
                        scale = 0.75f;
                    if ((double)checkOwnLifeForDraw > 0.0)
                        checkDrawPos = true;
                }
                if (checkDrawPos)
                    scale += Main.cursorScale - 1.0f;
                var x = 0;
                var y = 0;
                if (oneHeart > 10)
                {
                    x -= 260;
                    y += 26;
                }
                var a = (int)((double)statLife * 0.9);
                int startX;
                var info = typeof(Main).GetField("UI_ScreenAnchorX",
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                startX = (int)info.GetValue(null);
                ++timer;
                if (timer % 80f == 0f) currentState += 1;
                if (timer >= 80f) timer = 0.0f;
                if (currentState > 2) currentState = 0;
                if (!Main.player[Main.myPlayer].ghost)
                {
                    if (lifeForBlazingHeart > 0)
                    {
                        --lifeForBlazingHeart;
                        var texture2 = mod.GetTexture("Miscellaneous/LifeCrystal2");
                        spriteBatch.Draw(texture2, new Vector2((float)(500 + 26 * (oneHeart - 1) + x + startX + texture2.Width / 2), (float)(32.0 + ((double)texture2.Height - (double)texture2.Height * (double)scale) / 2.0) + (float)y + (float)(texture2.Height / 2)), new Rectangle?(new Rectangle(0, 0, texture2.Width, texture2.Height)), new Color(statLife, statLife, statLife, a), 0.0f, new Vector2((float)(texture2.Width / 2), (float)(texture2.Height / 2)), scale, SpriteEffects.None, 0.0f);
                    }
                    if (lifeForDazzlingHeart > 0)
                    {
                        --lifeForDazzlingHeart;
                        var texture2 = mod.GetTexture("Miscellaneous/LifeCrystal3");
                        spriteBatch.Draw(texture2, new Vector2((float)(500 + 26 * (oneHeart - 1) + x + startX + texture2.Width / 2), (float)(32.0 + ((double)texture2.Height - (double)texture2.Height * (double)scale) / 2.0) + (float)y + (float)(texture2.Height / 2)), new Rectangle?(new Rectangle(0, 0, texture2.Width, texture2.Height)), new Color(statLife, statLife, statLife, a), 0.0f, new Vector2((float)(texture2.Width / 2), (float)(texture2.Height / 2)), scale, SpriteEffects.None, 0.0f);
                    }
                    if (lifeForLifeFruit > 0)
                    {
                        --lifeForLifeFruit;
                        var texture3 = mod.GetTexture("Miscellaneous/LifeCrystal4");
                        spriteBatch.Draw(texture3, new Vector2((float)(500 + 26 * (oneHeart - 1) + x + startX + texture3.Width / 2), (float)(32.0 + ((double)texture3.Height - (double)texture3.Height * (double)scale) / 2.0) + (float)y + (float)(texture3.Height / 2)), new Rectangle?(new Rectangle(0, 0, texture3.Width, texture3.Height)), new Color(statLife, statLife, statLife, a), 0.0f, new Vector2((float)(texture3.Width / 2), (float)(texture3.Height / 2)), scale, SpriteEffects.None, 0.0f);
                    }
                }
            }
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            var player = spawnInfo.player;
            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }

        public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;
        }

        public static bool NoZone(NPCSpawnInfo spawnInfo)
        {
            return NoZoneAllowWater(spawnInfo) && !spawnInfo.water;
        }

        public static bool NormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && NoInvasion(spawnInfo);
        }

        public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZone(spawnInfo);
        }

        public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);
        }

        public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);
        }

        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            ModExplorer._drawMapIcon(this, ref mouseText);
            for (var i = 0; i < 50; i++)
            {
                if (Main.player[Main.myPlayer].inventory[i].type == mod.ItemType("DavysMap"))
                {
                    if (AntiarisWorld.PirateCovePositionX != 0 && AntiarisWorld.PirateCovePositionY != 0 && mod.GetModWorld<AntiarisWorld>().GeneratePirateCove)
                        MapIcons.Icon(this, ref mouseText, 0, AntiarisWorld.PirateCovePositionX + 15.0f, AntiarisWorld.PirateCovePositionY - 15.0f);
                }
            }
            var player = Main.player[Main.myPlayer];
            var questSystem = Main.player[Main.myPlayer].GetModPlayer<QuestSystem>();
			var pirateQuestSystem = Main.player[Main.myPlayer].GetModPlayer<Pirate.PirateQuestSystem>();
			int AdventurerNPC = NPC.FindFirstNPC(mod.NPCType("Adventurer"));
			int Pirate = NPC.FindFirstNPC(mod.NPCType("Pirate"));
			int PirateNPC = NPC.FindFirstNPC(NPCID.Pirate);
            var aPlayer = Main.player[Main.myPlayer].GetModPlayer<AntiarisPlayer>(mod);
            if (AdventurerNPC >= 0 && questSystem.CurrentQuest == -1 && !questSystem.CompletedToday)
            {
				Vector2 vector2 = Main.npc[AdventurerNPC].Center;
                float x = vector2.X / 16f;
                float y = vector2.Y / 16f;
                MapIcons.Icon(this, ref mouseText, 1, x - 1.0f, y - 4.0f);
            }
            if (AdventurerNPC >= 0 && questSystem.CurrentQuest >= 0 && questSystem.CurrentQuest != -1 && questSystem.GetCurrentQuest() is KillQuest && questSystem.QuestKills >= (questSystem.GetCurrentQuest() as KillQuest).TargetCount)
            {
                Vector2 vector2 = Main.npc[AdventurerNPC].Center;
                float x = vector2.X / 16f;
                float y = vector2.Y / 16f;
                MapIcons.Icon(this, ref mouseText, 2, x - 1.0f, y - 4.0f);
            }
            if (AdventurerNPC >= 0 && questSystem.CurrentQuest >= 0 && questSystem.CurrentQuest != -1 && questSystem.GetCurrentQuest() is ItemQuest && player.CountItem((questSystem.GetCurrentQuest() as ItemQuest).ItemType, (questSystem.GetCurrentQuest() as ItemQuest).ItemAmount) >= (questSystem.GetCurrentQuest() as ItemQuest).ItemAmount)
            {
                Vector2 vector2 = Main.npc[AdventurerNPC].Center;
                float x = vector2.X / 16f;
                float y = vector2.Y / 16f;
                MapIcons.Icon(this, ref mouseText, 2, x - 1.0f, y - 4.0f);
            }
			if (Pirate > 0 && pirateQuestSystem.CurrentPirateQuest == -1 && !mod.GetModWorld<AntiarisWorld>().FinishedPirateQuest)
			{
				Vector2 vector2 = Main.npc[AdventurerNPC].Center;
				float x = vector2.X / 16f;
				float y = vector2.Y / 16f;
				MapIcons.Icon(this, ref mouseText, 1, x - 1.0f, y - 4.0f);
			}
			foreach (var item in player.inventory)
			{
				if (Pirate > 0 && item.type == mod.ItemType("MagicalAmulet") && pirateQuestSystem.CurrentPirateQuest != -1)
				{
					Vector2 vector2 = Main.npc[Pirate].Center;
					float x = vector2.X / 16f;
					float y = vector2.Y / 16f;
					MapIcons.Icon(this, ref mouseText, 2, x - 1.0f, y - 4.0f);
				}
			}
            for (int j = 0; j < Main.npc.Length; j++)
            {
                NPC target = Main.npc[j];
                for (int h = 0; h < target.buffType.Length; h++)
                {
                    int type = target.buffType[h];
                    if (type == mod.BuffType("Prey") && target.active)
                    {
                        Vector2 vector2 = Main.npc[j].Center;
                        float x = vector2.X / 16f;
                        float y = vector2.Y / 16f;
                        MapIcons.Icon(this, ref mouseText, 3, x, y - 0.2f);
                    }
                }
            }
        }
    }

    enum QuestMessageType : byte
    {
        QuestID
    }
}

