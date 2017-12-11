using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SQ
{
    class ScreenManager
    {
        #region Variables
        //this means that there is only going to be one 
        private static ScreenManager instance;
        public Vector2 ScreenDimensions;
        public ContentManager Content { private set; get; }
        public GameScreen CurrentGameScreen;
        private GameScreenTransition ScreenChange;
        
        // this is how the attributes will be accessed
        #endregion

        #region ScreenMangerSpecificFunctions
        public static ScreenManager Instance()
        {
            if (instance == null)
                instance = new ScreenManager();
            return instance;
        }

        public ScreenManager()
        {
            
            CurrentGameScreen = new MainGameScreen();

        }

        public void ChangeGameScreen(GameScreen newGameScreen)
        {
            ScreenChange.ScreenChange(newGameScreen);
        }

        public void ChangeGameScreenBack(GameScreen oldGameScreen)
        {
            ScreenChange.ScreenChangeBack(oldGameScreen);
        }

        public void ChangeGameScreenKeepContent(GameScreen newGameScreen)
        {
            ScreenChange.ScreenChangeKeepContent(newGameScreen);
        }

        public void ChangeGameScreenBackkeepcontent(GameScreen oldGameScreen)
        {
            ScreenChange.ScreenChangeBackKeepContent(oldGameScreen);
        }
        #endregion

        #region BaseFunctions
        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            ScreenChange = new GameScreenTransition(content.Load<Texture2D>("FadeImage"), content);
            CurrentGameScreen.LoadContent(content);
    

        }

        public void UnloadContent()
        {
            CurrentGameScreen.UnloadContent();


        }

        public void Update(GameTime gameTime, Camera cam)
        {
            CurrentGameScreen.Update(gameTime, cam);
            ScreenChange.Update(gameTime, cam);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentGameScreen.Draw(spriteBatch);
            if (ScreenChange.IsScreenChanging == true)
            {
                ScreenChange.Draw(spriteBatch);
            }

        }
        #endregion
    }
}
