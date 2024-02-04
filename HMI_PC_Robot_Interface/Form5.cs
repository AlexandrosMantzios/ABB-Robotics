using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.IOSystemDomain;
using ABB.Robotics.Controllers.Messaging;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.EventLogDomain;
using String = ABB.Robotics.Controllers.RapidDomain.String;

namespace Robot_PC_Interface_Panel_Template_New
{
    public partial class Form5 : Form
    {
        public NetworkWatcher networkwatcher;
        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks;

        public Controller controller;
        public EventLogMessage eventlog;
      //  public EventLogMessage log;
        public EventLog log;
        public bool uiclose;
        public UIInstructionEventHandler OnUIInstructionEvent;
        public object Form3;
        public object eventobj;
        public object _ctrl;
        public object Robot_PC_Interface_Panel_Template_New;
        internal static Form5 form5;


        public Form5()
        {
            InitializeComponent();
            this.controller = Form1.form1.controller;
            this.controller.EventLog.MessageWritten += new EventHandler<ABB.Robotics.Controllers.EventLogDomain.MessageWrittenEventArgs>(HandleFoundEvent);
         
        }


        public void HandleFoundEvent(object sender, MessageWrittenEventArgs e)

        {
            this.Invoke(new EventHandler<MessageWrittenEventArgs>(AddEvent), new object[] { this, e });
        }


        public void AddEvent(object sender, EventArgs e)
        {
           EventLogCategory[] categories = controller.EventLog.GetCategories();
            listView1.Items.Clear();
            
         //  EventLogMessage[] messages = controller.EventLog.;
    
               

                ListViewItem item = null;
            //List<string> item = new List<string>();

           foreach (EventLogCategory cat in categories)
            {
                foreach (EventLogMessage msg in cat.Messages)
                {
                    if  (msg.Title.Contains("ERR"))
                    {
                
                        item = new ListViewItem(msg.Title);
                       // item.SubItems.Add(msg.Number.ToString());
                       //item.SubItems.Add(msg.Body);
                        item.SubItems.Add(msg.Number.ToString());
                        item.SubItems.Add(msg.Name);
                        //    item.SubItems.Add(msg.Number);
                        // item.SubItems.Add(msg.Number.ToString());
                        this.listView1.Items.Add(item);
                       // item.Tag = msg;
                        Console.WriteLine(string.Format(" Error {0} {1} {2}",msg.Number,msg.Title,msg.Body));

                       // this.listView1.item(string.Format(
                       //"Received {0} message from controller.\nTitle: {1}\nBody: {2}",
                       //msg.Type,
                       //msg.Title,
                       //msg.Body));
                    }
                    
                }
                
            }
         

        }


        //public void HandleFoundEvent(object sender, NetworkWatcherEventArgs e)

        //{
        //    // EventHandler<NetworkWatcherEventArgs> AddControllerToListView = null;
        //    this.Invoke(new EventHandler<NetworkWatcherEventArgs>(AddControllerToListView), new object[] { this, e });


        //}

        // public EventLog message { get; private set; }


        //public void Form5_Load(object sender, EventArgs e)
        //{

        //    //userControl11.Hide();
        //    // userControl11.controller1 = this.controller;
        //    // userControl11.controller1 = this.controller;
        //    //userControl21.Hide();

        //    //userControl31.Hide();

        //    this.scanner = new EventLog();
        //    this.scanner.MessageWritten();
        //    EventLogMessage events = scanner.MessageWritten;
        //    ListViewItem item = null;
        //    foreach (ControllerInfo controllerInfo in events)



        //    {
        //        item = new ListViewItem(controllerInfo.IPAddress.ToString());
        //        item.SubItems.Add(controllerInfo.Id);
        //        item.SubItems.Add(controllerInfo.Availability.ToString());
        //        item.SubItems.Add(controllerInfo.IsVirtual.ToString());
        //        item.SubItems.Add(controllerInfo.SystemName);
        //        item.SubItems.Add(controllerInfo.Version.ToString());
        //        item.SubItems.Add(controllerInfo.ControllerName);
        //        this.listView1.Items.Add(item);
        //        item.Tag = controllerInfo;
        //    }



        //    this.networkwatcher = new NetworkWatcher(scanner.Controllers);
        //    this.networkwatcher.Found += new EventHandler<NetworkWatcherEventArgs>(HandleFoundEvent);
        //    this.networkwatcher.Lost += new EventHandler<NetworkWatcherEventArgs>(HandleLostEvent);
        //    this.networkwatcher.EnableRaisingEvents = true;


        //private void EventMessageCallBack(object sender, MessageWrittenEventArgs e)
        //{
        //    EventLogMessage message = e.Message;
        //    this.log.Body.Contains(string.Format(
        //       "Received {0} message from controller.\nTitle: {1}\nBody: {2}",
        //       message.Type,
        //       message.Title,
        //       message.Body));

        //    //    //process message
        //    //}




            ////public void EventLog_MessageWritten(object sender, ABB.Robotics.Controllers.EventLogDomain.MessageWrittenEventArgs e)
            ////{
            ////    EventLogMessage msg = e.Message;
            ////    if (msg.Type == ABB.Robotics.Controllers.EventLogDomain.EventLogEntryType.Error)
            ////    {
            ////        EventLogMessage(EventArgs.Empty); // Notifies error
            ////    }
            ////    else if (msg.Type == ABB.Robotics.Controllers.EventLogDomain.EventLogEntryType.Information)
            ////    {
            ////        EventLogMessage(EventArgs.Empty); // Notifies error}
            ////    }
            ////    else if (msg.Type == ABB.Robotics.Controllers.EventLogDomain.EventLogEntryType.Warning)
            ////    {
            ////        EventLogMessage(EventArgs.Empty); // Notifies error}
            ////    }

            ////}



            ////private void EventLogMessage(EventArgs empty)
            ////{

            ////    //throw new NotImplementedException();
            ////}

            //public void EventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)

            //{
            //    EventLogCategory[] _cats = this.controller.EventLog.GetCategories();
            //    listView1.Items.Clear();
            //    ListViewItem _item = null;
            //    foreach (EventLogCategory _cat in _cats)
            //    {
            //        foreach (EventLogMessage _msg in _cat.Messages)
            //        {
            //            _item = new ListViewItem(_msg.SequenceNumber.ToString());
            //            _item.SubItems.Add(_msg.Number.ToString());
            //            _item.SubItems.Add(_msg.Title);
            //            this.listView1.Items.Add(_item);

            //        }

            //    }
            //    ////}

            //}
        }

    
   
}
