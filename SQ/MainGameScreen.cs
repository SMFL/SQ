using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{
    class MainGameScreen : GameScreen
    {
        MapGeneration map;
        Player player;
        public override void LoadContent(ContentManager content)
        {
            map = new MapGeneration();
            player = new Player(content.Load<Texture2D>("Character"), new Rectangle(0,0,32,42),new Rectangle(0,0,16,21),4,100,0,2,0,0);
            map.CreateMap(content);


        }

        public override void Update(GameTime gameTime, Camera cam)
        {
            player.Update(gameTime);

            cam.Position = new Vector2(player.SpritePOS.X - (ScreenManager.Instance().ScreenDimensions.X / 2), player.SpritePOS.Y - (ScreenManager.Instance().ScreenDimensions.Y / 2));

            base.Update(gameTime, cam);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
