// Отключение предупреждений
#define _CRT_SECURE_NO_WARNINGS

// Подключение общего заголовка
#include "ViceCityToolset.h"

// Корректные координаты авто в гаражах
float GR_GC_NormCoords[][6] = {
	{453.5f, 638.42f, 11.2f, -1.0f, -0.01f, 0.0f},
	{-844.52f, 1317.17f, 11.7f, 0.22f, -0.98f, 0.0f},
	{-824.77f, 1316.93f, 11.44f, 0.53f, -0.85f, 0.0f},
	{-816.0f, 1319.82f, 11.1f, 0.35f, -0.94f, 0.0f},
	{24.4f, -1487.2f, 10.32f, 0.15f, 1.0f, 0.0f},
	{302.7f, 406.1f, 13.21f, -0.21f, -0.98f, 0.0f},
	{-989.0f, -810.06f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-994.78f, -811.45f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-1010.4f, -810.07f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-1018.1f, -810.68f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-359.4f, -545.23f, 12.76f, 0.0f, 1.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{-849.26f, 1315.65f, 11.47f, 0.37f, -0.93f, 0.0f},
	{-820.71f, 1318.37f, 11.46f, 0.2f, -0.98f, 0.0f},
	{-811.96f, 1321.05f, 11.13f, 0.32f, -0.95f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{-984.52f, -810.82f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-999.22f, -811.41f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-1006.8f, -810.14f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-1022.38f, -810.61f, 7.22f, 0.0f, -1.0f, 0.0f},
	{-355.6f, -544.87f, 12.7f, 0.0f, 1.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{-847.94f, 1308.66f, 11.4f, 0.36f, -0.93f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
	{-843.27f, 1310.0f, 11.35f, 0.4f, -0.93f, 0.0f}
	};

// Реальные индексы гаражей в файле сохранения
uint GR_GC_GarageNumbers[] = {
	0,	// El Swanko Casa
	1,	// Human Condo 1, позиция 1
	13,	// Human Condo 1, позиция 2
	25,	// Human Condo 1, позиция 3
	37,	// Human Condo 1, позиция 4
	2,	// Human Condo 2, позиция 1
	14,	// Human Condo 2, позиция 2
	3,	// Human Condo 3, позиция 1
	15,	// Human Condo 3, позиция 2
	4,	// Ocean Heights
	5,	// Links View
	6,	// Sunshine auto 1, позиция 1
	18,	// Sunshine auto 1, позиция 2
	7,	// Sunshine auto 2, позиция 1
	19,	// Sunshine auto 2, позиция 2
	8,	// Sunshine auto 3, позиция 1
	20,	// Sunshine auto 3, позиция 2
	9,	// Sunshine auto 4, позиция 1
	21,	// Sunshine auto 4, позиция 2
	10,	// Versetti estate, позиция 1
	22	// Versetti estate, позиция 2
	};

// Значения интереса полиции для количества звёзд
uint SD_PPS_CriminalPoints[] = {
	0,
	225,
	450,
	900,
	1800,
	3600,
	7200
	};

// Базы опкодов
#define WeaponsCount		SD_PPS_WD_Count
#define WeaponsBaseOpCode	200
#define GaragesCount		(sizeof (GR_GC_GarageNumbers) / sizeof (GR_GC_GarageNumbers[0]))
#define GaragesBaseOpCode	300
#define PickupsCount		SD_PU_S_Count
#define PickupsBaseOpCode	1000
#define GangsCount			8
#define GangsBaseOpCode		400
#define GeneratorsCount		SD_CG_S_Count
#define GeneratorsBaseOpCode	2001

// Макросы (базовые обработчики)
#define SD_PROC_VARIABLE(SD_Variable,MinValue,MaxValue,NewValue)	\
	if (Mode == 1)	\
		{	\
		if ((NewValue < MinValue) || (NewValue > MaxValue))	\
			return SD_INTRPR_ERR_ValueOutOfRange;	\
		SD_Variable = NewValue;	\
		}	\
	if (Mode == 4)	\
		{	\
		sprintf (res, "%lf;%lf", (float)MinValue, (float)MaxValue);	\
		SaveData_SetLastMessage (res);	\
		return SD_OK;	\
		}	\
	sprintf (res, "%lf", (float)SD_Variable);	\
	SaveData_SetLastMessage (res);	\
	return SD_OK;

//
#define SD_PROC_STRING_VARIABLE(SD_Variable,StringLength,NewValue)	\
	if (Mode == 1)	\
		{	\
		for (i = 0; i < StringLength; i++)	\
			SD_Variable[i] = NewValue[i];	\
		SD_Variable[StringLength] = '\x0';	\
		}	\
	if (Mode == 4)	\
		return SD_INTRPR_ERR_OpCodeIsIncorrect;	\
	sprintf (res, "%s", SD_Variable);	\
	SaveData_SetLastMessage (res);	\
	return SD_OK;

// Командный интерпретатор. Возвращает сообщение с
// результатом выполнения команды или сообщение об ошибке
// • SD - структура сохранения
// • Mode - режим интерпретации
// • OpCode - код операции
// • ParCode - код параметра
// • Value - новое значение
sint SaveData_CommandInterpreter (struct SaveData *SD, uint Mode, uint OpCode, uint ParCode, schar *Value)
	{
	// Возвращаемое сообщение
	schar res[SD_MaxStrSize];
	res[0] = '\0';

	// Вспомогательные переменные
	slong i;

	// Контроль параметров
	if ((Mode != 4) && (SD->SD_DP.DP.DP_BlockSize == 0))
		return SD_INTRPR_ERR_FileNotLoaded;

	// Обработка
	switch (Mode)
		{
		// Get, set, limits
		case 0:
		case 1:
		case 4:
			// Пакетные команды
			// Оружие
			if ((OpCode >= WeaponsBaseOpCode) && (OpCode < (WeaponsBaseOpCode + WeaponsCount)))
				{
				switch (ParCode)
					{
					case 0:
						SD_PROC_VARIABLE (SD->SD_PPS[0].PPS.PPS_WD[OpCode - WeaponsBaseOpCode].PPS_WD_WeaponNumber,
							SD_LIMIT_plwnu_B, SD_LIMIT_plwnu_T, atol (Value));
					case 1:
						SD_PROC_VARIABLE (SD->SD_PPS[0].PPS.PPS_WD[OpCode - WeaponsBaseOpCode].PPS_WD_BulletsCount,
							SD_LIMIT_plwbu_B, SD_LIMIT_plwbu_T, atol (Value));

					default:
						return SD_INTRPR_ERR_ParCodeIsIncorrect;
					}
				}

			// Гаражи
			if ((OpCode >= GaragesBaseOpCode) && (OpCode < (GaragesBaseOpCode + GaragesCount)))
				{
				// Принудительное выравнивание позиций авто в гаражах
				// Применяется ко всем позициям при любой операции с гаражами
				for (i = 0; i < GaragesCount; i++)
					{
					SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[i]].GR_GC_X = GR_GC_NormCoords[GR_GC_GarageNumbers[i]][0];
					SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[i]].GR_GC_Y = GR_GC_NormCoords[GR_GC_GarageNumbers[i]][1];
					SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[i]].GR_GC_Z = GR_GC_NormCoords[GR_GC_GarageNumbers[i]][2];
					SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[i]].GR_GC_vX = GR_GC_NormCoords[GR_GC_GarageNumbers[i]][3];
					SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[i]].GR_GC_vY = GR_GC_NormCoords[GR_GC_GarageNumbers[i]][4];
					SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[i]].GR_GC_vZ = GR_GC_NormCoords[GR_GC_GarageNumbers[i]][5];
					}

				switch (ParCode)
					{
					case 0:
						SD_PROC_VARIABLE (SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[OpCode -
							GaragesBaseOpCode]].GR_GC_ModelID,
							SD_LIMIT_grcmo_B, SD_LIMIT_grcmo_T, atol (Value));
					case 1:
						SD_PROC_VARIABLE (SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[OpCode -
							GaragesBaseOpCode]].GR_GC_Immunity,
							SD_LIMIT_grimm_B, SD_LIMIT_grimm_T, atol (Value));
					case 2:
						SD_PROC_VARIABLE (SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[OpCode -
							GaragesBaseOpCode]].GR_GC_PrimaryColor,
							SD_LIMIT_grcol_B, SD_LIMIT_grcol_T, atoi (Value));
					case 3:
						SD_PROC_VARIABLE (SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[OpCode -
							GaragesBaseOpCode]].GR_GC_SecondaryColor,
							SD_LIMIT_grcol_B, SD_LIMIT_grcol_T, atoi (Value));
					case 4:
						SD_PROC_VARIABLE (SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[OpCode -
							GaragesBaseOpCode]].GR_GC_RadioStation,
							SD_LIMIT_grrad_B, SD_LIMIT_grrad_T, atoi (Value));
					case 5:
						SD_PROC_VARIABLE (SD->SD_GR.GR.GR_GC[GR_GC_GarageNumbers[OpCode -
							GaragesBaseOpCode]].GR_GC_BombType,
							SD_LIMIT_grbtp_B, SD_LIMIT_grbtp_T, atoi (Value));

					default:
						return SD_INTRPR_ERR_ParCodeIsIncorrect;
					}
				}

			// Собираемые объекты
			if ((OpCode >= PickupsBaseOpCode) && (OpCode < (PickupsBaseOpCode + PickupsCount)))
				{
				switch (ParCode)
					{
					// Временно read-only
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 6:
						if (Mode != 1)
							{
							if (ParCode == 0)
								{
								SD_PROC_VARIABLE (SD->SD_PU.PU.PU_S[OpCode - PickupsBaseOpCode].PU_S_ModelNumber,
									SD_LIMIT_pumod_B, SD_LIMIT_pumod_T, atoi (Value));
								}
							if (ParCode == 1)
								{
								SD_PROC_VARIABLE (SD->SD_PU.PU.PU_S[OpCode - PickupsBaseOpCode].PU_S_X,
									SD_LIMIT_cgx_B, SD_LIMIT_cgx_T, atof (Value));
								}
							if (ParCode == 2)
								{
								SD_PROC_VARIABLE (SD->SD_PU.PU.PU_S[OpCode - PickupsBaseOpCode].PU_S_Y,
									SD_LIMIT_cgy_B, SD_LIMIT_cgy_T, atof (Value));
								}
							if (ParCode == 3)
								{
								SD_PROC_VARIABLE (SD->SD_PU.PU.PU_S[OpCode - PickupsBaseOpCode].PU_S_Z,
									SD_LIMIT_cgz_B, SD_LIMIT_cgz_T, atof (Value));
								}
							if (ParCode == 4)
								{
								SD_PROC_VARIABLE (SD->SD_PU.PU.PU_S[OpCode - PickupsBaseOpCode].PU_S_PickupType,
									SD_LIMIT_putyp_B, SD_LIMIT_putyp_T, atoi (Value));
								}
							if (ParCode == 6)
								{
								SD_PROC_VARIABLE (SD->SD_PU.PU.PU_S[OpCode - PickupsBaseOpCode].PU_S_HasBeenPickedUp,
									SD_LIMIT_pupic_B, SD_LIMIT_pupic_T, atoi (Value));
								}
							}
						else
							{
							return SD_INTRPR_ERR_ParCodeIsIncorrect;
							}
					case 5:
						SD_PROC_VARIABLE (SD->SD_PU.PU.PU_S[OpCode - PickupsBaseOpCode].PU_S_MaxAsset,
							SD_LIMIT_puass_B, SD_LIMIT_puass_T, atol (Value));

					default:
						return SD_INTRPR_ERR_ParCodeIsIncorrect;
					}
				}

			// Банды
			if ((OpCode >= GangsBaseOpCode) && (OpCode < (GangsBaseOpCode + GangsCount)))
				{
				switch (ParCode)
					{
					case 0:
						SD_PROC_VARIABLE (SD->SD_GD.GD.GD_S[OpCode - GangsBaseOpCode].GD_S_CarModel,
							SD_LIMIT_gdcmo_B, SD_LIMIT_gdcmo_T, atol (Value));
					case 1:
						SD_PROC_VARIABLE (SD->SD_GD.GD.GD_S[OpCode - GangsBaseOpCode].GD_S_PedModel1,
							SD_LIMIT_gdpmo_B, SD_LIMIT_gdpmo_T, atol (Value));
					case 2:
						SD_PROC_VARIABLE (SD->SD_GD.GD.GD_S[OpCode - GangsBaseOpCode].GD_S_PedModel2,
							SD_LIMIT_gdpmo_B, SD_LIMIT_gdpmo_T, atol (Value));
					case 3:
						SD_PROC_VARIABLE (SD->SD_GD.GD.GD_S[OpCode - GangsBaseOpCode].GD_S_WeaponNumber1,
							SD_LIMIT_gdwnu_B, SD_LIMIT_gdwnu_T, atol (Value));
					case 4:
						SD_PROC_VARIABLE (SD->SD_GD.GD.GD_S[OpCode - GangsBaseOpCode].GD_S_WeaponNumber2,
							SD_LIMIT_gdwnu_B, SD_LIMIT_gdwnu_T, atol (Value));

					default:
						return SD_INTRPR_ERR_ParCodeIsIncorrect;
					}
				}

			// Парковки
			if ((OpCode >= GeneratorsBaseOpCode) && (OpCode < (GeneratorsBaseOpCode + GeneratorsCount)))
				{
				// Не знаю, почему так, но если это значение слишком большое, авто перестают генерироваться
				// Применяется к отдельному генератору при любом коде параметра
				SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_GameTimerWhenGenerated = 60;

				switch (ParCode)
					{
					case 0:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_ModelID,
							SD_LIMIT_cgcmo_B, SD_LIMIT_cgcmo_T, atol (Value));
					case 1:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_X,
							SD_LIMIT_cgx_B, SD_LIMIT_cgx_T, atof (Value));
					case 2:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_Y,
							SD_LIMIT_cgy_B, SD_LIMIT_cgy_T, atof (Value));
					case 3:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_Z,
							SD_LIMIT_cgz_B, SD_LIMIT_cgz_T, atof (Value));
					case 4:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_Rotation,
							SD_LIMIT_cgrot_B, SD_LIMIT_cgrot_T, atof (Value));
					case 5:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_AllowSpawn,
							SD_LIMIT_cgasp_B, SD_LIMIT_cgasp_T, atoi (Value));
					case 6:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_PrimaryColor,
							SD_LIMIT_cgcol_B, SD_LIMIT_cgcol_T, atoi (Value));
					case 7:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_SecondaryColor,
							SD_LIMIT_cgcol_B, SD_LIMIT_cgcol_T, atoi (Value));
					case 8:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_Alarm,
							SD_LIMIT_cgala_B, SD_LIMIT_cgala_T, atoi (Value));
					case 9:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_Lock,
							SD_LIMIT_cgloc_B, SD_LIMIT_cgloc_T, atoi (Value));
					case 10:
						SD_PROC_VARIABLE (SD->SD_CG.CG.CG_S[OpCode - GeneratorsBaseOpCode].CG_S_ForceSpawn,
							SD_LIMIT_cgfsp_B, SD_LIMIT_cgfsp_T, atoi (Value));

					default:
						return SD_INTRPR_ERR_ParCodeIsIncorrect;
					}
				}

			// Простые команды
			switch (OpCode)
				{
				// Основные параметры
				case 0:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_Year, SD_LIMIT_dpyea_B, SD_LIMIT_dpyea_T, atoi (Value));
				case 1:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_Month, SD_LIMIT_dpmon_B, SD_LIMIT_dpmon_T, atoi (Value));
				case 2:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_Day, SD_LIMIT_dpday_B, SD_LIMIT_dpday_T, atoi (Value));
				case 3:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_Hour, SD_LIMIT_dphou_B, SD_LIMIT_dphou_T, atoi (Value));
				case 4:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_Minute, SD_LIMIT_dpmin_B, SD_LIMIT_dpmin_T, atoi (Value));
				case 5:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_Second, SD_LIMIT_dpsec_B, SD_LIMIT_dpsec_T, atoi (Value));
				case 6:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_IngameMinuteLengthMs, SD_LIMIT_dpiml_B, SD_LIMIT_dpiml_T,
						atol (Value));
				case 7:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_GameHour, SD_LIMIT_dpgho_B, SD_LIMIT_dpgho_T, atoi (Value));
				case 8:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_GameMinute, SD_LIMIT_dpgmi_B, SD_LIMIT_dpgmi_T, atoi (Value));
				case 9:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_GameSpeed, SD_LIMIT_dpgsp_B, SD_LIMIT_dpgsp_T, atof (Value));
				case 10:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_ForcedWeatherNumber, SD_LIMIT_dpswn_B, SD_LIMIT_dpswn_T,
						atoi (Value));
				case 11:
					SD_PROC_VARIABLE (SD->SD_DP.DP.DP_CarCameraView, SD_LIMIT_dpccv_B, SD_LIMIT_dpccv_T, atof (Value));

					// Параметры субблоков переменных
				case 100:
					SD_PROC_VARIABLE (SD->SD_SBB.SBB.SBB_AllowCabsRadio, SD_LIMIT_sbcbr_B, SD_LIMIT_sbcbr_T,
						atoi (Value));

					// Параметры пула игроков
				case 210:
					SD_PROC_VARIABLE (SD->SD_PPS[0].PPS.PPS_CurrentArmor, SD_LIMIT_plcua_B, SD_LIMIT_plcua_T,
						atof (Value));

				case 211:
					if (Mode == 1)
						{
						i = atol (Value);
						if ((i < SD_LIMIT_plmwl_B) || (i > SD_LIMIT_plmwl_T))
							return SD_INTRPR_ERR_ValueOutOfRange;

						SD->SD_PPS[0].PPS.PPS_MaxWantedLevel = i;
						SD->SD_PPS[0].PPS.PPS_MaxCriminalPoints = SD_PPS_CriminalPoints[i];
						}
					if (Mode == 4)
						{
						sprintf (res, "%lf;%lf", (float)SD_LIMIT_plmwl_B, (float)SD_LIMIT_plmwl_T);
						SaveData_SetLastMessage (res);
						return SD_OK;
						}

					sprintf (res, "%lf", (float)SD->SD_PPS[0].PPS.PPS_MaxWantedLevel);
					SaveData_SetLastMessage (res);
					return SD_OK;

				case 212:
					SD_PROC_STRING_VARIABLE (SD->SD_PPS[0].PPS.PPS_SuitName,
						((strlen (Value) > sizeof (SD->SD_PPS[0].PPS.PPS_SuitName)) ?
							sizeof (SD->SD_PPS[0].PPS.PPS_SuitName) : strlen (Value)), Value);

					// Параметры игрока
				case 500:
					SD_PROC_VARIABLE (SD->SD_PL.PL.PL_CurrentMoney, SD_LIMIT_plcum_B, SD_LIMIT_plcum_T, atol (Value));
				case 501:
					SD_PROC_VARIABLE (SD->SD_PL.PL.PL_InfiniteRun, SD_LIMIT_pliru_B, SD_LIMIT_pliru_T, atoi (Value));
				case 502:
					SD_PROC_VARIABLE (SD->SD_PL.PL.PL_FastReload, SD_LIMIT_plfre_B, SD_LIMIT_plfre_T, atoi (Value));
				case 503:
					SD_PROC_VARIABLE (SD->SD_PL.PL.PL_Fireproof, SD_LIMIT_plfpr_B, SD_LIMIT_plfpr_T, atoi (Value));
				case 504:
					SD_PROC_VARIABLE (SD->SD_PL.PL.PL_MaxHealth, SD_LIMIT_plmxh_B, SD_LIMIT_plmxh_T, atoi (Value));
				case 505:
					SD_PROC_VARIABLE (SD->SD_PL.PL.PL_MaxArmor, SD_LIMIT_plmxa_B, SD_LIMIT_plmxa_T, atoi (Value));
				case 506:
					i = atoi (Value);
					if (Mode == 1)
						{
						if (i == 1)
							SD->SD_ST.ST.ST_ProgressMade = SD->SD_ST.ST.ST_TotalProgress;
						else if (i == 0)
							SD->SD_ST.ST.ST_ProgressMade = SD->SD_ST.ST.ST_TotalProgress - 1.0f;
						else
							return SD_INTRPR_ERR_ValueOutOfRange;
						}
					if (Mode == 4)
						{
						sprintf (res, "%lf;%lf", 0.0f, 1.0f);
						SaveData_SetLastMessage (res);
						return SD_OK;
						}

					sprintf (res, "%lf", (float)(SD->SD_ST.ST.ST_ProgressMade == SD->SD_ST.ST.ST_TotalProgress));
					SaveData_SetLastMessage (res);
					return SD_OK;

					// Параметры парковок
				case 2000:
					SD_PROC_VARIABLE (SD->SD_CG.CG.CG_CarsCount, 1, GeneratorsCount, atoi (Value));

				default:
					return SD_INTRPR_ERR_OpCodeIsIncorrect;
				}
			break;

			// Load
		case 2:
			switch (OpCode)
				{
				case GARAGES_FILE_CODE:
					i = SaveData_LoadGarages (SD, Value);
					break;
				case STATS_FILE_CODE:
					i = SaveData_LoadStats (SD, Value);
					break;
				case CG_FILE_CODE:
					i = SaveData_LoadCG (SD, Value);
					break;

				default:
					i = SD_INTRPR_ERR_OpCodeIsIncorrect;
					break;
				}

			return i;

			// Save
		case 3:
			switch (OpCode)
				{
				case 0:
					i = SaveData_Save (SD, Value);
					break;
				case GARAGES_FILE_CODE:
					i = SaveData_SaveGarages (SD, Value);
					break;
				case STATS_FILE_CODE:
					i = SaveData_SaveStats (SD, Value);
					break;
				case CG_FILE_CODE:
					i = SaveData_SaveCG (SD, Value);
					break;

				default:
					i = SD_INTRPR_ERR_OpCodeIsIncorrect;
					break;
				}
			return i;

			// Fix
		case 5:
			switch (OpCode)
				{
				// Обнуление кранов (вызывают ошибки при изменении количества объектов на картах)
				case 0:
					SD->SD_CR.CR.CR_CranesCount = 0;

					for (i = 0; i < SD_CR_S_Count; i++)
						{
						SD->SD_CR.CR.CR_S[i].CR_S_CraneObjectPointer = 0;
						SD->SD_CR.CR.CR_S[i].CR_S_MagnetObjectPointer = 0;
						SD->SD_CR.CR.CR_S[i].CR_S_CraneHookCurrentX = 0.0f;
						SD->SD_CR.CR.CR_S[i].CR_S_CraneHookCurrentY = 0.0f;
						SD->SD_CR.CR.CR_S[i].CR_S_CraneHookCurrentZ = 0.0f;
						}
					return SD_FIXED;

					// Обнуление замен объектов (по той же причине)
				case 1:
					for (i = 0; i < SD_SBB_R_Count; i++)
						{
						SD->SD_SBB.SBB.SBB_R[i].SBB_R_NewModelID = -1;
						SD->SD_SBB.SBB.SBB_R[i].SBB_R_ObjectHandle = 0;
						SD->SD_SBB.SBB.SBB_R[i].SBB_R_OldModelID = -1;
						SD->SD_SBB.SBB.SBB_R[i].SBB_R_Type = 0;
						}
					return SD_FIXED;

				default:
					return SD_INTRPR_ERR_OpCodeIsIncorrect;
				}
			break;

			// Недопустимый режим
		default:
			return SD_INTRPR_ERR_ModeIsIncorrect;
		}
	}
