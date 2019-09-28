using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Miscellaneous
{
	public class HeavenLight : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.alpha = 300;
			projectile.timeLeft = 1200;
			projectile.penetrate = -1;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

        //thanks to Hastur for this code
		public override void AI()
		{
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
			
			for (int num614 = 0; num614 < 5; num614++)
			{
				float num615 = projectile.velocity.X * 0.2f * (float)num614;
				float num616 = -(projectile.velocity.Y * 0.2f) * (float)num614;
				int num617 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 64, 0f, 0f, 100, default(Color), 1.1f);
				Main.dust[num617].noGravity = true;
				Main.dust[num617].velocity *= 0f;
				Dust dust61 = Main.dust[num617];
				dust61.position.X = dust61.position.X - num615;
				Dust dust62 = Main.dust[num617];
				dust62.position.Y = dust62.position.Y - num616;
			}
		}
	}
}