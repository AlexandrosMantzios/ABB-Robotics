MODULE operation_mode
    
    PROC main()
    
IF OpMode() = OP_AUTO  THEN
SetDO Robot_Manual,0;
SetDO Robot_Auto,1;
ELSEIF OpMode() = OP_MAN_PROG THEN
SetDO Robot_Auto,0;
SetDO Robot_Manual,1;
ENDIF


    ENDPROC
    
ENDMODULE