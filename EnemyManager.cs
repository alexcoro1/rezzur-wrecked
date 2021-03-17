using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rezzur_Wrecked
{
    // by Reece Brennan
    public class EnemyManager
    {

        #region Projectile Declarations
        static Texture2D projectileTexture;
        static Rectangle projectileRectangle;
        private static List<RangeAttack> projectileAttack;

        const float Seconds = 60f;
        const float FireRate = 200f;

        static TimeSpan projectileAvaliable = TimeSpan.FromSeconds(Seconds / FireRate);
        static TimeSpan lastProjectileAvaliable;

        #endregion

        #region EnemyManager Declarations
        Texture2D ea1, ed1, eh1, ea2, ed2, es2, enemyTexture; // enemies description
        static public List<Enemy> enemiesType1 = new List<Enemy>(); // enemies description

        GraphicsDeviceManager graphics; //Graphics info
        TimeSpan enemySpawnTime = TimeSpan.FromSeconds(5.0f); //speed of enemy respawn
        TimeSpan previousSpawnTime = TimeSpan.Zero;
        Random random = new Random(); // random number generator for position
        Animation enemy_anim;
        Rectangle rectangle;
        Vector2 graphicsInfo;

        internal static List<RangeAttack> ProjectileAttack { get => projectileAttack; set => projectileAttack = value; }
        #endregion

        //,Texture2D e1d, Texture2D e1a, Texture2D e2a, Texture2D e2d, Texture2D e2s 
        public void Initialize(Texture2D e1, Texture2D e2, GraphicsDevice Graphics, bool level)
        {
            //graphicsInfo.X = Graphics.Viewport.Width;
            //graphicsInfo.Y = Graphics.Viewport.Height;
            eh1 = e1;
            es2 = e2;

            AddEnemy(level);

            /* ed1 = e1d;
             ea1 = e1a;
             ea2 = e2a;
             ed2 = e2d;
             es2 = e2s;*/
            //enemyTexture = e1h;
        }

        private void AddEnemy(bool level)
        {
            // by Corodati Alexandru
            if (level == true)
            {
                Animation enemyAnimation = new Animation(); // create animation object
                Animation enemyAnimation2 = new Animation();
                // enemyAnimation.Initialize(Vector2.Zero, 72, 131, 4, 60, Color.White, 0.4f, true);
                enemyAnimation.Initialize(Vector2.Zero, 72, 131, 5, 120, Color.White, 0.4f, true);
                enemyAnimation2.Initialize(Vector2.Zero, 72, 96, 4, 120, Color.White, 0.6f, true);
                Vector2 position = new Vector2(0, 0);
                rectangle = new Rectangle(0, 0, 74, 131);
                //bool isr = new bool();
                //Enemy enemy = new Enemy();
                Enemy enemy1 = new Enemy();
                Enemy enemy2 = new Enemy();
                Enemy enemy3 = new Enemy();
                Enemy enemy4 = new Enemy();
                Enemy enemy5 = new Enemy();
                Enemy enemy6 = new Enemy();
                Enemy enemy7 = new Enemy();
                //enemy.Initialize(enemyTexture,enemyAnimation,position,rectangle,100,100,true, new Vector2(0f,0f),false, SpriteEffects.None);
                enemy2.Initialize(eh1, enemyAnimation, new Vector2(350, 325), rectangle, 100, 100, true, new Vector2(1f, 0f), false, SpriteEffects.FlipHorizontally);
                enemy1.Initialize(eh1, enemyAnimation, new Vector2(700, 35), rectangle, 75, 75, true, new Vector2(1f, 0f), false, SpriteEffects.FlipHorizontally);
                enemy3.Initialize(es2, enemyAnimation2, new Vector2(980, 35), rectangle, 50, 50, true, new Vector2(0f, 0f), true, SpriteEffects.None);
                enemy4.Initialize(eh1, enemyAnimation, new Vector2(1070, 35), rectangle, 50, 50, true, new Vector2(0f, 0f), true, SpriteEffects.None);
                enemy5.Initialize(eh1, enemyAnimation, new Vector2(950, 35), rectangle, 50, 50, true, new Vector2(1f, 0f), false, SpriteEffects.FlipHorizontally);
                enemy6.Initialize(es2, enemyAnimation2, new Vector2(550, 600), rectangle, 32, 32, true, new Vector2(0f, 0f), false, SpriteEffects.FlipHorizontally);
                //enemy7.Initialize(eh1, enemyAnimation, new Vector2(125, 600), rectangle, 10, 10, true, new Vector2(0f, 0f), true, SpriteEffects.None);

                //enemiesType1.Add(enemy7);
                enemiesType1.Add(enemy6);
                enemiesType1.Add(enemy5);
                enemiesType1.Add(enemy4);
                enemiesType1.Add(enemy3);
                enemiesType1.Add(enemy1);
                enemiesType1.Add(enemy2);
                //enemiesType1.Add(enemy);
            }
            else
            {
                Animation enemyAnimation = new Animation(); // create animation object
                Animation enemyAnimation2 = new Animation();
                // enemyAnimation.Initialize(Vector2.Zero, 72, 131, 4, 60, Color.White, 0.4f, true);
                enemyAnimation.Initialize(Vector2.Zero, 72, 131, 5, 120, Color.White, 0.4f, true);
                enemyAnimation2.Initialize(Vector2.Zero, 72, 96, 4, 120, Color.White, 0.6f, true);
                Vector2 position = new Vector2(0, 0);
                rectangle = new Rectangle(0, 0, 74, 131);
                //bool isr = new bool();
                //Enemy enemy = new Enemy();
                Enemy enemy1 = new Enemy();
                Enemy enemy2 = new Enemy();
                Enemy enemy3 = new Enemy();
                Enemy enemy4 = new Enemy();
                Enemy enemy5 = new Enemy();
                Enemy enemy6 = new Enemy();
                Enemy enemy7 = new Enemy();
                //enemy.Initialize(enemyTexture,enemyAnimation,position,rectangle,100,100,true, new Vector2(0f,0f),false, SpriteEffects.None);
                enemy2.Initialize(eh1, enemyAnimation, new Vector2(75, 500), rectangle, 100, 100, true, new Vector2(1f, 0f), false, SpriteEffects.FlipHorizontally);
                enemy1.Initialize(eh1, enemyAnimation, new Vector2(250, 500), rectangle, 75, 75, true, new Vector2(1f, 0f), false, SpriteEffects.FlipHorizontally);
                enemy3.Initialize(es2, enemyAnimation2, new Vector2(1200, 750), rectangle, 50, 50, true, new Vector2(0f, 0f), true, SpriteEffects.None);
                enemy4.Initialize(eh1, enemyAnimation, new Vector2(800, 150), rectangle, 50, 50, true, new Vector2(0f, 0f), true, SpriteEffects.None);
                enemy5.Initialize(eh1, enemyAnimation, new Vector2(800, 100), rectangle, 50, 50, true, new Vector2(1f, 0f), false, SpriteEffects.FlipHorizontally);
                enemy6.Initialize(es2, enemyAnimation2, new Vector2(100, 1250), rectangle, 32, 32, true, new Vector2(0f, 0f), false, SpriteEffects.FlipHorizontally);
                //enemy7.Initialize(eh1, enemyAnimation, new Vector2(125, 600), rectangle, 10, 10, true, new Vector2(0f, 0f), true, SpriteEffects.None);

                //enemiesType1.Add(enemy7);
                enemiesType1.Add(enemy6);
                enemiesType1.Add(enemy5);
                enemiesType1.Add(enemy4);
                enemiesType1.Add(enemy3);
                enemiesType1.Add(enemy1);
                enemiesType1.Add(enemy2);
                //enemiesType1.Add(enemy);
            }

        }

        public void UpdateEnemies(GameTime gameTime, Player player, Sound SND)
        {
            //by Corodati Alexandru

            for (int i = 0; i < enemiesType1.Count; i++)
            {
                enemiesType1[i].Update(gameTime, player, this, SND);
                if (enemiesType1[i].Active == false)
                { enemiesType1.RemoveAt(i); }
            }
            for (int i = 0; i < enemiesType1.Count; i++)
            {
                enemiesType1[i].EnemyAnimation.Position = enemiesType1[i].position;
                enemiesType1[i].EnemyAnimation.Update(gameTime, enemiesType1[i].spe, enemiesType1[i].texture);
            }

            UpdateEnemyLaser(gameTime, player);
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemiesType1.Count; i++)
            { enemiesType1[i].Draw(spriteBatch); }
        }

        // by Corodati Alexandru
        public void ResetEnemies(bool level)
        {
            for (int i = 0; i < enemiesType1.Count; i++)
            {
                enemiesType1.RemoveAt(i);
            }
        
                AddEnemy(level);
        }
    



        // by Corodati Alexandru;
        #region Projectile Methods

        /*public int Proj_Width
        {
            get { return projectileRectangle.Width; }
        }
        public int Proj_Height
        {
            get { return projectileRectangle.Height; }
        }*/

        public void InitializeProjectile(Texture2D texture)
        {
            projectileAttack = new List<RangeAttack>();
            lastProjectileAvaliable = TimeSpan.Zero;
            projectileTexture = texture;
            
        }
        public void EnemyFire(GameTime gameTime, Enemy E, Sound SND)
        {
            // Sets the rate of fire for the projectiles
            if (gameTime.TotalGameTime - lastProjectileAvaliable > projectileAvaliable)
            {
                lastProjectileAvaliable = gameTime.TotalGameTime;

                AddProjectile(E, SND); // Adds the projectile to the list created
            }
        }
        private void AddProjectile(Enemy E, Sound SND)
        {
            Animation rangeAnimation = new Animation();
            // initlize the range attack animation
            rangeAnimation.Initialize(E.position, 46, 16, 1, 30, Color.White, 1f, true);
            RangeAttack range = new RangeAttack();
            var projectilePostion = E.position; // Gets the starting position of the projectile.
            if (E.isPointedRight == true)
            {
                projectilePostion.Y += 17;
                projectilePostion.X += 25;
            }
            if (E.isPointedRight == false)
            {
                projectilePostion.Y += 17;
                projectilePostion.X -= 25;
            }
            // initlize the projectile
            SND.enemy_attack.Play();
            range.Initialize(rangeAnimation, projectilePostion, E.isPointedRight, projectileTexture,SND);
            projectileAttack.Add(range);
        }
        public void UpdateEnemyLaser(GameTime gameTime, Player p)
        {
            for (var i = 0; i < projectileAttack.Count; i++)
            {
                projectileAttack[i].Update(gameTime, p);
                // Remove the beam when its deactivated or is at the end of the screen.
                // || projectileAttack[i].Position.X > Game1.ScreenWidth
                if (!projectileAttack[i].Active)
                {
                    projectileAttack.Remove(projectileAttack[i]);
                }
            }

            // detect collisions between the player and all enemies.

            //create a retangle for the player

            Rectangle player_rectangle = new Rectangle();
            player_rectangle = new Rectangle(
                           (int)p.position.X,
                           (int)p.position.Y,
                           p.Width / 2, p.Height / 2);

            // enemy above colliding with the projectile?
            foreach (RangeAttack R in projectileAttack)
                {
                    // create a rectangle for this projectile
                    projectileRectangle = new Rectangle(
                    (int)R.Position.X,
                    (int)R.Position.Y,
                    R.Width,
                    R.Height);
                //! important to change the range attack function for the enemies !!!!!!!!
                    // testing the bounds of the projectile and enemy
                    if (projectileRectangle.Intersects(player_rectangle))
                    {
                        p.health = 0;  // kills the player.                       
                        R.Active = false;  //Removes projectile.
                    }
                }
            
        }
        public void EnemyDrawProjectiles(SpriteBatch spriteBatch)
        {
            // Draw the projectiles.

            foreach (var l in projectileAttack)
            {
                l.Draw(spriteBatch);
            }
        }

    #endregion
    }
}





