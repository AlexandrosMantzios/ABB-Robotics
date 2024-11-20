using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using System;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using System.Diagnostics;
using System.Windows.Forms;

namespace ConnectToRobotStudio
{
    public partial class Form1 : Form
    {
        private NetworkScanner scanner = null;
        private RobotClass myRobot = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //This will check for ABB controllers and list them in a combo box
            this.scanner = new NetworkScanner();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            foreach (ControllerInfo info in controllers)
            {
                comboBox1.Items.Add(info.ControllerName + " / " + info.IPAddress.ToString());
                comboBox1.SelectedIndex = 0;

            }
        }
        private void connectController()
        {
            ControllerInfoCollection controllers = scanner.Controllers;
            foreach (ControllerInfo info in controllers)
            {
                if (comboBox1.Text.Equals(info.ControllerName + " / " + info.IPAddress.ToString()))
                {
                    if (info.Availability == Availability.Available)
                    {
                        if (myRobot != null)
                        {
                            myRobot.Dispose(); // = LogOff
                            myRobot = null;
                        }
                        myRobot = new RobotClass(ControllerFactory.CreateFrom(info));
                        myRobot.StartRapidProgram();


                        break;
                    }
                }
                {
                    MessageBox.Show("Selected controller not available.");
                }
            }
            if (myRobot == null) MessageBox.Show("Selected controller not available. (comboBox String != controller info)");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectController();
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            myRobot.StartProcess();
        }
    }
}


namespace ConnectToRobotStudio
{
    class RobotClass
    {
        #region Gobal Variables

        private Controller controller = null;
        private RapidData rd_start = null;   // Variable used to pass Data to Rapid
        private RapidData rd_begin = null;   //Variable used to pass Data to Rapid  
        private const int SHUT_DOWN = -1;   // Must be the same as what is declare in ABB Rapid code
        private const int TO_WAIT = 0;     // Must be the same as what is declare in ABB Rapid code
        private const int TO_PickUp = 1; // Must be the same as what is declare in ABB Rapid code
        private Num processFlag;       // flag to start process

        #endregion


        public RobotClass(Controller controller)
        {
            this.controller = controller;
            this.controller.Logon(UserInfo.DefaultUser);
            InitDataStream();
        }
        public void InitDataStream()
        {
            // This loads the Task, ensure that the task name matches in robot studio
            ABB.Robotics.Controllers.RapidDomain.Task tRob1 = controller.Rapid.GetTask("T_ROB1");
            if (tRob1 != null)
            {

                // this line reads the variable Start on Rapid and passes the value to this app
                rd_start = tRob1.GetRapidData("MODULE_1", "Start");

                if (rd_start.Value is Num)
                {
                    // We set the process flag to the start variable
                    processFlag = (Num)rd_start.Value;
                }

                rd_begin = tRob1.GetRapidData("MODULE_1", "flag");

                if (rd_begin.Value is Num)
                {
                    processFlag = (Num)rd_start.Value;
                }

            }
        }
        public void StartRapidProgram()
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {
                        //Perform operation
                        Debug.WriteLine("Exec status of the controller ::: " + controller.Rapid.ExecutionStatus);
                        Debug.WriteLine("Controller State ::: " + controller.State);
                        controller.Rapid.Start(true);
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
        public void StopRapidProgram()
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {
                        controller.Rapid.Stop();
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
        public void StopProcess()
        {
            // fillfromstring2 allows us to pass a varable to rapid data
            processFlag.FillFromString2(SHUT_DOWN.ToString());
            using (Mastership m = Mastership.Request(controller.Rapid))
            {

                rd_start.Value = processFlag;
            }
        }
        public void StartProcess()
        {
            processFlag.FillFromString2(TO_PickUp.ToString());
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                rd_begin.Value = processFlag;
            }
        }
        public void Dispose()
        {
            if (controller.Rapid.ExecutionStatus == ExecutionStatus.Running)
            {
                StopProcess();
            }
            this.controller.Logoff();
            this.controller.Dispose();
            this.controller = null;
        }
        public Controller Controller
        {
            get
            {
                return controller;
            }
        }

    }
}