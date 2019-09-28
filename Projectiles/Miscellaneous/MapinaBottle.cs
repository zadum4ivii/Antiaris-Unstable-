using Terraria.ModLoader;
using Terraria;

namespace Antiaris.Projectiles.Miscellaneous
{
    public class MapinaBottle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.aiStyle = 2;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 10;
            height = 10;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 107);
            for (int i = 0; i < 3; i++)
            {
                Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 704, 1f);
                Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 705, 1f);
            }
            Item.NewItem(projectile.position, mod.ItemType("DavysMap"), 1, false, 0, false, false);
            Item.NewItem(projectile.position, mod.ItemType("Note2"), 1, false, 0, false, false);
        }
    }
}
