using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enclosure_WinForms.Model
{
    enum Player { Red, Blue };
    enum FieldState { Free, RedPlaced, BluePlaced, RedCaptured, BlueCaptured };
    enum GameSize { Small = 6, Medium = 8, Large = 10 };

    public struct Scores
    {
        public int Red;
        public int Blue;
    }

    internal class EnclosureModel
    {
        public EnclosureModel()
        {
            initGame(GameSize.Small);
        }

        public void initGame(GameSize gameSize)
        {
            currentPlayer_ = Player.Blue;
            scores.Blue = scores.Red = 0;

            int size = (int)gameSize;
            FieldState[,] board_ = new FieldState[size, size];
        }
        
        Player currentPlayer_;
        Scores scores;
        FieldState[,] board_; 
    }
}
