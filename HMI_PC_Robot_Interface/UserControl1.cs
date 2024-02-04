using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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


//namespace Robot_PC_Interface_Panel_Template_New
//{


//    public partial class UserControl1 : UserControl
//    {

//        public NetworkWatcher networkwatcher;
//        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks;
      
//        public Controller controller1;
//        public bool uiclose;
//        public UIInstructionEventHandler OnUIInstructionEvent;
//        public object Form3;
//        public object eventobj;
//        public object _ctrl;
//        public object Robot_PC_Interface_Panel_Template_New;
//        public Robot_PC_Interface_Panel_Template_New.Form1 form1;
        

       
     

       
        
        

//        public UserControl1( )
//        {
//            InitializeComponent();
//            // this.controller = ControllerFactory.CreateFrom(controllerInfo);

          


//        }


//        public void CheckBox1_Click(object sender, EventArgs e)
//        {
//             //controller1 = form1.controller;
//            form1.controller.Logon(UserInfo.DefaultUser);
//            Signal sig = form1.controller.IOSystem.GetSignal("Motors_ON");
//            DigitalSignal
//                digitalSig = (DigitalSignal)sig; int val = digitalSig.Get();
//            {
//                if (this.checkBox1.Checked)
//                {
//                    digitalSig.Set();
//                }
//                else
//                {
//                    digitalSig.Reset();
//                }
//            }

//        }

//        private void CheckBox2_Click(object sender, EventArgs e)
//        {
            
//            Signal sig = this.controller1.IOSystem.GetSignal("Motors_ON");
//            DigitalSignal
//                digitalSig = (DigitalSignal)sig; int val = digitalSig.Get();
//            {
//                if (this.checkBox1.Checked)
//                {
//                    digitalSig.Set();
//                }
//                else
//                {
//                    digitalSig.Reset();
//                }
//            }
//        }
//    }
//}
