using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// by Reece Brennan

namespace Rezzur_Wrecked
{
    class RangeAttackManager
    {
        static Texture2D projectileTexture;
        static Rectangle projectileRectangle;
        static public List<RangeAttack> projectileAttack;

        const float Seconds = 60f;
        const float FireRate = 120f;

        // Determines how fast the player can fire a Projectile attack.
        static TimeSpan projectileAvaliable = TimeSpan.FromSeconds(Seconds / FireRate);
        static TimeSpan lastProjectileAvaliable;


        GraphicsDeviceManager graphics;  //Handle the graphics info

        static Vector2 graphicsInfo; // Handle Graphics info


        KeyboardState currentKeyboardState; // <-- Keyboard states used to determine key presses
        KeyboardState previousKeyboardState; // <-- Keyboard states used to determine key presses

        GamePadState currentGamePadState; // <-- Gamepad states used to determine button presses
        GamePadState previousGamePadState; // <-- Gamepad states used to determine button presses

        public int Width
        {
            get { return projectileRectangle.Width; }
        }
        public int Height
        {
            get { return projectileRectangle.Height; }
        }

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            projectileAttack = new List<RangeAttack>();
            lastProjectileAvaliable = TimeSpan.Zero;
            projectileTexture = texture;
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        private static void FireLaser(GameTime gameTime, Player p,Sound SND,Texture2D patt)
        {
            // Sets the rate of fire for the projectiles
            if (gameTime.TotalGameTime - lastProjectileAvaliable > projectileAvaliable)
            {
                lastProjectileAvaliable = gameTime.TotalGameTime;

                AddProjectile(p,SND,patt); // Adds the projectile to the list created
            }
        }
        private static void AddProjectile(Player p, Sound SND, Texture2D p_att)
        {
            p.currentState = p_att;
            Animation rangeAnimation = new Animation();
            Texture2D player_anim_attack = p_att;
            // initlize the range attack animation
            rangeAnimation.Initialize( p.position, 46, 16, 1, 30, Color.White, 1f, true);
            RangeAttack range = new RangeAttack();
            var projectilePostion = p.position; // Gets the starting position of the projectile.
            if (p.isPointedRight == true)
            {
                projectilePostion.Y += 17; 
                projectilePostion.X += 25; 
            }
            if (p.isPointedRight == false)
            {
                projectilePostion.Y += 17; 
                projectilePostion.X -= 25; 
            }
            // initlize the projectile
            
            SND.player_attack.Play();
            range.Initialize(rangeAnimation, projectilePostion, p.isPointedRight, projectileTexture,SND);
            projectileAttack.Add(range);
        }
        public void UpdateManagerLaser(GameTime gameTime, Player p, Tilemap map,Sound SND,Texture2D patt, Boss boss)
        {
            //Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            //Read the current state of the keyboard and gamepad and store it
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                FireLaser(gameTime, p,SND,patt);
                
            }

            // update projectileAttack...
            for (int i = 0; i < projectileAttack.Count; i++)
            {
                projectileAttack[i].Update(gameTime,p);
                // Remove the beam when its deactivated or is at the end of the screen.
                //  || projectileAttack[i].Position.X > graphicsInfo.X !! to fix this later
                if (!projectileAttack[i].Active)
                {
                    projectileAttack.Remove(projectileAttack[i]);
                }
            }

            // detect collisions between the player and all enemies.
            foreach (Enemy E in EnemyManager.enemiesType1)
            {
                //create a retangle for the enemy
                Rectangle enemyRectangle2 = new Rectangle(
                    (int)E.position.X,
                    (int)E.position.Y,
                    E.Width,
                    E.Height);

                // enemy above colliding with the projectile?
                foreach (RangeAttack R in RangeAttackManager.projectileAttack)
                {
                    // create a rectangle for this projectile
                    projectileRectangle = new Rectangle(
                    (int)R.Position.X,
                    (int)R.Position.Y,
                    R.Width,
                    R.Height);

                    // testing the bounds of the projectile and enemy
                    if (projectileRectangle.Intersects(enemyRectangle2))
                    {
                        E.Health = 0;  // kills the enemy.                       
                        R.Active = false; // Removes projectile.
                    }
                }
            }
            Rectangle enemyRectangle = new Rectangle(
                    (int)boss.bossPosition.X,
                    (int)boss.bossPosition.Y,
                    boss.Width,
                    boss.Height);

            // enemy above colliding with the projectile?
            foreach (RangeAttack R in RangeAttackManager.projectileAttack)
            {
                // create a rectangle for this projectile
                projectileRectangle = new Rectangle(
                (int)R.Position.X,
                (int)R.Position.Y,
                R.Width,
                R.Height);

                // testing the bounds of the projectile and enemy
                if (projectileRectangle.Intersects(enemyRectangle))
                {
                    boss.bossHealth -= 50;  // dmg the boss                       
                    R.Active = false; // Removes projectile.
                }
            }

        }

        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            // Draw the projectiles.
            foreach (var l in projectileAttack)
            {
                l.Draw(spriteBatch);
            }
        }
    }
}
