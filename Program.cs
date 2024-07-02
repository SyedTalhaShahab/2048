﻿// tried working the animations but no luck
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Drawing;
// using System.Linq;
// using System.Threading;
// using System.Windows.Forms;

// namespace TheGame2048
// {
//     public class TheGame2048 : Form
//     {
//         private const int GRID_SIZE = 4;
//         private const int TILE_SIZE = 150;
//         private const int SCREEN_SIZE = GRID_SIZE * TILE_SIZE;
//         private int[,] board;
//         private int score = 0;
//         private Random rand = new Random();
//         private LogForm logForm;
//         private ArrayList history = new ArrayList();
//         private Form gameOver = new Form();

//         private bool ignoreKeyEvents = false;
//         private Graphics g;

//         private char Direction = ' ';
//         private List<int> box_number_before = new List<int>();
//         private List<int> box_number_after = new List<int>();

//         //  box_number_before, row_before, row_after, col_after, col_before;
//         public TheGame2048()
//         {
//             board = new int[GRID_SIZE, GRID_SIZE];

//             this.Text = "2048";
//             this.ClientSize = new Size(SCREEN_SIZE, SCREEN_SIZE + 50);
//             this.BackColor = Color.FromArgb(187, 173, 160);
//             this.StartPosition = FormStartPosition.CenterScreen; // Center the form
//             this.FormBorderStyle = FormBorderStyle.FixedDialog; // Prevent resizing
//             this.MaximizeBox = false; // Disable the maximize button

//             this.KeyDown += new KeyEventHandler(OnKeyDown);
//             StartGame();

//             logForm = new LogForm();
//             logForm.Show();


//             // Create gameOver form
//             gameOver.Text = "Game Over";
//             gameOver.ClientSize = new Size(SCREEN_SIZE, SCREEN_SIZE + 50);
//             gameOver.BackColor = Color.FromArgb(187, 173, 160);
//             gameOver.StartPosition = FormStartPosition.CenterScreen; // Center the form
//             gameOver.FormBorderStyle = FormBorderStyle.FixedDialog; // Prevent resizing
//             gameOver.MaximizeBox = false; // Disable the maximize button
//         }

//         private void StartGame()
//         {
//             Invalidate();
//             score = 0;
//             board = new int[GRID_SIZE, GRID_SIZE];
//             AddTile();
//             AddTile();
//         }

//         private void AddTile()
//         {
//             if (!HasEmptyTile()) return;

//             int r, c;
//             do
//             {
//                 r = rand.Next(GRID_SIZE);
//                 c = rand.Next(GRID_SIZE);
//             } while (board[r, c] != 0);

//             board[r, c] = 2;
//         }

//         private bool HasEmptyTile()
//         {
//             foreach (var tile in board)
//             {
//                 if (tile == 0) return true;
//             }
//             return false;
//         }

//         private void CalculateSlideLeft()
//         {
//             Direction = 'L';
//             Graphics g = this.CreateGraphics();  // Create a Graphics object

//             for (int r = 0; r < GRID_SIZE; r++)
//             {
//                 int[] newRow = new int[GRID_SIZE];
//                 int index = 0;

//                 for (int c = 0; c < GRID_SIZE; c++)
//                 {
//                     if (board[r, c] != 0)
//                     {
//                         logForm.LogMessage("box number before:" + coordinate_TO_pos(r, c));
//                         box_number_before.Add(coordinate_TO_pos(r, c));
//                         if (index > 0 && newRow[index - 1] == board[r, c])
//                         {
//                             newRow[index - 1] *= 2;
//                             score += newRow[index - 1];
//                         }
//                         else
//                         {
//                             newRow[index] = board[r, c];
//                             index++;
//                         }
//                     }
//                 }

//                 for (int c = 0; c < GRID_SIZE; c++)
//                 {
//                     board[r, c] = newRow[c];
//                 }
//             }
//             for (int c = 0; c < GRID_SIZE; c++)
//             {
//                 for (int r = 0; r < GRID_SIZE; r++)
//                 {
//                     if (board[r, c] != 0)
//                     {
//                         logForm.LogMessage("box number after:" + coordinate_TO_pos(r, c));
//                         box_number_after.Add(coordinate_TO_pos(r, c));
//                     }
//                 }
//             }
//         }

//         private void CalculateSlideRight()
//         {
//             Direction = 'R';
//             Graphics g = this.CreateGraphics();  // Create a Graphics object

