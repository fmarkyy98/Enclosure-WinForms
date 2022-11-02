using Enclosure_WinForms.Model;
using Enclosure_WinForms.Persistance;

namespace EnclosureModel_UnitTest
{
    class MockDataAccess : IDataAccess
    {
        public Task SaveAsync(string fileName, List<int> data)
        {
            data_ = data;
            return Task.CompletedTask;
        }

        public Task<List<int>> LoadAsync(string fileName)
        {
            return new Task<List<int>>(() => data_);
        }

        private List<int> data_;
    }

    [TestClass]
    public class UnitTest1
    {
        [DataRow(GameSize.Small)]
        [DataRow(GameSize.Medium)]
        [DataRow(GameSize.Large)]

        [TestMethod]
        public void NewGameTest(GameSize size)
        {
            model = new EnclosureModel(new MockDataAccess());

            model.NewGame(size);

            Assert.AreEqual(model.currentPlayer_, Player.Blue);
            Assert.AreEqual(model.scores.Red, 0);
            Assert.AreEqual(model.scores.Blue, 0);
            for (int i = 0; i < model.BoardSize; ++i)
                for (int j = 0; j < model.BoardSize; ++j)
                    Assert.AreEqual(model.board_[j, i], FieldState.Free);
        }

        [TestMethod]
        public void FieldAtTest()
        {
            model = new EnclosureModel(new MockDataAccess());
            // Blue
            model.ClickedAt(1, 0);
            model.ClickedAt(2, 0);
            // Red
            model.ClickedAt(1, 1);
            model.ClickedAt(2, 1);
            // Blue
            model.ClickedAt(3, 1);
            model.ClickedAt(3, 2);
            // Red
            model.ClickedAt(1, 2);
            model.ClickedAt(2, 2);
            // Blue
            model.ClickedAt(1, 3);
            model.ClickedAt(2, 3);
            // Red
            model.ClickedAt(5, 2);
            model.ClickedAt(5, 3);
            // Blue
            model.ClickedAt(0, 1);
            model.ClickedAt(0, 2);

            Assert.AreEqual(model.currentPlayer_, Player.Red);
            Assert.AreEqual(model.scores.Red, 2);
            Assert.AreEqual(model.scores.Blue, 12);
            for (int i = 0; i < model.BoardSize; i++)
                for (int j = 0; j < model.BoardSize; j++)
                    Assert.AreEqual(model.board_[j, i], model.FieldAt(j, i));
        }

        [DataRow(0, 0)]
        [DataRow(0, 5)]
        [DataRow(5, 0)]
        [DataRow(5, 5)]
        [DataRow(1, 1)]
        [DataRow(1, 4)]
        [DataRow(4, 1)]
        [DataRow(4, 4)]

        [TestMethod]
        public void ClickedAtOnceTest(int x, int y)
        {
            model = new EnclosureModel(new MockDataAccess());

            model.ClickedAt(x, y);

            Assert.AreEqual(model.currentPlayer_, Player.Blue);
            Assert.AreEqual(model.scores.Red, 0);
            Assert.AreEqual(model.scores.Blue, 0);
            Assert.AreEqual(model.board_[x, y], FieldState.BluePlaced);
        }

        [DataRow(0, 0, 0, 1)]
        [DataRow(0, 0, 1, 0)]

        [DataRow(0, 5, 0, 4)]
        [DataRow(0, 5, 1, 5)]

        [DataRow(5, 0, 5, 1)]
        [DataRow(5, 0, 4, 0)]

        [DataRow(5, 5, 5, 4)]
        [DataRow(5, 5, 4, 5)]

        [DataRow(1, 1, 1, 0)]
        [DataRow(1, 1, 0, 1)]
        [DataRow(1, 1, 1, 2)]
        [DataRow(1, 1, 2, 1)]

        [DataRow(1, 4, 1, 3)]
        [DataRow(1, 4, 2, 4)]
        [DataRow(1, 4, 1, 5)]
        [DataRow(1, 4, 0, 4)]

        [DataRow(4, 1, 4, 2)]
        [DataRow(4, 1, 3, 1)]
        [DataRow(4, 1, 4, 0)]
        [DataRow(4, 1, 5, 1)]

        [DataRow(4, 4, 4, 3)]
        [DataRow(4, 4, 3, 4)]
        [DataRow(4, 4, 4, 5)]
        [DataRow(4, 4, 5, 4)]

        [TestMethod]
        public void Clicked2CorrectlyTest(int x1, int y1, int x2, int y2)
        {
            model = new EnclosureModel(new MockDataAccess());

            model.ClickedAt(x1, y1);
            model.ClickedAt(x2, y2);

            Assert.AreEqual(model.currentPlayer_, Player.Red);
            Assert.AreEqual(model.scores.Red, 0);
            Assert.AreEqual(model.scores.Blue, 2);
            Assert.AreEqual(model.board_[x1, y1], FieldState.BluePlaced);
            Assert.AreEqual(model.board_[x2, y2], FieldState.BluePlaced);
        }

