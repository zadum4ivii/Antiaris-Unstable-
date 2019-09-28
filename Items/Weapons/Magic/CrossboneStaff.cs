using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Magic
{
    public class CrossboneStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 44;
            item.magic = true;
            item.mana = 9;
            item.width = 46;
            item.height = 46;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3f;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ClumpofBones");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crossbone Staff");
            Tooltip.SetDefault("Summons an exploding clump of bones");
            DisplayName.AddTranslation(GameCulture.Chinese, "交叉骨杖");
            Tooltip.AddTranslation(GameCulture.Chinese, "召唤一个爆炸骸骨丛");
            DisplayName.AddTranslation(GameCulture.Russian, "Посох перекрещенных костей");
            Tooltip.AddTranslation(GameCulture.Russian, "Призывает взрывающуюся груду костей");
        }
    }
}
