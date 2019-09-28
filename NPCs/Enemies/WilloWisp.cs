using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace Antiaris.NPCs.Enemies
{
    public class WilloWisp : ModNPC
    {
        private int spawnTimer = 0;
        private bool hasFlames = false;
        public override void SetDefaults()
        {
            npc.lifeMax = 300;
            npc.damage = 50;
            npc.defense = 35;
            npc.knockBackResist = 0f;
            npc.width = 30;
            npc.height = 38;
            npc.aiStyle = 14;
            aiType = NPCID.CaveBat;
            animationType = 49;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.Item29;
            npc.noGravity = true;
            npc.DeathSound = SoundID.Item27;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            bannerItem = mod.ItemType("WilloWispBanner");
            banner = npc.type;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Will-o'-Wisp");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Блуждающий огонёк");
            Main.npcFrameCount[npc.type] = 5;
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
				for (int i = 0; i < 5; i++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 86, npc.velocity.X, npc.velocity.Y);
				}
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WilloWispGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WilloWispGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WilloWispGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WilloWispGore3"), 1f);
            }
        }

        public override void AI()
        {
            Lighting.AddLight((int)npc.position.X / 16, (int)npc.position.Y / 16, 0.9f, 0.2f, 0.1f);
            spawnTimer++;
            if (spawnTimer == 180 && !hasFlames)
            {
                hasFlames = true;
                Main.PlaySound(SoundID.Item8);
                Projectile.NewProjectile(npc.Center.X + 10, npc.Center.Y + 60, 0, 0, mod.ProjectileType("WispFlame"), 60, 5.0f, 0, 1f, npc.whoAmI);
                Projectile.NewProjectile(npc.Center.X + 10, npc.Center.Y - 40, 0, 0, mod.ProjectileType("WispFlame"), 60, 5.0f, 0, 2f, npc.whoAmI);
                Projectile.NewProjectile(npc.Center.X + 60, npc.Center.Y, 0, 0, mod.ProjectileType("WispFlame"), 60, 5.0f, 0, 3f, npc.whoAmI);
                Projectile.NewProjectile(npc.Center.X - 40, npc.Center.Y, 0, 0, mod.ProjectileType("WispFlame"), 60, 5.0f, 0, 4f, npc.whoAmI);

            }
            if (spawnTimer == 660)
            {
                hasFlames = false;
                spawnTimer = 0;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            var GlowMask = mod.GetTexture("Glow/WilloWisp_GlowMask");
            var Effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(GlowMask, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, Effects, 0);
        }

        public override void NPCLoot()
        {
            if (Main.netMode != 1)
            {
                int centerX = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int centerY = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                int halfLength = npc.width / 2 / 16 + 1;
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WispLamp"), 1, false, 0, false, false);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofLight, 1, false, 0, false, false);
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            return (Antiaris.NormalSpawn(spawnInfo) && Main.raining && Antiaris.NoZoneAllowWater(spawnInfo)) && spawnInfo.player.ZoneHoly && y < Main.worldSurface ? 0.1f : 0f;
        }
    }
}