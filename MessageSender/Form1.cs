using MsQueue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MessageSender
{
    public partial class Form1 : Form
    {
        MsmqConfig queue;
        SynchronizationContext m_SyncContext = null;
        Random rd = new Random();

        public Form1()
        {
            InitializeComponent();
            //同步线程上下文
            m_SyncContext = SynchronizationContext.Current;
            this.timer1.Interval = 300;

            #region 核心代码
            queue = new MsmqConfig();
            #endregion
            ///设置消息队列名称
            //queue.QueuePath = ".\\Private$\\tsQueue";
        }

        /// <summary>
        /// 异步ui更新
        /// </summary>
        /// <param name="txt"></param>
        private void setText(object txt)
        {
            this.textBox1.Text = DateTime.Now.ToString("yyyy-mm-dd HH:MM:ss.fff") + "\r\n" + this.textBox1.Text;
            this.textBox1.Text = txt.ToString() + "\r\n" + this.textBox1.Text;
        }

        public void sendText(string txt)
        {
            #region 核心代码

            queue.SendText(txt);

            #endregion
            m_SyncContext.Post(setText, txt);
        }
        private string randTxt()
        {
            string t = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder st = new StringBuilder();
            while (st.Length < 5)
            {
                st.Append(t[rd.Next(0, 30)]);
            }
            return st.ToString();
        }

        private void CrazySend()
        {
            sendText(randTxt());
        }

        private void CrazyTest()
        {
            List<Thread> threads = new List<Thread>();
            while (threads.Count < 100)
            {
                threads.Add(new Thread(new ThreadStart(CrazySend)));
            }

            threads.ForEach(item => item.Start());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sendText(randTxt());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.CrazyTest();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }
    }
}
