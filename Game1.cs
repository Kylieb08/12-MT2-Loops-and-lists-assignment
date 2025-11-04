using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace _12_MT2_Loops_and_lists_assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D rectangleTexture, circleTexture;
        List<Texture2D> carTextures = new List<Texture2D>();
        List<Texture2D> drawCarTextures = new List<Texture2D>();

        Rectangle signRect, circleRect, signpostRect, roadRect, buildingRect, insideBuildingRect;
        Rectangle parkingGarageHeightLimiterRect, carRect;
        List<Rectangle> carRects = new List<Rectangle>();

        SpriteFont titleFont;

        int roadLineX = 10, pillarX = 300;

        MouseState mouseState, prevMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this.Window.Title = "Topic 2";

            signRect = new Rectangle(28, 35, 192, 80);
            circleRect = new Rectangle(28, 35, 192, 80);
            signpostRect = new Rectangle(94, 115, 60, 430);
            roadRect = new Rectangle(0, 350, 800, 150);
            buildingRect = new Rectangle(300, 130, 500, 350);
            insideBuildingRect = new Rectangle(300, 275, 500, 150);
            parkingGarageHeightLimiterRect = new Rectangle(300, 285, 500, 5);
            carRect = new Rectangle(640, 305, 170, 145); //y coords: 305-450

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("Images/rectangle");
            circleTexture = Content.Load<Texture2D>("Images/circle");
            titleFont = Content.Load<SpriteFont>("Font/titleFont");

            for (int i = 1; i <= 4; i++)
                carTextures.Add(Content.Load<Texture2D>("Images/car " + i));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            this.Window.Title = "x = " + mouseState.X + ", y = " + mouseState.Y;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //Sign
            _spriteBatch.Draw(rectangleTexture, signRect, Color.Blue);
            //Circle part of sign
            _spriteBatch.Draw(circleTexture, circleRect, Color.Yellow);
            //Sign post
            _spriteBatch.Draw(rectangleTexture, signpostRect, Color.DarkGray);
            //Building
            _spriteBatch.Draw(rectangleTexture, buildingRect, Color.Blue);
            _spriteBatch.Draw(rectangleTexture, insideBuildingRect, Color.Black);
            //Road
            _spriteBatch.Draw(rectangleTexture, roadRect, Color.DimGray);

            //Road lines
            for (int i = 0; i < 20; i++)
            {
                _spriteBatch.Draw(rectangleTexture, new Rectangle(i * 50 + roadLineX, 425, 40, 10), Color.Yellow);
            }

            //Parking garage height limiter
            _spriteBatch.Draw(rectangleTexture, parkingGarageHeightLimiterRect, Color.Red);

            //Parking garage pillars
            for (int i = 0; i < 7; i++)
            {
                _spriteBatch.Draw(rectangleTexture, new Rectangle(i * 100 + pillarX, 275, 50, 75), Color.Gray);
            }

            //Text
            _spriteBatch.DrawString(titleFont, "IKEA", new Vector2(50, 47), Color.Blue);
            _spriteBatch.DrawString(titleFont, "IKEA", new Vector2(314, 142), Color.Yellow);

            //Cars
            _spriteBatch.Draw(carTextures[3], carRect, null, Color.White, 0f, new Vector2(),
                SpriteEffects.FlipHorizontally, 1f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}