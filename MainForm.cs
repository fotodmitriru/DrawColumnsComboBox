using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DrawColumnsComboBox
{
    public partial class MainForm : XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void comboBoxEdit1_Properties_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (!(comboBoxEdit1.Properties.Items[e.Index] is ConfigRepository cfg)) return;
            Rectangle column1Rectangle = e.Bounds;
            column1Rectangle.Width /= 2;
            column1Rectangle.Width += 40;

            //e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);

            using (var backBrush = new SolidBrush(Color.White))
                e.Cache.FillRectangle(backBrush, e.Bounds);

            Rectangle column2Rectangle = e.Bounds;
            column2Rectangle.X = column2Rectangle.Width / 2;
            column2Rectangle.X += 40;
            column2Rectangle.Width /= 2;
            column2Rectangle.Width -= 40;

            var sizeStr = e.Graphics.MeasureString(cfg.SchemeType, e.Appearance.Font); //получаем ширину текста для второй колонки

            Rectangle column2MarginText = column2Rectangle;
            int widthOfText = column2MarginText.Width / 2 - Convert.ToInt32(sizeStr.Width / 2); //рассчёт координаты X текста
            column2MarginText.X += (widthOfText > 0 ? widthOfText : 0); //выравнивание текста по центру во второй колонке

            Pen p1 = new Pen(Color.DarkGray);
            e.Graphics.DrawLine(p1, column1Rectangle.Right, 0, column1Rectangle.Right, column1Rectangle.Bottom);

            //Pen p2 = new Pen(Color.Red);
            //e.Graphics.DrawLine(p2, column2Rectangle.Right, 0, column2Rectangle.Right, column2Rectangle.Bottom);

            SolidBrush sb1 = new SolidBrush(e.Appearance.ForeColor);
            SolidBrush sb2 = new SolidBrush(Color.Gray);
            var font = e.Appearance.Font;

            e.Graphics.DrawString(cfg.RepositoryUri.ToString(), font, sb1, column1Rectangle);
            e.Graphics.DrawString(cfg.SchemeType, font, sb2, column2MarginText);

            e.Handled = true;

            if (e.State != DrawItemState.Selected) return;

            using (var backBrush = new LinearGradientBrush(e.Bounds, Color.CornflowerBlue, Color.LightCyan,
                LinearGradientMode.Horizontal))
                e.Cache.FillRectangle(backBrush, e.Bounds);

            e.Graphics.DrawString(cfg.RepositoryUri.ToString(), font, sb1, column1Rectangle);
            e.Graphics.DrawString(cfg.SchemeType, font, sb2, column2MarginText);

            //e.Graphics.DrawLine(p1, column1Rectangle.Right, 0, column1Rectangle.Right, column1Rectangle.Bottom);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigRepository conf1 = new ConfigRepository()
                { SchemeType = "file://localRep", RepositoryUri = new Uri("file://C:/LocalRep/templates") };
            ConfigRepository conf2 = new ConfigRepository()
                { SchemeType = "file://localRep", RepositoryUri = new Uri("file://D:/LocalRep2/templates") };
            ConfigRepository conf3 = new ConfigRepository()
                { SchemeType = "http://ca.agroprombank.comlocalRep1234567890", RepositoryUri = new Uri("file://E:/GlobalRep3/templates/12345689012345678901234567890") };

            ConfigRepository[] arrayRepositories = new[] {conf1, conf2, conf3};

            foreach (ConfigRepository repository in arrayRepositories)
            {
                comboBoxEdit1.Properties.Items.Add(repository);
            }
        }
    }
}