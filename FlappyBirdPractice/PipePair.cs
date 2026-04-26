using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice
{
    public class PipePair
    {
       
        public float X { get; set; }

        public float Y { get; set; }

        public Dictionary<string, Pipe> Pipes { get; set; }

        public bool Remove { get; set; }

        public bool Scored { get; set; }

        private Random _random;

        public PipePair(Texture2D pipeImage, int y, Random random)
        {
            X = Constants.VIRTUAL_WIDTH + 32;
            Y = y;
            _random = random;
            Pipes = new Dictionary<string, Pipe>();
            Pipes.Add("upper", new Pipe("top", pipeImage, y));
            int gapHeight = _random.Next(90,105);
            Pipes.Add("lower", new Pipe("bottom", pipeImage, y + Constants.PIPE_HEIGHT + gapHeight));
            Remove = false;
            Scored = false;
        }

        public void Update(GameTime gameTime)
        {
            if (X > -Constants.PIPE_WIDTH)
            {
                X -= (float)(Constants.PIPE_SPEED * gameTime.ElapsedGameTime.TotalSeconds);
                Pipes["lower"].X = X;
                Pipes["upper"].X = X;
            }
            else
            {
                Remove = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var pipe in Pipes.Values)
            {
                pipe.Draw(spriteBatch);
            }
        }
    }
}
