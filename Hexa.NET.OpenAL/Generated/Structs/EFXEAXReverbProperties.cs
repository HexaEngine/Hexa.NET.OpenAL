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
	/// To be documented.
	/// </summary>
	[NativeName(NativeNameType.StructOrClass, "EFXEAXREVERBPROPERTIES")]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct EFXEAXReverbProperties
	{
		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flDensity")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlDensity;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flDiffusion")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlDiffusion;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flGain")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlGain;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flGainHF")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlGainHF;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flGainLF")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlGainLF;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flDecayTime")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlDecayTime;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flDecayHFRatio")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlDecayHFRatio;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flDecayLFRatio")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlDecayLFRatio;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flReflectionsGain")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlReflectionsGain;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flReflectionsDelay")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlReflectionsDelay;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flReflectionsPan")]
		[NativeName(NativeNameType.Type, "float[3]")]
		public float FlReflectionsPan_0;
		public float FlReflectionsPan_1;
		public float FlReflectionsPan_2;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flLateReverbGain")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlLateReverbGain;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flLateReverbDelay")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlLateReverbDelay;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flLateReverbPan")]
		[NativeName(NativeNameType.Type, "float[3]")]
		public float FlLateReverbPan_0;
		public float FlLateReverbPan_1;
		public float FlLateReverbPan_2;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flEchoTime")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlEchoTime;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flEchoDepth")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlEchoDepth;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flModulationTime")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlModulationTime;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flModulationDepth")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlModulationDepth;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flAirAbsorptionGainHF")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlAirAbsorptionGainHF;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flHFReference")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlHFReference;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flLFReference")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlLFReference;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "flRoomRolloffFactor")]
		[NativeName(NativeNameType.Type, "float")]
		public float FlRoomRolloffFactor;

		/// <summary>
		/// To be documented.
		/// </summary>
		[NativeName(NativeNameType.Field, "iDecayHFLimit")]
		[NativeName(NativeNameType.Type, "int")]
		public int IDecayHFLimit;


		/// <summary>
		/// To be documented.
		/// </summary>
		public unsafe EFXEAXReverbProperties(float flDensity = default, float flDiffusion = default, float flGain = default, float flGainHF = default, float flGainLF = default, float flDecayTime = default, float flDecayHFRatio = default, float flDecayLFRatio = default, float flReflectionsGain = default, float flReflectionsDelay = default, float* flReflectionsPan = default, float flLateReverbGain = default, float flLateReverbDelay = default, float* flLateReverbPan = default, float flEchoTime = default, float flEchoDepth = default, float flModulationTime = default, float flModulationDepth = default, float flAirAbsorptionGainHF = default, float flHFReference = default, float flLFReference = default, float flRoomRolloffFactor = default, int iDecayHFLimit = default)
		{
			FlDensity = flDensity;
			FlDiffusion = flDiffusion;
			FlGain = flGain;
			FlGainHF = flGainHF;
			FlGainLF = flGainLF;
			FlDecayTime = flDecayTime;
			FlDecayHFRatio = flDecayHFRatio;
			FlDecayLFRatio = flDecayLFRatio;
			FlReflectionsGain = flReflectionsGain;
			FlReflectionsDelay = flReflectionsDelay;
			if (flReflectionsPan != default(float*))
			{
				FlReflectionsPan_0 = flReflectionsPan[0];
				FlReflectionsPan_1 = flReflectionsPan[1];
				FlReflectionsPan_2 = flReflectionsPan[2];
			}
			FlLateReverbGain = flLateReverbGain;
			FlLateReverbDelay = flLateReverbDelay;
			if (flLateReverbPan != default(float*))
			{
				FlLateReverbPan_0 = flLateReverbPan[0];
				FlLateReverbPan_1 = flLateReverbPan[1];
				FlLateReverbPan_2 = flLateReverbPan[2];
			}
			FlEchoTime = flEchoTime;
			FlEchoDepth = flEchoDepth;
			FlModulationTime = flModulationTime;
			FlModulationDepth = flModulationDepth;
			FlAirAbsorptionGainHF = flAirAbsorptionGainHF;
			FlHFReference = flHFReference;
			FlLFReference = flLFReference;
			FlRoomRolloffFactor = flRoomRolloffFactor;
			IDecayHFLimit = iDecayHFLimit;
		}

		/// <summary>
		/// To be documented.
		/// </summary>
		public unsafe EFXEAXReverbProperties(float flDensity = default, float flDiffusion = default, float flGain = default, float flGainHF = default, float flGainLF = default, float flDecayTime = default, float flDecayHFRatio = default, float flDecayLFRatio = default, float flReflectionsGain = default, float flReflectionsDelay = default, Span<float> flReflectionsPan = default, float flLateReverbGain = default, float flLateReverbDelay = default, Span<float> flLateReverbPan = default, float flEchoTime = default, float flEchoDepth = default, float flModulationTime = default, float flModulationDepth = default, float flAirAbsorptionGainHF = default, float flHFReference = default, float flLFReference = default, float flRoomRolloffFactor = default, int iDecayHFLimit = default)
		{
			FlDensity = flDensity;
			FlDiffusion = flDiffusion;
			FlGain = flGain;
			FlGainHF = flGainHF;
			FlGainLF = flGainLF;
			FlDecayTime = flDecayTime;
			FlDecayHFRatio = flDecayHFRatio;
			FlDecayLFRatio = flDecayLFRatio;
			FlReflectionsGain = flReflectionsGain;
			FlReflectionsDelay = flReflectionsDelay;
			if (flReflectionsPan != default(Span<float>))
			{
				FlReflectionsPan_0 = flReflectionsPan[0];
				FlReflectionsPan_1 = flReflectionsPan[1];
				FlReflectionsPan_2 = flReflectionsPan[2];
			}
			FlLateReverbGain = flLateReverbGain;
			FlLateReverbDelay = flLateReverbDelay;
			if (flLateReverbPan != default(Span<float>))
			{
				FlLateReverbPan_0 = flLateReverbPan[0];
				FlLateReverbPan_1 = flLateReverbPan[1];
				FlLateReverbPan_2 = flLateReverbPan[2];
			}
			FlEchoTime = flEchoTime;
			FlEchoDepth = flEchoDepth;
			FlModulationTime = flModulationTime;
			FlModulationDepth = flModulationDepth;
			FlAirAbsorptionGainHF = flAirAbsorptionGainHF;
			FlHFReference = flHFReference;
			FlLFReference = flLFReference;
			FlRoomRolloffFactor = flRoomRolloffFactor;
			IDecayHFLimit = iDecayHFLimit;
		}


	}

}
