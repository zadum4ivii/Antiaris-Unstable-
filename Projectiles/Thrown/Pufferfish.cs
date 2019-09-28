using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Antiaris.Projectiles.Thrown
{
    public class Pufferfish : ModProjectile
    {
        private const float maxTicks = 25f;
        private const int alphaReduction = 25;

        public bool isStickingToTarget
        {
            get { return projectile.ai[0] == 1f; }
            set { projectile.ai[0] = value ? 1f : 0f; }
        }

        public float targetWhoAmI
        {
            get { return projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 130;
            projectile.aiStyle = -1;
            aiType = ProjectileID.Shuriken;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
            DisplayName.SetDefault("Pufferfish");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Иглобрюх");
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 10;
            height = 10;
            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            isStickingToTarget = true;
            targetWhoAmI = (float)target.whoAmI;
            projectile.velocity = (target.Center - projectile.Center) * 0.75f;
            projectile.netUpdate = true;
            target.AddBuff(169, 300);

            projectile.damage = 0;
            int maxStickingJavelins = 4;
            Point[] stickingJavelins = new Point[maxStickingJavelins];
            int javelinIndex = 0; 
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != projectile.whoAmI
                    && currentProjectile.active
                    && currentProjectile.owner == Main.myPlayer
                    && currentProjectile.type == projectile.type
                    && currentProjectile.ai[0] == 1f
                    && currentProjectile.ai[1] == (float)target.whoAmI
                )
                {
                    stickingJavelins[javelinIndex++] =
                        new Point(i, currentProjectile.timeLeft);
                    if (javelinIndex >= stickingJavelins.Length
                    ) 
                    {
                        break;
                    }
                }
            }
            if (javelinIndex >= stickingJavelins.Length)
            {
                int oldJavelinIndex = 0;
                for (int i = 1; i < stickingJavelins.Length; i++)
                {
                    if (stickingJavelins[i].Y < stickingJavelins[oldJavelinIndex].Y)
                    {
                        oldJavelinIndex = i;
                    }
                }
                Main.projectile[stickingJavelins[oldJavelinIndex].X].Kill();
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Projectiles/Pufferfish"), projectile.position);
        }

        public override void AI()
        {
            projectile.rotation += (float)projectile.direction * 0.2f;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= alphaReduction;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (!isStickingToTarget)
            {
                targetWhoAmI += 1f;
                if (targetWhoAmI >= maxTicks)
                {
                    float velXmult = 1f; 
                    float velYmult = 1f; 
                    targetWhoAmI = maxTicks;
                    projectile.velocity.X = projectile.velocity.X * velXmult;
                    projectile.velocity.Y = projectile.velocity.Y + velYmult;
                }
            }
            if (isStickingToTarget)
            {
                projectile.frame = 1;
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
                projectile.ignoreWater = true; 
                projectile.tileCollide = false; 
                int aiFactor = 15; 
                bool killProj = false;
                bool hitEffect = false; 
                projectile.localAI[0] += 1f;
                hitEffect = projectile.localAI[0] % 30f == 0f;
                int projTargetIndex = (int)targetWhoAmI;
                Main.npc[projTargetIndex].AddBuff(169, 60, false);
                if (projectile.localAI[0] >= (float)(60 * aiFactor)
                || (projTargetIndex < 0 || projTargetIndex >= 200)) 
                {
                    killProj = true;
                }
                else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
                {
                    projectile.Center = Main.npc[projTargetIndex].Center - projectile.velocity * 2f;
                    projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
                    if (hitEffect)
                    {
                        var direction = Main.npc[projTargetIndex].direction;
                        if (Main.npc[projTargetIndex].velocity.X < 0f)
                        {
                            direction = -1;
                        }
                        if (Main.npc[projTargetIndex].velocity.X > 0f)
                        {
                            direction = 1;
                        }
                        Main.npc[projTargetIndex].StrikeNPC((int)11, 1f, direction, false, false, false);
                        if (Main.netMode != 0)
                            NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), Main.npc[projTargetIndex].whoAmI, (float)1, 1f, (float)direction, 11);
                    }
                }
                else
                {
                    killProj = true;
                }

                if (killProj)
                {
                    projectile.Kill();
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];

            if ((int)Main.tile[(int)projectile.position.X / 16, (int)projectile.position.Y / 16].liquid == 0 || (int)Main.tile[(int)projectile.position.X / 16, (int)projectile.position.Y / 16].liquidType() == 0)
            { 
                Main.tile[(int)projectile.position.X / 16, (int)projectile.position.Y / 16].liquidType(0);
                Main.tile[(int)projectile.position.X / 16, (int)projectile.position.Y / 16].liquid = 255;
                WorldGen.SquareTileFrame((int)projectile.position.X / 16, (int)projectile.position.Y / 16, true);
                if (Main.netMode == 1)
                    NetMessage.sendWater((int)projectile.position.X / 16, (int)projectile.position.Y / 16);
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 85);
            if (projectile.owner == Main.myPlayer)
            {
                int amount = 4 + Main.rand.Next(3);
                for (int i = 0; i < amount; i++)
                {
                    Vector2 value17 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    value17.Normalize();
                    value17 *= (float)(100) * 0.03f;
                    var bubble = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, value17.X, value17.Y, mod.ProjectileType("Bubble"), 10, 1f, Main.myPlayer, 0f);
                    Main.projectile[bubble].tileCollide = false;
                }
                Vector2 pos = new Vector2((int)projectile.position.X, (int)projectile.position.Y) + new Vector2(projectile.width, projectile.height) / 2f;
                var fish = Projectile.NewProjectile(pos.X, pos.Y, 0, 0, mod.ProjectileType("SmallPufferfish"), 0, 0f, player.whoAmI, 0f);
                Main.projectile[fish].rotation = Main.projectile[fish].velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
