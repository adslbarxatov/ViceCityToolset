// COMPLETED //

// Общие параметры массива аудиоскриптов
union SD_AudioScripts
	{
	struct AU_Formatted
		{
		ulong AU_BlockSize;		// Размер блока
		ulong AU_SubBlockSize;	// Размер субблока
		uchar AU_BlockName[4];	// Имя блока (AUD)
		ulong AU_SubBlock1Size;	// Размер оставшегося субблока
		ulong AU_ScriptsCount;	// Количество скриптов
		} AU;

	uchar AU_Raw [sizeof (struct AU_Formatted)];
	};

// Массив аудиоскриптов
union SD_AudioScriptStructure
	{
	struct AS_Formatted
		{
		ulong AS_AudioIndex;	// Номер аудиоэффекта
		ulong AS_AudioNumber;
		// 0 - alarm, 2 - police cell open/close, 4 - garage gate open/close, 6 - 'No escape' snore, 11 - shooting range target,
		// 36 - 'Alloy Wheels of Steel' bikes, 37 - 'Messing with the Man' bikes, 38 - 'Hog tied' bikes, 39 - unused bikes sound,
		// 40-42 - Malibu club, 43-45 - Pole position, 46 - Church, 51 - Fountain
		float AS_PositionX;		// Координаты эффекта
		float AS_PositionY;
		float AS_PositionZ;
		ulong AS_AudioEntity;	// Сущность
		} AS;

	uchar AS_Raw[1];
	};
