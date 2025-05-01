using System.Windows.Forms;

namespace KigLand.Labeller;

public partial class frmColourLabeller : Form
{
    public frmColourLabeller()
    {
        InitializeComponent();
    }

    private void listBoxFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        var curItem = listBoxFile.SelectedItem as FileObj;
        picBox.Image = Image.FromFile(curItem.Path);
        picBox.SizeMode = PictureBoxSizeMode.Zoom;
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

        fileLists = Directory.GetFiles(fbd.SelectedPath).Where(x => {
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
                listBoxColours.Items.Add(curColour);
                break;
        }
    }

    private void listBoxFile_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;


        e.DrawBackground();

        var fileObj = listBoxFile.Items[e.Index] as FileObj;
        Brush textBrush;

        if (fileObj.HasLabel)
        {
            // Use a different color for files that have labels
            textBrush = new SolidBrush(Color.Green);
        }
        else
        {
            textBrush = new SolidBrush(Color.Red);//e.ForeColor);
        }

        e.Graphics.DrawString(fileObj.ToString(), e.Font, textBrush, e.Bounds);

        // Draw focus rectangle if item is selected
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
            Txt = global::System.IO.Path.Join(fi.Directory.FullName, string.Join(".", list)+".txt");
        }

    }

    public string BasePath { get; set; }

    public string Path { get; set; }

    public string Name { get; set; }
    public string Txt { get; set; }

    public bool HasLabel
    {
        get
        {
            return File.Exists(this.Txt);
        }
    }

    public override string ToString()
    {
        return Name;
    }
}