## State Machine Name: HVAC_DF
### States:
* Initial State
* IDLE
* BLOWER
* STOP_BLOWER
* PREHEAT
* HEATING
* STOP_HEAT
* PRECOOL
* COOLING
* STOP_COOL
#### State: Initial State
##### No Entry Actions
##### No Exit Actions
#### State: IDLE
##### Entry Actions
* HvacEnterIdle

##### No Exit Actions
#### State: BLOWER
##### Entry Actions
* HvacEnterBlower

##### No Exit Actions
#### State: STOP_BLOWER
##### No Entry Actions
##### No Exit Actions
#### State: PREHEAT
##### Entry Actions
* HvacEnterPreheat

##### No Exit Actions
#### State: HEATING
##### Entry Actions
* HvacEnterHeating

##### No Exit Actions
#### State: STOP_HEAT
##### Entry Actions
* HvacEnterStopHeat

##### No Exit Actions
#### State: PRECOOL
##### Entry Actions
* HvacEnterPrecool

##### No Exit Actions
#### State: COOLING
##### Entry Actions
* HvacEnterCooling

##### No Exit Actions
#### State: STOP_COOL
##### Entry Actions
* HvacEnterStopCool

##### No Exit Actions
### Transitions:
* InitIdle
* IdleToBlower
* BlowerToStopBlower
* StopBlowerToIdle
* BlowerToPreheat
* BlowerToPrecool
* PreheatToBlower
* PrecoolToBlower
* PreheatToHeating
* HeatingToStopHeat
* StopHeatToHeating
* StopHeatToBlower
* PrecoolToCooling
* CoolingToStopCool
* StopCoolToCooling
* StopCoolToBlower
#### Transition: InitIdle
##### Output Actions
* HvacInit
##### Condition
 IsStartupTimeElapsed
#### Transition: IdleToBlower
##### No Output Actions
##### Condition
 (  IsBlowerRequired AND  CanBlow ) 
#### Transition: BlowerToStopBlower
##### No Output Actions
##### Condition
 ( ! IsBlowerRequired OR ! CanBlow ) 
#### Transition: StopBlowerToIdle
##### No Output Actions
##### Condition
 IsStartupTimeElapsed
#### Transition: BlowerToPreheat
##### No Output Actions
##### Condition
 (  IsInHeatRange AND  CanHeat ) 
#### Transition: BlowerToPrecool
##### No Output Actions
##### Condition
 (  IsInCoolRange AND  CanCool ) 
#### Transition: PreheatToBlower
##### No Output Actions
##### Condition
 ( ! IsInHeatRange OR ! CanHeat ) 
#### Transition: PrecoolToBlower
##### No Output Actions
##### Condition
 ( ! IsInCoolRange OR ! CanCool ) 
#### Transition: PreheatToHeating
##### No Output Actions
##### Condition
 (  IsBlowerOn OR  IsBlowerInDelayedStart ) 
#### Transition: HeatingToStopHeat
##### No Output Actions
##### Condition
 ( ! IsInHeatRange OR ! CanHeat OR  ( ! IsBlowerOn AND ! IsBlowerInDelayedStart )  ) 
#### Transition: StopHeatToHeating
##### No Output Actions
##### Condition
 (  IsInHeatRange AND  CanHeat AND  IsHeatOn AND  (  IsBlowerOn OR  IsBlowerInDelayedStart )  ) 
#### Transition: StopHeatToBlower
##### No Output Actions
##### Condition
! IsHeatOn
#### Transition: PrecoolToCooling
##### No Output Actions
##### Condition
 IsBlowerOn
#### Transition: CoolingToStopCool
##### No Output Actions
##### Condition
 ( ! IsInCoolRange OR ! CanCool OR ! IsBlowerOn ) 
#### Transition: StopCoolToCooling
##### No Output Actions
##### Condition
 (  IsInCoolRange AND  CanCool AND  IsCoolOn AND  IsBlowerOn ) 
