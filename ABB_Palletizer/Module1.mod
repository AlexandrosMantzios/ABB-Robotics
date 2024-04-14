MODULE Module1
    !***********************************************************
    !
    ! Module:  Module1 - Palletizing Single Line (KRI-KRI Offer)
    !
    ! Description:
    !   
    !
    ! Author: a.mantzios (AS HELLAS)
    !
    ! Version: 1.2
    !

          
!##########################################################################################################
! Declaration of data type for storing positions of placing (patterns) for different products
RECORD xy_rot
    num pos_x;
    num pos_y;
    !num pos_z;
    num rot;
ENDRECORD   
!##########################################################################################################
            

!##########################################################################################################
! Declaration of data type for storing the product specification (name, length, width, height, pattern number, number of layers, mirroring)
RECORD product_spec
    string prod_name;
    num lenght;
    num width;
    num height;
    num patt_num;
    num NumOfLayers;
    num NuminLayer;
    bool mirror_layer;
    num carton_layer;
ENDRECORD
!##########################################################################################################    
     

!##########################################################################################################    
! Declaration of different place points for each pattern and product { pattern type, points } - Need to specify coordinates for all place positions.
VAR xy_rot Pattern_all{2,3}:=[[[0,0,0],[800,-400,90],[800,0,90]],
                             [[0,0,0],[800,-400,90],[800,0,90]]];
!##########################################################################################################    
                                    
! Declaration of assigning the specific product elements (product_spec) to MySpec array.                             
!KRI-KRI Dimensions                                     
VAR product_spec MySpec_array{2}:=[["Line  1kg (11)",800,400,129,1,11,3,TRUE,4],
                                   ["Line  500gr (20)",800,400,80,2,20,3,TRUE,1]];
!                                  ["Line  0.33 (54)",800,370,232,3,6,3,TRUE,1],
!                                  ["Line  0.5 (72)",800,400,170,4,8,3,TRUE,1],
!                                  ["Line  0.33 (108)",800,400,115,5,12,3,TRUE,1],  
!                                  ["Line  0.5 (80)",810,270,175,6,10,4,FALSE,1]];                                                                      
! Declaration of list for chosing product - This is used for operator to see names in FlexP                                   
VAR listitem Listitem1{2};
! Declaration of number of different products - Can be used as maximum limit.
VAR num NumOfDiffProd:=2; 
! Declaring number for storing the number of product_type
VAR num product_type; 
!Declaring variable for storing the line of the pattern array : 1 pattern / line. 
VAR num a;
VAR num b;
    
     
! SPEED DATA  
PERS speeddata  MaxVel := [7000,500,5000,1000];
PERS speeddata CartonVel:= [2000,500,5000,1000];
PERS speeddata PlaceVel:=[3000,500,5000,1000];
PERS speeddata PickVel:=[3000,500,5000,1000];
PERS num vel_operation:=75;
VAR jointtarget jntReach; 

!CONST speeddata vSmall:=[5000,500,5000,1000];
!CONST speeddata vMedium:=[5000,500,5000,1000];
!CONST speeddata vLarge:=[2000,500,5000,1000];


 VAR pos Current_Position_TCP;
