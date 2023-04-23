﻿using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму программы
	/// </summary>
	public partial class HandlingForm:Form
		{
		// Переменные
		private HandlingProvider hp;
		private bool loading = false;
		private int lastSelectedIndex = 0;

		// Метод возвращает название транспорта по его идентификатору в файле handling.cfg
		private string GetVehicheNameByID (string ID)
			{
			switch (ID)
				{
				case "ADMIRAL":
					return "Admiral";
				case "AIRTRAIN":
					return "Airbus";
				case "AMBULAN":
					return "Ambulance";
				case "ANGEL":
					return "Angel";
				case "BAGGAGE":
					return "Baggage";
				case "BANSHEE":
					return "Banshee";
				case "BARRACKS":
					return "Barracks";
				case "BENSON":
					return "Benson";
				case "BFINJECT":
					return "BF injection";
				case "BIKE":
					return "PCJ 600";
				case "BLISTAC":
					return "Blista compact";
				case "BLOODRA":
					return "Bloodring A";
				case "BLOODRB":
					return "Bloodring B";
				case "BOBCAT":
					return "Bobcat";
				case "BOXVILLE":
					return "Boxville";
				case "BURRITO":
					return "Burrito";
				case "BUS":
					return "Bus";
				case "CABBIE":
					return "Cabbie";
				case "CHEETAH":
					return "Cheetah";
				case "COACH":
					return "Coach";
				case "COASTGRD":
					return "Coast guard";
				case "COASTMAV":
					return "VCN maverick";
				case "COMET":
					return "Comet";
				case "CUBAN":
					return "Cuban hermes";
				case "CUPBOAT":
					return "Jetmax";
				case "DEADDODO":
					return "Plane";
				case "DELUXO":
					return "Deluxo";
				case "DESPERAD":
					return "Mesa grande";
				case "DINGHY":
					return "Dinghy";
				case "DIRTBIKE":
					return "Sanchez";
				case "ENFORCER":
					return "Enforcer";
				case "ESPERANT":
					return "Esperanto";
				case "FBICAR":
					return "FBI washington";
				case "FBIRANCH":
					return "FBI rancher";
				case "FIRETRUK":
					return "Firetruck";
				case "FLATBED":
					return "Flatbed";
				case "FREEWAY":
					return "Freeway";
				case "GANGBUR":
					return "Gang burrito";
				case "GLENDALE":
					return "Glendale";
				case "GOLFCART":
					return "Caddy";
				case "GREENWOO":
					return "Greenwood";
				case "HELI":
					return "Patrol helicopter";
				case "HERMES":
					return "Hermes";
				case "HOTRING":
					return "Hotring";
				case "HUNTER":
					return "Hunter";
				case "IDAHO":
					return "Idaho";
				case "INFERNUS":
					return "Infernus";
				case "KAUFMAN":
					return "Kauffman cab";
				case "LANDSTAL":
					return "Landstalker";
				case "LINERUN":
					return "Line runner";
				case "LOVEFIST":
					return "Lovefist";
				case "MAFIA":
					return "Sentinel XS";
				case "MANANA":
					return "Manana";
				case "MARQUIS":
					return "Marquis";
				case "MAVERICK":
					return "Maverick";
				case "MOONBEAM":
					return "Moonbeam";
				case "MOPED":
					return "Faggio/Pizzaboy";
				case "MRWHOOP":
					return "Mr Whoopie";
				case "MULE":
					return "Mule";
				case "OCEANIC":
					return "Oceanic";
				case "PACKER":
					return "Packer";
				case "PATRIOT":
					return "Patriot";
				case "PEREN":
					return "Perennial";
				case "PHEONIX":
					return "Pheonix";
				case "POLICE":
					return "Police";
				case "POLMAV":
					return "Police maverick";
				case "PONY":
					return "Pony";
				case "PREDATOR":
					return "Predator";
				case "RANCHER":
					return "Rancher";
				case "RCBANDIT":
					return "RC car";
				case "RCBARON":
					return "RC plane";
				case "RCCOPTER":
					return "RC helicopter";
				case "RCGOBLIN":
					return "RC demolition";
				case "REEFER":
					return "Reefer";
				case "REGINA":
					return "Regina";
				case "RHINO":
					return "Rhino";
				case "RIO":
					return "Rio";
				case "ROMERO":
					return "Romero";
				case "RUMPO":
					return "Rumpo";
				case "SABRE1":
					return "Sabre";
				case "SABRETUR":
					return "Sabre turbo";
				case "SANDKING":
					return "Sandking";
				case "SEAPLANE":
					return "Skimmer";
				case "SEASPAR":
					return "Sea sparrow";
				case "SECURICA":
					return "Securicar";
				case "SENTINEL":
					return "Sentinel";
				case "SPAND":
					return "Spand express";
				case "SPARROW":
					return "Sparrow";
				case "SPEEDER":
					return "Speeder";
				case "SQUALO":
					return "Squalo";
				case "STALLION":
					return "Stallion";
				case "STINGER":
					return "Stinger";
				case "STRETCH":
					return "Stretch";
				case "TAXI":
					return "Taxi";
				case "TOPFUN":
					return "Topfun";
				case "TRASH":
					return "Trash master";
				case "TROPIC":
					return "Tropic";
				case "VIRGO":
					return "Virgo";
				case "VOODOO":
					return "Voodoo";
				case "WALTON":
					return "Walton";
				case "WASHING":
					return "Washington";
				case "YANKEE":
					return "Yankee";
				case "ZEBRA":
					return "Zebra";
				default:
					return "—";
				}
			}

		/// <summary>
		/// Конструктор. Запускает главную форму программы
		/// </summary>
		public HandlingForm ()
			{
			InitializeComponent ();

			// Настройка контролов
			this.Text = ProgramDescription.AssemblyTitle;

			// Настройка диалогов
			OpenCfgDialog.Filter = SaveCfgDialog.Filter = "Файлы handling.cfg|handling.cfg";
			OpenCfgDialog.Title = "Выберите файл handling.cfg";
			SaveCfgDialog.Title = "Укажите новое место для файла handling.cfg";

			// Отключение управления до начала редактирования
			A_.Enabled = false;
			SaveCfgFile.Enabled = false;
			AddComments.Enabled = false;

			#region Основные дескрипторы
			SetGeneric1State (false);	// Начальная блокировка
			B_.Minimum = (decimal)GenericHandlingDescriptor.VehicleMass_Min;
			B_.Maximum = (decimal)GenericHandlingDescriptor.VehicleMass_Max;
			C_.Minimum = (decimal)GenericHandlingDescriptor.XDimension_Min;
			C_.Maximum = (decimal)GenericHandlingDescriptor.XDimension_Max;
			D_.Minimum = (decimal)GenericHandlingDescriptor.YDimension_Min;
			D_.Maximum = (decimal)GenericHandlingDescriptor.YDimension_Max;
			E_.Minimum = (decimal)GenericHandlingDescriptor.ZDimension_Min;
			E_.Maximum = (decimal)GenericHandlingDescriptor.ZDimension_Max;
			F_.Minimum = (decimal)GenericHandlingDescriptor.XCentreOfMass_Min;
			F_.Maximum = (decimal)GenericHandlingDescriptor.XCentreOfMass_Max;
			G_.Minimum = (decimal)GenericHandlingDescriptor.YCentreOfMass_Min;
			G_.Maximum = (decimal)GenericHandlingDescriptor.YCentreOfMass_Max;
			H_.Minimum = (decimal)GenericHandlingDescriptor.ZCentreOfMass_Min;
			H_.Maximum = (decimal)GenericHandlingDescriptor.ZCentreOfMass_Max;
			I_.Minimum = (decimal)GenericHandlingDescriptor.PercentSubmerged_Min;
			I_.Maximum = (decimal)GenericHandlingDescriptor.PercentSubmerged_Max;
			// J_, K_, L_ устанавливаются при загрузке параметров
			M_.Minimum = (decimal)GenericHandlingDescriptor.NumberOfGears_Min;
			M_.Maximum = (decimal)GenericHandlingDescriptor.NumberOfGears_Max;
			N_.Minimum = (decimal)GenericHandlingDescriptor.MaxVelocity_Min;
			N_.Maximum = (decimal)GenericHandlingDescriptor.MaxVelocity_Max;
			O_.Minimum = (decimal)GenericHandlingDescriptor.EngineAcceleration_Min;
			O_.Maximum = (decimal)GenericHandlingDescriptor.EngineAcceleration_Max;

			P_.Items.Add ("4 колеса");
			P_.Items.Add ("передние колёса");
			P_.Items.Add ("задние колёса");
			Q_.Items.Add ("бензиновый");
			Q_.Items.Add ("дизельный");
			Q_.Items.Add ("электрический");

			// R_, S_
			T_.Minimum = (decimal)GenericHandlingDescriptor.ABS_Min;
			T_.Maximum = (decimal)GenericHandlingDescriptor.ABS_Max;
			U_.Minimum = (decimal)GenericHandlingDescriptor.SteeringLock_Min;
			U_.Maximum = (decimal)GenericHandlingDescriptor.SteeringLock_Max;
			// V_, W_
			X_.Minimum = (decimal)GenericHandlingDescriptor.SeatOffsetDistance_Min;
			X_.Maximum = (decimal)GenericHandlingDescriptor.SeatOffsetDistance_Max;
			Y_.Minimum = (decimal)GenericHandlingDescriptor.CollisionDamageMultiplier_Min;
			Y_.Maximum = (decimal)GenericHandlingDescriptor.CollisionDamageMultiplier_Max;
			Z_.Minimum = (decimal)GenericHandlingDescriptor.MonetaryValue_Min;
			Z_.Maximum = (decimal)GenericHandlingDescriptor.MonetaryValue_Max;
			AA_.Minimum = (decimal)GenericHandlingDescriptor.SuspensionUpperLimit_Min;
			AA_.Maximum = (decimal)GenericHandlingDescriptor.SuspensionUpperLimit_Max;
			// AB_, AC_
			AD_.Minimum = (decimal)GenericHandlingDescriptor.SuspensionAntiDiveMultiplier_Min;
			AD_.Maximum = (decimal)GenericHandlingDescriptor.SuspensionAntiDiveMultiplier_Max;

			AF_.Items.Add ("длинные");
			AF_.Items.Add ("маленькие");
			AF_.Items.Add ("большие");
			AF_.Items.Add ("высокие");
			AG_.Items.Add ("длинные");
			AG_.Items.Add ("маленькие");
			AG_.Items.Add ("большие");
			AG_.Items.Add ("высокие");
			#endregion

			#region Флаги
			SetGeneric2State (false);
			AE_1_4.Items.Add ("стандартный багажник");
			AE_1_4.Items.Add ("дверь прикреплена к верху");
			AE_1_4.Items.Add ("дверь прикреплена к низу");
			AE_1_4.Items.Add ("дверь не открывается");
			AE_2.Items.Add ("стандартный");
			AE_2.Items.Add ("нет дверей");
			AE_2.Items.Add ("двойная дверь сзади");
			AE_2.Items.Add ("двери как у автобуса");
			AE_2.Items.Add ("двери как у спорткара");
			AE_5.Items.Add ("автомобиль");
			AE_5.Items.Add ("мотоцикл");
			AE_5.Items.Add ("вертолёт");
			AE_5.Items.Add ("самолёт");
			AE_5.Items.Add ("моторная лодка");
			#endregion

			#region Моторные лодки
			boat_B_.Minimum = (decimal)BoatHandlingDescriptor.ForwardThrust_Min;
			boat_C_.Minimum = (decimal)BoatHandlingDescriptor.UpwardThrust_Min;
			boat_D_.Minimum = (decimal)BoatHandlingDescriptor.ThrustAppZ_Min;
			boat_E_.Minimum = (decimal)BoatHandlingDescriptor.AquaPlaneForce_Min;
			boat_F_.Minimum = (decimal)BoatHandlingDescriptor.AquaPlaneLimit_Min;
			boat_G_.Minimum = (decimal)BoatHandlingDescriptor.AquaPlaneOffset_Min;
			boat_H_.Minimum = (decimal)BoatHandlingDescriptor.WavesLoudness_Min;
			boat_I_.Minimum = (decimal)BoatHandlingDescriptor.MoveXResistance_Min;
			boat_J_.Minimum = (decimal)BoatHandlingDescriptor.MoveYResistance_Min;
			boat_K_.Minimum = (decimal)BoatHandlingDescriptor.MoveZResistance_Min;
			boat_L_.Minimum = (decimal)BoatHandlingDescriptor.TurnXResistance_Min;
			boat_M_.Minimum = (decimal)BoatHandlingDescriptor.TurnYResistance_Min;
			boat_N_.Minimum = (decimal)BoatHandlingDescriptor.TurnZResistance_Min;
			boat_O_.Minimum = (decimal)BoatHandlingDescriptor.ViewCameraTopPosition_Min;

			boat_B_.Maximum = (decimal)BoatHandlingDescriptor.ForwardThrust_Max;
			boat_C_.Maximum = (decimal)BoatHandlingDescriptor.UpwardThrust_Max;
			boat_D_.Maximum = (decimal)BoatHandlingDescriptor.ThrustAppZ_Max;
			boat_E_.Maximum = (decimal)BoatHandlingDescriptor.AquaPlaneForce_Max;
			boat_F_.Maximum = (decimal)BoatHandlingDescriptor.AquaPlaneLimit_Max;
			boat_G_.Maximum = (decimal)BoatHandlingDescriptor.AquaPlaneOffset_Max;
			boat_H_.Maximum = (decimal)BoatHandlingDescriptor.WavesLoudness_Max;
			boat_I_.Maximum = (decimal)BoatHandlingDescriptor.MoveXResistance_Max;
			boat_J_.Maximum = (decimal)BoatHandlingDescriptor.MoveYResistance_Max;
			boat_K_.Maximum = (decimal)BoatHandlingDescriptor.MoveZResistance_Max;
			boat_L_.Maximum = (decimal)BoatHandlingDescriptor.TurnXResistance_Max;
			boat_M_.Maximum = (decimal)BoatHandlingDescriptor.TurnYResistance_Max;
			boat_N_.Maximum = (decimal)BoatHandlingDescriptor.TurnZResistance_Max;
			boat_O_.Maximum = (decimal)BoatHandlingDescriptor.ViewCameraTopPosition_Max;
			#endregion

			#region Мотоциклы
			bike_B_.Minimum = (decimal)BikeHandlingDescriptor.ForwardLeaningCoM_Min;
			bike_C_.Minimum = (decimal)BikeHandlingDescriptor.ForwardLeaningForce_Min;
			bike_D_.Minimum = (decimal)BikeHandlingDescriptor.BackwardLeaningCoM_Min;
			bike_E_.Minimum = (decimal)BikeHandlingDescriptor.BackwardLeaningForce_Min;
			bike_F_.Minimum = (decimal)BikeHandlingDescriptor.MaxLeaningAngle_Min;
			bike_G_.Minimum = (decimal)BikeHandlingDescriptor.MaxDriverLeaningAngle_Min;
			bike_H_.Minimum = (decimal)BikeHandlingDescriptor.MaxDecelerationLeaningAngle_Min;
			bike_I_.Minimum = (decimal)BikeHandlingDescriptor.SteeringOnSpeed_Min;
			bike_J_.Minimum = (decimal)BikeHandlingDescriptor.CoMWithoutDriver_Min;
			bike_K_.Minimum = (decimal)BikeHandlingDescriptor.SteeringOnSlippery_Min;
			bike_L_.Minimum = (decimal)BikeHandlingDescriptor.StoppieAngle_Min;
			bike_M_.Minimum = (decimal)BikeHandlingDescriptor.WheelieAngle_Min;
			bike_N_.Minimum = (decimal)BikeHandlingDescriptor.WheelieStabilization_Min;
			bike_O_.Minimum = (decimal)BikeHandlingDescriptor.SteeringOnWheelie_Min;
			bike_P_.Minimum = (decimal)BikeHandlingDescriptor.StoppieStabilization_Min;

			bike_B_.Maximum = (decimal)BikeHandlingDescriptor.ForwardLeaningCoM_Max;
			bike_C_.Maximum = (decimal)BikeHandlingDescriptor.ForwardLeaningForce_Max;
			bike_D_.Maximum = (decimal)BikeHandlingDescriptor.BackwardLeaningCoM_Max;
			bike_E_.Maximum = (decimal)BikeHandlingDescriptor.BackwardLeaningForce_Max;
			bike_F_.Maximum = (decimal)BikeHandlingDescriptor.MaxLeaningAngle_Max;
			bike_G_.Maximum = (decimal)BikeHandlingDescriptor.MaxDriverLeaningAngle_Max;
			bike_H_.Maximum = (decimal)BikeHandlingDescriptor.MaxDecelerationLeaningAngle_Max;
			bike_I_.Maximum = (decimal)BikeHandlingDescriptor.SteeringOnSpeed_Max;
			bike_J_.Maximum = (decimal)BikeHandlingDescriptor.CoMWithoutDriver_Max;
			bike_K_.Maximum = (decimal)BikeHandlingDescriptor.SteeringOnSlippery_Max;
			bike_L_.Maximum = (decimal)BikeHandlingDescriptor.StoppieAngle_Max;
			bike_M_.Maximum = (decimal)BikeHandlingDescriptor.WheelieAngle_Max;
			bike_N_.Maximum = (decimal)BikeHandlingDescriptor.WheelieStabilization_Max;
			bike_O_.Maximum = (decimal)BikeHandlingDescriptor.SteeringOnWheelie_Max;
			bike_P_.Maximum = (decimal)BikeHandlingDescriptor.StoppieStabilization_Max;
			#endregion

			#region Летательные аппараты
			flying_B_.Minimum = (decimal)FlyingHandlingDescriptor.NonControlledAcceleration_Min;
			flying_C_.Minimum = (decimal)FlyingHandlingDescriptor.ControlledAcceleration_Min;
			flying_D_.Minimum = (decimal)FlyingHandlingDescriptor.TurningLeftRightForce_Min;
			flying_E_.Minimum = (decimal)FlyingHandlingDescriptor.TurningLeftRightStabilization_Min;
			flying_F_.Minimum = (decimal)FlyingHandlingDescriptor.MovingAltitudeLoss_Min;
			flying_G_.Minimum = (decimal)FlyingHandlingDescriptor.RotationForce_Min;
			flying_H_.Minimum = (decimal)FlyingHandlingDescriptor.RotationStabilization_Min;
			flying_I_.Minimum = (decimal)FlyingHandlingDescriptor.NoseDeflectionForce_Min;
			flying_J_.Minimum = (decimal)FlyingHandlingDescriptor.NoseDeflectionStabilization_Min;
			flying_K_.Minimum = (decimal)FlyingHandlingDescriptor.LiftingSpeedMultiplier_Min;
			flying_L_.Minimum = (decimal)FlyingHandlingDescriptor.NoseAngleLiftingFactor_Min;
			flying_M_.Minimum = (decimal)FlyingHandlingDescriptor.MovingResistance_Min;
			flying_N_.Minimum = (decimal)FlyingHandlingDescriptor.TurningXResistance_Min;
			flying_O_.Minimum = (decimal)FlyingHandlingDescriptor.TurningYResistance_Min;
			flying_P_.Minimum = (decimal)FlyingHandlingDescriptor.TurningZResistance_Min;
			flying_Q_.Minimum = (decimal)FlyingHandlingDescriptor.AccelerationXResistance_Min;
			flying_R_.Minimum = (decimal)FlyingHandlingDescriptor.AccelerationYResistance_Min;
			flying_S_.Minimum = (decimal)FlyingHandlingDescriptor.AccelerationZResistance_Min;

			flying_B_.Maximum = (decimal)FlyingHandlingDescriptor.NonControlledAcceleration_Max;
			flying_C_.Maximum = (decimal)FlyingHandlingDescriptor.ControlledAcceleration_Max;
			flying_D_.Maximum = (decimal)FlyingHandlingDescriptor.TurningLeftRightForce_Max;
			flying_E_.Maximum = (decimal)FlyingHandlingDescriptor.TurningLeftRightStabilization_Max;
			flying_F_.Maximum = (decimal)FlyingHandlingDescriptor.MovingAltitudeLoss_Max;
			flying_G_.Maximum = (decimal)FlyingHandlingDescriptor.RotationForce_Max;
			flying_H_.Maximum = (decimal)FlyingHandlingDescriptor.RotationStabilization_Max;
			flying_I_.Maximum = (decimal)FlyingHandlingDescriptor.NoseDeflectionForce_Max;
			flying_J_.Maximum = (decimal)FlyingHandlingDescriptor.NoseDeflectionStabilization_Max;
			flying_K_.Maximum = (decimal)FlyingHandlingDescriptor.LiftingSpeedMultiplier_Max;
			flying_L_.Maximum = (decimal)FlyingHandlingDescriptor.NoseAngleLiftingFactor_Max;
			flying_M_.Maximum = (decimal)FlyingHandlingDescriptor.MovingResistance_Max;
			flying_N_.Maximum = (decimal)FlyingHandlingDescriptor.TurningXResistance_Max;
			flying_O_.Maximum = (decimal)FlyingHandlingDescriptor.TurningYResistance_Max;
			flying_P_.Maximum = (decimal)FlyingHandlingDescriptor.TurningZResistance_Max;
			flying_Q_.Maximum = (decimal)FlyingHandlingDescriptor.AccelerationXResistance_Max;
			flying_R_.Maximum = (decimal)FlyingHandlingDescriptor.AccelerationYResistance_Max;
			flying_S_.Maximum = (decimal)FlyingHandlingDescriptor.AccelerationZResistance_Max;
			#endregion

			this.ShowDialog ();
			}

		// Установка состояния элементов вкладки Основные параметры
		private void SetGeneric1State (bool State)
			{
			B_.Enabled = State;
			C_.Enabled = State;
			D_.Enabled = State;
			E_.Enabled = State;
			F_.Enabled = State;
			G_.Enabled = State;
			H_.Enabled = State;
			I_.Enabled = State;
			J_.Enabled = State;
			K_.Enabled = State;
			L_.Enabled = State;
			M_.Enabled = State;
			N_.Enabled = State;
			O_.Enabled = State;
			P_.Enabled = State;
			Q_.Enabled = State;
			R_.Enabled = State;
			S_.Enabled = State;
			//T_.Enabled = State;	// Изменение не разрешено
			U_.Enabled = State;
			V_.Enabled = State;
			W_.Enabled = State;
			X_.Enabled = State;
			Y_.Enabled = State;
			Z_.Enabled = State;
			AA_.Enabled = State;
			AB_.Enabled = State;
			AC_.Enabled = State;
			AD_.Enabled = State;
			AF_.Enabled = State;
			AG_.Enabled = State;
			}

		// Установка состояния элементов вкладки Флаги
		private void SetGeneric2State (bool State)
			{
			AE_1_1.Enabled = State;
			AE_1_2.Enabled = State;
			AE_1_3.Enabled = State;
			AE_1_4.Enabled = State;
			AE_2.Enabled = State;
			AE_3_1.Enabled = State;
			//AE_3_4.Enabled = State;	// Запрещено
			//AE_4_1.Enabled = State;	// Запрещено
			AE_4_2.Enabled = State;
			AE_4_3.Enabled = State;
			AE_4_4.Enabled = State;
			//AE_5.Enabled = State;		// Запрещено
			AE_6_1.Enabled = State;
			AE_6_2.Enabled = State;
			AE_6_3.Enabled = State;
			AE_6_4.Enabled = State;
			AE_7_1.Enabled = State;
			AE_7_2.Enabled = State;
			AE_7_3.Enabled = State;
			//AE_7_4.Enabled = State;	// Запрещено
			}

		// Установка состояния элементов вкладки Спецификации для лодок
		private void SetBoatState (bool State)
			{
			boat_B_.Enabled = State;
			boat_C_.Enabled = State;
			//boat_D_.Enabled = State;	// Запрещено
			boat_E_.Enabled = State;
			boat_F_.Enabled = State;
			boat_G_.Enabled = State;
			boat_H_.Enabled = State;
			boat_I_.Enabled = State;
			boat_J_.Enabled = State;
			boat_K_.Enabled = State;
			boat_L_.Enabled = State;
			boat_M_.Enabled = State;
			boat_N_.Enabled = State;
			boat_O_.Enabled = State;
			}

		// Установка состояния элементов вкладки Спецификации для мотоциклов
		private void SetBikeState (bool State)
			{
			bike_B_.Enabled = State;
			bike_C_.Enabled = State;
			bike_D_.Enabled = State;
			bike_E_.Enabled = State;
			bike_F_.Enabled = State;
			bike_G_.Enabled = State;
			bike_H_.Enabled = State;
			bike_I_.Enabled = State;
			bike_J_.Enabled = State;
			bike_K_.Enabled = State;
			bike_L_.Enabled = State;
			bike_M_.Enabled = State;
			bike_N_.Enabled = State;
			bike_O_.Enabled = State;
			bike_P_.Enabled = State;
			}

		// Установка состояния элементов вкладки Спецификации для летательных аппаратов
		private void SetFlyingState (bool State)
			{
			flying_B_.Enabled = State;
			flying_C_.Enabled = State;
			flying_D_.Enabled = State;
			flying_E_.Enabled = State;
			flying_F_.Enabled = State;
			flying_G_.Enabled = State;
			flying_H_.Enabled = State;
			flying_I_.Enabled = State;
			flying_J_.Enabled = State;
			flying_K_.Enabled = State;
			flying_L_.Enabled = State;
			flying_M_.Enabled = State;
			flying_N_.Enabled = State;
			flying_O_.Enabled = State;
			flying_P_.Enabled = State;
			flying_Q_.Enabled = State;
			flying_R_.Enabled = State;
			flying_S_.Enabled = State;
			}

		// Открытие файла
		private void OpenCfgFile_Click (object sender, System.EventArgs e)
			{
			OpenCfgDialog.ShowDialog ();
			}

		private void OpenCfgDialog_FileOk (object sender, System.ComponentModel.CancelEventArgs e)
			{
			// Попытка загрузки файла данных
			HandlingProvider hpTMP = new HandlingProvider (OpenCfgDialog.FileName);
			switch (hpTMP.InitStatus)
				{
				case HandlingProvider.InitStatuses.BrokenDescriptor:
					MessageBox.Show ("Одна или несколько строк в выбранном файле имеют некорректную структуру. " +
						"Загрузка и работа с файлом невозможна", ProgramDescription.AssemblyTitle, MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation);
					return;

				case HandlingProvider.InitStatuses.FileIsEmpty:
					MessageBox.Show ("В файле отсутствуют основные дескрипторы транспортных средств", ProgramDescription.AssemblyTitle,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;

				case HandlingProvider.InitStatuses.FileNotAvailable:
					MessageBox.Show ("Выбранный файл недоступен для загрузки. Убедитесь, что файл не занят другой программой, " +
						"и повторите попытку", ProgramDescription.AssemblyTitle, MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation);
					return;
				}

			// Загрузка
			hp = new HandlingProvider (OpenCfgDialog.FileName);

			// Запуск контролов
			if (!B_.Enabled)
				{
				A_.Enabled = SaveCfgFile.Enabled = AddComments.Enabled = true;
				SetGeneric1State (true);
				SetGeneric2State (true);
				// Остальные включаются при необходимости
				}

			// Инициализация списка транспортов
			loading = true;		// Блокировка обработчика списка
			for (uint i = 0; i < hp.GenericHDsCount; i++)
				{
				A_.Items.Add (GetVehicheNameByID (hp.GetGenericHD (i).VehicleIdentifier));
				}
			lastSelectedIndex = 0;
			A_.Text = A_.Items[lastSelectedIndex].ToString ();
			loading = false;
			}

		// Выбор новой позиции в списке транспортов
		private void A__SelectedIndexChanged (object sender, System.EventArgs e)
			{
			// Запись предыдущих значений
			if (!loading)
				{
				GenericHandlingDescriptor ghd = hp.GetGenericHD ((uint)lastSelectedIndex);

				#region Основные дескрипторы
				ghd.VehicleMass = (float)B_.Value;
				ghd.XDimension = (float)C_.Value;
				ghd.YDimension = (float)D_.Value;
				ghd.ZDimension = (float)E_.Value;
				ghd.XCentreOfMass = (float)F_.Value;
				ghd.YCentreOfMass = (float)G_.Value;
				ghd.ZCentreOfMass = (float)H_.Value;
				ghd.PercentSubmerged = (int)I_.Value;
				ghd.NumberOfGears = (uint)M_.Value;
				ghd.MaxVelocity = (float)N_.Value;
				ghd.EngineAcceleration = (float)O_.Value;

				switch (P_.SelectedIndex)
					{
					case 0:
						ghd.DriveType = GenericHandlingDescriptor.DriveTypes.FourWheelsDrive;
						break;

					case 1:
						ghd.DriveType = GenericHandlingDescriptor.DriveTypes.ForwardWheelsDrive;
						break;

					case 2:
						ghd.DriveType = GenericHandlingDescriptor.DriveTypes.RearWheelsDrive;
						break;
					}
				switch (Q_.SelectedIndex)
					{
					case 0:
						ghd.EngineType = GenericHandlingDescriptor.EngineTypes.Petrol;
						break;

					case 1:
						ghd.EngineType = GenericHandlingDescriptor.EngineTypes.Diesel;
						break;

					case 2:
						ghd.EngineType = GenericHandlingDescriptor.EngineTypes.Electric;
						break;
					}

				//ghd.ABS = (uint)T_.Value;
				ghd.SteeringLock = (float)U_.Value;
				ghd.SeatOffsetDistance = (float)X_.Value;
				ghd.CollisionDamageMultiplier = (float)Y_.Value;
				ghd.MonetaryValue = (uint)Z_.Value;
				ghd.SuspensionUpperLimit = (float)AA_.Value;
				ghd.SuspensionAntiDiveMultiplier = (float)AD_.Value;

				switch (AF_.SelectedIndex)
					{
					case 0:
						ghd.FrontLights = GenericHandlingDescriptor.LightsTypes.Long;
						break;

					case 1:
						ghd.FrontLights = GenericHandlingDescriptor.LightsTypes.Small;
						break;

					case 2:
						ghd.FrontLights = GenericHandlingDescriptor.LightsTypes.Big;
						break;

					case 3:
						ghd.FrontLights = GenericHandlingDescriptor.LightsTypes.High;
						break;
					}
				switch (AG_.SelectedIndex)
					{
					case 0:
						ghd.RearLights = GenericHandlingDescriptor.LightsTypes.Long;
						break;

					case 1:
						ghd.RearLights = GenericHandlingDescriptor.LightsTypes.Small;
						break;

					case 2:
						ghd.RearLights = GenericHandlingDescriptor.LightsTypes.Big;
						break;

					case 3:
						ghd.RearLights = GenericHandlingDescriptor.LightsTypes.High;
						break;
					}

				// Транспорт-зависимые параметры
				if (ghd.VehicleType != GenericHandlingDescriptor.VehicleTypes.Boat)
					{
					ghd.TractionMultiplier = (float)J_.Value;
					ghd.TractionLoss = (float)K_.Value;
					ghd.TractionBias = (float)L_.Value;
					ghd.BrakeDeceleration = (float)R_.Value;
					ghd.BrakeBias = (float)S_.Value;
					ghd.SuspensionForceLevel = (float)V_.Value;
					ghd.SuspensionDampingLevel = (float)W_.Value;
					ghd.SuspensionLowerLimit = (float)AB_.Value;
					ghd.SuspensionBias = (float)AC_.Value;
					}
				else
					{
					ghd.BankForceMultiplier = (float)J_.Value;
					ghd.RudderTurnForce = (float)K_.Value;
					ghd.SpeedSteerFalloff = (float)L_.Value;
					ghd.VerticalWaveHitLimit = (float)R_.Value;
					ghd.ForwardWaveHitBrake = (float)S_.Value;
					ghd.WaterResistanceMultiplier = (float)V_.Value;
					ghd.WaterDampingMultiplier = (float)W_.Value;
					ghd.HandbrakeDragMultiplier = (float)AB_.Value;
					ghd.SideslipForce = (float)AC_.Value;
					}
				#endregion

				#region Флаги
				ghd.FirstGearBoost = AE_1_1.Checked;
				ghd.SecondGearBoost = AE_1_2.Checked;
				ghd.ReversedBonnet = AE_1_3.Checked;

				switch (AE_1_4.SelectedIndex)
					{
					case 0:
						ghd.TailgateType = GenericHandlingDescriptor.TailgateTypes.DefaultBoot;
						break;

					case 1:
						ghd.TailgateType = GenericHandlingDescriptor.TailgateTypes.AttachedToTop;
						break;

					case 2:
						ghd.TailgateType = GenericHandlingDescriptor.TailgateTypes.AttachedToBottom;
						break;

					case 3:
						ghd.TailgateType = GenericHandlingDescriptor.TailgateTypes.Locked;
						break;
					}
				switch (AE_2.SelectedIndex)
					{
					case 0:
						ghd.DoorsType = GenericHandlingDescriptor.DoorsTypes.Default;
						break;

					case 1:
						ghd.DoorsType = GenericHandlingDescriptor.DoorsTypes.NoDoors;
						break;

					case 2:
						ghd.DoorsType = GenericHandlingDescriptor.DoorsTypes.IsVan;
						break;

					case 3:
						ghd.DoorsType = GenericHandlingDescriptor.DoorsTypes.IsBus;
						break;

					case 4:
						ghd.DoorsType = GenericHandlingDescriptor.DoorsTypes.IsLow;
						break;
					}

				ghd.DoubleExhaust = AE_3_1.Checked;
				//ghd.NonPlayerStabilizer = AE_3_4.Checked;
				//ghd.NeutralHandling = AE_4_1.Checked;
				ghd.HasNoRoof = AE_4_2.Checked;
				ghd.TransportIsBig = AE_4_3.Checked;
				ghd.HalogenLights = AE_4_4.Checked;

				/*switch (AE_5.SelectedIndex)
					{
					case 0:
						ghd.VehicleType = GenericHandlingDescriptor.VehicleTypes.Car;
						break;

					case 1:
						ghd.VehicleType = GenericHandlingDescriptor.VehicleTypes.Bike;
						break;

					case 2:
						ghd.VehicleType = GenericHandlingDescriptor.VehicleTypes.Helicopter;
						break;

					case 3:
						ghd.VehicleType = GenericHandlingDescriptor.VehicleTypes.Plane;
						break;

					case 4:
						ghd.VehicleType = GenericHandlingDescriptor.VehicleTypes.Boat;
						break;
					}*/

				ghd.NoExhaust = AE_6_1.Checked;
				ghd.RearWheelFirst = AE_6_2.Checked;
				ghd.HandbrakeTyre = AE_6_3.Checked;
				ghd.SitInBoat = AE_6_4.Checked;
				ghd.FatRearWheels = AE_7_1.Checked;
				ghd.NarrowFrontWheels = AE_7_2.Checked;
				ghd.GoodInSand = AE_7_3.Checked;
				//ghd.SpecialFlight = AE_7_4.Checked;
				#endregion

				#region Моторные лодки
				BoatHandlingDescriptor boathd = hp.GetBoatHD ((uint)lastSelectedIndex);
				if (boathd != null)
					{
					boathd.ForwardThrust = (float)boat_B_.Value;
					boathd.UpwardThrust = (float)boat_C_.Value;
					//boathd.ThrustAppZ = (float)boat_D_.Value;
					boathd.AquaPlaneForce = (float)boat_E_.Value;
					boathd.AquaPlaneLimit = (float)boat_F_.Value;
					boathd.AquaPlaneOffset = (float)boat_G_.Value;
					boathd.WavesLoudness = (float)boat_H_.Value;
					boathd.MoveXResistance = (float)boat_I_.Value;
					boathd.MoveYResistance = (float)boat_J_.Value;
					boathd.MoveZResistance = (float)boat_K_.Value;
					boathd.TurnXResistance = (float)boat_L_.Value;
					boathd.TurnYResistance = (float)boat_M_.Value;
					boathd.TurnZResistance = (float)boat_N_.Value;
					boathd.ViewCameraTopPosition = (float)boat_O_.Value;
					}
				#endregion

				#region Мотоциклы
				BikeHandlingDescriptor bikehd = hp.GetBikeHD ((uint)lastSelectedIndex);
				if (bikehd != null)
					{
					bikehd.ForwardLeaningCoM = (float)bike_B_.Value;
					bikehd.ForwardLeaningForce = (float)bike_C_.Value;
					bikehd.BackwardLeaningCoM = (float)bike_D_.Value;
					bikehd.BackwardLeaningForce = (float)bike_E_.Value;
					bikehd.MaxLeaningAngle = (float)bike_F_.Value;
					bikehd.MaxDriverLeaningAngle = (float)bike_G_.Value;
					bikehd.MaxDecelerationLeaningAngle = (float)bike_H_.Value;
					bikehd.SteeringOnSpeed = (float)bike_I_.Value;
					bikehd.CoMWithoutDriver = (float)bike_J_.Value;
					bikehd.SteeringOnSlippery = (float)bike_K_.Value;
					bikehd.StoppieAngle = (float)bike_L_.Value;
					bikehd.WheelieAngle = (float)bike_M_.Value;
					bikehd.WheelieStabilization = (float)bike_N_.Value;
					bikehd.SteeringOnWheelie = (float)bike_O_.Value;
					bikehd.StoppieStabilization = (float)bike_P_.Value;
					}
				#endregion

				#region Летательные аппараты
				FlyingHandlingDescriptor fhd = hp.GetFlyingHD ((uint)lastSelectedIndex);
				if (fhd != null)
					{
					fhd.NonControlledAcceleration = (float)flying_B_.Value;
					fhd.ControlledAcceleration = (float)flying_C_.Value;
					fhd.TurningLeftRightForce = (float)flying_D_.Value;
					fhd.TurningLeftRightStabilization = (float)flying_E_.Value;
					fhd.MovingAltitudeLoss = (float)flying_F_.Value;
					fhd.RotationForce = (float)flying_G_.Value;
					fhd.RotationStabilization = (float)flying_H_.Value;
					fhd.NoseDeflectionForce = (float)flying_I_.Value;
					fhd.NoseDeflectionStabilization = (float)flying_J_.Value;
					fhd.LiftingSpeedMultiplier = (float)flying_K_.Value;
					fhd.NoseAngleLiftingFactor = (float)flying_L_.Value;
					fhd.MovingResistance = (float)flying_M_.Value;
					fhd.TurningXResistance = (float)flying_N_.Value;
					fhd.TurningYResistance = (float)flying_O_.Value;
					fhd.TurningZResistance = (float)flying_P_.Value;
					fhd.AccelerationXResistance = (float)flying_Q_.Value;
					fhd.AccelerationYResistance = (float)flying_R_.Value;
					fhd.AccelerationZResistance = (float)flying_S_.Value;
					}
				#endregion
				}

			#region Установка транспорт-зависимых ограничений
			if (hp.GetGenericHD ((uint)A_.SelectedIndex).VehicleType != GenericHandlingDescriptor.VehicleTypes.Boat)
				{
				J_.Minimum = (decimal)GenericHandlingDescriptor.TractionMultiplier_Min;
				J_.Maximum = (decimal)GenericHandlingDescriptor.TractionMultiplier_Max;
				J_Label.Text = "Сцепление колёс с землёй:";
				K_.Minimum = (decimal)GenericHandlingDescriptor.TractionLoss_Min;
				K_.Maximum = (decimal)GenericHandlingDescriptor.TractionLoss_Max;
				K_Label.Text = "Сцепление при торможении:";
				L_.Minimum = (decimal)GenericHandlingDescriptor.TractionBias_Min;
				L_.Maximum = (decimal)GenericHandlingDescriptor.TractionBias_Max;
				L_Label.Text = "Смещение точки сцепления:";
				R_.Minimum = (decimal)GenericHandlingDescriptor.BrakeDeceleration_Min;
				R_.Maximum = (decimal)GenericHandlingDescriptor.BrakeDeceleration_Max;
				R_Label.Text = "Сила торможения:";
				S_.Minimum = (decimal)GenericHandlingDescriptor.BrakeBias_Min;
				S_.Maximum = (decimal)GenericHandlingDescriptor.BrakeBias_Max;
				S_Label.Text = "Смещение тормозного усилия:";
				V_.Minimum = (decimal)GenericHandlingDescriptor.SuspensionForceLevel_Min;
				V_.Maximum = (decimal)GenericHandlingDescriptor.SuspensionForceLevel_Max;
				V_Label.Text = "Сила прыжков на кочках:";
				W_.Minimum = (decimal)GenericHandlingDescriptor.SuspensionDampingLevel_Min;
				W_.Maximum = (decimal)GenericHandlingDescriptor.SuspensionDampingLevel_Max;
				W_Label.Text = "Стартовый/тормозной наклон:";
				AB_.Minimum = (decimal)GenericHandlingDescriptor.SuspensionLowerLimit_Min;
				AB_.Maximum = (decimal)GenericHandlingDescriptor.SuspensionLowerLimit_Max;
				AB_Label.Text = "минимум:";
				AC_.Minimum = (decimal)GenericHandlingDescriptor.SuspensionBias_Min;
				AC_.Maximum = (decimal)GenericHandlingDescriptor.SuspensionBias_Max;
				AC_Label.Text = "Распределение тормозного усилия между подвесками:";
				}
			else
				{
				J_.Minimum = (decimal)GenericHandlingDescriptor.BankForceMultiplier_Min;
				J_.Maximum = (decimal)GenericHandlingDescriptor.BankForceMultiplier_Max;
				J_Label.Text = "Наклон при повороте:";
				K_.Minimum = (decimal)GenericHandlingDescriptor.RudderTurnForce_Min;
				K_.Maximum = (decimal)GenericHandlingDescriptor.RudderTurnForce_Max;
				K_Label.Text = "Сила поворота руля:";
				L_.Minimum = (decimal)GenericHandlingDescriptor.SpeedSteerFalloff_Min;
				L_.Maximum = (decimal)GenericHandlingDescriptor.SpeedSteerFalloff_Max;
				L_Label.Text = "Потеря скорости на поворотах:";
				R_.Minimum = (decimal)GenericHandlingDescriptor.VerticalWaveHitLimit_Min;
				R_.Maximum = (decimal)GenericHandlingDescriptor.VerticalWaveHitLimit_Max;
				R_Label.Text = "Сила удара волны:";
				S_.Minimum = (decimal)GenericHandlingDescriptor.ForwardWaveHitBrake_Min;
				S_.Maximum = (decimal)GenericHandlingDescriptor.ForwardWaveHitBrake_Max;
				S_Label.Text = "Тормозящая сила волны:";
				V_.Minimum = (decimal)GenericHandlingDescriptor.WaterResistanceMultiplier_Min;
				V_.Maximum = (decimal)GenericHandlingDescriptor.WaterResistanceMultiplier_Max;
				V_Label.Text = "Сопротивление воды:";
				W_.Minimum = (decimal)GenericHandlingDescriptor.WaterDampingMultiplier_Min;
				W_.Maximum = (decimal)GenericHandlingDescriptor.WaterDampingMultiplier_Max;
				W_Label.Text = "Амортизация воды:";
				AB_.Minimum = (decimal)GenericHandlingDescriptor.HandbrakeDragMultiplier_Min;
				AB_.Maximum = (decimal)GenericHandlingDescriptor.HandbrakeDragMultiplier_Max;
				AB_Label.Text = "Сила торможения:";
				AC_.Minimum = (decimal)GenericHandlingDescriptor.SideslipForce_Min;
				AC_.Maximum = (decimal)GenericHandlingDescriptor.SideslipForce_Max;
				AC_Label.Text = "Сила бокового смещения:";
				}
			#endregion

			// Загрузка значений
			lastSelectedIndex = A_.SelectedIndex;
			GenericHandlingDescriptor ghd2 = hp.GetGenericHD ((uint)lastSelectedIndex);

			#region Основные дескрипторы
			A_InFile.Text = ghd2.VehicleIdentifier;
			B_.Value = (decimal)ghd2.VehicleMass;
			C_.Value = (decimal)ghd2.XDimension;
			D_.Value = (decimal)ghd2.YDimension;
			E_.Value = (decimal)ghd2.ZDimension;
			F_.Value = (decimal)ghd2.XCentreOfMass;
			G_.Value = (decimal)ghd2.YCentreOfMass;
			H_.Value = (decimal)ghd2.ZCentreOfMass;
			I_.Value = (decimal)ghd2.PercentSubmerged;
			J_.Value = (decimal)ghd2.TractionMultiplier;	// Одно и то же значение для лодок и не-лодок
			K_.Value = (decimal)ghd2.TractionLoss;			// Одно и то же значение для лодок и не-лодок
			L_.Value = (decimal)ghd2.TractionBias;			// Одно и то же значение для лодок и не-лодок
			M_.Value = (decimal)ghd2.NumberOfGears;
			N_.Value = (decimal)ghd2.MaxVelocity;
			O_.Value = (decimal)ghd2.EngineAcceleration;

			switch (ghd2.DriveType)
				{
				case GenericHandlingDescriptor.DriveTypes.FourWheelsDrive:
					P_.SelectedIndex = 0;
					break;

				case GenericHandlingDescriptor.DriveTypes.ForwardWheelsDrive:
					P_.SelectedIndex = 1;
					break;

				case GenericHandlingDescriptor.DriveTypes.RearWheelsDrive:
					P_.SelectedIndex = 2;
					break;
				}
			switch (ghd2.EngineType)
				{
				case GenericHandlingDescriptor.EngineTypes.Petrol:
					Q_.SelectedIndex = 0;
					break;

				case GenericHandlingDescriptor.EngineTypes.Diesel:
					Q_.SelectedIndex = 1;
					break;

				case GenericHandlingDescriptor.EngineTypes.Electric:
					Q_.SelectedIndex = 2;
					break;
				}

			R_.Value = (decimal)ghd2.BrakeDeceleration;			// Одно и то же значение для лодок и не-лодок
			S_.Value = (decimal)ghd2.BrakeBias;					// Одно и то же значение для лодок и не-лодок
			T_.Value = (decimal)ghd2.ABS;
			U_.Value = (decimal)ghd2.SteeringLock;
			V_.Value = (decimal)ghd2.SuspensionForceLevel;		// Одно и то же значение для лодок и не-лодок
			W_.Value = (decimal)ghd2.SuspensionDampingLevel;	// Одно и то же значение для лодок и не-лодок
			X_.Value = (decimal)ghd2.SeatOffsetDistance;
			Y_.Value = (decimal)ghd2.CollisionDamageMultiplier;
			Z_.Value = (decimal)ghd2.MonetaryValue;
			AA_.Value = (decimal)ghd2.SuspensionUpperLimit;
			AB_.Value = (decimal)ghd2.SuspensionLowerLimit;		// Одно и то же значение для лодок и не-лодок
			AC_.Value = (decimal)ghd2.SuspensionBias;			// Одно и то же значение для лодок и не-лодок
			AD_.Value = (decimal)ghd2.SuspensionAntiDiveMultiplier;

			switch (ghd2.FrontLights)
				{
				case GenericHandlingDescriptor.LightsTypes.Long:
					AF_.SelectedIndex = 0;
					break;

				case GenericHandlingDescriptor.LightsTypes.Small:
					AF_.SelectedIndex = 1;
					break;

				case GenericHandlingDescriptor.LightsTypes.Big:
					AF_.SelectedIndex = 2;
					break;

				case GenericHandlingDescriptor.LightsTypes.High:
					AF_.SelectedIndex = 3;
					break;
				}
			switch (ghd2.RearLights)
				{
				case GenericHandlingDescriptor.LightsTypes.Long:
					AG_.SelectedIndex = 0;
					break;

				case GenericHandlingDescriptor.LightsTypes.Small:
					AG_.SelectedIndex = 1;
					break;

				case GenericHandlingDescriptor.LightsTypes.Big:
					AG_.SelectedIndex = 2;
					break;

				case GenericHandlingDescriptor.LightsTypes.High:
					AG_.SelectedIndex = 3;
					break;
				}
			#endregion

			#region Флаги
			AE_1_1.Checked = ghd2.FirstGearBoost;
			AE_1_2.Checked = ghd2.SecondGearBoost;
			AE_1_3.Checked = ghd2.ReversedBonnet;

			switch (ghd2.TailgateType)
				{
				case GenericHandlingDescriptor.TailgateTypes.DefaultBoot:
					AE_1_4.SelectedIndex = 0;
					break;

				case GenericHandlingDescriptor.TailgateTypes.AttachedToTop:
					AE_1_4.SelectedIndex = 1;
					break;

				case GenericHandlingDescriptor.TailgateTypes.AttachedToBottom:
					AE_1_4.SelectedIndex = 2;
					break;

				case GenericHandlingDescriptor.TailgateTypes.Locked:
					AE_1_4.SelectedIndex = 3;
					break;
				}
			switch (ghd2.DoorsType)
				{
				case GenericHandlingDescriptor.DoorsTypes.Default:
					AE_2.SelectedIndex = 0;
					break;

				case GenericHandlingDescriptor.DoorsTypes.NoDoors:
					AE_2.SelectedIndex = 1;
					break;

				case GenericHandlingDescriptor.DoorsTypes.IsVan:
					AE_2.SelectedIndex = 2;
					break;

				case GenericHandlingDescriptor.DoorsTypes.IsBus:
					AE_2.SelectedIndex = 3;
					break;

				case GenericHandlingDescriptor.DoorsTypes.IsLow:
					AE_2.SelectedIndex = 4;
					break;
				}

			AE_3_1.Checked = ghd2.DoubleExhaust;
			AE_3_4.Checked = ghd2.NonPlayerStabilizer;
			AE_4_1.Checked = ghd2.NeutralHandling;
			AE_4_2.Checked = ghd2.HasNoRoof;
			AE_4_3.Checked = ghd2.TransportIsBig;
			AE_4_4.Checked = ghd2.HalogenLights;

			switch (ghd2.VehicleType)
				{
				case GenericHandlingDescriptor.VehicleTypes.Car:
					AE_5.SelectedIndex = 0;
					break;

				case GenericHandlingDescriptor.VehicleTypes.Bike:
					AE_5.SelectedIndex = 1;
					break;

				case GenericHandlingDescriptor.VehicleTypes.Helicopter:
					AE_5.SelectedIndex = 2;
					break;

				case GenericHandlingDescriptor.VehicleTypes.Plane:
					AE_5.SelectedIndex = 3;
					break;

				case GenericHandlingDescriptor.VehicleTypes.Boat:
					AE_5.SelectedIndex = 4;
					break;
				}

			AE_6_1.Checked = ghd2.NoExhaust;
			AE_6_2.Checked = ghd2.RearWheelFirst;
			AE_6_3.Checked = ghd2.HandbrakeTyre;
			AE_6_4.Checked = ghd2.SitInBoat;
			AE_7_1.Checked = ghd2.FatRearWheels;
			AE_7_2.Checked = ghd2.NarrowFrontWheels;
			AE_7_3.Checked = ghd2.GoodInSand;
			AE_7_4.Checked = ghd2.SpecialFlight;
			#endregion

			BoatHandlingDescriptor boathd2 = hp.GetBoatHD ((uint)lastSelectedIndex);

			#region Моторные лодки
			if (boathd2 != null)
				{
				SetBoatState (true);
				boat_B_.Value = (decimal)boathd2.ForwardThrust;
				boat_C_.Value = (decimal)boathd2.UpwardThrust;
				boat_D_.Value = (decimal)boathd2.ThrustAppZ;
				boat_E_.Value = (decimal)boathd2.AquaPlaneForce;
				boat_F_.Value = (decimal)boathd2.AquaPlaneLimit;
				boat_G_.Value = (decimal)boathd2.AquaPlaneOffset;
				boat_H_.Value = (decimal)boathd2.WavesLoudness;
				boat_I_.Value = (decimal)boathd2.MoveXResistance;
				boat_J_.Value = (decimal)boathd2.MoveYResistance;
				boat_K_.Value = (decimal)boathd2.MoveZResistance;
				boat_L_.Value = (decimal)boathd2.TurnXResistance;
				boat_M_.Value = (decimal)boathd2.TurnYResistance;
				boat_N_.Value = (decimal)boathd2.TurnZResistance;
				boat_O_.Value = (decimal)boathd2.ViewCameraTopPosition;
				}
			else
				{
				SetBoatState (false);
				}
			#endregion

			BikeHandlingDescriptor bikehd2 = hp.GetBikeHD ((uint)lastSelectedIndex);

			#region Мотоциклы
			if (bikehd2 != null)
				{
				SetBikeState (true);
				bike_B_.Value = (decimal)bikehd2.ForwardLeaningCoM;
				bike_C_.Value = (decimal)bikehd2.ForwardLeaningForce;
				bike_D_.Value = (decimal)bikehd2.BackwardLeaningCoM;
				bike_E_.Value = (decimal)bikehd2.BackwardLeaningForce;
				bike_F_.Value = (decimal)bikehd2.MaxLeaningAngle;
				bike_G_.Value = (decimal)bikehd2.MaxDriverLeaningAngle;
				bike_H_.Value = (decimal)bikehd2.MaxDecelerationLeaningAngle;
				bike_I_.Value = (decimal)bikehd2.SteeringOnSpeed;
				bike_J_.Value = (decimal)bikehd2.CoMWithoutDriver;
				bike_K_.Value = (decimal)bikehd2.SteeringOnSlippery;
				bike_L_.Value = (decimal)bikehd2.StoppieAngle;
				bike_M_.Value = (decimal)bikehd2.WheelieAngle;
				bike_N_.Value = (decimal)bikehd2.WheelieStabilization;
				bike_O_.Value = (decimal)bikehd2.SteeringOnWheelie;
				bike_P_.Value = (decimal)bikehd2.StoppieStabilization;
				}
			else
				{
				SetBikeState (false);
				}
			#endregion

			FlyingHandlingDescriptor fhd2 = hp.GetFlyingHD ((uint)lastSelectedIndex);

			#region Летательные аппараты
			if (fhd2 != null)
				{
				SetFlyingState (true);
				flying_B_.Value = (decimal)fhd2.NonControlledAcceleration;
				flying_C_.Value = (decimal)fhd2.ControlledAcceleration;
				flying_D_.Value = (decimal)fhd2.TurningLeftRightForce;
				flying_E_.Value = (decimal)fhd2.TurningLeftRightStabilization;
				flying_F_.Value = (decimal)fhd2.MovingAltitudeLoss;
				flying_G_.Value = (decimal)fhd2.RotationForce;
				flying_H_.Value = (decimal)fhd2.RotationStabilization;
				flying_I_.Value = (decimal)fhd2.NoseDeflectionForce;
				flying_J_.Value = (decimal)fhd2.NoseDeflectionStabilization;
				flying_K_.Value = (decimal)fhd2.LiftingSpeedMultiplier;
				flying_L_.Value = (decimal)fhd2.NoseAngleLiftingFactor;
				flying_M_.Value = (decimal)fhd2.MovingResistance;
				flying_N_.Value = (decimal)fhd2.TurningXResistance;
				flying_O_.Value = (decimal)fhd2.TurningYResistance;
				flying_P_.Value = (decimal)fhd2.TurningZResistance;
				flying_Q_.Value = (decimal)fhd2.AccelerationXResistance;
				flying_R_.Value = (decimal)fhd2.AccelerationYResistance;
				flying_S_.Value = (decimal)fhd2.AccelerationZResistance;
				}
			else
				{
				SetFlyingState (false);
				}
			#endregion
			}

		// Сохранение файла
		private void SaveCfgFile_Click (object sender, System.EventArgs e)
			{
			// Принудительное сохранение значений
			A__SelectedIndexChanged (null, null);

			// Вызов диалога
			SaveCfgDialog.ShowDialog ();
			}

		private void SaveCfgDialog_FileOk (object sender, System.ComponentModel.CancelEventArgs e)
			{
			if (!hp.SaveHandlingData (SaveCfgDialog.FileName, AddComments.Checked))
				{
				MessageBox.Show ("Не удалось сохранить файл. Убедитесь, что файл не занят другой программой или " +
					"попробуйте задать ему другое местоположение", ProgramDescription.AssemblyTitle,
					 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}
	}
