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
    public partial class Form4 : Form
    {
        public NetworkWatcher networkwatcher;
        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks;

        public Controller controller;
        public bool uiclose;
        public UIInstructionEventHandler OnUIInstructionEvent;
        public object Form3;
        public object eventobj;
        public object _ctrl;
        public object Robot_PC_Interface_Panel_Template_New;
        internal static Form4 form4;

        public Form4()
        {
            InitializeComponent();
            this.controller = Form1.form1.controller;
            ListViewItem item1 = null;
            {
                IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
                SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);

                IOFilterTypes mSigFilter = IOFilterTypes.Digital | IOFilterTypes.Output;
                SignalCollection signals_o = this.controller.IOSystem.GetSignals(mSigFilter);
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;


                }

                foreach (Signal Signal1 in signals_o)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

                {
                    Signal sig7 = controller.IOSystem.GetSignal("PalletSensor");
                    DigitalSignal digitalSig = (DigitalSignal)sig7; int val1 = digitalSig.Get();
                    this.textBox2.Text = val1.ToString();
                    sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    // userControl11.controller = this.controller;
                    //   userControl21.controller = this.controller;
                    //  userControl31.controller = this.controller;
                }

                {

                    Signal sig8 = controller.IOSystem.GetSignal("Air_On");
                    DigitalSignal digitalSig = (DigitalSignal)sig8; int val2 = digitalSig.Get();
                    this.textBox7.Text = val2.ToString();
                    sig8.Changed += new EventHandler<SignalChangedEventArgs>(sig8_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }


                {
                    Signal sig9 = controller.IOSystem.GetSignal("PLC_Comms");
                    DigitalSignal digitalSig = (DigitalSignal)sig9; int val3 = digitalSig.Get();
                    this.textBox3.Text = val3.ToString();
                    sig9.Changed += new EventHandler<SignalChangedEventArgs>(sig9_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }


                {
                    Signal sig10 = controller.IOSystem.GetSignal("ProductinPlace");
                    DigitalSignal digitalSig = (DigitalSignal)sig10; int val4 = digitalSig.Get();
                    this.textBox4.Text = val4.ToString();
                    sig10.Changed += new EventHandler<SignalChangedEventArgs>(sig10_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig11 = controller.IOSystem.GetSignal("Attach2");
                    DigitalSignal digitalSig = (DigitalSignal)sig11; int val5 = digitalSig.Get();
                    this.textBox5.Text = val5.ToString();
                    sig11.Changed += new EventHandler<SignalChangedEventArgs>(sig11_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig12 = controller.IOSystem.GetSignal("Motors_ON");
                    DigitalSignal digitalSig = (DigitalSignal)sig12; int val6 = digitalSig.Get();
                    this.textBox6.Text = val6.ToString();
                    sig12.Changed += new EventHandler<SignalChangedEventArgs>(sig12_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig13 = controller.IOSystem.GetSignal("Motors_Off");
                    DigitalSignal digitalSig = (DigitalSignal)sig13; int val7 = digitalSig.Get();
                    this.textBox1.Text = val7.ToString();
                    sig13.Changed += new EventHandler<SignalChangedEventArgs>(sig13_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig14 = controller.IOSystem.GetSignal("Remove_Pallet");
                    DigitalSignal digitalSig = (DigitalSignal)sig14; int val8 = digitalSig.Get();
                    this.textBox8.Text = val8.ToString();
                    sig14.Changed += new EventHandler<SignalChangedEventArgs>(sig14_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig15 = controller.IOSystem.GetSignal("PalletFull");
                    DigitalSignal digitalSig = (DigitalSignal)sig15; int val9 = digitalSig.Get();
                    this.textBox9.Text = val9.ToString();
                    sig15.Changed += new EventHandler<SignalChangedEventArgs>(sig15_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig16 = controller.IOSystem.GetSignal("Cycle_ON");
                    DigitalSignal digitalSig = (DigitalSignal)sig16; int val10 = digitalSig.Get();
                    this.textBox10.Text = val10.ToString();
                    sig16.Changed += new EventHandler<SignalChangedEventArgs>(sig16_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig17 = controller.IOSystem.GetSignal("RobHome");
                    DigitalSignal digitalSig = (DigitalSignal)sig17; int val11 = digitalSig.Get();
                    this.textBox11.Text = val11.ToString();
                    sig17.Changed += new EventHandler<SignalChangedEventArgs>(sig17_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig18= controller.IOSystem.GetSignal("Air_Fault");
                    DigitalSignal digitalSig = (DigitalSignal)sig18; int val12 = digitalSig.Get();
                    this.textBox12.Text = val12.ToString();
                    sig18.Changed += new EventHandler<SignalChangedEventArgs>(sig18_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig19 = controller.IOSystem.GetSignal("Execution_Error");
                    DigitalSignal digitalSig = (DigitalSignal)sig19; int val13 = digitalSig.Get();
                    this.textBox13.Text = val13.ToString();
                    sig19.Changed += new EventHandler<SignalChangedEventArgs>(sig19_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig20 = controller.IOSystem.GetSignal("Emergency_Stop");
                    DigitalSignal digitalSig = (DigitalSignal)sig20; int val14 = digitalSig.Get();
                    this.textBox14.Text = val14.ToString();
                    sig20.Changed += new EventHandler<SignalChangedEventArgs>(sig20_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig21 = controller.IOSystem.GetSignal("Load_Cartons");
                    DigitalSignal digitalSig = (DigitalSignal)sig21; int val15 = digitalSig.Get();
                    this.textBox15.Text = val15.ToString();
                    sig21.Changed += new EventHandler<SignalChangedEventArgs>(sig21_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig22 = controller.IOSystem.GetSignal("Load_Pallets");
                    DigitalSignal digitalSig = (DigitalSignal)sig22; int val16 = digitalSig.Get();
                    this.textBox16.Text = val16.ToString();
                    sig22.Changed += new EventHandler<SignalChangedEventArgs>(sig22_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig23 = controller.IOSystem.GetSignal("Change_Speed");
                    DigitalSignal digitalSig = (DigitalSignal)sig23; int val17 = digitalSig.Get();
                    this.textBox17.Text = val17.ToString();
                    sig23.Changed += new EventHandler<SignalChangedEventArgs>(sig23_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

                {
                    Signal sig24 = controller.IOSystem.GetSignal("EndCycle");
                    DigitalSignal digitalSig = (DigitalSignal)sig24; int val18 = digitalSig.Get();
                    this.textBox18.Text = val18.ToString();
                    sig24.Changed += new EventHandler<SignalChangedEventArgs>(sig24_Changed);
                    //  if (val == 1) { this.checkBox5.Checked = true; }
                    // if (val == 0) { this.checkBox5.Checked = false; }
                    //  sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);

                }

              


            }
        }


        public void sig7_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue), new object[] { this, e });

        }

        public void sig8_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI1), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue1), new object[] { this, e });

        }

        public void sig9_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI2), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue2), new object[] { this, e });

        }

        public void sig10_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI3), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue3), new object[] { this, e });

        }

        public void sig11_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI4), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue4), new object[] { this, e });

        }

        public void sig12_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI5), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue5), new object[] { this, e });

        }

        public void sig13_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI6), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue6), new object[] { this, e });

        }

        public void sig14_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI7), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue7), new object[] { this, e });

        }

        public void sig15_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI8), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue8), new object[] { this, e });

        }

        public void sig16_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI9), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue9), new object[] { this, e });

        }

        public void sig17_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI10), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue10), new object[] { this, e });

        }

        public void sig18_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI11), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue11), new object[] { this, e });

        }

        public void sig19_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI12), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue12), new object[] { this, e });

        }

        public void sig20_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI13), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue13), new object[] { this, e });

        }

        public void sig21_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI14), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue14), new object[] { this, e });

        }

        public void sig22_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI15), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue15), new object[] { this, e });

        }

        public void sig23_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI16), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue16), new object[] { this, e });

        }

        public void sig24_Changed(object sender, SignalChangedEventArgs e)
        {
            // SignalState sig4 = e.NewSignalState;
            // float val = sig4.Value;
            //  if (val == 1) { this.checkBox2.Checked = true; }
            //   if (val == 0) { this.checkBox2.Checked = false; }

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI17), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue17), new object[] { this, e });

        }



        //private void sig7_Changed(object sender, SignalChangedEventArgs e)
        //{
        //    SignalState sig7 = e.NewSignalState;
        //    float val = sig7.Value;
        //    if (val == 1) { this.checkBox2.Checked = true; }
        //    if (val == 0) { this.checkBox2.Checked = false; }
        //}

        //  void HandleFoundEvent(object sender, NetworkWatcherEventArgs e)

        //  {
        //      EventHandler<NetworkWatcherEventArgs> AddControllerToListView = null;
        //      this.Invoke(new EventHandler<NetworkWatcherEventArgs>(AddControllerToListView), new object[] { this, e });

        //  }




        //  private void sig7_Changed (object sender, SignalChangedEventArgs e)
        //  {
        //  this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI), new object[] { this, e });
        //  this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue), new object[] { this, e });
        //  }



        //private void Signal1_Changed(object sender, SignalChangedEventArgs e)
        //{
        //    this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue), new object[] { this, e });

        //}

        public void UpdateUI(object sender, SignalChangedEventArgs e)
        {

            SignalState sig7 = e.NewSignalState;
            float val1 = sig7.Value;
            {
                this.textBox2.Text = val1.ToString();

            }
        }

        public void UpdateUI1(object sender, SignalChangedEventArgs e)
        {
            SignalState sig8 = e.NewSignalState;
            float val2 = sig8.Value;
            {
                this.textBox7.Text = val2.ToString();

            }
        }

        public void UpdateUI2(object sender, SignalChangedEventArgs e)
        {
            SignalState sig9 = e.NewSignalState;
            float val3 = sig9.Value;
            {
                this.textBox3.Text = val3.ToString();

            }
        }


        public void UpdateUI3(object sender, SignalChangedEventArgs e)
        {
            SignalState sig10 = e.NewSignalState;
            float val4 = sig10.Value;
            {
                this.textBox4.Text = val4.ToString();

            }
        }


        public void UpdateUI4(object sender, SignalChangedEventArgs e)
        {
            SignalState sig11 = e.NewSignalState;
            float val5 = sig11.Value;
            {
                this.textBox5.Text = val5.ToString();

            }
        }

        public void UpdateUI5(object sender, SignalChangedEventArgs e)
        {
            SignalState sig12 = e.NewSignalState;
            float val6 = sig12.Value;
            {
                this.textBox6.Text = val6.ToString();

            }
        }


        public void UpdateUI6(object sender, SignalChangedEventArgs e)
        {
            SignalState sig13 = e.NewSignalState;
            float val7 = sig13.Value;
            {
                this.textBox1.Text = val7.ToString();

            }
        }


        public void UpdateUI7(object sender, SignalChangedEventArgs e)
        {
            SignalState sig14 = e.NewSignalState;
            float val8 = sig14.Value;
            {
                this.textBox8.Text = val8.ToString();

            }
        }

        public void UpdateUI8(object sender, SignalChangedEventArgs e)
        {
            SignalState sig15 = e.NewSignalState;
            float val9 = sig15.Value;
            {
                this.textBox9.Text = val9.ToString();

            }
        }


        public void UpdateUI9(object sender, SignalChangedEventArgs e)
        {
            SignalState sig16 = e.NewSignalState;
            float val10 = sig16.Value;
            {
                this.textBox10.Text = val10.ToString();

            }
        }

        public void UpdateUI10(object sender, SignalChangedEventArgs e)
        {
            SignalState sig17 = e.NewSignalState;
            float val11 = sig17.Value;
            {
                this.textBox11.Text = val11.ToString();

            }
        }


        public void UpdateUI11(object sender, SignalChangedEventArgs e)
        {
            SignalState sig18 = e.NewSignalState;
            float val12 = sig18.Value;
            {
                this.textBox12.Text = val12.ToString();

            }
        }


        public void UpdateUI12(object sender, SignalChangedEventArgs e)
        {
            SignalState sig19 = e.NewSignalState;
            float val13 = sig19.Value;
            {
                this.textBox13.Text = val13.ToString();

            }
        }

        public void UpdateUI13(object sender, SignalChangedEventArgs e)
        {
            SignalState sig20 = e.NewSignalState;
            float val14 = sig20.Value;
            {
                this.textBox14.Text = val14.ToString();

            }
        }


        public void UpdateUI14(object sender, SignalChangedEventArgs e)
        {
            SignalState sig21 = e.NewSignalState;
            float val15 = sig21.Value;
            {
                this.textBox15.Text = val15.ToString();

            }
        }

        public void UpdateUI15(object sender, SignalChangedEventArgs e)
        {
            SignalState sig22 = e.NewSignalState;
            float val16 = sig22.Value;
            {
                this.textBox16.Text = val16.ToString();

            }
        }

        public void UpdateUI16(object sender, SignalChangedEventArgs e)
        {
            SignalState sig23 = e.NewSignalState;
            float val17 = sig23.Value;
            {
                this.textBox17.Text = val17.ToString();

            }
        }

        public void UpdateUI17(object sender, SignalChangedEventArgs e)
        {
            SignalState sig24 = e.NewSignalState;
            float val18 = sig24.Value;
            {
                this.textBox18.Text = val18.ToString();

            }
        }


        public void UpdateListViewValue(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }


        public void UpdateListViewValue1(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }


        public void UpdateListViewValue2(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }



        public void UpdateListViewValue3(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }



        public void UpdateListViewValue4(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }



        public void UpdateListViewValue5(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }

        
        }


        public void UpdateListViewValue6(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue7(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue8(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue9(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue10(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue11(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue12(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue13(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue14(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue15(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue16(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        public void UpdateListViewValue17(object sender, SignalChangedEventArgs e)
        {
            ListViewItem item1 = null;

            IOFilterTypes aSigFilter = IOFilterTypes.Digital | IOFilterTypes.Input;
            SignalCollection signals = this.controller.IOSystem.GetSignals(aSigFilter);


            {
                //  SignalState Signal = e.NewSignalState;
                foreach (Signal Signal1 in signals)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                    this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

    }
}