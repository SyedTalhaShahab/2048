// using System;
// using System.Collections.Generic;
// using System.Drawing;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Windows.Forms;

// namespace Game2048
// {
//     public partial class Former2 : Form
//     {
//         private int[,] board = new int[4, 4];
//         private Label[,] labels = new Label[4, 4];
//         private Random rand = new Random();
//         private const int tileSize = 100;
//         private const int gridSize = 400;
//         private const int animationSpeed = 10;
//         private Timer animationTimer;
//         private List<MoveAnimation> animations;

//         public Former2()
//         {
//             InitializeComponent();
//             InitializeGameBoard();
//             StartGame();
//         }

//         private void InitializeComponent()
//         {
//             this.ClientSize = new Size(gridSize, gridSize);
//             this.Name = "Form1";
//             this.Text = "2048 Game";
//             this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);

//             animationTimer = new Timer();
//             animationTimer.Interval = 20;
//             // animationTimer.Tick += AnimationTimer_Tick;

//             animations = new List<MoveAnimation>();
//         }

//         private void InitializeGameBoard()
//         {
//             for (int i = 0; i < 4; i++)
//             {
//                 for (int j = 0; j < 4; j++)
//                 {
//                     labels[i, j] = new Label
//                     {
//                         Size = new Size(tileSize, tileSize),
//                         Location = new Point(tileSize * j, tileSize * i),
//                         TextAlign = ContentAlignment.MiddleCenter,
//                         Font = new Font("Arial", 24, FontStyle.Bold),
//                         BorderStyle = BorderStyle.FixedSingle
//                     };
//                     this.Controls.Add(labels[i, j]);
//                 }
//             }
//         }

//         private void StartGame()
//         {
//             Array.Clear(board, 0, board.Length);
//             AddRandomTile();
//             AddRandomTile();
//             UpdateUI();
//         }

//         private void AddRandomTile()
//         {
//             var emptyTiles = Enumerable.Range(0, 16).Where(i => board[i / 4, i % 4] == 0).ToList();
//             if (emptyTiles.Count > 0)
//             {
//                 int index = emptyTiles[rand.Next(emptyTiles.Count)];
//                 board[index / 4, index % 4] = rand.Next(10) == 0 ? 4 : 2;
//             }
//         }

//         private void UpdateUI()
//         {
//             for (int i = 0; i < 4; i++)
//             {
//                 for (int j = 0; j < 4; j++)
//                 {
//                     labels[i, j].Text = board[i, j] == 0 ? "" : board[i, j].ToString();
//                     labels[i, j].BackColor = GetTileColor(board[i, j]);
//                 }
//             }
//         }

//         private Color GetTileColor(int value)
//         {
//             switch (value)
//             {
//                 case 0: return Color.LightGray;
//                 case 2: return Color.Beige;
//                 case 4: return Color.Bisque;
//                 case 8: return Color.Orange;
//                 case 16: return Color.OrangeRed;
//                 case 32: return Color.Tomato;
//                 case 64: return Color.Red;
//                 case 128: return Color.Gold;
//                 case 256: return Color.Yellow;
//                 case 512: return Color.GreenYellow;
//                 case 1024: return Color.LightGreen;
//                 case 2048: return Color.Lime;
//                 default: return Color.DarkSlateGray;
//             }
//         }

//         private async void Form1_KeyDown(object sender, KeyEventArgs e)
//         {
//             bool moved = false;
//             switch (e.KeyCode)
//             {
//                 case Keys.Up: moved = await MoveUp(); break;
//                 case Keys.Down: moved = await MoveDown(); break;
//                 case Keys.Left: moved = await MoveLeft(); break;
//                 case Keys.Right: moved = await MoveRight(); break;
//             }

//             if (moved)
//             {
//                 AddRandomTile();
//                 UpdateUI();
//                 if (CheckGameOver())
//                 {
//                     MessageBox.Show("Game Over!");
//                     StartGame();
//                 }
//             }
//         }

