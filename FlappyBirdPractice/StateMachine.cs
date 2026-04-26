using FlappyBirdPractice.Input;
using FlappyBirdPractice.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdPractice
{
    public class StateMachine
    {     
        private State CurrentState;

        public Dictionary<string, State> States { get; set; }

        public void ChangeState(string state, params object[] args) 
        {
            CurrentState?.Exit();
            CurrentState = States[state];
            CurrentState?.Enter(args);
        }

        public void Update(GameTime gameTime, KeyboardInfo keyboard, MouseInfo mouse)
        {
            CurrentState.Update(gameTime, keyboard, mouse);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentState.Draw(spriteBatch);
        }
    }
}
