@echo off

call 1_InstrumentON.bat %1

call 2_PerformanceON.bat %1

call 3_RunTestsDebug.bat %1

call 4_PerformanceOFF.bat %1