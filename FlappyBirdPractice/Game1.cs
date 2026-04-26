using FlappyBirdPractice.Input;
using FlappyBirdPractice.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.AccessControl;

namespace FlappyBirdPractice
{
    
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private BoxingViewportAdapter _viewportAdapter;
        private OrthographicCamera _camera;

        private KeyboardInfo keyboard;
        private MouseInfo mouse;

        private Texture2D background;
        private Texture2D ground;
        private Texture2D birdImage;
        private Texture2D pipeImage;
        private Texture2D bronzeMedal;
        private Texture2D silverMedal;
        private Texture2D goldMedal;


        private float backgroundScroll = 0;
        private float groundScroll = 0;
        private Bird bird;
        private Random random;

        private SpriteFont smallFont;
        private SpriteFont mediumFont;
        private SpriteFont flappyFont;
        private SpriteFont hugeFont;
        private StateMachine stateMachine;

        private SoundEffect explosionSound;
        private SoundEffect hurtSound;
        private SoundEffect jumpSound;
        private SoundEffect scoreSound;
        private Song gameTheme;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Window Size
            _graphics.PreferredBackBufferWidth = Constants.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = Constants.WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Constants.VIRTUAL_WIDTH, Constants.VIRTUAL_HEIGHT);
            _camera = new OrthographicCamera(_viewportAdapter);
            keyboard = new KeyboardInfo();
            mouse = new MouseInfo();
            random = new Random();
            bird = new Bird(birdImage, jumpSound);

            // Game States
            stateMachine = new StateMachine();
            stateMachine.States = new Dictionary<string, State>
            {
                {"title", new TitleScreenState(flappyFont, mediumFont, stateMachine) },
                {"play", new PlayState(stateMachine, bird, pipeImage, random, flappyFont, scoreSound, explosionSound, hurtSound) },
                {"score", new ScoreState(stateMachine, flappyFont, mediumFont, bronzeMedal, silverMedal, goldMedal) },
                {"countdown", new CountdownState(stateMachine, hugeFont)},
                {"pause", new PauseState(flappyFont, mediumFont, stateMachine) }
            };
            stateMachine.ChangeState("title");

            // Music
            MediaPlayer.IsRepeating = true;
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            MediaPlayer.Play(gameTheme);


        }

        protected override void LoadContent()
        {
            // Load all assets.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");
            ground = Content.Load<Texture2D>("ground");
            birdImage = Content.Load<Texture2D>("bird");
            pipeImage = Content.Load<Texture2D>("pipe");
            bronzeMedal = Content.Load<Texture2D>("bronze_medal");
            silverMedal = Content.Load<Texture2D>("silver_medal");
            goldMedal = Content.Load<Texture2D>("gold_medal");

            smallFont = Content.Load<SpriteFont>("fonts/small");
            mediumFont = Content.Load<SpriteFont>("fonts/medium");
            flappyFont = Content.Load<SpriteFont>("fonts/flappy");
            hugeFont = Content.Load<SpriteFont>("fonts/huge");
            explosionSound = Content.Load<SoundEffect>("audio/explosion");
            hurtSound = Content.Load<SoundEffect>("audio/hurt");
            jumpSound = Content.Load<SoundEffect>("audio/jump");
            scoreSound = Content.Load<SoundEffect>("audio/score");
            gameTheme = Content.Load<Song>("audio/marios_way");
        }

        protected override void Update(GameTime gameTime)
        {
            // Accept User Input
            keyboard.Update();
            mouse.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
                Exit();

            backgroundScroll = (float)(backgroundScroll + Constants.BACKGROUND_SCROLL_SPEED * gameTime.ElapsedGameTime.TotalSeconds) % Constants.BACKGROUND_LOOPING_POINT;
            groundScroll = (float)(groundScroll + Constants.GROUND_SCROLL_SPEED * gameTime.ElapsedGameTime.TotalSeconds) % Constants.VIRTUAL_WIDTH;

            stateMachine.Update(gameTime, keyboard, mouse);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Get the camera's transformation matrix
            Matrix transformMatrix = _camera.GetViewMatrix();

            // Apply the transformation to the sprite batch
            _spriteBatch.Begin(transformMatrix: transformMatrix,samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(background, new Vector2(-backgroundScroll,0) , Color.White);
            stateMachine.Draw(_spriteBatch);
            _spriteBatch.Draw(ground, new Vector2(-groundScroll, Constants.VIRTUAL_HEIGHT - 16), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
