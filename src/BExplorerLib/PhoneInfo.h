// COMPLETED //

// Количество описателей телефонов-автоматов
#define SD_PI_PS_Count	50

// Блок телефонов-автоматов
union SD_PhoneInfo
	{
	struct PI_Formatted
		{
		ulong PI_BlockSize;			// Размер блока (0x0A34)
		ulong PI_SubBlockSize;		// Размер субблока (0x0A30)
		ulong PI_PhonesCount;		// Количество телефонов
		ulong PI_ActivePhonesCount;	// Количество активных телефонов

		// Описатели телефонов-автоматов
		struct PI_PhoneStructure
			{
			float PI_PS_X;							//  Расположение
			float PI_PS_Y;
			float PI_PS_Z;
			ulong PI_PS_PhoneMessagePointers[6];	// Сообщения gxt, закреплённые за телефоном
			ulong PI_PS_GameTimerOnMsgEnd;			// Время игры при последнем обращении
			ulong PI_PS_PhoneObjectIndex;			// Индекс объекта телефона
			ulong PI_PS_Status;						// Состояние
			// 3 - не звонит, 9 - звонит, остальные не используются
			// 4 = ringing, message just once; 5 = ringing, message repeated, phone was never picked up;
			// 6 = ringing, message repeated, phone was picked up before; 7 = no ringing, finished state of state 4;
			// 8 = no ringing, finished state of states 5 and 6
			uchar PI_PS_IsPlayerNearFrom;			// Флаг близости игрока (звонок слышен)
			uchar PI_PS_Unused01[3];
			} PI_PS[SD_PI_PS_Count];
		} PI;

	uchar PI_Raw [sizeof (struct PI_Formatted)];
	};