#### Transition: StopCoolToBlower
##### No Output Actions
##### Condition
! IsCoolOn
![image](StateMachine_0.png)
## State Machine Name: Blower_DF
### States:
* Initial State
* IDLE
* PRE-START
* DELAYED_START
* OPEN_DAMPER
* PROVE_AIR
* BLOWER_ON
* DELAYED_STOP
* RECOVER_AIR
#### State: Initial State
##### No Entry Actions
##### No Exit Actions
#### State: IDLE
##### Entry Actions
* BlowerEnterIdle

##### No Exit Actions
#### State: PRE-START
##### Entry Actions
* BlowerEnterPreStart

##### No Exit Actions
#### State: DELAYED_START
##### Entry Actions
* BlowerEnterDelayedStart

##### No Exit Actions
#### State: OPEN_DAMPER
##### Entry Actions
* BlowerEnterOpenDamper

##### No Exit Actions
#### State: PROVE_AIR
##### Entry Actions
* BlowerEnterProveAir

##### No Exit Actions
#### State: BLOWER_ON
##### Entry Actions
* BlowerEnterBlowerOn

##### No Exit Actions
#### State: DELAYED_STOP
##### Entry Actions
* BlowerEnterDelayedStop

##### No Exit Actions
#### State: RECOVER_AIR
##### Entry Actions
* BlowerEnterRecoverAir

##### No Exit Actions
### Transitions:
* InitIdle
* IdleToPreStart
* PreStartToDelayedStart
* PreStartToOpenDamper
* DelayedStartToOpenDamper
* OpenDamperToProveAir
* ProveAirToOpenDamper
* ProveAirToBlowerOn
* BlowerOnToDelayedStop
* BlowerOnToIdle
* DelayedStopToIdle
* DelayedStartToIdle
* DelayedStopToBlowerOn
* BlowerOnToRecoverAir
* RecoverAirToProveAir
* RecoverAirToOpenDamper
* RecoverAirToBlowerOn
* OpenDamperToIdle
* ProveAirToIdle
#### Transition: InitIdle
##### Output Actions
* BlowerInitIdle
##### Condition
 IsStartupTimeElapsed
#### Transition: IdleToPreStart
##### No Output Actions
##### Condition
 IsHvacCallingForBlower
#### Transition: PreStartToDelayedStart
##### No Output Actions
##### Condition
 IsDelayedStartEnabled
#### Transition: PreStartToOpenDamper
##### No Output Actions
##### Condition
! IsDelayedStartEnabled
#### Transition: DelayedStartToOpenDamper
##### No Output Actions
##### Condition
 IsDelayedStartTimerExpired
#### Transition: OpenDamperToProveAir
##### No Output Actions
##### Condition
 IsDamperEndReceived
#### Transition: ProveAirToOpenDamper
##### No Output Actions
##### Condition
! IsDamperEndReceived
#### Transition: ProveAirToBlowerOn
##### No Output Actions
##### Condition
 IsAirProvingReceived
#### Transition: BlowerOnToDelayedStop
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForBlower AND  IsDelayedStopEnabled ) 
#### Transition: BlowerOnToIdle
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForBlower AND ! IsDelayedStopEnabled ) 
#### Transition: DelayedStopToIdle
##### No Output Actions
##### Condition
 IsDelayedStopTimerExpired
#### Transition: DelayedStartToIdle
##### No Output Actions
##### Condition
! IsHvacCallingForBlower
#### Transition: DelayedStopToBlowerOn
##### No Output Actions
##### Condition
 IsHvacCallingForBlower
#### Transition: BlowerOnToRecoverAir
##### No Output Actions
##### Condition
 ( ! IsAirProvingReceived OR ! IsDamperEndReceived ) 
#### Transition: RecoverAirToProveAir
##### No Output Actions
##### Condition
 (  IsRecoverAirTimerExpired AND ! IsAirProvingReceived ) 
#### Transition: RecoverAirToOpenDamper
##### No Output Actions
##### Condition
 (  IsRecoverAirTimerExpired AND ! IsDamperEndReceived ) 
