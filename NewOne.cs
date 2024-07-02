// using System;
// using System.Drawing;
// using System.Linq;
// using System.Windows.Forms;

// namespace Game2048
// {
//     public partial class Form1 : Form
//     {
//         private int[,] board = new int[400, 400];
//         private Label[,] labels = new Label[4, 4];
//         private Random rand = new Random();

//         public Form1()
//         {
//             InitializeComponent();
//             InitializeGameBoard();
//             StartGame();
//         }

//         private void InitializeComponent()
//         {
//             this.ClientSize = new Size(400, 400);
//             this.Name = "Form1";
//             this.Text = "2048 Game";
//             this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
//         }

//         private void InitializeGameBoard()
//         {
//             for (int i = 0; i < 4; i++)
//             {
//                 for (int j = 0; j < 4; j++)
//                 {
//                     labels[i, j] = new Label
//                     {
//                         Size = new Size(100, 100),
//                         Location = new Point(100 * j, 100 * i),
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
        
//         // [STAThread]
//         // public static void Main()
//         // {
//         //     Application.Run(new Form1());
//         // }

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

//         private void Form1_KeyDown(object sender, KeyEventArgs e)
//         {
//             bool moved = false;
//             switch (e.KeyCode)
//             {
//                 case Keys.Up: moved = MoveUp(); break;
//                 case Keys.Down: moved = MoveDown(); break;
//                 case Keys.Left: moved = MoveLeft(); break;
//                 case Keys.Right: moved = MoveRight(); break;
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

//         private bool MoveUp()
//         {
//             bool moved = false;
//             for (int col = 0; col < 4; col++)
//             {
//                 int[] column = new int[4];
//                 for (int row = 0; row < 4; row++) column[row] = board[row, col];
//                 moved |= MoveAndMerge(column);
//                 for (int row = 0; row < 4; row++) board[row, col] = column[row];
//             }
//             return moved;
//         }

//         private bool MoveDown()
//         {
//             bool moved = false;
//             for (int col = 0; col < 4; col++)
//             {
//                 int[] column = new int[4];
//                 for (int row = 0; row < 4; row++) column[row] = board[3 - row, col];
//                 moved |= MoveAndMerge(column);
//                 for (int row = 0; row < 4; row++) board[3 - row, col] = column[row];
//             }
//             return moved;
//         }

//         private bool MoveLeft()
//         {
//             bool moved = false;
//             for (int row = 0; row < 4; row++)
//             {
//                 int[] line = new int[4];
//                 for (int col = 0; col < 4; col++) line[col] = board[row, col];
//                 moved |= MoveAndMerge(line);
//                 for (int col = 0; col < 4; col++) board[row, col] = line[col];
//             }
//             return moved;
//         }

//         private bool MoveRight()
//         {
//             bool moved = false;
//             for (int row = 0; row < 4; row++)
//             {
//                 int[] line = new int[4];
//                 for (int col = 0; col < 4; col++) line[col] = board[row, 3 - col];
//                 moved |= MoveAndMerge(line);
//                 for (int col = 0; col < 4; col++) board[row, 3 - col] = line[col];
//             }
//             return moved;
//         }

//         private bool MoveAndMerge(int[] line)
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
//                 if (line[i] != newLine[i])
//                 {
//                     moved = true;
//                 }
//                 line[i] = newLine[i];
//             }

//             return moved;
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
//     }
// }
