using System.Globalization;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает общие для всех видов дескрипторов элементы. Предполагает наследование
	/// </summary>
	public class HandlingDescriptor
		{
		/// <summary>
		/// Переменная используется для переключения десятичной запятой на точку
		/// </summary>
		protected CultureInfo cie = new CultureInfo ("en-us");

		/// <summary>
		/// Возвращает статус инициализации класса
		/// </summary>
		public bool IsInited
			{
			get
				{
				return isInited;
				}
			}
		/// <summary>
		/// Переменная хранит статус инициализации класса
		/// </summary>
		protected bool isInited = false;

		/// <summary>
		/// Метод контроля нового значения параметра типа float на вхождение в допустимый диапазон
		/// </summary>
		/// <param name="Maximum">Верхняя граница допустимого диапазона</param>
		/// <param name="Minimum">Нижняя граница допустимого диапазона</param>
		/// <param name="Value">Проверяемое значение</param>
		/// <returns>Значение, скорректированное с учётом диапазона</returns>
		protected float CheckRange (float Value, float Minimum, float Maximum)
			{
			if (Value < Minimum)
				{
				return Minimum;
				}
			else if (Value > Maximum)
				{
				return Maximum;
				}
			else
				{
				return Value;
				}
			}

		/// <summary>
		/// Метод контроля нового значения параметра типа int на вхождение в допустимый диапазон
		/// </summary>
		/// <param name="Maximum">Верхняя граница допустимого диапазона</param>
		/// <param name="Minimum">Нижняя граница допустимого диапазона</param>
		/// <param name="Value">Проверяемое значение</param>
		/// <returns>Значение, скорректированное с учётом диапазона</returns>
		protected int CheckRange (int Value, int Minimum, int Maximum)
			{
			if (Value < Minimum)
				{
				return Minimum;
				}
			else if (Value > Maximum)
				{
				return Maximum;
				}
			else
				{
				return Value;
				}
			}

		/// <summary>
		/// Метод контроля нового значения параметра типа uint на вхождение в допустимый диапазон
		/// </summary>
		/// <param name="Maximum">Верхняя граница допустимого диапазона</param>
		/// <param name="Minimum">Нижняя граница допустимого диапазона</param>
		/// <param name="Value">Проверяемое значение</param>
		/// <returns>Значение, скорректированное с учётом диапазона</returns>
		protected uint CheckRange (uint Value, uint Minimum, uint Maximum)
			{
			if (Value < Minimum)
				{
				return Minimum;
				}
			else if (Value > Maximum)
				{
				return Maximum;
				}
			else
				{
				return Value;
				}
			}

		// #region A
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

		/// <summary>
		/// Переменная хранит идентификатор транспортного средства согласно файлу default.ide 
		/// </summary>
		protected string a_VehicleIdentifier = "";
		// #endregion
		}
	}
