using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// By Reece Brennan

namespace Rezzur_Wrecked
{
    public class Button
    {
        public Texture2D btnTexture;
        public Vector2 btnPosition;
        public Rectangle btnRectangle;

        Color btnColour = new Color(255, 255, 255, 255);// used for visual purposes, it shows that the user is hovering over the button
        bool down;
        public bool isClicked;

        public Button()
        {

        }

        public void btnLoad(Texture2D newBtnTexture, Vector2 newBtnPosition)
        {
            btnTexture = newBtnTexture;
            btnPosition = newBtnPosition;
        }

        public void Update(MouseState mouse)
        {
            mouse = Mouse.GetState();
            
                btnRectangle = new Rectangle((int)btnPosition.X, (int)btnPosition.Y, btnTexture.Width/2, btnTexture.Height/2);

            Rectangle mouseRectangle = new Rectangle(mouse.X - 750, mouse.Y - 375, 1, 1);
            
            if (mouseRectangle.Intersects(btnRectangle))// if the mouse colides with the button...
            {
                if (btnColour.A == 255) down = false;//if the button is visible, down = false
                if (btnColour.A == 0) down = true;// if the button isnt visible, down = true
                if (down) btnColour.A += 3; else btnColour.A -= 3;// if down = true then +3, if down is false then -3
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                    btnColour.A = 255;
                }
                else isClicked = false;
            }
            else if (btnColour.A < 255)
                btnColour.A += 3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(btnTexture !=null)
            spriteBatch.Draw(btnTexture, btnRectangle, btnColour);
        }
    }
}
