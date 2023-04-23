// Отключение предупреждений
#define _CRT_SECURE_NO_WARNINGS

// Подключение общего заголовка
#include "ViceCityToolset.h"

// Функция, отвечающая за вывод сообщений об ошибках. Возвращает код ошибки
// • Error - любой поддерживаемый код ошибки
schar *SaveData_ErrorPrompt (sint Error)
	{
	// Переменные
	static schar msg[SD_MaxStrSize];
	schar msg1[SD_MaxStrSize];

	// Построение сообщения
	switch (Error)
		{
		// Новые ошибки
		case SD_FIXED:
			sprintf (msg1, "Корректировка выполнена успешно");
			break;

		case SD_LOAD_SUCCESS:
			sprintf (msg1, "Файл успешно загружен");
			break;

		case SD_SAVE_SUCCESS:
			sprintf (msg1, "Файл успешно сохранён");
			break;

		// Ошибки в командах
		case SD_INTRPR_ERR_ValueOutOfRange:
			sprintf (msg1, "Указанное значение находится вне допустимого диапазона");
			break;

		case SD_INTRPR_ERR_ModeIsIncorrect:
			sprintf (msg1, "Недопустимый код режима");
			break;

		case SD_INTRPR_ERR_OpCodeIsIncorrect:
			sprintf (msg1, "Недопустимый код операции для данного режима");
			break;

		case SD_INTRPR_ERR_ParCodeIsIncorrect:
			sprintf (msg1, "Недопустимый код параметра для данных режима и операции");
			break;

			// Ошибки в файлах обмена
		case SD_INTRPR_ERR_StatsFileNotFound:
			sprintf (msg1, "Указанный файл статистики недоступен");
			break;

		case SD_INTRPR_ERR_StatsFileIsIncorrect:
			sprintf (msg1, "Указанный файл повреждён или не является файлом статистики");
			break;

		case SD_INTRPR_ERR_CannotCreateStatsFile:
			sprintf (msg1, "Не удаётся записать файл статистики");
			break;

		case SD_INTRPR_ERR_CGFileNotFound:
			sprintf (msg1, "Указанный файл параметров парковок недоступен");
			break;

		case SD_INTRPR_ERR_CGFileIsIncorrect:
			sprintf (msg1, "Указанный файл повреждён или не является файлом параметров парковок");
			break;

		case SD_INTRPR_ERR_CannotCreateCGFile:
			sprintf (msg1, "Не удаётся записать файл параметров парковок");
			break;

		case SD_INTRPR_ERR_GaragesFileNotFound:
			sprintf (msg1, "Указанный файл параметров гаражей недоступен");
			break;

		case SD_INTRPR_ERR_GaragesFileIsIncorrect:
			sprintf (msg1, "Указанный файл повреждён или не является файлом параметров гаражей");
			break;

		case SD_INTRPR_ERR_CannotCreateGaragesFile:
			sprintf (msg1, "Не удаётся записать файл параметров гаражей");
			break;

			// Ошибки сохранения и загрузки
		case SD_SAVE_ERR_CannotCreateFile:
			sprintf (msg1, "Не удаётся создать заданный файл. Возможно, выбранное расположение или файл недоступны для записи");
			break;

		case SD_INTRPR_ERR_FileNotLoaded:
			sprintf (msg1, "Файл сохранения не был загружен (структура пуста)");
			break;

		case SD_LOAD_ERR_FileNotFound:
			sprintf (msg1, "Указанный файл отсутствует или недоступен");
			break;

		case SD_LOAD_ERR_MemoryAllocationFailure:
			sprintf (msg1, "Не удаётся выделить память для хранения данных программы");
			break;

#define UEFE	"Неожиданный конец файла или ошибка чтения при загрузке "
		case SD_LOAD_ERR_LoadDP:
			sprintf (msg1, "%sобщих параметров сохранения", UEFE);
			break;

		case SD_LOAD_ERR_LoadSB:
			sprintf (msg1, "%sблока общих параметров массива переменных", UEFE);
			break;

		case SD_LOAD_ERR_LoadSBA:
			sprintf (msg1, "%sмассива переменных", UEFE);
			break;

		case SD_LOAD_ERR_LoadSBB:
			sprintf (msg1, "%sпродолжения блока переменных", UEFE);
			break;

		case SD_LOAD_ERR_LoadSC:
			sprintf (msg1, "%sобщих параметров массива скриптов", UEFE);
			break;

		case SD_LOAD_ERR_LoadSS:
			sprintf (msg1, "%sмассива скриптов", UEFE);
			break;

		case SD_LOAD_ERR_LoadPPL:
			sprintf (msg1, "%sобщих параметров массива игроков", UEFE);
			break;

		case SD_LOAD_ERR_LoadPPS:
			sprintf (msg1, "%sмассива игроков", UEFE);
			break;

		case SD_LOAD_ERR_LoadGR:
			sprintf (msg1, "%sблока описателей гаражей", UEFE);
			break;

		case SD_LOAD_ERR_LoadVS:
			sprintf (msg1, "%sобщих параметров массива транспортных средств", UEFE);
			break;

		case SD_LOAD_ERR_LoadVH:
			sprintf (msg1, "%sмассива транспортных средств", UEFE);
			break;

		case SD_LOAD_ERR_LoadSR:
			sprintf (msg1, "%sблока стримминга", UEFE);
			break;

		case SD_LOAD_ERR_LoadTS:
			sprintf (msg1, "%sблока описателей точек создания такси", UEFE);
			break;

		case SD_LOAD_ERR_LoadPH:
			sprintf (msg1, "%sобщих параметров массива трасс", UEFE);
			break;

		case SD_LOAD_ERR_LoadPHD:
			sprintf (msg1, "%sмассива трасс", UEFE);
			break;

		case SD_LOAD_ERR_LoadOP:
			sprintf (msg1, "%sобщих параметров массива скрипт-контролируемых объектов", UEFE);
			break;

		case SD_LOAD_ERR_LoadOS:
			sprintf (msg1, "%sмассива скрипт-контролируемых объектов", UEFE);
			break;

		case SD_LOAD_ERR_LoadCR:
			sprintf (msg1, "%sблока портовых кранов", UEFE);
			break;

		case SD_LOAD_ERR_LoadPU:
			sprintf (msg1, "%sблока собираемых объектов", UEFE);
			break;

		case SD_LOAD_ERR_LoadPI:
			sprintf (msg1, "%sблока телефонов-автоматов", UEFE);
			break;

		case SD_LOAD_ERR_LoadRL:
			sprintf (msg1, "%sблока зон респауна", UEFE);
			break;

		case SD_LOAD_ERR_LoadRB:
			sprintf (msg1, "%sблока радарных указателей", UEFE);
			break;

		case SD_LOAD_ERR_LoadZB:
			sprintf (msg1, "%sблока зон карты", UEFE);
			break;

		case SD_LOAD_ERR_LoadGD:
			sprintf (msg1, "%sблока банд", UEFE);
			break;

		case SD_LOAD_ERR_LoadCG:
			sprintf (msg1, "%sблока парковок", UEFE);
			break;

		case SD_LOAD_ERR_LoadPR:
			sprintf (msg1, "%sобщих параметров массива эффектов анимации", UEFE);
			break;

		case SD_LOAD_ERR_LoadPRD:
			sprintf (msg1, "%sмассива эффектов анимации", UEFE);
			break;

		case SD_LOAD_ERR_LoadAU:
			sprintf (msg1, "%sобщих параметров массива аудиоскриптов", UEFE);
			break;

		case SD_LOAD_ERR_LoadAS:
			sprintf (msg1, "%sмассива аудиоскриптов", UEFE);
			break;

		case SD_LOAD_ERR_LoadSP:
			sprintf (msg1, "%sблока параметров пути перемещения спецобъектов", UEFE);
			break;

		case SD_LOAD_ERR_LoadPL:
			sprintf (msg1, "%sблока информации о текущем игроке", UEFE);
			break;

		case SD_LOAD_ERR_LoadST:
			sprintf (msg1, "%sблока статистики игры", UEFE);
			break;

		case SD_LOAD_ERR_LoadTZ:
			sprintf (msg1, "%sблока триггер-зон", UEFE);
			break;

		case SD_LOAD_ERR_LoadPT:
			sprintf (msg1, "%sблока типов персонажей", UEFE);
			break;

		case SD_LOAD_ERR_LoadCS:
			sprintf (msg1, "%sблока контрольной суммы или её сравнении", UEFE);
			break;

			// Неизвестные ошибки (по идее, таких быть не должно)
		default:
			sprintf (msg1, "Неопознанная ошибка в процессе обработки файла сохранения");
			break;
		}

	// Возврат сформированного сообщения
	if (Error >= 0)
		sprintf (msg, "> %s", msg1);
	else
		sprintf (msg, "\x13 %s", msg1);

	return msg;
	}
