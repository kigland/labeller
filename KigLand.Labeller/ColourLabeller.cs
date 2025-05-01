using System.Windows.Forms;

namespace KigLand.Labeller;

public partial class frmColourLabeller : Form
{
    public frmColourLabeller()
    {
        InitializeComponent();
    }

    FileObj curItem {
        get {
            return listBoxFile.SelectedItem as FileObj;
        }
    }
    private void listBoxFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (curItem == null) return;
        curItem.RefreshTxt();
        listBoxFile.Invalidate(listBoxFile.GetItemRectangle(listBoxFile.SelectedIndex));
        picBox.Image = Image.FromFile(curItem.Path);
        picBox.SizeMode = PictureBoxSizeMode.Zoom;
        refreshListBoxColours();
    }

    Color curColour;
    private void picBox_MouseClick(object sender, MouseEventArgs e)
    {
        if (picBox.Image == null) return;

        float scaleX = (float)picBox.Image.Width / picBox.ClientSize.Width;
        float scaleY = (float)picBox.Image.Height / picBox.ClientSize.Height;
        float scale = Math.Max(scaleX, scaleY);

        int displayedWidth = (int)(picBox.Image.Width / scale);
        int displayedHeight = (int)(picBox.Image.Height / scale);

        int offsetX = (picBox.ClientSize.Width - displayedWidth) / 2;
        int offsetY = (picBox.ClientSize.Height - displayedHeight) / 2;

        if (e.X < offsetX || e.X >= offsetX + displayedWidth ||
            e.Y < offsetY || e.Y >= offsetY + displayedHeight)
        {
            return;
        }

        int imageX = (int)((e.X - offsetX) * scale);
        int imageY = (int)((e.Y - offsetY) * scale);

        try
        {
            using var bit = new Bitmap(picBox.Image);
            imageX = Math.Max(0, Math.Min(bit.Width - 1, imageX));
            imageY = Math.Max(0, Math.Min(bit.Height - 1, imageY));

            curColour = bit.GetPixel(imageX, imageY);
            lblColour.Text = curColour.ToString();
            colourBox.BackColor = curColour;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error getting pixel color: {ex.Message}", "Error");
        }
    }

    string[] fileLists;

    private void btnOpen_Click(object sender, EventArgs e)
    {
        using var fbd = new FolderBrowserDialog();
        DialogResult result = fbd.ShowDialog();

        if (result != DialogResult.OK || string.IsNullOrWhiteSpace(fbd.SelectedPath)) return;

        fileLists = Directory.GetFiles(fbd.SelectedPath).Where(x =>
        {
            x = x.ToLower();
            return x.EndsWith(".png") || x.EndsWith(".jpg") || x.EndsWith("jpeg");
        }).ToArray();
        foreach (var f in fileLists)
        {
            listBoxFile.Items.Add(new FileObj(f));
        }
    }

    private void frmColourLabeller_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Enter:
                if (curItem != null)
                {
                    if (curItem.Colours == null) {
                        curItem.Colours = new List<Color>();
                        listBoxFile.Invalidate(listBoxFile.GetItemRectangle(listBoxFile.SelectedIndex));
                    }
                    curItem.Colours.Add(curColour);
                    curItem.SaveTxt();
                    refreshListBoxColours();
                }
                break;
            case Keys.A:
                var idx = listBoxFile.SelectedIndex;
                if (idx == -1) idx = 0;
                idx = Math.Max(0, idx - 1);
                listBoxFile.SelectedIndex = idx;
                break;
            case Keys.D:
                idx = listBoxFile.SelectedIndex;
                if (idx == -1) idx = 0;
                idx = Math.Min(listBoxFile.Items.Count - 1, idx + 1);
                listBoxFile.SelectedIndex = idx;
                break;
        }
    }

    private void refreshListBoxColours() {
        listBoxColours.Items.Clear();
        if (curItem != null && curItem.Colours != null) {
            listBoxColours.Items.AddRange(curItem.Colours.Select(c => $"{c.R},{c.G},{c.B}").ToArray());
        }
    }


    private void listBoxFile_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;


        e.DrawBackground();

        var fileObj = listBoxFile.Items[e.Index] as FileObj;
        Brush textBrush = fileObj.HasLabel ? new SolidBrush(Color.Green) : new SolidBrush(Color.Red);

        e.Graphics.DrawString(fileObj.ToString(), e.Font, textBrush, e.Bounds);

        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            e.DrawFocusRectangle();
    }

    private void listBoxColours_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;

        e.DrawBackground();

        // Get the color string from the listbox
        string colorStr = listBoxColours.Items[e.Index].ToString();
        
        // Parse the RGB values
        string[] rgb = colorStr.Split(',');
        if (rgb.Length == 3 && int.TryParse(rgb[0], out int r) && 
            int.TryParse(rgb[1], out int g) && 
            int.TryParse(rgb[2], out int b))
        {
            // Create color and brushes
            Color itemColor = Color.FromArgb(r, g, b);
            using var colorBrush = new SolidBrush(itemColor);
            using var textBrush = new SolidBrush(Color.Black);

            // Draw color rectangle
            int colorBoxWidth = 50;
            var colorRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 
                                       colorBoxWidth, e.Bounds.Height - 4);
            e.Graphics.FillRectangle(colorBrush, colorRect);
            e.Graphics.DrawRectangle(Pens.Black, colorRect);

            // Draw RGB text
            var textRect = new Rectangle(e.Bounds.X + colorBoxWidth + 5, e.Bounds.Y,
                                       e.Bounds.Width - colorBoxWidth - 5, e.Bounds.Height);
            e.Graphics.DrawString(colorStr, e.Font, textBrush, textRect, 
                                StringFormat.GenericDefault);
        }

        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            e.DrawFocusRectangle();
    }
}

class FileObj
{
    public FileObj(string path)
    {
        this.Path = path;
        FileInfo fi = new FileInfo(path);
        this.Name = fi.Name;
        this.BasePath = fi.Directory.FullName;
        if (fi.Extension == ".txt") Txt = fi.FullName;
        else
        {
            var stems = fi.Name.Split('.');
            var list = new List<string>(stems);
            list.RemoveAt(list.Count - 1);
            Txt = global::System.IO.Path.Join(fi.Directory.FullName, string.Join(".", list) + ".txt");
        }
        RefreshTxt();

    }

    public void SaveTxt()
    {
        if (Colours == null) return;
        File.WriteAllLines(Txt, Colours.Select(c => $"{c.R},{c.G},{c.B}").ToArray());
    }

    public List<Color> Colours { get; set; }

    public string BasePath { get; set; }

    public string Path { get; set; }

    public string Name { get; set; }
    public string Txt { get; set; }

    public void RefreshTxt()
    {
        if (File.Exists(Txt))
        {
            var colours = File.ReadAllLines(Txt).Select<string, Color?>(x =>
            {
                x = x.Trim();
                if (string.IsNullOrWhiteSpace(x)) return null;
                try
                {
                    var split = x.Split(',');
                    return Color.FromArgb(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
                }
                catch (Exception ex) { MessageBox.Show($"Error reading colour: {ex.Message}", "Error"); return null; }
            }).ToArray();
            Colours = colours.Where(x => x != null).Select(c => c.Value).ToList();
        }
    }

    public bool HasLabel
    {
        get
        {
            return File.Exists(this.Txt) && File.ReadAllLines(this.Txt).Length > 0;
        }
    }

    public override string ToString()
    {
        return Name;
    }
}