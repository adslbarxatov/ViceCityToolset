using System.Globalization;
using System.Text;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает общие для всех видов дескрипторов элементы
	/// </summary>
	public abstract class HandlingDescriptor
		{
		/// <summary>
		/// Возвращает идентификатор транспортного средства согласно файлу default.ide 
		/// </summary>
		public string VehicleIdentifier
			{
			get
				{
				return a_VehicleIdentifier;
				}
			}
		internal string a_VehicleIdentifier = "";

		/// <summary>
		/// Возвращает статус инициализации данного экземпляра
		/// </summary>
		public bool IsInited
			{
			get
				{
				return isInited;
				}
			}
		internal bool isInited = false;

		// Сокращённое имя для формата чисел
		internal NumberFormatInfo nfi = HandlingSupport.ValuesFormat;
		}

	/// <summary>
	/// Класс описывает вспомогательные функции
	/// </summary>
	public static class HandlingSupport
		{
		/// <summary>
		/// Возвращает числовой формат, поддерживающий десятичную точку
		/// </summary>
		public static NumberFormatInfo ValuesFormat
			{
			get
				{
				return RDLocale.GetCulture (RDLanguages.en_us).NumberFormat;
				}
			}

		/// <summary>
		/// Метод контроля нового значения параметра на вхождение в допустимый диапазон
		/// </summary>
		/// <param name="Maximum">Верхняя граница допустимого диапазона</param>
		/// <param name="Minimum">Нижняя граница допустимого диапазона</param>
		/// <param name="Value">Проверяемое значение</param>
		/// <returns>Значение, скорректированное с учётом диапазона</returns>
		public static float CheckRange (float Value, float Minimum, float Maximum)
			{
			if (Value < Minimum)
				return Minimum;
			else if (Value > Maximum)
				return Maximum;

			return Value;
			}

		/// <summary>
		/// Метод контроля нового значения параметра на вхождение в допустимый диапазон
		/// </summary>
		/// <param name="Maximum">Верхняя граница допустимого диапазона</param>
		/// <param name="Minimum">Нижняя граница допустимого диапазона</param>
		/// <param name="Value">Проверяемое значение</param>
		/// <returns>Значение, скорректированное с учётом диапазона</returns>
		public static int CheckRange (int Value, int Minimum, int Maximum)
			{
			if (Value < Minimum)
				return Minimum;
			else if (Value > Maximum)
				return Maximum;

			return Value;
			}

		/// <summary>
		/// Метод контроля нового значения параметра на вхождение в допустимый диапазон
		/// </summary>
		/// <param name="Maximum">Верхняя граница допустимого диапазона</param>
		/// <param name="Minimum">Нижняя граница допустимого диапазона</param>
		/// <param name="Value">Проверяемое значение</param>
		/// <returns>Значение, скорректированное с учётом диапазона</returns>
		public static uint CheckRange (uint Value, uint Minimum, uint Maximum)
			{
			if (Value < Minimum)
				return Minimum;
			else if (Value > Maximum)
				return Maximum;

			return Value;
			}

		/// <summary>
		/// Метод генерирует заголовок раздела файла
		/// </summary>
		/// <param name="LastLetterNumber">Номер последней буквы-идентификатора столбца</param>
		public static string CreateHeader (uint LastLetterNumber)
			{
			string res = "  A";
			Encoding enc = RDGenerics.GetEncoding (RDEncodings.UTF8);

			for (int i = 2; (i <= 26) && (i <= LastLetterNumber); i++)
				res += ("\t\t" + enc.GetString ([(byte)(0x40 + i)]));

			if (LastLetterNumber <= 26)
				return res;

			for (int i = 1; (i <= 26) && (i <= LastLetterNumber - 26); i++)
				res += ("\t\tA" + enc.GetString ([(byte)(0x40 + i)]));

			return res;
			}
		}
	}
