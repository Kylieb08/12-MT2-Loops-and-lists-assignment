using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _12_MT2_Loops_and_lists_assignment
{
    enum Screen
    {
        Intro,
        Ikea,
        End
    }
    public class Game1 : Game
    {
        Random generator = new Random();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D rectangleTexture, circleTexture, carTexture, nightTexture;
        List<Texture2D> carTextures = new List<Texture2D>();

        Rectangle signRect, circleRect, signpostRect, roadRect, buildingRect, insideBuildingRect;
        Rectangle parkingGarageHeightLimiterRect, carRect, window;

        SpriteFont titleFont, introFont;

        int roadLineX = 10, pillarX = 300, index = 0;

        MouseState mouseState, prevMouseState;

        Vector2 carSpeed;

        SpriteEffects flipCar;

        bool carClicked = false;

        float lightLevel;

        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 500);
            screen = Screen.Intro;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            this.Window.Title = "Topic 2";

            signRect = new Rectangle(28, 35, 192, 80);
            circleRect = new Rectangle(28, 35, 192, 80);
            signpostRect = new Rectangle(94, 115, 60, 430);

            roadRect = new Rectangle(0, 350, 800, 150);
            buildingRect = new Rectangle(300, 130, 500, 350);
            insideBuildingRect = new Rectangle(300, 275, 500, 150);
            parkingGarageHeightLimiterRect = new Rectangle(300, 285, 500, 5);

            carRect = new Rectangle(0, 305, 170, 145);
            carSpeed = new Vector2(7, 0);

            lightLevel = 1f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("Images/rectangle");
            circleTexture = Content.Load<Texture2D>("Images/circle");
            titleFont = Content.Load<SpriteFont>("Font/titleFont");
            introFont = Content.Load<SpriteFont>("Font/introFont");

            nightTexture = Content.Load<Texture2D>("Images/night_sky");

            carTexture = Content.Load<Texture2D>("Images/car 1");

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
            carRect.X += (int)carSpeed.X;

            if (screen == Screen.Intro)
            {
                if (mouseState.RightButton == ButtonState.Pressed)
                    screen = Screen.Ikea;
            }

            else if (screen == Screen.Ikea)
            {
                if (carRect.X > window.Width)
                {
                    carRect.X = window.Left - carRect.Width;
                    index = generator.Next(carTextures.Count);
                }

                flipCar = SpriteEffects.FlipHorizontally;
                if (NewClick() && carRect.Contains(mouseState.Position))
                    carClicked = true;

                if (carClicked)
                {
                    flipCar = SpriteEffects.None;
                    carRect.Y = 390;
                    carSpeed.X = -7;
                }

                if (!carClicked)
                {
                    flipCar = SpriteEffects.FlipHorizontally;
                    carRect.Y = 305;
                    carSpeed.X = 7;
                }

                if (carRect.Right < window.Left && carSpeed.X < 0)
                {
                    carClicked = false;
                    carTextures.RemoveAt(index);
                    index = generator.Next(carTextures.Count);
                    carRect.X = window.Left - carRect.Width;
                    if (carTextures.Count == 0)
                        screen = Screen.End;
                }

                if (mouseState.ScrollWheelValue > prevMouseState.ScrollWheelValue)
                {
                    lightLevel -= 0.1f;
                    if (lightLevel < 0f)
                        lightLevel = 0f;
                }

                if (mouseState.ScrollWheelValue < prevMouseState.ScrollWheelValue)
                {
                    lightLevel += 0.1f;
                    if (lightLevel > 1f)
                    {
                        lightLevel = 1f;
                    }
                        
                }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.DrawString(introFont, "Welcome to IKEA", new Vector2(10, 10), Color.White);
                _spriteBatch.DrawString(introFont, "IKEA is closed", new Vector2(10, 60), Color.White);
                _spriteBatch.DrawString(introFont, "Your job is to stop cars from coming to IKEA", new Vector2(10, 110), Color.White);
                _spriteBatch.DrawString(introFont, "Left click on them to turn them around", new Vector2(10, 160), Color.White);
                _spriteBatch.DrawString(introFont, "You can use the scroll wheel to change the time of day", new Vector2(10, 210), Color.White);
                _spriteBatch.DrawString(introFont, "If it becomes fully day time, a car will be added", new Vector2(10, 260), Color.White);
                _spriteBatch.DrawString(introFont, "Right click to continue to your job", new Vector2(10, 310), Color.White);
            }

            else if (screen == Screen.Ikea)
            {
                //Sky
                _spriteBatch.Draw(nightTexture, window, Color.White * lightLevel);
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
                _spriteBatch.Draw(carTextures[index], carRect, null, Color.White, 0f, new Vector2(),
                    flipCar, 1f);
            }

            else if (screen == Screen.End)
            {
                _spriteBatch.DrawString(introFont, "Congratulations! You have removed all the cars", new Vector2(10, 10), Color.White);
            }

                _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool NewClick()
        {
            return mouseState.LeftButton == ButtonState.Pressed
                && prevMouseState.LeftButton == ButtonState.Released;
        }
    }
}
