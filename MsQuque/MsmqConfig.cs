using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Threading;

namespace MsQueue
{
    public class MsmqConfig
    {
        public delegate void OnRceive(string txt);
        public MessageQueue Queue { get; private set; }
        private string _path = ".\\Private$\\qiquanQueue"; 
        private Thread listenThread = null;

        public MsmqConfig()
        {
            init();
            Queue.ReceiveCompleted += new ReceiveCompletedEventHandler(mq_ReceiveCompleted);
        }
        /// <summary>
        /// 消息监听回调
        /// </summary>
        public OnRceive ListenCallback { private get; set; }

        /// <summary>
        /// 队列路径
        /// </summary>
        public string QueuePath
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value; 
                init();
            }
        }
        /// <summary>
        /// 消息队列初始化
        /// </summary>
        private void init()
        { 
            //如果存在指定路径的消息队列 
            if (MessageQueue.Exists(_path))
            {
                //获取这个消息队列
                Queue = new MessageQueue(_path);
            }
            else
            {
                //不存在，就创建一个新的，并获取这个消息队列对象
                Queue = MessageQueue.Create(_path);
            }
        }

        /// <summary>
        /// 发送文本内容
        /// </summary>
        /// <param name="txt"></param>
        public void SendText(string txt)
        {
            System.Messaging.Message msg = new System.Messaging.Message();
            //内容
            msg.Body = txt;
            //指定格式化程序
            msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            Queue.Send(msg);
        }

        /// <summary>
        /// 接受第一条文本内容（线程阻塞，会导致线程无响应。窗口关闭时需终止所有进程，否则会消费掉下一条消息）
        /// </summary>
        public void ReceiveText(OnRceive callback)
        {
            //接收到的消息对象
            System.Messaging.Message msg = Queue.Receive();
            //指定格式化程序
            msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            //接收到的内容
            string str = msg.Body.ToString();
            callback(str); 
        }

        /// <summary>
        /// 异步消息接收
        /// </summary>
        public void BeginReceiveText()
        {
            if (ListenCallback == null)
            {
                throw new Exception("设置ListenCallback");
            }
            Queue.BeginReceive(); 
        }

        #region 异步接收
        public void mq_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {
            try
            {
                Message msg = Queue.EndReceive(e.AsyncResult);
                msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                //接收到的内容
                string str = msg.Body.ToString();
                if (this.ListenCallback != null)
                {
                    this.ListenCallback(str);
                }

            }
            catch (Exception ex)
            { 
                throw ex;
            }
            finally
            {
                BeginReceiveText();
            }
            
        } 

        public void MessageListen()
        {
            if (ListenCallback == null)
            {
                throw new Exception("必须设置ListenCallback"); 
            }
             
            listenThread= new Thread(new ThreadStart(this.BeginReceiveText));
            listenThread.Start();

        }
        #endregion

        /// <summary>
        /// 结束监听
        /// </summary>
        public void AbotListen()
        {
            if(listenThread!=null && listenThread.ThreadState !=ThreadState.Aborted) { 
                listenThread.Abort(); 
            }
            
        }

         
    }
}