//             for (int r = 0; r < GRID_SIZE; r++)
//             {
//                 int[] newRow = new int[GRID_SIZE];
//                 int index = GRID_SIZE - 1;

//                 for (int c = GRID_SIZE - 1; c >= 0; c--)
//                 {
//                     if (board[r, c] != 0)
//                     {
//                         logForm.LogMessage("box number before:" + coordinate_TO_pos(r, c));
//                         box_number_before.Add(coordinate_TO_pos(r, c));
//                         if (index < GRID_SIZE - 1 && newRow[index + 1] == board[r, c])
//                         {
//                             newRow[index + 1] *= 2;
//                             score += newRow[index + 1];
//                         }
//                         else
//                         {
//                             newRow[index] = board[r, c];
//                             index--;
//                         }
//                     }
//                 }

//                 for (int c = 0; c < GRID_SIZE; c++)
//                 {
//                     board[r, c] = newRow[c];
//                 }
//             }
//             for (int c = 0; c < GRID_SIZE; c++)
//             {
//                 for (int r = 0; r < GRID_SIZE; r++)
//                 {
//                     if (board[r, c] != 0)
//                     {
//                         logForm.LogMessage("box number after:" + coordinate_TO_pos(r, c));
//                         box_number_after.Add(coordinate_TO_pos(r, c));
//                     }
//                 }
//             }
//         }

//         private void CalculateSlideUp()
//         {
//             Direction = 'U';
//             Graphics g = this.CreateGraphics();  // Create a Graphics object

//             for (int c = 0; c < GRID_SIZE; c++)
//             {
//                 int[] newCol = new int[GRID_SIZE];
//                 int index = 0;

//                 for (int r = 0; r < GRID_SIZE; r++)
//                 {
//                     if (board[r, c] != 0)
//                     {
//                         logForm.LogMessage("box number before:" + coordinate_TO_pos(r, c));
//                         box_number_before.Add(coordinate_TO_pos(r, c));
//                         if (index > 0 && newCol[index - 1] == board[r, c])
//                         {
//                             newCol[index - 1] *= 2;
//                             score += newCol[index - 1];
//                         }
//                         else
//                         {
//                             newCol[index] = board[r, c];
//                             index++;
//                         }
//                     }
//                 }

//                 for (int r = 0; r < GRID_SIZE; r++)
//                 {
//                     board[r, c] = newCol[r];
//                 }
//             }
//             for (int c = 0; c < GRID_SIZE; c++)
//             {
//                 for (int r = 0; r < GRID_SIZE; r++)
//                 {
//                     if (board[r, c] != 0)
//                     {
//                         logForm.LogMessage("box number after:" + coordinate_TO_pos(r, c));
//                         box_number_after.Add(coordinate_TO_pos(r, c));
//                     }
//                 }
//             }
//         }

//         private void CalculateSlideDown()
//         {
//             Stack<int> box_number_before1 = new Stack<int>();
//             Stack<int> box_number_after1 = new Stack<int>();

//             // List<int> box_number_before1 = new List<int>();
//             // List<int> box_number_after1 = new List<int>();

//             box_number_before1.Clear();
//             box_number_after1.Clear();

//             Direction = 'D';
//             Graphics g = this.CreateGraphics();  // Create a Graphics object

//             for (int c = 0; c < GRID_SIZE; c++)
//             {
//                 int[] newCol = new int[GRID_SIZE];
//                 int index = GRID_SIZE - 1;

//                 for (int r = GRID_SIZE - 1; r >= 0; r--)
//                 {
//                     if (board[r, c] != 0)
//                     {
//                         box_number_before1.Push(coordinate_TO_pos(r, c));
//                         if (index < GRID_SIZE - 1 && newCol[index + 1] == board[r, c])
//                         {
//                             newCol[index + 1] *= 2;
//                             score += newCol[index + 1];
//                         }
//                         else
//                         {
//                             newCol[index] = board[r, c];
//                             index--;
//                         }
//                     }
//                 }

//                 for (int r = 0; r < GRID_SIZE; r++)
//                 {
//                     board[r, c] = newCol[r];
//                 }

//             }

//             for (int i = 1; i <= 16; i++)
//             {
//                 if (board[getRow(i), getCol(i)] != 0)
//                 {
//                     box_number_after1.Push(coordinate_TO_pos(getRow(i), getCol(i)));
//                 }
//             }

//             logForm.LogMessage("box number before   :" + string.Join(", ", box_number_before1));
//             logForm.LogMessage("box number after    :" + string.Join(", ", box_number_after1));

