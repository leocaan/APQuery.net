using System;
using System.IO;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Generate host interface.
	/// </summary>
	public interface IAPGenHost
	{

		/// <summary>
		/// Initialize generate host.
		/// </summary>
		void Init();


		/// <summary>
		/// Decrypt section.
		/// </summary>
		/// <param name="encryptedXml">Encrypted xml.</param>
		/// <returns>Decrypted xml.</returns>
		string DecryptSection(string encryptedXml);


		/// <summary>
		/// Encrypt section.
		/// </summary>
		/// <param name="rowXml">Raw xml.</param>
		/// <returns>Encrypted xml.</returns>
		string EncryptSection(string rowXml);


		/// <summary>
		/// Get generate type.
		/// </summary>
		/// <param name="typeName">Type name.</param>
		/// <param name="throwOnError">Whether to throw an exception on error.</param>
		/// <returns>Generate type.</returns>
		Type GetGenType(string typeName, bool throwOnError);


		/// <summary>
		/// Get generate type name.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <returns>Generate type name.</returns>
		string GetGenTypeName(Type type);


		/// <summary>
		/// Get stream name.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <returns>Stream name.</returns>
		string GetStreamName(string path);


		/// <summary>
		/// Open a stream for read.
		/// </summary>
		/// <param name="streamName">Stream name.</param>
		/// <returns>Stream.</returns>
		Stream OpenStreamForRead(string streamName);

	}
}
