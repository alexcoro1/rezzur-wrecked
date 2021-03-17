using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// by Reece Brennan

namespace Rezzur_Wrecked
{
    public class Boss 
    {
        #region Projectile Declarations
        static Texture2D projectileTexture;
        static Rectangle projectileRectangle;
        private static List<RangeAttack> projectileAttack = new List<RangeAttack>();
        

        const float Seconds = 60f;
        const float FireRate = 50f;

        static TimeSpan projectileAvaliable = TimeSpan.FromSeconds(Seconds / FireRate);
        static TimeSpan lastProjectileAvaliable;

        #endregion

        #region Boss Declarations
        public Animation EnemyAnimation;
        public Vector2 bossPosition, bossVelocity;
        public Texture2D texture, bs,btp,bap,bat,bd;
        public Rectangle rectangle;
        public bool bossActive;
        public int bossHealth;
        public int bossDamage;
        float bossMoveSpeed;
        float timeSinceAttack;
        public bool hasJumped;
        public SpriteEffects spe;
        public double distance;
        public bool isRanged, isPointedRight;
        public int limitLeft, limitRight;
        public Vector2 originalPosition;
        public bool bossLevel;


        #endregion

        #region Proprieties
        public int Width
        {
            //  get { return EnemyAnimation.FrameWidth; }
            get { return EnemyAnimation.FrameWidth ; }
        }

        public int Height
        {
            //get { return EnemyAnimation.FrameHeight; }
            get { return EnemyAnimation.FrameHeight ; }
        }
        public Vector2 LocationEnemy
        {
            get { return bossPosition; }
        }
        #endregion

        #region Methods
        public void Initialize(Texture2D stand,Texture2D attack,Texture2D tp,Texture2D ap, Texture2D death,Animation animation, Vector2 newBossPosition, Rectangle newRectangle, int left, int right, bool iRanged, Texture2D proj, bool level)
        {
            texture = stand;
            bs = stand;
            bat = attack;
            btp = tp;
            bap = ap;
            bd = death;
            projectileTexture = proj;
            EnemyAnimation = animation;
            bossPosition = newBossPosition; // sets boss's position
            originalPosition = newBossPosition; // sets boss's start position
            rectangle = newRectangle;
            //velocity.X = v.X;
            //velocity.Y = v.Y;
            bossLevel = level;
            bossActive = true; // Boss is active
            bossHealth = 500; // health of Boss
            bossDamage = 50; // damage dealt by the Boss
            bossMoveSpeed = 9f; // Speed of the Boss
            timeSinceAttack = 3f;
            hasJumped = true;
            isPointedRight = true;
            isRanged = iRanged;
            bossVelocity.X = 0f;
            spe = SpriteEffects.FlipHorizontally;

            limitLeft = left;
            limitRight = right;
        }
        public void Update(GameTime gameTime, Player player, EnemyManager EM, Sound SND)
        {
            
            
            bossPosition += bossVelocity;
            if (player.position.X > bossPosition.X)
            {
                isPointedRight = true;
                spe = SpriteEffects.FlipHorizontally;
            }
            if (player.position.X <= bossPosition.X)
            {
                isPointedRight = false;
                spe = SpriteEffects.None;
            }
            if (isRanged == false)
            {
                float disX = bossPosition.X - player.position.X;
                float disY = bossPosition.Y - player.position.Y;

                distance = Math.Sqrt((disX * disX) + (disY * disY));

                if (distance < 10 && timeSinceAttack > 2f)
                {
                    //player.health = 0;
                    timeSinceAttack = 0f;
                    bossVelocity.X = 0f;
                }
                else
                {
                    timeSinceAttack += 0.05f;
                    if (bossVelocity.X == 0 && spe == SpriteEffects.None)
                        bossVelocity.X = 0f;
                    else if (bossVelocity.X == 0 && spe == SpriteEffects.FlipHorizontally)
                        bossVelocity.X = 0f;
                }
            }
            if (isRanged == true)
            {
                float disX = bossPosition.X - player.position.X;
                float disY = bossPosition.Y - player.position.Y;

                distance = Math.Sqrt((disX * disX) + (disY * disY));

                if (distance < 500 && timeSinceAttack > 2.5f)
                {

                    BossFire(gameTime, this, SND);
                    texture = bat;
                    timeSinceAttack = 0f;
                    bossVelocity.X = 0f;
                }
                else
                {
                    timeSinceAttack += 0.05f;
                    if (bossVelocity.X == 0 && spe == SpriteEffects.None)
                        bossVelocity.X = 0f;
                    else if (bossVelocity.X == 0 && spe == SpriteEffects.FlipHorizontally)
                        bossVelocity.X = 0f;
                }
            }

            if (hasJumped == true)
            {
                float p = 1;
                bossVelocity.Y += 0.15f * p;
            }
            // by Alexandru Corodati
            if (bossHealth <= 0)// if  enemy health is 0 then deactivate it.
            {
                texture = bd;
                isRanged = false;
                
            }
            if (bossHealth == 50)
            {
                if (EnemyAnimation.currentFrame < 10)
                {
                    bossHealth = 50;
                    texture = btp;
                }
                else
                {
                    bossHealth = 0;
                    texture = bap;
                    if (bossLevel == true)
                    {
                        bossPosition = new Vector2(900, 1150);
                    }
                    else bossPosition = new Vector2(1100, 1150);
                }
            }
            if (bossHealth == 250)
            {
                if (EnemyAnimation.currentFrame < 10)
                {
                    bossHealth = 250;
                    texture = btp;
                }
                else
                {
                    bossHealth = 200;
                    texture = bap;
                    if (bossLevel == true)
                    {
                        bossPosition = new Vector2(650, 1150);
                        
                    }
                    else bossPosition = new Vector2(1300, 1150);
                }
            }
            if (bossHealth == 400)
            {
                if(EnemyAnimation.currentFrame < 10)
                {
                    bossHealth = 400;
                    texture = btp;
                }
                else
                {
                    bossHealth = 350;
                    texture = bap;
                    if (bossLevel == true)
                    {
                        bossPosition = new Vector2(950, 1150);
                    }
                    else bossPosition = new Vector2(1050, 1150);
                }
            }

            if (texture == bat && EnemyAnimation.currentFrame == 10)
                texture = bs;
            if (texture == bap && EnemyAnimation.currentFrame == 10)
                texture = bs;
            if (texture == bd && EnemyAnimation.currentFrame == 10)
                bossActive = false;

            EnemyAnimation.Position = bossPosition;
            EnemyAnimation.Update(gameTime, spe, texture);
        }
        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            if(bossActive == true)
             EnemyAnimation.Draw(spriteBatch); 