#### Transition: RecoverAirToBlowerOn
##### No Output Actions
##### Condition
 (  IsDamperEndReceived AND  IsAirProvingReceived ) 
#### Transition: OpenDamperToIdle
##### No Output Actions
##### Condition
! IsHvacCallingForBlower
#### Transition: ProveAirToIdle
##### No Output Actions
##### Condition
! IsHvacCallingForBlower
![image](StateMachine_1.png)
## State Machine Name: Heating_DF
### States:
* Initial State
* IDLE
* DF_PRE_AIR
* ON_NORMAL
* ON_TESTING
* STOP_DF
* DF_PRE_PGAS
* DF_PRE_SPARK
* DF_PRE_MGAS
* DF_SPARK_OFF
#### State: Initial State
##### No Entry Actions
##### No Exit Actions
#### State: IDLE
##### Entry Actions
* HeatEnterIdle

##### No Exit Actions
#### State: DF_PRE_AIR
##### Entry Actions
* HeatEnterPreAir

##### No Exit Actions
#### State: ON_NORMAL
##### Entry Actions
* HeatEnterOnNormal

##### No Exit Actions
#### State: ON_TESTING
##### Entry Actions
* HeatEnterOnTesting

##### No Exit Actions
#### State: STOP_DF
##### Entry Actions
* HeatEnterStopDf

##### No Exit Actions
#### State: DF_PRE_PGAS
##### Entry Actions
* HeatEnterPrePGas

##### No Exit Actions
#### State: DF_PRE_SPARK
##### Entry Actions
* HeatEnterPreSpark

##### No Exit Actions
#### State: DF_PRE_MGAS
##### Entry Actions
* HeatEnterPreMGas

##### No Exit Actions
#### State: DF_SPARK_OFF
##### Entry Actions
* HeatEnterSparkOff

##### No Exit Actions
### Transitions:
* InitIdle
* IdleToStartDf
* OnNormalToStopDf
* OnTestingToStopDf
* StopDfToIdle
* DfPreAirToPrePGas
* DfPrePGasToPreSpark
* DfPreSparkToPreMGas
* DfPreMGasToSparkOff
* DfSparkOffToOnNormal
* DfSparkOffToOnTesting
* DfPreAirToStop
* DfPrePGasToStop
* DfPreSparkToStop
* DfPreMGasToStop
* DfSparkOffToStop
#### Transition: InitIdle
##### No Output Actions
##### Condition
 IsStartupTimeElapsed
#### Transition: IdleToStartDf
##### No Output Actions
##### Condition
 IsHvacCallingForHeat
#### Transition: OnNormalToStopDf
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForHeat OR ! IsBlowerOn ) 
#### Transition: OnTestingToStopDf
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForHeat OR ! IsBlowerOn ) 
#### Transition: StopDfToIdle
##### No Output Actions
##### Condition
 IsStartupTimeElapsed
#### Transition: DfPreAirToPrePGas
##### No Output Actions
##### Condition
 IsBlowerOn
#### Transition: DfPrePGasToPreSpark
##### No Output Actions
##### Condition
 IsPilotGasInReceived
#### Transition: DfPreSparkToPreMGas
##### No Output Actions
##### Condition
 IsSparkInReceived
#### Transition: DfPreMGasToSparkOff
##### No Output Actions
##### Condition
 IsMainGasInReceived
#### Transition: DfSparkOffToOnNormal
##### No Output Actions
##### Condition
 ( ! IsSparkInReceived AND ! IsTestingModeRequested ) 
#### Transition: DfSparkOffToOnTesting
##### No Output Actions
##### Condition
 ( ! IsSparkInReceived AND  IsTestingModeRequested ) 
