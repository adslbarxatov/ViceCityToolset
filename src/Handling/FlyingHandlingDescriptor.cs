namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает специальный дескриптор летательного аппарата
	/// </summary>
	public class FlyingHandlingDescriptor: HandlingDescriptor
		{
		// Переменные
		private const uint expectedColumnsCount = 19;

		/// <summary>
		/// Строка заголовка таблицы данных дескриптора
		/// </summary>
		public static string TableHeader
			{
			get
				{
				return HandlingSupport.CreateHeader (expectedColumnsCount);
				}
			}

		/// <summary>
		/// Символ-признак дескриптора
		/// </summary>
		public const string IdentifyingSymbol = "$";

		/*
		/// <summary>
		/// Строка заголовка таблицы данных дескриптора
		/// </summary>
		public const string TableHeader = "	A		B		C		D			E		F		G		H		I		J		" +
			"K		L		M		N		O		P			Q			R			S";
		*/

		/// <summary>
		/// Конструктор. Загружает параметры транспортного средства из массива строк
		/// </summary>
		/// <param name="Values">Массив строк, содержащий значения параметров</param>
		public FlyingHandlingDescriptor (string[] Values)
			{
			// Контроль массива
			if ((Values.Length != expectedColumnsCount + 1) || (Values[0] != IdentifyingSymbol))
				return;

			// Загрузка параметров
			a_VehicleIdentifier = Values[1];

			try
				{
				NonControlledAcceleration = float.Parse (Values[2], nfi);
				ControlledAcceleration = float.Parse (Values[3], nfi);
				TurningLeftRightForce = float.Parse (Values[4], nfi);
				TurningLeftRightStabilization = float.Parse (Values[5], nfi);
				MovingAltitudeLoss = float.Parse (Values[6], nfi);
				RotationForce = float.Parse (Values[7], nfi);
				RotationStabilization = float.Parse (Values[8], nfi);
				NoseDeflectionForce = float.Parse (Values[9], nfi);
				NoseDeflectionStabilization = float.Parse (Values[10], nfi);
				LiftingSpeedMultiplier = float.Parse (Values[11], nfi);
				NoseAngleLiftingFactor = float.Parse (Values[12], nfi);
				MovingResistance = float.Parse (Values[13], nfi);
				TurningXResistance = float.Parse (Values[14], nfi);
				TurningYResistance = float.Parse (Values[15], nfi);
				TurningZResistance = float.Parse (Values[16], nfi);
				AccelerationXResistance = float.Parse (Values[17], nfi);
				AccelerationYResistance = float.Parse (Values[18], nfi);
				AccelerationZResistance = float.Parse (Values[19], nfi);
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
				return result;

			// Сборка
			result = IdentifyingSymbol + " " + VehicleIdentifier.PadRight (8, ' ') + "\t";
			result += (b_NonControlledAcceleration.ToString ("F4", nfi) + "\t");
			result += (c_ControlledAcceleration.ToString ("F2", nfi) + "\t");
			result += (d_TurningLeftRightForce.ToString ("F5", nfi) + "\t");
			result += (e_TurningLeftRightStabilization.ToString ("F4", nfi) + "\t");
			result += (f_MovingAltitudeLoss.ToString ("F2", nfi) + "\t");
			result += (g_RotationForce.ToString ("F5", nfi) + "\t");
			result += (h_RotationStabilization.ToString ("F3", nfi) + "\t");
			result += (i_NoseDeflectionForce.ToString ("F4", nfi) + "\t");
			result += (j_NoseDeflectionStabilization.ToString ("F4", nfi) + "\t");
			result += (k_LiftingSpeedMultiplier.ToString ("F3", nfi) + "\t");
			result += (l_NoseAngleLiftingFactor.ToString ("F3", nfi) + "\t");
			result += (m_MovingResistance.ToString ("F3", nfi) + "\t");
			result += (n_TurningXResistance.ToString ("F3", nfi) + "\t");
			result += (o_TurningYResistance.ToString ("F3", nfi) + "\t");
			result += (p_TurningZResistance.ToString ("F3", nfi) + "\t");
			result += (q_AccelerationXResistance.ToString ("F1", nfi).PadLeft (8, ' ') + "\t");
			result += (r_AccelerationYResistance.ToString ("F1", nfi).PadLeft (8, ' ') + "\t");
			result += s_AccelerationZResistance.ToString ("F1", nfi).PadLeft (8, ' ');

			// Завершено
			return result;
			}

		// Region A -> HandlingDescriptor

		#region B
		/// <summary>
		/// Минимальное ускорение при инерционном полёте
		/// </summary>
		public const float NonControlledAcceleration_Min = 0.0f;

		/// <summary>
		/// Максимальное ускорение при инерционном полёте
		/// </summary>
		public const float NonControlledAcceleration_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт ускорение при инерционном полёте
		/// </summary>
		public float NonControlledAcceleration
			{
			get
				{
				return b_NonControlledAcceleration;
				}
			set
				{
				b_NonControlledAcceleration = HandlingSupport.CheckRange (value, NonControlledAcceleration_Min,
					NonControlledAcceleration_Max);
				}
			}
		private float b_NonControlledAcceleration = 0.3f;
		#endregion

		#region C
		/// <summary>
		/// Минимальное ускорение при контролируемом полёте
		/// </summary>
		public const float ControlledAcceleration_Min = 0.0f;

		/// <summary>
		/// Максимальное ускорение при контролируемом полёте
		/// </summary>
		public const float ControlledAcceleration_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт ускорение при контролируемом полёте
		/// </summary>
		public float ControlledAcceleration
			{
			get
				{
				return c_ControlledAcceleration;
				}
			set
				{
				c_ControlledAcceleration = HandlingSupport.CheckRange (value, ControlledAcceleration_Min,
					ControlledAcceleration_Max);
				}
			}
		private float c_ControlledAcceleration = 0.75f;
		#endregion

		#region D
		/// <summary>
		/// Минимальная сила поворота влево и вправо
		/// </summary>
		public const float TurningLeftRightForce_Min = -0.01f;

		/// <summary>
		/// Максимальная сила поворота влево и вправо
		/// </summary>
		public const float TurningLeftRightForce_Max = 0.0f;

		/// <summary>
		/// Возвращает или задаёт силу поворота влево и вправо
		/// </summary>
		public float TurningLeftRightForce
			{
			get
				{
				return d_TurningLeftRightForce;
				}
			set
				{
				d_TurningLeftRightForce = HandlingSupport.CheckRange (value, TurningLeftRightForce_Min,
					TurningLeftRightForce_Max);
				}
			}
		private float d_TurningLeftRightForce = -0.001f;
		#endregion

		#region E
		/// <summary>
		/// Минимальный показатель стабилизации поворота влево и вправо
		/// </summary>
		public const float TurningLeftRightStabilization_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель стабилизации поворота влево и вправо
		/// </summary>
		public const float TurningLeftRightStabilization_Max = 0.5f;

		/// <summary>
		/// Возвращает или задаёт показатель стабилизации поворота влево и вправо
		/// </summary>
		public float TurningLeftRightStabilization
			{
			get
				{
				return e_TurningLeftRightStabilization;
				}
			set
				{
				e_TurningLeftRightStabilization = HandlingSupport.CheckRange (value, TurningLeftRightStabilization_Min,
					TurningLeftRightStabilization_Max);
				}
			}
		private float e_TurningLeftRightStabilization = 0.02f;
		#endregion

		#region F
		/// <summary>
		/// Минимальная потеря высоты при движении в одном направлении
		/// </summary>
		public const float MovingAltitudeLoss_Min = 0.0f;

		/// <summary>
		/// Максимальная потеря высоты при движении в одном направлении
		/// </summary>
		public const float MovingAltitudeLoss_Max = 0.5f;

		/// <summary>
		/// Возвращает или задаёт потерю высоты при движении в одном направлении
		/// </summary>
		public float MovingAltitudeLoss
			{
			get
				{
				return f_MovingAltitudeLoss;
				}
			set
				{
				f_MovingAltitudeLoss = HandlingSupport.CheckRange (value, MovingAltitudeLoss_Min, MovingAltitudeLoss_Max);
				}
			}
		private float f_MovingAltitudeLoss = 0.1f;
		#endregion

		#region G
		/// <summary>
		/// Минимальная сила поворота вокруг оси
		/// </summary>
		public const float RotationForce_Min = 0.0f;

		/// <summary>
		/// Максимальная сила поворота вокруг оси
		/// </summary>
		public const float RotationForce_Max = 0.1f;

		/// <summary>
		/// Возвращает или задаёт силу поворота вокруг оси
		/// </summary>
		public float RotationForce
			{
			get
				{
				return g_RotationForce;
				}
			set
				{
				g_RotationForce = HandlingSupport.CheckRange (value, RotationForce_Min, RotationForce_Max);
				}
			}
		private float g_RotationForce = 0.0065f;
		#endregion

		#region H
		/// <summary>
		/// Минимальный показатель стабилизации поворота вокруг оси
		/// </summary>
		public const float RotationStabilization_Min = -2.0f;

		/// <summary>
		/// Максимальный показатель стабилизации поворота вокруг оси
		/// </summary>
		public const float RotationStabilization_Max = 20.0f;

		/// <summary>
		/// Возвращает или задаёт показатель стабилизации поворота вокруг оси
		/// </summary>
		public float RotationStabilization
			{
			get
				{
				return h_RotationStabilization;
				}
			set
				{
				h_RotationStabilization = HandlingSupport.CheckRange (value, RotationStabilization_Min,
					RotationStabilization_Max);
				}
			}
		private float h_RotationStabilization = 3.0f;
		#endregion

		#region I
		/// <summary>
		/// Минимальная сила наклона носа вперёд и назад
		/// </summary>
		public const float NoseDeflectionForce_Min = 0.0f;

		/// <summary>
		/// Максимальная сила наклона носа вперёд и назад
		/// </summary>
		public const float NoseDeflectionForce_Max = 0.1f;

		/// <summary>
		/// Возвращает или задаёт силу наклона носа вперёд и назад
		/// </summary>
		public float NoseDeflectionForce
			{
			get
				{
				return i_NoseDeflectionForce;
				}
			set
				{
				i_NoseDeflectionForce = HandlingSupport.CheckRange (value, NoseDeflectionForce_Min,
					NoseDeflectionForce_Max);
				}
			}
		private float i_NoseDeflectionForce = 0.0065f;
		#endregion

		#region J
		/// <summary>
		/// Минимальный показатель стабилизации наклона носа вперёд и назад
		/// </summary>
		public const float NoseDeflectionStabilization_Min = -2.0f;

		/// <summary>
		/// Максимальный показатель стабилизации наклона носа вперёд и назад
		/// </summary>
		public const float NoseDeflectionStabilization_Max = 20.0f;

		/// <summary>
		/// Возвращает или задаёт показатель стабилизации наклона носа вперёд и назад
		/// </summary>
		public float NoseDeflectionStabilization
			{
			get
				{
				return j_NoseDeflectionStabilization;
				}
			set
				{
				j_NoseDeflectionStabilization = HandlingSupport.CheckRange (value, NoseDeflectionStabilization_Min,
					NoseDeflectionStabilization_Max);
				}
			}
		private float j_NoseDeflectionStabilization = 3.0f;
		#endregion

		#region K
		/// <summary>
		/// Минимальный множитель скорости, дающий набор высоты
		/// </summary>
		public const float LiftingSpeedMultiplier_Min = 0.0f;

		/// <summary>
		/// Максимальный множитель скорости, дающий набор высоты
		/// </summary>
		public const float LiftingSpeedMultiplier_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт множитель скорости, дающий набор высоты
		/// </summary>
		public float LiftingSpeedMultiplier
			{
			get
				{
				return k_LiftingSpeedMultiplier;
				}
			set
				{
				k_LiftingSpeedMultiplier = HandlingSupport.CheckRange (value, LiftingSpeedMultiplier_Min,
					LiftingSpeedMultiplier_Max);
				}
			}
		private float k_LiftingSpeedMultiplier = 0.7f;
		#endregion

		#region L
		/// <summary>
		/// Минимальный показатель влияния наклона носа на подъёмную силу
		/// </summary>
		public const float NoseAngleLiftingFactor_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель влияния наклона носа на подъёмную силу
		/// </summary>
		public const float NoseAngleLiftingFactor_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт показатель влияния наклона носа на подъёмную силу
		/// </summary>
		public float NoseAngleLiftingFactor
			{
			get
				{
				return l_NoseAngleLiftingFactor;
				}
			set
				{
				l_NoseAngleLiftingFactor = HandlingSupport.CheckRange (value, NoseAngleLiftingFactor_Min,
					NoseAngleLiftingFactor_Max);
				}
			}
		private float l_NoseAngleLiftingFactor = 0.1f;
		#endregion

		#region M
		/// <summary>
		/// Минимальное сопротивление движению
		/// </summary>
		public const float MovingResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление движению
		/// </summary>
		public const float MovingResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление движению
		/// </summary>
		public float MovingResistance
			{
			get
				{
				return m_MovingResistance;
				}
			set
				{
				m_MovingResistance = HandlingSupport.CheckRange (value, MovingResistance_Min, MovingResistance_Max);
				}
			}
		private float m_MovingResistance = 0.996f;
		#endregion

		#region N
		/// <summary>
		/// Минимальное сопротивление наклону вперёд и назад
		/// </summary>
		public const float TurningXResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление наклону вперёд и назад
		/// </summary>
		public const float TurningXResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление наклону вперёд и назад
		/// </summary>
		public float TurningXResistance
			{
			get
				{
				return n_TurningXResistance;
				}
			set
				{
				n_TurningXResistance = HandlingSupport.CheckRange (value, TurningXResistance_Min, TurningXResistance_Max);
				}
			}
		private float n_TurningXResistance = 0.9f;
		#endregion

		#region O
		/// <summary>
		/// Минимальное сопротивление наклону влево и вправо
		/// </summary>
		public const float TurningYResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление наклону влево и вправо
		/// </summary>
		public const float TurningYResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление наклону влево и вправо
		/// </summary>
		public float TurningYResistance
			{
			get
				{
				return o_TurningYResistance;
				}
			set
				{
				o_TurningYResistance = HandlingSupport.CheckRange (value, TurningYResistance_Min, TurningYResistance_Max);
				}
			}
		private float o_TurningYResistance = 0.9f;
		#endregion

		#region P
		/// <summary>
		/// Минимальное сопротивление повороту влево и вправо
		/// </summary>
		public const float TurningZResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление повороту влево и вправо
		/// </summary>
		public const float TurningZResistance_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление повороту влево и вправо
		/// </summary>
		public float TurningZResistance
			{
			get
				{
				return p_TurningZResistance;
				}
			set
				{
				p_TurningZResistance = HandlingSupport.CheckRange (value, TurningZResistance_Min, TurningZResistance_Max);
				}
			}
		private float p_TurningZResistance = 0.99f;
		#endregion

		#region Q
		/// <summary>
		/// Минимальное сопротивление ускорению при движении влево и вправо
		/// </summary>
		public const float AccelerationXResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление ускорению при движении влево и вправо
		/// </summary>
		public const float AccelerationXResistance_Max = 1000.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление ускорению при движении влево и вправо
		/// </summary>
		public float AccelerationXResistance
			{
			get
				{
				return q_AccelerationXResistance;
				}
			set
				{
				q_AccelerationXResistance = HandlingSupport.CheckRange (value, AccelerationXResistance_Min,
					AccelerationXResistance_Max);
				}
			}
		private float q_AccelerationXResistance = 0.0f;
		#endregion

		#region R
		/// <summary>
		/// Минимальное сопротивление ускорению при движении вперёд и назад
		/// </summary>
		public const float AccelerationYResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление ускорению при движении вперёд и назад
		/// </summary>
		public const float AccelerationYResistance_Max = 1000.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление ускорению при движении вперёд и назад
		/// </summary>
		public float AccelerationYResistance
			{
			get
				{
				return r_AccelerationYResistance;
				}
			set
				{
				r_AccelerationYResistance = HandlingSupport.CheckRange (value, AccelerationYResistance_Min,
					AccelerationYResistance_Max);
				}
			}
		private float r_AccelerationYResistance = 0.0f;
		#endregion

		#region S
		/// <summary>
		/// Минимальное сопротивление ускорению при движении вверх и вниз
		/// </summary>
		public const float AccelerationZResistance_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление ускорению при движении вверх и вниз
		/// </summary>
		public const float AccelerationZResistance_Max = 1000.0f;

		/// <summary>
		/// Возвращает или задаёт сопротивление ускорению при движении вверх и вниз
		/// </summary>
		public float AccelerationZResistance
			{
			get
				{
				return s_AccelerationZResistance;
				}
			set
				{
				s_AccelerationZResistance = HandlingSupport.CheckRange (value, AccelerationZResistance_Min,
					AccelerationZResistance_Max);
				}
			}
		private float s_AccelerationZResistance = 5.0f;
		#endregion
		}
	}
