// INCOMPLETED //

// Количество описаний оружия игрока
#define SD_PPS_WD_Count	10

// Общие параметры массива игроков
union SD_PedPlayer
	{
	struct PPL_Formatted
		{
		ulong PPL_BlockSize;		// Размер блока (0x0708)
		ulong PPL_SubBlock1Size;	// Размер субблока 1 (0x0703)
		ulong PPL_NumberOfPlayers;	// Число игроков (1)
		} PPL;

	uchar PPL_Raw [sizeof (struct PPL_Formatted)];
	};

// Массив игроков
union SD_PedPlayerStructure
	{
	struct PPS_Formatted
		{
		ulong PPS_PedType;							// Тип игрока (0)
		uint PPS_PedModelIndex;						// Модель игрока (0)
		ulong PPS_PlayerIndex;						// Номер игрока
		uchar PPS_Unknown01[52];
		float PPS_X;								// Положение игрока
		float PPS_Y;
		float PPS_Z;
		uchar PPS_Unknown02[788];
		float PPS_CurrentHealth;					// Текущее здоровье (не изменяется вне игры)
		float PPS_CurrentArmor;						// Текущее состояние брони
		uchar PPS_Unknown03[172];

		// Структура описателя оружия
		struct PPS_WeaponData
			{
			ulong PPS_WD_WeaponNumber;	// Номер оружия по порядку
			uchar PPS_WD_Unknown01[4];
			ulong PPS_WD_HolderSize;	// Размер обоймы
			ulong PPS_WD_BulletsCount;	// Число пуль
			uchar PPS_WD_Unknown02[8];
			} PPS_WD[SD_PPS_WD_Count];

		uchar PPS_Unknown04[12];
		uchar PPS_SelectedWeapon;				// Текущее выьранное оружие
		uchar PPS_Unknown05[255];
		float PPS_MaxStamina;					// Максимальная устойчивость
		uchar PPS_Unknown06[28];
		slong PPS_TargettableObjects[4];		// Объекты, на которых работает автоприцел
		uchar PPS_Unknown07[164];
		ulong PPS_MaxWantedLevel;				// Максимальный уровень розыска (рекомендуется 4)
		ulong PPS_MaxCriminalPoints;			// Максимальная величина интереса полиции (рекомендуется 1800)
		uchar PPS_SuitName[22];					// Текущий костюм
		} PPS;

	uchar PPS_Raw [1];
	};