//             logForm.LogMessage("    start tile pixel:");
//             foreach (var num in box_number_before1)
//             {
//                 logForm.LogMessage(string.Join(", ", getRow(num) * 150));
//             }

//             logForm.LogMessage("    stop tile pixel:");
//             foreach (var num in box_number_after1)
//             {
//                 logForm.LogMessage(string.Join(", ", getRow(num) * 150));
//             }


//             if (false)
//                 for (int counter = 0; counter < box_number_before.Count; counter++)
//                 {
//                     for (int pixels = getRow(box_number_before[counter]) * 150; pixels < getRow(box_number_after[counter]) * 150; pixels++)
//                     {
//                         Thread.Sleep(1);
//                     }
//                 }



//             if (false)
//                 for (int counter = 0; (counter < box_number_before.Count) || (counter < box_number_after.Count); counter++)
//                 {
//                     int i = 0;
//                     // logForm.LogMessage("start tile pixel:" + string.Join(", ", getRow(box_number_before[counter]) * 150));
//                     // logForm.LogMessage("stop tile pixel:" + string.Join(", ", getRow(box_number_after[counter]) * 150));

//                     if (false)
//                         for (int pixels = getRow(box_number_before[counter]) * 150; pixels < getRow(box_number_after[counter]) * 150; pixels++)
//                         {
//                             DrawTile(g, board[getRow(i), getCol(i)], getCol(i) * 150, pixels);
//                             DrawTile(g, 0, getCol(i) * 150, pixels - 150);
//                             Thread.Sleep(1);
//                         }
//                 }


//             logForm.LogMessage("method end");
//             Thread.Sleep(5000);
//         }

//         // Converting a 4x4 grid coordinate to a number
//         private int coordinate_TO_pos(int r, int c)
//         {
//             return ((r * 4) + c + 1);
//         }

//         // Converting a number to a 4x4 grid coordinate
//         private int getRow(int pos)
//         {
//             int convertedRow = (pos - 1) / 4;
//             return convertedRow;
//         }

//         // Converting a number to a 4x4 grid coordinate
//         private int getCol(int pos)
//         {
//             int convertedCol = (pos - 1) % 4;
//             return convertedCol;
//         }
//         protected override void OnPaint(PaintEventArgs e)
//         {
//             base.OnPaint(e);
//             g = e.Graphics;

//             for (int r = 0; r < GRID_SIZE; r++)
//             {
//                 for (int c = 0; c < GRID_SIZE; c++)
//                 {
//                     // Thread.Sleep(500);
//                     // if (board[r, c] != 0)
//                     // logForm.LogMessage("Word: " + board[r, c].ToString());
//                     // logForm.LogMessage("box number:" + findBoxNumber(r, c));
//                     // logForm.LogMessage("r: " + r);
//                     // logForm.LogMessage("c: " + c);
//                     // logForm.LogMessage("x axis start: " + column);
//                     // logForm.LogMessage("y axis start: " + row);
//                     // logForm.LogMessage("===============================================");
//                     DrawTile(g, board[r, c], c * TILE_SIZE, r * TILE_SIZE);
//                     if (false)
//                         switch (Direction)
//                         {
//                             case 'U':
//                                 // logForm.LogMessage("in up");
//                                 DrawTile(g, board[r, c], c * TILE_SIZE, r * TILE_SIZE);
//                                 break;
//                             case 'D':
//                                 // logForm.LogMessage("in down");
//                                 DrawTile(g, board[r, c], c * TILE_SIZE, r * TILE_SIZE);
//                                 break;
//                             case 'R':
//                                 // logForm.LogMessage("in right");
//                                 DrawTile(g, board[r, c], c * TILE_SIZE, r * TILE_SIZE);
//                                 break;
//                             case 'L':
//                                 // logForm.LogMessage("in left");
//                                 DrawTile(g, board[r, c], c * TILE_SIZE, r * TILE_SIZE);
//                                 break;
//                             default:
//                                 DrawTile(g, board[r, c], c * TILE_SIZE, r * TILE_SIZE);
//                                 // logForm.LogMessage("in defualt");
//                                 break;
//                         }

//                 }
//             }
//             Font font = new Font("Arial", 16);
//             g.DrawString($"Score: {score}", font, Brushes.Black, 10, SCREEN_SIZE + 10);
//         }

//         private void DrawTile(Graphics g, int value, int x, int y)
//         {
//             Color tileColor = GetTileColor(value);
//             g.FillRectangle(new SolidBrush(tileColor), x, y, TILE_SIZE, TILE_SIZE);

