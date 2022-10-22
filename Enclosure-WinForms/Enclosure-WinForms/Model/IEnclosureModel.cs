using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public FieldState FieldAt(int x, int y);
        public event EventHandler<Player> CurrentPlayerCnahged;
        public event EventHandler<Tuple<Player?, Scores>> GameFinished;
        public event EventHandler<Scores> ScoresCnahged;
        public event EventHandler<FieldState[,]> BoardCnahged;
        public event EventHandler<Tuple<int, int>> RecursionStarted;
        public event EventHandler<Tuple<int, int>> RecursionFinished;
        public void NewGame(GameSize gameSize = GameSize.Small);
        public void ClickedAt(int x, int y);
        public Task SaveAsync(String filename);
        public Task LoadAsync(String filename);
    }
}
