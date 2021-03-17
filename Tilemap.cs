using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rezzur_Wrecked
{
    // by Sebastian Kucharczyk, with Collision by Alexandru Corodati
    public class Tilemap
    {
        #region Declarations
        public const int TileWidth = 32;
        public const int TileHeight = 32;
        //public const int MapWidth = 30;
        //public const  int MapHeight = 10;
        public const int MapWidth = 50;
        public const int MapHeight = 60;

        public const int FloorTileStart = 0;
        public const int FloorTileEnd = 3;
        public const int WallTileStart = 4;
        public const int WallTileEnd = 7;



        static private Texture2D texture;

        static private List<Rectangle> tiles = new List<Rectangle>();

        static private int[,] mapSquares = new int[MapWidth, MapHeight];
        static private Random rand = new Random();
        public bool isTouching;



        #endregion

        #region Initialization
        public void Initialize(Texture2D tileTexture,bool l)
        {

            texture = tileTexture;
            tiles.Clear();
            tiles.Add(new Rectangle(0, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(0, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 32, TileWidth, TileHeight));

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    mapSquares[x, y] = FloorTileStart;
                }
            }
            if (l == true)
                Level1();
            if (l == false)
                Level2();
        }
        #endregion
        // Collision by Alexandru Corodati
        #region Collision Update

        public void UpdateCollision(GameTime gameTime, Player player, Boss boss,bool level)
        {
            #region Player Collision
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    if (mapSquares[x, y] >= 4 && mapSquares[x, y] != 6)
                    {
                        Rectangle player_rectangle;
                        Rectangle tile_rectangle;



                        player_rectangle = new Rectangle(
                            (int)player.position.X,
                            (int)player.position.Y,
                            player.Width / 2, player.Height / 2);

                        //player_rectangle = player.p_rectangle;

                        tile_rectangle = SquareScreenRectangle(x, y);



                        // collision for player with any sprite

                        //left
                        if (player_rectangle.Right + player.velocity.X * 1 > tile_rectangle.Left &&
                            player_rectangle.Left < tile_rectangle.Left &&
                            player_rectangle.Bottom > tile_rectangle.Top &&
                            player_rectangle.Top < tile_rectangle.Bottom)
                        {
                            player.velocity.X = 0f;
                        }

                        //right
                        if (player_rectangle.Left + player.velocity.X * 1 < tile_rectangle.Right &&
                            player_rectangle.Right > tile_rectangle.Left &&
                            player_rectangle.Bottom > tile_rectangle.Top &&
                            player_rectangle.Top < tile_rectangle.Bottom)
                        {
                            player.velocity.X = 0f;
                        }

                        //bottom
                        if (player_rectangle.Bottom + player.velocity.Y * 1 > tile_rectangle.Top &&
                            player_rectangle.Top < tile_rectangle.Top &&
                            player_rectangle.Right > tile_rectangle.Left &&
                            player_rectangle.Left < tile_rectangle.Right)
                        {
                            player.velocity.Y = 0f;
                            player.hasJumped = false;
                            isTouching = true;

                        }
                        else
                        {
                            isTouching = false;

                        }

                        //top
                        if (player_rectangle.Top + player.velocity.Y * 1 < tile_rectangle.Bottom &&
                            player_rectangle.Bottom > tile_rectangle.Bottom &&
                            player_rectangle.Right > tile_rectangle.Left &&
                            player_rectangle.Left < tile_rectangle.Right)
                        {
                            player.velocity.Y = 0.1f;

                        }


                    }
                    if (mapSquares[x, y] == 6)
                    {
                        Rectangle player_rectangle;
                        Rectangle tile_rectangle;



                        player_rectangle = new Rectangle(
                            (int)player.position.X,
                            (int)player.position.Y,
                            player.Width / 2, player.Height / 2);



                        tile_rectangle = SquareScreenRectangle(x, y);

                        tile_rectangle.Y += 20;

                        if (player_rectangle.Intersects(tile_rectangle))
                        {
                            if(player.death_count == 0)
                            player.Die(this,level);
                            else
                            {
                                player.health = 0;
                            }
                        }
                    }
                }
            }

            #endregion

            #region EnemyCollision
            foreach (Enemy enemy in EnemyManager.enemiesType1)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    for (int y = 0; y < MapHeight; y++)
                    {
                        if (mapSquares[x, y] >= 4 && mapSquares[x, y] != 6)
                        {
                            Rectangle enemy_rectangle;
                            Rectangle tile_rectangle;




                            enemy_rectangle = new Rectangle(
                                (int)enemy.position.X,
                                (int)enemy.position.Y,
                                enemy.Width / 2, enemy.Height / 2);



                            tile_rectangle = SquareScreenRectangle(x, y);



                            // collision for enemy with any sprite

                            //right
                            if (enemy_rectangle.Right + enemy.velocity.X * 1.5 > tile_rectangle.Left &&
                                enemy_rectangle.Left < tile_rectangle.Left &&
                                enemy_rectangle.Bottom > tile_rectangle.Top &&
                                enemy_rectangle.Top < tile_rectangle.Bottom)
                            {

                                enemy.velocity.X = -1f;
                                enemy.spe = SpriteEffects.None;
                                enemy.isPointedRight = false;
                            }


                            //left
                            if (enemy_rectangle.Left + enemy.velocity.X * 1.5 < tile_rectangle.Right &&
                                enemy_rectangle.Right > tile_rectangle.Left &&
                                enemy_rectangle.Bottom > tile_rectangle.Top &&
                                enemy_rectangle.Top < tile_rectangle.Bottom)
                            {

                                enemy.velocity.X = 1f;
                                enemy.spe = SpriteEffects.FlipHorizontally;
                                enemy.isPointedRight = true;

                            }

                            //bottom
                            if (enemy_rectangle.Bottom + enemy.velocity.Y * 1.5 > tile_rectangle.Top &&
                                enemy_rectangle.Top < tile_rectangle.Top &&
                                enemy_rectangle.Right > tile_rectangle.Left &&
                                enemy_rectangle.Left < tile_rectangle.Right)
                            {
                                // ifIntersects = true;
                                enemy.velocity.Y = 0f;
                                enemy.hasJumped = false;
                            }
                            else enemy.hasJumped = true;



                            //top
                            /*
                            if (player_rectangle.Top + player.velocity.Y * 4 < tile_rectangle.Bottom &&
                                player_rectangle.Bottom > tile_rectangle.Bottom &&
                                player_rectangle.Right > tile_rectangle.Left &&
                                player_rectangle.Left < tile_rectangle.Right)
                            {
                                player.velocity.Y = 0.1f;

                            }*/

                        }
                    }
                }
            }

            #endregion

            #region Projectile Collision

            foreach (RangeAttack R in RangeAttackManager.projectileAttack)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    for (int y = 0; y < MapHeight; y++)
                    {
                        if (mapSquares[x, y] >= 4 && mapSquares[x, y] != 6)
                        {
                            Rectangle enemy_rectangle;
                            Rectangle tile_rectangle;




                            enemy_rectangle = new Rectangle(
                                (int)R.Position.X,
                                (int)R.Position.Y,
                                R.Width, R.Height);



                            tile_rectangle = SquareScreenRectangle(x, y);



                            // collision for enemy with any sprite

                            //right
                            if (enemy_rectangle.Right + R.attackMoveSpeed * 1.5 > tile_rectangle.Left &&
                                enemy_rectangle.Left < tile_rectangle.Left &&
                                enemy_rectangle.Bottom > tile_rectangle.Top &&
                                enemy_rectangle.Top < tile_rectangle.Bottom)
                            {

                                R.Active = false;
                            }


                            //left
                            if (enemy_rectangle.Left + R.attackMoveSpeed * 1.5 < tile_rectangle.Right &&
                                enemy_rectangle.Right > tile_rectangle.Left &&
                                enemy_rectangle.Bottom > tile_rectangle.Top &&
                                enemy_rectangle.Top < tile_rectangle.Bottom)
                            {

                                R.Active = false;

                            }


                        }
                    }
                }
            }

            #endregion

            #region Boss Collision
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    if (mapSquares[x, y] >= 4 && mapSquares[x, y] != 6)
                    {
                        Rectangle enemy_rectangle;
                        Rectangle tile_rectangle;




                        enemy_rectangle = new Rectangle(
                            (int)boss.bossPosition.X,
                            (int)boss.bossPosition.Y,
                            boss.Width , boss.Height - 20);



                        tile_rectangle = SquareScreenRectangle(x, y);



                        // collision for enemy with any sprite

                        //right
                        /*if (enemy_rectangle.Right + boss.velocity.X * 1.5 > tile_rectangle.Left &&
                            enemy_rectangle.Left < tile_rectangle.Left &&
                            enemy_rectangle.Bottom > tile_rectangle.Top &&
                            enemy_rectangle.Top < tile_rectangle.Bottom)
                        {

                            boss.velocity.X = -0f;
                            enemy.spe = SpriteEffects.None;
                            enemy.isPointedRight = false;
                        }*/


                        //left
                        /*
                        if (enemy_rectangle.Left + enemy.velocity.X * 1.5 < tile_rectangle.Right &&
                            enemy_rectangle.Right > tile_rectangle.Left &&
                            enemy_rectangle.Bottom > tile_rectangle.Top &&
                            enemy_rectangle.Top < tile_rectangle.Bottom)
                        {

                            enemy.velocity.X = 1f;
                            enemy.spe = SpriteEffects.FlipHorizontally;
                            enemy.isPointedRight = true;

                        }*/

                        //bottom
                        if (enemy_rectangle.Bottom + boss.bossVelocity.Y * 1.5 > tile_rectangle.Top &&
                            enemy_rectangle.Top < tile_rectangle.Top &&
                            enemy_rectangle.Right > tile_rectangle.Left &&
                            enemy_rectangle.Left < tile_rectangle.Right)
                        {
                            // ifIntersects = true;
                            boss.bossVelocity.Y = 0f;
                            boss.hasJumped = false;
                        }
                        else boss.hasJumped = true;



                        //top
                        /*
                        if (player_rectangle.Top + player.velocity.Y * 4 < tile_rectangle.Bottom &&
                            player_rectangle.Bottom > tile_rectangle.Bottom &&
                            player_rectangle.Right > tile_rectangle.Left &&
                            player_rectangle.Left < tile_rectangle.Right)
                        {
                            player.velocity.Y = 0.1f;

                        }*/

                    }
                }
            }
                #endregion

        }

            #endregion
            #region Information about Map Squares
            public int GetSquareByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }
        public int GetSquareByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }
        public Vector2 GetSquareAtPixel(Vector2 pixelLocation)
        {
            return new Vector2(GetSquareByPixelX((int)pixelLocation.X), (GetSquareByPixelY((int)pixelLocation.Y)));
        }
        public Vector2 GetSquareCenter(int squareX, int SquareY)
        {
            return new Vector2((squareX * TileWidth) + (TileWidth / 2), (SquareY * TileHeight) + (TileHeight / 2));
        }
        public Vector2 GetSquareCenter(Vector2 square)
        {
            return GetSquareCenter(
                (int)square.X, (int)square.Y);
        }
        public Rectangle SquareWorldRectangle(int x, int y)
        {
            return new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
        }
        public Rectangle SquareWorldRectangle(Vector2 square)
        {
            return SquareWorldRectangle((int)square.X, (int)square.Y);
        }
        public Rectangle SquareScreenRectangle(int x, int y)
        {
            return Camera.Transform(SquareWorldRectangle(x, y));
        }
        public Rectangle SquareScreenRectangle(Vector2 square)
        {
            return SquareScreenRectangle((int)square.X, (int)square.Y);
        }

        #endregion

        #region Information about Map Tiles
        public int GetTileAtSquare(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &
                (tileY >= 0) && (tileY < MapHeight))
            {
                return mapSquares[tileX, tileY];
            }
            else
            { return -1; }
        }
        public void SetTileAtSquare(int tileX, int tileY, int tile)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &
                (tileY >= 0) && (tileY < MapHeight))
            { mapSquares[tileX, tileY] = tile; }

        }

        public int GetTileAtPixel(int pixelX, int pixelY)
        {
            return GetTileAtSquare(GetSquareByPixelX(pixelX), GetSquareByPixelY(pixelY));
        }
        public int GetTileAtPixel(Vector2 pixelLocation)
        {
            return GetTileAtPixel((int)pixelLocation.X, (int)pixelLocation.Y);
        }

        public bool IsWallTile(int tileX, int tileY)
        {
            int tileIndex = GetTileAtSquare(tileX, tileY);
            if (tileIndex == -1)
            {
                return false;
            }
            return tileIndex >= WallTileEnd;
        }
        public bool IsWallTile(Vector2 square)
        {
            return IsWallTile((int)square.X, (int)square.Y);
        }
        public bool IsWallTileByPixel(Vector2 pixelLocation)
        {
            return IsWallTile(GetSquareByPixelX((int)pixelLocation.X), GetSquareByPixelY((int)pixelLocation.Y));
        }
        #endregion

        #region Drawing

        public void Draw(SpriteBatch spriteBatch)
        {
            int startX = 0;
            int endX = 800;

            int startY = 0;
            int endY = 600;

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if ((x >= 0) && (y >= 0) && (x < MapWidth) && (y < MapHeight))
                    {
                        if (Camera.ObjectIsVisible(Camera.WorldRectangle))
                        {
                            spriteBatch.Draw(texture, SquareScreenRectangle(x, y), tiles[GetTileAtSquare(x, y)], Color.White);
                        }



                    }
                }
            }
        }
        #endregion

        public void ClearLevel()
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                    mapSquares[x, y] = 0;
            }
        }



        #region MAP Generation
        public void Level2()
        {

            for (int x = 0; x < 10; x++)//top 1st room
            {
                mapSquares[x, 0] = 4;
            }
            for (int x = 0; x < 6; x++)// wall first room
            {
                mapSquares[0, x] = 4;
            }
            for (int x = 0; x < 6; x++)// wall first room
            {
                mapSquares[9, x] = 4;
            }
            for (int x = 0; x < 6; x++)//top 1st room
            {
                mapSquares[x, 5] = 4;
            }

            for (int x = 8; x < 10; x++)//top 1st room
            {
                mapSquares[x, 5] = 4;
            }
            for (int x = 5; x < 13; x++)// wall first room
            {
                mapSquares[5, x] = 4;
            }
            for (int x = 5; x < 13; x++)// wall first room
            {
                mapSquares[8, x] = 4;
            }
            for (int x = 0; x < 6; x++)//top 1st room
            {
                mapSquares[x, 12] = 4;
            }
            for (int x = 12; x < 19; x++)// wall first room
            {
                mapSquares[0, x] = 4;
            }

            for (int x = 0; x < 6; x++)//top 1st room
            {
                mapSquares[x, 18] = 4;
            }

            mapSquares[6, 18] = 5; //hell remove
            mapSquares[7, 18] = 5; //hell remove

            for (int x = 8; x < 27; x++)//top 1st room
            {
                mapSquares[x, 18] = 4;
            }
            mapSquares[10, 12] = 4;
            mapSquares[9, 12] = 4;
            mapSquares[11, 12] = 4;
            for (int x = 3; x < 13; x++)// wall first room
            {
                mapSquares[11, x] = 4;
            }
            for (int x = 11; x < 35; x++)//top room 2nd
            {
                mapSquares[x, 3] = 4;
            }

            mapSquares[16, 17] = 4;
            mapSquares[17, 17] = 4;

            mapSquares[19, 15] = 4;
            mapSquares[20, 15] = 4;
            mapSquares[21, 15] = 5;//hell
            mapSquares[22, 15] = 5;//hell

            mapSquares[25, 16] = 5; //hell
            mapSquares[26, 16] = 5;//hell
            for (int x = 18; x < 28; x++)//spikes
            {
                mapSquares[x, 17] = 6;
            }

            mapSquares[27, 18] = 4;
            mapSquares[28, 18] = 4;
            mapSquares[29, 18] = 4;

            mapSquares[30, 18] = 5; //hell
            mapSquares[31, 18] = 5; //hell
            mapSquares[32, 18] = 5; //hell

            mapSquares[33, 18] = 4;
            mapSquares[34, 18] = 4;
            mapSquares[35, 18] = 4;
            mapSquares[36, 18] = 4;
            mapSquares[37, 18] = 4;
            mapSquares[38, 18] = 4;
            mapSquares[39, 18] = 4;
            mapSquares[40, 18] = 4;
            mapSquares[40, 17] = 4;
            mapSquares[40, 16] = 4;
            mapSquares[40, 15] = 4;


            for (int x = 18; x < 29; x++)// wall first room
            {
                mapSquares[29, x] = 4;
            }

            for (int x = 29; x < 45; x++)//spikes
            {
                mapSquares[x, 28] = 4;
            }

            for (int x = 3; x < 29; x++)// wall first room
            {
                mapSquares[44, x] = 4;
            }
            mapSquares[30, 22] = 4;
            mapSquares[31, 25] = 4;

            mapSquares[35, 27] = 4;
            mapSquares[35, 26] = 4;

            mapSquares[36, 25] = 4;
            mapSquares[37, 25] = 4;
            mapSquares[38, 25] = 4;

            mapSquares[41, 24] = 4;
            mapSquares[43, 22] = 4;
            mapSquares[40, 27] = 4;

            mapSquares[41, 20] = 4;
            mapSquares[43, 18] = 4;
            mapSquares[41, 16] = 4;

            mapSquares[39, 14] = 4;
            mapSquares[38, 14] = 4;
            mapSquares[37, 14] = 4;

            mapSquares[36, 13] = 4;
            mapSquares[35, 13] = 4;
            mapSquares[34, 13] = 4;

            mapSquares[33, 12] = 4;
            mapSquares[32, 12] = 4;
            mapSquares[31, 12] = 4;

            mapSquares[30, 11] = 4;
            mapSquares[29, 11] = 4;
            mapSquares[28, 11] = 4;

            mapSquares[27, 10] = 4;
            mapSquares[26, 10] = 4;
            mapSquares[25, 10] = 4;
            mapSquares[24, 10] = 4;
            mapSquares[23, 10] = 4;
            mapSquares[22, 10] = 4;
            mapSquares[21, 10] = 4;
            mapSquares[20, 10] = 4;
            mapSquares[19, 10] = 4;
            mapSquares[18, 10] = 4;
            mapSquares[17, 10] = 4;

            mapSquares[7, 18] = 5; //hell remove
            for (int x = 18; x < 24; x++)// wall first room
            {
                mapSquares[5, x] = 4;
            }
            for (int x = 18; x < 24; x++)// wall first room
            {
                mapSquares[8, x] = 4;
            }

            for (int x = 18; x < 31; x++)// wall first room
            {
                mapSquares[0, x] = 4;
            }

            for (int x = 0; x < 23; x++)// wall first room
            {
                mapSquares[x, 30] = 4;
            }
            for (int x = 1; x < 23; x++)// spiikes
            {
                mapSquares[x, 29] = 6;
            }

            mapSquares[6, 27] = 4;
            mapSquares[7, 27] = 4;
            mapSquares[8, 27] = 4;
            mapSquares[5, 27] = 4;

            mapSquares[12, 27] = 4;
            mapSquares[13, 27] = 4;
            mapSquares[14, 27] = 4;

            mapSquares[18, 27] = 4;
            mapSquares[19, 27] = 4;
            mapSquares[20, 27] = 4;

            mapSquares[23, 29] = 4;
            mapSquares[24, 29] = 4;

            mapSquares[28, 29] = 4;
            for (int x = 19; x < 30; x++)// 
            {
                mapSquares[29, x] = 4;
            }
            for (int x = 30; x < 36; x++)//  helll remove
            {
                mapSquares[29, x] = 5;
            }
            mapSquares[29, 36] = 4;
            mapSquares[28, 36] = 4;
            mapSquares[27, 36] = 4;
            mapSquares[26, 36] = 4;
            for (int x = 36; x < 45; x++)// 
            {
                mapSquares[29, x] = 4;
            }
            for (int x = 0; x < 50; x++)// 
            {
                mapSquares[x, 45] = 4;
            }
            for (int x = 20; x < 45; x++)// 
            {
                mapSquares[0, x] = 4;
            }

            mapSquares[28, 42] = 4;
            mapSquares[27, 42] = 4;
            mapSquares[26, 42] = 4;

            mapSquares[26, 41] = 5; //hell
            mapSquares[26, 40] = 5;
            mapSquares[26, 39] = 5;
            mapSquares[26, 38] = 5;
            mapSquares[26, 37] = 5;

            mapSquares[23, 44] = 4;
            mapSquares[22, 44] = 4;
            mapSquares[21, 44] = 4;
            mapSquares[20, 44] = 4;
            mapSquares[19, 44] = 4;
            mapSquares[18, 44] = 4;


            mapSquares[16, 42] = 4;
            mapSquares[16, 43] = 4;
            mapSquares[16, 44] = 4;

            mapSquares[16, 40] = 5;
            mapSquares[16, 39] = 5;

            mapSquares[16, 42] = 4;
            mapSquares[16, 43] = 4;
            mapSquares[16, 44] = 4;

            mapSquares[15, 42] = 4;
            mapSquares[15, 43] = 4;
            mapSquares[15, 44] = 4;

            mapSquares[14, 42] = 4;
            mapSquares[14, 43] = 4;
            mapSquares[14, 44] = 4;

            mapSquares[13, 42] = 4;
            mapSquares[13, 43] = 4;
            mapSquares[13, 44] = 4;

            mapSquares[10, 40] = 4;
            mapSquares[9, 40] = 4;

            mapSquares[6, 42] = 4;
            mapSquares[6, 43] = 4;
            mapSquares[6, 44] = 4;


            mapSquares[5, 42] = 4;
            mapSquares[5, 43] = 4;
            mapSquares[5, 44] = 4;


            mapSquares[4, 42] = 4;
            mapSquares[4, 43] = 4;
            mapSquares[4, 44] = 4;

            mapSquares[2, 41] = 4;

            mapSquares[1, 39] = 4;

            mapSquares[4, 38] = 4;
            mapSquares[5, 38] = 4;

            mapSquares[6, 37] = 4;
            mapSquares[7, 37] = 4;

            mapSquares[8, 36] = 4;
            mapSquares[9, 36] = 4;
            mapSquares[10, 36] = 4;

            mapSquares[16, 36] = 4;
            mapSquares[14, 36] = 4;
            mapSquares[15, 36] = 4;

            mapSquares[17, 35] = 4;
            mapSquares[18, 35] = 4;
            mapSquares[19, 35] = 4;
            mapSquares[20, 35] = 4;
            mapSquares[21, 34] = 4;
            mapSquares[22, 34] = 4;
            mapSquares[23, 34] = 4;

            mapSquares[23, 33] = 5; //hell

            mapSquares[23, 32] = 5;
            mapSquares[23, 31] = 5;
            mapSquares[23, 30] = 5;

            mapSquares[3, 44] = 6;
            mapSquares[2, 44] = 6;
            mapSquares[1, 44] = 6; //spikes
            mapSquares[7, 44] = 6; //spikes
            mapSquares[8, 44] = 6; //spikes
            mapSquares[9, 44] = 6; //spikes
            mapSquares[10, 44] = 6; //spikes
            mapSquares[11, 44] = 6; //spikes
            mapSquares[12, 44] = 6; //spikes

            for (int x = 29; x < 36; x++)// 
            {
                mapSquares[32, x] = 4;
            }
            for (int x = 32; x < 50; x++)// 
            {
                mapSquares[x, 28] = 4;
            }
            for (int x = 28; x < 45; x++)//  

                mapSquares[49, x] = 4;

            mapSquares[37, 42] = 4;
            mapSquares[38, 42] = 4;
            mapSquares[36, 42] = 4;

            mapSquares[44, 42] = 4;
            mapSquares[45, 42] = 4;
            mapSquares[46, 42] = 4;

            mapSquares[43, 40] = 4;
            mapSquares[42, 40] = 4;
            mapSquares[41, 40] = 4;
            mapSquares[40, 40] = 4;
            mapSquares[39, 40] = 4;

            mapSquares[45, 39] = 4;
            mapSquares[46, 39] = 4;

            mapSquares[37, 39] = 4;
            mapSquares[36, 39] = 4;
        }

        static public void TutorialMap()
        {
            for (int x = 0; x < 30; x++)//bottom
            {
                mapSquares[x, 9] = 4;
            }
            for (int x = 0; x < 30; x++)//bottom +1 
            {
                mapSquares[x, 8] = 4;

            }
            for (int x = 4; x < 14; x++)// bottom +2 from the 5th block
            {
                mapSquares[x, 7] = 4;

            }
            for (int x = 0; x < 30; x++) //top
            {
                mapSquares[x, 0] = 4;

            }

            mapSquares[17, 7] = 4;

            mapSquares[19, 7] = 4;
            mapSquares[19, 6] = 4;

            mapSquares[21, 7] = 4;
            mapSquares[21, 6] = 4;
            mapSquares[21, 5] = 4;

            mapSquares[23, 7] = 4;
            mapSquares[23, 6] = 4;
            mapSquares[23, 5] = 4;
            mapSquares[23, 4] = 4;
            mapSquares[23, 3] = 4;

            mapSquares[24, 7] = 4;
            mapSquares[24, 6] = 4;
            mapSquares[24, 5] = 4;
            mapSquares[24, 4] = 4;
            mapSquares[24, 3] = 4;

            mapSquares[25, 7] = 4;
            mapSquares[25, 6] = 4;
            mapSquares[25, 5] = 4;
            mapSquares[25, 4] = 4;

            mapSquares[26, 6] = 4;
            mapSquares[26, 5] = 4;
            mapSquares[26, 7] = 4;

            mapSquares[27, 7] = 4;
            mapSquares[27, 6] = 4;
            mapSquares[28, 7] = 4;
            mapSquares[28, 6] = 4;
            mapSquares[29, 7] = 4;
            mapSquares[29, 6] = 4;

        }
        public void TutorialMapHell()
        {
            for (int x = 0; x < 30; x++)//bottom
            {
                mapSquares[x, 9] = 4;
            }
            for (int x = 0; x < 30; x++)//bottom +1 
            {
                mapSquares[x, 8] = 4;

            }
            for (int x = 4; x < 14; x++)// bottom +2 from the 5th block
            {
                mapSquares[x, 7] = 4;

            }
            for (int x = 0; x < 30; x++) //top
            {
                mapSquares[x, 0] = 4;

            }

            mapSquares[17, 7] = 4;

            mapSquares[19, 7] = 4;
            mapSquares[19, 6] = 4;

            mapSquares[21, 7] = 4;
            mapSquares[21, 6] = 4;
            mapSquares[21, 5] = 4;

            mapSquares[23, 7] = 4;
            mapSquares[23, 6] = 4;
            mapSquares[23, 5] = 4;
            mapSquares[23, 4] = 4;

            mapSquares[24, 7] = 4;
            mapSquares[24, 6] = 4;
            mapSquares[24, 5] = 4;
            mapSquares[24, 4] = 4;

            mapSquares[25, 7] = 4;
            mapSquares[25, 6] = 4;
            mapSquares[25, 5] = 4;
            mapSquares[25, 4] = 4;

            mapSquares[26, 6] = 4;
            mapSquares[26, 5] = 4;
            mapSquares[26, 7] = 4;

            mapSquares[27, 7] = 4;
            mapSquares[27, 6] = 4;
            mapSquares[28, 7] = 4;
            mapSquares[28, 6] = 4;
            mapSquares[29, 7] = 4;
            mapSquares[29, 6] = 4;

        }

        public void Level1()
        {
            for (int x = 0; x < 10; x++)//top 1st room
            {
                mapSquares[x, 5] = 4;
            }
            for (int x = 0; x < 10; x++)//bottom 1st room
            {
                mapSquares[x, 12] = 4;
            }
            for (int x = 0; x < 3; x++)// 3 tiles spawn
            {
                mapSquares[x, 9] = 4;
            }
            mapSquares[4, 8] = 4; //first jump
            mapSquares[6, 9] = 4; //second jump
            for (int x = 6; x < 10; x++)// wall first room
            {
                mapSquares[9, x] = 4;
            }

            mapSquares[5, 12] = 6; // spikes
            mapSquares[4, 12] = 6; // spikes
            mapSquares[6, 12] = 6; // spikes
            mapSquares[3, 12] = 6; // spikes

            mapSquares[9, 11] = 5; // hell block
            mapSquares[9, 10] = 5; //hell block
            for (int x = 10; x < 19; x++)//bottom first corrder
            {
                mapSquares[x, 12] = 4;
            }

            mapSquares[18, 12] = 6; // spikes
            mapSquares[18, 13] = 4; // spikes


            for (int x = 10; x < 15; x++)// top first corrder
            {
                mapSquares[x, 9] = 4;
            }

            for (int x = 3; x < 13; x++)// wall first corrder
            {
                mapSquares[19, x] = 4;
            }
            for (int x = 0; x < 10; x++)// wall first corrder
            {
                mapSquares[15, x] = 4;
            }
            mapSquares[17, 11] = 4;
            mapSquares[18, 9] = 5; //hell variant remove later
            mapSquares[16, 7] = 4;
            mapSquares[18, 5] = 4;//jumps corrider

            for (int x = 19; x < 27; x++)// bottom 2ndcorrder
            {
                mapSquares[x, 3] = 4;
            }
            mapSquares[3, 12] = 6; // spikes
            mapSquares[3, 13] = 4; // spikes


            for (int x = 16; x < 35; x++)// top 2ndcorrder
            {
                mapSquares[x, 0] = 4;
            }
            for (int x = 4; x < 18; x++)// wall 2nd corrder
            {
                mapSquares[27, x] = 4;
            }
            for (int x = 0; x < 10; x++)// wall 2nd corrder
            {
                mapSquares[35, x] = 4;
            }
            for (int x = 30; x < 35; x++)// top 2ndcorrder
            {
                mapSquares[x, 9] = 4;
            }
            mapSquares[30, 4] = 4;//blocks in the second room
            mapSquares[31, 4] = 4;
            mapSquares[32, 4] = 4;
            mapSquares[33, 5] = 4;
            mapSquares[34, 5] = 4;

            mapSquares[28, 6] = 5; //hell remove
            mapSquares[30, 8] = 5; //hell remove

            mapSquares[29, 9] = 5; //hell remove
            mapSquares[28, 9] = 5; //hell remove


            for (int x = 9; x < 24; x++)// wall 3rd corrder
            {
                mapSquares[30, x] = 4;
            }

            for (int x = 19; x < 31; x++)// bottom 3rd corrder
            {
                mapSquares[x, 24] = 4;
            }

            mapSquares[34, 9] = 6; //spikes
            mapSquares[34, 10] = 4; //spikes


            for (int x = 0; x < 28; x++)// top 3rd corrder
            {
                mapSquares[x, 18] = 4;
            }
            mapSquares[29, 20] = 4;

            mapSquares[27, 23] = 4;
            mapSquares[24, 22] = 4;
            mapSquares[21, 21] = 4;
            mapSquares[20, 21] = 4;
            mapSquares[19, 21] = 4;
            mapSquares[19, 22] = 4;
            mapSquares[19, 23] = 4;

            mapSquares[26, 24] = 6; //spikes
            mapSquares[25, 24] = 6; //spikes
            mapSquares[24, 24] = 6; //spikes
            mapSquares[23, 24] = 6; //spikes
            mapSquares[22, 24] = 6; //spikes

            mapSquares[22, 25] = 4; //spikes
            mapSquares[23, 25] = 4; //spikes
            mapSquares[24, 25] = 4; //spikes
            mapSquares[25, 25] = 4; //spikes
            mapSquares[26, 25] = 4; //spikes



            mapSquares[19, 19] = 5; //hell
            mapSquares[19, 20] = 5; //hell



            for (int x = 15; x < 19; x++)// bottom 3rd corrder
            {
                mapSquares[x, 24] = 4;
            }
            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[15, x] = 4;
            }
            mapSquares[14, 27] = 6; //spikes
            mapSquares[13, 27] = 6; //spikes

            mapSquares[13, 28] = 4; //spikes
            mapSquares[14, 28] = 4; //spikes


            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[12, x] = 4;
            }
            mapSquares[11, 24] = 4;
            mapSquares[10, 24] = 4;
            mapSquares[9, 24] = 4;
            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[9, x] = 4;
            }
            mapSquares[8, 27] = 6; //spikes
            mapSquares[7, 27] = 6; //spikes

            mapSquares[8, 28] = 4; //spikes
            mapSquares[7, 28] = 4; //spikes


            mapSquares[8, 25] = 5; //hell
            mapSquares[7, 25] = 5; //hell

            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[6, x] = 4;
            }


            mapSquares[5, 24] = 6; //spikes
            mapSquares[4, 24] = 6; //spikes
            mapSquares[3, 24] = 6; //spikes

            mapSquares[5, 25] = 4; //spikes
            mapSquares[4, 25] = 4; //spikes
            mapSquares[3, 25] = 4; //spikes


            mapSquares[5, 23] = 5;// remove hell variant
            mapSquares[4, 21] = 4;
            mapSquares[3, 21] = 4;
            for (int x = 19; x < 40; x++)// wall 3rd corrder
            {
                mapSquares[0, x] = 4;
            }
            for (int x = 24; x < 36; x++)// wall 3rd corrder
            {
                mapSquares[3, x] = 4;
            }
            mapSquares[3, 24] = 6; //spikes
            mapSquares[3, 25] = 4; //spikes


            for (int x = 0; x < 12; x++)// bottom boss corrder
            {
                mapSquares[x, 40] = 4;
            }
            for (int x = 4; x < 35; x++)// top boss corrder
            {
                mapSquares[x, 35] = 4;
            }

            for (int x = 12; x < 35; x++)// bottom  boss r
            {
                mapSquares[x, 49] = 4;
            }
            for (int x = 40; x < 49; x++)// wall 3rd corrder
            {
                mapSquares[12, x] = 4;
            }
            for (int x = 35; x < 50; x++)// wall 3rd corrder
            {
                mapSquares[35, x] = 4;
            }

            mapSquares[14, 48] = 4;

            mapSquares[13, 46] = 4;
            mapSquares[16, 45] = 4;
            mapSquares[17, 45] = 4;

            mapSquares[20, 44] = 4;
            mapSquares[21, 44] = 4;
            mapSquares[22, 44] = 4;
            mapSquares[23, 44] = 4;
            mapSquares[24, 44] = 4;
            mapSquares[25, 44] = 4;
            mapSquares[26, 44] = 4;
            mapSquares[27, 44] = 4;

            mapSquares[30, 45] = 4;
            mapSquares[31, 45] = 4;

            mapSquares[33, 48] = 4;

            mapSquares[34, 46] = 4;
            //under spikes
            mapSquares[3, 13] = 4; // spikes
            mapSquares[4, 13] = 4; // spikes
            mapSquares[5, 13] = 4; // spikes
            mapSquares[6, 13] = 4; // spikes

            for (int x = 6; x < 13; x++)// wall 3rd corrder
            {
                mapSquares[0, x] = 4;
            }
        }





        public void Level1Hell()
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    mapSquares[x, y] = 2;
                }
            }
            for (int x = 0; x < 10; x++)//top 1st room
            {
                mapSquares[x, 5] = 4;
            }
            for (int x = 0; x < 10; x++)//bottom 1st room
            {
                mapSquares[x, 12] = 4;
            }
            for (int x = 0; x < 3; x++)// 3 tiles spawn
            {
                mapSquares[x, 9] = 4;
            }
            mapSquares[4, 8] = 4; //first jump
            mapSquares[6, 9] = 4; //second jump
            for (int x = 6; x < 10; x++)// wall first room
            {
                mapSquares[9, x] = 4;
            }

            mapSquares[5, 12] = 7; // spikes
            mapSquares[4, 12] = 7; // spikes
            mapSquares[6, 12] = 7; // spikes
            mapSquares[3, 12] = 7; // spikes
                                   //under spikes
            mapSquares[3, 13] = 4; // spikes
            mapSquares[4, 13] = 4; // spikes
            mapSquares[5, 13] = 4; // spikes
            mapSquares[6, 13] = 4; // spikes



            for (int x = 10; x < 19; x++)//bottom first corrder
            {
                mapSquares[x, 12] = 4;
            }

            mapSquares[18, 12] = 7; // spikes
            mapSquares[18, 13] = 4; // spikes




            for (int x = 10; x < 15; x++)// top first corrder
            {
                mapSquares[x, 9] = 4;
            }

            for (int x = 3; x < 13; x++)// wall first corrder
            {
                mapSquares[19, x] = 4;
            }
            for (int x = 0; x < 10; x++)// wall first corrder
            {
                mapSquares[15, x] = 4;
            }
            mapSquares[17, 11] = 4;
            mapSquares[16, 7] = 4;
            mapSquares[18, 5] = 4;//jumps corrider

            for (int x = 19; x < 27; x++)// bottom 2ndcorrder
            {
                mapSquares[x, 3] = 4;
            }
            mapSquares[3, 12] = 7; // spikes
            mapSquares[3, 13] = 4; // spikes


            for (int x = 16; x < 35; x++)// top 2ndcorrder
            {
                mapSquares[x, 0] = 4;
            }
            for (int x = 4; x < 18; x++)// wall 2nd corrder
            {
                mapSquares[27, x] = 4;
            }
            for (int x = 0; x < 10; x++)// wall 2nd corrder
            {
                mapSquares[35, x] = 4;
            }
            for (int x = 30; x < 35; x++)// top 2ndcorrder
            {
                mapSquares[x, 9] = 4;
            }
            mapSquares[30, 4] = 4;//blocks in the second room
            mapSquares[31, 4] = 4;
            mapSquares[32, 4] = 4;
            mapSquares[33, 5] = 4;
            mapSquares[34, 5] = 4;




            for (int x = 9; x < 24; x++)// wall 3rd corrder
            {
                mapSquares[30, x] = 4;
            }

            for (int x = 19; x < 31; x++)// bottom 3rd corrder
            {
                mapSquares[x, 24] = 4;
            }

            mapSquares[34, 9] = 7; //spikes
            mapSquares[34, 10] = 4; //spikes


            for (int x = 0; x < 28; x++)// top 3rd corrder
            {
                mapSquares[x, 18] = 4;
            }
            mapSquares[29, 20] = 4;

            mapSquares[27, 23] = 4;
            mapSquares[24, 22] = 4;
            mapSquares[21, 21] = 4;
            mapSquares[20, 21] = 4;
            mapSquares[19, 21] = 4;
            mapSquares[19, 22] = 4;
            mapSquares[19, 23] = 4;

            mapSquares[26, 24] = 7; //spikes
            mapSquares[25, 24] = 7; //spikes
            mapSquares[24, 24] = 7; //spikes
            mapSquares[23, 24] = 7; //spikes
            mapSquares[22, 24] = 7; //spikes

            mapSquares[22, 25] = 4; //spikes
            mapSquares[23, 25] = 4; //spikes
            mapSquares[24, 25] = 4; //spikes
            mapSquares[25, 25] = 4; //spikes
            mapSquares[26, 25] = 4; //spikes




            for (int x = 15; x < 19; x++)// bottom 3rd corrder
            {
                mapSquares[x, 24] = 4;
            }
            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[15, x] = 4;
            }
            mapSquares[14, 27] = 7; //spikes
            mapSquares[13, 27] = 7; //spikes

            mapSquares[14, 28] = 4; //spikes
            mapSquares[13, 28] = 4; //spikes

            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[12, x] = 4;
            }
            mapSquares[11, 24] = 4;
            mapSquares[10, 24] = 4;
            mapSquares[9, 24] = 4;
            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[9, x] = 4;
            }
            mapSquares[8, 27] = 7; //spikes
            mapSquares[7, 27] = 7; //spikes

            mapSquares[8, 28] = 4; //spikes
            mapSquares[7, 28] = 4; //spikes





            for (int x = 24; x < 28; x++)// wall 3rd corrder
            {
                mapSquares[6, x] = 4;
            }


            mapSquares[5, 24] = 7; //spikes
            mapSquares[4, 24] = 7; //spikes
            mapSquares[3, 24] = 7; //spikes

            mapSquares[5, 25] = 4; //spikes
            mapSquares[4, 25] = 4; //spikes
            mapSquares[3, 25] = 4; //spikes

            mapSquares[4, 21] = 4;
            mapSquares[3, 21] = 4;
            for (int x = 19; x < 40; x++)// wall 3rd corrder
            {
                mapSquares[0, x] = 4;
            }
            for (int x = 24; x < 36; x++)// wall 3rd corrder
            {
                mapSquares[3, x] = 4;
            }
            mapSquares[3, 24] = 7; //spikes

            mapSquares[3, 25] = 4; //spikes


            for (int x = 0; x < 12; x++)// bottom boss corrder
            {
                mapSquares[x, 40] = 4;
            }
            for (int x = 4; x < 35; x++)// top boss corrder
            {
                mapSquares[x, 35] = 4;
            }

            for (int x = 12; x < 35; x++)// bottom  boss r
            {
                mapSquares[x, 49] = 4;
            }
            for (int x = 40; x < 49; x++)// wall 3rd corrder
            {
                mapSquares[12, x] = 4;
            }
            for (int x = 35; x < 50; x++)// wall 3rd corrder
            {
                mapSquares[35, x] = 4;
            }

            mapSquares[14, 48] = 4;

            mapSquares[13, 46] = 4;
            mapSquares[16, 45] = 4;
            mapSquares[17, 45] = 4;

            mapSquares[20, 44] = 4;
            mapSquares[21, 44] = 4;
            mapSquares[22, 44] = 4;
            mapSquares[23, 44] = 4;
            mapSquares[24, 44] = 4;
            mapSquares[25, 44] = 4;
            mapSquares[26, 44] = 4;
            mapSquares[27, 44] = 4;

            mapSquares[30, 45] = 4;
            mapSquares[31, 45] = 4;

            mapSquares[33, 48] = 4;

            mapSquares[34, 46] = 4;

            for (int x = 6; x < 13; x++)// wall 3rd corrder
            {
                mapSquares[0, x] = 4;
            }
        }
            public void Level2Hell()
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    for (int y = 0; y < MapHeight; y++)
                    {
                        mapSquares[x, y] = 2;
                    }
                }

                for (int x = 0; x < 10; x++)//top 1st room
                {
                    mapSquares[x, 0] = 4;
                }
                for (int x = 0; x < 6; x++)// wall first room
                {
                    mapSquares[0, x] = 4;
                }
                for (int x = 0; x < 6; x++)// wall first room
                {
                    mapSquares[9, x] = 4;
                }
                for (int x = 0; x < 6; x++)//top 1st room
                {
                    mapSquares[x, 5] = 4;
                }

                for (int x = 8; x < 10; x++)//top 1st room
                {
                    mapSquares[x, 5] = 4;
                }
                for (int x = 5; x < 13; x++)// wall first room
                {
                    mapSquares[5, x] = 4;
                }
                for (int x = 5; x < 13; x++)// wall first room
                {
                    mapSquares[8, x] = 4;
                }
                for (int x = 0; x < 6; x++)//top 1st room
                {
                    mapSquares[x, 12] = 4;
                }
                for (int x = 12; x < 19; x++)// wall first room
                {
                    mapSquares[0, x] = 4;
                }

                for (int x = 0; x < 6; x++)//top 1st room
                {
                    mapSquares[x, 18] = 4;
                }


                for (int x = 8; x < 27; x++)//top 1st room
                {
                    mapSquares[x, 18] = 4;
                }
                mapSquares[10, 12] = 4;
                mapSquares[9, 12] = 4;
                mapSquares[11, 12] = 4;
                for (int x = 3; x < 13; x++)// wall first room
                {
                    mapSquares[11, x] = 4;
                }
                for (int x = 11; x < 35; x++)//top room 2nd
                {
                    mapSquares[x, 3] = 4;
                }

                mapSquares[16, 17] = 4;
                mapSquares[17, 17] = 4;

                mapSquares[19, 15] = 4;
                mapSquares[20, 15] = 4;

                for (int x = 18; x < 28; x++)//spikes
                {
                    mapSquares[x, 17] = 7;
                }

                mapSquares[27, 18] = 4;
                mapSquares[28, 18] = 4;
                mapSquares[29, 18] = 4;



                mapSquares[33, 18] = 4;
                mapSquares[34, 18] = 4;
                mapSquares[35, 18] = 4;
                mapSquares[36, 18] = 4;
                mapSquares[37, 18] = 4;
                mapSquares[38, 18] = 4;
                mapSquares[39, 18] = 4;
                mapSquares[40, 18] = 4;
                mapSquares[40, 17] = 4;
                mapSquares[40, 16] = 4;
                mapSquares[40, 15] = 4;


                for (int x = 18; x < 29; x++)// wall first room
                {
                    mapSquares[29, x] = 4;
                }

                for (int x = 29; x < 45; x++)
                {
                    mapSquares[x, 28] = 4;
                }

                for (int x = 3; x < 29; x++)// wall first room
                {
                    mapSquares[44, x] = 4;
                }
                mapSquares[30, 22] = 4;
                mapSquares[31, 25] = 4;

                mapSquares[35, 27] = 4;
                mapSquares[35, 26] = 4;

                mapSquares[36, 25] = 4;
                mapSquares[37, 25] = 4;
                mapSquares[38, 25] = 4;

                mapSquares[41, 24] = 4;
                mapSquares[43, 22] = 4;
                mapSquares[40, 27] = 4;

                mapSquares[41, 20] = 4;
                mapSquares[43, 18] = 4;
                mapSquares[41, 16] = 4;

                mapSquares[39, 14] = 4;
                mapSquares[38, 14] = 4;
                mapSquares[37, 14] = 4;

                mapSquares[36, 13] = 4;
                mapSquares[35, 13] = 4;
                mapSquares[34, 13] = 4;

                mapSquares[33, 12] = 4;
                mapSquares[32, 12] = 4;
                mapSquares[31, 12] = 4;

                mapSquares[30, 11] = 4;
                mapSquares[29, 11] = 4;
                mapSquares[28, 11] = 4;

                mapSquares[27, 10] = 4;
                mapSquares[26, 10] = 4;
                mapSquares[25, 10] = 4;
                mapSquares[24, 10] = 4;
                mapSquares[23, 10] = 4;
                mapSquares[22, 10] = 4;
                mapSquares[21, 10] = 4;
                mapSquares[20, 10] = 4;
                mapSquares[19, 10] = 4;
                mapSquares[18, 10] = 4;
                mapSquares[17, 10] = 4;

                for (int x = 18; x < 24; x++)// wall first room
                {
                    mapSquares[5, x] = 4;
                }
                for (int x = 18; x < 24; x++)// wall first room
                {
                    mapSquares[8, x] = 4;
                }

                for (int x = 18; x < 31; x++)// wall first room
                {
                    mapSquares[0, x] = 4;
                }

                for (int x = 0; x < 23; x++)// wall first room
                {
                    mapSquares[x, 30] = 4;
                }
                for (int x = 1; x < 23; x++)// spiikes
                {
                    mapSquares[x, 29] = 7;
                }

                mapSquares[6, 27] = 4;
                mapSquares[7, 27] = 4;
                mapSquares[8, 27] = 4;
                mapSquares[5, 27] = 4;

                mapSquares[12, 27] = 4;
                mapSquares[13, 27] = 4;
                mapSquares[14, 27] = 4;

                mapSquares[18, 27] = 4;
                mapSquares[19, 27] = 4;
                mapSquares[20, 27] = 4;

                mapSquares[23, 29] = 4;
                mapSquares[24, 29] = 4;

                mapSquares[28, 29] = 4;
                for (int x = 19; x < 30; x++)// 
                {
                    mapSquares[29, x] = 4;
                }

                mapSquares[29, 36] = 4;
                mapSquares[28, 36] = 4;
                mapSquares[27, 36] = 4;
                mapSquares[26, 36] = 4;
                for (int x = 36; x < 45; x++)// 
                {
                    mapSquares[29, x] = 4;
                }
                for (int x = 0; x < 50; x++)// 
                {
                    mapSquares[x, 45] = 4;
                }
                for (int x = 20; x < 45; x++)// 
                {
                    mapSquares[0, x] = 4;
                }

                mapSquares[28, 42] = 4;
                mapSquares[27, 42] = 4;
                mapSquares[26, 42] = 4;


                mapSquares[23, 44] = 4;
                mapSquares[22, 44] = 4;
                mapSquares[21, 44] = 4;
                mapSquares[20, 44] = 4;
                mapSquares[19, 44] = 4;
                mapSquares[18, 44] = 4;


                mapSquares[16, 42] = 4;
                mapSquares[16, 43] = 4;
                mapSquares[16, 44] = 4;


                mapSquares[16, 42] = 4;
                mapSquares[16, 43] = 4;
                mapSquares[16, 44] = 4;

                mapSquares[15, 42] = 4;
                mapSquares[15, 43] = 4;
                mapSquares[15, 44] = 4;

                mapSquares[14, 42] = 4;
                mapSquares[14, 43] = 4;
                mapSquares[14, 44] = 4;

                mapSquares[13, 42] = 4;
                mapSquares[13, 43] = 4;
                mapSquares[13, 44] = 4;

                mapSquares[10, 40] = 4;
                mapSquares[9, 40] = 4;

                mapSquares[6, 42] = 4;
                mapSquares[6, 43] = 4;
                mapSquares[6, 44] = 4;


                mapSquares[5, 42] = 4;
                mapSquares[5, 43] = 4;
                mapSquares[5, 44] = 4;


                mapSquares[4, 42] = 4;
                mapSquares[4, 43] = 4;
                mapSquares[4, 44] = 4;

                mapSquares[2, 41] = 4;

                mapSquares[1, 39] = 4;

                mapSquares[4, 38] = 4;
                mapSquares[5, 38] = 4;

                mapSquares[6, 37] = 4;
                mapSquares[7, 37] = 4;

                mapSquares[8, 36] = 4;
                mapSquares[9, 36] = 4;
                mapSquares[10, 36] = 4;

                mapSquares[16, 36] = 4;
                mapSquares[14, 36] = 4;
                mapSquares[15, 36] = 4;

                mapSquares[17, 35] = 4;
                mapSquares[18, 35] = 4;
                mapSquares[19, 35] = 4;
                mapSquares[20, 35] = 4;
                mapSquares[21, 34] = 4;
                mapSquares[22, 34] = 4;
                mapSquares[23, 34] = 4;



                mapSquares[3, 44] = 7;
                mapSquares[2, 44] = 7;
                mapSquares[1, 44] = 7; //spikes
                mapSquares[7, 44] = 7; //spikes
                mapSquares[8, 44] = 7; //spikes
                mapSquares[9, 44] = 7; //spikes
                mapSquares[10, 44] = 7; //spikes
                mapSquares[11, 44] = 7; //spikes
                mapSquares[12, 44] = 7; //spikes

                for (int x = 29; x < 36; x++)// 
                {
                    mapSquares[32, x] = 4;
                }
                for (int x = 32; x < 50; x++)// 
                {
                    mapSquares[x, 28] = 4;
                }
                for (int x = 28; x < 45; x++)//  

                    mapSquares[49, x] = 4;

                mapSquares[37, 42] = 4;
                mapSquares[38, 42] = 4;
                mapSquares[36, 42] = 4;

                mapSquares[44, 42] = 4;
                mapSquares[45, 42] = 4;
                mapSquares[46, 42] = 4;

                mapSquares[43, 40] = 4;
                mapSquares[42, 40] = 4;
                mapSquares[41, 40] = 4;
                mapSquares[40, 40] = 4;
                mapSquares[39, 40] = 4;

                mapSquares[45, 39] = 4;
                mapSquares[46, 39] = 4;

                mapSquares[37, 39] = 4;
                mapSquares[36, 39] = 4;

            }
            #endregion
        
    }
}
