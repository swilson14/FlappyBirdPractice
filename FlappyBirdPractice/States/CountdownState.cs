using FlappyBirdPractice.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice.States
{
    public class CountdownState : State
    {
        public const float COUNTDOWN_TIME = 0.75f;
        private int count;
        private float timer;
        private SpriteFont HugeFont;

        public CountdownState(StateMachine stateMachine, SpriteFont hugeFont) : base(stateMachine)
        {
            count = 3;
            timer = 0;
            HugeFont = hugeFont;
        }

        public override void Update(GameTime gameTime, KeyboardInfo keyboard, MouseInfo mouse)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > COUNTDOWN_TIME)
            {
                timer = timer % COUNTDOWN_TIME;
                count -= 1;
            }

            if (count == 0)
            {
                _stateMachine.ChangeState("play");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float countWidth = HugeFont.MeasureString(count.ToString()).X;
            spriteBatch.DrawString(HugeFont, count.ToString(), new Vector2((Constants.VIRTUAL_WIDTH - countWidth) / 2, 120), Color.White);
        }
    }
}
