using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{ 
    public class Game1 : Game
    {
        #region BaseVariables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Camera camera2D;
        #endregion

        #region BaseFunctions
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.Viewport.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.Viewport.Height;
            ScreenManager.Instance().ScreenDimensions.X = graphics.GraphicsDevice.Viewport.Width;
            ScreenManager.Instance().ScreenDimensions.Y = graphics.GraphicsDevice.Viewport.Height;
            graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphics.GraphicsDevice.BlendState = BlendState.Opaque;
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();
            camera2D = new Camera(graphics.GraphicsDevice.Viewport);
            camera2D.Zoom = 1f;


            this.IsMouseVisible = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance().LoadContent(Content);


        }

        protected override void UnloadContent()
        {
            ScreenManager.Instance().UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            ScreenManager.Instance().Update(gameTime, camera2D);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(100, 179, 188));

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, camera2D.GetViewMatrix());

            ScreenManager.Instance().Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
