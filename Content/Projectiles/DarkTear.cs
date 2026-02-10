using RoryMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.ComponentModel.DataAnnotations;
using System;

namespace RoryMod.Content.Projectiles
{
	public class DarkTear : ModProjectile
	{
		public override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 600;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            DrawOffsetX = -(int)((72 / 2) - (Projectile.Size.X / 2));
            DrawOriginOffsetY = -(int)((72 / 2) - (Projectile.Size.Y / 2));

            AIType = ProjectileID.Bullet;
			 
			//Projectile.rotation = Projectile.velocity.ToRotation() * MathHelper.PiOver4;
		}
		public override void AI()
  		{
			Lighting.AddLight(Projectile.position, 0.5f, 0f, 0.5f);
			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 0f, Projectile.oldVelocity.Y * 0f);
			
			if (Math.Abs(Projectile.velocity.X) <= 20)
			{
				Projectile.velocity *= 1.05f;
			}
			//float valueChangeX = Math.Abs(Projectile.velocity.X) / (int)Projectile.velocity.X;
            //Projectile.velocity.X += valueChangeX;
  		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if(hit.Crit){
				if (Projectile.owner == Main.myPlayer)
				{
					Vector2 standstill = new Vector2(0, 0);
					int newProj = ModContent.ProjectileType<DarkSlash>();
                    int newSlash = Projectile.NewProjectile(Projectile.InheritSource(Projectile), 
															Projectile.position, 
															standstill, 
															newProj, 
															(int)(Projectile.damage / 2), 
															0, 
															Projectile.owner);
				}
			}
			Projectile.damage = (int)(Projectile.damage * 0.7f);
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.Kill();
			return false;
		}

		public override void OnKill(int timeLeft) {
			for (int k = 0; k < 5; k++) {
				// Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Sparkle>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
				// Lighting.AddLight(Projectile.position, 0.4f, 0f, 0.4f);
			}
			// SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
		}
	}
}