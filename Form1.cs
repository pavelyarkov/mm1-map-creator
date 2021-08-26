using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace MM1_map_editor
{
 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<char, TextBox> letterTextBoxDictionary = new Dictionary<char, TextBox>();
        List<Figure> figures = new List<Figure>();
        int intervalSize, width, height;
        Stack<char> availableLetters = new Stack<char>();
        Stack<char> markersCurrentlyInUse = new Stack<char>();
        Image pictureBoxInitialState;
        Figure redoBuffer = null;//last action on pictureBox
        //for checking unsaved actions before closing the program
        List<Figure> initialFigures = new List<Figure>();
        string filePath = null;


        /*types:
      0 - line;
      1 - door;
      2 - marker;
      3 - empty area.
          */
        [Serializable()]
        public class Figure : IEquatable<Figure>
        {
            public Figure(Point a, Point b, int type)
            {
                this.A = a;
                this.B = b;
                this.Type = type;
            }
            public Figure(Point a, Point b, int type, Color color)
            {
                this.A = a;
                this.B = b;
                this.Type = type;
                this.Color = color;
            }
            public Figure(Point a, Point b, int type, DashStyle style, Color color)
            {
                this.A = a;
                this.B = b;
                this.Type = type;
                this.Style = style;
                this.Color = color;
            }

            public Figure(Point a, Point b, int type, char letter)
            {
                this.A = a;
                this.B = b;
                this.Type = type;
                this.Letter = letter;
            }


            public bool Equals(Figure other)
            {
                if ((this.A == other.A) && (this.B == other.B) && (this.Type == other.Type) && (this.Letter == other.Letter))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public Point A { get; }
            public Point B { get; }
            public int Type { get; }
            public char Letter { get; }
            public DashStyle Style { get; set; }
            public Color Color { get; set; }
        }


        private void InitializeStacks()
        {
            //letters for marker action
            for (char letter = 'J'; letter >= 'A'; letter--)
            {
                availableLetters.Push(letter);
            }
        }
        private void InitialState()
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Pen pen = new Pen(new SolidBrush(Color.Black), 1f);
            Font drawFont = new Font("Times New Roman", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            int x1 = 20;
            int y1 = 20;
            width = pictureBox1.Width - 40;
            height = pictureBox1.Height - 40;
            graphics.DrawRectangle(pen, x1, y1, width, height);
            intervalSize = width / 16;

            for (int i = intervalSize; i <= x1 + width; i += intervalSize)
            {
                for (int j = intervalSize; j <= y1 + height; j += intervalSize)
                {
                    graphics.DrawRectangle(pen, i - 1, j - 1, 2, 2);
                }
            }
            int number = 0, numberReversed = 15;
            for (int i = intervalSize; i < x1 + width; i += intervalSize)
            {
                string drawStringX = Convert.ToString(number);
                string drawStringY = Convert.ToString(numberReversed);
                if (numberReversed < 10)
                {
                    graphics.DrawString(drawStringY, drawFont, drawBrush, 4, i);
                }
                else
                {
                    graphics.DrawString(drawStringY, drawFont, drawBrush, 0, i);

                }
                if (number < 10)
                {
                    graphics.DrawString(drawStringX, drawFont, drawBrush, i + 5, pictureBox1.Height - 20);
                }
                else
                {

                    graphics.DrawString(drawStringX, drawFont, drawBrush, i, pictureBox1.Height - 20);
                }
                number++;
                numberReversed--;
            }
            graphics.DrawString("X", drawFont, drawBrush, x1 + width, pictureBox1.Height - 20);
            graphics.DrawString("Y", drawFont, drawBrush, 4, 0);
            this.ActiveControl = pictureBox1;
            pictureBox1.Image = bitmap;
            pictureBoxInitialState = bitmap;
            pen.Dispose();
            drawFont.Dispose();
            drawBrush.Dispose();
            graphics.Dispose();
            
        }
        private void SaveMapInFile(string fPath)
        {
            initialFigures = figures;
            Bitmap bitmap = new Bitmap(pictureBox1.Width + 60, pictureBox1.Height + 300);
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width + 60, pictureBox1.Height + 300);
            pictureBox1.DrawToBitmap(bitmap, pictureBox1.ClientRectangle);
            filePath = fPath; 
            
            string fileName = Path.GetFileName(fPath);
            string newFilePath = fPath.Remove(fPath.Length - fileName.Length);
            newFilePath = Path.Combine(newFilePath, "resources");
            fileName = fileName.Remove(fileName.Length - 4);
            string txtFilePath = Path.Combine(newFilePath, fileName + ".txt");

            string figuresFilePath = Path.Combine(newFilePath, fileName + ".bin");

            Font drawFont = new Font("Times New Roman", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Point titleLocation = tBoxTitle.Location;
            titleLocation.Y = 375;
            if (tBoxTitle.TextLength > 50)
            {
                string tBoxTextSecondPart = tBoxTitle.Text.Substring(50);
                tBoxTitle.Text = tBoxTitle.Text.Remove(50);
                g.DrawString(tBoxTitle.Text, drawFont, drawBrush, titleLocation);
                titleLocation.Y += 13;
                g.DrawString(tBoxTextSecondPart, drawFont, drawBrush, titleLocation);
            }
            else
            {
                g.DrawString(tBoxTitle.Text, drawFont, drawBrush, titleLocation);
            }

            foreach (var item in letterTextBoxDictionary)
            {
                TextBox currentTBox = item.Value;
                if (currentTBox.TextLength > 4)
                {
                    string tBoxText = currentTBox.Text;
                    Point tBoxLocation = currentTBox.Location;
                    if (currentTBox.TextLength > 50)
                    {
                        string tBoxTextSecondPart = tBoxText.Substring(50);
                        tBoxText = tBoxText.Remove(50);
                        g.DrawString(tBoxText, drawFont, drawBrush, tBoxLocation);
                        tBoxLocation.Y += 13;
                        g.DrawString(tBoxTextSecondPart, drawFont, drawBrush, tBoxLocation);
                    }
                    else
                    {
                        g.DrawString(tBoxText, drawFont, drawBrush, tBoxLocation);
                    }
                }

            }
            try
            {
                DirectoryInfo dInfo = Directory.CreateDirectory(newFilePath);
                using (Stream fs = File.Create(fPath))
                {
                    bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                }
                using (Stream fs = File.Create(figuresFilePath))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(fs, figures);
                }
                using (StreamWriter txtFile = new StreamWriter(txtFilePath))
                {
                    txtFile.WriteLine(tBoxTitle.Text);
                    foreach (var item in letterTextBoxDictionary.Reverse())
                    {
                        txtFile.WriteLine(item.Value.Text);
                    }

                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Can't save in the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            drawBrush.Dispose();
            drawFont.Dispose();
        }
        
        private void SaveDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Gif Image|*.gif";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                SaveMapInFile(dialog.FileName);
            }
            dialog.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;//for handling KeyDowns
            letterTextBoxDictionary.Add('J', textBox10);
            letterTextBoxDictionary.Add('I', textBox9);
            letterTextBoxDictionary.Add('H', textBox8);
            letterTextBoxDictionary.Add('G', textBox7);
            letterTextBoxDictionary.Add('F', textBox6);
            letterTextBoxDictionary.Add('E', textBox5);
            letterTextBoxDictionary.Add('D', textBox4);
            letterTextBoxDictionary.Add('C', textBox3);
            letterTextBoxDictionary.Add('B', textBox2);
            letterTextBoxDictionary.Add('A', textBox1);
            InitializeStacks();
            InitialState();
        }
        private double CalculateDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Focus();
            //if clicked outside the grid
            if ((e.X >= 340) || (e.X <= 20) || (e.Y >= 340) || (e.Y <= 20))
            {
                return;
            }
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            //e is the pixel that was clicked
            //locating the square where e is placed in
            //square coords:(x1,y1,x2,y2)
            for (int i = intervalSize; i <= width; i += intervalSize)
            {
                if (i + intervalSize > e.X)
                {
                    x1 = i;
                    x2 = i + intervalSize;
                    break;
                }
            }
            for (int i = intervalSize; i <= height; i += intervalSize)
            {
                if (i + intervalSize > e.Y)
                {
                    y1 = i;
                    y2 = i + intervalSize;
                    break;
                }
            }
            //drawing a marker
            if ((e.X < (x2 + x1) / 2 + 6) && (e.X > (x2 + x1) / 2 - 6) && (e.Y < (y2 + y1) / 2 + 6) && (e.Y > (y2 + y1) / 2 - 6))
            {

                if (e.Button == MouseButtons.Left)
                {
                    Point point1 = new Point(x1, y1);
                    Point point2 = new Point(x2, y2);
                    //draw a filled rectagle
                    if (ModifierKeys == Keys.Alt || ModifierKeys == (Keys.Shift | Keys.Alt))
                    {
                        Figure newFigure = new Figure(point1, point2, 3);
                        if (ModifierKeys == (Keys.Shift | Keys.Alt))
                        {
                            newFigure.Color = Color.FromArgb(128, 204, 255);
                        }
                        else
                        {
                            newFigure.Color = Color.FromArgb(200, 200, 200);
                        }
                        if (figures.Contains(newFigure))
                        {
                            int figIndex = figures.IndexOf(newFigure);
                            newFigure.Color = figures.ElementAt(figIndex).Color;
                            figures.Remove(newFigure);
                            redoBuffer = newFigure;
                        }
                        else
                        {
                            figures.Add(newFigure);
                            redoBuffer = null;
                        }
                    }
                    //draw a marker
                    else
                    {
                        Figure occupyingMarker = figures.Find(x => (x.A == point1) && (x.B == point2) && (x.Type == 2)); //that occupies the candidate place
                        //if there is a marker in (point1,point2) position - copy the letter and use it for Ctrl+Click new markers
                        if (occupyingMarker != null)
                        {
                            char occupyingMarkerLetter = occupyingMarker.Letter;
                            //control+LMB handling
                            if (ModifierKeys == Keys.Control)
                            {
                                markersCurrentlyInUse.Push(occupyingMarkerLetter);
                            }
                            else
                            {
                                figures.Remove(occupyingMarker);
                                redoBuffer = occupyingMarker;
                                if (!figures.Exists(x => x.Letter == occupyingMarkerLetter))
                                {
                                    TextBox currentTextBox = letterTextBoxDictionary[occupyingMarkerLetter];
                                    currentTextBox.Enabled = false;
                                    if (currentTextBox.TextLength > 4)
                                    {
                                        currentTextBox.Text = currentTextBox.Text.Remove(4);
                                    }
                                    markersCurrentlyInUse.Pop();
                                    availableLetters.Push(occupyingMarkerLetter);
                                }

                            }
                        }
                        //if there is no marker in (point1,point2) position then create a new marker
                        else
                        {
                            char lastPlacedLetter;
                            if (markersCurrentlyInUse.Count == 0)
                            {
                                lastPlacedLetter = 'A';
                            }
                            else
                            {
                                lastPlacedLetter = markersCurrentlyInUse.Peek();
                            }

                            if ((ModifierKeys == Keys.Control) && figures.Exists(x => x.Letter == lastPlacedLetter))
                            {
                                Figure newFigure = new Figure(point1, point2, 2, lastPlacedLetter);
                                figures.Add(newFigure);
                                redoBuffer = null;
                                markersCurrentlyInUse.Push(lastPlacedLetter);
                            }
                            else
                            {

                                try
                                {
                                    char currentLetter = availableLetters.Pop();
                                    TextBox currentTextBox = letterTextBoxDictionary[currentLetter];
                                    lastPlacedLetter = currentLetter;
                                    currentTextBox.Enabled = true;
                                    Figure newFigure = new Figure(point1, point2, 2, currentLetter);
                                    figures.Add(newFigure);
                                    redoBuffer = null;
                                    markersCurrentlyInUse.Push(currentLetter);
                                }
                                catch (Exception)
                                {
                                    //if all available (A-J) letters is used
                                }

                            }
                        }
                    }
                }

                pictureBox1.Invalidate();
                return;
            }
            //finding the two nearest neighbors to e 
            List<Point> square;
            square = new List<Point>();
            square.Add(new Point(x1, y1));
            square.Add(new Point(x1, y2));
            square.Add(new Point(x2, y1));
            square.Add(new Point(x2, y2));
            double distMin = CalculateDistance(x1, e.X, y1, e.Y);
            Point pointMinPrev = square[0];
            Point pointMin = square[0];
            double distMinPrev = 999;
            for (int i = 1; i < 4; i++)
            {
                double dist = CalculateDistance(square[i].X, e.X, square[i].Y, e.Y);
                if (dist + 1e-8 < distMin)
                {
                    distMinPrev = distMin;
                    pointMinPrev = pointMin;
                    distMin = dist;
                    pointMin = square[i];
                }
                else
                {
                    if (dist + 1e-8 < distMinPrev)
                    {
                        pointMinPrev = square[i];
                        distMinPrev = dist;
                    }
                }
            }

            //drawing a line or a rectangle between the two points 
            //replace start and end points in ascending order
            if (pointMin.X == pointMinPrev.X)
            {
                if (pointMin.Y > pointMinPrev.Y)
                {
                    Point forSwap = pointMin;
                    pointMin = pointMinPrev;
                    pointMinPrev = forSwap;
                }
            }
            else
            {
                if (pointMin.X > pointMinPrev.X)
                {
                    Point forSwap = pointMin;
                    pointMin = pointMinPrev;
                    pointMinPrev = forSwap;
                }
            }
            Figure line = new Figure(pointMin, pointMinPrev, 0);
            Figure door = new Figure(pointMin, pointMinPrev, 1);
            if (ModifierKeys == Keys.Alt || (ModifierKeys == (Keys.Shift | Keys.Alt)))
            {
                line.Style = DashStyle.Dash;
                door.Style = DashStyle.Dash;
            }
            if (ModifierKeys==Keys.Shift || (ModifierKeys==(Keys.Shift | Keys.Alt)))
            {
                line.Color = Color.FromArgb(0,153,255);
                door.Color = Color.FromArgb(0,153,255);
            }
            else
            {
                line.Color = Color.Black;
                door.Color = Color.Black;
            }
            if (!figures.Exists(x => (x.A == pointMin) && (x.B == pointMinPrev)))
            {
                if (e.Button == MouseButtons.Right)
                {
                    figures.Add(door);
                    redoBuffer = null;
                }
                else
                {
                    figures.Add(line);
                    redoBuffer = null;
                }
            }
            else
            {
                if ((e.Button == MouseButtons.Left))
                {
                    if (figures.Contains(door))
                    {
                        int doorIndex = figures.IndexOf(door);
                        door.Style = figures.ElementAt(doorIndex).Style;
                        door.Color= figures.ElementAt(doorIndex).Color;
                        figures.Remove(door);
                        redoBuffer = door;
                    }
                    else
                    {
                        int lineIndex = figures.IndexOf(line);
                        line.Style = figures.ElementAt(lineIndex).Style;
                        line.Color = figures.ElementAt(lineIndex).Color;
                        figures.Remove(line);
                        redoBuffer = line;
                    }
                }

            }

            pictureBox1.Invalidate();
        }

        private void DeselectTextBoxContent(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Select(tb.Text.Length, 0);
        }


        private void GetToInitialState()
        {
            filePath = null;
            figures.Clear();
            initialFigures.Clear();
            availableLetters.Clear();
            markersCurrentlyInUse.Clear();
            InitializeStacks();
            redoBuffer = null;
            foreach (var item in letterTextBoxDictionary)
            {
                string dirtyText = item.Value.Text;
                string clearedText = item.Key+" - "; 
                item.Value.Text = clearedText;
                item.Value.Enabled = false;
            }
            if (tBoxTitle.Text.Length > 0)
            {
                tBoxTitle.Text = "";
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                int listSize = figures.Count;
                //if there is nothing to redo then undo last action
                if ((redoBuffer == null) && (listSize > 0))
                {
                    Figure figureForUndo = figures.ElementAt(listSize - 1);
                    figures.Remove(figureForUndo);
                    redoBuffer = figureForUndo;
                    if (figureForUndo.Type == 2)
                    {
                        char currentLetter = figureForUndo.Letter;
                        if (!figures.Exists(x => x.Letter == currentLetter))
                        {
                            TextBox currentTextBox = letterTextBoxDictionary[currentLetter];
                            currentTextBox.Enabled = false;
                            if (currentTextBox.TextLength > 4)
                            {
                                currentTextBox.Text = currentTextBox.Text.Remove(4);
                            }
                            markersCurrentlyInUse.Pop();
                            availableLetters.Push(currentLetter);
                        }
                    }
                }
                else
                {
                    if (redoBuffer != null)
                    {
                        Figure figureForRedo = redoBuffer;
                        redoBuffer = null;
                        if (figureForRedo.Type == 2)
                        {
                            char currentLetter = figureForRedo.Letter;
                            if (!figures.Exists(x => x.Letter == currentLetter))
                            {
                                TextBox currentTextBox = letterTextBoxDictionary[currentLetter];
                                currentTextBox.Enabled = true;
                                markersCurrentlyInUse.Push(currentLetter);
                                availableLetters.Pop();
                            }
                        }
                        figures.Add(figureForRedo);
                    }

                }
                pictureBox1.Invalidate();
            }
            else
            {
                if (e.Control && e.KeyCode == Keys.E)
                {
                    DialogResult answer = AreThereUnsavedChanges();
                    if (answer != DialogResult.Cancel)
                    {
                        if (answer == DialogResult.Yes)
                        {
                            if (filePath != null)
                            {
                                SaveMapInFile(filePath);
                            }
                            else
                            {
                                SaveDialog();
                            }
                        }
                        pictureBox1.Image = pictureBoxInitialState;
                        GetToInitialState();
                    }
                }
                else
                {
                    if (e.Control && e.KeyCode == Keys.S)
                    {
                        if ((filePath != null))
                        {
                            if (e.Shift)
                            {
                                SaveDialog();
                            }
                            else
                            {
                                SaveMapInFile(filePath);
                            }
                        }
                        else
                        {
                            SaveDialog();
                        }
                    }
                    else
                    {
                        if (e.Control && e.KeyCode == Keys.L)
                        {
                            DialogResult answer = AreThereUnsavedChanges();
                            if (answer != DialogResult.Cancel)
                            {
                                if (answer == DialogResult.Yes)
                                {
                                    if (filePath != null)
                                    {
                                        SaveMapInFile(filePath);
                                    }
                                    else
                                    {
                                        SaveDialog();
                                    }
                                }
                                LoadMapFromFile();
                            }
                        }
                        else
                        {
                            if (e.KeyCode == Keys.F1)
                            {
                                MessageBox.Show("Ctrl+Z - undo;\nCtrl+E - clear;\nCtrl+S - save, Ctrl+Shift+S - save as...;\n" +
                                    "Ctrl+L - load .gif;\nRMB - place a door;\nLMB - place/delete a wall, a marker;\n" +
                                    "LMB + Ctrl - duplicate a marker.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                    }
                }
            }
        }
        private DialogResult AreThereUnsavedChanges()
        {
            DialogResult answer=DialogResult.None;
            if (!figures.All(initialFigures.Contains) || (figures.Count != initialFigures.Count))
            {
                answer = MessageBox.Show("There are unsaved changes. Save them in a file?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            }
            return answer;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
                DialogResult answer = AreThereUnsavedChanges();
                if (answer == DialogResult.Yes)
                {
                    if (filePath != null)
                    {
                        SaveMapInFile(filePath);
                    }
                    else
                    {
                        SaveDialog();
                    }
                }
                else
                {
                    if (answer == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
        }

        private void LoadMapFromFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Gif Image|*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //load a map
                GetToInitialState();
                filePath = dialog.FileName;
                //load tBoxes of the map

                string fileName = Path.GetFileName(filePath);
                string newFilePath = filePath.Remove(filePath.Length - fileName.Length);
                //file path to resources folder
                newFilePath = Path.Combine(newFilePath, "resources");
                //filename without extension
                fileName = fileName.Remove(fileName.Length - 4);

                string txtFilePath = Path.Combine(newFilePath, fileName + ".txt");
                string figuresFilePath = Path.Combine(newFilePath, fileName + ".bin");

                try
                {
                    using (Stream fs = File.OpenRead(figuresFilePath))
                    {
                        BinaryFormatter deserializer = new BinaryFormatter();
                        figures = (List<Figure>)deserializer.Deserialize(fs);
                        //redoBuffer = null;
                        pictureBox1.Invalidate();
                    }
                    using (StreamReader txtFile = new StreamReader(txtFilePath))
                    {
                        tBoxTitle.Text = txtFile.ReadLine();
                        IEnumerable<Figure> markersList = figures.Where(x => x.Letter != 0);
                        foreach (var item in markersList)
                        {
                            //if //(!markersCurrentlyInUse.Contains(item.Letter))
                            {
                                if (!markersCurrentlyInUse.Contains(item.Letter))
                                {
                                    availableLetters.Pop();
                                    letterTextBoxDictionary[item.Letter].Enabled = true;
                                    letterTextBoxDictionary[item.Letter].Text = txtFile.ReadLine();
                                }
                                markersCurrentlyInUse.Push(item.Letter);

                            }

                        }

                    }
                    Figure[] t = new Figure[figures.Count];
                    figures.CopyTo(t);
                    initialFigures = t.ToList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error while loading map", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            dialog.Dispose();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics myGraphics = e.Graphics;
            Pen pen = new Pen(new SolidBrush(Color.Black), 1f);
            foreach (Figure fig in figures)
            {
                Point a = fig.A;
                Point b = fig.B;
                pen.DashStyle = fig.Style;
                pen.Color = fig.Color;
                if (fig.Type == 0)
                {
                    myGraphics.DrawLine(pen, a, b);
                }
                else
                {
                    if (fig.Type == 2)
                    {
                        string letter = fig.Letter.ToString();
                        Font font = new Font("Arial", 8, FontStyle.Bold);
                        SolidBrush brush = new SolidBrush(Color.Black);
                        myGraphics.DrawString(letter, font, brush, (a.X + b.X) / 2 - 5, (a.Y + b.Y) / 2 - 5);
                        font.Dispose();
                        brush.Dispose();
                    }
                    else
                    {
                        if (fig.Type == 3)
                        {
                            SolidBrush brush = new SolidBrush(fig.Color);

                            Rectangle rectangle = new Rectangle(a.X, a.Y, intervalSize, intervalSize);
                            myGraphics.FillRectangle(brush, rectangle);
                        }
                        else
                        {
                            GraphicsPath door = new GraphicsPath();
                            Rectangle rectangle = new Rectangle((a.X + b.X) / 2 - 3, (a.Y + b.Y) / 2 - 3, 6, 6);
                            door.AddLine(a, b);
                            door.AddRectangle(rectangle);
                            myGraphics.DrawPath(pen, door);
                        }

                    }

                }
            }
            pen.Dispose();
        }
    }
}
