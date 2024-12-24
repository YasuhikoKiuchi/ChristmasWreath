namespace ChristmasWreath
{
    /// <summary>
    /// フォームクラス
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>乱数発生オブジェクト</summary>
        private Random random = new Random();

        /// <summary>リボンデータ</summary>
        private string data = "M 18.898809,103.1875 9.8273807,90.525294 20.410715,76.351189 52.916666,70.303572 83.910712,89.958331 122.84226,69.925593 l 27.97024,5.669645 6.80357,13.040178 -6.04762,13.040174 -25.3244,6.4256 -35.15179,-5.66964 27.97024,24.19047 13.60714,24.19048 13.22917,53.67262 -12.09524,-6.80357 -9.82738,13.22916 -10.58333,-48.38095 -11.33929,-38.93154 -18.520832,-18.14286 -13.229168,15.49702 -9.827381,41.1994 -1.133929,49.13691 -11.339285,-11.33929 -14.363095,4.91369 9.071428,-54.05059 8.315478,-21.54464 22.300593,-27.21429 -32.127976,7.18155 z";

        /// <summary>リボン描画データ1</summary>
        private List<PointF> list = new List<PointF>();

        /// <summary>リボン描画データ2</summary>
        private List<PointF> list2 = new List<PointF>();

        /// <summary>リボン描画データ3</summary>
        private List<PointF> list3 = new List<PointF>();

        /// <summary>文字描画フォント</summary>
        private Font font = new Font("Comic Sans MS", 64);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            MakeRibbon();
        }

        /// <summary>
        /// 描画イベント
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            DrawWreath(e.Graphics);
            DrawRibbon(e.Graphics);
            DrawString(e.Graphics);

            //DrawSineWave(e.Graphics);
        }

        /// <summary>
        /// リースを描く
        /// </summary>
        /// <param name="g">グラフィック描画オブジェクト</param>
        private void DrawWreath(Graphics g)
        {
            double xx = -1, yy = -1, r = 30;

            for (int i = 0; i <= 360; i += 5)
            {
                double theta = i * Math.PI / 180;
                double x = Math.Cos(theta) * 200 + 245;
                double y = Math.Sin(theta) * 200 + 240;
                if (xx > 0)
                {
                    g.DrawLine(Pens.AliceBlue, (float)xx, (float)yy, (float)x, (float)y);
                }
                xx = x;
                yy = y;
                DrawCircle(g, (float)x, (float)y, (float)r);
            }
        }

        /// <summary>
        /// 円(モール)を描く
        /// </summary>
        /// <param name="g">グラフィック描画オブジェクト</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="r">半径</param>
        private void DrawCircle(Graphics g, float x, float y, float r)
        {
            for (int j = 0; j < 50; j++)
            {
                double theta2 = random.NextDouble() * Math.PI * 2;
                double x1 = x + Math.Cos(theta2) * r;
                double y1 = y + Math.Sin(theta2) * r;
                Pen color = new Pen[] { Pens.Green, Pens.Green, Pens.White }[j % 3];
                Pen color2 = new Pen[] { Pens.Black, Pens.Black, Pens.White }[j % 3];
                g.DrawLine(color, (float)x, (float)y, (float)x1, (float)y1);
                g.DrawLine(color2, (float)x + 1, (float)y, (float)x1 + 1, (float)y1);
            }
        }

        /// <summary>
        /// リボンの描画データを作成する
        /// </summary>
        private void MakeRibbon()
        {
            string[] ds = data.Split(' ');
            int mode = 0;
            float x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            float xmax = 0, ymax = 0, xmin = 999, ymin = 999;

            for (int i = 0; i < ds.Length; i++)
            {
                if (ds[i] == "M")
                {
                    mode = 1;
                }
                else if (ds[i] == "l")
                {
                    mode = 2;
                }
                else if (ds[i].IndexOf(",") > -1)
                {
                    string[] xy = ds[i].Split(',');
                    float x = float.Parse(xy[0]) * 2F;
                    float y = float.Parse(xy[1]) * 1.5F;
                    if (mode == 1)
                    {
                        x2 = x;
                        y2 = y;
                    }
                    else if (mode == 2)
                    {
                        x2 = x1 + x;
                        y2 = y1 + y;
                    }
                    x1 = x2;
                    y1 = y2;
                    list.Add(new PointF(x1 + 68, y1 - 80));
                    if (xmin > x1 + 68) xmin = x1 + 80;
                    if (xmax < x1 + 68) xmax = x1 + 80;
                    if (ymin > y1 - 80) ymin = y1 - 80;
                    if (ymax < y1 - 80) ymax = y1 - 80;
                }

            }
            list.Add(new PointF(list[0].X, list[0].Y));

            float w = xmax - xmin;
            float h = ymax - ymin;
            float halfW = w / 2F;
            float halfH = h / 2F;

            for (int i = 0; i < list.Count; i++)
            {
                list2.Add(new PointF((list[i].X - halfW) / halfW * 190 + 115, (list[i].Y - halfH) / halfH * 80 + 70));
                list3.Add(new PointF((list[i].X - halfW) / halfW * 180 + 120, (list[i].Y - halfH) / halfH * 60 + 60));
                list[i] = new PointF((list[i].X - halfW) / halfW * 200 + 110, (list[i].Y - halfH) / halfH * 100 + 80);
            }
        }

        /// <summary>
        /// リボンを描く
        /// </summary>
        /// <param name="g">グラフィック描画オブジェクト</param>
        private void DrawRibbon(Graphics g)
        {
            g.FillPolygon(Brushes.Red, list.ToArray());
            g.FillPolygon(Brushes.White, list2.ToArray());
            g.FillPolygon(Brushes.Red, list3.ToArray());
            g.FillEllipse(Brushes.Red, 220, 20, 60, 70);
            g.FillEllipse(Brushes.White, 225, 25, 50, 60);
            g.FillEllipse(Brushes.Red, 230, 30, 40, 50);
        }

        /// <summary>
        /// 文字を描く
        /// </summary>
        /// <param name="g">グラフィック描画オブジェクト</param>
        private void DrawString(Graphics g)
        {
            g.DrawString("Merry", font, Brushes.Black, 104, 204);
            g.DrawString("Merry", font, Brushes.Yellow, 100, 200);
            g.DrawString("Christmas!", font, Brushes.Black, 24, 304);
            g.DrawString("Christmas!", font, Brushes.Yellow, 20, 300);
        }

        /// <summary>
        /// モールでサイン波を描く
        /// </summary>
        /// <param name="g">グラフィック描画オブジェクト</param>
        private void DrawSineWave(Graphics g)
        {
            this.SetBounds(0, 0, 1200, 640, BoundsSpecified.Size);
            for (int j = 0; j < 360; j++)
            {
                double x = j * 5;
                double y = Math.Sin((j * 10) * Math.PI / 180) * 150 + 160;
                DrawCircle(g, (float)x, (float)y, 30F);
                y = Math.Sin((j * 10) * Math.PI / 180) * 150 + 160 + 320;
                DrawCircle(g, (float)x, (float)y, 30F);
            }
        }
    }
}