using Terraria; 
using Terraria.ModLoader; 
using Terraria.Graphics.Effects; 

namespace Antiaris.Projectiles.Miscellaneous
{ 
	public class TimeWave : ModProjectile 
	{ 
		private int rippleCount = 10; 
		private int rippleSize = 1; 
		private int rippleSpeed = 3; 
		private float distortStrength = 900f; 
		
		public override void SetDefaults() 
		{ 
			projectile.width = 30; 
			projectile.height = 30; 
			projectile.light = 0.9f; 
			projectile.penetrate = -1; 
			projectile.timeLeft = 120; 
			projectile.friendly = true; 
			projectile.tileCollide = false; 
			aiType = 24; 
		} 
		
		public override void AI() 
		{ 
			projectile.position = Main.player[projectile.owner].position;
			if (!Filters.Scene["Shockwave"].IsActive()) 
			{
				Filters.Scene.Activate("Shockwave", projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(projectile.Center); 
			} 
			float progress = (180f - projectile.timeLeft) / 60f; 
			Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f)); 
		} 

		public override void Kill(int timeLeft) 
		{ 
			Filters.Scene["Shockwave"].Deactivate(); 
		} 
	} 
}