//         private async Task<bool> MoveUp()
//         {
//             bool moved = false;
//             for (int col = 0; col < 4; col++)
//             {
//                 int[] column = new int[4];
//                 for (int row = 0; row < 4; row++) column[row] = board[row, col];
//                 moved |= await MoveAndMerge(column, (row, val) => (row, col));
//                 for (int row = 0; row < 4; row++) board[row, col] = column[row];
//             }
//             return moved;
//         }

//         private async Task<bool> MoveDown()
//         {
//             bool moved = false;
//             for (int col = 0; col < 4; col++)
//             {
//                 int[] column = new int[4];
//                 for (int row = 0; row < 4; row++) column[row] = board[3 - row, col];
//                 moved |= await MoveAndMerge(column, (row, val) => (3 - row, col));
//                 for (int row = 0; row < 4; row++) board[3 - row, col] = column[row];
//             }
//             return moved;
//         }

//         private async Task<bool> MoveLeft()
//         {
//             bool moved = false;
//             for (int row = 0; row < 4; row++)
//             {
//                 int[] line = new int[4];
//                 for (int col = 0; col < 4; col++) line[col] = board[row, col];
//                 moved |= await MoveAndMerge(line, (col, val) => (row, col));
//                 for (int col = 0; col < 4; col++) board[row, col] = line[col];
//             }
//             return moved;
//         }

//         private async Task<bool> MoveRight()
//         {
//             bool moved = false;
//             for (int row = 0; row < 4; row++)
//             {
//                 int[] line = new int[4];
//                 for (int col = 0; col < 4; col++) line[col] = board[row, 3 - col];
//                 moved |= await MoveAndMerge(line, (col, val) => (row, 3 - col));
//                 for (int col = 0; col < 4; col++) board[row, 3 - col] = line[col];
//             }
//             return moved;
//         }

//         private async Task<bool> MoveAndMerge(int[] line, Func<int, int, (int, int)> getPosition)
//         {
//             bool moved = false;
//             int[] newLine = new int[4];
//             int index = 0;

//             for (int i = 0; i < 4; i++)
//             {
//                 if (line[i] != 0)
//                 {
//                     if (index > 0 && newLine[index - 1] == line[i])
//                     {
//                         newLine[index - 1] *= 2;
//                         moved = true;
//                     }
//                     else
//                     {
//                         newLine[index++] = line[i];
//                     }
//                 }
//             }

//             for (int i = 0; i < 4; i++)
//             {
//                 var (row, col) = getPosition(i, line[i]);
//                 if (line[i] != newLine[i])
//                 {
//                     moved = true;
//                     var targetPos = getPosition(i, newLine[i]);
//                     await AnimateTile(row, col, targetPos.Item1, targetPos.Item2);
//                 }
//                 line[i] = newLine[i];
//             }

//             return moved;
//         }

//         private async Task AnimateTile(int fromRow, int fromCol, int toRow, int toCol)
//         {
//             var label = labels[fromRow, fromCol];
//             var startPos = label.Location;
//             var endPos = new Point(tileSize * toCol, tileSize * toRow);
//             int steps = 20;

//             for (int i = 1; i <= steps; i++)
//             {
//                 label.Location = new Point(
//                     startPos.X + (endPos.X - startPos.X) * i / steps,
//                     startPos.Y + (endPos.Y - startPos.Y) * i / steps);
//                 await Task.Delay(animationSpeed);
//             }

//             label.Location = endPos;
//         }

//         private bool CheckGameOver()
//         {
//             for (int i = 0; i < 4; i++)
//             {
//                 for (int j = 0; j < 4; j++)
//                 {
//                     if (board[i, j] == 0) return false;
//                     if (i < 3 && board[i, j] == board[i + 1, j]) return false;
//                     if (j < 3 && board[i, j] == board[i, j + 1]) return false;
//                 }
//             }
//             return true;
//         }
//         // [STAThread]
//         // public static void Main()
//         // {
//         //     Application.Run(new Former2());
//         // }
//     }

//     public class MoveAnimation
//     {
//         public Label Label { get; set; }
//         public Point StartPosition { get; set; }
//         public Point EndPosition { get; set; }
//         public int Step { get; set; }
//         public int TotalSteps { get; set; }
//     }
// }
