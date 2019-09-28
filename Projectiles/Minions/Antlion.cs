using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Minions
{
    public class Antlion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 52;  
            projectile.hostile = false;  
            projectile.friendly = true;
            projectile.netUpdate = true;
            projectile.ignoreWater = true; 
            projectile.timeLeft = 7200;  
            projectile.penetrate = -1; 
            projectile.tileCollide = true;
            projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			Main.projFrames[projectile.type] = 2;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antlion");
            DisplayName.AddTranslation(GameCulture.Chinese, "蚁狮");
            DisplayName.AddTranslation(GameCulture.Russian, "Муравьиный лев");
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player owner = null;
			if (projectile.owner != -1)
			{
				owner = Main.player[projectile.owner];
			}
			else if (projectile.owner == 255)
			{
				owner = Main.LocalPlayer;
			}
			var player = owner;
			var aPlayer = player.GetModPlayer<AntiarisPlayer>(mod);
			if (aPlayer.necromancerSet && target.life <= 0)
			{
				int newLife = player.statLifeMax / 20;
				if (newLife > 0)
				{
					player.statLife += newLife;
					player.HealEffect(newLife);
					NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, projectile.owner, newLife);
				}
			}
			if (aPlayer.shadowflameCharm && Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.ShadowFlame, 120);
			}
		}

		public override void AI()
        {
            Player owner = null;
            if (projectile.owner != -1)
            {
                owner = Main.player[projectile.owner];
            }
            else if (projectile.owner == 255)
            {
                owner = Main.LocalPlayer;
            }
            var player = owner;
            projectile.velocity.Y = 10f;
            ++projectile.frameCounter;
            if (projectile.frameCounter > 5)
            {
                ++projectile.frame;
                projectile.frameCounter = 0;
            }
            if (projectile.frame < 2)
                return;
            projectile.frame = 0;
            ++projectile.ai[1];
            if ((double)projectile.ai[1] >= 0.0)
            {
                Main.PlaySound(4, (int)projectile.position.X, (int)projectile.position.Y, 9);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0.9f, -4.5f, mod.ProjectileType("Sand"), projectile.damage, 1f, projectile.owner, 0.0f, (float)projectile.whoAmI);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0.0f, -4.5f, mod.ProjectileType("Sand"), projectile.damage, 1f, projectile.owner, 0.0f, (float)projectile.whoAmI);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -0.9f, -4.5f, mod.ProjectileType("Sand"), projectile.damage, 1f, projectile.owner, 0.0f, (float)projectile.whoAmI);
                projectile.ai[1] = -10.0f;
            }
            projectile.netUpdate = true;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 2)
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(4, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0.0f);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
