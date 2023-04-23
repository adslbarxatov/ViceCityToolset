// COMPLETED //

// Общие параметры сохранения
union SD_DefaultParameters
	{
	struct DP_Formatted
		{
		ulong DP_BlockSize;				// Размер блока (в сумме с SB, SBA, SBB)
		wchar_t DP_SaveName[24];		// Название файла сохранения
		uint DP_Year;					// Дата сохранения
		uint DP_Month;
		uint DP_DayOfWeek;
		uint DP_Day;
		uint DP_Hour;					// Время сохранения
		uint DP_Minute;
		uint DP_Second;
		uint DP_Millisecond;
		ulong DP_FileSize_Sub99;		// (Размер файла сохранения) - 99
		ulong DP_CurrentIsland;			// Номер текущего острова
		float DP_CameraX;				// Позиция камеры игрока
		float DP_CameraY;
		float DP_CameraZ;
		ulong DP_IngameMinuteLengthMs;	// Длительность минуты в игре (норма - 1000 ms)
		ulong DP_GameInternalTimer;		// Таймер игры
		uchar DP_GameHour;				// Время в игре
		uchar DP_Unused01[3];
		uchar DP_GameMinute;
		uchar DP_Unused02[3];
		ulong DP_PadNumber;				// Номер джойстика ???
		ulong DP_GameGlobalTimer;		// Глобальный таймер
		float DP_GameSpeed;				// Скорость игры (норма - 1.0)
		float DP_FrameDelta;			// Параметры смены кадров
		float DP_TimeStep;
		ulong DP_TotalFramesCount;
		float DP_TimeStep2;				// 1.0
		float DP_FramesPerUpdate;		// 1.0
		float DP_TimeScale;				// 1.0
		ulong DP_OldWeatherNumber;		// Старый номер погоды
		ulong DP_NewWeatherNumber;		// Новый номер погоды
		sint DP_ForcedWeatherNumber;	// Текущая установленная погода (0 - 63, -1 = случайная)
		uint DP_Unused03;
		float DP_WeatherInterpolation;	// Статус перехода от старой погоды к новой (0.0 - 1.0)
		ulong DP_CurrentWeatherNumber;	// Номер текущей погоды в списке (0 - 63)
		float DP_CarCameraView;			// Положение камеры в авто (0 - 5)
		float DP_FootCameraView;		// Положение камеры при ходьбе (0 - 5)
		ulong DP_InteriorNumber;		// Номер интерьера (0 - 17)
		uchar DP_TaxiBoost;				// Разрешение прыжков такси (0/1)
		uchar DP_Unused04[3];
		uchar DP_InvertLook;			// Обратить взгляд ???
		uchar DP_Unused05[3];
		ulong DP_ExtraSkyColor;			// Цвет неба в интерьере
		ulong DP_IsExtraSkyActive;		// Активен/неактивен
		float DP_SkyOverlay;			// Степень перехода к цвету интерьера (0.0 - 1.0)
		ulong DP_CurrentRadioStationPositions[10];	// Текущие позиции треков радиостанций
		} DP;

	uchar DP_Raw [sizeof (struct DP_Formatted)];
	};
