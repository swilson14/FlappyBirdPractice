using FlappyBirdPractice.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice.States
{
    public class ScoreState : State
    {

        private int score;
        private SpriteFont FlappyFont;
        private SpriteFont MediumFont;

        private Texture2D _bronzeMedal;
        private Texture2D _silverMedal;
        private Texture2D _goldMedal;


        public ScoreState(StateMachine stateMachine, SpriteFont flappyFont, SpriteFont mediumFont, Texture2D bronzeMedal, Texture2D silverMedal, Texture2D goldMedal) : base(stateMachine)
        {
            FlappyFont = flappyFont;
            MediumFont = mediumFont;
            _bronzeMedal = bronzeMedal;
            _silverMedal = silverMedal;
            _goldMedal = goldMedal;
        }

        public override void Enter(params object[] args)
        {
            score = (int)args[0];
        }


        public override void Update(GameTime gameTime, KeyboardInfo keyboard, MouseInfo mouse)
        {
            if (keyboard.WasKeyJustPressed(Keys.Enter)) 
            {
                _stateMachine.ChangeState("play");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string youLostText = "Oof! You lost!";
            float youLostWidth = FlappyFont.MeasureString(youLostText).X;
            spriteBatch.DrawString(FlappyFont, youLostText, new Vector2((Constants.VIRTUAL_WIDTH - youLostWidth) / 2, 64), Color.White);

            string scoreText = $"Score: {score}";
            float scoreWidth = MediumFont.MeasureString(scoreText).X;
            spriteBatch.DrawString(MediumFont, scoreText, new Vector2((Constants.VIRTUAL_WIDTH - scoreWidth) / 2, 110), Color.White);

            if (score >= 20)
            {
                spriteBatch.Draw(_goldMedal, new Vector2((Constants.VIRTUAL_WIDTH - scoreWidth) / 2 - 35, 100),  Color.White);
            }
            else if (score >= 10)
            {
                spriteBatch.Draw(_silverMedal, new Vector2((Constants.VIRTUAL_WIDTH - scoreWidth) / 2 - 35, 100), Color.White);
            }
            else if (score >= 5)
            {
                spriteBatch.Draw(_bronzeMedal, new Vector2((Constants.VIRTUAL_WIDTH - scoreWidth) / 2 - 35, 100), Color.White);
            }

            string playAgainText = "Press Enter to Play Again!";
            float playAgainWidth = MediumFont.MeasureString(playAgainText).X;
            spriteBatch.DrawString(MediumFont, playAgainText, new Vector2((Constants.VIRTUAL_WIDTH - playAgainWidth) / 2, 160), Color.White);
        }


    }
}
