// COMPLETED //

// Количество описателей зон рестарта типа "госпиталь"
#define SD_RL_H_Count	8

// Количество описателей зон рестарта типа "полицейский участок"
#define SD_RL_P_Count	8

// Блок зон респауна
union SD_RestartLocations
	{
	struct RL_Formatted
		{
		ulong RL_BlockSize;			// Размер блока (0x0128)
		ulong RL_SubBlockSize;		// Размер субблока (0x0124)
		uchar RL_BlockName[4];		// Название блока (RST)
		ulong RL_SubBlock1Size;		// Размер оставшегося блока (0x011C)

		// Зоны возрождения типа "госпиталь"
		struct RL_Hospital
			{
			float RL_H_X;			// Расположение
			float RL_H_Y;
			float RL_H_Z;
			float RL_H_Rotation;	// Поворот
			} RL_H[SD_RL_H_Count];

		// Зоны возрождения типа "полицейский участок"
		struct RL_Police
			{
			float RL_P_X;			// Расположение
			float RL_P_Y;
			float RL_P_Z;
			float RL_P_Rotation;	// Поворот
			} RL_P[SD_RL_P_Count];

		uint RL_HospitalRPCount;		// Число госпиталей
		uint RL_PoliceRPCount;			// Число полицейских участков
		uchar RL_OverrideNextRestart;	// Переопределение зоны следующего возрождения
		uchar RL_Unused01[3];
		float RL_OverrideX;				// Координаты переопределённой зоны
		float RL_OverrideY;
		float RL_OverrideZ;
		float RL_OverrideRotation;		// Угол переопределённого поворота
		uchar RL_Unused02[2];
		uchar RL_OverrideHospitalLevel;	// Уровни переопределения
		uchar RL_OverridePoliceLevel;
		} RL;

	char RL_Raw [sizeof (struct RL_Formatted)];
	};
