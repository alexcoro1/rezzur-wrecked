using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// by Reece Brennan

namespace Rezzur_Wrecked
{
    class RangeAttack
    {
        public Animation rangeAnimation; // Declares animation of our range attack.  
        public Texture2D projectile_texture;
        public float attackMoveSpeed = 10f; // Speed of range attack.
        public Vector2 Position;  // Position of range projectile.      
        int Damage = 10; // Damage of range attack.    
        public bool Active;  // Sets the range attack to "Active"    
        int Range; // Declares the range of the attack.
        public Rectangle rectangle;
        

        public int Width
        {
            get { return rectangle.Width; }
        }
        public int Height
        {
            get { return rectangle.Height; }
        }



        public void Initialize(Animation animation, Vector2 position, bool Direction, Texture2D tex, Sound SND)
        {

            rangeAnimation = animation;
            Position = position;
            projectile_texture = tex;
            Active = true;
            if (Direction == true)
                attackMoveSpeed *= 1;
            else
                attackMoveSpeed *= -1;
        }

        public void Update(GameTime gameTime, Player player)
        {
            Position.X += attackMoveSpeed;
            rangeAnimation.Position = Position;
            rangeAnimation.Update(gameTime, SpriteEffects.None, projectile_texture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rangeAnimation.Draw(spriteBatch); // Displays the animation of the range attack
        }
    }
}
