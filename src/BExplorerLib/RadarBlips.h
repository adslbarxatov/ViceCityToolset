// COMPLETED //

// Количество описателей радарных указателей
#define SD_RB_S_Count	75

// Блок радарных указателей
union SD_RadarBlips
	{
	struct RB_Formatted
		{
		ulong RB_BlockSize;			// Размер блока (0x0E1C)
		ulong RB_SubBlockSize;		// Размер субблока (0x0E18)
		uchar RB_BlockName[4];		// Название блока (RDR)
		ulong RB_SubBlock2Size;		// Размер оставшегося блока

		// Описатели радарных указателей
		struct RB_Structure
			{
			ulong RB_S_Color;		// Цвет (0-6 - фиксированные цвета, остальное - RGB)
			float RB_S_DebugSphereOffset;	// Не используется, всегда 1.0
			ulong RB_S_BlipType;	// Тип метки: 0 - none, 1 - car, 2 - char, 3 - object/pickup, 4 - coord, 5 - contact point
			ulong RB_S_Entity;		// 0, если не объект
			float RB_S_RadarX;		// Положение точки на радаре (не для entity)
			float RB_S_RadarY;
			float RB_S_MapX;		// Положение точки на карте (не для entity)
			float RB_S_MapY;
			float RB_S_MapZ;
			uchar RB_S_Unused01[2];
			uchar RB_S_Brightness;	// Яркость (1 - затемнить; для фиксированных цветов; не для спрайтов)
			uchar RB_S_IsActive;	// Точка активна
			uchar RB_S_ShortRange;	// Скрывать при выходе за границы радара
			uchar RB_S_Unused03;
			uint RB_S_Size;			// Размер
			uint RB_S_ShowType;		// Способ отображения: 0 - скрыть всё; 1 - только на карте; 2 - только на радаре; 3 - показать всё
			uint RB_S_SpriteNumber;	// Тип спрайта (если есть)
			// 4 - Avery, 5 - Bikers (буква B), 6 - Cortez, 7 - Diaz, 8 - Kent Paul, 9 - Rosenberg, 10 - Phil, 11 - Bikers (пика),
			// 12 - Boatyard, 13 - Malibu, 14 - Cubans, 15 - Film studio, 16 - Ammu-Nation, 17 - Haitians, 18 - Hardware store,
			// 19 - Save house, 20 - Pole position, 21 - Ice cream, 22 - Kaufman cabs, 23 - Lovefist, 24 - Printworks,
			// 26 - Sunshine auto, 27 - Pay'n'spray, 28 - Clothes shop, 29 - Versetti estate, 30 - Phone mission, 31 - 39 - radio stations
			} RB_S[SD_RB_S_Count];
		} RB;

	uchar RB_Raw [sizeof (struct RB_Formatted)];
	};
