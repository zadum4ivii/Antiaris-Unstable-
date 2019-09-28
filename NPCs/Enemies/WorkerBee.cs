using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Antiaris.NPCs.Enemies
{
    public class WorkerBee : ModNPC
    {
        private int spawnTimer = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 200;
            npc.damage = 70;
            npc.defense = 20;
            npc.knockBackResist = 0f;
            npc.width = 50;
            npc.height = 44;
            npc.aiStyle = 14;
            aiType = NPCID.CaveBat;
            animationType = 48;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit35;
            npc.noGravity = true;
            npc.DeathSound = SoundID.NPCDeath11;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            bannerItem = mod.ItemType("WorkerBeeBanner");
            banner = npc.type;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worker Bee");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Рабочая пчела");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1);
            npc.damage = (int)(npc.damage * 1);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 0.7f);
                }
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WorkerBeeGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WorkerBeeGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WorkerBeeGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WorkerBeeGore3"), 1f);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Venom, Main.expertMode ? 180 : 120);
        }

        public override void AI()
        {
            if (spawnTimer < 2)
                spawnTimer++;
            if (spawnTimer == 1)
            {
                for (int i = 0; i < Main.rand.Next(3, 5); i++)
                    NPC.NewNPC((int)npc.Center.X + Main.rand.Next(100), (int)npc.Center.Y + Main.rand.Next(100), NPCID.Bee, 0, 1, (float)npc.whoAmI, 0.0f, 0.0f, 255);
            }
        }

        public override void NPCLoot()
        {
            if (Main.netMode != 1)
            {
                int centerX = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int centerY = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                int halfLength = npc.width / 2 / 16 + 1;
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HoneyExtract"), Main.rand.Next(4, 6), false, 0, false, false);
                }
                if (Main.rand.Next(140) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TatteredBeeWing, 1, false, 0, false, false);
                }
                if (Main.rand.Next(Main.expertMode ? 50 : 100) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Bezoar, 1, false, 0, false, false);
                }
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Stinger, 1, false, 0, false, false);
                }
                if (Main.rand.Next(20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PortableHive"), 1, false, 0, false, false);
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            return (Antiaris.NormalSpawn(spawnInfo) && Main.hardMode && Antiaris.NoZoneAllowWater(spawnInfo)) && spawnInfo.player.ZoneJungle && y > Main.rockLayer ? 0.01f : 0f;
        }
    }
}