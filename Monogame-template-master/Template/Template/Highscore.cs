using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
    class Highscore
    {
        private static int score = 0;
        private static int highScore = 0;
        private SpriteFont text;

        public Highscore(SpriteFont sprf)
        {
            text = sprf;
        }

        public static void Add()
        {
            score++;
        }

        public void Init()
        {
            BinaryReader br = null;
            try
            {
                 br = new BinaryReader(
                        new FileStream("score.data",
                        FileMode.OpenOrCreate,
                        FileAccess.Read));
                highScore = br.ReadInt32();
                br.Close();
            }
            catch
            {
                Write();
            }
            finally
            {
                if (br != null)
                    br.Close();
            }

        }

        private void Write()
        {

            highScore = score;

            BinaryWriter br = null;

            try
            {
                br = new BinaryWriter(
                    new FileStream("score.data",
                    FileMode.OpenOrCreate,
                    FileAccess.Write));
                br.Write(highScore);
                br.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (br != null)
                    br.Close();
            }
        }

        public void Update()
        {
            if(score > highScore)
            {
                Write();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(text, "Score: " + score + "  Highscore: " + highScore, new Vector2(30, 10), Color.Black);
        }
    }
}
