using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Thrown
{
    public class Beehive : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 14;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 180;
            projectile.extraUpdates = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beehive");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Пчелиный улей");
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 10;
            height = 10;
            return true;
        }


        public override void Kill(int timeLeft)
        {
            //vanilla code
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int index1 = 0; index1 < 20; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2((float)projectile.position.X, (float)projectile.position.Y), projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, Color.White, 1.5f);
                Dust dust = Main.dust[index2];
                dust.velocity = dust.velocity * 1f;
            }
            int index6 = Gore.NewGore(new Vector2((float)projectile.position.X, (float)projectile.position.Y), new Vector2(0, 0), Main.rand.Next(61, 64), 1f);
            Gore gore4 = Main.gore[index6];
            gore4.velocity = gore4.velocity * 0.3f;
            if (projectile.owner == Main.myPlayer)
            {
                int num = Main.rand.Next(15, 25);
                for (int index1 = 0; index1 < num; ++index1)
                    Projectile.NewProjectile((float)projectile.position.X, (float)projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.02f, (float)Main.rand.Next(-35, 36) * 0.02f, 189, Main.player[projectile.owner].beeDamage(projectile.damage), Main.player[projectile.owner].beeKB(0.0f), Main.myPlayer, 0.0f, 0.0f);
            }
        }
    }
}
