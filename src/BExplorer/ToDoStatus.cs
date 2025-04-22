using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс обеспечивает получение ToDo-статуса сохранения (списка действий,
	/// необходимых для прохождения игры)
	/// </summary>
	public class ToDoStatus
		{
		/// <summary>
		/// Вспомогательный класс-описатель элемента ToDo-статуса
		/// </summary>
		public class ToDoStatusElement
			{
			/// <summary>
			/// Возвращает описание элемента ToDo-статуса (действия, необходимого для прохождения игры)
			/// </summary>
			public string ElementDescription
				{
				get
					{
					return elementDescription;
					}
				}
			private string elementDescription;

			/// <summary>
			/// Возвращает состояние элемента (завершён/не завершён)
			/// </summary>
			public bool Completed
				{
				get
					{
					return completed;
					}
				}
			private bool completed;

			/// <summary>
			/// Конструктор. Создаёт элемент ToDo-статуса
			/// </summary>
			/// <param name="Completed">Состояние элемента</param>
			/// <param name="ElementDescription">Описание элемента</param>
			public ToDoStatusElement (string ElementDescription, bool Completed)
				{
				elementDescription = ElementDescription;
				completed = Completed;
				}
			}

		/// <summary>
		/// Возвращает список полученных элементов ToDo-статуса
		/// </summary>
		public List<ToDoStatusElement> Elements
			{
			get
				{
				return elements;
				}
			}
		private List<ToDoStatusElement> elements = [];

		/// <summary>
		/// Конструктор. Получает ToDo-статус сохранения
		/// </summary>
		public ToDoStatus ()
			{
			// Получение статуса
			string status = BExplorerLib.SaveData_ToDoStatus;
			if (status == null)
				return;

			// Разделение статуса на элементы
			string count, all;

			count = status.Substring (40, 2).Trim ();
			all = status.Substring (42, 2);
			elements.Add (new ToDoStatusElement (string.Format (RDLocale.GetText ("ToDoStatus01"),
				count, all), count == all));

			count = status.Substring (44, 2).Trim ();
			all = status.Substring (46, 2);
			elements.Add (new ToDoStatusElement (string.Format (RDLocale.GetText ("ToDoStatus02"),
				count, all), count == all));

			count = status.Substring (48, 2).Trim ();
			all = status.Substring (50, 2);
			elements.Add (new ToDoStatusElement (string.Format (RDLocale.GetText ("ToDoStatus03"),
				count, all), count == all));

			count = status.Substring (52, 3).Trim ();
			all = status.Substring (55, 3);
			elements.Add (new ToDoStatusElement (string.Format (RDLocale.GetText ("ToDoStatus04"),
				count, all), count == all));

			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus05"),
				status.Substring (3, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus06"),
				status.Substring (4, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus07"),
				status.Substring (13, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus08"),
				status.Substring (19, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus09"),
				status.Substring (21, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus10"),
				status.Substring (14, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus11"),
				status.Substring (23, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus12"),
				status.Substring (24, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus13"),
				status.Substring (25, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus14"),
				status.Substring (26, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus15"),
				status.Substring (15, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus16"),
				status.Substring (20, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus17"),
				status.Substring (22, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus18"),
				status.Substring (33, 1) == "1"));

			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus19"),
				status.Substring (16, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus20"),
				status.Substring (17, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus21"),
				status.Substring (18, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus22"),
				status.Substring (1, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus23"),
				status.Substring (2, 1) == "1"));

			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus24"),
				status.Substring (12, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus25"),
				status.Substring (5, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus26"),
				status.Substring (6, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus27"),
				status.Substring (7, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus28"),
				status.Substring (8, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus29"),
				status.Substring (9, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus30"),
				status.Substring (10, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus31"),
				status.Substring (11, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus32"),
				status.Substring (27, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus33"),
				status.Substring (28, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus34"),
				status.Substring (29, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus35"),
				status.Substring (30, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus36"),
				status.Substring (31, 1) == "1"));
			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus37"),
				status.Substring (32, 1) == "1"));

			count = status.Substring (34, 1);
			all = status.Substring (35, 1);
			elements.Add (new ToDoStatusElement (string.Format (RDLocale.GetText ("ToDoStatus38"),
				count, all), count == all));

			count = status.Substring (36, 2).Trim ();
			all = status.Substring (38, 2);
			elements.Add (new ToDoStatusElement (string.Format (RDLocale.GetText ("ToDoStatus39"),
				count, all), count == all));

			elements.Add (new ToDoStatusElement (RDLocale.GetText ("ToDoStatus40"),
				status.Substring (0, 1) == "1"));
			}
		}
	}
