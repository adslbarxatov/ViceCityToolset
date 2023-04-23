// INCOMPLETED //

// Общие параметры массива скрипт-контролируемых объектов
union SD_ObjectPool
	{
	struct OP_Formatted
		{
		ulong OP_BlockSize;			// Размер блока
		ulong OP_SubBlockSize;		// Размер субблока
		ulong OP_ObjectsCount;		// Число объектов
		} OP;

	uchar OP_Raw [sizeof (struct OP_Formatted)];
	};

// Массив скрипт-контролируемых объектов
union SD_ObjectStructure
	{
	struct OS_Formatted
		{
		uint OS_ModelID;		// Модель объекта
		ulong OS_ObjectHandle;	// Указатель объекта
		float OS_X;				// Координаты объекта
		float OS_Y;
		float OS_Z;
		uchar OS_Unknown01[40];
		uchar OS_ObjectSubtype;
		uchar OS_PickupFlag;	// Является ли объект собираемым
		uchar OS_PickupInShopFlag;
		uchar US_PickupInShopOOSFlag;
		uchar OS_Unknown02[26];
		} OS;

	uchar OS_Raw[1];
	};
