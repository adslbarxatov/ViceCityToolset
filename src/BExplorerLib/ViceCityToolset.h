// Исправление для MSVC 2019
#define _CRT_SECURE_NO_WARNINGS

// Подключение заголовков
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>
#include <windows.h>
#include "..\..\Generics\CSTypes.h"

#define B_API(t)	extern __declspec(dllexport) t

#define B_VERSION			3,8,0,0
#define B_VERSION_S			"3.8.0.0"
#define B_PRODUCT			"Save files API for Vice city toolset"
#define B_COMPANY			FDL_COMPANY

#define BFileSize			0x31464

// Подключение определений структур
#include "DefaultParameters.h"
#include "ScriptBlock.h"
#include "ScriptPool.h"
#include "PedPlayer.h"
#include "Garages.h"
#include "TaxiShortcuts.h"
#include "Vehicles.h"
#include "ObjectPool.h"
#include "Paths.h"
#include "Cranes.h"
#include "Pickups.h"
#include "PhoneInfo.h"
#include "RestartLocations.h"
#include "RadarBlips.h"
#include "ZonesBuffer.h"
#include "GangData.h"
#include "CarGenerators.h"
#include "Particles.h"
#include "AudioScripts.h"
#include "ScriptPaths.h"
#include "PlayerInfo.h"
#include "Stats.h"
#include "TriggerZones.h"
#include "Streaming.h"
#include "PedTypes.h"
#include "CheckSum.h"

// Главная структура сохранения
struct SaveData
	{
	// Default parameters
	union SD_DefaultParameters SD_DP;

	// Script block
	union SD_ScriptBlock SD_SB;
	union SD_ScriptBlockAUnit *SD_SBA;	ulong SD_SBA_Count;
	union SD_ScriptBlockB SD_SBB;

	// Script pool
	union SD_ScriptPool SD_SC;
	union SD_ScriptStructure *SD_SS;	ulong SD_SS_Count;

	// Ped player
	union SD_PedPlayer SD_PPL;
	union SD_PedPlayerStructure *SD_PPS;	ulong SD_PPS_Count;

	// Garages
	union SD_Garages SD_GR;

	// Taxi shortcuts
	union SD_TaxiShortcuts SD_TS;

	// Vehicles
	union SD_Vehicles SD_VH;
	union SD_VehicleStructure *SD_VS;	ulong SD_VS_Count;

	// Objects pool
	union SD_ObjectPool SD_OP;
	union SD_ObjectStructure *SD_OS;	ulong SD_OS_Count;

	// Paths block
	union SD_Paths SD_PH;
	union SD_PathsDescription *SD_PHD;	ulong SD_PHD_Count;

	// Cranes
	union SD_Cranes SD_CR;

	// Pickups
	union SD_Pickups SD_PU;

	// Phone info
	union SD_PhoneInfo SD_PI;

	// Restart points
	union SD_RestartLocations SD_RL;

	// Radar blips
	union SD_RadarBlips SD_RB;

	// Zones buffer
	union SD_ZonesBuffer SD_ZB;

	// Gang data
	union SD_GangData SD_GD;

	// Car generators
	union SD_CarGenerators SD_CG;

	// Particles
	union SD_Particles SD_PR;
	union SD_ParticleDescription *SD_PRD;	ulong SD_PRD_Count;

	// Audio scripts
	union SD_AudioScripts SD_AU;
	union SD_AudioScriptStructure *SD_AS;	ulong SD_AS_Count;

	// Script path
	union SD_ScriptPathHeader SD_SPH;

	union SD_ScriptPath SD_SP[SP_PathsCount];
	union SD_ScriptPathNodes *SD_SPN[SP_PathsCount];

	// Player info
	union SD_PlayerInfo SD_PL;

	// Stats
	union SD_Stats SD_ST;

	// Trigger zones
	union SD_TriggerZones SD_TZ;

	// Streaming block
	union SD_Streaming SD_SR;

	// Ped types
	union SD_PedTypes SD_PT;

	// Checksum
	union SD_CheckSum SD_CS;
	ulong SD_CheckSum;
	};

