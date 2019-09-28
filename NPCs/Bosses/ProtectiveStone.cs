using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.NPCs.Bosses
{
    public class ProtectiveStone : ModNPC
    {
        public float rot;
        public Vector2 rotVec = new Vector2(0.0f, 170.0f);

        public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1);
            npc.damage = (int)(npc.damage * 1);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Protective Stone");
            DisplayName.AddTranslation(GameCulture.Chinese, "护佑魔石");
            DisplayName.AddTranslation(GameCulture.Russian, "Защитный камень");
        }

        public void OverhaulInit()
        {
            this.SetTag(NPCTags.NoStuns);
        }

        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 42;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.damage = 0;
            npc.lifeMax = 180;
			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit42;
            npc.DeathSound = SoundID.NPCDeath44;
        }

        public override bool PreAI()
        {
            npc.TargetClosest(true);
            int boss = (int)npc.ai[0];
            if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != mod.NPCType("TowerKeeper"))
            {
                npc.active = false;
                return false;
            }
            this.rot += 0.02f;
            npc.netUpdate = true;
            Vector2 v = Main.npc[boss].Center - npc.Center;
            v.Normalize();
            v *= 9f;
            npc.rotation = Utils.ToRotation(v);
            NPC npc2 = Main.npc[(int)npc.ai[0]];
            npc.Center = npc2.Center + AntiarisHelper.RotateVector(new Vector2(), this.rotVec, this.rot + npc.ai[2] * 0.628f);
			npc.ai[3]++;
			if ((double)npc.ai[3] % 10.0 == 1.0 && Main.rand.Next(2) == 0 && Main.netMode != 1)
			{
				float speed = (float)(3.0 + (double)Main.rand.NextFloat() * 6.0);
				Vector2 start = Vector2.UnitY.RotatedByRandom(6.28318548202515);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, start.X * speed, start.Y * speed, mod.ProjectileType("Energy"), 15, 0.0f, Main.myPlayer, Main.npc[boss].Center.X, Main.npc[boss].Center.Y - 42f);
			}
			return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.velocity /= 2.0f;
            projectile.damage /= 2;
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
