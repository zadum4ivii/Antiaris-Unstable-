using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Antiaris
{
    public class AntiarisBuff : GlobalBuff
    {
        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            if (type == BuffID.OnFire && npc.type == mod.NPCType("HoneySlime"))
            {
                Main.PlaySound(SoundID.LiquidsHoneyLava, npc.position);
                npc.Transform(mod.NPCType("CrispyHoneySlime"));
            }
			
			if (type == BuffID.OnFire && npc.type == mod.NPCType("HoneyGolem"))
            {
                Main.PlaySound(SoundID.LiquidsHoneyLava, npc.position);
                npc.Transform(mod.NPCType("CrispyHoneyGolem"));
            }
        }
    }
}