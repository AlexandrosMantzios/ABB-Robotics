MODULE Counter
    PERS num counter1; 
    
    PROC Main()
       counter1:=0;
       WHILE TRUE DO
        IF Table_Position_Sensor=1 THEN 
            counter1:=counter1+1;
            WaitTime 0.2;
            IF counter1=3 THEN
                SetDO Full_Product_Table,1;
                counter1:=0;
                WaitTime 0.2;
                SetDO Full_Product_Table,0;        
        ENDIF
             
        ENDIF     
       ENDWHILE
      
        
    ENDPROC
    
    
ENDMODULE