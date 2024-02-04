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
    public partial class Form3 : Form
    {

        public NetworkWatcher networkwatcher;
        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks;

        public Controller controller;
        public bool uiclose;
        public UIInstructionEventHandler OnUIInstructionEvent;
        //public object Form3;
        public object eventobj;
        public object _ctrl;
        public object Robot_PC_Interface_Panel_Template_New;
        internal static Form2 form2;
        public Form3()
        {
            InitializeComponent();
            this.controller = Form1.form1.controller;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (controller.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    RapidData product = controller.Rapid.GetRapidData("T_ROB1", "Module1", "product_type");
                    ABB.Robotics.Controllers.RapidDomain.Num products;
                    Num ChangedValue1 = new Num();
                    ChangedValue1.Value = 1;
                    {
                        product.Value = ChangedValue1;
                        //   m.Dispose();
                    }
                    // ABB.Robotics.Controllers.RapidDomain.String rapidString;
                    // rapidString.FillFromString(textBox1.Text);       
                    // rd.Value = rapidString;
                }
                // this.controller.Dispose();
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (controller.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    RapidData product = controller.Rapid.GetRapidData("T_ROB1", "Module1", "product_type");
                    ABB.Robotics.Controllers.RapidDomain.Num products;
                    Num ChangedValue2 = new Num();
                    ChangedValue2.Value = 2;
                    {
                        product.Value = ChangedValue2;
                        //  m.Dispose();

                    }
                    //  this.controller.Dispose();
                    // ABB.Robotics.Controllers.RapidDomain.String rapidString;
                    // rapidString.FillFromString(textBox1.Text);       
                    // rd.Value = rapidString;
                }
            }
        }



        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {

                        RapidData rd = controller.Rapid.GetRapidData("T_ROB1", "Module1", "VelocityPercentage");
                        Num ChangedValue = new Num();
                        //   Num Value ;
                        string str = textBox1.Text;
                        ChangedValue.FillFromString2(str);
                        {

                            rd.Value = ChangedValue;
                            // m.Dispose();

                        }
                        //  this.controller.Dispose();
                        // ABB.Robotics.Controllers.RapidDomain.String rapidString;
                        // rapidString.FillFromString(textBox1.Text);       
                        // rd.Value = rapidString;
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }

            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                Signal sig11 = this.controller.IOSystem.GetSignal("Change_Speed");
                DigitalSignal
                digitalSig11 = (DigitalSignal)sig11; int val7 = digitalSig11.Get();
                {
                    digitalSig11.Reset();


                }
            }

        }

        private void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {

                        RapidData rd1 = controller.Rapid.GetRapidData("T_ROB1", "Module1", "Layers3");
                        Num ChangedValue1 = new Num();
                        //   Num Value ;
                        string str1 = textBox9.Text;
                        ChangedValue1.FillFromString2(str1);
                        {

                            rd1.Value = ChangedValue1;
                            //m.Dispose();

                        }
                        // ABB.Robotics.Controllers.RapidDomain.String rapidString;
                        // rapidString.FillFromString(textBox1.Text);       
                        // rd.Value = rapidString;

                    }
                    // this.controller.Dispose();

                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                Signal sig8 = this.controller.IOSystem.GetSignal("Change_Speed");
                DigitalSignal
                digitalSig8 = (DigitalSignal)sig8; int val6 = digitalSig8.Get();
                {
                    digitalSig8.Set();


                }
            }

        }
    }
}
