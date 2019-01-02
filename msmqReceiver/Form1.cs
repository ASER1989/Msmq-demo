using MsQueue; 
using System; 
using System.Threading; 
using System.Windows.Forms;

namespace msmqReceiver
{
    public partial class Form1 : Form
    {
        MsmqConfig mq = new MsmqConfig();
        SynchronizationContext m_SyncContext = null; 

        public Form1()
        {
            InitializeComponent();
            //同步线程上下文
            m_SyncContext = SynchronizationContext.Current;

            #region 核心代码

            //异步消息监听
            mq.ListenCallback = MessageHandle;
            //开启异步消息监听，独立线程，逐条消费消息内容，直到程序终止或调用AbotListen();
            mq.MessageListen();

            #endregion

        }


        #region 核心代码
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="txt">消息内容</param>
        public void MessageHandle(string txt)
        { 
            m_SyncContext.Post(setText, txt); 
        }

        #endregion

        /// <summary>
        /// 异步ui更新
        /// </summary>
        /// <param name="txt"></param>
        private void setText(object txt)
        {
            this.retextbox.Text = DateTime.Now.ToString("yyyy-mm-dd HH:MM:ss.fff") + "\r\n" + this.retextbox.Text;
            this.retextbox.Text = txt.ToString() + "\r\n" + this.retextbox.Text; 
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            this.retextbox.Text = "";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // System.Environment.Exit(0);
           mq.AbotListen();
        }
    }
}
