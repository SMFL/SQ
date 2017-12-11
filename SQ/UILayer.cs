using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{

    public class UILayer
    {

        public UILayer()
        {

        }

        public void LoadContent(ContentManager Content)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void Update(GameTime gameTime, Camera cam, MouseCursor mouse, MouseState PreviousMouseState)
        {

        }




    }

    public class UIelements
    {
        public SpriteFont font;
        public BasicSprite UiItem;
        public bool active;
        public Vector2 UIoffset;

        public string Name;
        public int Amount;

        public virtual void Update(GameTime gameTime, Camera cam)
        {
            UiItem.SpritePOS.X = (int)cam.Position.X + (int)UIoffset.X;
            UiItem.SpritePOS.Y = (int)cam.Position.Y + (int)UIoffset.Y;
        }

        public virtual void Update(GameTime gameTime, Camera cam, MouseCursor mouse, MouseState previousMouseState)
        {
            UiItem.SpritePOS.X = (int)cam.Position.X + (int)UIoffset.X;
            UiItem.SpritePOS.Y = (int)cam.Position.Y + (int)UIoffset.Y;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                UiItem.Draw(spriteBatch);
            }
        }

    }

}