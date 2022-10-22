using Enclosure_WinForms.Model;
using System.Reflection;
using System.Windows.Forms;

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

            model.BoardCnahged += (_, board) =>
            {
                int n = model.BoardSize;
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        switch (board[j, i])
                        {
                            case FieldState.Free:
                                tableLayoutPanel1.Controls[i * n + j].BackColor = Color.LightGray;
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

            model.ScoresCnahged += (_, score) =>
            {
                redScoreLabel.Text = score.Red.ToString();
                blueScoreLabel.Text = score.Blue.ToString();
            };

            model.CurrentPlayerCnahged += (_, player) =>
            {
                playerLabel.Text = player == Player.Red ? "Red" : "Blue";
            };

            model.GameFinished += (_, tup) =>
            {
                Player? winer = tup.Item1;
                Scores sc = tup.Item2;

                String result =
                    winer == null
                        ? "Draw"
                        : winer.Value == Player.Red
                            ? "Red wins"
                            : "Blue wins";

                DialogResult dr =
                    MessageBox.Show($"{result},\n" +
                                        $"Red score: {sc.Red},\n" +
                                        $"Blue score: {sc.Blue},\n" +
                                        "Wanna start a new game?",
                                    "Game Finished",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                    NewGame((GameSize)model.BoardSize);
            };

            showRecurion.Click += (_, _) =>
            {
                if (showRecurion.Checked)
                {
                    model.RecursionStarted -= onRecursionStarted;
                    model.RecursionFinished -= onRecursionFinished;
                }
                else
                {
                    model.RecursionStarted += onRecursionStarted;
                    model.RecursionFinished += onRecursionFinished;
                }
                showRecurion.Checked = !showRecurion.Checked;
            };

            saveBtn.Click += async (_, _) =>
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    await model.SaveAsync(saveFileDialog1.FileName);
                }
            };

            loadBtn.Click += async (_, _) =>
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    await model.LoadAsync(openFileDialog1.FileName);
                    InitBoard((GameSize)model.BoardSize);
                }
            };

        }

        private void OnLoad(object? sender, EventArgs e)
        {
            NewGame();
        }

        private void InitBoard(GameSize gameSize = GameSize.Small)
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

            int n = (int)gameSize;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Label l = new();
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
        }

        private void NewGame(GameSize gameSize = GameSize.Small)
        {
            InitBoard(gameSize);

            gameModel_.NewGame(gameSize);
        }

        private void onRecursionStarted(object? _, Tuple<int, int> tup)
        {
            tableLayoutPanel1.Controls[tup.Item2 * gameModel_.BoardSize + tup.Item1].BackColor
                    = Color.Yellow;
            Thread.Sleep(20);
            Application.DoEvents();
        }

        private void onRecursionFinished(object? _, Tuple<int, int> tup)
        {
            int n = gameModel_.BoardSize;
            switch (gameModel_.FieldAt(tup.Item1, tup.Item2))
            {
                case FieldState.Free:
                    tableLayoutPanel1.Controls[tup.Item2 * n + tup.Item1].BackColor = Color.LightGray;
                    break;
                case FieldState.RedPlaced:
                    tableLayoutPanel1.Controls[tup.Item2 * n + tup.Item1].BackColor = Color.Red;
                    break;
                case FieldState.BluePlaced:
                    tableLayoutPanel1.Controls[tup.Item2 * n + tup.Item1].BackColor = Color.Blue;
                    break;
                case FieldState.RedCaptured:
                    tableLayoutPanel1.Controls[tup.Item2 * n + tup.Item1].BackColor = Color.Pink;
                    break;
                case FieldState.BlueCaptured:
                    tableLayoutPanel1.Controls[tup.Item2 * n + tup.Item1].BackColor = Color.LightBlue;
                    break;
            }
            Thread.Sleep(20);
            Application.DoEvents();
        }

        private IEnclosureModel gameModel_;
    }
}