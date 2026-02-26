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
	public class BloodyTear : ModProjectile
	{
		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.BloodShot);

			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 600;
            DrawOffsetX = -(int)((72 / 2) - (Projectile.Size.X / 2));
            DrawOriginOffsetY = -(int)((72 / 2) - (Projectile.Size.Y / 2));

            AIType = ProjectileID.BloodShot;

            //Projectile.rotation = Projectile.velocity.ToRotation() * MathHelper.PiOver4;
        }

        public override void AI() {
            Lighting.AddLight(Projectile.position, 0.8f, 0f, 0f);

            if (Projectile.ai[0] >= 15f)
            {
                Projectile.ai[0] = 15f;
                Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

        }

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hit.Crit)
			{
				if (Projectile.owner == Main.myPlayer)
				{
					int newProj = ModContent.ProjectileType<BloodySlash>();
					int newSlash = Projectile.NewProjectile(Projectile.InheritSource(Projectile),
															Projectile.position,
                                                            Vector2.Zero,
															newProj,
															(int)(Projectile.damage),
															0,
															Projectile.owner);
				}
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.Kill();
			return false;
		}

		public override void OnKill(int timeLeft) {
			for (int k = 0; k < 5; k++) {
				// Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Sparkle>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
				Lighting.AddLight(Projectile.position + Projectile.velocity, 0.5f, 0f, 0f);
			}
			// SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
		}
	}
}