//             if (value != 0)
//             {
//                 Font font = new Font("Arial", 24, FontStyle.Bold);
//                 SizeF size = g.MeasureString(value.ToString(), font);
//                 g.DrawString(value.ToString(), font, Brushes.Black, x + (TILE_SIZE - size.Width) / 2, y + (TILE_SIZE - size.Height) / 2);
//             }
//         }

//         private Color GetTileColor(int value)
//         {
//             Color color;
//             switch (value)
//             {
//                 case -1: color = Color.FromArgb(0, 0, 0); break;
//                 case 0: color = Color.FromArgb(205, 193, 180); break;
//                 case 2: color = Color.FromArgb(238, 228, 218); break;
//                 case 4: color = Color.FromArgb(237, 224, 200); break;
//                 case 8: color = Color.FromArgb(242, 177, 121); break;
//                 case 16: color = Color.FromArgb(245, 149, 99); break;
//                 case 32: color = Color.FromArgb(246, 124, 95); break;
//                 case 64: color = Color.FromArgb(246, 94, 59); break;
//                 case 128: color = Color.FromArgb(237, 207, 114); break;
//                 case 256: color = Color.FromArgb(237, 204, 97); break;
//                 case 512: color = Color.FromArgb(237, 200, 80); break;
//                 case 1024: color = Color.FromArgb(237, 197, 63); break;
//                 case 2048: color = Color.FromArgb(237, 194, 46); break;
//                 default: color = Color.FromArgb(60, 58, 50); break;
//             }
//             return color;
//         }

//         private void OnKeyDown(object sender, KeyEventArgs e)
//         {

//             if (ignoreKeyEvents)
//                 return;  // Ignore key events if flag is true

//             if (false)
//             {
//                 var historyBefore = "";
//                 logForm.LogMessage("============Board Before================");
//                 for (int r = 0; r < 4; r++)
//                 {
//                     for (int c = 0; c < 4; c++)
//                     {
//                         historyBefore += "" + board[r, c];
//                         logForm.LogMessage("box number:" + coordinate_TO_pos(r, c));
//                         if (board[r, c] != 0)
//                             logForm.LogMessage("Word: " + board[r, c].ToString());
//                     }
//                 }
//                 history.Add(historyBefore);
//                 logForm.LogMessage(historyBefore);
//             }
//             logForm.LogMessage("============Board================");

//             switch (e.KeyCode)
//             {
//                 case Keys.Left:
//                     CalculateSlideLeft();
//                     AddTile();
//                     Invalidate();
//                     break;
//                 case Keys.Right:
//                     CalculateSlideRight();
//                     AddTile();
//                     Invalidate();
//                     break;
//                 case Keys.Up:
//                     CalculateSlideUp();
//                     AddTile();
//                     Invalidate();
//                     break;
//                 case Keys.Down:
//                     CalculateSlideDown();
//                     AddTile();
//                     Invalidate();
//                     break;
//             }
//             if (false)
//             {
//                 var historyAfter = "";
//                 logForm.LogMessage("============Board After==============");
//                 for (int r = 0; r < 4; r++)
//                 {
//                     for (int c = 0; c < 4; c++)
//                     {
//                         historyAfter += "" + board[r, c];
//                         logForm.LogMessage("box number:" + coordinate_TO_pos(r, c));
//                         if (board[r, c] != 0)
//                             logForm.LogMessage("Word: " + board[r, c].ToString());
//                     }
//                 }
//                 history.Add(historyAfter);
//                 logForm.LogMessage(historyAfter);
//             }

//             if (false)
//                 if (history[history.Count - 2].Equals(history[history.Count - 1]))
//                 {
//                     // Create and configure a Label for displaying text
//                     Label gameOverLabel = new Label();
//                     gameOverLabel.Text = $"Game\nOver!\nScore:\n{score}";
//                     gameOverLabel.Font = new Font("Arial", 60, FontStyle.Bold);
//                     gameOverLabel.ForeColor = Color.LightGray;
//                     gameOverLabel.TextAlign = ContentAlignment.MiddleCenter;
//                     gameOverLabel.Dock = DockStyle.Fill; // Dock label to fill the form

//                     // Add Label to gameOver form
//                     gameOver.Controls.Add(gameOverLabel);

//                     ignoreKeyEvents = true;
//                     gameOver.Show();
//                 }

//         }

//         [STAThread]
//         public static void Main()
//         {
//             Application.Run(new TheGame2048());
//         }
//     }
// }