﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Minions
{
    public class TrueTreacherousEnergy : ModProjectile
    {
        public override void SetDefaults()
        {
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 0;
			projectile.tileCollide = false;
			projectile.timeLeft = 350;
            projectile.penetrate = 2;
			projectile.alpha = 255;
            projectile.friendly = true;
            projectile.minion = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy");
            DisplayName.AddTranslation(GameCulture.Russian, "Энергия");
			DisplayName.AddTranslation(GameCulture.Chinese, "能量");
            Main.projFrames[projectile.type] = 1;
        }

        public override void AI()
        {
            Vector2 vector = Main.npc[(int)projectile.ai[0]].Center - projectile.Center;
            if ((double)projectile.timeLeft < 275.0) projectile.Kill();
			if ((double)vector.Length() < (double)projectile.velocity.Length()) projectile.Kill();
			else
			{
                vector.Normalize();
				projectile.velocity = Vector2.Lerp(projectile.velocity, vector * 11.2f, 0.1f);
				for (int k = 0; k < 3; k++)
				{
					int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.GoldFlame, 0.0f, 0.0f, 100, new Color(), 1f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position += new Vector2(4f);
					Main.dust[dust].scale += Main.rand.NextFloat() * 1.0f;
				}
			}
            projectile.netUpdate = true;
        }
    }
}
