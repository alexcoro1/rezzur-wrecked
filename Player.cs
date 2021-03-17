using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

// Movemenet and basic structure by Sam Tautvydas, jump, animations, checkpoints and stats (ex life) by Alexandru Corodati

namespace Rezzur_Wrecked
{
    public class Player
    {

        #region Declaration
        public Vector2 position;
        public Vector2 velocity;
        Texture2D p_texture;
        
        public SpriteEffects spe;
        public bool isPointedRight;
        Vector2 graphicsInfo;
        KeyboardState currentKeyboardState;
        KeyboardState prevKeyboardState;

        GamePadState currentGamePadState;
        GamePadState prevGamePadState;
        
        public float playerMoveSpeed;
        public Rectangle p_rectangle;
        public bool hasJumped;
        public int health;
        public int death_count;

        public Animation PlayerAnimation;
        public Texture2D player_move,player_jump,player_attack, player_stand,currentState;
        public Rectangle SourceRect;
        public Vector2 originalPos;
        public Vector2 checkPos;
        
        public int Width
          {
              get { return p_rectangle.Width; }
          }
          public int Height
          {
              get { return p_rectangle.Height; }
          }
          #endregion
         /*
        #endregion
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }
        
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
        }
        */

        #region Constructors
        public void Initialize(Animation player_anim,Vector2 newPosition, Vector2 grInfo,Rectangle rectangle,Texture2D move, Texture2D jump, Texture2D attack,Texture2D stand,bool level)
        {
            if (level == true)
            {
                graphicsInfo = grInfo;
                playerMoveSpeed = 3f;
                position = newPosition;
                originalPos = newPosition;
                checkPos = newPosition;
                //p_texture = texture;
                p_rectangle = rectangle;
                hasJumped = true;
                spe = SpriteEffects.None; // it starts pointed right
                isPointedRight = true;
                health = 50;
                death_count = 0;
                SourceRect = rectangle;
                PlayerAnimation = player_anim;
                player_jump = jump;
                player_move = move;
                player_attack = attack;
                player_stand = stand;
                currentState = move;
            }
            else
            {
                graphicsInfo = grInfo;
                playerMoveSpeed = 3f;
                position = newPosition;
                originalPos = newPosition;
                checkPos = newPosition;
                //p_texture = texture;
                p_rectangle = rectangle;
                hasJumped = true;
                spe = SpriteEffects.None; // it starts pointed right
                isPointedRight = true;
                health = 50;
                death_count = 0;
                SourceRect = rectangle;
                PlayerAnimation = player_anim;
                player_jump = jump;
                player_move = move;
                player_attack = attack;
                player_stand = stand;
                currentState = move;
            }


        }
        #endregion

        #region Update and Draw methods
        public void Update(GameTime gameTime,Tilemap map, Sound SND, bool level)
        {
            position += velocity;

            prevGamePadState = currentGamePadState;
            prevKeyboardState = currentKeyboardState;

            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            position.Y += currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                velocity.X = -playerMoveSpeed;
                spe = SpriteEffects.FlipHorizontally;
                isPointedRight = false;
                currentState = player_move;
                SND.player_walk.Play();
                
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                velocity.X = playerMoveSpeed;
                spe = SpriteEffects.None;
                isPointedRight = true;
                currentState = player_move;
                SND.player_walk.Play();

            }
            else if (currentKeyboardState.IsKeyDown(Keys.P) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                velocity.X = -playerMoveSpeed;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                velocity.X = playerMoveSpeed;
            }
            else
            {
                velocity.X = 0f;
                
            }

           

            if ((currentKeyboardState.IsKeyDown(Keys.Up) && hasJumped == false) || (currentGamePadState.Buttons.X == ButtonState.Pressed && hasJumped == false))
            {
                position.Y -= 11f;
                velocity.Y = -5.5f;
                hasJumped = true;
                currentState = player_jump;
                SND.player_jump.Play();
                

            }

            if (hasJumped == true && map.isTouching == false)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
               

            }

            if (map.isTouching == false)
            {
                hasJumped = true;
                
            }

            #region CheckPoints
            if (level == true)
            {
                if ((position.X >= 350 && position.X <= 400) && (position.Y >= 300 && position.Y <= 400))
                {
                    checkPos = position;
                }
                if ((position.X >= 700 && position.X <= 800) && (position.Y >= 0 && position.Y <= 96))
                {
                    checkPos = position;
                }
                if ((position.X >= 900 && position.X <= 1000) && (position.Y >= 400 && position.Y <= 600))
                {
                    checkPos = position;
                }
                if ((position.X >= 50 && position.X <= 150) && (position.Y >= 800 && position.Y <= 900))
                {
                    checkPos = position;
                }
            }
            else
            {
                if ((position.X >= 50 && position.X <= 250) && (position.Y >= 500 && position.Y <= 900))
                {
                    checkPos = position;
                }
                if ((position.X >= 450 && position.X <= 800) && (position.Y >= 0 && position.Y <= 200))
                {
                    checkPos = position;
                }
                if ((position.X >= 900 && position.X <= 1100) && (position.Y >= 1000 && position.Y <= 1250))
                {
                    checkPos = position;
                }
                if ((position.X >= 50 && position.X <= 150) && (position.Y >= 800 && position.Y <= 900))
                {
                    checkPos = position;
                }
            }
            #endregion
            if (death_count == 2)
            {
                PickUpLife(map,level);
            }
            if (health == 0 && death_count == 0)
            {
                Die(map,level);
            } 
            

            if (currentState == null)
                currentState = player_stand;
            PlayerAnimation.Position = position;
            PlayerAnimation.Update(gameTime, spe, currentState);
            
            if(PlayerAnimation.currentFrame +1 == 4)
            {
                currentState = player_stand;
            }


        }
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            //float scale = 0.5f;

            //spriteBatch.Draw(p_texture, position, p_rectangle, Color.White, 0f, new Vector2(0, 0), scale, spe, 0f);
            PlayerAnimation.Draw(spriteBatch);


        }

        public void Die(Tilemap m, bool level)
        {
            velocity.X = 0f;
            velocity.Y = 0f;
            if (level == true)
            {
                m.ClearLevel();
                m.Level1Hell();
            }
            else
            {
                m.ClearLevel();
                m.Level2Hell();
            }
            position = checkPos;
            death_count++;
            health = 50;
        }
    
        public void PickUpLife(Tilemap m,bool level)
        {
            velocity.X = 0f;
            velocity.Y = 0f;
            position.Y -= 0.5f;
            if (level == true)
            {
                m.ClearLevel();
                m.Level1();
            }
            else
            {
                m.ClearLevel();
                m.Level2();
            }
            death_count = 0;
            health = 50;

        }
    }
}
