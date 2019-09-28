using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Enemies
{
    public class WispFlame : ModProjectile
    {
        private int aiTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 26;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wisp Flame");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Огонь духа");
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {  
            if (aiTimer <= 240)
            {
                aiTimer++;
                projectile.timeLeft++;
            }
            if (aiTimer == 240)
            {
                projectile.timeLeft = 240;
            }
			projectile.frameCounter++;
			if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= (int)Main.projFrames[projectile.type])
                projectile.frame = 0;
            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.1f);
            if (aiTimer < 240)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].type == mod.NPCType("WilloWisp") && projectile.ai[1] == Main.npc[i].whoAmI)
                    {
                        projectile.direction = Main.npc[i].direction;
                        if (projectile.ai[0] == 1f)
                        {
                            projectile.position.X = Main.npc[i].position.X + 10;
                            projectile.position.Y = Main.npc[i].position.Y + 60;
                        }
                        else if (projectile.ai[0] == 2f)
                        {
                            projectile.position.X = Main.npc[i].position.X + 10;
                            projectile.position.Y = Main.npc[i].position.Y - 40;
                        }
                        else if (projectile.ai[0] == 3f)
                        {
                            projectile.position.X = Main.npc[i].position.X + 60;
                            projectile.position.Y = Main.npc[i].position.Y;
                        }
                        else if (projectile.ai[0] == 4f)
                        {
                            projectile.position.X = Main.npc[i].position.X - 40;
                            projectile.position.Y = Main.npc[i].position.Y;
                        }
                    }
                }
            }
            else
            {
                projectile.rotation = 0;
                Player player = Main.player[Main.myPlayer];

                int num491 = 0;
                int num604 = 0;
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
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 86, projectile.velocity.X, projectile.velocity.Y);
            }
        }
    }
}
