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

            smallBtn.Click  += (_,_) => { NewGame(GameSize.Small); };
            mediumBtn.Click += (_,_) => { NewGame(GameSize.Medium); };
            largeBtn.Click  += (_,_) => { NewGame(GameSize.Large); };
        }

        private void OnLoad(object? sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame(GameSize gameSize = GameSize.Small)
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = (int)gameSize;
            tableLayoutPanel1.ColumnCount = (int)gameSize;

            int n = gameModel_.BoardSize;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Label l = new();
                    l.BackColor = Color.Gray;
                    l.Text = $"({j};{i})";
                    tableLayoutPanel1.Controls.Add(l);

                    l.Click += (_,_) => { gameModel_.ClickedAt(j, i); };
                }
            }

            gameModel_.NewGame(gameSize);
        }

        private IEnclosureModel gameModel_;
    }
}