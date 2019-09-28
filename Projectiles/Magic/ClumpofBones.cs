using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Magic
{
    public class ClumpofBones : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 42;
            projectile.aiStyle = 14;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 600;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clump of Bones");
			DisplayName.AddTranslation(GameCulture.Russian, "Глыба костей");
            DisplayName.AddTranslation(GameCulture.Chinese, "骸骨丛");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 25;
			height = 25;
			return true;
		}

        public override void Kill(int timeLeft)
        {
		    Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("BigBone"), 0, 1f, projectile.owner, 0f, (float)Main.rand.Next(-45, 1));
			for (int k = 0; k < 30; ++k)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 1, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, new Color(), 1.8f);
                Main.dust[dust].noGravity = true;
            }
            Main.PlaySound(3, (int)projectile.position.X, (int)projectile.position.Y, 2);
        }
    }
}
