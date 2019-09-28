using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.NPCs.Bosses
{
    [AutoloadBossHead]
    public class DeadlyJones : ModNPC
    {
        /*private bool attack = false;
        private int frame = 0;
        private int frameTimer = 0;
        private int aiTimer = 0;
        private int attackType;
        private bool attackAnim = false;

        float positionX = 0;
        float positionY = 0;
        float playerOldPosX = 0;
        float playerOldPosY = 0;
        float bossOldPosX = 0;
        float bossOldPosY = 0;
        int teleportingTimer = 0;
        int teleportCount = 0;*/

        int frameTimer = 0;
        bool attackAnim = false;
        int aiTimer = 0;
        int frame = 0;
        int tornadoTimer = 0;
        int waterTimer = 0;
        int teleportingTimer = 0;
        int soulTimer = 0;
        float positionX = 0;
        float positionY = 0;
        int attackID = -1;
        float playerOldPosX = 0;
        float playerOldPosY = 0;
        float bossOldPosX = 0;
        float bossOldPosY = 0;

        public override void SetDefaults()
        {
            npc.lifeMax = 8100;
            npc.damage = 30;
            npc.defense = 5;
            npc.knockBackResist = 0f;
            npc.width = 112;
            npc.height = 156;
            npc.HitSound = SoundID.NPCHit49;
            npc.noGravity = true;
            npc.npcSlots = 20f;
            npc.boss = true;
            npc.DeathSound = SoundID.NPCDeath42;
            npc.value = Item.buyPrice(0, 8, 0, 0);
            npc.noTileCollide = true;
            npc.aiStyle = 44;
            aiType = NPCID.FlyingFish;
            bossBag = mod.ItemType("DeadlyJonesTreasureBag");
            music = MusicID.PirateInvasion;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deadly Jones");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Дэдли Джонс");
            Main.npcFrameCount[npc.type] = 10;
        }

        public void OverhaulInit()
        {
            this.SetTag(NPCTags.Boss);
        }

        public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
        {
            if (playerXPlayers > 1)
            {
                npc.lifeMax = (int)(8150 + (double)npc.lifeMax * 0.2 * (double)playerXPlayers);
            }
            else
            {
                npc.lifeMax = 8150;
            }
            npc.damage = (int)(npc.damage * 0.65f);
        }

        /*public override void AI()
        {
            var player = Main.player[npc.target];
            var anyPlayer = Main.player[Main.myPlayer];
            //anyPlayer.
            anyPlayer.AddBuff(BuffID.WaterWalking, 60);
            frameTimer++;
            aiTimer++;
            npc.rotation = 0f;
            npc.direction = npc.spriteDirection;
            if ((double)player.position.X > (double)npc.position.X)
                npc.spriteDirection = 1;
            else if ((double)player.position.X < (double)npc.position.X)
                npc.spriteDirection = -1;

            if (frameTimer % 10 == 0)
            {
                frame++;
                frameTimer = 0;
            }
            if (frame > 4 && !attackAnim)
            {
                frame = 0;
            }
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, 10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }

            if (aiTimer > 240 && !attack)
            {
                attack = true;
                if (aiTimer < 265)
                {
                    attackAnim = true;
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;
                }
                if (attackAnim && frame > 9)
                {
                    frame = 0;
                    attackAnim = false;
                    aiTimer = 0;
                }
            }

            if (attack)
            {
                switch (Main.rand.Next(1))
                {
                    case 0:
                        {
                            teleportingTimer++;
                            if (teleportingTimer == 120)
                            {
                                positionX = npc.position.X;
                                positionY = npc.position.Y;
                            }
                            if (teleportingTimer % 160 == 0)
                            {
                                teleportCount++;
                                Main.NewText("Teleport number" + teleportCount);
                                bossOldPosX = npc.position.X;
                                bossOldPosY = npc.position.Y;

                                playerOldPosX = player.position.X;
                                playerOldPosY = player.position.Y;
                                Main.PlaySound(SoundID.Item8, npc.position);
                                for (int i = 0; i < 10; i++)
                                {
                                    int dust = Dust.NewDust(new Vector2((float)npc.position.X, (float)npc.position.Y), npc.width, npc.height, 56, npc.velocity.X + Main.rand.Next(-10, 10), npc.velocity.Y + Main.rand.Next(-10, 10), 5, npc.color, 2.6f);
                                    Main.dust[dust].noGravity = true;
                                }
                                Main.PlaySound(SoundID.Item8, player.position);
                                for (int i = 0; i < 10; i++)
                                {
                                    int dust = Dust.NewDust(new Vector2((float)player.position.X, (float)player.position.Y), player.width, player.height, 56, player.velocity.X + Main.rand.Next(-10, 10), player.velocity.Y + Main.rand.Next(-10, 10), 5, Color.LightBlue, 2.6f);
                                    Main.dust[dust].noGravity = true;
                                }
                                player.position.X = bossOldPosX;
                                player.position.Y = bossOldPosY;
                                npc.position.X = playerOldPosX;
                                npc.position.Y = playerOldPosY;

                                var velocity = AntiarisHelper.VelocityToPoint(npc.Center, AntiarisHelper.RandomPointInArea(new Vector2(player.Center.X - 10, player.Center.Y - 10), new Vector2(player.Center.X + 20, player.Center.Y + 20)), Main.expertMode ? 16 : 20);
                                var shark = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, velocity.X, velocity.Y, 408, Main.rand.Next(20, 30), 1f);
                                Main.projectile[shark].friendly = false;
                                Main.projectile[shark].hostile = true;
                                Main.projectile[shark].tileCollide = false;
                                Main.projectile[shark].timeLeft = 300;
                            }
                            if (teleportCount == (Main.expertMode ? 7 : 6))
                            {
                                attack = false;
                                teleportCount = 0;
                            }
                        }
                        break;
                }
            }
        }*/

        public override void AI()
        {
            aiTimer++;
            frameTimer++;
            Player player = Main.player[npc.target];
            player.AddBuff(BuffID.WaterWalking, 60);
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, 10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
            npc.rotation = 0f;
            npc.direction = npc.spriteDirection;
            if ((double)player.position.X > (double)npc.position.X)
                npc.spriteDirection = 1;
            else if ((double)player.position.X < (double)npc.position.X)
                npc.spriteDirection = -1;

            if (frameTimer % 10 == 0)
            {
                frame++;
                frameTimer = 0;
            }
            if (frame > 4 && !attackAnim)
            {
                frame = 0;
            }
            if (aiTimer > 160 && aiTimer < 1200)
            {
                if (aiTimer == 161)
                {
                    attackAnim = true;
                    frame = 5;
                }
                if (aiTimer == 167)
                {
                    frame = 6;
                }
                if (aiTimer == 173)
                {
                    frame = 7;
                }
                if (aiTimer == 179)
                {
                    frame = 8;
                }
                if (aiTimer == 185)
                {
                    frame = 9;
                    attackAnim = false;
                }
                waterTimer++;
                teleportingTimer++;
                switch (Main.rand.Next(4))
                {
                    case 0:
                        {
                            if (attackID == -1)
                            {
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                attackID = 0;
                            }
                        }
                        break;
                    case 1:
                        {
                            if (attackID == -1)
                            {
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                attackID = 1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (attackID == -1)
                            {
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                attackID = 2;
                            }
                        }
                        break;
                    case 3:
                        {
                            if (attackID == -1)
                            {
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                attackID = 3;
                            }
                        }
                        break;
                }

                //summons tornados that chase the player & suck him in
                if (attackID == 0)
                {
                    tornadoTimer++;
                    if (tornadoTimer % (Main.expertMode ? 40 : 60) == 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                        NPC.NewNPC((int)((Main.player[npc.target].position.X - 500) + Main.rand.Next(1000)), (int)((Main.player[npc.target].position.Y - 500) + Main.rand.Next(1000)), mod.NPCType("SeaTornado"));
                    }
                }

                //curses the air, starts a countdown, in the end deals 1/3 (1/2 in expert) health damage to the player if he's not in the water
                else if (attackID == 1)
                {
                    string CursedAir = Language.GetTextValue("Mods.Antiaris.CursedAir");
                    waterTimer++;
                    if (waterTimer == 50)
                    {
                        Main.NewText(CursedAir, 76, 255, 246);
                    }
                    if (waterTimer < 760 && waterTimer % 120 == 0)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Cyan, 7 - waterTimer / 120, false, false);
                    }
                    if (waterTimer == 880 && !player.wet)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Red, player.statLife / 3, false, false);
                        player.statLife -= player.statLife / (Main.expertMode ? 2 : 3);
                    }
                }

                //swaps himself & the player, shoots sharks at the player, in the end returns to the beginning position
                if (attackID == 2)
                {
                    teleportingTimer++;
                    if (teleportingTimer == 120)
                    {
                        positionX = npc.position.X;
                        positionY = npc.position.Y;
                    }
                    if (teleportingTimer % 160 == 0)
                    {
                        bossOldPosX = npc.position.X;
                        bossOldPosY = npc.position.Y;

                        playerOldPosX = player.position.X;
                        playerOldPosY = player.position.Y;
                        Main.PlaySound(SoundID.Item8, npc.position);
                        for (int i = 0; i < 10; i++)
                        {
                            int dust = Dust.NewDust(new Vector2((float)npc.position.X, (float)npc.position.Y), npc.width, npc.height, 56, npc.velocity.X + Main.rand.Next(-10, 10), npc.velocity.Y + Main.rand.Next(-10, 10), 5, npc.color, 2.6f);
                            Main.dust[dust].noGravity = true;
                        }
                        Main.PlaySound(SoundID.Item8, player.position);
                        for (int i = 0; i < 10; i++)
                        {
                            int dust = Dust.NewDust(new Vector2((float)player.position.X, (float)player.position.Y), player.width, player.height, 56, player.velocity.X + Main.rand.Next(-10, 10), player.velocity.Y + Main.rand.Next(-10, 10), 5, Color.LightBlue, 2.6f);
                            Main.dust[dust].noGravity = true;
                        }
                        player.position.X = bossOldPosX;
                        player.position.Y = bossOldPosY;
                        npc.position.X = playerOldPosX;
                        npc.position.Y = playerOldPosY;

                        var velocity = AntiarisHelper.VelocityToPoint(npc.Center, AntiarisHelper.RandomPointInArea(new Vector2(player.Center.X - 10, player.Center.Y - 10), new Vector2(player.Center.X + 20, player.Center.Y + 20)), Main.expertMode ? 16 : 20);
                        var shark = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, velocity.X, velocity.Y, 408, Main.rand.Next(20, 30), 1f);
                        Main.projectile[shark].friendly = false;
                        Main.projectile[shark].hostile = true;
                        Main.projectile[shark].tileCollide = false;
                        Main.projectile[shark].timeLeft = 300;
                    }
                }

                //steals player's soul, if the soul gets to Deadly Jones, he damages the player for 1/2 of health & restores his health for same amount
                //stay near soul to return it
                if (attackID == 3)
                {
                    string SoulStolen = Language.GetTextValue("Mods.Antiaris.SoulStolen");
                    soulTimer++;
                    if (soulTimer == 60)
                    {
                        Main.PlaySound(SoundID.Item8, npc.position);
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Cyan, SoulStolen, false, false);
                        var velocity = AntiarisHelper.VelocityToPoint(player.Center, AntiarisHelper.RandomPointInArea(new Vector2(npc.Center.X - 10, npc.Center.Y - 10), new Vector2(npc.Center.X + 20, npc.Center.Y + 20)), Main.expertMode ? 1 : 2);
                        var soul = Projectile.NewProjectile(player.Center.X, player.Center.Y, velocity.X, velocity.Y, mod.ProjectileType("StolenSoul"), 0, 0f, Main.myPlayer);
                    }
                }
            }
            if (aiTimer == 600)
            {
                aiTimer = 0;
                tornadoTimer = 0;
                waterTimer = 0;
                teleportingTimer = 0;
                soulTimer = 0;
                attackID = -1;
            }
            if (!player.ZoneBeach)
            {
                string LeavingBeach = Language.GetTextValue("Mods.Antiaris.LeavingBeach");
                Main.NewText(LeavingBeach);
                Main.PlaySound(SoundID.Item8, player.position);
                for (int i = 0; i < 10; i++)
                {
                    int dust = Dust.NewDust(new Vector2((float)player.position.X, (float)player.position.Y), player.width, player.height, 56, player.velocity.X + Main.rand.Next(-10, 10), player.velocity.Y + Main.rand.Next(-10, 10), 5, Color.LightBlue, 2.6f);
                    Main.dust[dust].noGravity = true;
                }
                player.position.X = npc.position.X;
                player.position.Y = npc.position.Y;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * frame;
        }
    }
}