            //float scale = 0.4f;
            //spriteBatch.Draw(texture, bossPosition, rectangle, Color.White, 0f, Vector2.Zero, scale, spe, 0f);
        }

        #region Projectile Methods

        /*public int Proj_Width
        {
            get { return projectileRectangle.Width; }
        }
        public int Proj_Height
        {
            get { return projectileRectangle.Height; }
        }*/

            //by Alexandru Corodati
        public void InitializeProjectile(Texture2D texture)
        {
            projectileAttack = new List<RangeAttack>();
            lastProjectileAvaliable = TimeSpan.Zero;
            projectileTexture = texture;

        }
        public void BossFire(GameTime gameTime, Boss E, Sound SND)
        {
            // Sets the rate of fire for the projectiles
            if (gameTime.TotalGameTime - lastProjectileAvaliable > projectileAvaliable)
            {
                lastProjectileAvaliable = gameTime.TotalGameTime;

                AddProjectile(E, SND); // Adds the projectile to the list created
            }
        }
        private void AddProjectile(Boss E, Sound SND)
        {
            Animation rangeAnimation = new Animation();
            // initlize the range attack animation
            rangeAnimation.Initialize(E.bossPosition, 46, 16, 1, 30, Color.White, 1f, true);
            RangeAttack range = new RangeAttack();
            var projectilePostion = E.bossPosition; // Gets the starting position of the projectile.
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
            range.Initialize(rangeAnimation, projectilePostion, E.isPointedRight, projectileTexture, SND);
            projectileAttack.Add(range);
        }
        public void UpdateEnemyLaser(GameTime gameTime, Player p)
        {
            if (projectileAttack != null)
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
        }
        public void EnemyDrawProjectiles(SpriteBatch spriteBatch)
        {
            // Draw the projectiles.
            if (projectileAttack != null)
            {
                foreach (RangeAttack l in projectileAttack)
                {
                    l.Draw(spriteBatch);
                }
            }
        }
        #endregion
    }
}
