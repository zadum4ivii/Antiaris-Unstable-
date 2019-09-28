using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Antiaris.Tiles.Decorations;

namespace Antiaris.UIs
{
	/// <summary>
	/// Description of NixieTubeUI.
	/// </summary>
	public class NixieTubeUI : UIState
	{
		public UIPanel MainPanel;
		public static NixieTube assignedTile;
		public static NixieButton button;
		public static NixieTubeSlot slot;
		public static bool visible;
        public static int cordX;
        public static int cordY;

		public static NixieTubeEntity entity = new NixieTubeEntity();
		public override void OnInitialize()
		{
			entity = new NixieTubeEntity();


			MainPanel = new UIPanel();
			MainPanel.Height.Set(Language.ActiveCulture == GameCulture.Russian ? 370 : 220, 0);
			MainPanel.Width.Set(340, 0);
			MainPanel.SetPadding(0f);
			MainPanel.Top.Set(Main.instance.invBottom + 60, 0);
			MainPanel.Left.Set(Main.screenWidth / 2 - 510, 0);

			int xPos = 0;
            int yPos = 0;
			for(int i = 0; i < (Language.ActiveCulture == GameCulture.Russian ? 73 : 41); i++)
			{
				button = new NixieButton();
                button.Width.Set(30f, 0f);
                button.Height.Set(48f, 0f);
                button.Top.Set(10f + yPos, 0);
                button.Left.Set(6f + xPos, 0f);
                xPos += 30;
                button.Index = i;
                button.SetPadding(0f);
                MainPanel.Append(button);
                if(xPos >= 330)
                {
                    xPos = 0;
                    yPos += 50;
                }
            }
            var button2 = new UIDisable();
            button2.Top.Set(Language.ActiveCulture == GameCulture.Russian ? 330 : 180, 0);
            button2.Left.Set(310, 0f);
            button2.SetPadding(0f);
            MainPanel.Append(button2);

			slot = new NixieTubeSlot(0);
			slot.Top.Set(20, 0);
			slot.Left.Set(290, 0);
			slot.VAlign = 1;
			MainPanel.Append(slot);

			slot = new NixieTubeSlot(1);
			slot.Top.Set(20, 0);
			slot.Left.Set(260, 0);
			slot.VAlign = 1;
			MainPanel.Append(slot);

			base.Append(MainPanel);
		}

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
			Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
            if (MainPanel.ContainsPoint(MousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            Recalculate();
        }
    }
}
