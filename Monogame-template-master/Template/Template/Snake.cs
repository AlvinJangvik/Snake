using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Template
{
    class Snake : Texture_Base
    {
        private static bool quit = false;
        private static Game1 game = new Game1();
        private static int speed = 6;
        private static int TIMER = 7;
        private static int GROW_SIZE = 5;
        private static int timer = TIMER;
        private static float turn = 2;
        private static int length = 10;
        private static int SIZE = 20;
        private static Vector2[] currentPos = new Vector2[length];
        private static Vector2[] oldPos = new Vector2[length];
        private static Vector2[] tempPos = new Vector2[length];
        private static Vector2[] oldTempPos = new Vector2[length];
        private enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }
        private Direction dir = Direction.Up;

        public static bool Quit
        {
            get { return quit; }
        }
        public static int Size
        {
            get { return SIZE; }
        }

        public Snake(Texture2D skin)
        {
            tex = skin;
            for(int i = 0; i < length; i++)
            {
                currentPos[i] = new Vector2(410 + (i * 10),240);
                oldPos[i] = new Vector2(410 + (i * 10), 240);
            }
        }

        private void Controll()
        {
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.W) && dir != Direction.Down)
            {
                dir = Direction.Up;
            }
            else if (kState.IsKeyDown(Keys.A) && dir != Direction.Right)
            {
                dir = Direction.Left;
            }
            else if (kState.IsKeyDown(Keys.S) && dir != Direction.Up)
            {
                dir = Direction.Down;
            }
            else if (kState.IsKeyDown(Keys.D) && dir != Direction.Left)
            {
                dir = Direction.Right;
            }
        }

        private void Movement()
        {
            if(dir == Direction.Up)
            {
                currentPos[0].Y-= speed;
            }
            else if (dir == Direction.Down)
            {
                currentPos[0].Y += speed;
            }
            else if (dir == Direction.Right)
            {
                currentPos[0].X += speed;
            }
            else if (dir == Direction.Left)
            {
                currentPos[0].X -= speed;
            }
        }

        private static void Tail()
        {
            for(int i = 1; i < length; i++)
            {
                currentPos[i] = oldPos[i - 1];
            }
        }

        public static void grow()
        {
            length+=GROW_SIZE;
            tempPos = new Vector2[length];
            oldTempPos = new Vector2[length];
            for (int i = 0; i < length; i++)
            {
                if (i < length - GROW_SIZE)
                {
                    tempPos[i] = currentPos[i];
                    oldTempPos[i] = oldPos[i];
                }
                else
                {

                    tempPos[i] = new Vector2(-100);
                    oldTempPos[i] = new Vector2(-100);
                }
            }
            currentPos = new Vector2[length];
            oldPos = new Vector2[length];
            for (int i = 0; i < length - 1; i++)
            {
                currentPos[i] = tempPos[i];
                oldPos[i] = oldTempPos[i];
            }
            Tail();
        }

        private void Lose()
        {
            for(int i = 1; i < length; i++)
            {
                if(Collision.Vector2(currentPos[0], SIZE, currentPos[i], SIZE))
                {
                    quit = true;
                }
            }
            if(currentPos[0].X < 0 || currentPos[0].X > 820 - SIZE)
            {
                quit = true;
            }
            else if (currentPos[0].Y < 0 || currentPos[0].Y > 480 - SIZE)
            {
                quit = true;
            }
        }

        public void Update()
        {
            if(timer == 0) { timer = TIMER - speed; }
            if(turn <= 0) { turn = 3; }
            Lose();
            if(Collision.Vector2( Apple.Pos, Apple.Size, currentPos[0], SIZE))
            {
                Highscore.Add();
                grow();
                Apple.Set_pos();
            }


            Controll();
            Movement();
            if (timer == 1)
            {
                Tail();
            }

            if (turn == 1)
            {
                oldPos[0] = tempPos[0];
                for (int i = 1; i < length; i++)
                {
                    oldPos[i] = currentPos[i];
                }
            }
            else if(turn == 3)
            {
                tempPos[0] = currentPos[0];
            }

            turn-= 1;
            timer--;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            First_draw(spriteBatch);
            for(int i = 1; i < length; i++)
            {
                spriteBatch.Draw(tex, new Rectangle((int)currentPos[i].X, (int)currentPos[i].Y, SIZE, SIZE), Color.Blue);
            }
        }

        private void First_draw(SpriteBatch spriteBatch)
        {
            int temp = SIZE / 2;
            if(dir == Direction.Up)
            {
                spriteBatch.Draw(tex, new Rectangle((int)currentPos[0].X, (int)currentPos[0].Y, SIZE, SIZE + temp), Color.Blue);
            }
            else if (dir == Direction.Down)
            {
                spriteBatch.Draw(tex, new Rectangle((int)currentPos[0].X, (int)currentPos[0].Y - temp, SIZE, SIZE + temp), Color.Blue);
            }
            else if (dir == Direction.Left)
            {
                spriteBatch.Draw(tex, new Rectangle((int)currentPos[0].X, (int)currentPos[0].Y, SIZE + temp, SIZE), Color.Blue);
            }

            else if (dir == Direction.Right)
            {
                spriteBatch.Draw(tex, new Rectangle((int)currentPos[0].X - temp, (int)currentPos[0].Y, SIZE + temp, SIZE), Color.Blue);
            }
        }
    }
}
