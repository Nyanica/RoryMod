using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoryMod.Content.Dusts;
using System;
using System.ComponentModel.DataAnnotations;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RoryMod.Content.Projectiles
{
    public class BloodySlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            Main.projFrames[Projectile.type] = 28;
        }
        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.DemonScythe);
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.scale = 0.8f;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 31;
            Projectile.penetrate = -1;
            Projectile.alpha = 80;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 15;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ArmorPenetration = 10;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            //Projectile.aiStyle = ProjAIStyleID.Sickle;
            // def draw offset
            DrawOffsetX = -(int)((56 / 2) - (Projectile.Size.X / 2));
            DrawOriginOffsetY = -(int)((56 / 2) - (Projectile.Size.Y / 2));
            Projectile.rotation += MathHelper.Pi;


        }

        public override void AI()
        {
            // All projectiles have timers that help to delay certain events
            // Projectile.ai[0], Projectile.ai[1] — timers that are automatically synchronized on the client and server
            // Projectile.localAI[0], Projectile.localAI[0] — only on the client
            // In this example, a timer is used to control the fade in / out and despawn of the projectile
            //Lighting.AddLight(Projectile.position, 0.5f, 0f, 0.5f);
            //Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Corruption, Projectile.oldVelocity.X * 0f, Projectile.oldVelocity.Y * 0f);


            if (Projectile.ai[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item21, Projectile.Center);
            }

            Projectile.ai[0] += 1f;

            for (int num154 = 0; num154 < 2; num154++)
            {
                int num155 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 
                                            Projectile.width, 
                                            Projectile.height, 
                                            DustID.Blood, 
                                            0.1f, 
                                            0.1f, 
                                            100);
                Main.dust[num155].noGravity = true;
            }

            FadeInAndOut();

            // Loop through the 4 animation frames, spending 5 ticks on each
            // Projectile.frame — index of current frame
            if (++Projectile.frameCounter >= 1)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            // Despawn this projectile after 1 second (60 ticks)
            // You can use Projectile.timeLeft = 60f in SetDefaults() for same goal

            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;
            float rotationCoeff = 0.5f;
            if (Main.rand.Next(2) == 0)
            {
                Projectile.rotation += rotationCoeff * (float)Projectile.direction;
            } 
            {
                Projectile.rotation -= rotationCoeff * (float)Projectile.direction;
            }

            if (Main.rand.Next(11) == 0)
            {
                Projectile.rotation += MathHelper.PiOver2;
            }


            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            float alphaLightingMod = (float)(Math.Abs(Projectile.alpha - 255f) / 155f);
            if (alphaLightingMod > 1f)
            {
                alphaLightingMod = 1f;
            }
            Lighting.AddLight(Projectile.position, alphaLightingMod, 0f, 0f);

            // Fade out
            if (Projectile.ai[0] >= 20f)
            {
                Projectile.alpha += 25;
                // Cal alpha to the maximum 255(complete transparent)
                if (Projectile.alpha > 255)
                    Projectile.alpha = 255;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
            float offsetX = 20f;
            origin.X = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }
    }
}