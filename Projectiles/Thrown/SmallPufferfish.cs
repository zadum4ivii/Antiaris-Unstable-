using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Antiaris.Projectiles.Thrown
{
    public class SmallPufferfish : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 240;
            projectile.aiStyle = 14;
            aiType = ProjectileID.SpikyBall;
            projectile.tileCollide = true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 10;
            height = 10;
            return true;
        }

        int wetTimer = 0;
        public override void AI()
        {
            int i = (int)projectile.position.X / 16;
            int j = (int)projectile.position.Y / 16;
            projectile.rotation += (float)projectile.direction * 0.01f;
            if (Main.tile[i, j].liquid == 255)
            {
                wetTimer++;
            }
            if (wetTimer == 60)
            {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.owner == Main.myPlayer)
            {
                if (wetTimer < 60)
                    Gore.NewGore(projectile.position, new Vector2(0, 0), mod.GetGoreSlot("Gores/SmallPufferfish"));
                else
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Projectiles/Pufferfish"), projectile.position);
                    int puffer = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Pufferfish"));

                    if (Main.netMode == 1 && puffer >= 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, puffer, 1f);
                    }
                }
            }
        }
    }
}