// Максимальная длина сообщений и команд
#define SD_MaxStrSize	257

//////////////////////////////////////////////////////////////////////
// Следующая функция загружает файл сохранения GTA VC (.b) и формирует
// структуру SaveData, выполняя все необходимые проверки. В случае
// ошибки не заполняет структуру и возвращает один из следующих кодов
// ошибок:
//
#define SD_LOAD_ERR_FileNotFound			-1	// Файл с указанным именем недоступен или отсутствует
#define SD_LOAD_ERR_MemoryAllocationFailure	-2	// Ошибка выделения памяти под одну из структур
//
#define SD_LOAD_ERR_LoadDP	-101	// Ошибка загрузки структуры (тип структуры, по порядку)
#define SD_LOAD_ERR_LoadSB	-102
#define SD_LOAD_ERR_LoadSBA	-103
#define SD_LOAD_ERR_LoadSBB	-104
#define SD_LOAD_ERR_LoadSC	-105
#define SD_LOAD_ERR_LoadSS	-106
#define SD_LOAD_ERR_LoadPPL	-107
#define SD_LOAD_ERR_LoadPPS	-135
#define SD_LOAD_ERR_LoadGR	-108
#define SD_LOAD_ERR_LoadTS	-109
#define SD_LOAD_ERR_LoadVH	-136
#define SD_LOAD_ERR_LoadVS	-110
#define SD_LOAD_ERR_LoadOP	-111
#define SD_LOAD_ERR_LoadOS	-112
#define SD_LOAD_ERR_LoadPH	-113
#define SD_LOAD_ERR_LoadPHD	-114
#define SD_LOAD_ERR_LoadCR	-115
#define SD_LOAD_ERR_LoadPU	-116
#define SD_LOAD_ERR_LoadPI	-117
#define SD_LOAD_ERR_LoadRL	-118
#define SD_LOAD_ERR_LoadRB	-119
#define SD_LOAD_ERR_LoadZB	-120
#define SD_LOAD_ERR_LoadGD	-121
#define SD_LOAD_ERR_LoadCG	-122
#define SD_LOAD_ERR_LoadPR	-123
#define SD_LOAD_ERR_LoadPRD	-124
#define SD_LOAD_ERR_LoadAU	-125
#define SD_LOAD_ERR_LoadAS	-126
#define SD_LOAD_ERR_LoadSP	-127
#define SD_LOAD_ERR_LoadPL	-129
#define SD_LOAD_ERR_LoadST	-130
#define SD_LOAD_ERR_LoadTZ	-131
#define SD_LOAD_ERR_LoadSR	-132
#define SD_LOAD_ERR_LoadPT	-133
#define SD_LOAD_ERR_LoadCS	-134
//
// В случае успешного выполнения код ошибки
#define SD_LOAD_SUCCESS		1
//
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_Load (schar *FilePath, struct SaveData *SD);
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// Функция, отвечающая за вывод сообщений об ошибках
// Возвращает текст ошибки
// • Error - любой поддерживаемый код ошибки (любой из описанных в
// этом файле)
schar *SaveData_ErrorPrompt (sint Error);
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// Следующая функция - командный интерпретатор. Возвращает сообщение с
// результатом выполнения команды или сообщение об ошибке, вызываемое
// по одному из следующих кодов:
//
#define SD_FIXED								0		// Результат успешной работы режима fix
//
#define SD_INTRPR_ERR_FileNotLoaded				-1001	// Файл не был загружен
#define SD_INTRPR_ERR_ValueOutOfRange			-1002	// Значение параметра находится вне допустимых границ
#define SD_INTRPR_ERR_ModeIsIncorrect			-1003	// Указан недопустимый режим
#define SD_INTRPR_ERR_OpCodeIsIncorrect			-1004	// Указана недопустимая операция для данного режима
#define SD_INTRPR_ERR_ParCodeIsIncorrect		-1005	// Указан недопустимый параметр для данных режима и операции
//
#define SD_INTRPR_ERR_StatsFileNotFound			-1101	// Указанный файл не найден или недоступен
#define SD_INTRPR_ERR_StatsFileIsIncorrect		-1102	// Указанный файл не является допустимым или повреждён
#define SD_INTRPR_ERR_CannotCreateStatsFile		-1103	// Не удаётся создать файл
#define SD_INTRPR_ERR_CGFileNotFound			-1104
#define SD_INTRPR_ERR_CGFileIsIncorrect			-1105
#define SD_INTRPR_ERR_CannotCreateCGFile		-1106
#define SD_INTRPR_ERR_GaragesFileNotFound		-1107
#define SD_INTRPR_ERR_GaragesFileIsIncorrect	-1108
#define SD_INTRPR_ERR_CannotCreateGaragesFile	-1109
//
// • SD - структура сохранения
// • Mode - режим интерпретации
//   0 - get (получить значение)
//   1 - set (установить значение)
//   2 - load (загрузить файл)
//   3 - save (сохранить файл)
//   4 - limits (получить допустимый диапазон значения)
//   5 - fix (исправить файл сохранения)
// • OpCode - код операции
// • ParCode - код параметра
// • Value - новое значение
//
//  OpCode для режимов 0, 1 и 4
//   0-5 - год, месяц, день, час, минута и секунда сохранения
//   6 - длина секунды в игре
//   7, 8 - час и минута в игре
//   9 - скорость течения времени в игре
//   10 - текущая погода
//   11 - текущий вид обзора камеры автомобиля
//   100 - состояние CabsRadio
//   200-209 - оружие игрока, зависит от ParCode:
//     0 - тип оружия
//     1 - количество патронов
//   210 - текущая броня
//   211 - максимальное число звёзд полиции
//   212 - костюм игрока
//   300-320 - гаражи, зависит от ParCode:
//     0 - модель авто
//     1 - флаги иммунитета
//     2, 3 - основной и дополнительный цвета
//     4 - текущее радио
//     5 - вид мины
//   400-407 - банды, зависит от ParCode:
//     0 - модель авто
//     1, 2 - модели членов банды
//     3, 4 - типы оружия
//   500 - текущее состояние денежного счёта
//   501 - флаг бесконечного бега
//   502 - флаг быстрой перезарядки оружия
//   503 - флаг несгораемости
//   504 - максимальное здоровье
//   505 - максимальная броня
//   506 - флаг бесконечного оружия
//   1000-1335 - собираемые объекты, зависит от ParCode:
//     0 - модель объекта (пока read-only)
//     1-3 - координаты x, y и z (пока read-only)
//     4 - тип объекта (пока read-only)
//     5 - накопитель объекта
//     6 - состояние "собран" (пока read-only)
//   2000 - количество активных парковок
//   2001-2185 - парковки, зависит от ParCode:
//     0 - модель авто
//     1-3 - координаты x, y и z
//     4 - угол поворота, °
//     5 - флаг разрешения генерации
//     6, 7 - основной и дополнительный цвета
//     8 - вероятность срабатывания сигнализации, %
//     9 - вероятность блокировки, %
//     10 - флаг обязательной генерации
//  OpCode для режима 2
//   1 - загрузка параметров гаражей
//   2 - загрузка статистики
//   3 - загрузка параметров парковок
//  OpCode для режима 3
//   0 - сохранение файла сохранения
//   1 - сохранение параметров гаражей
//   2 - сохранение статистики
//   3 - сохранение параметров парковок
//  OpCode для режима 5
//   0 - обнуление структуры кранов
//   1 - обнуление замен объектов
schar *SaveData_CommandInterpreter (struct SaveData *SD, uint Mode, uint OpCode, uint ParCode, schar *Value);
//////////////////////////////////////////////////////////////////////

