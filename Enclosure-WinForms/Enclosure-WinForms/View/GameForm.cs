using Enclosure_WinForms.Model;

namespace Enclosure_WinForms
{
    public partial class GameForm : Form
    {
        public GameForm(IEnclosureModel model)
        {
            InitializeComponent();

            gameModel_ = model;

            Load += OnLoad;
            smallBtn.Click += (object? _, EventArgs _) => { gameModel_.NewGame(GameSize.Small); };
            mediumBtn.Click += (object? _, EventArgs _) => { gameModel_.NewGame(GameSize.Medium); };
            largeBtn.Click += (object? _, EventArgs _) => { gameModel_.NewGame(GameSize.Large); };


        }

        private void OnLoad(object? sender, EventArgs e)
        {
            gameModel_.NewGame();
        }

        private IEnclosureModel gameModel_;
    }
}