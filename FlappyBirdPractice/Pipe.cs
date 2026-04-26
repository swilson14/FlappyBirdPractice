using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice
{
    public class Pipe
    {
        private Texture2D _texture;

        public const int PIPE_SCROLL = -60;

        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public string Orientation { get; set; }

        public Pipe(string orientation, Texture2D texture, float y)
        {
            _texture = texture;
            X = Constants.VIRTUAL_WIDTH;
            Y = y;
            Width = _texture.Width;
            Height = Constants.PIPE_HEIGHT;
            Orientation = orientation;
        }

        public void Update(GameTime gameTime)
        {
            X = (float)(X + PIPE_SCROLL * gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pipeLocation = new Vector2(X, Y);
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (Orientation == "top")
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }

            spriteBatch.Draw(_texture, pipeLocation, null, Color.White, 0.0f, Vector2.Zero, new Vector2(1f,1f), spriteEffects, 0.0f);
        }
    }
}
