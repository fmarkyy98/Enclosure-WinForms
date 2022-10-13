using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enclosure_WinForms.Model
{
    enum Player { Red, Blue };
    public enum GameSize { Small = 6, Medium = 8, Large = 10 };

    public interface IEnclosureModel
    {
        public void NewGame(GameSize gameSize = GameSize.Small);
    }
}
