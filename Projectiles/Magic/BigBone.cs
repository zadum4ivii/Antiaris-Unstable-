using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Magic
{
    public class BigBone : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 14;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big Bone");
            DisplayName.AddTranslation(GameCulture.Russian, "Большая кость");
            DisplayName.AddTranslation(GameCulture.Chinese, "骸骨");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 10;
            height = 5;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int j = -1; j <= 1; j += 2)
                Projectile.NewProjectile(projectile.Center, Vector2.Zero - new Vector2(-0.16f * -(float)j, 7f), 532, 25, projectile.knockBack, projectile.owner);
            for (int k = 0; k < 30; ++k)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 1, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, new Color(), 1.8f);
                Main.dust[dust].noGravity = true;
            }
            Main.PlaySound(3, (int)projectile.position.X, (int)projectile.position.Y, 2);
        }
    }
}

