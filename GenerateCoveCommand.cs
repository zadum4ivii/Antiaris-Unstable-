using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Commands
{
	public class GenerateCoveCommand : ModCommand
	{
	    public override string Command
		{
			get 
            {
                 return "generateCove";
            }
		}

	    public override string Usage
		{
			get 
            {
                 return "/generateCove";
            }
		}

	    public override CommandType Type
		{
			get 
            {
                 return CommandType.Chat;
            }
		}

	    public override void Action(CommandCaller caller, string input, string[] args)
        {
			Mod mod = ModLoader.GetMod("Antiaris");
            string PirateCoveCommand1 = Language.GetTextValue("Mods.Antiaris.PirateCoveCommand1");
            string PirateCoveCommand3 = Language.GetTextValue("Mods.Antiaris.PirateCoveCommand3");
			if(Main.netMode == 0)
			{
                Main.NewText(PirateCoveCommand1, 255, 0, 21);
                mod.GetModWorld<AntiarisWorld>().AddPirateCove();
			}
			else
			{
				Main.NewText(PirateCoveCommand3, 255, 0, 21);
			}  
        }
	}
}