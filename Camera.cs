using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rezzur_Wrecked
{
    public  class Camera
    {
        //by Sam Tautvydas
        #region Var Declarations
        private static Vector2 position = Vector2.Zero;
        private static Vector2 viewPortSize = Vector2.Zero;
        private static Rectangle worldRectangle = new Rectangle(0, 0, 0, 0);
        #endregion

        #region Properties 
        public static Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = new Vector2(MathHelper.Clamp(value.X, worldRectangle.X, worldRectangle.Width - ViewPortWidth), MathHelper.Clamp(value.Y, worldRectangle.Y, worldRectangle.Height - ViewPortHeight));
            }
        }
        public static Rectangle WorldRectangle
        {
            get { return worldRectangle; }
            set { worldRectangle = value; }
        }
        public static int ViewPortWidth
        {
            get { return (int)viewPortSize.X; }
            set { viewPortSize.X = value; }
        }
        public static int ViewPortHeight
        {
            get { return (int)viewPortSize.Y; }
            set { viewPortSize.Y = value; }
        }
        public static Rectangle ViewPort
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, ViewPortWidth, ViewPortHeight);
            }
        }
        #endregion 
        #region Public Methods 
        public static void Move(Vector2 offset)
        {
            Position += offset;
        }
        public static bool ObjectIsVisible(Rectangle bounds)
        {
            return (ViewPort.Intersects(bounds));
        }
        public static Vector2 Transform(Vector2 point)
        {
            return point - position;
        }
        public static Rectangle Transform(Rectangle rectangle)
        {
            return new Rectangle(rectangle.Left - (int)position.X, rectangle.Top - (int)position.Y, rectangle.Width, rectangle.Height);
        }

        // By Corodati Alexandru;

        public Matrix trans { get; private set; }

        public void Follow(Player player)
        {
            var position = Matrix.CreateTranslation(
                -player.position.X - (player.p_rectangle.Width / 2),
                -player.position.Y - (player.p_rectangle.Height / 2),
                0);
            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);
            trans = position * offset;
        }
        public void Follow(Button player)
        {
            var position = Matrix.CreateTranslation(
                -player.btnPosition.X - (player.btnRectangle.Width/2),
                -player.btnPosition.Y - (player.btnRectangle.Height/2),
                0);
            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth /2 + 47,
                Game1.ScreenHeight /2,
                0);
            trans = position * offset;
        }
        
        #endregion
    }
}

