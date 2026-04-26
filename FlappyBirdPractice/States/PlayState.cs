using FlappyBirdPractice.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice.States
{
    
    public class PlayState : State
    {
        private Bird _bird;
        private List<PipePair> _pipePairs;
        private float spawnTimer = 0f;
        private int lastY;
        private Texture2D _pipeImage;
        private Random _random;
        private int score;
        private SpriteFont FlappyFont;
        private SoundEffect _scoreSound;
        private SoundEffect _explosionSound;
        private SoundEffect _hurtSound;
        private float _spawnTime;

        public PlayState(StateMachine stateMachine, Bird bird, Texture2D pipeImage, Random random, SpriteFont flappyFont, SoundEffect scoreSound, SoundEffect explosionSound, SoundEffect hurtSound) : base(stateMachine) 
        {
           _bird = bird;
           _pipeImage = pipeImage;
            _random = random;
            lastY = -Constants.PIPE_HEIGHT + _random.Next(80) + 20;
            _pipePairs = new List<PipePair>();
            score = 0;
            FlappyFont = flappyFont;
            _scoreSound = scoreSound;
            _explosionSound = explosionSound;
            _hurtSound = hurtSound;
            _spawnTime = 2;
        }

        public override void Enter(params object[] args)
        {
            if (args.Count() < 1)
            {
                // Reset state.
                lastY = -Constants.PIPE_HEIGHT + _random.Next(80) + 20;
                _pipePairs = new List<PipePair>();
                score = 0;
                _bird.X = Constants.VIRTUAL_WIDTH / 2 - (_bird.Width / 2);
                _bird.Y = Constants.VIRTUAL_HEIGHT / 2 - (_bird.Height / 2);
                _bird.DY = 0;
            }
        }

        public override void Update(GameTime gameTime, KeyboardInfo keyboard, MouseInfo mouse)
        {
            if (keyboard.WasKeyJustPressed(Keys.Enter))
            {
                _stateMachine.ChangeState("pause", true);
            }


            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > _spawnTime)
            {
                int y = Math.Max(-Constants.PIPE_HEIGHT + 10, Math.Min(lastY + _random.Next(-20, 21),
                    Constants.VIRTUAL_HEIGHT - 90 - Constants.PIPE_HEIGHT));

                lastY = y;

                _pipePairs.Add(new PipePair(_pipeImage, y, _random));

                spawnTimer = 0;
                _spawnTime = _random.Next(2, 5);
            }

            _bird.Update(gameTime, keyboard, mouse);

            if (_pipePairs.Count > 0)
            {
                for (int i = 0; i < _pipePairs.Count; i++)
                {
                    var pair = _pipePairs[i];

                    pair.Update(gameTime);

                    foreach (var pipe in pair.Pipes)
                    {
                        if (_bird.Collides(pipe.Value))
                        {
                            _stateMachine.ChangeState("score", score);
                            _explosionSound.Play();
                            _hurtSound.Play();
                        }
                    }

                    if (!pair.Scored)
                    {
                        if (pair.X + Constants.PIPE_WIDTH < _bird.X)
                        {
                            score += 1;
                            pair.Scored = true;
                            _scoreSound.Play();
                        }
                    }
                }

                for (int i = 0; i < _pipePairs.Count; i++)
                {
                    var pair = _pipePairs[i];

                    if (pair.Remove)
                    {
                        _pipePairs.Remove(pair);
                    }
                }

                if (_bird.Y > Constants.VIRTUAL_HEIGHT - 15)
                {
                    _stateMachine.ChangeState("score", score);
                    _explosionSound.Play();
                    _hurtSound.Play();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_pipePairs.Count > 0)
            {
                foreach (var pair in _pipePairs)
                {
                    pair.Draw(spriteBatch);
                }
            }

            spriteBatch.DrawString(FlappyFont, $"Score: {score}", new Vector2(8,8), Color.White);

            _bird.Draw(spriteBatch);
        }
    }
}
