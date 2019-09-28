using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Antiaris.Projectiles.Bosses
{
    public class StolenSoul : ModProjectile
    {
        private bool intersects = false;
        private int returnTimer = 0;

        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 7;
            projectile.width = 34;
            projectile.height = 38;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.damage = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            string SoulReturned = Language.GetTextValue("Mods.Antiaris.SoulReturned");
            Player owner = null;
            if (projectile.owner != -1)
            {
                owner = Main.player[projectile.owner];
            }
            else if (projectile.owner == 255)
            {
                owner = Main.LocalPlayer;
            }
            var player = owner;
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 3)
            {
                projectile.frame = 0;
            }

            var ProjRectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, 28, 30);
            var NPCRectangle = new Rectangle();
            var PlayerRectangle = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
            foreach (NPC npc in Main.npc)
            {
                if (npc.type == mod.NPCType("DeadlyJones"))
                {
                    NPCRectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, 40, 58);
                }
                if (ProjRectangle.Intersects(NPCRectangle))
                {
                    if (!intersects)
                    {
                        intersects = true;
                        projectile.Kill();
                        int heal = player.statLife / 2;
                        CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), Color.LightGreen, heal, false, false);
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Red, heal, false, false);
                        player.statLife -= heal;
						if (npc.life < npc.lifeMax)
							npc.life += heal;
                    }
                }
                if (ProjRectangle.Intersects(PlayerRectangle))
                {
                    returnTimer++;
                    if (returnTimer == 6000)
                    {
                        projectile.Kill();
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Cyan, SoulReturned, false, false);
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            for (var k = 0; k < 25; k++)
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 56, projectile.oldVelocity.X * 0.2f, projectile.oldVelocity.Y * 0.2f);
            Main.PlaySound(4, (int)projectile.position.X, (int)projectile.position.Y, 6);
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