        [DataRow(0, 0, 1, 1)]

        [DataRow(0, 5, 1, 4)]

        [DataRow(5, 0, 4, 1)]

        [DataRow(5, 5, 4, 4)]

        [DataRow(1, 1, 0, 0)]
        [DataRow(1, 1, 0, 2)]
        [DataRow(1, 1, 2, 0)]
        [DataRow(1, 1, 2, 2)]

        [DataRow(1, 4, 0, 3)]
        [DataRow(1, 4, 0, 5)]
        [DataRow(1, 4, 2, 3)]
        [DataRow(1, 4, 2, 5)]

        [DataRow(4, 1, 3, 0)]
        [DataRow(4, 1, 3, 2)]
        [DataRow(4, 1, 5, 0)]
        [DataRow(4, 1, 5, 2)]

        [DataRow(4, 4, 3, 3)]
        [DataRow(4, 4, 3, 5)]
        [DataRow(4, 4, 5, 3)]
        [DataRow(4, 4, 5, 5)]

        [DataRow(1, 2, 3, 4)]
        [DataRow(2, 4, 1, 1)]
        [DataRow(3, 2, 4, 1)]
        [DataRow(1, 3, 5, 4)]
        [DataRow(4, 1, 2, 3)]

        [TestMethod]
        public void Clicked2IncorrectlyTest(int x1, int y1, int x2, int y2)
        {
            model = new EnclosureModel(new MockDataAccess());

            model.ClickedAt(x1, y1);
            model.ClickedAt(x2, y2);

            Assert.AreEqual(model.currentPlayer_, Player.Blue);
            Assert.AreEqual(model.scores.Red, 0);
            Assert.AreEqual(model.scores.Blue, 0);
            Assert.AreEqual(model.board_[x1, y1], FieldState.BluePlaced);
            Assert.AreEqual(model.board_[x2, y2], FieldState.Free);
        }

        [TestMethod]
        public void EnclosingTest()
        {
            model = new EnclosureModel(new MockDataAccess());
            // Blue
            model.ClickedAt(1, 0);
            model.ClickedAt(2, 0);
            // Red
            model.ClickedAt(1, 1);
            model.ClickedAt(2, 1);
            // Blue
            model.ClickedAt(3, 1);
            model.ClickedAt(3, 2);
            // Red
            model.ClickedAt(1, 2);
            model.ClickedAt(2, 2);
            // Blue
            model.ClickedAt(1, 3);
            model.ClickedAt(2, 3);
            // Red
            model.ClickedAt(5, 2);
            model.ClickedAt(5, 3);
            // Blue
            model.ClickedAt(0, 1);
            model.ClickedAt(0, 2);

            Assert.AreEqual(model.currentPlayer_, Player.Red);
            Assert.AreEqual(model.scores.Red, 2);
            Assert.AreEqual(model.scores.Blue, 12);
        }

        [TestMethod]
        public void SaveLoadTest()
        {
            model = new EnclosureModel(new MockDataAccess());
            // Blue
            model.ClickedAt(1, 0);
            model.ClickedAt(2, 0);
            // Red
            model.ClickedAt(1, 1);
            model.ClickedAt(2, 1);
            // Blue
            model.ClickedAt(3, 1);
            model.ClickedAt(3, 2);
            // Red
            model.ClickedAt(1, 2);
            model.ClickedAt(2, 2);
            // Blue
            model.ClickedAt(1, 3);
            model.ClickedAt(2, 3);
            // Red
            model.ClickedAt(5, 2);
            model.ClickedAt(5, 3);
            // Blue
            model.ClickedAt(0, 1);
            model.ClickedAt(0, 2);

            var boardBeforeSave = model.board_;
            model.SaveAsync("");
            model.LoadAsync("");

            Assert.AreEqual(model.board_, boardBeforeSave);
            Assert.AreEqual(model.currentPlayer_, Player.Red);
            Assert.AreEqual(model.scores.Red, 2);
            Assert.AreEqual(model.scores.Blue, 12);
        }

        [TestMethod]
        public void FullRefreshTest()
        {
            model = new EnclosureModel(new MockDataAccess());
            bool l1 = false, l2 = false, l3 = false;

            model.BoardCnahged += (_, _) => { l1 = true; };
            model.ScoresCnahged += (_, _) => { l2 = true; };
            model.CurrentPlayerCnahged += (_, _) => { l3 = true; };

            model.FullRefresh();

            Assert.IsTrue(l1 && l2 && l3);
        }

        [TestMethod]
        public void GameOverTest()
        {
            model = new EnclosureModel(new MockDataAccess());
            bool l1 = false;

            model.GameFinished += (_, _) => { l1 = true; };

            for (int i = 0; i < model.BoardSize; i++)
                for (int j = 0; j < model.BoardSize; j++)
                    model.ClickedAt(j, i);

            Assert.IsTrue(l1);
        }

        private EnclosureModel model;
    }
}