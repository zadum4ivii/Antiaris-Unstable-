using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Equipables.Accessories
{
    public class BeePheromones : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 38;
            item.accessory = true;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee Pheromones");
			Tooltip.SetDefault("Bees and hornets become friendly\nHornets become aggressive if attacked");
            DisplayName.AddTranslation(GameCulture.Russian, "Пчелиные феромоны");
            Tooltip.AddTranslation(GameCulture.Russian, "Пчелы и шершни становятся дружелюбными\nШершни становятся агрессивными, если атакованы");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AntiarisPlayer>(mod).beePheromones = true;
        }
    }
}
