using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Enclosure_WinForms.Persistance;

namespace Enclosure_WinForms.Model
{
    struct Pos
    {
        public Pos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public int x;
        public int y;
    }

    internal class EnclosureModel : IEnclosureModel
    {
        public int BoardSize { get { return board_.GetLength(0); } }

        public event EventHandler<Player> CurrentPlayerCnahged;
        public event EventHandler<Scores> ScoresCnahged;
        public event EventHandler<FieldState[,]> BoardCnahged;

        public EnclosureModel(IDataAccess dataAccess)
        {
            initGame(GameSize.Small);
            dataAccess_ = dataAccess;
        }

        public void NewGame(GameSize gameSize)
        {
            initGame(gameSize);
        }

        public void ClickedAt(int x, int y)
        {
            if (lastClickPos == null)
            {
                if (board_[x, y] != FieldState.Free)
                    return;

                board_[x, y] = (FieldState)currentPlayer_;
                lastClickPos = new(x, y);
            }
            else // (lsatClickPos != null)
            {
                if (board_[x, y] != FieldState.Free && hasCommonNeighbour(x, y))
                    return;

                board_[x, y] = (FieldState)currentPlayer_;
                lastClickPos = null;
                currentPlayer_ = (Player)((int)currentPlayer_ % 2 + 1);
            }
        }

        private void initGame(GameSize gameSize)
        {
            currentPlayer_ = Player.Blue;
            CurrentPlayerCnahged?.Invoke(this, currentPlayer_);

            scores.Blue = scores.Red = 0;
            ScoresCnahged?.Invoke(this, scores);

            int size = (int)gameSize;
            FieldState[,] board_ = new FieldState[size, size];
            BoardCnahged?.Invoke(this, board_);
        }

        private bool hasCommonNeighbour(int x, int y)
        {
            if (lastClickPos == null)
                return false;

            int l_x = lastClickPos.Value.x;
            int l_y = lastClickPos.Value.y;

            bool result = (x-1 == l_x && y   == l_y) ||
                          (x+1 == l_x && y   == l_y) ||
                          (x   == l_x && y-1 == l_y) ||
                          (x   == l_x && y+1 == l_y);
            return result;
        }

        IDataAccess dataAccess_;

        Player currentPlayer_;
        Scores scores;
        FieldState[,] board_;

        Pos? lastClickPos = null;
    }
}
