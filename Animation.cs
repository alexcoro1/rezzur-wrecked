using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

// by Corodati Alexandru

namespace Rezzur_Wrecked
{
    public class Animation
    {
        #region Declarations
        // The image representing the collection of images used for animation
        Texture2D spriteStrip;
        // The scale used to display the sprite strip
        float scale;
        // The time since we last updated the frame
        int elapsedTime;
        // The time we display a frame until the next one
        int frameTime, originalframeTime;
        // The number of frames that the animation contains
        public int frameCount, originalframeCount;
        // The index of the current frame we are displaying
        public int currentFrame;
        // The color of the frame we will be displaying
        Color color;
        // The area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();
        // The area where we want to display the image strip in the game
        public Rectangle destinationRect = new Rectangle();
        // Width of a given frame
        public int FrameWidth, originalFrameWidth;
        // Height of a given frame
        public int FrameHeight, originalFrameHeight;
        // The state of the Animation
        public bool Active;
        // Determines if the animation will keep playing or deactivate after one run
        public bool Looping;
        // Width of a given frame
        public Vector2 Position;
        
        private List<Rectangle> frames = new List<Rectangle>();
        public SpriteEffects SE = new SpriteEffects();

        #endregion

        public void Initialize( Vector2 position, int frameWidth,
            int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            // Keep a local copy of the values passed in
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;
            originalframeCount = frameCount;
            originalFrameHeight = frameHeight;
            originalFrameWidth = FrameWidth;
            originalframeTime = frametime;
            Looping = looping;
            Position = position;
            //spriteStrip = texture;

            // Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;

            // Set the Animation to active by default
            Active = true;

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the Frame width
            //This is the Animation Rectangle to be picked from the actual SpriteSheet
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            // This is for the Rectangle animation to be played in the game World
            destinationRect = new Rectangle(
                (int)Position.X - (int)(FrameWidth * scale) / 2,
                (int)Position.Y - (int)(FrameHeight * scale) / 2,
                (int)(FrameWidth * scale),
                (int)(FrameHeight * scale));

            //Adding Animation Sequence
            for (int x = 0; x < frameCount; x++)
            {
                frames.Add(new Rectangle(
                (FrameWidth * x),
                0,
                FrameWidth, FrameHeight));
            }
        }

        public void Update(GameTime gameTime, SpriteEffects ASE, Texture2D texture)
        {
            
            SE = ASE;
            spriteStrip = texture;
                // Do not update the game if we are not active
                if (Active == false) return;
                // Update the elapsed time
                elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                // If the elapsed time is larger than the frame time we need to switch frames
                if (elapsedTime > frameTime)
                {
                    currentFrame++;// Move to the next frame
                    if (currentFrame == frameCount)// If the currentFrame is equal to frameCount reset currentFrame to zero
                    {
                        currentFrame = 0;
                        if (Looping == false)// If we are not looping deactivate the animation
                            Active = false;
                    }
                    elapsedTime = 0;// Reset the elapsed time to zero
                }

                sourceRect = frames[currentFrame];
                // This is for the Rectangle animation to be played in the game World
                destinationRect = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(FrameWidth * scale) ,
                    (int)(FrameHeight * scale));
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active && spriteStrip!=null)// Only draw the animation when we are active
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color,0f,Vector2.Zero,SE,0f);
            }
        }
    }
}

//by me