VAR pos Current_Position; 
VAR robtarget Current_Position1; 
CONST robtarget HomePos:=[[996.974164261,0,607.521793986],[0,0,1,0],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2:=[[-927.502103227,-2202.904,-299.990205162],[0,1,0,0],[-2,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];	
CONST robtarget Place_3:=[[-307.451421677,-460.558163921,311.958905012],[0,1,0,0],[0,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place:=[[-263.288869629,-292.857,-23.571092407],[0,1,0,0],[0,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_2:=[[407.44478181,914.866486224,6.240188412],[0.707106781,0,0,0.707106781],[-1,0,-2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR intnum ErrorInt;
VAR intnum Homingint;
VAR intnum Speedint;
VAR robtarget robtarget_reach;
VAR num VelocityPercentage;
PERS num nPalletCount:=0;
PERS num nPalletCount1:=19;
PERS num Boxes1:=15;
PERS num Trays1:=45;
PERS num Products1:=270;
PERS num Boxes2:=0;
PERS num Trays2:=0;
PERS num Products2:=0;
PERS num LayerCount;
PERS num Xoffset:=0;
PERS num Yoffset:=0;
PERS num Zoffset:=0;
PERS num Z2offset:=0;
PERS num nZrelTool:=0;
VAR num bReachable;


CONST string Name:="TpsViewASIRB120Controller1.gtpu.dll";
CONST string Type:="ABB.Robotics.SDK.Views.MainScreen";

! DETERMINE LOAD ---------------------------------------------------------------
PERS loaddata load1:= [6,[-200,0,700],[1,0,0,0],0,0,0];
! ------------------------------------------------------------------------------
CONST robtarget Target_10:=[[1704.910097272,0,-161.537939468],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
!  VAR robtarget Carton_Finish:=[[767.395,-1435.364,-1132.968810694],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
!  VAR robtarget Carton_Start_2:=[[767.395,-1435.364,-318.036839984],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Carton_Placer:=[[1791.669472271,44.101062819,-213.634870957],[0,0,1,0],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Carton_Placer_2:=[[1558.510684757,-53.203045073,-221.534784785],[0,0.707106781,0.707106781,0],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Target_20:=[[264.658853265,-1403.879337853,1277.812331583],[0,0.687839018,0.725863269,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
!  VAR robtarget Carton_Start_2_2:=[[-21.95061604,1611.815905354,339.932008226],[0,0,1,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
!    !***********************************************************
VAR bool bProgEnd;
VAR robtarget p_err;
PERS bool bReject:=FALSE; 
PERS bool bCycle_Stop:=FALSE; 
PERS bool bSafe:=TRUE; 
PERS bool bPalletinPlace:=FALSE;
VAR num Layers3;
VAR num Layers;
VAR num CountPallets;
VAR robtarget p10;
VAR intnum err_interrupt;
VAR trapdata err_data;
VAR errdomain err_domain;
VAR num err_number; 
VAR errtype err_type;
VAR num errorid;

VAR errstr arg:="Test";
VAR busstate bstate;
CONST robtarget Place_Ver:=[[-398.266428958,-1018.977097669,175.526034099],[0,-0.707106781,0.707106781,0],[-1,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Carton_Placer_2_2:=[[904.240967181,1491.528896019,-168.095675742],[0,0,1,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2_2:=[[168.147276503,-1897.749047275,241.53497609],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
! CONST robtarget pPlace:=[[-200,-400,190],[0,0.707106781,0.707106781,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR bool Mirror; 
CONST robtarget Target_10_2:=[[1259.814832031,1017.970446518,1341.11539547],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_3:=[[33.986354575,-1857.119149053,-49.097996677],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Carton_Placer_3:=[[1890.433976622,-50.973060166,-66.050821766],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR num NewPos; 
CONST robtarget Pick:=[[33.986354575,-1857.119149053,-214.097996677],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR signaldo alias_do;
CONST robtarget Carton:=[[767.395,-1435.364,-870.266205162],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Home:=[[1910,0,1338],[0,0,1,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1:=[[674.66490139,-2219.467318636,-308.458780378],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_Approach:=[[937.912449386,1434.321016458,564.542296942],[0,-0.707106781,0.707106781,0],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2_Approach:=[[-839.928668614,-2202.904,-300.109205162],[0,1,0,0],[-2,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Target_30:=[[394.162,287.272224073,-38.683979103],[0.707106781,0,0,-0.707106781],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_1:=[[400.265101441,287.272224073,-4.043669422],[0.707106781,0,0,-0.707106781],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pallet_Approach:=[[2312.080608209,1469.687143642,-121.686395209],[0,0.707106781,0.707106781,0],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_Approach_2:=[[1428.981,915.786055397,-803.17002947],[0,-0.707106781,0.707106781,0],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2_Approach_2:=[[-850.773361175,-2202.904,-0.397205162],[0,1,0,0],[-2,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR robtarget Carton_Start:=[[767.395,-1435.364,-503.908754982],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR robtarget Cartons_Start:=[[0.333,-1882.002944603,-168.326317465],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR robtarget Cartons_Finish:=[[489.212820417,1782.232350614,-1132.968810694],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR robtarget Cartons_Start_2:=[[-67.807,-1866.943944603,-190.487140169],[0,0.707106781,0.707106781,0],[-2,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Cartons_Placer:=[[2091.675746756,-839.033052886,-942.17002947],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Cartons_Approach:=[[18.224,-1890.775944603,190.418168972],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Cartons_Approach_2:=[[947.357,-1516.470944603,538.82997053],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_3:=[[593.948197323,-2171.97303463,-324.819907176],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2_3:=[[-850.112992391,-2137.530394593,-324.819907176],[0,1,0,0],[-2,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_1_3:=[[390.448049773,272.453507804,-4.043669422],[0.707106781,0,0,-0.707106781],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_3_90:=[[597.891,-2025.265,-325.747205162],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_1_3_M:=[[390.448049773,273.894643613,-4.043669422],[0,-0.707106781,0.707106781,0],[-1,0,-2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_3_90_M:=[[593.227710406,-2306.451018246,-320.881809283],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_Approach_3:=[[1416.996769653,1151.518760126,-320.881809283],[0,0.707106781,0.707106781,0],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pallet_Approach_2:=[[2312.080608209,1469.687143642,-121.686395209],[0,0.707106781,0.707106781,0],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_2:=[[912.911,1490.063055397,-590.17002947],[0,-0.707106781,0.707106781,0],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_Approach_2_2:=[[303.708,1733.841,-48.331577796],[0,0.707106781,0.707106781,0],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_Approach_3_2:=[[1416.996769653,1151.518760126,-320.881809283],[0,0.707106781,0.707106781,0],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_1_Line2:=[[421.537107736,287.272224073,-4.043669422],[0.707106781,0,0,-0.707106781],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2_5:=[[-39.831843829,-2202.904,-86.359942469],[0,0,1,0],[-2,0,-2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_1_Line2_2:=[[421.537107736,920.385448923,-4.043669422],[0.707106781,0,0,0.707106781],[-1,0,-2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_Approach_2_Line_2:=[[542.867,-2219.46654715,-22.124577796],[0,1,0,0],[-1,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_Line_2:=[[674.66490139,-2219.467318636,-308.458780378],[0,1,0,0],[-1,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2_3_90:=[[-847.049717971,-2015.529710177,-325.747205162],[0,1,0,0],[-2,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_2_3_90_M:=[[-843.735965091,-2285.954195178,-320.881809283],[0,1,0,0],[-2,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_1_Syn_10:=[[2.261225129,-406.344712631,-16.088921085],[1,0,0,-0.000000008],[-1,0,-2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_2_Synerlink_10:=[[402.168036242,-406.517379846,17.245895332],[0,-0.000000008,1,0],[-1,0,-2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_1_2_2:=[[942.222,1980.271055397,-590.17002947],[0,-0.707106781,0.707106781,0],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Home_1:=[[1784.925,-20.721944603,1013.162521302],[0,0,1,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAR robtarget Cartons_End:=[[-5.384459385,-1936.9837714,-951.616832539],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Place_1_Syn_10_3:=[[-7.755027487,-404.51550512,16.088921085],[0,1,0.000000008,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pallet_Placer_2:=[[2190.83,-782.027944603,-948.17002947],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pallet_Placer_2_2:=[[2190.829622864,-782.027772847,482.153864529],[0,0,1,0],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
VAr robtarget Pick_Pallet_Finish:=[[-1053.115018567,1834.800837736,-923.222666306],[0,-0.707106781,0.707106781,0],[1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
var robtarget Pick_Pallet_Start:=[[-1053.115,1834.801055397,97.82997053],[0,-0.707106781,0.707106781,0],[1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
var robtarget Pick_Pallet_Start_2:=[[-1053.115,1834.801055397,232.82997053],[0,-0.707106781,0.707106781,0],[1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_Pallet_Approach_2:=[[-1000.722,1834.801055397,323.82997053],[0,-0.707106781,0.707106781,0],[1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
CONST robtarget Pick_Pallet_Approach:=[[964.785,1550.630055397,323.82997053],[0,-0.707106781,0.707106781,0],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Homing_Right_Approach:=[[860.898,1534.847055397,323.82997053],[0,-0.707106781,0.707106781,0],[1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
   VAR string servicestring;
    CONST robtarget Homing_Left_Approach:=[[947.357,-1516.470944603,638.82997053],[0,0.707106781,0.707106781,0],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
! ###################################################################################################################################################################################################
     
    
    ! SYSTEM INPUT SIGNALS
    !  "MOTORS ON AND START"
    !  "QUICK STOP"
    !  "RESET EMERGENCY STOP"
    !  "SYSTEM RESTART"
    !  "RESET EXECUTION ERROR SIGNAL"
    !  "PP TO MAIN"
! SYSTEM  OUTPUT SIGNALS
    !  "POWER FAIL ERROR"
    !  "PRODUCTION EXECUTION ERROR"

!######################################################################################
! CHECK AIR PRESSURE SYSTEM ----> SIGNAL TO BE SUPPLIED BY PLC USING ANALOGUE MEASUREMENTS OR A SIMPLE DI / DO      
PROC CheckAir()
    WHILE Air_On=0 DO
        Set Air_Fault;
        Set Sequence_Fault;
        TPWrite "Air Supply Problem";
        TPWrite "Check Compressed Air Supply";
        TPWrite "Wait for Fault Rectification";
        WaitDI Air_On,1; 
        Reset Air_Fault;
        Reset Sequence_Fault;
    ENDWHILE
    RETURN;       
ENDPROC
!######################################################################################   

!######################################################################################
! INITIALIZE BOOLEANS AND FLAGS      
PROC SetFlags()    
bReject:=FALSE; 
bCycle_Stop:=FALSE; 
bSafe:=FALSE;  
bPalletinPlace:=FALSE;
ENDPROC
!######################################################################################


!######################################################################################
! INITIALIZE IO's -----> TO BE EXPANDED TO INCLUDE SENSORS FOR GRIPPER AND PLC COMMUNICATION SIGNALS 
    PROC InitializeIO()
        Reset Air_Fault;
        Reset Robot_Auto;
        Reset Robot_Manual;
        Reset Load_Cartons;
        Reset Load_Cartons;
        Reset Load_Pallets;
        Reset Production_Exec_Error;
        Reset Sequence_Fault;
        Reset PlacedCarton;
        Reset Start_Home_DO;
        Reset EndCycle;
        !Reset Power_Fail_Error;
        Reset GripCarton;
      !  Reset Attach2;
        Reset Sequence_Fault;
!        Reset Picked_Product;
        Reset PalletFull;
      !  WaitUntil Line_1 = 1 OR Line_1 = 2; 
!        Reset Released_Product;
!        Reset RobHome;
    ENDPROC
!######################################################################################  


!######################################################################################
! HOMING PROCEDURE ----> TO BE EXPANDED
! GET CURRENT POSITION OF ROBOT AND DETERMINE MOVEMENTS
   PROC Homing()
         Current_Position:=CPos(\Tool:=Tooldata_1 \WObj:=wobj0);
       Current_Position1:=CRobt(\Tool:=Tooldata_1 \WObj:=wobj0);
         p10 := CRobT(\Tool:=Tooldata_1 \WObj:=wobj0);
        bSafe:=FALSE;
        IF Current_Position1= Home_1 THEN
            bSafe:=TRUE;
            SetDO RobHome,1;
            RETURN;
        ENDIF
    IF PalletSensor = 1 AND (PalletFull=1 OR Products1<>0) THEN
        WaitDI PalletSensor,0;
    ENDIF
IF Current_Position1.trans.y > 0 THEN
        IF Current_Position1.trans.x < 0 THEN
              IF Current_Position1.trans.z > 280 THEN
            !  MoveL Homing_Right_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
              MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
              Set RobHome;
               bSafe:=TRUE;
           ELSE
               IF Current_Position1.trans.z <100 THEN
                 MoveL Offs(Current_Position1,0,0,800),v500,z5,Tooldata_1\WObj:=wobj0;
               !   MoveL Homing_Right_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                  MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                bSafe:=TRUE;
                  Set RobHome;
               ELSE
                 MoveL Offs(Current_Position1,0,0,600),v500,z5,Tooldata_1\WObj:=wobj0;
              !    MoveL Homing_Right_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                  MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                bSafe:=TRUE;
                  Set RobHome;
               ENDIF               
          ENDIF           
        ELSEIF Current_Position1.trans.x > 0 THEN
                   IF Current_Position1.trans.z > 600 THEN
                 !    MoveL Homing_Right_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                     MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                     Set RobHome;
                     bSafe:=TRUE;
                   ELSE
                  MoveL Offs(Current_Position1,0,0,1000),v500,z5,Tooldata_1\WObj:=wobj0;
                !   MoveL Homing_Right_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                  MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                   bSafe:=TRUE;
                  Set RobHome;  
                   ENDIF       
        ENDIF
        
ELSEIF Current_Position1.trans.y<0 THEN
    
             IF Current_Position1.trans.x < 0 THEN
                 
              IF Current_Position1.trans.z > 1050 THEN
           !   MoveL Homing_Left_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
              MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
              Set RobHome;
               bSafe:=TRUE;
           ELSE
               IF Current_Position1.trans.z <800 THEN
                 MoveL Offs(Current_Position1,0,0,1000),v500,z5,Tooldata_1\WObj:=wobj0;
              !    MoveL Homing_Left_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                  MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                bSafe:=TRUE;
                  Set RobHome;
               ELSE
                 MoveL Offs(Current_Position1,0,0,600),v500,z5,Tooldata_1\WObj:=wobj0;
               !   MoveL Homing_Left_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                  MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                bSafe:=TRUE;
                  Set RobHome;
               ENDIF               
          ENDIF           
        ELSEIF Current_Position1.trans.x > 0 THEN
                   IF Current_Position1.trans.z > 500 THEN
                !     MoveL Homing_Left_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                     MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                     Set RobHome;
                     bSafe:=TRUE;
                   ELSE
                  MoveJ Offs(Current_Position1,0,0,1000),v500,z5,Tooldata_1\WObj:=wobj0;
                !   MoveL Homing_Left_Approach,v500,z100,Tooldata_1\WObj:=wobj0;
                  MoveJ Home_1,v500,z5,Tooldata_1\WObj:=wobj0;
                   bSafe:=TRUE;
                  Set RobHome;  
                   ENDIF       
        ENDIF           
        ENDIF     
   

WaitTime 2;
    ENDPROC 
!######################################################################################   


     PROC Stop()
         StopMove;
    ENDPROC


 !######################################################################################
! PROCEDURE TO TEST GRIPPER MOVEMENT      
PROC GripperTest()
     Dettach;
     Attach1;
     Dettach;
     Attach1;
ENDPROC
 !######################################################################################  

!######################################################################################
! PROCEDURE TO INITIALIZE     
    PROC Initialize()
    AccSet 100,70;
    MaxVel:=vmax;
   ! NumOfPallets:=0;
    CountPallets:=0;
    Boxes1:=0;
    Boxes2:=0;
    Trays1:=0;
    Products1:=0;
    SetDO Attach2,0;
    SetDO RobHome,0;
    VelocityPercentage:=0;
    SetDO Error_Gen,0;
   ! ErrorCode:=ERRNO;
    !Workobject_Carton.oframe.trans.z:=0;
    !MoveJ Target_10,v3000,fine,Tooldata_1\WObj:=wobj0;
    ! SetDO Attach,0; 
    ENDPROC   
!######################################################################################   


!######################################################################################
! PROCEDURE TO GRIP ITEM - AIR ON      
    PROC Attach1()
        WaitTime 0.7; 
        !Grip
        SetDO D652_10_DO2,0;
        SetDO D652_10_DO1,1;
        SetDO D652_10_DO4,0;
        SetDO D652_10_DO3,1;
        SetDO Attach2,1;
        WaitTime 0.7;
    ENDPROC
!###################################################################################### 
    
    
 !######################################################################################
! PROCEDURE TO RELEASE ITEM - AIR OFF
!!!! NEED TO INCORPORATE SOME GRIPPING FEEDBACK FUNCTION TO ENSURE ITEM REMAINS GRIPPED 
!!!! GRIPPER SENSORS    
    PROC Dettach() 
        WaitTime 1; 
        !Release
        SetDO Attach2,0;
        SetDO D652_10_DO1,0;
        SetDO Attach2,0;
        SetDO D652_10_DO2,1;
        WaitTime 0.3;
        SetDO D652_10_DO4,1;
        SetDO D652_10_DO3,0;
        SetDO Attach2,0;
        WaitTime 1; 
    ENDPROC
 !######################################################################################   
    
    
  
!#####################################################################################
! PROCEDURE TO ATTACH CARTON SIGNAL - VACUUM ON   
       PROC AttachCarton()
       !  WaitTime 0.5; 
         WaitTime 0.5;
         SetDO GripCarton,1;
         SetDO DettachCarton1,0;
          WaitTime 0.5; 
    ENDPROC
!#####################################################################################
    


!#####################################################################################
! PROCEDURE TO DETTACH CARTON SIGNAL - VACUUM OFF       
    PROC DettachCarton()
         WaitTime 0.5; 
         SetDO GripCarton,0;
        SetDO DettachCarton1,1;
          WaitTime 1.5; 
    ENDPROC
!#####################################################################################   
    
   

!#####################################################################################
! PROCEDURE TO ATTACH PALLET SIGNAL   
       PROC AttachPallet()
         WaitTime 0.5; 
         WaitTime 0.5;
         SetDO Attach3,1;
        ! SetDO DettachCarton1,0;
          WaitTime 0.5; 
    ENDPROC
!#####################################################################################
 

!#####################################################################################
! PROCEDURE TO DETTACH PALLET - VACUUM OFF       
    PROC DettachPallet()
         WaitTime 0.5; 
         SetDO Attach3,0;
     !   SetDO DettachCarton1,1;
          WaitTime 1.5; 
    ENDPROC
!#####################################################################################  
 
!#####################################################################################
! PROCEDURE TO TEST BOARDS 

PROC TestIO()
     IF (IOUnitState ("PN_Internal_Anybus")= IOUNIT_RUNNING) THEN
      ! PROFINET MODULE RUNNING
    ELSE
      ! Read/Write signal on the I/O unit result in error
      TPErase;
      TPWrite "PN_Internal_Anybus-adapter  broken";
    ENDIF
    
    IF (IOUnitState ("D652_10")= IOUNIT_RUNNING) THEN
      ! Possible to access the signal on the I/O unit
    ELSE
      ! Read/Write signal on the I/O unit result in error
      TPErase;
      TPWrite "D652_10  broken";
    ENDIF
        
    ! *** IOBusState ***
    ! 0 BUSSTATE_HALTED 
    ! 1 BUSSTATE_RUN     
    ! 2 BUSSTATE_ERROR   
    ! 3 BUSSTATE_STARTUP 
    ! 4 BUSSTATE_INIT    
        
    IOBusState "PROFINET_Anybus", bstate;
    TEST bstate
    CASE BUSSTATE_RUN:
    TPWrite "PROFINET OK";
      ! Possible to access the signal on the I/O unit
    DEFAULT:
      ! Read/Write signal on the I/O unit result in error
      TPErase;
      TPWrite "PROFINET_Anybus  broken";
    ENDTEST
    RETURN;
    
    
ENDPROC
!#####################################################################################


!#################################################################################  
    !!! PROCEDURE FOR PICK & PLACE OF CARTON ON PALLET !!!    
    PROC PlaceCarton()
	! Workobject_2.oframe.trans.z:=Z2offset;
    IF Carton_Sensor_Empty=1 THEN
        IF Carton_Sensor_Low = 0 THEN
            TPWrite "Warning - Low Level Cartons";
        ENDIF
        SetDO PlacedCarton,0;
        WaitTime 0.5; 
   MoveJ Offs(Cartons_Approach_2,0,0,0),vmax,z10,Tooldata_1\WObj:=wobj0; 
SetDO D652_10_DO2,0;
SetDO D652_10_DO1,1;
      MoveJ Cartons_Approach,vmax,z10,Tooldata_1\WObj:=wobj0;   
      MoveL Cartons_Start_2,vmax,z10,Tooldata_1\WObj:=wobj0;  
     SearchL\SStop, Empty,\Flanks,Cartons_Start,Cartons_End,v100,Tooldata_1\WObj:=wobj0;
     WaitTime 0.5; 
      AttachCarton;
       WaitTime 0.3; 
      MoveL Cartons_Start_2,v1000,z10,Tooldata_1\WObj:=wobj0;  
      MoveL Cartons_Approach,v1000,z10,Tooldata_1\WObj:=wobj0; 
        MoveL Cartons_Approach_2,v1000,z10,Tooldata_1\WObj:=wobj0; 
          MoveJ Offs(Cartons_Placer,0,0,300),v1000,fine,Tooldata_1\WObj:=Workobject_3; 
      MoveL Offs(Cartons_Placer,0,0,30),v1000,fine,Tooldata_1\WObj:=Workobject_3; 
      WaitTime 0.5; 
          DettachCarton;
          SetDO Attach2,0;
          SetDO PlacedCarton,1;
      WaitTime 0.5;     
        SetDO PlacedCarton,1;
         MoveJ Offs(Cartons_Placer,0,0,100),vmax,fine,Tooldata_1\WObj:=Workobject_3; 
    ELSE
          TPWrite"Carton Tray Empty - Load Cartons to Continue";
          SetDO Load_Cartons,0;
          WaitUntil Carton_Sensor_Empty=1;
          WaitDI Cartons_Loaded,1;
    ENDIF
         
    ERROR
    IF ERRNO = ERR_WHLSEARCH THEN    
    errorid:=ERRNO;
    Set Sequence_Fault;
    SetGO ErrorCode,ERRNO;
    SetDO Error_Gen,1;
    StorePath; 
    SetDO Load_Cartons,0;
    MoveJ Cartons_Start_2,v800,z100,Tooldata_1\WObj:=wobj0;
    TPWrite "Insert Cartons";
    TPWrite "ErrorID: "+ NumToStr(errorid,0);
    ErrWrite "ERR_WHLSEARCH","Error while searching Cartons";
   ! ErrLog errorid,ERRSTR_TASK,arg,ERRSTR_CONTEXT,ERRSTR_UNUSED,ERRSTR_UNUSED;
    !ErrLog errorid\W,ERRSTR_TASK,arg,ERRSTR_CONTEXT,ERRSTR_UNUSED,ERRSTR_UNUSED;
    !SetDO Load_Cartons,1;
    WaitDI Carton_Sensor_Empty,1; WaitDI Carton_Sensor_Low,1;
    WaitDI Reset_Exec_Error,1;
    SetDO Error_Gen,0;
    Reset Sequence_Fault;
    RestoPath;
    ClearPath;
    StartMove;
    RETRY;
    ENDIF 
   IF ERRNO=ERR_EXCRTYMAX THEN
    errorid:=ERRNO;
    SetGO ErrorCode,ERRNO;
    TPWrite "Restore System Manually";
    TPWrite "ErrorID: "+ NumToStr(errorid,0);
    ErrWrite "MAX TRIES FOR RESTORING ERROR","Error while searching Cartons";
    WaitDI Reset_Exec_Error,1;
    RETURN;
    ENDIF 
        
!       IF ERRNO = ERR_WHLSEARCH THEN
!    Set Sequence_Fault;
!       StorePath; 
!    SetDO Load_Cartons,0;
!      MoveL Cartons_Start,v800,fine,Tooldata_1\WObj:=wobj0;
!      WaitDI Cartons_Loaded,1;
!      Reset Sequence_Fault;
!      TPWrite "Insert Empty";
!      RestoPath;
!      ClearPath;
!      StartMove;    
!      RETRY;
!    ENDIF
    ENDPROC
!#################################################################################    
    

!#################################################################################  
PROC Test_Rob_Lim()
MoveL Offs(Pick_1_2_2,0,-400,100),vmax,z50,Tooldata_1\WObj:=wobj0; 
IF bReachable=1 THEN
 MoveL Offs(Pick_1_2_2,0,-400,3000),vmax,z50,Tooldata_1\WObj:=wobj0; 
ELSE
Homing;
ENDIF
 
 
ENDPROC
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



!#################################################################################  
    !!! PROCEDURE FOR PICK & PLACE OF PALLET !!!    
    PROC PlacePallet()
	! Workobject_2.oframe.trans.z:=Z2offset;
        SetDO Placed_Pallet,0;
        WaitTime 0.5; 
       ! Homing;
   MoveJ Offs(Pick_Pallet_Approach,0,0,0),vmax,z10,Tooldata_1\WObj:=wobj0; 
      MoveJ Pick_Pallet_Approach_2,vmax,z10,Tooldata_1\WObj:=wobj0;   
      
      MoveJ Pick_Pallet_Start_2,vmax,z10,Tooldata_1\WObj:=wobj0;  
     SearchL\SStop, Empty,\Flanks,Pick_Pallet_Start,Pick_Pallet_Finish,v100,Tooldata_1\WObj:=wobj0;
     WaitTime 0.5; 
      AttachPallet;
       WaitTime 0.3; 
      MoveL Pick_Pallet_Start_2,v1000,z10,Tooldata_1\WObj:=wobj0;  
      MoveJ Pick_Pallet_Approach_2,v1000,z10,Tooldata_1\WObj:=wobj0; 
        MoveL Pick_Pallet_Approach,v1000,z10,Tooldata_1\WObj:=wobj0; 
        
          !MoveJ Offs(Cartons_Placer,0,0,300),v1000,fine,Tooldata_1\WObj:=wobj0; 
      MoveJ Pallet_Placer_2_2,v1000,fine,Tooldata_1\WObj:=wobj0; 
            MoveJ Pallet_Placer_2,v1000,fine,Tooldata_1\WObj:=wobj0; 
      WaitTime 0.5; 
          DettachPallet;
          SetDO Attach3,0;
          SetDO Placed_Pallet,1;
      WaitTime 0.5;     
        SetDO Placed_Pallet,1;
         MoveJ Offs(Pallet_Placer_2_2,0,0,100),vmax,fine,Tooldata_1\WObj:=wobj0; 
        ERROR
       IF ERRNO = ERR_WHLSEARCH THEN
           Set Sequence_Fault;
       StorePath; 
        SetDO Load_Pallets,0;
      MoveL Pick_Pallet_Start,vmax,fine,Tooldata_1\WObj:=wobj0;
      ! NEED TO CHANGE SIGNAL TO PALLETS LOADED DI!!
      WaitDI Cartons_Loaded,1;
      Reset Sequence_Fault;
      TPWrite "Insert Empty";
      RestoPath;
      ClearPath;
      StartMove;    
      RETRY;
    ENDIF
    ENDPROC
!#################################################################################    
  
!#################################################################################    
!PROCEDURE TO SET INITIAL SPEED FROM HMI
PROC SetSpeed()
    
      ! HMI Use
      vel_operation:=Vel_Percentage;
      
!!       PC Interface Use
!      vel_operation:=VelocityPercentage;
      
      WHILE vel_operation=0 DO
        TPErase;
        TPWrite " Speed setted from PLC is zero";
        TPWrite " Waiting for value greater than zero";
        
!        ! PC Interface Use
!        WaitUntil VelocityPercentage>0;
!        WaitTime 1;
        
        ! HMI Use
        vel_operation:=Vel_Percentage;
        
!        ! PC Interface Use
!        vel_operation:=VelocityPercentage;
        
        TPErase;
      ENDWHILE
    IF vel_operation>100 vel_operation:=100;
    IF vel_operation<20 vel_operation:=20;
    VelSet vel_operation,7000;
  ENDPROC   
    
    
    










!#################################################################################    NEEDS FURTHER WORK FOR DETERMINING REACHABILITY #################
   PROC CheckReachable (robtarget robtarget_reach, PERS tooldata Tooldata_1)!,\PERS wobjdata Workobject_2)

    VAR jointtarget jntReach; 
    bReachable:=1;
    !IF present (Workobject_2) THEN 
       ! jntReach:=CalcJointT(robtarget_reach,Tooldata_1\WObj:=Workobject_2);
   ! ELSE
        jntReach:=CalcJointT(robtarget_reach,Tooldata_1\WObj:=Workobject_2);
   ! ENDIF
    ERROR
      IF ERRNO=ERR_ROBLIMIT  THEN 
          bReachable:=2;
          TPWrite "Not Reachable";
          Homing;
          !TPWrite "Not Reachable";
          StopMove;
          TRYNEXT;
      ENDIF
          IF ERRNO=ERR_OUTSIDE_REACH  THEN 
          bReachable:=2;
          TPWrite "Not Reachable";
          Homing;
          !TPWrite "Not Reachable";
          StopMove;
          TRYNEXT;
      ENDIF
   ENDPROC
!#################################################################################   



!#################################################################################    NEEDS FURTHER WORK FOR DETERMINING REACHABILITY #################
   PROC CheckReachable1 (robtarget robtarget_reach, PERS tooldata Tooldata_1,\PERS wobjdata Workoject_1)

    VAR jointtarget jntReach; 
    bReachable:=1;
    IF present (Workobject_1) THEN 
        jntReach:=CalcJointT(robtarget_reach,Tooldata_1\WObj:=Workobject_1);
    ELSE
        jntReach:=CalcJointT(robtarget_reach,Tooldata_1);
    ENDIF
    ERROR
      IF ERRNO=ERR_ROBLIMIT  THEN 
          bReachable:=2;
          TPWrite "Not Reachable";
          Homing;
          !TPWrite "Not Reachable";
          StopMove;
          TRYNEXT;
      ENDIF
          IF ERRNO=ERR_OUTSIDE_REACH  THEN 
          bReachable:=2;
          TPWrite "Not Reachable";
          Homing;
         ! TPWrite "Not Reachable";
          StopMove;
          TRYNEXT;
      ENDIF
   ENDPROC
!#################################################################################   
    
    
    TRAP Traproutine
     StopMove;
     WaitUntil Continue=1 OR Restart =1 OR EndCycle=1;
     Current_Position:=CPos(\Tool:=tool0 \WObj:=wobj0);
     IF Continue=1 THEN
        StorePath;
         WaitDI Interrupt,0;
           p10:=CRobT(\Tool:=tool0 \Wobj:=wobj0); 
            RestoPath;
         StartMove;
     ENDIF
     IF Restart=1 THEN
         ClearPath;
           WaitDI Interrupt,0;
         IDelete ErrorInt;
         StartMove;
         ExitCycle;
     ENDIF
     IF EndCycle=1 THEN
         Homing;
          StopMove;
          StorePath;
            MoveJ Home_1,v3000,fine,Tooldata_1\WObj:=wobj0;
           p10:=CRobT(\Tool:=tool0 \Wobj:=wobj0);
         WaitDI Interrupt,0;
         ClearPath;
         TPWrite "Stop at end of Cycle";
         bProgEND:=TRUE;
!          ExitCycle;
     ENDIF
       ERROR
         IF ERRNO = ERR_WHLSEARCH THEN
       StorePath; 
     SetDO Load_Cartons,0;
      MoveJ Cartons_Start_2,vmax,z100,tool0\WObj:=wobj0;
      WaitDI Cartons_Loaded,1;
      TPWrite "Insert";
      SetDO Load_Cartons,1;
      RestoPath;
      ClearPath;
      StartMove;
      RETRY;
         endif
 ENDTRAP  
  
 
     !! Traproutine connected to collision error
     TRAP Traproutine2
        !Reset Homing_button;
!        StopMove;
!        StorePath;
!         WaitDI Homing_Button,0;
!           p10:=CRobT(\Tool:=tool0 \Wobj:=wobj0); 
!            RestoPath;
!         StartMove;
         Current_Position:=CPos(\Tool:=Tooldata_1 \WObj:=wobj0);
          WaitDI Homing_Button,0;
            TPWrite "Press Start to GO TO HOME";
            WaitTime 2;
        !  WaitDI Start_Home_DI,1;
             Homing;
             StopMove;
            StorePath;
          !Homing;
         ! WaitTime 2;
       !   StopMove;
      ! RestoPath;
        !  StartMove;
           !MoveJ Home_1,v3000,fine,Tooldata_1\WObj:=wobj0;
         ClearPath;
         ExitCycle;
         ReSet Start_Home_DO;  
    ENDTRAP
 
TRAP Traproutine3
VAR num speed_corr;
WaitDI Velocity_Control,0;

!Include a velocity Set DI  with DO crossconnection

!HMI Use
vel_operation := Vel_Percentage;

!!PC Interface Use
!vel_operation:= VelocityPercentage;

VelSet vel_operation,7000;
ERROR
IF ERRNO = ERR_SPEED_REFRESH_LIM THEN
IF vel_operation > 100 THEN 
    vel_operation := 100;
ENDIF
IF vel_operation < 0 THEN
vel_operation := 0;
ENDIF
RETURN;
ENDIF
ENDTRAP


! #####################################################################################
! MAIN PROCEDURE   
    
    PROC main()
   
         
! Trap declaration for signal DI Interrupt 
IDelete ErrorInt; 
CONNECT ErrorInt WITH Traproutine;
ISignalDI Interrupt,1,ErrorInt;    


IDelete Homingint; 
CONNECT Homingint WITH Traproutine2;
ISignalDI Homing_Button,1,Homingint;   
!!      AliasIO Attach , alias_do; 
        
! !!!! INTERRUPT FOR COLLISION DETECTION        
!        IDelete CollisionDetect; 
!        CONNECT CollisionDetect WITH Traproutine1;
!        ISignalDO Trigger,1,CollisionDetect;


!!!INTERRUPT FOR VELOCITY CHANGE!!!
IDelete Speedint; 
CONNECT Speedint WITH Traproutine3;
ISignalDI Velocity_Control,1,Speedint;


!!! ACTIVATION OF MOTION SUPERVISION WITH SENSITIVITY AT 70% OF THE REQUIRED FORCE. IDEALLY SHOULD BE AT 100%
MotionSup\On,\TuneValue:=100; 

! Test IO Devices
TestIO;
! Initialization of I/Os Procedure call 
InitializeIO;
! Set Booleans - SetFlags Procedure call  
SetFlags;
! Check Air Pressurization
CheckAir;
! Initialization of remaining variables -----> ** REQUIRES ADJUSTMENTS
Initialize;
! Homing Procedure call -----> ** REQUIRES ADJUSTMENTS 
SetSpeed;

IF OpMode() = OP_AUTO  THEN
    IF RobHome=0 THEN
    WaitTime 5;
     TPWrite "Homing Robot in Auto";
Homing;
    ELSE
        bSafe:=TRUE;
    ENDIF


ENDIF
IF OpMode() = OP_MAN_PROG THEN
    IF RobHome=0 THEN
        WaitTime 5;
    TPWrite "Homing Robot in Manual";
    WaitDI Homing_Button,1;
Homing;
ELSE
        bSafe:=TRUE;
    ENDIF
ENDIF
! Gripper Testing ----> Open and close cylinders
GripperTest;
!ADDITION -----> Velocity setting procedure 
!VelocitySet;
!Test_Rob_Lim;

! Display running hours
servicestring:=GetServiceInfo(ROB_1 \DutyTimeCnt);
TPWrite "DutyTimeCnt for ROB_1: " + servicestring;


IF bSafe=TRUE THEN
    
!! FLexPendant USE         
!Filling Product List ----> For use with Flexpendant
!!FOR i FROM 1 TO NumOfDiffProd DO     
!!    Listitem1{i}.text:=MySpec_array{i}.prod_name;        
!!ENDFOR
      
!! Showing and selecting product from list -----> For use with FlexPendant
!product_type:=UIListView(Listitem1);
        

! HMI-PLC use 
TPWrite "Select Product";
TPWrite "Select No. of Layers - Max 11 layers for 1kg and max 20 layers for product 0.5kg";
WaitUntil Product_Select >0;
WaitUntil Layers_per_Pallet>0;   

! HMI-PLC Use
IF Layers_per_Pallet>0 AND Layers_per_Pallet<23 THEN


!PC Inteface Use
!TPWrite "Select Product";
!WaitUntil product_type >0;
!TPWrite "Select No. of Layers - Max 11 layers for 1kg and max 20 layers for product 0.5kg";
!WaitUntil Layers3>0;
!IF Layers3>0 AND Layers3<23 THEN
    
! HMI USE? ---- Reconsider
product_type:=Product_Select;

!! Writing number of pattern to varialbe "a" ---- > Can be used with PC Interface and HMI PLC SCADA Panel
a:=MySpec_array{product_type}.patt_num;  
 
 !HMI Use
Layers:=Layers_per_Pallet;
 
ELSE
 TPWrite "Fault -Invalid number of layers";
 Stop;
ENDIF
   
! While Cycle for Palletization
WaitUntil EndCycle=0; WaitUntil PLC_Comms=1;  WaitUntil Air_on=1; 

WHILE EndCycle=0 and PLC_Comms=1 and Air_On=1 DO
    
    ! Place Pallet if no Pallet exists on the pre-determined position
  IF PalletSensor=0 AND PalletFull=0 AND bPalletinPlace=FALSE THEN
        PlacePallet;   
  ELSE
      WHILE PalletSensor=1 DO
       !For Loop for fixed number of Layers      
       !FOR j FROM 0 TO MySpec_array{product_type}.NumOfLayers-1 DO
       
       ! For loop for Layers3 with PC Interface or Layers for HMI use Input
        FOR j FROM 0 TO Layers DO
                
            ! If Product (a) = 1 (1kg) Include the Placement of Carton Layers -----> Requires adjustments
            IF a = 1 and j=0 THEN           
                IF (MySpec_array{product_type}.carton_layer <>0) AND ((j) MOD 4)=0 THEN  !((MySpec_array{product_type}.carton_layer))<>0) THEN
                Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j));
                PlaceCarton;
                ENDIF     
            ENDIF
                            
!  Check if you want to mirrorTRUE/FALSE 2)Check number of layers(using division / 4) ------> Requires adjustments for case of carton input in HMI
            IF MySpec_array{product_type}.mirror_layer =TRUE  AND ((j MOD 2)<>0) THEN
                TPWrite "Mirrored";
                Mirror:=TRUE;
            ELSE
                Mirror:=FALSE;
            ENDIF
            
        !   For loop for patterns for each Pick and Place in Layer
        FOR i FROM 1 TO MySpec_array{product_type}.NuminLayer DO      
        ! Check if End Cycle Signal has been given
          IF EndCycle = 1  THEN 
             bCycle_Stop:=TRUE;
             Homing;
             WaitTime 3;
             StopMove;
             TPWrite "Sequence Fault...Awaiting Rectification";
       !     StartMove;
             WaitUntil EndCycle=0;
             WaitUntil Continue=1 OR Restart=1;
                IF Restart=1 THEN 
                !!! Signal for Moving Conveyor
                !!! Signal to byPass ROBOPACK / Labeller
                ClearPath;
                StartMove;
                ExitCycle;
                ELSE
                StorePath;
                p10:=CRobT(\Tool:=tool0 \Wobj:=wobj0); 
                RestoPath;
                StartMove;
                ENDIF
         ! StopMove;
          ELSE
            bCycle_Stop:=FALSE;
          ENDIF
                

! PICK SEQUENCE!################################################################################# 
! If Command has not been given for the removal of the pallet and the Air is On
IF Remove_Pallet=0 and Air_On=1 and PalletSensor=1 THEN
    
    IF Mirror = TRUE AND i=2 THEN
    MoveL Offs(Pick_1_Approach_2,0,350,MySpec_array{product_type}.height*j),PlaceVel,z50,Tooldata_1\WObj:=wobj0; 
    ELSE
    MoveL Offs(Pick_1_Approach_2,0,350,MySpec_array{product_type}.height*j),MaxVel,z50,Tooldata_1\WObj:=wobj0;     
    ENDIF
    SetDO D652_10_DO2,0;
    SetDO D652_10_DO1,1;
    MoveL Pick_1_2,PickVel,fine,Tooldata_1\WObj:=wobj0;
    WaitDI ProductinPlace,1;
!    !Wait for sensors to sense product
  ! WaitDI Gripper_Sensor_1,1;
!    !Wait for sensros to sense product
  !  WaitDI Gripper_Sensor_2,1;
    MoveL Pick_1_2_2,PickVel,fine,Tooldata_1\WObj:=wobj0;
    GripLoad load1;
    Attach1;      
    MoveL Offs(Pick_1_2_2,0,-400,350),MaxVel,z50,Tooldata_1\WObj:=wobj0;
    MoveL Offs(Pick_1_Approach_2,0,350,MySpec_array{product_type}.height*j),vmax,z50,Tooldata_1\WObj:=wobj0;
    IF a=1 THEN
Boxes1:=Boxes1+1;
Trays1:= (Boxes1 * 3);
Products1:= (Trays1 * 6);
ELSEIF a=2 THEN
Boxes2:=Boxes2+1;
!Boxes2:=(Boxes2*6)*3;
ENDIF

ELSEIF Remove_Pallet=1 and PalletSensor=1 THEN
    Homing;
    WaitDI PalletSensor,0;
   ! PlacePallet;
    WaitDI PalletSensor,1;
    ExitCycle;
   ! RETURN;
ELSEIF Air_On=0 THEN
    Homing;
    CheckAir;  
    ExitCycle;
   ! RETURN; 
ELSEIF Remove_Pallet=1 AND Air_On=0 THEN
    Homing;
    CheckAir; 
    WaitDI PalletSensor,0;
   ! PlacePallet;
    WaitDI PalletSensor,1;
   ! RETURN;   
   ExitCycle;
ELSEIF Remove_Pallet=0 AND PalletSensor=0 THEN
    WaitDI PalletSensor,1;
     IF Mirror = TRUE AND i=2 THEN
    MoveL Offs(Pick_1_Approach_2,0,350,MySpec_array{product_type}.height*j),PlaceVel,z50,Tooldata_1\WObj:=wobj0; 
    ELSE
    MoveL Offs(Pick_1_Approach_2,0,350,MySpec_array{product_type}.height*j),MaxVel,z50,Tooldata_1\WObj:=wobj0;     
    ENDIF
    SetDO D652_10_DO2,0;
    SetDO D652_10_DO1,1;
    MoveL Pick_1_2,PickVel,fine,Tooldata_1\WObj:=wobj0;
    WaitDI ProductinPlace,1;
!    !Wait for sensors to sense product
  ! WaitDI Gripper_Sensor_1,1;
!    !Wait for sensros to sense product
  !  WaitDI Gripper_Sensor_2,1;
    MoveL Pick_1_2_2,PickVel,fine,Tooldata_1\WObj:=wobj0;
    GripLoad load1;
    Attach1;      
    MoveL Offs(Pick_1_2_2,0,-400,350),MaxVel,z50,Tooldata_1\WObj:=wobj0;
    MoveL Offs(Pick_1_Approach_2,0,350,MySpec_array{product_type}.height*j),vmax,z50,Tooldata_1\WObj:=wobj0;
    IF a=1 THEN
Boxes1:=Boxes1+1;
Trays1:= (Boxes1 * 3);
Products1:= (Trays1 * 6);
ELSEIF a=2 THEN
Boxes2:=Boxes2+1;
!Boxes2:=(Boxes2*6)*3;
ENDIF
             
ENDIF
    

        
 !PLACE SEQUENCE !################################################################################# 
           
ConfJ\Off;
ConfL\Off;
! Check if Place is Mirrored
 IF Mirror = FALSE THEN   

!! Define target for reachability 
!robtarget_reach:=Offs(RelTool(Place_1_Syn_10,100,0,-250\Rz:=Pattern_all{a,i}.rot),
!Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(-j));
!WaitTime 0.2;
!! Check Reachability (Workobject_1)
!CheckReachable1 robtarget_reach,Tooldata_1;

!IF bReachable=1 THEN  
!TPWrite "Reachable";   
! Once Reachable perform movement
MoveJ Offs(RelTool(Place_1_Syn_10,100,0,-250\Rz:=Pattern_all{a,i}.rot),
Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(-j)),
PlaceVel,z50,Tooldata_1\WObj:=Workobject_1;

! WaitTime 0.5; 
 
MoveJ Offs(RelTool(Place_1_Syn_10,100,0,-250\Rz:=Pattern_all{a,i}.rot),
Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(-j)),
PlaceVel,z50,Tooldata_1\WObj:=Workobject_1;

! WaitTime 0.1;
! SetGO AttachHook1,1;
! SetGO AttachHook2,1;

MoveL Offs(RelTool(Place_1_Syn_10,0,0,-30\Rz:=Pattern_all{a,i}.rot),
Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(-j)),
v500,fine,Tooldata_1\WObj:=Workobject_1;

Dettach;
GripLoad load0;
WaitTime 0.5;


MoveL Offs(RelTool(Place_1_Syn_10,0,0,-250\Rz:=Pattern_all{a,i}.rot),
Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(-j)),vmax,fine,
Tooldata_1\WObj:=Workobject_1;
!ENDIF

!tpWrite "off x: " \NUM:= Pattern_all{a,i}.pos_x;
!TPWrite "Place of prodcut nr"+NumToStr(i,0)+" in layer nr "+NumToStr(j,0);    

! Add in counter


ELSEIF Mirror = TRUE THEN         
! Check which place sequence (i) in order to determine placement of tray
IF i = 1 THEN    
    
    
!! Define target for reachability 
!robtarget_reach:=Offs(RelTool(Place_1_Syn_10_3,0,0,-250\Rz:=Pattern_all{a,i}.rot),
!Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j));

!WaitTime 0.4;
!! Check Reachability (Workobject_1)
!CheckReachable robtarget_reach,Tooldata_1;

!IF bReachable=1 THEN  
!TPWrite "Reachable";  
     
    
MoveJ Offs(RelTool(Place_1_Syn_10_3,200,0,-250\Rz:=Pattern_all{a,i}.rot),
Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j)),
PlaceVel,z50,Tooldata_1\WObj:=Workobject_2;
               
!WaitTime 0.5; 
                 
MoveJ Offs(RelTool(Place_1_Syn_10_3,200,0,-250\Rz:=Pattern_all{a,i}.rot),
    Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j)),
    PlaceVel,z50,Tooldata_1\WObj:=Workobject_2;
    
!WaitTime 0.1; 

MoveL Offs(RelTool(Place_1_Syn_10_3,0,0,-30\Rz:=Pattern_all{a,i}.rot),
    Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j)),
    v500,fine,Tooldata_1\WObj:=Workobject_2;
    
Dettach;
GripLoad load0;
WaitTime 0.5;

 MoveL Offs(RelTool(Place_1_Syn_10_3,0,0,-250\Rz:=Pattern_all{a,i}.rot),
     Pattern_all{a,i}.pos_x,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j)),
     PlaceVel,fine,Tooldata_1\WObj:=Workobject_2;
!ENDIF
 
!tpWrite "off x: " \NUM:= Pattern_all{a,i}.pos_x;
!TPWrite "Place of prodcut nr"+NumToStr(i,0)+" in layer nr "+NumToStr(j,0);    

!! Add in counter

             
ELSE

!!     Define target for reachability 
!robtarget_reach:=Offs(RelTool(Place_2_Synerlink_10,0,0,-250\Rz:=Pattern_all{a,i}.rot),
!    Pattern_all{a,i}.pos_x-400,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j));
!WaitTime 0.2;
! Check Reachability (Workobject_1)
!CheckReachable robtarget_reach,Tooldata_1;

!IF bReachable=1 THEN  
!TPWrite "Reachable";  
                             
MoveJ Offs(RelTool(Place_2_Synerlink_10,-100,0,-250\Rz:=Pattern_all{a,i}.rot),
    Pattern_all{a,i}.pos_x-400,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j)),
    PlaceVel,z50,Tooldata_1\WObj:=Workobject_2;
              
!WaitTime 0.5; 

MoveJ Offs(RelTool(Place_2_Synerlink_10,-100,0,-250\Rz:=Pattern_all{a,i}.rot),
    Pattern_all{a,i}.pos_x-400,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j)),
    PlaceVel,z50,Tooldata_1\WObj:=Workobject_2;
    
!WaitTime 0.1; 
!SetGO AttachHook1,1;
!SetGO AttachHook2,1;


MoveL Offs(RelTool(Place_2_Synerlink_10,0,0,-30\Rz:=Pattern_all{a,i}.rot),
    Pattern_all{a,i}.pos_x-400,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j)),
    v500,fine,Tooldata_1\WObj:=Workobject_2;
    
Dettach;
GripLoad load0;
WaitTime 0.5;

  
MoveL Offs(RelTool(Place_2_Synerlink_10,0,0,-250\Rz:=Pattern_all{a,i}.rot),
    Pattern_all{a,i}.pos_x-400,Pattern_all{a,i}.pos_y,MySpec_array{product_type}.height*(j))
    ,vmax,fine,Tooldata_1\WObj:=Workobject_2;

!ENDIF
! TP WRITE functions 
!tpWrite "off x: " \NUM:= Pattern_all{a,i}.pos_x;
!TPWrite "Place of prodcut nr"+NumToStr(i,0)+" in layer nr "+NumToStr(j,0);

! Add in counter
! -!################################################################################# 
        ENDIF           
     ENDIF          
   ENDFOR
        
            
!! Carton Placement sequence            
!IF (a = 1 and j<>0) and ((j)=((4)-1) or (j)=((8)-1)) THEN 
    
!    ! IF (MySpec_array{product_type}.carton_layer <>0) AND ((j) MOD 4)=0 THEN  !((MySpec_array{product_type}.carton_layer))<>0) THEN
!    Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
!    ! Workobject_Carton.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+100);
!    PlaceCarton;
!ENDIF

    IF j=0 AND C1=1 THEN
    Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=1 AND C2=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=2 AND C3=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=3 AND C4=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=4 AND C5=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=5 AND C6=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=6 AND C7=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=7 AND C8=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=8 AND C9=1 THEN
        Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
    PlaceCarton;
    ELSEIF j=9 AND C10=1 THEN 
     Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
     PlaceCarton;
     ELSEIF j=10 AND C11=1 THEN 
     Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
     PlaceCarton;
     ELSEIF j=11 AND C12=1 THEN 
     Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
     PlaceCarton;
     ELSEIF j=12 AND C13=1 THEN 
     Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
     PlaceCarton;
     ELSEIF j=13 AND C14=1 THEN 
     Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
     PlaceCarton;
     ELSEIF j=14 AND C15=1 THEN 
     Workobject_3.oframe.trans.z:=(MySpec_array{product_type}.height*(j)+160);
     PlaceCarton;
ENDIF
    




TPWrite "End of layer";    
LayerCount:=LayerCount+1;

ENDFOR
         
TPWrite "End of pallet";
LayerCount:=0;
IF a=1 THEN
TPWrite "Num of Products: "+ NumToStr(Products1,0); 
ELSEIF a = 2 THEN 
TPWrite "Num of Products: "+ NumToStr(Products2,0); 
ENDIF

CountPallets:=CountPallets+1;
TPWrite "Num of pallets: "+ NumToStr(CountPallets,0);
MoveL Home_1,v3000,fine,Tooldata_1\WObj:=wobj0; 
SetDO PalletFull,1;
!!! Signal to Move Pallet Conveyor (if any)
WaitDI PalletSensor,0; 
bPalletinPlace:=FALSE;
WaitDI PalletSensor,1;
!!! Signal to StopConveyor (if any)
         
  !!!!!!!!!!!!!!!!!!!!!!!!!! END OF COMMENT QUOTES OLD METHOD!!!!!!!!!!!!!!!!!!!      
      ENDWHILE
ENDIF
ENDWHILE       
ENDIF
         
! ERROR HANDLERS ####################################################################         
ERROR
    IF ERRNO = ERR_WHLSEARCH THEN
    errorid:=ERRNO;
    SetGO ErrorCode,ERRNO;
    SetDO Error_Gen,1;
    StorePath; 
    SetDO Load_Cartons,0;
    MoveJ Cartons_Start_2,vmax,z100,Tooldata_1\WObj:=wobj0;
    WaitDI Cartons_Loaded,1;
    TPWrite "Insert Cartons";
    TPWrite "ErrorID: "+ NumToStr(errorid,0);
    ErrWrite "ERR_WHLSEARCH","Error while searching Cartons";
   ! ErrWrite \W , "Warning", " Warning Message";
   ! ErrWrite \I , "Information","Information Message";
    WaitDI Reset_Exec_Error,1;
    SetDO Error_Gen,0;
    !ErrLog errorid,ERRSTR_TASK,arg,ERRSTR_CONTEXT,ERRSTR_UNUSED,ERRSTR_UNUSED;
    !ErrLog errorid\W,ERRSTR_TASK,arg,ERRSTR_CONTEXT,ERRSTR_UNUSED,ERRSTR_UNUSED;
    !SetDO Load_Cartons,1;
    RestoPath;
    ClearPath;
    StartMove;
    RETRY;
    ENDIF
    IF ERRNO=ERR_EXCRTYMAX THEN
    errorid:=ERRNO;
    SetGO ErrorCode,ERRNO;
    TPWrite "Restore System Manually";
    TPWrite "ErrorID: "+ NumToStr(errorid,0);
    ErrWrite "MAX TRIES FOR RESTORING ERROR","Error while searching Cartons";
    WaitDI Reset_Exec_Error,1;
    RETURN;
    ENDIF 
    IF ERRNO=ERR_PATH_STOP THEN
    errorid:=ERRNO;
    SetGO ErrorCode,ERRNO;
    TPWrite "Restore System Manually";
    TPWrite "ErrorID: "+ NumToStr(errorid,0);
    StorePath;
      !  SetDO LoadTrays,0;
        Homing;
        WaitDI Reset_Exec_Error,1;
       ! TPWrite "Insert Trays";
        !SetDO LoadTrays,0;
        RestoPath;
        ClearPath;
        StartMove;
        RETRY;
    ENDIF
    IF ERRNO=ERR_COLL_STOP THEN
        errorid:=ERRNO;
    SetGO ErrorCode,ERRNO;
    TPWrite "Restore System Manually";
    TPWrite "ErrorID: "+ NumToStr(errorid,0);
        StorePath;
        !SetDO LoadTrays,0;
        MotionSup\Off;
        Homing;
        MotionSup\On;
        WaitTime 5; 
       ! WaitUntil Trigger=0;
        WaitDI Reset_Exec_Error,1;
       ! TPWrite "Insert Trays";
       ! SetDO LoadTrays,0;
        RestoPath;
        ClearPath;
        StartMove;
        RETRY;
    ENDIF
    IF ERRNO=50204 THEN
    errorid:=ERRNO;
    SetGO ErrorCode,ERRNO;
    TPWrite "Restore System Manually";
    TPWrite "ErrorID: "+ NumToStr(errorid,0);
        StorePath;
  !      SetDO LoadTrays,0;
        Homing;
        WaitDI Reset_Exec_Error,1;
        TPWrite "Insert Trays";
        RestoPath;
        ClearPath;
        StartMove;
        RETRY; 
         ENDIF
        IF ERRNO=ERR_ROBLIMIT  THEN 
        errorid:=ERRNO;
        SetGO ErrorCode,ERRNO;
        TPWrite "Restore System Manually";
        TPWrite "ErrorID: "+ NumToStr(errorid,0);
        StorePath;
        Homing;
          bReachable:=2;
          TPWrite "Provide New Target";
        WaitDI Reset_Exec_Error,1;
        TPWrite "Insert Trays";
        RestoPath;
        ClearPath;
        StartMove;
        TRYNEXT;
      ENDIF
          IF ERRNO=ERR_OUTSIDE_REACH  THEN 
          bReachable:=2;
          errorid:=ERRNO;
        SetGO ErrorCode,ERRNO;
        TPWrite "Restore System Manually";
        TPWrite "ErrorID: "+ NumToStr(errorid,0);
        StorePath;
        Homing;
          !bReachable:=2;
          TPWrite "Provide New Target";
        WaitDI Reset_Exec_Error,1;
        TPWrite "Insert Trays";
        RestoPath;
         ClearPath;
        StartMove;
        TRYNEXT;
      ENDIF
        IF ERRNO=50050  THEN 
         ! bReachable:=2;
          errorid:=ERRNO;
        SetGO ErrorCode,ERRNO;
        TPWrite "Restore System Manually";
        TPWrite "ErrorID: "+ NumToStr(errorid,0);
        StorePath;
        Homing;
          !bReachable:=2;
          TPWrite "Provide New Target";
        WaitDI Reset_Exec_Error,1;
        TPWrite "Insert Trays";
        RestoPath;
        ClearPath;
        StartMove;
       TRYNEXT;
      ENDIF
       IF ERRNO=ERR_OUTOFBND  THEN 
         ! bReachable:=2;
          errorid:=ERRNO;
        SetGO ErrorCode,ERRNO;
        TPWrite "Restore System Manually";
        TPWrite "ErrorID: "+ NumToStr(errorid,0);
        StorePath;
        Homing;
        TPWrite "Select Product From Screen";
        WHILE a=0 DO 
        a:=MySpec_array{product_type}.patt_num; 
        WaitUntil a>0;
        ENDWHILE
          !bReachable:=2;
        WaitDI Reset_Exec_Error,1;
        RestoPath;
        ClearPath;
        StartMove;
       TRYNEXT;
      ENDIF
    ENDPROC
    

ENDMODULE
