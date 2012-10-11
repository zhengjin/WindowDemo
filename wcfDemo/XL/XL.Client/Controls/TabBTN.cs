using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XL.Client
{
    public partial class TabBTN : UserControl
    {
        /// <summary>
        /// Tab标题
        /// </summary>
        public string Caption;
        /// <summary>
        /// 是否选中
        /// </summary>
        bool IsSelected = true;
        /// <summary>
        /// 文字的颜色
        /// </summary>
        Color StrColor = Color.Black;
        /// <summary>
        /// 宽度
        /// </summary>
        int StrWidth;
        /// <summary>
        /// 选中事件
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public event EventHandler OnSelect;
        /// <summary>
        /// 单击关闭按钮事件
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public event EventHandler OnClose;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TabBTN()
        {
            InitializeComponent();
        }
        #region 事件
        /// <summary>
        /// 鼠标移入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBTN_MouseEnter(object sender, EventArgs e)
        {
            if (!IsSelected)
            {
                this.BackColor = ColorTranslator.FromHtml("#4D6082");
            }
        }
        /// <summary>
        /// 鼠标移出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBTN_MouseLeave(object sender, EventArgs e)
        {
            if (!IsSelected)
            {
                this.BackColor = ColorTranslator.FromHtml("#293955");
            }
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBTN_MouseMove(object sender, MouseEventArgs e)
        {
            var flag = IsMouseOnClosePoint();
            if (flag)
            {
                DrawControl(Color.Black, Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(166))))));
            }
            else
            {
                DrawControl(StrColor, this.BackColor);
            }
        }
        /// <summary>
        /// 重绘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBTN_Paint(object sender, PaintEventArgs e)
        {
            DrawControl(StrColor, this.BackColor);
        }
        #endregion
        /// <summary>
        /// 重写创建事件
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            var g = this.CreateGraphics();
            StrWidth = (int)g.MeasureString(Caption, SystemFonts.DefaultFont).Width;
            g.Dispose();
            this.Width = StrWidth + 24;
        }
        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="fore"></param>
        /// <param name="bg"></param>
        void DrawControl(Color fore, Color bg)
        {
            var g = this.CreateGraphics();
            g.DrawString(Caption, SystemFonts.DefaultFont, new SolidBrush(StrColor), new PointF(3, 8));
            var p = new Pen(fore, (float)1);
            g.FillRectangle(new SolidBrush(bg), new Rectangle(StrWidth + 6, 7, 13, 13));
            g.TranslateTransform(StrWidth + 12, 13);
            g.RotateTransform(45);
            for (var i = 0; i < 4; i++)
            {
                g.RotateTransform(90);
                g.DrawLine(p, 0, 0, 6, 0);
            }
            g.ResetTransform();
            p.Dispose();
            g.Dispose();
        }
        /// <summary>
        /// 鼠标位置
        /// </summary>
        /// <returns></returns>
        public bool IsMouseOnClosePoint()
        {
            var p = this.PointToClient(MousePosition);
            var crx = new Rectangle(StrWidth + 3, 3, 16, 16);
            return crx.Contains(p);
        }
        /// <summary>
        /// 取消选中
        /// </summary>
        public void DisSelectMe()
        {
            IsSelected = false;
            this.BackColor = ColorTranslator.FromHtml("#293955");
            StrColor = Color.White;
            DrawControl(StrColor, this.BackColor);
        }
        /// <summary>
        /// 选择中
        /// </summary>
        public void SelectMe()
        {
            IsSelected = true;
            this.BackColor = SystemColors.Info;
            StrColor = Color.Black;
            DrawControl(StrColor, this.BackColor);
        }
        /// <summary>
        /// 触发自定义事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBTN_Click(object sender, EventArgs e)
        {
            var flag = IsMouseOnClosePoint();
            if (flag)
            {
                OnClose(this, EventArgs.Empty);
            }
            else
            {
                if (IsSelected)
                {
                    return;
                }
                OnSelect(this, EventArgs.Empty);
                SelectMe();
            }
        }

    }
}
