using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.NPCs.Enemies
{
    public class WrathSorcerer : ModNPC
    {
        private int timer = 0;
        private bool attacking = false;
        private int frame = 0;

        public override void SetDefaults()
        {
            npc.lifeMax = 60;
            npc.damage = 10;
            npc.defense = 6;
            npc.knockBackResist = 0.4f;
            npc.width = 30;
            npc.height = 48;
            npc.aiStyle = 0;
            npc.npcSlots = 0.2f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 0, 4, 0);
            banner = npc.type;
            bannerItem = mod.ItemType("WrathSorcererBanner");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath Sorcerer");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Яростный колдун");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1);
            npc.damage = (int)(npc.damage * 1);
        }

        public override void NPCLoot()
        {
            if (Main.netMode != 1)
            {
                int centerX = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int centerY = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                int halfLength = npc.width / 2 / 16 + 1;
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WrathElement"), 1, false, 0, false, false);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            var GlowMask = mod.GetTexture("Glow/WrathSorcerer_GlowMask");
            var Effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(GlowMask, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, Effects, 0);
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1f, 0.6f, 0.3f);
            Player player = Main.player[npc.target];
            timer++;
            if (timer % 10 == 0)
            {
                frame++;
            }
            if (timer % 80 == 0)
            {
                attacking = true;
            }
            if (frame > 3 && !attacking)
            {
                frame = 0;
            }
            if ((frame < 4 || frame > 7) && attacking)
            {
                frame = 4;
            }
            if (attacking)
            {
                if (timer == 110)
                {
                    var velocity = AntiarisHelper.VelocityToPoint(npc.Center, AntiarisHelper.RandomPointInArea(new Vector2(player.Center.X, player.Center.Y), new Vector2(player.Center.X + 20, player.Center.Y + 20)), 12);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, velocity.X, velocity.Y, mod.ProjectileType("WrathSkull"), Main.rand.Next(20, 30), 1f);
                    Main.PlaySound(SoundID.Item8);
                    npc.position.X = (Main.player[npc.target].position.X - 500) + Main.rand.Next(1000);
                    npc.position.Y = (Main.player[npc.target].position.Y - 500) + Main.rand.Next(1000);
                    attacking = false;
                    frame = 0;
                    timer = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * frame;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 127, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 0.7f);
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            return (Antiaris.NormalSpawn(spawnInfo) && Antiaris.NoZoneAllowWater(spawnInfo)) && NPC.downedBoss1 && !spawnInfo.player.ZoneDungeon && !spawnInfo.player.ZoneJungle && y > Main.rockLayer ? 0.1f : 0f;
        }
    }
}