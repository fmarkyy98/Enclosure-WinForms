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

            smallBtn.Click += (_, _) => { NewGame(GameSize.Small); };
            mediumBtn.Click += (_, _) => { NewGame(GameSize.Medium); };
            largeBtn.Click += (_, _) => { NewGame(GameSize.Large); };

            gameModel_.BoardCnahged += (_, board) =>
            {
                int n = model.BoardSize;
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        switch (board[j, i])
                        {
                            case FieldState.Free:
                                tableLayoutPanel1.Controls[i * n + j].BackColor = Color.Gray;
                                break;
                            case FieldState.RedPlaced:
                                tableLayoutPanel1.Controls[i * n + j].BackColor = Color.Red;
                                break;
                            case FieldState.BluePlaced:
                                tableLayoutPanel1.Controls[i * n + j].BackColor = Color.Blue;
                                break;
                            case FieldState.RedCaptured:
                                tableLayoutPanel1.Controls[i * n + j].BackColor = Color.Pink;
                                break;
                            case FieldState.BlueCaptured:
                                tableLayoutPanel1.Controls[i * n + j].BackColor = Color.LightBlue;
                                break;
                        }
                    }
                }
            };
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

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            }
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            }

            int n = gameModel_.BoardSize;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Label l = new();
                    l.BackColor = Color.Gray;
                    l.Text = $"({j};{i})";
                    l.AutoSize = false;
                    l.Dock = DockStyle.Fill;
                    tableLayoutPanel1.Controls.Add(l);

                    l.Click += (sender, _) =>
                    {
                        int x = tableLayoutPanel1.Controls.IndexOf((Label)sender) % n;
                        int y = tableLayoutPanel1.Controls.IndexOf((Label)sender) / n;
                        gameModel_.ClickedAt(x, y);
                    };
                }
            }

            gameModel_.NewGame(gameSize);
        }

        private IEnclosureModel gameModel_;

    }
}