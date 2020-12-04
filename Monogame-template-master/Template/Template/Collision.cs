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
    class Collision
    {
        public static bool Vector2(Vector2 first, int sizeF, Vector2 second, int sizeS)
        {
            sizeF = sizeF / 2;
            if(first.X + sizeF > second.X && first.X + sizeF < second.X + sizeS)
            {
                if (first.Y + sizeF > second.Y && first.Y + sizeF < second.Y + sizeS)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
