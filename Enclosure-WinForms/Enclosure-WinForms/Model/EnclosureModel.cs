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
        // signals:
        public event EventHandler<Player> CurrentPlayerCnahged;
        public event EventHandler<Tuple<Player?, Scores>> GameFinished;
        public event EventHandler<Scores> ScoresCnahged;
        public event EventHandler<FieldState[,]> BoardCnahged;
        public event EventHandler<Tuple<int, int>> RecursionStarted;
        public event EventHandler<Tuple<int, int>> RecursionFinished;

        // public:
        public int BoardSize { get { return board_.GetLength(0); } }
        public FieldState FieldAt(int x, int y)
        {
            return board_[x, y];
        }
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
                if (board_[x, y] != FieldState.Free || !HasFreeNeighbour(x, y))
                    return;

                board_[x, y] = (FieldState)currentPlayer_;
                BoardCnahged?.Invoke(this, board_);
                lastClickPos = new(x, y);
            }
            else // (lsatClickPos != null)
            {
                if (board_[x, y] != FieldState.Free || !HasCommonNeighbour(x, y))
                    return;

                board_[x, y] = (FieldState)currentPlayer_;
                BoardCnahged?.Invoke(this, board_);

                captureSuroundedFields();

                CalculateScores();
                BoardCnahged?.Invoke(this, board_);

                lastClickPos = null;
                currentPlayer_ = (Player)((int)currentPlayer_ % 2 + 1);
                CurrentPlayerCnahged?.Invoke(this, currentPlayer_);

                if (IsGameOver())
                {
                    Player? winer =
                        scores.Red == scores.Blue
                            ? null
                            : scores.Red > scores.Blue
                                ? Player.Red
                                : Player.Blue;
                    GameFinished?.Invoke(this, new Tuple<Player?, Scores>(winer, scores));
                }
            }
        }

        public async Task SaveAsync(String filename)
        {

        }

        public async Task LoadAsync(String filename)
        {

        }

        // private:
        private void initGame(GameSize gameSize)
        {
            currentPlayer_ = Player.Blue;
            CurrentPlayerCnahged?.Invoke(this, currentPlayer_);

            scores.Blue = scores.Red = 0;
            ScoresCnahged?.Invoke(this, scores);

            int size = (int)gameSize;
            board_ = new FieldState[size, size];
            BoardCnahged?.Invoke(this, board_);

            lastClickPos = null;
        }

        private bool HasCommonNeighbour(int x, int y)
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
                        Player? capturer = AmISurrounded(j, i, checkedFields);
                        if (capturer != null)
                            board_[j, i] = (FieldState)(int)capturer.Value + 2;
                    }
                    catch (IndexOutOfRangeException) { }
                }
            }
        }

        private Player? AmISurrounded(int x, int y, bool[,] checkedFields)
        {
            RecursionStarted?.Invoke(this, new Tuple<int, int>(x, y));

            checkedFields[x, y] = true;
            Player? capturer = null;

            Player? left = null;
            try
            {
                if ((int)board_[x - 1, y] == (int)currentPlayer_)
                    left = (Player)board_[x - 1, y];
                else if (!checkedFields[x - 1, y])
                    left = AmISurrounded(x - 1, y, checkedFields);
            }
            catch (IndexOutOfRangeException e)
            {
                RecursionFinished?.Invoke(this, new Tuple<int, int>(x, y));
                throw e;
            }


            Player? right = null;
            try
            {
                if ((int)board_[x + 1, y] == (int)currentPlayer_)
                    right = (Player)board_[x + 1, y];
                else if (!checkedFields[x + 1, y])
                    right = AmISurrounded(x + 1, y, checkedFields);
            }
            catch (IndexOutOfRangeException e)
            {
                RecursionFinished?.Invoke(this, new Tuple<int, int>(x, y));
                throw e;
            }

            Player? top = null;
            try
            {
                if ((int)board_[x, y - 1] == (int)currentPlayer_)
                    top = (Player)board_[x, y - 1];
                else if (!checkedFields[x, y - 1])
                    top = AmISurrounded(x, y - 1, checkedFields);
            }
            catch (IndexOutOfRangeException e)
            {
                RecursionFinished?.Invoke(this, new Tuple<int, int>(x, y));
                throw e;
            }

            Player? bot = null;
            try
            {
                if ((int)board_[x, y + 1] == (int)currentPlayer_)
                    bot = (Player)board_[x, y + 1];
                else if (!checkedFields[x, y + 1])
                    bot = AmISurrounded(x, y + 1, checkedFields);
            }
            catch (IndexOutOfRangeException e)
            {
                RecursionFinished?.Invoke(this, new Tuple<int, int>(x, y));
                throw e;
            }

            if (left == null && right == null && top == null && bot == null)
            {
                RecursionFinished?.Invoke(this, new Tuple<int, int>(x, y));
                return null;
            }

            capturer = left ?? right ?? top ?? bot;

            foreach (var res in new List<Player?> { left, right, top, bot })
            {
                if (res != null && res != capturer)
                    capturer = null;
            }

            RecursionFinished?.Invoke(this, new Tuple<int, int>(x, y));
            return capturer;
        }

        bool HasFreeNeighbour(int x, int y)
        {
            bool result = false;

            try { result |= board_[x - 1, y] == FieldState.Free; }
            catch (IndexOutOfRangeException) { result |= false; }

            try { result |= board_[x + 1, y] == FieldState.Free; }
            catch (IndexOutOfRangeException) { result |= false; }

            try { result |= board_[x, y - 1] == FieldState.Free; }
            catch (IndexOutOfRangeException) { result |= false; }

            try { result |= board_[x, y + 1] == FieldState.Free; }
            catch (IndexOutOfRangeException) { result |= false; }

            return result;
        }

        private void CalculateScores()
        {
            scores.Red = scores.Blue = 0;
            for (int i = 0; i < BoardSize; ++i)
            {
                for (int j = 0; j < BoardSize; ++j)
                {
                    switch (board_[j, i])
                    {
                        case FieldState.RedPlaced:
                        case FieldState.RedCaptured:
                            ++scores.Red;
                            break;
                        case FieldState.BluePlaced:
                        case FieldState.BlueCaptured:
                            ++scores.Blue;
                            break;
                    }
                }
            }

            ScoresCnahged?.Invoke(this, scores);
        }

        private bool IsGameOver()
        {
            for (int i = 0; i < BoardSize; ++i)
            {
                for (int j = 0; j < BoardSize; ++j)
                {
                    if (board_[j, i] == FieldState.Free && HasFreeNeighbour(j, i))
                        return false;
                }
            }
            return true;
        }

        IDataAccess dataAccess_;

        Player currentPlayer_;
        Scores scores;
        FieldState[,] board_;

        Pos? lastClickPos = null;
    }
}
