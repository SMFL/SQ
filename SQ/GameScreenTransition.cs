using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SQ
{
    class GameScreenTransition
    {
        #region Variables
        public bool IsScreenChanging = false;
        public bool ChangeBack;
        public bool KeepContent;
        public float Alpha = 0.0f;
        public float SpeedOfTransition = 0.01f;
        Texture2D FadeTexture;
        GameScreen NewGameScreen;
        ContentManager Content;
        Vector2 FadePosition;
        #endregion

        #region GameScreenSpecificFunctions
        public GameScreenTransition(Texture2D fadeTexture, ContentManager content)
        {
            FadeTexture = fadeTexture;
            Content = content;
            FadePosition = new Vector2();
        }

        public void ScreenChangeBack(GameScreen gameScreen)
        {
            IsScreenChanging = true;
            ChangeBack = true;
            NewGameScreen = gameScreen;
            KeepContent = false;
            Alpha = 0.0f;
            FadeTexture = Content.Load<Texture2D>("FadeImage");
        }

        public void ScreenChange(GameScreen gameScreen)
        {
            IsScreenChanging = true;
            ChangeBack = false;
            NewGameScreen = gameScreen;
            KeepContent = false;
            Alpha = 0.0f;
            FadeTexture =  Content.Load<Texture2D>("FadeImage");
        }

        public void ScreenChangeKeepContent(GameScreen gameScreen)
        {
            IsScreenChanging = true;
            ChangeBack = false;
            NewGameScreen = gameScreen;
            KeepContent = true;
            Alpha = 0.0f;
            FadeTexture = Content.Load<Texture2D>("FadeImage");
        }

        public void ScreenChangeBackKeepContent(GameScreen gameScreen)
        {
            IsScreenChanging = true;
            ChangeBack = true;
            NewGameScreen = gameScreen;
            KeepContent = true;
            Alpha = 0.0f;
            FadeTexture = Content.Load<Texture2D>("FadeImage");
        }
        #endregion

        #region BaseFunction
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(FadeTexture, new Rectangle((int) FadePosition.X, (int) FadePosition.Y, (int)ScreenManager.Instance().ScreenDimensions.X, (int)ScreenManager.Instance().ScreenDimensions.Y), new Color(Color.White, Alpha));

        }

        public void Update(GameTime gameTime, Camera cam)
        {
            FadePosition = cam.Position;
            if (IsScreenChanging == true)
                if (Alpha != 1)
                    Alpha += SpeedOfTransition;

            if (Alpha >= 0.9f)
            {
                if(ChangeBack == false)
                {
                    if (KeepContent == false)
                    {
                        ScreenManager.Instance().CurrentGameScreen.UnloadContent();
                        Content.Unload();
                    }

                    NewGameScreen.LoadContent(Content);
                    ScreenManager.Instance().CurrentGameScreen = NewGameScreen;

                    Alpha = 0.0f;
                    IsScreenChanging = false;
                }
                else
                {
                    if(KeepContent == false)
                    {
                        ScreenManager.Instance().CurrentGameScreen.UnloadContent();
                        Content.Unload();
                    }


                    ScreenManager.Instance().CurrentGameScreen = NewGameScreen;

                    Alpha = 0.0f;
                    IsScreenChanging = false;
                }


            }


        }
        #endregion
    }
}
