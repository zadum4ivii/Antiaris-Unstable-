using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace Antiaris
{
    public static class Util
    {
        public static bool ConsumeAmmo(ref Player player, short ammo)
        {
            for (int i = 0; i < player.inventory.Length; i++)
            {
                Item item = player.inventory[i];
                if (item.type == ammo && item.stack > 0)
                {
                    item.stack--;
                    if (item.stack <= 0)
                    {
                        item = new Item();
                    }
                    return true;
                }
            }
            return false;
        }

        public static Vector2 DistanceToMouse(Player player, float speed)
        {
            return (Main.MouseWorld - player.Center).SafeNormalize(-Vector2.UnitY) * speed;
        }

        public static Vector2 MuzzleOffsets(Vector2 position, float speedX, float speedY, float offset)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * offset;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                return position + muzzleOffset;
            }
            return position;
        }

        public static void StayNearPosition(Projectile proj, Vector2 pos)
        {
            proj.position = pos;
            proj.Center = pos;
        }

        public static void ChanneledProjectile(Player p, Projectile projectile)
        {
            int dir = projectile.direction;
            p.ChangeDir(dir);
            p.itemAnimation = 2;
            p.itemTime = 2;
            p.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);
            p.heldProj = projectile.whoAmI;
        }
    }
}