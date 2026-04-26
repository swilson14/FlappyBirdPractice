using FlappyBirdPractice.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice.States
{
    public class TitleScreenState : State
    {
        private SpriteFont FlappyFont;
        private SpriteFont MediumFont;

        public TitleScreenState(SpriteFont flappyFont, SpriteFont mediumFont, StateMachine _stateMachine) : base(_stateMachine)
        {
            FlappyFont = flappyFont;
            MediumFont = mediumFont;
        }

        public override void Update(GameTime gameTime, KeyboardInfo keyboard, MouseInfo mouse)
        {
            if (keyboard.WasKeyJustPressed(Keys.Enter)) 
            {
                _stateMachine.ChangeState("countdown");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string flappyBirdText = "Flappy Bird";
            Vector2 flappyBirdTextSize = FlappyFont.MeasureString(flappyBirdText);
            float flappyBirdTextWidth = flappyBirdTextSize.X;

            string pressEnterText = "Press Enter";
            Vector2 pressEnterTextSize = MediumFont.MeasureString(pressEnterText);
            float pressEnterTextWidth = pressEnterTextSize.X;

            spriteBatch.DrawString(FlappyFont, flappyBirdText, new Vector2((Constants.VIRTUAL_WIDTH - flappyBirdTextWidth) / 2, 64f), Color.White);
            spriteBatch.DrawString(MediumFont, pressEnterText, new Vector2((Constants.VIRTUAL_WIDTH - pressEnterTextWidth) / 2, 105f), Color.White);
        }
    }
}
