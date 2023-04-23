namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает специальный дескриптор моторной лодки
	/// </summary>
	public class BoatHandlingDescriptor:HandlingDescriptor
		{
		/// <summary>
		/// Символ-признак дескриптора
		/// </summary>
		public const string IdentifyingSymbol = "%";

		/// <summary>
		/// Строка заголовка таблицы данных дескриптора
		/// </summary>
		public const string TableHeader = "	A		B		C		D		E		F		G		H		I		J		" +
			"K		L		M		N		O";

		/// <summary>
		/// Конструктор. Загружает параметры транспортного средства из массива строк
		/// </summary>
		/// <param name="Values">Массив строк, содержащий значения параметров</param>
		public BoatHandlingDescriptor (string[] Values)
			{
			// Контроль массива
			if ((Values.Length != 16) || (Values[0] != IdentifyingSymbol))
				{
				return;
				}

			// Загрузка параметров
			a_VehicleIdentifier = Values[1];

			try
				{
				ForwardThrust = float.Parse (Values[2], cie.NumberFormat);
				UpwardThrust = float.Parse (Values[3], cie.NumberFormat);
				d_ThrustAppZ = float.Parse (Values[4], cie.NumberFormat);
				AquaPlaneForce = float.Parse (Values[5], cie.NumberFormat);
				AquaPlaneLimit = float.Parse (Values[6], cie.NumberFormat);
				AquaPlaneOffset = float.Parse (Values[7], cie.NumberFormat);
				WavesLoudness = float.Parse (Values[8], cie.NumberFormat);
				MoveXResistance = float.Parse (Values[9], cie.NumberFormat);
				MoveYResistance = float.Parse (Values[10], cie.NumberFormat);
				MoveZResistance = float.Parse (Values[11], cie.NumberFormat);
				TurnXResistance = float.Parse (Values[12], cie.NumberFormat);
				TurnYResistance = float.Parse (Values[13], cie.NumberFormat);
				TurnZResistance = float.Parse (Values[14], cie.NumberFormat);
				ViewCameraTopPosition = float.Parse (Values[15], cie.NumberFormat);
				}
			catch
				{
				return;
				}

			// Завершено
			isInited = true;
			}

		/// <summary>
		/// Метод собирает все параметры дескриптора в строку для вывода в файл
		/// </summary>
		/// <returns>Строка параметров</returns>
		public string GetHDAsString ()
			{
			// Переменные
			string result = "";

			// Контроль инициализации
			if (!isInited)
				{
				return result;
				}

			// Сборка
			result = IdentifyingSymbol + " " + VehicleIdentifier.PadRight (8, ' ') + "\t";
			result += (b_ForwardThrust.ToString ("F2", cie.NumberFormat) + "\t");
			result += (c_UpwardThrust.ToString ("F2", cie.NumberFormat) + "\t");
			result += (d_ThrustAppZ.ToString ("F2", cie.NumberFormat) + "\t");
			result += (e_AquaPlaneForce.ToString ("F2", cie.NumberFormat) + "\t");
			result += (f_AquaPlaneLimit.ToString ("F2", cie.NumberFormat) + "\t");
			result += (g_AquaPlaneOffset.ToString ("F2", cie.NumberFormat) + "\t");
			result += (h_WavesLoudness.ToString ("F1", cie.NumberFormat).PadLeft (4, ' ') + "\t");
			result += (i_MoveXResistance.ToString ("F3", cie.NumberFormat) + "\t");
			result += (j_MoveYResistance.ToString ("F3", cie.NumberFormat) + "\t");
			result += (k_MoveZResistance.ToString ("F3", cie.NumberFormat) + "\t");
			result += (l_TurnXResistance.ToString ("F3", cie.NumberFormat) + "\t");
			result += (m_TurnYResistance.ToString ("F3", cie.NumberFormat) + "\t");
			result += (n_TurnZResistance.ToString ("F3", cie.NumberFormat) + "\t");
			result += o_ViewCameraTopPosition.ToString ("F1", cie.NumberFormat);

			// Завершено
			return result;
			}

		// Region A -> HandlingDescriptor

		#region B
		/// <summary>
		/// Минимальная сила толчка при ускорении
		/// </summary>
		public const float ForwardThrust_Min = 0.0f;

		/// <summary>
		/// Максимальная сила толчка при ускорении
		/// </summary>
		public const float ForwardThrust_Max = 3.0f;

		/// <summary>
		/// Возвращает или задаёт силу толчка при ускорении
		/// </summary>
		public float ForwardThrust
			{
			get
				{
				return b_ForwardThrust;
				}
			set
				{
				b_ForwardThrust = CheckRange (value, ForwardThrust_Min, ForwardThrust_Max);
				}
			}
		private float b_ForwardThrust = 0.7f;
		#endregion

		#region C
		/// <summary>
		/// Минимальная сила подпрыгивания над водой при ускорении
		/// </summary>
		public const float UpwardThrust_Min = 0.0f;

		/// <summary>
		/// Максимальная сила подпрыгивания над водой при ускорении
		/// </summary>
		public const float UpwardThrust_Max = 3.0f;

		/// <summary>
		/// Возвращает или задаёт силу подпрыгивания над водой при ускорении
		/// </summary>
		public float UpwardThrust
			{
			get
				{
				return c_UpwardThrust;
				}
			set
				{
				c_UpwardThrust = CheckRange (value, UpwardThrust_Min, UpwardThrust_Max);
				}
			}
		private float c_UpwardThrust = 0.7f;
		#endregion

		#region D
		/// <summary>
		/// Минимальное значение параметра ThrustAppZ
		/// </summary>
		public const float ThrustAppZ_Min = 0.0f;

		/// <summary>
		/// Максимальное значение параметра ThrustAppZ
		/// </summary>
		public const float ThrustAppZ_Max = 2.0f;

		/// <summary>
		/// Возвращает параметр ThrustAppZ
		/// </summary>
		public float ThrustAppZ
			{
			get
				{
				return d_ThrustAppZ;
				}
			}
		private float d_ThrustAppZ = 0.5f;
		#endregion

		#region E
		/// <summary>
		/// Минимальная сила ускорения за счёт движения по волнам
		/// </summary>
		public const float AquaPlaneForce_Min = 0.0f;

		/// <summary>
		/// Максимальная сила ускорения за счёт движения по волнам
		/// </summary>
		public const float AquaPlaneForce_Max = 100.0f;

		/// <summary>
		/// Возвращает или задаёт силу ускорения за счёт движения по волнам
		/// </summary>
		public float AquaPlaneForce
			{
			get
				{
				return e_AquaPlaneForce;
				}
			set
				{
				e_AquaPlaneForce = CheckRange (value, AquaPlaneForce_Min, AquaPlaneForce_Max);
				}
			}
		private float e_AquaPlaneForce = 7.0f;
		#endregion

		#region F
		/// <summary>
		/// Минимальное ограничение ускорения за счёт движения по волнам
		/// </summary>
		public const float AquaPlaneLimit_Min = 0.0f;

		/// <summary>
		/// Максимальное ограничение ускорения за счёт движения по волнам
		/// </summary>
		public const float AquaPlaneLimit_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт ограничение ускорения за счёт движения по волнам
		/// </summary>
		public float AquaPlaneLimit
			{
			get
				{
				return f_AquaPlaneLimit;
				}
			set
				{
				f_AquaPlaneLimit = CheckRange (value, AquaPlaneLimit_Min, AquaPlaneLimit_Max);
				}
			}
		private float f_AquaPlaneLimit = 0.7f;
		#endregion

		#region G
		/// <summary>
		/// Минимальная высота подъёма носа при движении по волнам
		/// </summary>
		public const float AquaPlaneOffset_Min = -5.0f;

		/// <summary>
		/// Максимальная высота подъёма носа при движении по волнам
		/// </summary>
		public const float AquaPlaneOffset_Max = 5.0f;

		/// <summary>
		/// Возвращает или задаёт высоту подъёма носа при движении по волнам
		/// </summary>
		public float AquaPlaneOffset
			{
			get
				{
				return g_AquaPlaneOffset;
				}
			set
				{
				g_AquaPlaneOffset = CheckRange (value, AquaPlaneOffset_Min, AquaPlaneOffset_Max);
				}
			}
		private float g_AquaPlaneOffset = 0.0f;
		#endregion

		#region H
		/// <summary>
		/// Минимальная громкость шума волн
		/// </summary>
		public const float WavesLoudness_Min = 0.0f;

		/// <summary>
		/// Максимальная громкость шума волн
		/// </summary>
		public const float WavesLoudness_Max = 30.0f;

		/// <summary>
		/// Возвращает или задаёт громкость шума волн
		/// </summary>
		public float WavesLoudness
			{
			get
				{
				return h_WavesLoudness;
				}
			set
				{
				h_WavesLoudness = CheckRange (value, WavesLoudness_Min, WavesLoudness_Max);
				}
			}
		private float h_WavesLoudness = 3.0f;
		#endregion

		#region I
		/// <summary>
		/// Минимальное сопротивление боковому движению
		/// </summary>
		public const float MoveXResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление боковому движению
		/// </summary>
		public const float MoveXResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление боковому движению
		/// </summary>
		public float MoveXResistance
			{
			get
				{
				return i_MoveXResistance;
				}
			set
				{
				i_MoveXResistance = CheckRange (value, MoveXResistance_Min, MoveXResistance_Max);
				}
			}
		private float i_MoveXResistance = 0.8f;
		#endregion

		#region J
		/// <summary>
		/// Минимальное сопротивление движению вперёд и назад
		/// </summary>
		public const float MoveYResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление движению вперёд и назад
		/// </summary>
		public const float MoveYResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление движению вперёд и назад
		/// </summary>
		public float MoveYResistance
			{
			get
				{
				return j_MoveYResistance;
				}
			set
				{
				j_MoveYResistance = CheckRange (value, MoveYResistance_Min, MoveYResistance_Max);
				}
			}
		private float j_MoveYResistance = 0.998f;
		#endregion

		#region K
		/// <summary>
		/// Минимальное сопротивление вертикальному движению
		/// </summary>
		public const float MoveZResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление вертикальному движению
		/// </summary>
		public const float MoveZResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление вертикальному движению
		/// </summary>
		public float MoveZResistance
			{
			get
				{
				return k_MoveZResistance;
				}
			set
				{
				k_MoveZResistance = CheckRange (value, MoveZResistance_Min, MoveZResistance_Max);
				}
			}
		private float k_MoveZResistance = 0.995f;
		#endregion

		#region L
		/// <summary>
		/// Минимальное сопротивление перевороту вперёд и назад
		/// </summary>
		public const float TurnXResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление перевороту вперёд и назад
		/// </summary>
		public const float TurnXResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление перевороту вперёд и назад
		/// </summary>
		public float TurnXResistance
			{
			get
				{
				return l_TurnXResistance;
				}
			set
				{
				l_TurnXResistance = CheckRange (value, TurnXResistance_Min, TurnXResistance_Max);
				}
			}
		private float l_TurnXResistance = 0.85f;
		#endregion

		#region M
		/// <summary>
		/// Минимальное сопротивление перевороту влево и вправо
		/// </summary>
		public const float TurnYResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление перевороту влево и вправо
		/// </summary>
		public const float TurnYResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление перевороту влево и вправо
		/// </summary>
		public float TurnYResistance
			{
			get
				{
				return m_TurnYResistance;
				}
			set
				{
				m_TurnYResistance = CheckRange (value, TurnYResistance_Min, TurnYResistance_Max);
				}
			}
		private float m_TurnYResistance = 0.96f;
		#endregion

		#region N
		/// <summary>
		/// Минимальное сопротивление повороту налево и направо
		/// </summary>
		public const float TurnZResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление повороту налево и направо
		/// </summary>
		public const float TurnZResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление повороту налево и направо
		/// </summary>
		public float TurnZResistance
			{
			get
				{
				return n_TurnZResistance;
				}
			set
				{
				n_TurnZResistance = CheckRange (value, TurnZResistance_Min, TurnZResistance_Max);
				}
			}
		private float n_TurnZResistance = 0.96f;
		#endregion

		#region O
		/// <summary>
		/// Минимальная высота положения камеры обзора
		/// </summary>
		public const float ViewCameraTopPosition_Min = 0.0f;

		/// <summary>
		/// Максимальная высота положения камеры обзора
		/// </summary>
		public const float ViewCameraTopPosition_Max = 10.0f;

		/// <summary>
		/// Возвращает или задаёт высоту положения камеры обзора
		/// </summary>
		public float ViewCameraTopPosition
			{
			get
				{
				return o_ViewCameraTopPosition;
				}
			set
				{
				o_ViewCameraTopPosition = CheckRange (value, ViewCameraTopPosition_Min, ViewCameraTopPosition_Max);
				}
			}
		private float o_ViewCameraTopPosition = 4.0f;
		#endregion
		}
	}
