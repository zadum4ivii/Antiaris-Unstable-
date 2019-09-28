using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Antiaris.NPCs.Enemies
{
    public class GatlingBee : ModNPC
    {
        private bool attacking = false;
        private int attackTimer = 0;

        public override void SetDefaults()
        {
            npc.lifeMax = 175;
            npc.damage = 50;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.width = 44;
            npc.height = 54;
            npc.aiStyle = 14;
            aiType = NPCID.CaveBat;
            animationType = 48;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit35;
            npc.noGravity = true;
            npc.DeathSound = SoundID.NPCDeath11;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            bannerItem = mod.ItemType("GatlingBeeBanner");
            banner = npc.type;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gatling Bee");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Пчела гатлинга");
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
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GatlingBeeGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GatlingBeeGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GatlingBeeGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GatlingBeeGore3"), 1f);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Venom, Main.expertMode ? 180 : 120);
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if ((double)npc.position.X <= (double)player.position.X + 20 && (double)npc.position.X >= (double)player.position.X - 20 && (double)npc.position.Y < (double)player.position.Y)
            {
                if (Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                { 
                    attacking = true;
                }   
            }            
            if (attacking)
            {
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
                attackTimer++;
                npc.direction = npc.spriteDirection;
                if ((double)player.position.X > (double)npc.position.X)
                    npc.spriteDirection = 1;
                else if ((double)player.position.X < (double)npc.position.X)
                    npc.spriteDirection = -1;
                if (attackTimer % (Main.expertMode ? 14 : 18) == 0)
                {
                    Main.PlaySound(SoundID.Item17, npc.position);
                    Vector2 player2 = player.Center;
                    Vector2 vector2_1 = player2;
                    float speed = 10f;
                    Vector2 vector2_2 = vector2_1 - npc.Center;
                    float distance = (float)System.Math.Sqrt((double)vector2_2.X * (double)vector2_2.X + (double)vector2_2.Y * (double)vector2_2.Y);
                    vector2_2 *= speed / distance;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 20, vector2_2.X, vector2_2.Y, 55, npc.damage / 3, 5.0f, 0, 0.0f, 0.0f);

                }
                if (attackTimer == 180)
                {
                    attackTimer = 0;
                    attacking = false;
                }
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HoneyExtract"), Main.rand.Next(3, 6), false, 0, false, false);
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GatlingStinger"), 1, false, 0, false, false);
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