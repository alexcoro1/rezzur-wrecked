using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// by Sam Tautvydas

namespace Rezzur_Wrecked
{
    public class Sprite
    {
        #region Declarations
        public Texture2D Texture;

        private Vector2 worldPos = Vector2.Zero;
        private Vector2 velocity = Vector2.Zero;
        private Rectangle frame;
        
        #endregion

        #region Constructors
        public Sprite(Vector2 worldPos, Texture2D texture, Rectangle startFrame, int startHealth, Vector2 velocity)
        {
            this.worldPos = worldPos;
            Texture = texture;
            this.velocity = velocity;
            frame = startFrame;
        }
        #endregion

        #region Pos Properties
        public Vector2 WorldPos
        {
            get { return worldPos; }
            set { worldPos = value; }
        }
        public Vector2 screenPos
        {
            get
            {
                return Camera.Transform(worldPos);
            }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle((int)worldPos.X, (int)worldPos.Y, frame.Width, frame.Height);
            }
        }
        public Rectangle ScreenRectangle
        {
            get
            {
                return Camera.Transform(WorldRectangle);
            }
        }
        public Vector2 RelativeCenter
        {
            get { return new Vector2(frame.Width / 2, frame.Height / 2); }
        }
        public Vector2 WorldCenter
        {
            get { return worldPos + RelativeCenter; }
        }
        public Vector2 ScreenCenter
        {
            get { return Camera.Transform(worldPos + RelativeCenter); }
        }

       
        #endregion

        #region Update and Draw methods
        public virtual void Update(GameTime gameTime, Player player)
        {
            /*Rectangle player_rectangle;


            player_rectangle = new Rectangle(
                (int)player.position.X,
                (int)player.position.Y,
                player.Width, player.Height);


            #region Player Collision
            // collision for player with any sprite

            //left
            if (player_rectangle.Right + player.velocity.X > this.WorldRectangle.Left &&
                player_rectangle.Left < this.WorldRectangle.Left &&
                player_rectangle.Bottom > this.WorldRectangle.Top &&
                player_rectangle.Top < this.WorldRectangle.Bottom)
            {
                player.velocity.X = 0f;
            }

            //right
            if (player_rectangle.Left + player.velocity.X < this.WorldRectangle.Right &&
                player_rectangle.Right > this.WorldRectangle.Left &&
                player_rectangle.Bottom > this.WorldRectangle.Top &&
                player_rectangle.Top < this.WorldRectangle.Bottom)
            {
                player.velocity.X = 0f;
            }

            //bottom
            if (player_rectangle.Bottom + player.velocity.Y + 50 > this.WorldRectangle.Top &&
                player_rectangle.Top < this.WorldRectangle.Top &&
                player_rectangle.Right > this.WorldRectangle.Left &&
                player_rectangle.Left < this.WorldRectangle.Right)
            {
                player.velocity.Y = 0f;
                player.hasJumped = false;
            }
            
            
           

            //top
            if (player_rectangle.Top + player.velocity.Y < this.WorldRectangle.Bottom &&
                player_rectangle.Bottom > this.WorldRectangle.Bottom &&
                player_rectangle.Right > this.WorldRectangle.Left &&
                player_rectangle.Left < this.WorldRectangle.Right)
            {
                player.velocity.Y = 0f;
                
            }
            #endregion

            #region EnemyCollision

            #endregion
            */

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Camera.ObjectIsVisible(WorldRectangle))
            {
                spriteBatch.Draw(Texture, ScreenCenter, frame, Color.White, 0, RelativeCenter, 1f, SpriteEffects.None, 0f);
            }
        }
        #endregion

    }
}
