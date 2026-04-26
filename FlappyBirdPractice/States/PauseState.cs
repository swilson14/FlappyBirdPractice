using FlappyBirdPractice.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice.States
{
    public class PauseState : State
    {
        private SpriteFont FlappyFont;
        private SpriteFont MediumFont;
        public PauseState(SpriteFont flappyFont, SpriteFont mediumFont, StateMachine stateMachine) : base(stateMachine)
        {
            FlappyFont = flappyFont;
            MediumFont = mediumFont;
        }

        public override void Update(GameTime gameTime, KeyboardInfo keyboard, MouseInfo mouse)
        {
            if (keyboard.WasKeyJustPressed(Keys.Enter))
            {
                _stateMachine.ChangeState("play", true);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string gamePausedText = "Game Paused";
            float pausedTextWidth = FlappyFont.MeasureString(gamePausedText).X;


            string pressEnterText = "Press Enter to resume";
            float pressEnterTextWidth = MediumFont.MeasureString(pressEnterText).X;

            spriteBatch.DrawString(FlappyFont, gamePausedText, new Vector2((Constants.VIRTUAL_WIDTH - pausedTextWidth) / 2, 64f), Color.White);
            spriteBatch.DrawString(MediumFont, pressEnterText, new Vector2((Constants.VIRTUAL_WIDTH - pressEnterTextWidth) / 2, 105f), Color.White);
        }


    }
}
