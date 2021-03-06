using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Magic
{
    public class NatureBeam : ModProjectile
    {
        private float timer = 0;

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = 2;
            projectile.extraUpdates = 100;
            projectile.alpha = 255;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Beam");
            DisplayName.AddTranslation(GameCulture.Chinese, "自然光线");
            DisplayName.AddTranslation(GameCulture.Russian, "Природный луч");
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
                for (int k = 0; k < 1; k++)
                {
                    projectile.position -= projectile.velocity * ((float)k * 0.25f);
                    var dust = Dust.NewDustDirect(projectile.position, 1, 1, mod.DustType("NatureLaser"), 0f, 0f, 200, Scale: 1.55f);
                    dust.position = projectile.position;
                    dust.velocity *= 0.1f;
                }
                return;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.penetrate++;
        }
    }
}