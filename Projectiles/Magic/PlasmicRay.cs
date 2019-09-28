using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Antiaris.Projectiles.Magic
{
    public class PlasmicRay : ModProjectile
    {
        private float timer = 0;

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 600;
            projectile.alpha = 255;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasmic Ray");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Плазменный луч");
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.position, 0.8f, 0.0f, 0.0f);
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            timer += 1f;
            if (timer > 9f)
            {
                if (timer == 10f)
                {
                    float count = 25.0f;
                    for (int k = 0; (double)k < (double)count; k++)
                    {
                        Vector2 vector2 = (Vector2.UnitX * 0.0f + -Vector2.UnitY.RotatedBy((double)k * (6.22 / (double)count), new Vector2()) * new Vector2(2.0f, 8.0f)).RotatedBy((double)projectile.velocity.ToRotation(), new Vector2());
                        int dust = Dust.NewDust(projectile.Center - new Vector2(0.0f, 4.0f), 1, 1, mod.DustType("BlueLaser"), 0f, 0f, 200, Scale: 1.55f);
                        Main.dust[dust].scale = 1.25f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = projectile.Center + vector2;
                        Main.dust[dust].velocity = projectile.velocity * 0.0f + vector2.SafeNormalize(Vector2.UnitY) * 1.0f;
                    }
                }
                for (int k = 0; k < 1; k++)
                {
                    projectile.position -= projectile.velocity * ((float)k * 0.25f);
                    var dust = Dust.NewDustDirect(projectile.position, 1, 1, mod.DustType("BlueLaser"), 0f, 0f, 200, Scale: 1.55f);
                    dust.position = projectile.position;
                    dust.velocity *= 0.1f;
                }
                return;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Main.myPlayer];
            if (Main.rand.Next(4) == 0)
            {
                var sphere = Projectile.NewProjectile(target.position, new Vector2(0f, 0f), ProjectileID.Electrosphere, projectile.damage, 0, player.whoAmI);
                Main.projectile[sphere].timeLeft = 60;
            }
        }
    }
}