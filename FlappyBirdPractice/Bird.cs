using FlappyBirdPractice.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice
{

    public class Bird
    {
        private Texture2D _texture;
        public float Width { get; set; }

        public float Height { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float DY { get; set; }

        private SoundEffect _jumpSound;

        public Bird(Texture2D texture, SoundEffect jumpSound)
        {
            _texture = texture;
            Width = texture.Width;
            Height = texture.Height;
            X = Constants.VIRTUAL_WIDTH / 2 - (Width / 2);
            Y = Constants.VIRTUAL_HEIGHT / 2 - (Height / 2);
            DY = 0;
            _jumpSound = jumpSound;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(X,Y), Color.White);
        }

        public void Update(GameTime gameTime, KeyboardInfo keyboardInfo, MouseInfo mouse)
        {
            DY = (float)(DY + Constants.GRAVITY * gameTime.ElapsedGameTime.TotalSeconds);

            if (keyboardInfo.WasKeyJustPressed(Keys.Space) || mouse.WasButtonJustPressed(MouseButton.Left))
            {
                DY = Constants.ANTI_GRAVITY;
                _jumpSound.Play();
            }

            Y += DY;
        }

        public bool Collides(Pipe pipe)
        {
            if ( (X + 2) + (Width - 4) >= pipe.X && X + 2 <= pipe.X + pipe.Width)
            {
                if ( (Y + 2) + (Height - 4) >= pipe.Y && Y + 2 <= pipe.Y + pipe.Height)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
