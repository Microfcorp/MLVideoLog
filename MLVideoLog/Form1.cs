using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicLantern.MLLog;

namespace MLVideoLog
{
    public partial class Form1 : Form
    {
        MLLogFile log = null;
        TimeSpan time;
        string VIDEOPath
        {
            get
            {
                return axWindowsMediaPlayer1.URL;
            }
            set
            {
                ResetLabel();
                PlayVideo(value);
                LoadTree();
            }
        }
        public Form1(string[] files)
        {
            InitializeComponent();
            VIDEOPath = (files[0]);
        }

        void PlayVideo(string path)
        {
            var pathlog = MLSearchLogFile.GetMLPath(path);
            axWindowsMediaPlayer1.URL = (path);
            axWindowsMediaPlayer1.Ctlcontrols.play();

            axWindowsMediaPlayer1.OpenStateChange += (o, e) =>
            {
                if (e.newState == 13)
                {
                    float sooth = (float)axWindowsMediaPlayer1.Ctlcontrols.currentItem.imageSourceWidth / (float)axWindowsMediaPlayer1.Ctlcontrols.currentItem.imageSourceHeight;
                    Width = (int)((float)Screen.PrimaryScreen.WorkingArea.Width);
                    Height = (int)(Width * sooth) /*(int)(axWindowsMediaPlayer1.Ctlcontrols.currentItem.imageSourceHeight * (Width / sooth))*/;
                    Location = new Point(0,0);
                    WindowState = FormWindowState.Maximized;
                }
            };

            /*axWindowsMediaPlayer1.PlayStateChange += (o, e) =>
            {
                if (e.newState == 8)
                    button1_Click(null,null);
            };*/

            if (pathlog == null) return;
            log = new MLLogFile(pathlog);
            var staticl = log.StaticData;

            time = TimeSpan.Parse(log.StartTime.Split(' ').Last());
            label6.Text = log.StartTime;
            label7.Text = staticl.WhiteBalance;
            label8.Text = staticl.PictureStyle;
            label9.Text = staticl.FPS.ToString();
            label10.Text = staticl.BitRate;

            label11.Text = staticl.FocusDistance.ToString();
            label12.Text = staticl.FocalLength.ToString();
            label13.Text = staticl.Apperture.ToString();
            label14.Text = staticl.Shutter;
            label15.Text = staticl.ISO.ToString();
        }

        void ResetLabel()
        {
            label6.Text = "None Log File";
            label7.Text = "None Log File";
            label8.Text = "None Log File";
            label9.Text = "None Log File";
            label10.Text = "None Log File";

            label11.Text = "None Log File";
            label12.Text = "None Log File";
            label13.Text = "None Log File";
            label14.Text = "None Log File";
            label15.Text = "None Log File";
        }

        void LoadTree()
        {
            FilesPlaylists.Nodes.Clear();
            var curex = Path.GetExtension(VIDEOPath);
            var dir = Path.GetDirectoryName(VIDEOPath);
            var filescurs = Directory.GetFiles(dir, "*" + curex).OrderBy(tmp => tmp).ToList();
            var curpos = filescurs.IndexOf(VIDEOPath);
            FilesPlaylists.Nodes.AddRange(filescurs.Select(tmp => new TreeNode(tmp)).ToArray());
            FilesPlaylists.Nodes[curpos].NodeFont = GetFont();
            FilesPlaylists.Nodes[curpos].Text += " [PLAY]";
            FilesPlaylists.Nodes[curpos].ForeColor = Color.MediumVioletRed;
        }

        Font GetFont()
        {
            var fnt = new Font(SystemFonts.DefaultFont, FontStyle.Italic);           
            return fnt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsPlaying || log == null) return;

            var time1 = time + new TimeSpan(0,0,(int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition);

            var data = log.DynamicData.Where(tmp => time1 >= TimeSpan.Parse(tmp.Time)).LastOrDefault();

            label11.Text = data.FocusDistance.ToString();
            label12.Text = data.FocalLength.ToString();
            label13.Text = data.Apperture.ToString();
            label14.Text = data.Shutter;
            label15.Text = data.ISO.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var curex = Path.GetExtension(VIDEOPath);
            var dir = Path.GetDirectoryName(VIDEOPath);
            var filescurs = Directory.GetFiles(dir, "*" + curex).OrderBy(tmp => tmp).ToList();
            var curpos = filescurs.IndexOf(VIDEOPath);
            VIDEOPath = filescurs.Count > curpos+1 ? filescurs[curpos+1] : filescurs.First();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var curex = Path.GetExtension(VIDEOPath);
            var dir = Path.GetDirectoryName(VIDEOPath);
            var filescurs = Directory.GetFiles(dir, "*" + curex).OrderBy(tmp => tmp).ToList();
            var curpos = filescurs.IndexOf(VIDEOPath);
            VIDEOPath = curpos - 1 >= 0 ? filescurs[curpos - 1] : filescurs.Last();
        }

        private void FilesPlaylists_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            VIDEOPath = e.Node.Name;
        }

        private void FilesPlaylists_Resize(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            bool isneedpress = FilesPlaylists.Width < VIDEOPath.Length * 6;

            for (int i = 0; i < FilesPlaylists.Nodes.Count; i++)
            {
                if (isneedpress)
                {
                    FilesPlaylists.Nodes[i].Text = Path.GetFileName(FilesPlaylists.Nodes[i].Text);
                }
                else
                {
                    if(FilesPlaylists.Nodes[i].Name != "")
                        FilesPlaylists.Nodes[i].Text = FilesPlaylists.Nodes[i].Name;
                    else FilesPlaylists.Nodes[i].Name = FilesPlaylists.Nodes[i].Text;
                }
            }
        }
    }
}
