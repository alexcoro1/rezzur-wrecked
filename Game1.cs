using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Rezzur_Wrecked
{
    /// <summary>
    /// By Corodati Alexandru, with assitance on  level wrapper (menu especially) from Gabriel Figur 
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        RangeAttackManager RAM = new RangeAttackManager();

        enum GameStates { MenuScreen, startLvL1, Playing, PauseMenu, startLevel2, GameOver, EndScreen, CtrlImg, Credits}
        GameStates gameState = GameStates.MenuScreen;
        SpriteBatch spriteBatch;
        Texture2D projectile;

        Vector2 graphicsInfo;
        Texture2D player_texture, texture_e1;
        Texture2D enemy_texture1;
        public static int ScreenWidth, ScreenHeight;
        Texture2D tile1, tile2, spike, tilemap;
        Player player = new Player();
        Tilemap map = new Tilemap();
        GraphicsDevice details;
        Camera camera;
        EnemyManager EM = new EnemyManager();
        Sound SoundManager = new Sound();
        Texture2D Character_Sheet, congrats,l1,l2;
        Texture2D player_jump, player_attack,player_stand,player_walk_anim;

        Texture2D enemy1_hover, enemy1_attack, enemy1_death;

        Texture2D enemy2_stand, enemy2_attack, enemy2_death;

        Texture2D bs, bd, bt, bap, ba;

        Texture2D BGMM, CB, CMM, CTRLB, CTRLMM, LB, PB, QUIT;

        Boss boss = new Boss();

        Texture2D game_over;
        float gameOverTimer = 0.0f;
        float gameOverDelay = 5.0f;
        float endgameTimer = 0.0f;
        float endgameDelay = 4.0f;


        lifeOrb orb = new lifeOrb();
        lifeOrb orb2 = new lifeOrb();
        lifeOrb orb3 = new lifeOrb();
        lifeOrb orb4 = new lifeOrb();
        lifeOrb orb5 = new lifeOrb();
        lifeOrb orb6 = new lifeOrb();
        lifeOrb orb7 = new lifeOrb();

        Texture2D pbt, qbt, rbt;

        public Button play = new Button();
        public Button quit = new Button();
        public Button resume = new Button();
        public Button QU = new Button();
        public Button lev = new Button();
        public Button ctrl = new Button();
        public Button credits = new Button();
        public Texture2D pause_menu;
        bool yes2 = true;
        public int LevelSelection = new int();
        /*
         * 0 = menu
         * 1 = tutorial
         * 2 = tutorial hell
         * 3 = level 1
         * 4 = level 1 hell
         * 5 = level 2
         * 6 = leve; 2 hell
         * 7 = level 3
         * 8 = level 3 hell
         * 
         * */

        public bool yes = true;
        public Song s1, s2, s3, s4, s5, s6, s7, s8;
        public Song seq1, seq2, seq3, seq4;
        public SongCollection playlist;
        public int playlist_count = new int();

        private SoundEffect player_walk, player_attack_sound, player_jump_sound, enemy_hover, enemy_attack;


        bool level = true;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            details = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters());
            graphics.ToggleFullScreen();
            graphics.PreferredBackBufferWidth = 640;
            ScreenWidth = 640;
            graphics.PreferredBackBufferHeight = 360;
            ScreenHeight = 360;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Set the World Rectangle to 1600*1600 – room dimensions
            Camera.WorldRectangle = new Rectangle(0, 0, 1600, 1600);
            //Set the Monitor Rectangle to 400*200– room dimensions
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 400;
            camera = new Camera();
            
            IsMouseVisible = true;

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            //gameMusic = Content.Load<Song>("music7normal");

            enemy_texture1 = Content.Load<Texture2D>("enemy_sheet1");
            player_texture = Content.Load<Texture2D>("main_char");
            //Character_Sheet = Content.Load<Texture2D>("character_sheet");
            projectile = Content.Load<Texture2D>("test_projectile");
            tile1 = Content.Load<Texture2D>("tile1");
            tile2 = Content.Load<Texture2D>("tile2");
            spike = Content.Load<Texture2D>("spikes");
            tilemap = Content.Load<Texture2D>("TileSheet");
            texture_e1 = Content.Load<Texture2D>("enemy_test");
            player_jump = Content.Load<Texture2D>("mc_jump");
            player_attack = Content.Load<Texture2D>("mc_attack");
            player_stand = Content.Load<Texture2D>("mc_stand");
            player_walk_anim = Content.Load<Texture2D>("mc_move");
            game_over = Content.Load<Texture2D>("game_over");
            seq1 = Content.Load<Song>("Sounds\\UnderWorldTheme\\sequence1");
            seq2 = Content.Load<Song>("Sounds\\UnderWorldTheme\\sequence2");
            seq3 = Content.Load<Song>("Sounds\\UnderWorldTheme\\sequence3_new");
            seq4 = Content.Load<Song>("Sounds\\UnderWorldTheme\\sequence4_nopunch");

            enemy1_attack = Content.Load<Texture2D>("enemy1_attack");
            enemy1_death = Content.Load<Texture2D>("enemy1_death");
            enemy1_hover = Content.Load<Texture2D>("enemy1_hover");

            enemy2_attack = Content.Load<Texture2D>("enemy2_attack");
            enemy2_death = Content.Load<Texture2D>("enemy2_death");
            enemy2_stand = Content.Load<Texture2D>("enemy2_stand");

            pbt = Content.Load<Texture2D>("play_button");
            qbt = Content.Load<Texture2D>("quit_button");
            rbt = Content.Load<Texture2D>("resume");

            bs = Content.Load<Texture2D>("boss_stand");
            bt = Content.Load<Texture2D>("boss_tp");
            bap = Content.Load<Texture2D>("boss_ap");
            bd = Content.Load<Texture2D>("boss_death");
            ba = Content.Load<Texture2D>("boss_attack");

            BGMM = Content.Load<Texture2D>("BGMM");
            CB = Content.Load<Texture2D>("CB");
            CMM = Content.Load<Texture2D>("CMM");
            CTRLB = Content.Load<Texture2D>("CTRLB");
            CTRLMM = Content.Load<Texture2D>("CTRLMM");
            LB = Content.Load<Texture2D>("LB");
            PB = Content.Load<Texture2D>("PB");
            QUIT = Content.Load<Texture2D>("quit");
            congrats = Content.Load<Texture2D>("congrats");
            l1 = Content.Load<Texture2D>("l1");
            l2 = Content.Load<Texture2D>("l2");

            pause_menu = Content.Load<Texture2D>("paused_menu");

            player_walk = Content.Load<SoundEffect>("Sounds\\SoundEffects\\walking");
            player_attack_sound = Content.Load<SoundEffect>("Sounds\\SoundEffects\\fire");
            player_jump_sound = Content.Load<SoundEffect>("Sounds//SoundEffects//jump");
            enemy_hover = Content.Load<SoundEffect>("Sounds\\SoundEffects\\hover");
            enemy_attack = Content.Load<SoundEffect>("Sounds\\SoundEffects\\fire2");

            #region Sounds and Songs
            s1 = Content.Load<Song>("Sounds\\Songs\\riser");
            s2 = Content.Load<Song>("Sounds\\Songs\\bassprogressionnormal");
            s3 = Content.Load<Song>("Sounds\\Songs\\music2_chorus_verse_chorus_bgg");
            s4 = Content.Load<Song>("Sounds\\Songs\\music4_full_highpitch");
            s5 = Content.Load<Song>("Sounds\\Songs\\music4_full_highpitch");
            s6 = Content.Load<Song>("Sounds\\Songs\\music5");
            s7 = Content.Load<Song>("Sounds\\Songs\\music6");
            s8 = Content.Load<Song>("music7normal");


            

            /*playlist.Add(s1);
            playlist.Add(s2);
            playlist.Add(s3);
            playlist.Add(s4);
            playlist.Add(s5);
            playlist.Add(s6);
            playlist.Add(s7);
            playlist.Add(s8);*/
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(s8);
            MediaPlayer.IsRepeating = true;

            #endregion


            graphicsInfo.X = GraphicsDevice.Viewport.Width;
            graphicsInfo.Y = GraphicsDevice.Viewport.Height;
            details = GraphicsDevice;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            
            
            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameStates.MenuScreen:
                    camera.Follow(play);
                    MainMenu(yes);
                    MouseState mouse = new MouseState();
                    play.Update(mouse);
                    QU.Update(mouse);
                    lev.Update(mouse);
                    ctrl.Update(mouse);
                    credits.Update(mouse);
                    if (Keyboard.GetState().IsKeyDown(Keys.T))
                    {

                        gameState = GameStates.EndScreen;
                    }
                    if (play.isClicked == true)
                    {
                        StartLevel1();
                        gameState = GameStates.startLvL1;
                    }
                    if(QU.isClicked == true)
                    {
                        Exit();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    {
                        level = true;
                        StartLevel1();
                        gameState = GameStates.startLvL1;
                    }
                    if(Keyboard.GetState().IsKeyDown(Keys.I))
                    {
                        level = false;
                        startLevel2();
                        gameState = GameStates.startLevel2;
                    }
                    if(ctrl.isClicked == true)
                    {
                        gameState = GameStates.CtrlImg;
                    }
                    if(credits.isClicked == true)
                    {
                        gameState = GameStates.Credits;
                    }
                    break;

                case GameStates.startLvL1:
                    endgameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (endgameTimer > endgameDelay)
                    {

                        gameOverTimer = 0.0f;
                        gameState = GameStates.Playing;
                    }
                    
                    break;
                case GameStates.Playing:
                    
                    
                    if (level == true)
                    {
                        camera.Follow(player);
                        if (Keyboard.GetState().IsKeyDown(Keys.P))
                        {

                            gameState = GameStates.PauseMenu;
                        }
                        else
                        {
                            player.Update(gameTime, map, SoundManager,level);
                            RAM.UpdateManagerLaser(gameTime, player, map, SoundManager, player_attack, boss);
                            EM.UpdateEnemies(gameTime, player, SoundManager);
                            map.UpdateCollision(gameTime, player, boss,level);
                            boss.Update(gameTime, player, EM, SoundManager);
                            boss.UpdateEnemyLaser(gameTime, player);
                            orb6.Update(gameTime, player, map);
                            orb5.Update(gameTime, player, map);
                            orb4.Update(gameTime, player, map);
                            orb3.Update(gameTime, player, map);
                            orb2.Update(gameTime, player, map);
                            orb.Update(gameTime, player, map);
                            if (boss.bossActive == false)
                            {
                                level = false;
                                startLevel2();
                                gameState = GameStates.startLevel2;
                            }
                        }
                    }
                    if(level == false)
                    {
                        camera.Follow(player);
                        if (Keyboard.GetState().IsKeyDown(Keys.P))
                        {

                            gameState = GameStates.PauseMenu;
                        }
                        else
                        {
                            player.Update(gameTime, map, SoundManager,level);
                            RAM.UpdateManagerLaser(gameTime, player, map, SoundManager, player_attack, boss);
                            EM.UpdateEnemies(gameTime, player, SoundManager);
                            map.UpdateCollision(gameTime, player, boss,level);
                            boss.Update(gameTime, player, EM, SoundManager);
                            boss.UpdateEnemyLaser(gameTime, player);
                            orb6.Update(gameTime, player, map);
                            orb5.Update(gameTime, player, map);
                            orb4.Update(gameTime, player, map);
                            orb3.Update(gameTime, player, map);
                            orb2.Update(gameTime, player, map);
                            orb.Update(gameTime, player, map);
                            if (boss.bossActive == false)
                                gameState = GameStates.EndScreen;
                        }
                    }
                    break;
                case GameStates.PauseMenu:

                    camera.Follow(resume);
                    PM(yes2);
                    MouseState mouse1 = new MouseState();
                    resume.Update(mouse1);
                    quit.Update(mouse1);
                    if (resume.isClicked == true)
                    {
                        gameState = GameStates.Playing;
                    }
                    if (quit.isClicked == true)
                    {
                        Exit();
                    }
                    break;
                case GameStates.startLevel2:
                    endgameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (endgameTimer > endgameDelay)
                    {
                        
                        endgameTimer = 0.0f;
                        gameState = GameStates.Playing;
                    }
                    
                    break;
                case GameStates.GameOver:
                    MediaPlayer.Stop();
                    gameOverTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(gameOverTimer > gameOverDelay)
                    {
                        Exit();
                        gameOverTimer = 0.0f;
                    }

                    break;
                case GameStates.EndScreen:
                    MediaPlayer.Stop();
                    endgameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (endgameTimer > endgameDelay)
                    {
                        gameState = GameStates.MenuScreen;
                        gameOverTimer = 0.0f;
                    }

                    break;
                case GameStates.CtrlImg:
                    MouseState mouse2 = new MouseState();
                    QU.Update(mouse2);
                    if(QU.isClicked == true)
                    {
                        gameState = GameStates.MenuScreen;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.U))
                    {

                        gameState = GameStates.MenuScreen;
                    }
                    break;
                case GameStates.Credits:
                    MouseState mouse3 = new MouseState();
                    QU.Update(mouse3);
                    
                    if (QU.isClicked == true)
                    {
                        gameState = GameStates.MenuScreen;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Y))
                    {

                        gameState = GameStates.MenuScreen;
                    }
                    break;

            }


            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            
            spriteBatch.Begin(transformMatrix: camera.trans);
            if (gameState == GameStates.MenuScreen)
            {
                spriteBatch.Draw(BGMM, new Rectangle((int)player.position.X - 225, (int)player.position.Y - 160, ScreenWidth+170, ScreenHeight+170), Color.White);
                play.Draw(spriteBatch);
                lev.Draw(spriteBatch);
                ctrl.Draw(spriteBatch);
                credits.Draw(spriteBatch);
                QU.Draw(spriteBatch);
            }
            if(gameState == GameStates.startLvL1)
            {
                spriteBatch.Draw(l1, new Rectangle((int)player.position.X - 290, (int)player.position.Y - 378, ScreenWidth + 190, ScreenHeight + 160), Color.White);
            }
            if (gameState == GameStates.Playing)
            {
                if (level == true)
                {
                    CheckDeath();
                    map.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    RAM.DrawProjectiles(spriteBatch);
                    EM.DrawEnemies(spriteBatch);
                    EM.EnemyDrawProjectiles(spriteBatch);

                    boss.Draw(spriteBatch);
                    boss.EnemyDrawProjectiles(spriteBatch);

                    orb6.Draw(spriteBatch);
                    orb5.Draw(spriteBatch);
                    orb4.Draw(spriteBatch);
                    orb3.Draw(spriteBatch);
                    orb2.Draw(spriteBatch);
                    orb.Draw(spriteBatch);
                }
                else
                {
                    CheckDeath();
                    map.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    RAM.DrawProjectiles(spriteBatch);
                    EM.DrawEnemies(spriteBatch);
                    EM.EnemyDrawProjectiles(spriteBatch);

                    boss.Draw(spriteBatch);
                    boss.EnemyDrawProjectiles(spriteBatch);

                    orb6.Draw(spriteBatch);
                    orb5.Draw(spriteBatch);
                    orb4.Draw(spriteBatch);
                    orb3.Draw(spriteBatch);
                    orb2.Draw(spriteBatch);
                    orb.Draw(spriteBatch);
                }

            }
            if(gameState == GameStates.PauseMenu)
            {
                spriteBatch.Draw(pause_menu, new Rectangle((int)player.position.X - 290 , (int)player.position.Y - 378, ScreenWidth + 160, ScreenHeight + 160), Color.White);
                resume.Draw(spriteBatch);
                quit.Draw(spriteBatch);
            }
            if(gameState == GameStates.startLevel2)
            {
                spriteBatch.Draw(l2, new Rectangle((int)player.position.X - 290, (int)player.position.Y - 210, ScreenWidth + 190, ScreenHeight + 160), Color.White);
            }
            if(gameState == GameStates.GameOver)
            {
                spriteBatch.Draw(game_over, new Rectangle( (int)player.position.X - 290, (int)player.position.Y - 136 , ScreenWidth + 160 , ScreenHeight + 157), Color.White);
            }
            if(gameState == GameStates.EndScreen)
            {
                spriteBatch.Draw(congrats, new Rectangle((int)player.position.X - 220, (int)player.position.Y - 180, ScreenWidth + 180, ScreenHeight + 150), Color.White);
                
            }
            if(gameState == GameStates.CtrlImg)
            {
                spriteBatch.Draw(CTRLMM, new Rectangle((int)player.position.X - 225, (int)player.position.Y - 160, ScreenWidth + 170, ScreenHeight + 170), Color.White);
                QU.Draw(spriteBatch);
            }
            if(gameState == GameStates.Credits)
            {
                spriteBatch.Draw(CMM, new Rectangle((int)player.position.X - 225, (int)player.position.Y - 160, ScreenWidth + 170, ScreenHeight + 170), Color.White);
                QU.Draw(spriteBatch);
            }
                spriteBatch.End();
          

            base.Draw(gameTime);
        }
        
        public void StartLevel1()
        {
            
            EM.Initialize(enemy1_hover,enemy2_stand, details,level);
            EM.InitializeProjectile(projectile);
            Animation playerAnimation = new Animation();
            playerAnimation.Initialize(Vector2.Zero, 61, 96, 4, 150, Color.White, 0.5f, true);

            SoundManager.Initialize(player_walk, player_attack_sound, player_jump_sound, enemy_hover, enemy_attack);
            
            player.Initialize(playerAnimation, new Vector2(50, 220), graphicsInfo, new Rectangle(0, 0, 61, 96), player_walk_anim, player_jump, player_attack, player_stand,true);
            RAM.Initialize(projectile, details);
            map.Initialize(tilemap,level);

            Animation orb_animation = new Animation();
            orb_animation.Initialize(Vector2.Zero, 75, 75, 1, 60, Color.White, 0.5f, true);
            orb.Initialize(orb_animation, new Vector2(400, 325), new Rectangle(0, 0, 50, 50), projectile);
            orb2.Initialize(orb_animation, new Vector2(700, 40), new Rectangle(0, 0, 50, 50), projectile);
            orb3.Initialize(orb_animation, new Vector2(980, 40), new Rectangle(0, 0, 50, 50), projectile);
            orb4.Initialize(orb_animation, new Vector2(940, 500), new Rectangle(0, 0, 50, 50), projectile);
            orb5.Initialize(orb_animation, new Vector2(500, 650), new Rectangle(0, 0, 50, 50), projectile);
            orb6.Initialize(orb_animation, new Vector2(55, 750), new Rectangle(0, 0, 50, 50), projectile);

            Animation boss_anim = new Animation();
            boss_anim.Initialize(Vector2.Zero, 71, 91, 11, 120, Color.White, 0.8f, true);
            boss.Initialize(bs, ba,bt,bap,bd,boss_anim, new Vector2(800, 1150), new Rectangle(0, 0, 71, 91), 32, 32, true,projectile,level);


        }
        public void startLevel2()
        {
            EM.Initialize(enemy1_hover, enemy2_stand, details,level);
            EM.InitializeProjectile(projectile);
            Animation playerAnimation = new Animation();
            playerAnimation.Initialize(Vector2.Zero, 61, 96, 4, 150, Color.White, 0.5f, true);

            SoundManager.Initialize(player_walk, player_attack_sound, player_jump_sound, enemy_hover, enemy_attack);

            player.Initialize(playerAnimation, new Vector2(50, 50), graphicsInfo, new Rectangle(0, 0, 61, 96), player_walk_anim, player_jump, player_attack, player_stand,false);
            RAM.Initialize(projectile, details);
            map.Initialize(tilemap,level);

            Animation orb_animation = new Animation();
            orb_animation.Initialize(Vector2.Zero, 75, 75, 1, 60, Color.White, 0.5f, true);
            orb.Initialize(orb_animation, new Vector2(400, 325), new Rectangle(0, 0, 50, 50), projectile);
            orb2.Initialize(orb_animation, new Vector2(700, 40), new Rectangle(0, 0, 50, 50), projectile);
            orb3.Initialize(orb_animation, new Vector2(980, 40), new Rectangle(0, 0, 50, 50), projectile);
            orb4.Initialize(orb_animation, new Vector2(940, 500), new Rectangle(0, 0, 50, 50), projectile);
            orb5.Initialize(orb_animation, new Vector2(500, 650), new Rectangle(0, 0, 50, 50), projectile);
            orb6.Initialize(orb_animation, new Vector2(55, 750), new Rectangle(0, 0, 50, 50), projectile);

            Animation boss_anim = new Animation();
            boss_anim.Initialize(Vector2.Zero, 71, 91, 11, 120, Color.White, 0.8f, true);
            boss.Initialize(bs, ba, bt, bap, bd, boss_anim, new Vector2(1300, 1050), new Rectangle(0, 0, 71, 91), 32, 32, true, projectile, level);

        }
        public void MainMenu(bool Yes)
        {
            if(Yes == true)
            {
                play.btnLoad(PB, new Vector2(53, 10));
                QU.btnLoad(QUIT, new Vector2(53+300, 185));
                lev.btnLoad(LB, new Vector2(53, 50));
                ctrl.btnLoad(CTRLB, new Vector2(53, 100));
                credits.btnLoad(CB, new Vector2(53, 150));
                Yes = false;
                yes = false;
            }
        }
        public void PM(bool Yes)
        {
            if (Yes == true)
            {
                resume.btnLoad(rbt, new Vector2(0, 0));
                quit.btnLoad(qbt, new Vector2(0, 100));
                Yes = false;
                yes2 = false;
            }
        }

        public void CheckDeath()
        {
            if(player.death_count == 1 && player.health == 0)
            {
                player.death_count = -1;
                MediaPlayer.Stop();
                gameState = GameStates.GameOver;
            }
        }
    }
}
