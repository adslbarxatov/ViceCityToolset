// Отключение предупреждений
#define _CRT_SECURE_NO_WARNINGS

// Подключение общего заголовка
#include "ViceCityToolset.h"

// Макросы
#define SD_LOAD_TO_VAR(WriteTo,BytesCount,ErrorNumber)		if (fread (WriteTo, 1, BytesCount, FI) != BytesCount)	\
		{	\
		fclose (FI);	\
		\
		SD = NULL;	\
		return ErrorNumber;	\
		}	

#define SD_LOAD_TO_PTR(Pointer,StructName,BytesCount,ErrorNumber)	if (Pointer)	\
		free (Pointer);	\
	\
	if ((Pointer = (union StructName *)malloc (BytesCount)) == NULL)	\
		{	\
		fclose (FI);	\
		\
		SD = NULL;	\
		return SD_LOAD_ERR_MemoryAllocationFailure;	\
		}	\
	\
	SD_LOAD_TO_VAR (Pointer, BytesCount, ErrorNumber)

// Функция загрузки файла сохранения и создания структуры
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_Load (schar *FilePath, struct SaveData *SD)
	{
	// Переменные
	FILE *FI;					// Дескриптор файла
	uint i;						// Счётчик
	ulong num;					// Вспомогательная переменная
	ulong alignStart, alignEnd;	// Границы блока выравнивания

	// Попытка открытия файла
	if ((FI = fopen (FilePath, "rb")) == NULL)
		return SD_LOAD_ERR_FileNotFound;

	// Default parameters
	SD_LOAD_TO_VAR (SD->SD_DP.DP_Raw, sizeof (union SD_DefaultParameters), SD_LOAD_ERR_LoadDP);

	// Script block
	SD_LOAD_TO_VAR (SD->SD_SB.SB_Raw, sizeof (union SD_ScriptBlock), SD_LOAD_ERR_LoadSB);
	SD->SD_SBA_Count = SD->SD_SB.SB.SB_ASubblockSize / sizeof (union SD_ScriptBlockAUnit);

	// Subblock A
	SD_LOAD_TO_PTR (SD->SD_SBA, SD_ScriptBlockAUnit, SD->SD_SBA_Count *
		sizeof (union SD_ScriptBlockAUnit), SD_LOAD_ERR_LoadSBA);

	// Subblock B
	SD_LOAD_TO_VAR (SD->SD_SBB.SBB_Raw, sizeof (union SD_ScriptBlockB), SD_LOAD_ERR_LoadSBB);

	// Scripts pool
	SD_LOAD_TO_VAR (SD->SD_SC.SC_Raw, sizeof (union SD_ScriptPool), SD_LOAD_ERR_LoadSC);
	SD->SD_SS_Count = SD->SD_SC.SC.SC_ScriptsCount;

	// Script structures
	SD_LOAD_TO_PTR (SD->SD_SS, SD_ScriptStructure, SD->SD_SS_Count * sizeof (union SD_ScriptStructure),
		SD_LOAD_ERR_LoadSS);

	// Ped player
	SD_LOAD_TO_VAR (SD->SD_PPL.PPL_Raw, sizeof (union SD_PedPlayer), SD_LOAD_ERR_LoadPPL);
	SD->SD_PPS_Count = SD->SD_PPL.PPL.PPL_NumberOfPlayers;

	// Ped player structure
	SD_LOAD_TO_PTR (SD->SD_PPS, SD_PedPlayerStructure, SD->SD_PPS_Count * sizeof (union SD_PedPlayerStructure),
		SD_LOAD_ERR_LoadPPS);

	// Garages
	SD_LOAD_TO_VAR (SD->SD_GR.GR_Raw, sizeof (union SD_Garages), SD_LOAD_ERR_LoadGR);

	// Taxi shortcuts
	SD_LOAD_TO_VAR (SD->SD_TS.TS_Raw, sizeof (union SD_TaxiShortcuts), SD_LOAD_ERR_LoadTS);

	//////////////////////////////////////////////////////////////////
	// Vehicles
	SD_LOAD_TO_VAR (SD->SD_VH.VH_Raw, sizeof (union SD_Vehicles), SD_LOAD_ERR_LoadVH);
	SD->SD_VS_Count = SD->SD_VH.VH.VH_BikesCount + SD->SD_VH.VH.VH_BoatsCount + SD->SD_VH.VH.VH_GeneralVehiclesCount;

	// Vehicle structures
	if (SD->SD_VS)
		free (SD->SD_VS);

	if ((SD->SD_VS = (union SD_VehicleStructure *)malloc (SD->SD_VS_Count * sizeof (union SD_VehicleStructure))) == NULL)
		{
		fclose (FI);
		SD = NULL;
		return SD_LOAD_ERR_MemoryAllocationFailure;
		}

	for (i = 0; i < SD->SD_VS_Count; i++)
		{
		// Чтение типа авто
		if (fread (&num, 1, sizeof (ulong), FI) != sizeof (ulong))
			{
			fclose (FI);
			SD = NULL;
			return SD_LOAD_ERR_LoadVS;
			}
		SD->SD_VS[i].VS.VS_VehicleType = num;

		// Чтение остальных параметров
		switch (num)
			{
			case 0:	// General
				num = SD_VS_GVS_Size;
				break;

			case 1:	// Boat
				num = SD_VS_BoS_Size;
				break;

			case 5:	// Bike
				num = SD_VS_BiS_Size;
				break;

			default:
				fclose (FI);
				SD = NULL;
				return SD_LOAD_ERR_LoadVS;
			}

		if (fread (SD->SD_VS->VS_Raw + i * sizeof (union SD_VehicleStructure) + sizeof (ulong), 1,
			num - sizeof (ulong), FI) != (num - sizeof (ulong)))
			{
			fclose (FI);
			SD = NULL;
			return SD_LOAD_ERR_LoadVS;
			}
		}

	//////////////////////////////////////////////////////////////////

	// Object pool
	SD_LOAD_TO_VAR (SD->SD_OP.OP_Raw, sizeof (union SD_ObjectPool), SD_LOAD_ERR_LoadOP);
	SD->SD_OS_Count = SD->SD_OP.OP.OP_ObjectsCount;

	// Objects structures
	SD_LOAD_TO_PTR (SD->SD_OS, SD_ObjectStructure, SD->SD_OS_Count * sizeof (union SD_ObjectStructure),
		SD_LOAD_ERR_LoadOS);

	// Paths block
	SD_LOAD_TO_VAR (SD->SD_PH.PH_Raw, sizeof (union SD_Paths), SD_LOAD_ERR_LoadPH);
	SD->SD_PHD_Count = SD->SD_PH.PH.PH_BlockSize - sizeof (ulong);
	// По какой-то причине SubBlockSize этой структуры периодически врёт

	SD_LOAD_TO_PTR (SD->SD_PHD, SD_PathsDescription, SD->SD_PHD_Count, SD_LOAD_ERR_LoadPH);

	// Cranes
	SD_LOAD_TO_VAR (SD->SD_CR.CR_Raw, sizeof (union SD_Cranes), SD_LOAD_ERR_LoadCR);

	// Pickups
	SD_LOAD_TO_VAR (SD->SD_PU.PU_Raw, sizeof (union SD_Pickups), SD_LOAD_ERR_LoadPU);

	// Phone info
	SD_LOAD_TO_VAR (SD->SD_PI.PI_Raw, sizeof (union SD_PhoneInfo), SD_LOAD_ERR_LoadPI);

	// Restart points
	SD_LOAD_TO_VAR (SD->SD_RL.RL_Raw, sizeof (union SD_RestartLocations), SD_LOAD_ERR_LoadRL);

	// Radar blips
	SD_LOAD_TO_VAR (SD->SD_RB.RB_Raw, sizeof (union SD_RadarBlips), SD_LOAD_ERR_LoadRB);

	// Zones buffer
	SD_LOAD_TO_VAR (SD->SD_ZB.ZB_Raw, sizeof (union SD_ZonesBuffer), SD_LOAD_ERR_LoadZB);

	// Gang data
	SD_LOAD_TO_VAR (SD->SD_GD.GD_Raw, sizeof (union SD_GangData), SD_LOAD_ERR_LoadGD);

	// Car generators
	SD_LOAD_TO_VAR (SD->SD_CG.CG_Raw, sizeof (union SD_CarGenerators), SD_LOAD_ERR_LoadCG);

	// Particles
	SD_LOAD_TO_VAR (SD->SD_PR.PR_Raw, sizeof (union SD_Particles), SD_LOAD_ERR_LoadPR);
	SD->SD_PRD_Count = SD->SD_PR.PR.PR_ParticlesCount + 1;
	// Вслед за массивом структур идёт ещё один элемент такого же размера,
	// как элемент массива, не являющийся описанием particle.
	// Пока неизвестно, что это

	// Particles descriptions
	SD_LOAD_TO_PTR (SD->SD_PRD, SD_ParticleDescription, SD->SD_PRD_Count * sizeof (union SD_ParticleDescription),
		SD_LOAD_ERR_LoadPRD);

	// Audio scripts
	SD_LOAD_TO_VAR (SD->SD_AU.AU_Raw, sizeof (union SD_AudioScripts), SD_LOAD_ERR_LoadAU);
	SD->SD_AS_Count = SD->SD_AU.AU.AU_ScriptsCount;

	// Audio structures
	SD_LOAD_TO_PTR (SD->SD_AS, SD_AudioScriptStructure, SD->SD_AS_Count * sizeof (union SD_AudioScriptStructure),
		SD_LOAD_ERR_LoadAS);

	// Script path
	SD_LOAD_TO_VAR (SD->SD_SPH.SPH_Raw, sizeof (union SD_ScriptPathHeader), SD_LOAD_ERR_LoadSP);
	for (i = 0; i < SP_PathsCount; i++)
		{
		SD_LOAD_TO_VAR (SD->SD_SP[i].SP_Raw, sizeof (union SD_ScriptPath), SD_LOAD_ERR_LoadSP);

		if (SD->SD_SP[i].SP.SP_PointsCount < 1)
			continue;

		SD_LOAD_TO_PTR (SD->SD_SPN[i], SD_ScriptPathNodes, SD->SD_SP[i].SP.SP_PointsCount *
			sizeof (union SD_ScriptPathNodes), SD_LOAD_ERR_LoadSP);
		}

	// Player info
	SD_LOAD_TO_VAR (SD->SD_PL.PL_Raw, sizeof (union SD_PlayerInfo), SD_LOAD_ERR_LoadPL);

	// Stats
	SD_LOAD_TO_VAR (SD->SD_ST.ST_Raw, sizeof (union SD_Stats), SD_LOAD_ERR_LoadST);

	// Trigger zones
	SD_LOAD_TO_VAR (SD->SD_TZ.TZ_Raw, sizeof (union SD_TriggerZones), SD_LOAD_ERR_LoadTZ);

	// Streaming block
	SD_LOAD_TO_VAR (SD->SD_SR.SR_Raw, sizeof (union SD_Streaming), SD_LOAD_ERR_LoadSR);

	// Ped types
	SD_LOAD_TO_VAR (SD->SD_PT.PT_Raw, sizeof (union SD_PedTypes), SD_LOAD_ERR_LoadPT);

	// Переход в конец файла и контроль размера файла
	alignStart = ftell (FI);
	if (fseek (FI, BFileSize - sizeof (union SD_CheckSum), SEEK_SET))
		{
		fclose (FI);

		SD = NULL;
		return SD_LOAD_ERR_LoadCS;
		}
	alignEnd = ftell (FI);

	if (SD->SD_PT.PT.LastAlignBlockSize != (alignEnd - alignStart))
		{
		fclose (FI);

		SD = NULL;
		return SD_LOAD_ERR_LoadCS;
		}

	// CheckSum
	SD_LOAD_TO_VAR (SD->SD_CS.CS_Raw, sizeof (union SD_CheckSum), SD_LOAD_ERR_LoadCS);
	SD->SD_CheckSum = SD->SD_CS.CS.CS_Sum;

	// Успех
	fclose (FI);
	return SD_LOAD_SUCCESS;
	}
