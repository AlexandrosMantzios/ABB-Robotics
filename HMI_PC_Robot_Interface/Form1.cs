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
    public partial class Form1 : Form
    {
        public NetworkWatcher networkwatcher;
        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks;
       // public ABB.Robotics.Controllers.Controller controller;
        public Controller controller;
      //  public Mess
        public bool uiclose;
        public UIInstructionEventHandler OnUIInstructionEvent;
        public object Form3;
        public object eventobj;
        public object _ctrl;
        internal static Form2 form2;
        internal static Form1 form1;
        internal static Form3 form3;
        internal static Form4 form4;
        internal static Form5 form5;



        public Form1()
        {   // Initialize screen 
            InitializeComponent();
            form1 = this;
           
        }

    
        public NetworkScanner scanner { get; private set; }

        // LOAD FORM TO FIND CONTROLLER

        public void Form1_Load(object sender, EventArgs e)
        {
            this.scanner = new NetworkScanner();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            ListViewItem item = null;
            foreach (ControllerInfo controllerInfo in controllers)
            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.Id);
                item.SubItems.Add(controllerInfo.Availability.ToString());
                item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                item.SubItems.Add(controllerInfo.SystemName);
                item.SubItems.Add(controllerInfo.Version.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                this.listView1.Items.Add(item);
                item.Tag = controllerInfo;
            }
            this.networkwatcher = new NetworkWatcher(scanner.Controllers);
            this.networkwatcher.Found += new EventHandler<NetworkWatcherEventArgs>(HandleFoundEvent);
            this.networkwatcher.Lost += new EventHandler<NetworkWatcherEventArgs>(HandleLostEvent);
            this.networkwatcher.EnableRaisingEvents = true;
        }
        public void HandleLostEvent(object sender, NetworkWatcherEventArgs e)
        {
            this.Invoke(new EventHandler<NetworkWatcherEventArgs>(RemoveControllerFromListView), new Object[] { sender, e });
        }

        public void HandleFoundEvent(object sender, NetworkWatcherEventArgs e)

        {
            this.Invoke(new EventHandler<NetworkWatcherEventArgs>(AddControllerToListView), new object[] { this, e });
        }

        private void RemoveControllerFromListView(object sender, NetworkWatcherEventArgs e)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                if ((ControllerInfo)item.Tag == e.Controller)
                {
                    this.listView1.Items.Remove(item);
                    break;
                }
            }
        }

        private void AddControllerToListView(object sender, NetworkWatcherEventArgs e)
        {
            ControllerInfo controllerInfo = e.Controller;
            ListViewItem item = new ListViewItem(controllerInfo.IPAddress.ToString());
            item.SubItems.Add(controllerInfo.Id);
            item.SubItems.Add(controllerInfo.Availability.ToString());
            item.SubItems.Add(controllerInfo.IsVirtual.ToString());
            item.SubItems.Add(controllerInfo.SystemName);
            item.SubItems.Add(controllerInfo.Version.ToString());
            item.SubItems.Add(controllerInfo.ControllerName);
            this.listView1.Items.Add(item);
            item.Tag = controllerInfo;
        }

        // SELECT CONTROLLER AND SHOW SIGNALS
        public void ListView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            if (item.Tag != null)
            {
                ControllerInfo controllerInfo = (ControllerInfo)item.Tag;
                if (controllerInfo.Availability == ABB.Robotics.Controllers.Availability.Available)
                {                    
                    if (controller != null)
                    {
                        this.controller.Logoff();
                        this.controller.Dispose();
                        this.controller = null;
                    }
                    this.controller = ControllerFactory.CreateFrom(controllerInfo);

                    MessageBox.Show("Is Available");
                    this.controller.Logon(UserInfo.DefaultUser);
                }
                else
                {
                    MessageBox.Show("Selected controller not available");
                }
            }
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
                  //  this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

                foreach (Signal Signal1 in signals_o)
                {
                    item1 = new ListViewItem(Signal1.Name);
                    item1.SubItems.Add(Signal1.Type.ToString());
                    item1.SubItems.Add(Signal1.Value.ToString());
                  //  this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }

            {
                Signal sig7 = controller.IOSystem.GetSignal("PalletSensor");
                DigitalSignal digitalSig = (DigitalSignal)sig7; int val1 = digitalSig.Get();
                sig7.Changed += new EventHandler<SignalChangedEventArgs>(sig7_Changed);
            }

            {

                Signal sig8 = controller.IOSystem.GetSignal("Air_On");
                DigitalSignal digitalSig = (DigitalSignal)sig8; int val2 = digitalSig.Get();
                sig8.Changed += new EventHandler<SignalChangedEventArgs>(sig8_Changed);
            }

            {
                Signal sig9 = controller.IOSystem.GetSignal("PLC_Comms");
                DigitalSignal digitalSig = (DigitalSignal)sig9; int val3 = digitalSig.Get();
                sig9.Changed += new EventHandler<SignalChangedEventArgs>(sig9_Changed);
            }

            {
                Signal sig10 = controller.IOSystem.GetSignal("ProductinPlace");
                DigitalSignal digitalSig = (DigitalSignal)sig10; int val4 = digitalSig.Get();
                sig10.Changed += new EventHandler<SignalChangedEventArgs>(sig10_Changed);

            }

            {
                Signal sig11 = controller.IOSystem.GetSignal("Attach2");
                DigitalSignal digitalSig = (DigitalSignal)sig11; int val5 = digitalSig.Get();
                sig11.Changed += new EventHandler<SignalChangedEventArgs>(sig11_Changed);
            }

            {
                Signal sig12 = controller.IOSystem.GetSignal("Motors_ON");
                DigitalSignal digitalSig = (DigitalSignal)sig12; int val6 = digitalSig.Get();
                sig12.Changed += new EventHandler<SignalChangedEventArgs>(sig12_Changed);
            }



        }

        public void sig7_Changed(object sender, SignalChangedEventArgs e)
        {
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListValueControllers), new object[] { this, e });
        }

        public void sig8_Changed(object sender, SignalChangedEventArgs e)
        {
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI1), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue1), new object[] { this, e });
        }

        public void sig9_Changed(object sender, SignalChangedEventArgs e)
        {

            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI2), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue2), new object[] { this, e });

        }

        public void sig10_Changed(object sender, SignalChangedEventArgs e)
        {
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI3), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue3), new object[] { this, e });
        }

        public void sig11_Changed(object sender, SignalChangedEventArgs e)
        {
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI4), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue4), new object[] { this, e });
        }

        public void sig12_Changed(object sender, SignalChangedEventArgs e)
        {
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateUI5), new object[] { this, e });
            this.Invoke(new EventHandler<SignalChangedEventArgs>(UpdateListViewValue5), new object[] { this, e });
        }

        public void UpdateUI(object sender, SignalChangedEventArgs e)
        {
            SignalState sig7 = e.NewSignalState;
            float val1 = sig7.Value;   
        }

        public void UpdateUI1(object sender, SignalChangedEventArgs e)
        {
            SignalState sig8 = e.NewSignalState;
            float val2 = sig8.Value;  
        }

        public void UpdateUI2(object sender, SignalChangedEventArgs e)
        {
            SignalState sig9 = e.NewSignalState;
            float val3 = sig9.Value;   
        }


        public void UpdateUI3(object sender, SignalChangedEventArgs e)
        {
            SignalState sig10 = e.NewSignalState;
            float val4 = sig10.Value;
        }


        public void UpdateUI4(object sender, SignalChangedEventArgs e)
        {
            SignalState sig11 = e.NewSignalState;
            float val5 = sig11.Value;
        }

        public void UpdateUI5(object sender, SignalChangedEventArgs e)
        {
            SignalState sig12 = e.NewSignalState;
            float val6 = sig12.Value;
        }

        public void UpdateListValueControllers(object sender, SignalChangedEventArgs e)
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
                 //   this.listView2.Items.Add(item1);
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
                 //   this.listView2.Items.Add(item1);
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
                  //  this.listView2.Items.Add(item1);
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
                 //   this.listView2.Items.Add(item1);
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
                 //   this.listView2.Items.Add(item1);
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
                 //   this.listView2.Items.Add(item1);
                    item1.Tag = Signal1;
                }

            }


        }

        // MOTORS ON 


       

     

        public void Button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    
                    {
                        //Perform Operation
                        this.controller.Rapid.Start(true);
                        this.controller.Rapid.UIInstruction.UIInstructionEvent += new UIInstructionEventHandler(OnUIInstructionEvent);
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




        }

        public void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller.Rapid))

                    {
                        //Perform Operation
                        this.controller.Rapid.Start(true);
                        this.controller.Rapid.UIInstruction.UIInstructionEvent += new UIInstructionEventHandler(OnUIInstructionEvent);
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

            Signal sig1 = this.controller.IOSystem.GetSignal("Start_Program_DO");
            DigitalSignal
                digitalSig1 = (DigitalSignal)sig1; int val1 = digitalSig1.Get();
            {

                digitalSig1.Set();

            }

            Signal sig3 = this.controller.IOSystem.GetSignal("Stop_Program_DO");
            DigitalSignal
                digitalSig3 = (DigitalSignal)sig3; int val = digitalSig3.Get();
            {
                digitalSig3.Reset();
            }
            // if (controller.OperatingMode == ControllerOperatingMode.Auto)
            //  {
            //   using (Mastership m = Mastership.Request(controller.Rapid))
            //  {        

        }




        public void Button2_Click(object sender, EventArgs e)
        {
            if (controller.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    Signal sig3 = this.controller.IOSystem.GetSignal("Stop_Program_DO");
                    DigitalSignal
                        digitalSig3 = (DigitalSignal)sig3; int val = digitalSig3.Get();
                    {
                        digitalSig3.Pulse();
                        //digitalSig3.Set();
                     //   m.Dispose();

                    }
                  //  this.controller.Dispose();
                }
            }
        }

        public void Button4_Click(object sender, EventArgs e)
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

                    }

                }
            }
        }

        public void Button5_Click(object sender, EventArgs e)
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

                    }

                }
            }
        }



        public void Button7_Click(object sender, EventArgs e)
        {

            if (controller.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    Signal sig2 = this.controller.IOSystem.GetSignal("PPMain");
                    DigitalSignal
                        digitalSig2 = (DigitalSignal)sig2; int val3 = digitalSig2.Get();
                    {
                        digitalSig2.Pulse();
                        

                    }
                    

                }
                // this.controller.Dispose();
                //m.Dispose();
            }
            
        }

       




        public void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller.Rapid))

                    {
                        //Perform Operation
                        this.controller.Rapid.Start(true);
                        this.controller.Rapid.UIInstruction.UIInstructionEvent += new UIInstructionEventHandler(OnUIInstructionEvent);
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

            Signal sig1 = this.controller.IOSystem.GetSignal("Start_at_Main_DO");
            DigitalSignal
                digitalSig1 = (DigitalSignal)sig1; int val1 = digitalSig1.Get();
            {

                digitalSig1.Set();

            }

            Signal sig3 = this.controller.IOSystem.GetSignal("Stop_Program_DO");
            DigitalSignal
                digitalSig3 = (DigitalSignal)sig3; int val = digitalSig3.Get();
            {
                digitalSig3.Reset();
            }
            // if (controller.OperatingMode == ControllerOperatingMode.Auto)          
        }



        public void Button14_Click(object sender, EventArgs e)
        {
     
            Form2 f = new Form2();
            f.Show();
            //form1.Hide();
          //  this.controller = f.controller;
        }

        public void Button13_Click(object sender, EventArgs e)
        {
            Form3 f1 = new Form3();
            f1.Show();
        }

        public void Button15_Click(object sender, EventArgs e)
        {
            {
                Form4 f2 = new Form4();
                f2.Show();

            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Form5 f3 = new Form5();
            f3.Show();

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            this.scanner = new NetworkScanner();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            ListViewItem item = null;
            foreach (ControllerInfo controllerInfo in controllers)



            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.Id);
                item.SubItems.Add(controllerInfo.Availability.ToString());
                item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                item.SubItems.Add(controllerInfo.SystemName);
                item.SubItems.Add(controllerInfo.Version.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                this.listView1.Items.Add(item);
                item.Tag = controllerInfo;
            }



            this.networkwatcher = new NetworkWatcher(scanner.Controllers);
            this.networkwatcher.Found += new EventHandler<NetworkWatcherEventArgs>(HandleFoundEvent);
            this.networkwatcher.Lost += new EventHandler<NetworkWatcherEventArgs>(HandleLostEvent);
            this.networkwatcher.EnableRaisingEvents = true;
        }


    }
    }








}

