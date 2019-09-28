using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Projectiles.Minions
{
    public class WispFlame2 : ModProjectile
    {
        private int aiTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 360;
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
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= (int)Main.projFrames[projectile.type])
                projectile.frame = 0;
            if (aiTimer < 60)
            {
                aiTimer++;
            }
            if (aiTimer == 60)
            {
                float CenterX = projectile.Center.X;
                float CenterY = projectile.Center.Y;
                float Distanse = 800f;
                bool CheckDistanse = false;
                for (int MobCounts = 0; MobCounts < 200; MobCounts++)
                {
                    if (Main.npc[MobCounts].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[MobCounts].Center, 1, 1))
                    {
                        float Position1 = Main.npc[MobCounts].position.X + (float)(Main.npc[MobCounts].width / 2);
                        float Position2 = Main.npc[MobCounts].position.Y + (float)(Main.npc[MobCounts].height / 2);
                        float Position3 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - Position1) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - Position2);
                        if (Position3 < Distanse)
                        {
                            Distanse = Position3;
                            CenterX = Position1;
                            CenterY = Position2;
                            CheckDistanse = true;
                        }
                    }
                }
                if (CheckDistanse)
                {
                    float Speed = 20f;
                    Vector2 FinalPos = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float NewPosX = CenterX - FinalPos.X;
                    float NewPosY = CenterY - FinalPos.Y;
                    float FinPos = (float)Math.Sqrt((double)(NewPosX * NewPosX + NewPosY * NewPosY));
                    FinPos = Speed / FinPos;
                    NewPosX *= FinPos;
                    NewPosY *= FinPos;
                    projectile.velocity.X = (projectile.velocity.X * 20f + NewPosX) / 30f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + NewPosY) / 30f;
                    return;
                }
                projectile.tileCollide = false;
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
