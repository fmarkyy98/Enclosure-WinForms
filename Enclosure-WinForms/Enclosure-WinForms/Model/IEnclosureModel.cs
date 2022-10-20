using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enclosure_WinForms.Model
{
    public enum Player { Red = 1, Blue = 2 };
    public enum GameSize { Small = 6, Medium = 8, Large = 10 };
    public enum FieldState { Free, RedPlaced = 1, BluePlaced = 2, RedCaptured, BlueCaptured };
    public struct Scores
    {
        public int Red;
        public int Blue;
    }

    public interface IEnclosureModel
    {
        public int BoardSize { get; }
        public event EventHandler<Player> CurrentPlayerCnahged;
        public event EventHandler<Scores> ScoresCnahged;
        public event EventHandler<FieldState[,]> BoardCnahged;
        public void NewGame(GameSize gameSize = GameSize.Small);
        public void ClickedAt(int x, int y);
    }
}
