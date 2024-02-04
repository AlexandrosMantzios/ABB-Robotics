MODULE Background
    
     PROC Main() 
        
        
        
        IF PalletFull=1 THEN 
            WaitUntil PalletSensor = 0; 
            SetDO PalletFull,0; 
            WaitTime 3; 
            WaitUntil PalletSensor = 1;
            !SetDO PalletFull,0; 
        ELSEIF Remove_Pallet=1 THEN 
            WaitUntil PalletSensor = 0; 
            SetDO PalletFull,0;
            WaitTime 3;
            WaitUntil PalletSensor=1;
           ! SetDO PalletFull,0;
        ENDIF
        
        
        
        
    ENDPROC
    
    
ENDMODULE