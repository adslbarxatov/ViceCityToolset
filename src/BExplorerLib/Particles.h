// INCOMPLETED //

// Общие параметры массива эффектов анимации
union SD_Particles
	{
	struct PR_Formatted
		{
		ulong PR_BlockSize;			// Размер блока
		ulong PR_SubBlockSize;		// Размер субблока
		ulong PR_ParticlesCount;	// Число объектов
		} PR;

	uchar PR_Raw [sizeof (struct PR_Formatted)];
	};

// Массив эффектов анимации
union SD_ParticleDescription
	{
	struct PRD_Formatted
		{
		uchar PRD_Unknown01[48];
		float PRD_PositionX;		// Расположение
		float PRD_PositionY;
		float PRD_PositionZ;
		uchar PRD_Unknown02[28];
		ulong PRD_ParticleType1;	// Типы эффекта
		ulong PRD_ParticleType2;
		uchar PRD_Visibility;		// Видимость
		uchar PRD_Sparsity;			// Разреженность
		uint PRD_SparsityCounter;	// Счётчик разреженности
		uchar PRD_Unknown03[4];
		float PRD_StrengthX;		// Координаты усилия?
		float PRD_StrengthY;
		float PRD_StrengthZ;
		uchar PRD_Unknown04[4];
		float PRD_Scale;			// Масштаб
		uchar PRD_Unknown05[8];
		} PRD;

	uchar PRD_Raw[1];
	};
