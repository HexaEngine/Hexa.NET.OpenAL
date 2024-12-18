// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HexaGen.Runtime;

namespace Hexa.NET.OpenAL
{
	/// <summary>
	/// Opaque device handle <br/>
	/// </summary>
	[NativeName(NativeNameType.StructOrClass, "ALCdevice")]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct ALCdevice
	{


	}

	/// <summary>
	/// Opaque device handle <br/>
	/// </summary>
	[NativeName(NativeNameType.Typedef, "ALCdevice")]
	#if NET5_0_OR_GREATER
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	#endif
	public unsafe struct ALCdevicePtr : IEquatable<ALCdevicePtr>
	{
		public ALCdevicePtr(ALCdevice* handle) { Handle = handle; }

		public ALCdevice* Handle;

		public bool IsNull => Handle == null;

		public static ALCdevicePtr Null => new ALCdevicePtr(null);

		public ALCdevice this[int index] { get => Handle[index]; set => Handle[index] = value; }

		public static implicit operator ALCdevicePtr(ALCdevice* handle) => new ALCdevicePtr(handle);

		public static implicit operator ALCdevice*(ALCdevicePtr handle) => handle.Handle;

		public static bool operator ==(ALCdevicePtr left, ALCdevicePtr right) => left.Handle == right.Handle;

		public static bool operator !=(ALCdevicePtr left, ALCdevicePtr right) => left.Handle != right.Handle;

		public static bool operator ==(ALCdevicePtr left, ALCdevice* right) => left.Handle == right;

		public static bool operator !=(ALCdevicePtr left, ALCdevice* right) => left.Handle != right;

		public bool Equals(ALCdevicePtr other) => Handle == other.Handle;

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is ALCdevicePtr handle && Equals(handle);

		/// <inheritdoc/>
		public override int GetHashCode() => ((nuint)Handle).GetHashCode();

		#if NET5_0_OR_GREATER
		private string DebuggerDisplay => string.Format("ALCdevicePtr [0x{0}]", ((nuint)Handle).ToString("X"));
		#endif
	}

}
