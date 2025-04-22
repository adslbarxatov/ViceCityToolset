using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Главная форма приложения
	/// </summary>
	public partial class BExplorerForm: Form
		{
		// Флаг состояния загрузки данных; блокирует события обновления значений
		private bool loading = false;

		// Код ошибки
		private int error;

		// Экземпляр-загрузчик цветов авто
		private CarColors cc;

		// Экземпляр-обработчик списка парковок
		private CarGenerators cg;

		// Экземпляр-обработчик списка денежных накопителей
		private Pickups pu;

		// Форма выбора координат на карте
		private CoordsPicker cp;

		// Экземпляр-обработчик ToDo-статуса
		private ToDoStatus tds;

		// Расширение файлов сохранений
		private const string savesExtension = ".b";

		// Стандартный таймаут для сообщений
		private const uint defaultTimeout = 1500;

		/// <summary>
		/// Метод инициализирует форму редактирования файлов сохранений
		/// </summary>
		public BExplorerForm ()
			{
			// Инициализация формы
			InitializeComponent ();

			Application.CurrentCulture = RDLocale.GetCulture (RDLanguages.en_us);
			this.Text = ProgramDescription.AssemblyTitle + " – " + RDLocale.GetText (this.Name);

			// Запуск
			this.ShowDialog ();
			}

		// Запуск формы
		private void MainForm_Load (object sender, EventArgs e)
			{
			// Контроль корректности связи с библиотекой функций
			if (BExplorerLib.Check () != 0)
				{
				RDInterface.MessageBox (RDMessageTypes.Error_Center,
					string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.MessageFormat_WrongVersion_Fmt),
					ProgramDescription.AssemblyLibName));

				error = -1;
				this.Close ();
				return;
				}

			// Проверка наличия файла цветовой схемы
			cc = new CarColors (out error);
			if (error != 0)
				{
				this.Close ();
				return;
				}

			// Загрузка ограничений и списков
			loading = true;

			DP_Date.MaxDate = new DateTime ((int)BExplorerLib.SaveData_GetParameterLimit (OpCodes.SaveYear,
				0, true),
				(int)BExplorerLib.SaveData_GetParameterLimit (OpCodes.SaveMonth, 0, true),
				(int)BExplorerLib.SaveData_GetParameterLimit (OpCodes.SaveDay, 0, true));
			DP_Date.MinDate = new DateTime ((int)BExplorerLib.SaveData_GetParameterLimit (OpCodes.SaveYear,
				0, false),
				(int)BExplorerLib.SaveData_GetParameterLimit (OpCodes.SaveMonth, 0, false),
				(int)BExplorerLib.SaveData_GetParameterLimit (OpCodes.SaveDay, 0, false));

			DP_IML.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.InGameMinuteLength,
				0, true);
			DP_IML.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.InGameMinuteLength,
				0, false);

			DP_GameSpeed.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.GameSpeed,
				0, true);
			DP_GameSpeed.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.GameSpeed,
				0, false);

			PL_MaxHealth.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.MaxHealth,
				0, true);
			PL_MaxHealth.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.MaxHealth,
				0, false);

			PL_MaxArmor.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.MaxArmor,
				0, true);
			PL_MaxArmor.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.MaxArmor,
				0, false);

			PL_CurArmor.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.CurrentArmor,
				0, true);
			PL_CurArmor.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.CurrentArmor,
				0, false);

			for (int i = 0; i <= 6; i++)
				PL_MWL.Items.Add (i.ToString ());

			PL_CurMoney.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.CurrentMoney,
				0, true);
			PL_CurMoney.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.CurrentMoney,
				0, false);

			PL_Bullets.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.PlayerWeapons_Base,
				(UInt16)WeaponsParCodes.WeaponAmmo, true);
			PL_Bullets.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.PlayerWeapons_Base,
				(UInt16)WeaponsParCodes.WeaponAmmo, false);

			GR_CarModel.DataSource = ParametersValues.CarsForGarages;
			GR_CarModel.DisplayMember = GR_CarModel.ValueMember = "Name";

			GR_CarColor1.Maximum = cc.Colors.Count - 1;
			GR_CarColor2.Maximum = cc.Colors.Count - 1;

			GR_Radio.DataSource = ParametersValues.Radios;
			GR_Radio.DisplayMember = GR_Radio.ValueMember = "Name";

			PU_AssetValue.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Pickups_Base,
				(UInt16)PickupsParCodes.ObjectAsset, true);
			PU_AssetValue.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Pickups_Base,
				(UInt16)PickupsParCodes.ObjectAsset, false);

			GD_CarModel.DataSource = ParametersValues.CarsForGangs;
			GD_CarModel.DisplayMember = GD_CarModel.ValueMember = "Name";

			GD_PedModel1.Maximum = GD_PedModel2.Maximum =
				(decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Gangs_Base,
				(UInt16)GangsParCodes.PrimaryPedModel, true);
			GD_PedModel1.Minimum = GD_PedModel2.Minimum =
				(decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Gangs_Base,
				(UInt16)GangsParCodes.PrimaryPedModel, false);

			CarGenList.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.ActiveGenerators,
				0, true);
			CarGenList.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.ActiveGenerators,
				0, false);

			CG_CarModel.DataSource = ParametersValues.CarsForCG;
			CG_CarModel.DisplayMember = CG_CarModel.ValueMember = "Name";

			CG_X.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarX, true);
			CG_X.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarX, false);

			CG_Y.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarY, true);
			CG_Y.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarY, false);

			CG_Z.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarZ, true);
			CG_Z.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarZ, false);

			CG_Rotation.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarAngle, true);
			CG_Rotation.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.CarAngle, false);

			CG_CarColor1.Maximum = cc.Colors.Count - 1;
			CG_CarColor1.Minimum = -1;
			CG_CarColor2.Maximum = cc.Colors.Count - 1;
			CG_CarColor2.Minimum = -1;

			CG_AlarmProb.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.AlarmProbability, true);
			CG_AlarmProb.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.AlarmProbability, false);

			CG_LockProb.Maximum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.LockProbability, true);
			CG_LockProb.Minimum = (decimal)BExplorerLib.SaveData_GetParameterLimit (OpCodes.Generators_Base,
				(UInt16)GeneratorsParCodes.LockProbability, false);

			cp = new CoordsPicker (CG_X.Minimum, CG_X.Maximum, CG_Y.Minimum, CG_Y.Maximum, CG_Z.Minimum, CG_Z.Maximum,
				CG_Rotation.Minimum, CG_Rotation.Maximum);

			// Окончательная настройка
			SetState (false);   // Начальная блокировка

			LocalizeForm ();

			loading = false;
			RDGenerics.LoadWindowDimensions (this);
			}

		// Выбор файла сохранения для загрузки
		private void OpenFile_Click (object sender, EventArgs e)
			{
			OFDialog.FileName = "";
			OFDialog.ShowDialog ();
			}

		private void DefaultFileButton_Click (object sender, EventArgs e)
			{
			// Пробуем открыть файл из стандартного расположения
			OFDialog.FileName = ViceCityToolsetProgram.GTAVCSavesDirectory + "\\GTAVCsf" +
				((uint)OpenFileNumber.Value).ToString () + savesExtension;

			OFDialog_FileOk (null, null);
			}

		// Файл выбран
		private void OFDialog_FileOk (object sender, CancelEventArgs e)
			{
			// Загрузка файла сохранения
			ResultCodes errCode;

			// В случае ошибки вывести сообщение и прекратить исполнение программы
			SetState (false);   // Блокировать заранее
			if ((errCode = BExplorerLib.SaveData_Load (OFDialog.FileName)) != ResultCodes.LoadSuccess)
				{
				string errText = RDLocale.GetText ("SaveLoadingError");
				if ((errCode >= ResultCodes.ErrorLoadDP) && (errCode <= ResultCodes.ErrorLoadCS))
					errText += RDLocale.GetText ("Result_LoadingErrorPrefix");
				errText += RDLocale.GetText ("Result_" + errCode.ToString ());

				RDInterface.MessageBox (RDMessageTypes.Warning_Center, errText);
				SaveInfoLabel.Text = RDLocale.GetText ("SaveNotSpecified");

				return;
				}

			// В случае успеха
			SaveInfoLabel.Text = RDLocale.GetText ("CurrentSave") + BExplorerLib.SaveData_SaveInfo;
			SetState (true);

			// Загрузка начальных параметров в поля формы
			LoadParameters ();
			}

		// Метод отвечает за получение одного параметра из структуры
		private static bool GetParameterValue (OpCodes OpCode, uint ParCode, out string Result)
			{
			Result = BExplorerLib.SaveData_GetParameterValue (OpCode, ParCode);
			if (Result.StartsWith ('\x13'))
				{
				ResultCodes errCode = (ResultCodes)int.Parse (Result.Substring (1));
				RDInterface.MessageBox (RDMessageTypes.Warning_Center,
					RDLocale.GetText ("Result_" + errCode.ToString ()));
				return false;
				}

			return true;
			}

		// Метод отвечает за установку нового значения параметра
		private static void SetParameterValue (OpCodes OpCode, uint ParCode, string NewValue)
			{
			ResultCodes result = BExplorerLib.SaveData_SetParameterValue (OpCode, ParCode, NewValue);

			if (result != ResultCodes.OK)
				RDInterface.MessageBox (RDMessageTypes.Warning_Center,
					RDLocale.GetText ("Result_" + result.ToString ()));
			}

		// Загрузчик параметров
		private void LoadParameters ()
			{
			string s1, s2, s3;

			// Переход в режим загрузки
			loading = true;

			// Получение даты
			if (!GetParameterValue (OpCodes.SaveYear, 0, out s1) ||
				!GetParameterValue (OpCodes.SaveMonth, 0, out s2) ||
				!GetParameterValue (OpCodes.SaveDay, 0, out s3))
				{
				loading = false;
				return;
				}

			DP_Date.Value = new DateTime ((int)decimal.Parse (s1), (int)decimal.Parse (s2),
				(int)decimal.Parse (s3));

			// Получение времени
			if (!GetParameterValue (OpCodes.SaveHour, 0, out s1) ||
				!GetParameterValue (OpCodes.SaveMinute, 0, out s2) ||
				!GetParameterValue (OpCodes.SaveSecond, 0, out s3))
				{
				loading = false;
				return;
				}

			DP_Time.Value = new DateTime (2000, 1, 1, (int)decimal.Parse (s1), (int)decimal.Parse (s2),
				(int)decimal.Parse (s3));

			// Получение длины минуты
			if (!GetParameterValue (OpCodes.InGameMinuteLength, 0, out s1))
				{
				loading = false;
				return;
				}

			DP_IML.Value = decimal.Parse (s1);

			// Получение времени внутри игры
			if (!GetParameterValue (OpCodes.InGameHour, 0, out s1) ||
				!GetParameterValue (OpCodes.InGameMinute, 0, out s2))
				{
				loading = false;
				return;
				}

			DP_GameTime.Value = new DateTime (2000, 1, 1, (decimal.Parse (s1) > 23) ? 23 : (int)decimal.Parse (s1),
				(decimal.Parse (s2) > 59) ? 59 : (int)decimal.Parse (s2), 0);

			// Получение скорости игры
			if (!GetParameterValue (OpCodes.GameSpeed, 0, out s1))
				{
				loading = false;
				return;
				}

			DP_GameSpeed.Value = decimal.Parse (s1);

			// Получение погоды
			if (!GetParameterValue (OpCodes.CurrentWeather, 0, out s1))
				{
				loading = false;
				return;
				}

			decimal d = decimal.Parse (s1);
			for (int i = 0; i < ParametersValues.Weathers.Length; i++)
				{
				if (d == ParametersValues.Weathers[i].ID)
					{
					DP_Weather.SelectedIndex = i;
					break;
					}
				}

			// Получение положения камеры
			if (!GetParameterValue (OpCodes.CarOverview, 0, out s1))
				{
				loading = false;
				return;
				}

			float f = float.Parse (s1);
			for (int i = 0; i < ParametersValues.CameraPositions.Length; i++)
				{
				if (f == (float)ParametersValues.CameraPositions[i].ID)
					{
					DP_CameraPos.SelectedIndex = i;
					break;
					}
				}

			// Получение состояния радио Cauffman cabs
			if (!GetParameterValue (OpCodes.CabsRadio, 0, out s1))
				{
				loading = false;
				return;
				}

			SBB_CabsRadio.Checked = (decimal.Parse (s1) != 0);

			// Получение величин здоровья и брони
			if (!GetParameterValue (OpCodes.MaxHealth, 0, out s1) ||
				!GetParameterValue (OpCodes.MaxArmor, 0, out s2) ||
				!GetParameterValue (OpCodes.CurrentArmor, 0, out s3))
				{
				loading = false;
				return;
				}

			PL_MaxHealth.Value = (decimal.Parse (s1) > PL_MaxHealth.Maximum) ? PL_MaxHealth.Maximum : decimal.Parse (s1);
			PL_MaxArmor.Value = (decimal.Parse (s2) > PL_MaxArmor.Maximum) ? PL_MaxArmor.Maximum : decimal.Parse (s2);
			PL_CurArmor.Value = decimal.Parse (s3);

			// Получение максимума звёзд розыска
			if (!GetParameterValue (OpCodes.MaxPoliceStars, 0, out s1))
				{
				loading = false;
				return;
				}

			PL_MWL.SelectedIndex = (int)decimal.Parse (s1);

			// Получение наличных
			if (!GetParameterValue (OpCodes.CurrentMoney, 0, out s1))
				{
				loading = false;
				return;
				}

			PL_CurMoney.Value = ((decimal.Parse (s1) > PL_CurMoney.Maximum) ? PL_CurMoney.Maximum :
				decimal.Parse (s1));

			// Получение костюма
			if (!GetParameterValue (OpCodes.PlayerSuit, 0, out s1))
				{
				loading = false;
				return;
				}

			for (int i = 0; i < ParametersValues.Suits.Length; i++)
				{
				if (s1 == ParametersValues.Suits[i].CodeName)
					{
					PL_Suit.SelectedIndex = i;
					break;
					}
				}

			// Получение флагов несгораемости, вечного бега и быстрой перезарядки
			if (!GetParameterValue (OpCodes.InfiniteRun, 0, out s1) ||
				!GetParameterValue (OpCodes.FastReload, 0, out s2) ||
				!GetParameterValue (OpCodes.Fireproof, 0, out s3))
				{
				loading = false;
				return;
				}

			PL_InfRun.Checked = (decimal.Parse (s1) != 0);
			PL_FastReload.Checked = (decimal.Parse (s2) != 0);
			PL_Fireproof.Checked = (decimal.Parse (s3) != 0);

			// Получение флага бесконечных патронов
			if (!GetParameterValue (OpCodes.InfiniteAmmo, 0, out s1))
				{
				loading = false;
				return;
				}

			ST_InfBullets.Checked = (decimal.Parse (s1) != 0);

			// Завершение загрузки
			loading = false;

			// Выбор слота оружия (вызывает событие запроса имени оружия и числа патронов)
			WeaponsList.SelectedIndex = 0;
			WeaponsList_SelectedIndexChanged (null, null);

			// Выбор слота гаража (вызывает событие запроса параметров гаража)
			GaragesList.SelectedIndex = 0;
			GaragesList_SelectedIndexChanged (null, null);

			// Выбор слота накопителя (вызывает событие запроса параметров накопителя)
			pu = new Pickups ();
			AssetList.Items.Clear ();
			for (int i = 0; i < pu.ActiveAssetsCount; i++)
				AssetList.Items.Add (RDLocale.GetText ("AssetNumber") + (i + 1).ToString ());

			if (pu.ActiveAssetsCount > 0)
				{
				AssetList.Text = AssetList.Items[0].ToString ();
				AssetList.Enabled = PU_AssetValue.Enabled = PickupViewCoords.Enabled = true;
				}
			else
				{
				AssetList.Enabled = PU_AssetValue.Enabled = PickupViewCoords.Enabled = false;
				}
			AssetList_SelectedIndexChanged (null, null);

			// Выбор слота банды (вызывает событие запроса параметров банды)
			GangsList.SelectedIndex = 0;
			GangsList_SelectedIndexChanged (null, null);

			// Загрузка списка парковок и выбор слота
			cg = new CarGenerators ();
			CGCountLabel.Text = RDLocale.GetText ("CGLoaded") + cg.ActiveGeneratorsCount.ToString ();
			CarGenList.Value = 1;
			CarGenList_ValueChanged (null, null);

			// Загрузка ToDo-статуса
			tds = new ToDoStatus ();
			ToDoStatusView.Items.Clear ();

			for (int i = 0; i < tds.Elements.Count; i++)
				{
				if (!tds.Elements[i].Completed)
					ToDoStatusView.Items.Add (tds.Elements[i].ElementDescription);
				}

			if (ToDoStatusView.Items.Count == 0)
				{
				ToDoStatusView.Items.Add (RDLocale.GetText ("SaveCompleted"));
				DangerousReset.Enabled = AbortSorting.Enabled = ST_InfBullets.Enabled = true;
				// Только при полном прохождении
				}
			}

		// Выбор места сохранения файла
		private void SaveFileButton_Click (object sender, EventArgs e)
			{
			// Запись данных генераторов авто
			cg.SaveGenerators (!AbortSorting.Checked);
			CGCountLabel.Text = RDLocale.GetText ("CGSaved") + cg.ActiveGeneratorsCount.ToString ();

			// Отображение диалога
			SFDialog.FileName = "";
			SFDialog.ShowDialog ();
			}

		private void UpdateDefaultButton_Click (object sender, EventArgs e)
			{
			// Запись данных генераторов авто
			cg.SaveGenerators (!AbortSorting.Checked);
			CGCountLabel.Text = RDLocale.GetText ("CGSaved") + cg.ActiveGeneratorsCount.ToString ();

			// Пробуем сохранить файл в стандартном расположении
			SFDialog.FileName = ViceCityToolsetProgram.GTAVCSavesDirectory + "\\GTAVCsf" +
				((uint)OpenFileNumber.Value).ToString () + savesExtension;

			SFDialog_FileOk (null, null);
			}

		// Место выбрано
		private void SFDialog_FileOk (object sender, CancelEventArgs e)
			{
			ResultCodes res = BExplorerLib.SaveData_SaveParametersFile (SaveableParameters.SaveFile, SFDialog.FileName);

			if (res == ResultCodes.SaveSuccess)
				{
				RDInterface.MessageBox (RDMessageTypes.Success_Center,
					RDLocale.GetText ("Result_" + res.ToString ()), defaultTimeout);
				LoadParameters ();
				}
			else
				{
				RDInterface.MessageBox (RDMessageTypes.Warning_Center, RDLocale.GetText ("SaveSavingError") +
					RDLocale.GetText ("Result_" + res.ToString ()));
				}
			}

		// Кнопка выхода
		private void ExitButton_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void MainForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			e.Cancel = (error == 0) && (RDInterface.LocalizedMessageBox (RDMessageTypes.Warning_Center,
				"ChangesSaved", RDLDefaultTexts.Button_Yes, RDLDefaultTexts.Button_No) ==
				RDMessageButtons.ButtonTwo);

			RDGenerics.SaveWindowDimensions (this);
			ParametersValues.ResetCache ();
			}

		// Установка всех настроечных контролов в состояние доступен/недоступен
		private void SetState (bool State)
			{
			SaveFileButton.Enabled = UpdateDefaultFileButton.Enabled =
				RecommendedSettings.Enabled = State;

			DPTab.Enabled = State;
			PLTab.Enabled = State;
			GRTab.Enabled = State;
			PUTab.Enabled = State;
			GDTab.Enabled = STTab.Enabled = State;
			CGTab.Enabled = State;

			// Для этих элементов в общем порядке допустима только блокировка
			if (State == false)
				AbortSorting.Enabled = ST_InfBullets.Enabled = DangerousReset.Enabled = State;
			}

		// Изменена дата
		private void DP_Date_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.SaveYear, 0, DP_Date.Value.Year.ToString ());
			SetParameterValue (OpCodes.SaveMonth, 0, DP_Date.Value.Month.ToString ());
			SetParameterValue (OpCodes.SaveDay, 0, DP_Date.Value.Day.ToString ());
			}

		// Изменено время
		private void DP_Time_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.SaveHour, 0, DP_Time.Value.Hour.ToString ());
			SetParameterValue (OpCodes.SaveMinute, 0, DP_Time.Value.Minute.ToString ());
			SetParameterValue (OpCodes.SaveSecond, 0, DP_Time.Value.Second.ToString ());
			}

		// Изменена длительность минуты в игре
		private void DP_IML_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.InGameMinuteLength, 0, DP_IML.Value.ToString ());
			}

		// Изменено время в игре
		private void DP_GameTime_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.InGameHour, 0, DP_GameTime.Value.Hour.ToString ());
			SetParameterValue (OpCodes.InGameMinute, 0, DP_GameTime.Value.Minute.ToString ());
			}

		// Изменена скорость игры
		private void DP_GameSpeed_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.GameSpeed, 0, DP_GameSpeed.Value.ToString ());
			}

		// Изменена текущая погода
		private void DP_Weather_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.CurrentWeather, 0,
				ParametersValues.Weathers[DP_Weather.SelectedIndex].ID.ToString ());
			}

		// Изменено положение камеры
		private void DP_CameraPos_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.CarOverview, 0,
				ParametersValues.CameraPositions[DP_CameraPos.SelectedIndex].ID.ToString ());
			}

		// Изменено состояние радио Caufman cabs
		private void SBB_CabsRadio_CheckedChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.CabsRadio, 0, SBB_CabsRadio.Checked ? "1" : "0");
			}

		// Изменена величина максимального здоровья
		private void PL_MaxHealth_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.MaxHealth, 0, PL_MaxHealth.Value.ToString ());
			}

		// Изменена величина максимальной брони
		private void PL_MaxArmor_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.MaxArmor, 0, PL_MaxArmor.Value.ToString ());
			}

		// Изменена величина текущей брони
		private void PL_CurArmor_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.CurrentArmor, 0, PL_CurArmor.Value.ToString ());
			}

		// Изменён максимум звёзд розыска
		private void PL_MWL_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.MaxPoliceStars, 0, PL_MWL.SelectedIndex.ToString ());
			}

		// Изменена сумма наличных
		private void PL_CurMoney_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.CurrentMoney, 0, PL_CurMoney.Value.ToString ());
			}

		// Изменён костюм
		private void PL_Suit_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.PlayerSuit, 0, ParametersValues.Suits[PL_Suit.SelectedIndex].CodeName);
			}

		// Изменён флаг вечного бега
		private void PL_InfRun_CheckedChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.InfiniteRun, 0, PL_InfRun.Checked ? "1" : "0");
			}

		// Изменён флаг быстрой перезарядки
		private void PL_FastReload_CheckedChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.FastReload, 0, PL_FastReload.Checked ? "1" : "0");
			}

		// Изменён флаг несгораемости
		private void PL_Fireproof_CheckedChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.Fireproof, 0, PL_Fireproof.Checked ? "1" : "0");
			}

		// Выбран слот оружия
		private void WeaponsList_SelectedIndexChanged (object sender, EventArgs e)
			{
			loading = true;

			string s1, s2;
			if (!GetParameterValue ((OpCodes)((int)OpCodes.PlayerWeapons_Base + WeaponsList.SelectedIndex),
				(UInt16)WeaponsParCodes.WeaponType, out s1) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.PlayerWeapons_Base + WeaponsList.SelectedIndex),
				(UInt16)WeaponsParCodes.WeaponAmmo, out s2))
				{
				loading = false;
				return;
				}

			decimal d = decimal.Parse (s1);
			for (int i = 0; i < ParametersValues.Weapons.Length; i++)
				{
				if (d == ParametersValues.Weapons[i].ID)
					{
					PL_Weapon.SelectedIndex = i;
					break;
					}
				}

			PL_Bullets.Value = (decimal.Parse (s2) > PL_Bullets.Maximum) ? PL_Bullets.Maximum : decimal.Parse (s2);

			loading = false;
			}

		// Изменён тип оружия
		private void PL_Weapon_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.PlayerWeapons_Base + WeaponsList.SelectedIndex),
				(UInt16)WeaponsParCodes.WeaponType,
				ParametersValues.Weapons[PL_Weapon.SelectedIndex].ID.ToString ());
			}

		// Изменено число патронов
		private void PL_Bullets_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.PlayerWeapons_Base + WeaponsList.SelectedIndex),
				(uint)WeaponsParCodes.WeaponAmmo, PL_Bullets.Value.ToString ());
			}

		// Загрузка статистики
		private void LoadStats_Click (object sender, EventArgs e)
			{
			OStatsDialog.FileName = "";
			OStatsDialog.ShowDialog ();
			}

		// Файл выбран
		private void OStatsDialog_FileOk (object sender, CancelEventArgs e)
			{
			// Загрузка файла статистики
			ResultCodes res = BExplorerLib.SaveData_LoadParametersFile (LoadableParameters.Stats, OStatsDialog.FileName);

			if (res == ResultCodes.LoadSuccess)
				RDInterface.MessageBox (RDMessageTypes.Success_Center,
					RDLocale.GetText ("Result_" + res.ToString ()), defaultTimeout);
			else
				RDInterface.MessageBox (RDMessageTypes.Warning_Center, RDLocale.GetText ("StatsLoadingError") +
					RDLocale.GetText ("Result_" + res.ToString ()));
			}

		// Выгрузка статистики
		private void SaveStats_Click (object sender, EventArgs e)
			{
			saveMode = SaveableParameters.Stats;

			SStatsDialog.FileName = saveMode.ToString ();
			SStatsDialog.ShowDialog ();
			}
		private SaveableParameters saveMode;

		// Файл выбран
		private void SStatsDialog_FileOk (object sender, CancelEventArgs e)
			{
			// Сохранение файла статистики
			ResultCodes res = BExplorerLib.SaveData_SaveParametersFile (saveMode, SStatsDialog.FileName);

			if (saveMode != SaveableParameters.SaveFile)
				{
				if (res != ResultCodes.SaveSuccess)
					{
					RDInterface.MessageBox (RDMessageTypes.Warning_Center, RDLocale.GetText ("StatsSavingError") +
						RDLocale.GetText ("Result_" + res.ToString ()));
					return;
					}

				switch (saveMode)
					{
					case SaveableParameters.Stats:
						res = BExplorerLib.SaveData_MergeStats (SStatsDialog.FileName);
						break;

					case SaveableParameters.Garages:
						res = SaveData_DescriptGarages (SStatsDialog.FileName);
						break;

					case SaveableParameters.Generators:
						res = SaveData_DescriptGenerators (SStatsDialog.FileName);
						break;
					}
				}

			if (res == ResultCodes.SaveSuccess)
				RDInterface.MessageBox (RDMessageTypes.Success_Center,
					RDLocale.GetText ("Result_" + res.ToString ()), defaultTimeout);
			else
				RDInterface.MessageBox (RDMessageTypes.Warning_Center, RDLocale.GetText ("StatsSavingError") +
					RDLocale.GetText ("Result_" + res.ToString ()));
			}

		// Выбран слот гаража
		private void GaragesList_SelectedIndexChanged (object sender, EventArgs e)
			{
			loading = true;

			string model, imm, c1, c2, radio, bomb;
			if (!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.CarModel, out model) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.Immunity, out imm) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.PrimaryColor, out c1) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.SecondaryColor, out c2) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.RadioStation, out radio) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.BombType, out bomb))
				{
				loading = false;
				return;
				}

			GR_CarModel.SelectedIndex = ParametersValues.FindCarIndex (ParametersValues.CarsListTypes.ForGarages,
				(int)decimal.Parse (model));

			GR_CarColor1.Value = (decimal.Parse (c1) > GR_CarColor1.Maximum) ?
				GR_CarColor1.Maximum : decimal.Parse (c1);
			GR_CarColor2.Value = (decimal.Parse (c2) > GR_CarColor1.Maximum) ?
				GR_CarColor2.Maximum : decimal.Parse (c2);

			GR_Radio.SelectedIndex = ParametersValues.FindRadioIndex ((int)decimal.Parse (radio));
			GR_Bomb.SelectedIndex = ParametersValues.FindBombIndex ((int)decimal.Parse (bomb));

			int d = (int)decimal.Parse (imm);
			GR_BulletsProof.Checked = ((d & 0x1) != 0);
			GR_FireProof.Checked = ((d & 0x2) != 0);
			GR_ExplProof.Checked = ((d & 0x4) != 0);
			GR_DamageProof.Checked = ((d & 0x8) != 0);

			loading = false;
			}

		// Метод разворачивает описание гаражей в табличный файл
		private ResultCodes SaveData_DescriptGarages (string Path)
			{
			// Создание файла
			FileStream FO;
			try
				{
				FO = new FileStream (Path + ".csv", FileMode.Create);
				}
			catch
				{
				return ResultCodes.CannotCreateGaragesFile;
				}
			StreamWriter SW = new StreamWriter (FO, RDGenerics.GetEncoding (RDEncodings.UTF8));

			// Запись заголовка
			const string sp = ";";
			SW.WriteLine (Label14.Text.Replace (":", "") + sp +
				Label15.Text.Replace (":", "").Replace ("•", "").Trim () + sp +
				Label16.Text.Replace (":", "").Replace ("•", "").Trim () + " (1)" + sp +
				Label16.Text.Replace (":", "").Replace ("•", "").Trim () + " (2)" + sp +
				Label17.Text.Replace (":", "").Replace ("•", "").Trim () + sp +
				Label18.Text.Replace (":", "").Replace ("•", "").Trim () + sp +
				GR_BulletsProof.Text + sp + GR_FireProof.Text + sp +
				GR_ExplProof.Text + sp + GR_DamageProof.Text);

			// Запись таблицы
			string model, imm, c1, c2, radio, bomb;

			for (int i = 0; i < GaragesList.Items.Count; i++)
				{
				// Запрос
				if (!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + i),
						(UInt16)GaragesParCodes.CarModel, out model) ||
					!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + i),
						(UInt16)GaragesParCodes.Immunity, out imm) ||
					!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + i),
						(UInt16)GaragesParCodes.PrimaryColor, out c1) ||
					!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + i),
						(UInt16)GaragesParCodes.SecondaryColor, out c2) ||
					!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + i),
						(UInt16)GaragesParCodes.RadioStation, out radio) ||
					!GetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + i),
						(UInt16)GaragesParCodes.BombType, out bomb))
					{
					SW.Close ();
					FO.Close ();

					return ResultCodes.ErrorLoadGR;
					}

				// Гараж и модель
				SW.Write (GaragesList.Items[i].ToString () + sp);

				SW.Write (ParametersValues.FindCar (ParametersValues.CarsListTypes.ForGarages,
					(int)decimal.Parse (model)).Name + sp);

				// Цвета
				Color color1 = cc.Colors[(int)decimal.Parse (c1)];
				Color color2 = cc.Colors[(int)decimal.Parse (c2)];
				SW.Write (color1.R + " " + color1.G + " " + color1.B + sp +
					color2.R + " " + color2.G + " " + color2.B + sp);

				// Радио
				SW.Write (ParametersValues.FindRadio ((int)decimal.Parse (radio)).Name + sp);

				// Бомба
				SW.Write (ParametersValues.FindBomb ((int)decimal.Parse (bomb)).Name + sp);

				// Флаги
				int d = (int)decimal.Parse (imm);
				SW.Write (((d & 0x1) != 0 ? "V" : " ") + sp);
				SW.Write (((d & 0x2) != 0 ? "V" : " ") + sp);
				SW.Write (((d & 0x4) != 0 ? "V" : " ") + sp);
				SW.WriteLine ((d & 0x8) != 0 ? "V" : " ");
				}

			// Завершено
			SW.Close ();
			FO.Close ();
			return ResultCodes.SaveSuccess;
			}

		// Изменена модель авто
		private void GR_CarModel_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.CarModel,
				ParametersValues.CarsForGarages[GR_CarModel.SelectedIndex].ID.ToString ());
			}

		// Изменены цвета авто
		private void GR_CarColor1_ValueChanged (object sender, EventArgs e)
			{
			GDColorLabel1.ForeColor = cc.Colors[(int)GR_CarColor1.Value];

			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.PrimaryColor, GR_CarColor1.Value.ToString ());
			}

		private void GR_CarColor2_ValueChanged (object sender, EventArgs e)
			{
			GDColorLabel2.ForeColor = cc.Colors[(int)GR_CarColor2.Value];

			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.SecondaryColor, GR_CarColor2.Value.ToString ());
			}

		// Изменена радиостанция
		private void GR_Radio_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.RadioStation, ParametersValues.Radios[GR_Radio.SelectedIndex].ID.ToString ());
			}

		// Изменён вид минирования
		private void GR_Bomb_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.BombType, ParametersValues.Bombs[GR_Bomb.SelectedIndex].ID.ToString ());
			}

		// Изменены флаги защиты
		private void GR_FireProof_CheckedChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			int f = 0;
			if (GR_BulletsProof.Checked)
				f |= 0x1;

			if (GR_FireProof.Checked)
				f |= 0x2;

			if (GR_ExplProof.Checked)
				f |= 0x4;

			if (GR_DamageProof.Checked)
				f |= 0x8;

			SetParameterValue ((OpCodes)((int)OpCodes.Garages_Base + GaragesList.SelectedIndex),
				(UInt16)GaragesParCodes.Immunity, f.ToString ());
			}

		// Выбран денежный накопитель
		private void AssetList_SelectedIndexChanged (object sender, EventArgs e)
			{
			loading = true;

			if (AssetList.SelectedIndex >= 0)
				PU_AssetValue.Value = pu.GetAssetMaximum (AssetList.SelectedIndex);

			loading = false;
			}

		// Изменено значение накопителя
		private void PU_AssetValue_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			if (AssetList.SelectedIndex >= 0)
				pu.SetAssetMaximum (AssetList.SelectedIndex, (uint)PU_AssetValue.Value);
			}

		// Кнопка позволяет посмотреть, где находится накопитель
		private void PickupViewCoords_Click (object sender, EventArgs e)
			{
			if (AssetList.SelectedIndex >= 0)
				cp.PickCoords ((decimal)pu.GetAssetX (AssetList.SelectedIndex),
					(decimal)pu.GetAssetY (AssetList.SelectedIndex),
					(decimal)pu.GetAssetZ (AssetList.SelectedIndex), 0, true);
			}

		// Выбрана банда для редактирования
		private void GangsList_SelectedIndexChanged (object sender, EventArgs e)
			{
			loading = true;

			string car, p1, p2, w1, w2;
			if (!GetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.CarModel, out car) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.PrimaryPedModel, out p1) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.SecondaryPedModel, out p2) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.PrimaryWeapon, out w1) ||
				!GetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.SecondaryWeapon, out w2))
				{
				loading = false;
				return;
				}

			decimal d1 = decimal.Parse (car);
			GD_CarModel.SelectedIndex = ParametersValues.FindCarIndex (ParametersValues.CarsListTypes.ForGangs, (int)d1);

			GD_PedModel1.Value = (decimal.Parse (p1) > GD_PedModel1.Maximum) ? GD_PedModel1.Maximum : decimal.Parse (p1);
			GD_PedModel2.Value = (decimal.Parse (p2) > GD_PedModel2.Maximum) ? GD_PedModel2.Maximum : decimal.Parse (p2);

			d1 = decimal.Parse (w1);
			decimal d2 = decimal.Parse (w2);
			for (int i = 0; i < ParametersValues.Weapons.Length; i++)
				{
				if (d1 == ParametersValues.Weapons[i].ID)
					GD_Weapon1.SelectedIndex = i;

				if (d2 == ParametersValues.Weapons[i].ID)
					GD_Weapon2.SelectedIndex = i;
				}

			loading = false;
			}

		// Выбрана модель авто
		private void GD_CarModel_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.CarModel,
				ParametersValues.CarsForGangs[GD_CarModel.SelectedIndex].ID.ToString ());
			}

		// Изменён скин
		private void GD_PedModel1_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.PrimaryPedModel, GD_PedModel1.Value.ToString ());
			}

		private void GD_PedModel2_ValueChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.SecondaryPedModel, GD_PedModel2.Value.ToString ());
			}

		// Выбрано оружие
		private void GD_Weapon1_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.PrimaryWeapon,
				ParametersValues.Weapons[GD_Weapon1.SelectedIndex].ID.ToString ());
			}

		private void GD_Weapon2_SelectedIndexChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue ((OpCodes)((int)OpCodes.Gangs_Base + GangsList.SelectedIndex),
				(uint)GangsParCodes.SecondaryWeapon,
				ParametersValues.Weapons[GD_Weapon2.SelectedIndex].ID.ToString ());
			}

		// Выбор слота парковки
		private void CarGenList_ValueChanged (object sender, EventArgs e)
			{
			loading = true;
			CarGenerators.CGData cgd = cg.GetGeneratorData ((int)CarGenList.Value);

			CG_CarModel.Text = ParametersValues.FindCar (ParametersValues.CarsListTypes.ForCarGenerators,
				cgd.CarModel).Name;

			CG_X.Value = (decimal)cgd.X;
			CG_Y.Value = (decimal)cgd.Y;
			CG_Z.Value = (decimal)cgd.Z;
			CG_Rotation.Value = (decimal)cgd.Rotation;

			CG_AllowSpawn.Checked = (cgd.AllowSpawn < 0);
			CG_ForceSpawn.Checked = (cgd.ForceSpawn > 0);

			CG_CarColor1.Value = (cgd.Color1 > CG_CarColor1.Maximum) ?
				CG_CarColor1.Maximum : cgd.Color1;
			CG_CarColor2.Value = (cgd.Color2 > CG_CarColor2.Maximum) ?
				CG_CarColor2.Maximum : cgd.Color2;

			CG_AlarmProb.Value = cgd.AlarmProbability;
			CG_LockProb.Value = cgd.LockProbability;

			loading = false;

			CGColorLabel1.ForeColor = (CG_CarColor1.Value >= 0) ? cc.Colors[(int)CG_CarColor1.Value] :
				Color.FromName ("ControlDark");
			CGColorLabel2.ForeColor = (CG_CarColor2.Value >= 0) ? cc.Colors[(int)CG_CarColor2.Value] :
				Color.FromName ("ControlDark");
			}

		// Метод разворачивает описание парковок в табличный файл
		private ResultCodes SaveData_DescriptGenerators (string Path)
			{
			// Создание файла
			FileStream FO;
			try
				{
				FO = new FileStream (Path + ".csv", FileMode.Create);
				}
			catch
				{
				return ResultCodes.CannotCreateCGFile;
				}
			StreamWriter SW = new StreamWriter (FO, RDGenerics.GetEncoding (RDEncodings.UTF8));

			// Запись заголовка
			const string sp = ";";
			SW.WriteLine ("#" + sp + Label25.Text.Replace (":", "").Replace ("•", "").Trim () + sp +
				Label26.Text.Replace (":", "").Replace ("•", "").Trim () + sp +
				Label27.Text.Replace (":", "").Replace ("•", "").Trim () + ", °" + sp +
				Label28.Text.Replace (":", "").Replace ("•", "").Trim () + " (1)" + sp +
				Label28.Text.Replace (":", "").Replace ("•", "").Trim () + " (2)" + sp +
				Label29.Text.Replace (":", "").Replace ("•", "").Trim () + ", %" + sp +
				Label30.Text.Replace (":", "").Replace ("•", "").Trim () + ", %" + sp +
				CG_AllowSpawn.Text + sp + CG_ForceSpawn.Text);

			for (int i = (int)CarGenList.Minimum; i <= CarGenList.Maximum; i++)
				{
				CarGenerators.CGData cgd = cg.GetGeneratorData (i);

				// Модель
				SW.Write (i.ToString () + sp);
				SW.Write (ParametersValues.FindCar (ParametersValues.CarsListTypes.ForCarGenerators,
					cgd.CarModel).Name + sp);

				// Координаты и угол
				SW.Write (cgd.X.ToString (RDLocale.GetCulture ()) + " " +
					cgd.Y.ToString (RDLocale.GetCulture ()) + " " +
					cgd.Z.ToString (RDLocale.GetCulture ()) + sp);
				SW.Write (cgd.Rotation.ToString (RDLocale.GetCulture ()) + sp);

				// Цвета
				bool color1b = cgd.Color1 >= 0;
				Color color1 = color1b ? cc.Colors[cgd.Color1] : Color.Black;
				bool color2b = cgd.Color2 >= 0;
				Color color2 = color2b ? cc.Colors[cgd.Color2] : Color.Black;

				SW.Write (color1.R + " " + color1.G + " " + color1.B + sp +
					color2.R + " " + color2.G + " " + color2.B + sp);

				// Сигнализация и блокировка
				SW.Write (cgd.AlarmProbability.ToString (RDLocale.GetCulture ()) + sp);
				SW.Write (cgd.LockProbability.ToString (RDLocale.GetCulture ()) + sp);

				SW.Write (((cgd.AllowSpawn < 0) ? "V" : " ") + sp);
				SW.WriteLine ((cgd.ForceSpawn > 0) ? "V" : " ");
				}

			// Завершено
			SW.Close ();
			FO.Close ();
			return ResultCodes.SaveSuccess;
			}

		// Изменены настройки авто
		private void CG_CarModel_SelectedIndexChanged (object sender, EventArgs e)
			{
			CGColorLabel1.ForeColor = (CG_CarColor1.Value >= 0) ? cc.Colors[(int)CG_CarColor1.Value] :
				Color.FromName ("ControlDark");
			CGColorLabel2.ForeColor = (CG_CarColor2.Value >= 0) ? cc.Colors[(int)CG_CarColor2.Value] :
				Color.FromName ("ControlDark");

			if (loading)
				return;

			cg.SetGeneratorData ((int)CarGenList.Value,
				new CarGenerators.CGData ((Int32)ParametersValues.CarsForCG[CG_CarModel.SelectedIndex].ID,
				(float)CG_X.Value, (float)CG_Y.Value, (float)CG_Z.Value, (float)CG_Rotation.Value,
				(Int16)(CG_AllowSpawn.Checked ? -1 : 0),
				(short)CG_CarColor1.Value, (short)CG_CarColor2.Value, (uint)CG_AlarmProb.Value, (uint)CG_LockProb.Value,
				CG_ForceSpawn.Checked ? 1U : 0U));
			}

		// Выбор координат
		private void CarGenGetCoords_Click (object sender, EventArgs e)
			{
			// Запуск выбора
			cp.PickCoords (CG_X.Value, CG_Y.Value, CG_Z.Value, CG_Rotation.Value, false);

			// Получение результатов
			loading = true;     // Чтобы не выполнять одно и то же 4 раза
			CG_X.Value = cp.PickedX;
			CG_Y.Value = cp.PickedY;
			CG_Z.Value = cp.PickedZ;
			loading = false;
			CG_Rotation.Value = cp.PickedRot;
			}

		// Загрузка параметров парковок
		private void LoadCG_Click (object sender, EventArgs e)
			{
			OCGDialog.FileName = "";
			OCGDialog.ShowDialog ();
			}

		// Файл выбран
		private void OCGDialog_FileOk (object sender, CancelEventArgs e)
			{
			// Загрузка файла статистики
			ResultCodes res = BExplorerLib.SaveData_LoadParametersFile (LoadableParameters.Generators,
				OCGDialog.FileName);

			if (res == ResultCodes.LoadSuccess)
				RDInterface.MessageBox (RDMessageTypes.Success_Center,
					RDLocale.GetText ("Result_" + res.ToString ()), defaultTimeout);
			else
				RDInterface.MessageBox (RDMessageTypes.Warning_Center, RDLocale.GetText ("CGLoadingError") +
					RDLocale.GetText ("Result_" + res.ToString ()));

			// Обновление загруженных данных сохранения
			LoadParameters ();
			}

		// Выгрузка параметров парковок
		private void SaveCG_Click (object sender, EventArgs e)
			{
			// Запись параметров
			cg.SaveGenerators (!AbortSorting.Checked);
			CGCountLabel.Text = RDLocale.GetText ("CGSaved") + cg.ActiveGeneratorsCount.ToString ();

			// Вызов окна
			saveMode = SaveableParameters.Generators;

			SStatsDialog.FileName = saveMode.ToString ();
			SStatsDialog.ShowDialog ();
			}

		// Установка флага бесконечных патронов
		private void ST_InfBullets_CheckedChanged (object sender, EventArgs e)
			{
			if (loading)
				return;

			SetParameterValue (OpCodes.InfiniteAmmo, 0, ST_InfBullets.Checked ? "1" : "0");
			}

		// Сброс потенциально опасных параметров
		private void DangerousReset_Click (object sender, EventArgs e)
			{
			// Контроль
			if (RDInterface.LocalizedMessageBox (RDMessageTypes.Warning_Center, "DangerousResetMessage",
				RDLDefaultTexts.Button_YesNoFocus, RDLDefaultTexts.Button_No) ==
				RDMessageButtons.ButtonTwo)
				return;

			// Выполнение
			ResultCodes res = BExplorerLib.SaveData_FixFile4 ();

			if (res == ResultCodes.FileFixed)
				RDInterface.MessageBox (RDMessageTypes.Success_Center,
					RDLocale.GetText ("Result_" + res.ToString ()), defaultTimeout);
			else
				RDInterface.MessageBox (RDMessageTypes.Warning_Center,
				RDLocale.GetText ("Result_" + res.ToString ()));
			}

		// Загрузка параметров гаражей
		private void LoadGarages_Click (object sender, EventArgs e)
			{
			OGDialog.FileName = "";
			OGDialog.ShowDialog ();
			}

		// Файл выбран
		private void OGDialog_FileOk (object sender, CancelEventArgs e)
			{
			// Загрузка файла статистики
			ResultCodes res = BExplorerLib.SaveData_LoadParametersFile (LoadableParameters.Garages, OGDialog.FileName);

			if (res == ResultCodes.LoadSuccess)
				RDInterface.MessageBox (RDMessageTypes.Success_Center,
					RDLocale.GetText ("Result_" + res.ToString ()), defaultTimeout);
			else
				RDInterface.MessageBox (RDMessageTypes.Warning_Center, RDLocale.GetText ("GRLoadingError") +
					RDLocale.GetText ("Result_" + res.ToString ()));

			// Обновление загруженных данных сохранения
			LoadParameters ();
			}

		// Выгрузка параметров парковок
		private void SaveGarages_Click (object sender, EventArgs e)
			{
			// Вызов окна
			saveMode = SaveableParameters.Garages;

			SStatsDialog.FileName = saveMode.ToString ();
			SStatsDialog.ShowDialog ();
			}

		// Применение рекомендуемых настроек
		private void RecommendedSettings_Click (object sender, EventArgs e)
			{
			DP_Date.Value = DP_Time.Value = DateTime.Now;
			DP_GameTime.Value = new DateTime (2000, 1, 1, 5, 1, 0);
			DP_Weather.SelectedIndex = 0;
			PL_CurArmor.Value = PL_CurArmor.Maximum;
			PL_MWL.SelectedIndex = 0;
			}

		// Выбор языка интерфейса
		private void LocalizeForm ()
			{
			// Настройка списков
			string s;

			for (int i = 0; i < ParametersValues.Weathers.Length; i++)
				{
				s = RDLocale.GetText ("Weathers_" + ParametersValues.Weathers[i].Name);
				if (DP_Weather.Items.Count < ParametersValues.Weathers.Length)
					DP_Weather.Items.Add (s);
				else
					DP_Weather.Items[i] = s;
				}

			for (int i = 0; i < ParametersValues.CameraPositions.Length; i++)
				{
				s = RDLocale.GetText ("CameraPositions_" + ParametersValues.CameraPositions[i].Name);
				if (DP_CameraPos.Items.Count < ParametersValues.CameraPositions.Length)
					DP_CameraPos.Items.Add (s);
				else
					DP_CameraPos.Items[i] = s;
				}

			for (int i = 0; i < ParametersValues.Suits.Length; i++)
				{
				s = RDLocale.GetText ("Suits_" + ParametersValues.Suits[i].Name);
				if (PL_Suit.Items.Count < ParametersValues.Suits.Length)
					PL_Suit.Items.Add (s);
				else
					PL_Suit.Items[i] = s;
				}

			for (int i = 0; i < BExplorerLib.WeaponsCount; i++)
				{
				s = RDLocale.GetText ("WeaponSlot") + (i + 1).ToString ();
				if (WeaponsList.Items.Count < BExplorerLib.WeaponsCount)
					WeaponsList.Items.Add (s);
				else
					WeaponsList.Items[i] = s;
				}

			for (int i = 0; i < ParametersValues.Weapons.Length; i++)
				{
				s = RDLocale.GetText ("Weapons_" + ParametersValues.Weapons[i].Name);
				if (PL_Weapon.Items.Count < ParametersValues.Weapons.Length)
					{
					PL_Weapon.Items.Add (s);
					GD_Weapon1.Items.Add (s);
					GD_Weapon2.Items.Add (s);
					}
				else
					{
					PL_Weapon.Items[i] = s;
					GD_Weapon1.Items[i] = s;
					GD_Weapon2.Items[i] = s;
					}
				}

			for (int i = 0; i < BExplorerLib.GaragesCount; i++)
				{
				s = RDLocale.GetText ("Garages_" + (i + 1).ToString ());
				if (GaragesList.Items.Count < BExplorerLib.GaragesCount)
					GaragesList.Items.Add (s);
				else
					GaragesList.Items[i] = s;
				}

			for (int i = 0; i < ParametersValues.Bombs.Length; i++)
				{
				s = RDLocale.GetText ("Bombs_" + ParametersValues.Bombs[i].Name);
				if (GR_Bomb.Items.Count < ParametersValues.Bombs.Length)
					GR_Bomb.Items.Add (s);
				else
					GR_Bomb.Items[i] = s;
				}

			for (int i = 0; i < BExplorerLib.GangsCount; i++)
				{
				s = RDLocale.GetText ("Gangs_" + (i + 1).ToString ());
				if (GangsList.Items.Count < BExplorerLib.GangsCount)
					GangsList.Items.Add (s);
				else
					GangsList.Items[i] = s;
				}

			// Настройка диалогов
			OFDialog.Filter = SFDialog.Filter = string.Format (RDLocale.GetText ("OFDialogFilter"),
				savesExtension);

			SStatsDialog.Filter = RDLocale.GetText ("GenericSettingsDialogFilter");
			OStatsDialog.Filter = RDLocale.GetText ("OStatsDialogFilter") +
				SStatsDialog.Filter;
			OCGDialog.Filter = RDLocale.GetText ("OCGDialogFilter") +
				SStatsDialog.Filter;
			OGDialog.Filter = RDLocale.GetText ("OGDialogFilter") +
				SStatsDialog.Filter;

			OFDialog.Title = OStatsDialog.Title = OCGDialog.Title = OGDialog.Title =
				RDLocale.GetText ("OFDialogTitle");
			SFDialog.Title = SStatsDialog.Title = RDLocale.GetText ("SFDialogTitle");

			// Настройка контролов
			RDLocale.SetControlsText (FileTab);
			ExitButton.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Exit);
			RDLocale.SetControlsText (DPTab);
			RDLocale.SetControlsText (PLTab);
			RDLocale.SetControlsText (GRTab);
			LoadGarages.Text = RDLocale.GetText ("LoadParameters");
			SaveGarages.Text = RDLocale.GetText ("SaveParameters");
			RDLocale.SetControlsText (PUTab);
			RDLocale.SetControlsText (GDTab);
			RDLocale.SetControlsText (CGTab);
			LoadCG.Text = RDLocale.GetText ("LoadParameters");
			SaveCG.Text = RDLocale.GetText ("SaveParameters");
			RDLocale.SetControlsText (STTab);

			FileTab.Text = RDLocale.GetText ("MainTab_FileTab");
			DPTab.Text = RDLocale.GetText ("MainTab_DPTab");
			PLTab.Text = RDLocale.GetText ("MainTab_PLTab");
			GRTab.Text = RDLocale.GetText ("MainTab_GRTab");
			PUTab.Text = RDLocale.GetText ("MainTab_PUTab");
			GDTab.Text = RDLocale.GetText ("MainTab_GDTab");
			CGTab.Text = RDLocale.GetText ("MainTab_CGTab");
			STTab.Text = RDLocale.GetText ("MainTab_STTab");

			if (DPTab.Enabled)
				SaveInfoLabel.Text = RDLocale.GetText ("CurrentSave") + BExplorerLib.SaveData_SaveInfo;
			else
				SaveInfoLabel.Text = RDLocale.GetText ("SaveNotSpecified");
			}
		}
	}
