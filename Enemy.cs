using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

// by Reece Brennan and Corodati Alexandru
namespace Rezzur_Wrecked
{
    public class Enemy
    {
       

        #region Enemy Declarations
        public Animation EnemyAnimation;
        public Vector2 position, velocity;
        public Texture2D texture;
        public Rectangle rectangle;
        public bool Active;
        public int Health;
        public int Damage;
        float enemyMoveSpeed;
        float timeSinceAttack;
        public bool hasJumped;
        public SpriteEffects spe;
        public double distance;
        public bool isRanged, isPointedRight, isStill;
        public int limitLeft, limitRight;
        public Vector2 originalPosition;
        public Rectangle sourceRectangle;
        #endregion

        #region Proprieties
        public int Width
        {
            //  get { return EnemyAnimation.FrameWidth; }
            get { return rectangle.Width - 15; }
        }

        public int Height
        {
            //get { return EnemyAnimation.FrameHeight; }
            get { return rectangle.Height - 15 ; }
        }
        public Vector2 LocationEnemy
        {
            get { return position; }
        }
        #endregion
       
        #region Methods
        public void Initialize(Texture2D newTexture, Animation animation, Vector2 newPosition, Rectangle newRectangle, int left, int right, bool iRanged,Vector2 initialVel,  bool Still, SpriteEffects dir)
        {
            // by Reece, updated by Alex
            texture = newTexture;
            EnemyAnimation = animation;
            position = newPosition; // sets enemy position
            originalPosition = newPosition; // sets enemy start position
            rectangle = newRectangle;
            //velocity.X = v.X;
            //velocity.Y = v.Y;
            Active = true; // Enemy is active
            Health = 50; // health of enemy
            Damage = 50; // damage dealt by enemy
            enemyMoveSpeed = 6f; // Speed of enemy
            timeSinceAttack = 3f;
            hasJumped = true;
            if(dir == SpriteEffects.FlipHorizontally)
                isPointedRight = true;
            else
                isPointedRight = false;
            isRanged = iRanged;
            isStill = Still;
            velocity.X = initialVel.X;
            spe = dir;
            //currentstate = texture;
            limitLeft = left;
            limitRight = right;
            sourceRectangle = newRectangle;
   
        }
        // by Alexandru
        public void Update(GameTime gameTime, Player player, EnemyManager EM, Sound SND)
        {
             
            
            position += velocity;
            if (Health <= 0)
            {
                Active = false;
            }
            if(position.X >= originalPosition.X + limitRight && isStill == false)
            {
                velocity.X = -1f;
                spe = SpriteEffects.None;
                isPointedRight = false;
            }
            if(position.X <= originalPosition.X - limitLeft && isStill == false)
            {
                velocity.X = 1f;
                spe = SpriteEffects.FlipHorizontally;
                isPointedRight = true;
            }
            if (isRanged == false)
            {
                float disX = position.X - player.position.X;
                float disY = position.Y - player.position.Y;

                distance = Math.Sqrt((disX * disX) + (disY * disY));

                if (distance < 50 && timeSinceAttack > 2f)
                {
                    player.health = 0;
                    timeSinceAttack = 0f;
                    velocity.X = 0f;


                }
                else
                {
                    timeSinceAttack += 0.05f;
                    if (velocity.X == 0 && spe == SpriteEffects.None && isStill == false)
                        velocity.X = -.5f;
                    else if (velocity.X == 0 && spe == SpriteEffects.FlipHorizontally && isStill == false)
                        velocity.X = .5f;


                }
            }
            if (isRanged == true)
            {

                SND.enemy_hover.Play();
                float disX = position.X - player.position.X;
                float disY = position.Y - player.position.Y;

                distance = Math.Sqrt((disX * disX) + (disY * disY));

                if (distance < 200 && timeSinceAttack > 5f )
                {
                    EM.EnemyFire(gameTime,this,SND);
                    
                    timeSinceAttack = 0f;
                    velocity.X = 0f;
                    

                }
                else
                {
                    timeSinceAttack += 0.05f;
                    if (velocity.X == 0 && spe == SpriteEffects.None &&  isStill == false)
                        velocity.X = -.5f;
                    else if (velocity.X == 0 && spe == SpriteEffects.FlipHorizontally && isStill == false)
                        velocity.X = .5f;


                }
                
            }

            if (hasJumped == true)
            {
                float p = 1;
                velocity.Y += 0.15f * p;
            }
            //EnemyAnimation.Position = position;
            //EnemyAnimation.Update(gameTime, spe, texture);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // EnemyAnimation.Draw(spriteBatch); 

            //float scale = 0.4f;
            //spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, Vector2.Zero, scale, spe, 0f);
            if(EnemyAnimation != null)
            EnemyAnimation.Draw(spriteBatch);
        }

        #endregion

         
    }
}

//basics by Reece / attack & collision by me
