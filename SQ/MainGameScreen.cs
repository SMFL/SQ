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
            if (player.getMovingBool() == false)
            {
                player.setProperGridPosition(map.NumberOfFloorObjects);
                int gridPosition = player.getProperGridPosition();
                player.setMovementBools(map.CanPlayerMoveToTile(gridPosition - map.NumberOfFloorObjects), map.CanPlayerMoveToTile(gridPosition - 1), map.CanPlayerMoveToTile(gridPosition + 1), map.CanPlayerMoveToTile(gridPosition + map.NumberOfFloorObjects));
            }
            player.Update(gameTime);
           
            cam.Position = new Vector2(player.SpritePOS.X - (ScreenManager.Instance().ScreenDimensions.X / 2), player.SpritePOS.Y - (ScreenManager.Instance().ScreenDimensions.Y / 2));

            base.Update(gameTime, cam);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            map.Draw(spriteBatch);
            map.DrawBeforePlayer(player.getProperGridPosition(), spriteBatch);
            player.Draw(spriteBatch);
            map.DrawAfterPlayer(player.getProperGridPosition(), spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
