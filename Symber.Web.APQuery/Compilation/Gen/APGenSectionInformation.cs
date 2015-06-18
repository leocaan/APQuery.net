
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Contains metadata about an individual section within the configuration hierarchy.
	/// This class cannot be inherited.
	/// </summary>
	public sealed class APGenSectionInformation
	{

		#region [ Fields ]


		private string _source;
		private string _name;
		private string _typeName;
		private string _rawXml;


		#endregion


		#region [ Constructors ]


		internal APGenSectionInformation()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the name of the object that corresponds to a configuration attribute.
		/// </summary>
		public string Source
		{
			get { return _source; }
			set { _source = value; }
		}


		/// <summary>
		/// Gets the name of the associated configuration section.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}


		/// <summary>
		/// Gets the name of the associated configuration section.
		/// </summary>
		public string SectionName
		{
			get { return _name; }
		}


		/// <summary>
		/// Gets or sets the section class name.
		/// </summary>
		public string Type
		{
			get { return _typeName; }
			set { _typeName = value; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Returns an XML node object that represents the associated configuration-section object.
		/// </summary>
		/// <returns>The XML representation for this configuration section.</returns>
		public string GetRawXml()
		{
			return _rawXml;
		}


		/// <summary>
		/// Sets the object to an XML representation of the associated configuration section within the configuration file.
		/// </summary>
		/// <param name="xml">The XML to use.</param>
		public void SetRawXml(string xml)
		{
			_rawXml = xml;
		}


		#endregion


		#region [ Internal Melthods ]


		internal void SetName(string name)
		{
			this._name = name;
		}


		#endregion

	}
}
