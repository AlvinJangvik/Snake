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
    class Apple : Texture_Base
    {
        private static int size = 10;
        private static Vector2 pos;
        private static Random rand = new Random();

        public static Vector2 Pos
        {
            get { return pos; }
        }
        public static int Size
        {
            get { return size; }
        }

        public Apple(Texture2D skin)
        {
            tex = skin;
            Set_pos();
        }

        public static void Set_pos()
        {
            pos = new Vector2(rand.Next(10 + Snake.Size, 800- Snake.Size), rand.Next(10 + Snake.Size, 440 - Snake.Size));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, size, size), Color.Red);
        }
    }
}
