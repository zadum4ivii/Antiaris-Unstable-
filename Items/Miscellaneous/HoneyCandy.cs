using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Miscellaneous
{
    public class HoneyCandy : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Star);
            item.width = 18;
            item.height = 18;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Candy");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Медовая конфетка");
        }

        public override bool ItemSpace(Player player)
        {
            return true;
        }

        public override bool CanPickup(Player player)
        {
            return true;
        }

        public override bool OnPickup(Player player)
        {
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 2);
            player.statLife += 10;
            player.AddBuff(BuffID.Honey, 300);
            if (Main.myPlayer == player.whoAmI)
                player.HealEffect(10);
            return false;
        }
    }
}
