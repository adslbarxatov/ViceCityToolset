// COMPLETED //

// Общие параметры массива скриптов
union SD_ScriptPool
	{
	struct SC_Formatted
		{
		ulong SC_ScriptsCount;	// Количество скриптов
		} SC;

	uchar SC_Raw [sizeof (struct SC_Formatted)];
	};

// Массив скриптов
union SD_ScriptStructure
	{
	struct SS_Formatted
		{
		ulong SS_PointerToNextScript;	// Указатель на предыдущий скрипт
		ulong SS_PointerToPrevScript;	// Указатель на следующий скрипт
		uchar SS_ScriptName[8];			// Название скрипта
		ulong SS_CurrentInstructionPointer;	// Указатель на выполняемую команду
		ulong SS_ReturnStack[6];		// Стэк результатов
		uint SS_StackCounter;			// Счётчик стека
		uint SS_Unused01;
		ulong SS_LocalVariables[16];	// Локальные переменные
		ulong SS_TimerA;				// Таймеры скриптов
		ulong SS_TimerB;
		uchar SS_IsActive;				// Скрипт активен
		uchar SS_IfStatementResult;		// Результат выполнения выражения If
		uchar SS_MissionCleanup;		// Сброс перед выполнением миссии
		uchar SS_SkipWakeTime;
		ulong SS_WakeTime;				// Время приостановки работы скрипта, мс
		uint SS_IfCheckResult;			// Результат проверки выражения If
		uchar SS_NotFlag;				// Флаг "не"
		uchar SS_DeathArrestState;		// Арест или смерть во время миссии
		uchar SS_DeathArrestExecuted;	// Тест на арест или смерть
		uchar SS_MissionFlag;			// Флаг миссии
		uint SS_Unused02;
		} SS;

	uchar SS_Raw[1];
	};
