using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaOverhaul;

namespace Antiaris.NPCs.Bosses
{
    public class SeaTornado : ModNPC
    {
        int frame = 0;
        int timer = 0;
        int timer2 = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sea Tornado");
            DisplayName.AddTranslation(GameCulture.Chinese, "海龙卷风");
            DisplayName.AddTranslation(GameCulture.Russian, "Морское торнадо");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.damage = 15;
            npc.lifeMax = 80;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.aiStyle = 0;
        }

        public void OverhaulInit()
        {
            this.SetTag(NPCTags.NoStuns);
        }

        public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1);
            npc.damage = (int)(npc.damage * 1);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * frame;
        }

        public override void AI()
        {
            var player = Main.player[npc.target];
            timer++;
            timer2++;
            if (timer2 == 120)
            {
                aiType = NPCID.Wraith;
                npc.aiStyle = 14;
                npc.velocity.X = (Main.expertMode ? 6f : 5f) * npc.direction;
            }
            if (timer == 5)
            {
                if (frame < 5)
                    frame++;
                else
                    frame = 0;
                timer = 0;
            }
            npc.netUpdate = true;
            npc.TargetClosest(false);
            npc.ai[1] = 0;
            npc.ai[2] = 0;
            if (npc.ai[1] != 1)
            {
                if (npc.position.X < Main.player[npc.target].position.X)
                {
                    npc.direction = 1;
                    npc.spriteDirection = npc.direction;
                }
                if (npc.position.X > Main.player[npc.target].position.X)
                {
                    npc.direction = -1;
                    npc.spriteDirection = npc.direction;
                }
            }
            if ((!player.dead || player.active) && npc.Hitbox.Intersects(player.Hitbox))
            {
                npc.TargetClosest(true);
                npc.target = 0;
                npc.ai[1] = 1;
                npc.spriteDirection = npc.direction;
                player.mount.Dismount(player);
                player.controlHook = false;
                player.controlUseItem = false;
                for (var i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].aiStyle == 7)
                    {
                        Main.projectile[i].Kill();
                    }
                }
                player.position.X = npc.position.X;
                player.position.Y = npc.position.Y;
                player.gfxOffY = npc.gfxOffY;
            }
            if (player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
        }
    }
}