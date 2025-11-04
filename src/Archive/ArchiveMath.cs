using System;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает отдельный элемент архива IMG
	/// </summary>
	public class IMGItem: IComparable<IMGItem>
		{
		/// <summary>
		/// Возвращает смещение (в секторах) в архиве для файла,
		/// представляемого этим экземпляром
		/// </summary>
		public UInt32 Offset
			{
			get
				{
				return offset;
				}
			}
		private UInt32 offset;

		/// <summary>
		/// Возвращает размер (в секторах) в архиве для файла,
		/// представляемого этим экземпляром
		/// </summary>
		public UInt32 Size
			{
			get
				{
				return size;
				}
			}
		private UInt32 size;

		/// <summary>
		/// Возвращает имя в архиве для файла, представляемого этим экземпляром
		/// </summary>
		public string FileName
			{
			get
				{
				return fileName;
				}
			}
		private string fileName;

		/// <summary>
		/// Возвращает максимальную допустимую длину имени файла в архиве
		/// </summary>
		public const byte MaxFileNameLength = 24;

		/// <summary>
		/// Возвращает путь к файлу данных архива
		/// </summary>
		public const string IMGArchiveDataPath = IMGArchiveSubPath + IMGArchiveDataExt;

		/// <summary>
		/// Возвращает путь к файлу карты архива
		/// </summary>
		public const string IMGArchiveMapPath = IMGArchiveSubPath + IMGArchiveMapExt;

		private const string IMGArchiveSubPath = "\\models\\gta3.";
		private const string IMGArchiveMapExt = "dir";
		private const string IMGArchiveDataExt = "img";

		/// <summary>
		/// Конструктор. Инициализирует экземпляр-представитель файла архива
		/// </summary>
		/// <param name="Offset">Смещение (в секторах)</param>
		/// <param name="Size">Размер (в секторах)</param>
		/// <param name="FileName">Имя файла</param>
		public IMGItem (UInt32 Offset, UInt32 Size, string FileName)
			{
			offset = Offset;
			size = Size;
			fileName = FileName;
			}

		/// <summary>
		/// Метод выполняет сравнение данного экземпляра с указанным
		/// </summary>
		/// <param name="other">Экземпляр для сравнения</param>
		public int CompareTo (IMGItem other)
			{
			return fileName.CompareTo (other.fileName);
			}
		}
	}
