using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Enemies
{
    public class WrathSkull : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 44;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath Skull");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Череп гнева");
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.6f, 0.3f);
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= (int)Main.projFrames[projectile.type])
                projectile.frame = 0;

            projectile.spriteDirection = -projectile.direction;

            Player player = Main.player[Main.myPlayer];

            int num491 = (int)projectile.ai[0];
            int num604 = (int)projectile.ai[0];
            float num605 = 4f;
            Vector2 vector44 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num606 = Main.player[num604].Center.X - vector44.X;
            float num607 = Main.player[num604].Center.Y - vector44.Y;
            float num608 = (float)Math.Sqrt((double)(num606 * num606 + num607 * num607));
            if (num608 < 50f && projectile.position.X < Main.player[num604].position.X + (float)Main.player[num604].width && projectile.position.X + (float)projectile.width > Main.player[num604].position.X && projectile.position.Y < Main.player[num604].position.Y + (float)Main.player[num604].height && projectile.position.Y + (float)projectile.height > Main.player[num604].position.Y)
            {
                projectile.Kill();
            }
            num608 = num605 / num608;
            num606 *= num608;
            num607 *= num608;
            projectile.velocity.X = (projectile.velocity.X * 15f + num606) / 16f;
            projectile.velocity.Y = (projectile.velocity.Y * 15f + num607) / 16f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, projectile.velocity.X, projectile.velocity.Y);
            }
        }
    }
}