// Ограничения значений параметров
#define SD_LIMIT_dpyea_B	1980	// dp-year
#define SD_LIMIT_dpyea_T	2080
#define SD_LIMIT_dpmon_B	1		// dp-month
#define SD_LIMIT_dpmon_T	12
#define SD_LIMIT_dpday_B	1		// dp-day
#define SD_LIMIT_dpday_T	31
#define SD_LIMIT_dphou_B	0		// dp-hour
#define SD_LIMIT_dphou_T	23
#define SD_LIMIT_dpmin_B	0		// dp-minute
#define SD_LIMIT_dpmin_T	59
#define SD_LIMIT_dpsec_B	SD_LIMIT_dpmin_B	// dp-second
#define SD_LIMIT_dpsec_T	SD_LIMIT_dpmin_T
#define SD_LIMIT_dpiml_B	100		// dp-ingameminutelengthms
#define SD_LIMIT_dpiml_T	60000
#define SD_LIMIT_dpgho_B	SD_LIMIT_dphou_B	// dp-gamehour
#define SD_LIMIT_dpgho_T	SD_LIMIT_dphou_T
#define SD_LIMIT_dpgmi_B	SD_LIMIT_dpmin_B	// dp-gameminute
#define SD_LIMIT_dpgmi_T	SD_LIMIT_dpmin_T
#define SD_LIMIT_dpgsp_B	0.1f	// dp-gamespeed
#define SD_LIMIT_dpgsp_T	10.0f
#define SD_LIMIT_dpswn_B	-1		// dp-staticweathernumber
#define SD_LIMIT_dpswn_T	5
#define SD_LIMIT_dpccv_B	0.0f	// dp-carcameraview
#define SD_LIMIT_dpccv_T	5.0f
#define SD_LIMIT_dpinu_B	0		// dp-interiornumber
#define SD_LIMIT_dpinu_T	18
#define SD_LIMIT_sbcbr_B	0		// sbb-cabsradio
#define SD_LIMIT_sbcbr_T	1
#define SD_LIMIT_plcua_B	0.0f	// pl-currentarmor
#define SD_LIMIT_plcua_T	900.f
#define SD_LIMIT_plwnu_B	0		// pl-weaponnumber
#define SD_LIMIT_plwnu_T	36
#define SD_LIMIT_plwbu_B	0		// pl-weaponbullets
#define SD_LIMIT_plwbu_T	12000
#define SD_LIMIT_plmwl_B	0		// pl-maxwantedlevel
#define SD_LIMIT_plmwl_T	6
#define SD_LIMIT_plmcp_B	0		// pl-maxcriminalpoints
#define SD_LIMIT_plmcp_T	7200
//#define SD_LIMIT_grsgs_B	0		// pl-sungaragestatus
//#define SD_LIMIT_grsgs_T	63
#define SD_LIMIT_grcmo_B	0		// gr-carmodel (0, 130 - 236)
#define SD_LIMIT_grcmo_T	236
#define SD_LIMIT_grimm_B	0		// gr-immunity
#define SD_LIMIT_grimm_T	15
#define SD_LIMIT_grcol_B	0		// gr-color
#define SD_LIMIT_grcol_T	99
#define SD_LIMIT_grrad_B	0		// gr-radio
#define SD_LIMIT_grrad_T	23
#define SD_LIMIT_grbtp_B	0		// gr-bombtype
#define SD_LIMIT_grbtp_T	5
#define SD_LIMIT_puass_B	100		// pu-assetmax
#define SD_LIMIT_puass_T	50000
#define SD_LIMIT_gdcmo_B	-1		// gd-carmodel (-1, 130 - 236)
#define SD_LIMIT_gdcmo_T	SD_LIMIT_grcmo_T
#define SD_LIMIT_gdpmo_B	9		// gd-pedmodel
#define SD_LIMIT_gdpmo_T	96
#define SD_LIMIT_gdwnu_B	SD_LIMIT_plwnu_B	// gd-weaponnumber
#define SD_LIMIT_gdwnu_T	SD_LIMIT_plwnu_T
#define SD_LIMIT_plcum_B	0.0f	// pl-currentmoney
#define SD_LIMIT_plcum_T	99000000.0f
#define SD_LIMIT_pliru_B	0		// pl-infiniterun
#define SD_LIMIT_pliru_T	1
#define SD_LIMIT_plfre_B	0		// pl-fastreload
#define SD_LIMIT_plfre_T	1
#define SD_LIMIT_plfpr_B	0		// pl-fireproof
#define SD_LIMIT_plfpr_T	1
#define SD_LIMIT_plmxh_B	100		// pl-maxhealth
#define SD_LIMIT_plmxh_T	200
#define SD_LIMIT_plmxa_B	SD_LIMIT_plmxh_B	// pl-maxarmor
#define SD_LIMIT_plmxa_T	SD_LIMIT_plmxh_T
#define SD_LIMIT_cgcmo_B	SD_LIMIT_gdcmo_B	// cg-carmodel (-1, 130 - 236)
#define SD_LIMIT_cgcmo_T	SD_LIMIT_grcmo_T
#define SD_LIMIT_cgx_B		-2000.0f		// cg-x
#define SD_LIMIT_cgx_T		2000.0f
#define SD_LIMIT_cgy_B		SD_LIMIT_cgx_B	// cg-y
#define SD_LIMIT_cgy_T		SD_LIMIT_cgx_T
#define SD_LIMIT_cgz_B		-20.0f			// cg-z
#define SD_LIMIT_cgz_T		500.0f
#define SD_LIMIT_cgrot_B	0.0f			// cg-rot
#define SD_LIMIT_cgrot_T	360.0f
#define SD_LIMIT_cgasp_B	-1				// cg-allowspawn
#define SD_LIMIT_cgasp_T	0
#define SD_LIMIT_cgcol_B	-1				// cg-color
#define SD_LIMIT_cgcol_T	SD_LIMIT_grcol_T
#define SD_LIMIT_cgala_B	0				// cg-alarm
#define SD_LIMIT_cgala_T	100
#define SD_LIMIT_cgloc_B	SD_LIMIT_cgala_B	// cg-lock
#define SD_LIMIT_cgloc_T	SD_LIMIT_cgala_T
#define SD_LIMIT_cgfsp_B	0			// cg-forcespawn
#define SD_LIMIT_cgfsp_T	1
#define SD_LIMIT_stinb_B	0			// st-infinitebullets
#define SD_LIMIT_stinb_T	1
#define SD_LIMIT_pumod_B	258
#define SD_LIMIT_pumod_T	650
#define SD_LIMIT_pupic_B	0
#define SD_LIMIT_pupic_T	1
#define SD_LIMIT_putyp_B	0
#define SD_LIMIT_putyp_T	18

//////////////////////////////////////////////////////////////////////
// Следующая функция сохраняет файл сохранения GTA VC (.b) на основе
// ранее сформированной структуры. Возвращает одно из следующих
// значений при возникновении ошибок при создании файла:
#define SD_SAVE_ERR_CannotCreateFile	-2001	// Не удаётся создать файл
// Код ошибки в случае успеха
#define SD_SAVE_SUCCESS					2
//
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_Save (schar *FilePath, struct SaveData *SD);
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// Обработчики дополнительных файлов (см. CommandInterpreter)
sint SaveData_LoadStats (struct SaveData *SD, schar *FilePath);
sint SaveData_SaveStats (struct SaveData *SD, schar *FilePath);
sint SaveData_LoadCG (struct SaveData *SD, schar *FilePath);
sint SaveData_SaveCG (struct SaveData *SD, schar *FilePath);
sint SaveData_LoadGarages (struct SaveData *SD, schar *FilePath);
sint SaveData_SaveGarages (struct SaveData *SD, schar *FilePath);
//////////////////////////////////////////////////////////////////////
