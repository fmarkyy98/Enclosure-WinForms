using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Enclosure_WinForms.Persistance;

namespace Enclosure_WinForms.Model
{
    
    enum FieldState { Free, RedPlaced, BluePlaced, RedCaptured, BlueCaptured };

    struct Scores
    {
        public int Red;
        public int Blue;
    }

    internal class EnclosureModel : IEnclosureModel
    {
        public EnclosureModel(IDataAccess dataAccess)
        {
            initGame(GameSize.Small);
            dataAccess_ = dataAccess;
        }

        public void NewGame(GameSize gameSize)
        {
            initGame(gameSize); 
        }

        public event EventHandler<Player> CurrentPlayerCnahged;
        public event EventHandler<Scores> ScoresCnahged;
        public event EventHandler<FieldState[,]> BoardCnahged;

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

        IDataAccess dataAccess_;

        Player currentPlayer_;
        Scores scores;
        FieldState[,] board_; 
    }
}
