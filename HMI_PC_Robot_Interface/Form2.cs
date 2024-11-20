using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


namespace Robot_PC_Interface_Panel_Template_New
{
    public partial class Form2 : Form
    {
        public NetworkWatcher networkwatcher;
        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks;

        public Controller controller1;
        public bool uiclose;
        public UIInstructionEventHandler OnUIInstructionEvent;
        public object Form3;
        public object eventobj;
        public object _ctrl;
        public object Robot_PC_Interface_Panel_Template_New;
        internal static Form2 form2;
        // public Robot_PC_Interface_Panel_Template_New.Form1 form1;

        public Form2()
        {
            InitializeComponent();
            this.controller1 = Form1.form1.controller;
        }



        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Signal sig = this.controller1.IOSystem.GetSignal("Motors_ON");
            DigitalSignal 
                digitalSig = (DigitalSignal)sig; int val = digitalSig.Get();
            {
                if (this.checkBox2.Checked)
                {
                    digitalSig.Set();
                }
                else
                {
                    digitalSig.Reset();
                }
            }
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            Signal sig = this.controller1.IOSystem.GetSignal("Motors_Off");
            DigitalSignal
                digitalSig = (DigitalSignal)sig; int val = digitalSig.Get();
            {
                if (this.checkBox6.Checked)
                {
                    digitalSig.Set();
                }
                else
                {
                    digitalSig.Reset();
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (controller1.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller1.Rapid))
                {
                    Signal sig2 = this.controller1.IOSystem.GetSignal("PPMain");
                    DigitalSignal
                        digitalSig2 = (DigitalSignal)sig2; int val3 = digitalSig2.Get();
                    {
                        digitalSig2.Pulse();
                    }
                }
            }
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (controller1.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller1.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller1.Rapid))
                    {
                        //Perform Operation
                        this.controller1.Rapid.Start(true);
                        this.controller1.Rapid.UIInstruction.UIInstructionEvent += new UIInstructionEventHandler(OnUIInstructionEvent)
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (controller1.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller1.Rapid))
                {
                    Signal sig4 = this.controller1.IOSystem.GetSignal("Reset_Emergency_DO");
                    DigitalSignal
                        digitalSig4 = (DigitalSignal)sig4; int val = digitalSig4.Get();
                    {
                        digitalSig4.Pulse();
                    }
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (controller1.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller1.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller1.Rapid))

                    {
                        //Perform Operation
                        this.controller1.Rapid.Start(true);
                        this.controller1.Rapid.UIInstruction.UIInstructionEvent += new UIInstructionEventHandler(OnUIInstructionEvent);

                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }




            using (Mastership m = Mastership.Request(controller1.Rapid))
            {
                Signal sig2 = this.controller1.IOSystem.GetSignal("PPMain");
                DigitalSignal
                digitalSig2 = (DigitalSignal)sig2; int val3 = digitalSig2.Get();
                {
                    digitalSig2.Pulse();


                }
                Signal sig1 = this.controller1.IOSystem.GetSignal("Start_Program_DO");
                DigitalSignal
                    digitalSig1 = (DigitalSignal)sig1; int val1 = digitalSig1.Get();
                {
                    digitalSig1.Set();
                    Thread.Sleep(2);
                }
            }   

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (controller1.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller1.Rapid))
                {
                    Signal sig3 = this.controller1.IOSystem.GetSignal("Stop_Program_DO");
                    DigitalSignal
                        digitalSig3 = (DigitalSignal)sig3; int val = digitalSig3.Get();
                    {
                        digitalSig3.Pulse();

                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (controller1.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller1.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller1.Rapid))

                    {
                        //Perform Operation
                        this.controller1.Rapid.Start(true);
                        this.controller1.Rapid.UIInstruction.UIInstructionEvent += new UIInstructionEventHandler(OnUIInstructionEvent);
                        //m.Dispose();

                    }

                    //  using (Mastership m = Mastership.Release(controller.Rapid)
                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
            using (Mastership m = Mastership.Request(controller1.Rapid))
            {
                Signal sig1 = this.controller1.IOSystem.GetSignal("Start_Program_DO");
                DigitalSignal
                    digitalSig1 = (DigitalSignal)sig1; int val1 = digitalSig1.Get();
                {

                    digitalSig1.Pulse();

                }
            }

            {
                Signal sig3 = this.controller1.IOSystem.GetSignal("Stop_Program_DO");
                DigitalSignal
                    digitalSig3 = (DigitalSignal)sig3; int val = digitalSig3.Get();
                {
                    digitalSig3.Reset();
                }
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            if (controller1.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller1.Rapid))
                {
                    Signal sig10 = this.controller1.IOSystem.GetSignal("Reset_Exec_Error_DO");
                    DigitalSignal
                        digitalSig10 = (DigitalSignal)sig10; int val10 = digitalSig10.Get();
                    {
                        digitalSig10.Pulse();
                        
                        //digitalSig3.Set();
                        //   m.Dispose();

                    }
                    //  this.controller.Dispose();
                }
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (controller1.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller1.Rapid))
                {
                    Signal sig5 = this.controller1.IOSystem.GetSignal("Homing_DO");
                    DigitalSignal
                        digitalSig5 = (DigitalSignal)sig5; int val = digitalSig5.Get();
                    {
                        digitalSig5.Pulse();

                        //digitalSig3.Set();
                        //   m.Dispose();

                    }


                    Signal sig6 = this.controller1.IOSystem.GetSignal("Start_Home_DO");
                    DigitalSignal
                        digitalSig6 = (DigitalSignal)sig6; int val1 = digitalSig6.Get();
                    {
                        digitalSig6.Set();

                        //digitalSig3.Set();
                        //   m.Dispose();

                    }
                    //  this.controller.Dispose();
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (controller1.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller1.Rapid))
                {
                    Signal sig20 = this.controller1.IOSystem.GetSignal("Reset_Exec_Error_DO");
                    DigitalSignal
                        digitalSig20 = (DigitalSignal)sig20; int val20 = digitalSig20.Get();
                    {
                        digitalSig20.Pulse();

                        //digitalSig3.Set();
                        //   m.Dispose();

                    }
                    //  this.controller.Dispose();
                }
            }
        }
    }
    
}