#### Transition: DfPreAirToStop
##### No Output Actions
##### Condition
! IsHvacCallingForHeat
#### Transition: DfPrePGasToStop
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForHeat OR ! IsBlowerOn ) 
#### Transition: DfPreSparkToStop
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForHeat OR ! IsBlowerOn ) 
#### Transition: DfPreMGasToStop
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForHeat OR ! IsBlowerOn ) 
#### Transition: DfSparkOffToStop
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForHeat OR ! IsBlowerOn ) 
![image](StateMachine_2.png)
## State Machine Name: Cooling_DF
### States:
* Initial State
* IDLE
* START_COOLING
* COOL_EVAP
* ON_TESTING
* COOL_STAGE1
* STOP_COOLING
* COOL_STAGE2
* COOL_STAGE3
#### State: Initial State
##### No Entry Actions
##### No Exit Actions
#### State: IDLE
##### Entry Actions
* CoolingEnterIdle

##### No Exit Actions
#### State: START_COOLING
##### Entry Actions
* CoolingEnterStart

##### No Exit Actions
#### State: COOL_EVAP
##### Entry Actions
* CoolingEnterEvap

##### No Exit Actions
#### State: ON_TESTING
##### Entry Actions
* CoolingEnterTest

##### No Exit Actions
#### State: COOL_STAGE1
##### Entry Actions
* CoolingEnterStage1

##### No Exit Actions
#### State: STOP_COOLING
##### No Entry Actions
##### Exit Actions
* CoolingExitStop

#### State: COOL_STAGE2
##### Entry Actions
* CoolingEnterStage2

##### No Exit Actions
#### State: COOL_STAGE3
##### Entry Actions
* CoolingEnterStage3

##### No Exit Actions
### Transitions:
* InitIdle
* IdleToStartCooling
* StartCoolingToEvap
* StartCoolingToOnTesting
* OnTestingToStopCooling
* Stage1ToStopCooling
* EvapToStopCooling
* StopCoolingToIdle
* EvapToStage1
* Stage1ToStage2
* Stage2ToStage3
* Stage2ToStopCooling
* Stage3ToStopCooling
* StartCoolingToStage1
#### Transition: InitIdle
##### Output Actions
* CoolingInitIdle
##### Condition
 IsStartupTimeElapsed
#### Transition: IdleToStartCooling
##### No Output Actions
##### Condition
 IsHvacCallingForCooling
#### Transition: StartCoolingToEvap
##### No Output Actions
##### Condition
 (  IsBlowerOn AND  IsEvapEnabled AND ! IsOaTempTooLowForEvap AND ! IsTestingModeRequested ) 
#### Transition: StartCoolingToOnTesting
##### No Output Actions
##### Condition
 (  IsBlowerOn AND  IsTestingModeRequested ) 
#### Transition: OnTestingToStopCooling
##### No Output Actions
##### Condition
 ( ! IsTestingModeRequested OR ! IsBlowerOn ) 
#### Transition: Stage1ToStopCooling
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForCooling OR ! IsBlowerOn ) 
#### Transition: EvapToStopCooling
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForCooling OR ! IsBlowerOn ) 
#### Transition: StopCoolingToIdle
##### No Output Actions
##### Condition
 (  IsAllMinOnTimesExpired OR ! IsQuickStopRequired ) 
#### Transition: EvapToStage1
##### No Output Actions
##### Condition
 (  (  IsEvapHeadStartTimerExpired OR  IsOaTempTooLowForEvap )  AND  IsY1MinOffTimeExpired ) 
#### Transition: Stage1ToStage2
##### No Output Actions
##### Condition
 IsStage2Required
#### Transition: Stage2ToStage3
##### No Output Actions
##### Condition
 IsStage3Required
#### Transition: Stage2ToStopCooling
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForCooling OR ! IsBlowerOn ) 
#### Transition: Stage3ToStopCooling
##### No Output Actions
##### Condition
 ( ! IsHvacCallingForCooling OR ! IsBlowerOn ) 
#### Transition: StartCoolingToStage1
##### No Output Actions
##### Condition
 (  IsBlowerOn AND ! IsTestingModeRequested AND  ( ! IsEvapEnabled OR  IsOaTempTooLowForEvap )  ) 
![image](StateMachine_3.png)
