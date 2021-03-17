using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezzur_Wrecked
{
    //by Alexandru Corodati
    class lifeOrb
    {
        public Texture2D orb_texture;
        public Animation orb_anim;
        public Vector2 position;
        public Rectangle rectangle, anim_rect;
        public bool isActive;

        public void Initialize(Animation orb_a,Vector2 pos,Rectangle rect,Texture2D orb_t)
        {
            orb_anim = orb_a;
            position = pos;
            orb_anim.Position = position;
            rectangle = rect;
            orb_texture = orb_t;
            isActive = true;
        }

        public void Update(GameTime gameTime, Player player, Tilemap m)
        {
            if (this.isActive == true)
            {
                Rectangle player_rectangle = new Rectangle((int)player.position.X, (int)player.position.Y, player.Width / 2, player.Height / 2);
                anim_rect = orb_anim.destinationRect;
                if (player_rectangle.Intersects(anim_rect))
                {
                    this.isActive = false;
                    player.death_count = 2;

                }
                
            }
            orb_anim.Position = position;
            orb_anim.Update(gameTime, SpriteEffects.None, orb_texture);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(isActive == true)
            {
                orb_anim.Draw(spriteBatch);
            }
        }
    }
}
