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
                if (board_[x, y] != FieldState.Free || !hasCommonNeighbour(x, y))
                    return;

                board_[x, y] = (FieldState)currentPlayer_;
                captureSuroundedFields();

                lastClickPos = null;
                currentPlayer_ = (Player)((int)currentPlayer_ % 2 + 1);
            }



            BoardCnahged?.Invoke(this, board_);
        }

        private void initGame(GameSize gameSize)
        {
            currentPlayer_ = Player.Blue;
            CurrentPlayerCnahged?.Invoke(this, currentPlayer_);

            scores.Blue = scores.Red = 0;
            ScoresCnahged?.Invoke(this, scores);

            int size = (int)gameSize;
            board_ = new FieldState[size, size];
            BoardCnahged?.Invoke(this, board_);
        }

        private bool hasCommonNeighbour(int x, int y)
        {
            if (lastClickPos == null)
                return false;

            int l_x = lastClickPos.Value.x;
            int l_y = lastClickPos.Value.y;

            bool result = (x - 1 == l_x && y == l_y) ||
                          (x + 1 == l_x && y == l_y) ||
                          (x == l_x && y - 1 == l_y) ||
                          (x == l_x && y + 1 == l_y);
            return result;
        }

        private void captureSuroundedFields()
        {
            int n = BoardSize;


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {

                    try
                    {
                        bool[,] checkedFields = new bool[n, n];
                        Player? capturer = AmISurrounded(i, j, checkedFields);
                        if (capturer != null)
                            board_[i, j] = (FieldState)(int)capturer.Value + 2;

                    }
                    catch (IndexOutOfRangeException e) { }
                }
            }
        }

        Player? AmISurrounded(int x, int y, bool[,] checkedFields)
        {
            checkedFields[x, y] = true;
            Player? capturer = null;

            Player? left = null;
            if ((int)board_[x - 1, y] == (int)currentPlayer_)
                left = (Player)board_[x - 1, y];
            else if (!checkedFields[x - 1, y])
                left = AmISurrounded(x - 1, y, checkedFields);


            Player? right = null;
            if ((int)board_[x + 1, y] == (int)currentPlayer_)
                right = (Player)board_[x + 1, y];
            else if (!checkedFields[x + 1, y])
                right = AmISurrounded(x + 1, y, checkedFields);

            Player? top = null;
            if ((int)board_[x, y - 1] == (int)currentPlayer_)
                top = (Player)board_[x, y - 1];
            else if (!checkedFields[x, y - 1])
                top = AmISurrounded(x, y - 1, checkedFields);

            Player? bot = null;
            if ((int)board_[x, y + 1] == (int)currentPlayer_)
                bot = (Player)board_[x, y + 1];
            else if (!checkedFields[x, y + 1])
                bot = AmISurrounded(x, y + 1, checkedFields);

            if (left == null && right == null && top == null && bot == null)
                return null;

            capturer = left ?? right ?? top ?? bot;

            foreach (var res in new List<Player?> { left, right, top, bot })
            {
                if (res != null && res != capturer)
                    capturer = null;
            }

            return capturer;
        }

        IDataAccess dataAccess_;

        Player currentPlayer_;
        Scores scores;
        FieldState[,] board_;

        Pos? lastClickPos = null;
    }
}
