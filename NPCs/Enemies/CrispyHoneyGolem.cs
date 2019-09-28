using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Antiaris.NPCs.Enemies
{
    public class CrispyHoneyGolem : ModNPC
    {
        private int frame = 0;
        private int timer = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 300;
            npc.damage = 75;
            npc.defense = 40;
            npc.knockBackResist = 0.1f;
            npc.width = 46;
            npc.height = 46;
            npc.aiStyle = 3;
            aiType = 73;
            animationType = 48;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit38;
            npc.DeathSound = SoundID.NPCDeath22;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            bannerItem = mod.ItemType("HoneyGolemBanner");
            banner = npc.type;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crispy Honey Golem");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Хрустящий медовый голем");
            Main.npcFrameCount[npc.type] = 7;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1);
            npc.damage = (int)(npc.damage * 1);
        }

        public override void AI()
        {
            if (npc.velocity.Y == 0)
            {
                timer++;
                if (timer == 10)
                {
                    timer = 0;
                    frame++;
                }
                if (frame > 6)
                {
                    frame = 1;
                }
            }
            else
            {
                frame = 0;
            }
            npc.spriteDirection = npc.direction;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * frame;
        }

        public override void NPCLoot()
        {
            if (Main.netMode != 1)
            {
                int centerX = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int centerY = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                int halfLength = npc.width / 2 / 16 + 1;
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HoneyExtract"), Main.rand.Next(4, 6), false, 0, false, false);
                }
                if (Main.rand.Next(20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HiveSwatter"), 1, false, 0, false, false);
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CrispyHoneyBlock, Main.rand.Next(10, 15), false, 0, false, false);
            }
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(4) == 0)
            {
                int num220 = Main.rand.Next(6, 8);
                for (int num221 = 0; num221 < num220; num221++)
                {
                    Vector2 value17 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    value17.Normalize();
                    value17 *= (float)(100) * 0.06f;
                    var honey = Projectile.NewProjectile(npc.position.X, npc.position.Y, value17.X, value17.Y, mod.ProjectileType("HoneySpray"), 25, 1f, 0, 0f);
                    Main.projectile[honey].friendly = false;
                    Main.projectile[honey].hostile = true;
                }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(4) == 0)
            {
                int num220 = Main.rand.Next(6, 8);
                for (int num221 = 0; num221 < num220; num221++)
                {
                    Vector2 value17 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    value17.Normalize();
                    value17 *= (float)(100) * 0.06f;
                    var honey = Projectile.NewProjectile(npc.position.X, npc.position.Y, value17.X, value17.Y, mod.ProjectileType("HoneySpray"), 30, 1f, 0, 0f);
                    Main.projectile[honey].friendly = false;
                    Main.projectile[honey].hostile = true;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 138, 0f, 0f, 50, default(Color), 1.5f);
                    Main.dust[dust].velocity *= 2f;
                    Main.dust[dust].noGravity = true;
                }
                if ((int)Main.tile[(int)npc.position.X / 16, (int)npc.position.Y / 16].liquid == 0 || (int)Main.tile[(int)npc.position.X / 16, (int)npc.position.Y / 16].liquidType() == 0)
                {
                    Main.tile[(int)npc.position.X / 16, (int)npc.position.Y / 16].liquidType(2);
                    Main.tile[(int)npc.position.X / 16, (int)npc.position.Y / 16].liquid = 255;
                    WorldGen.SquareTileFrame((int)npc.position.X / 16, (int)npc.position.Y / 16, true);
                    if (Main.netMode == 1)
                        NetMessage.sendWater((int)npc.position.X / 16, (int)npc.position.Y / 16);
                }
            }
        }
    }
}